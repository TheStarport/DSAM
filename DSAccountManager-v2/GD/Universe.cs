using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using DSAccountManager_v2.GD.DB;
using FLDataFile;
using System.Linq;

namespace DSAccountManager_v2.GD
{
    class Universe
    {

        #region "Unmanaged DLL load func"

        /// <summary>
        /// Unmanaged functions to access libraries
        /// </summary>
        private const int DontResolveDllReferences = 0x00000001;
        private const int LoadLibraryAsDatafile = 0x00000002;

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr LoadLibraryExA(string lpLibFileName, int hFile, int dwFlags);

        [DllImport("user32.dll", SetLastError = true)]
        static extern int LoadString(IntPtr hInstance, int uID, byte[] lpBuffer, int nBufferMax);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern int FreeLibrary(IntPtr hInstance);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern int LockResource(int hResData);

        [DllImport("kernel32.dll")]
        static extern IntPtr FindResource(IntPtr hModule, int lpID, int lpType);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr LoadResource(IntPtr hModule, IntPtr hResInfo);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern int SizeofResource(IntPtr hModule, IntPtr hResInfo);

        /// <summary>
        /// Resource dlls containing strings.
        /// </summary>
        readonly List<IntPtr> _vDlLs = new List<IntPtr>();

        private void LoadLibrary(string dllPath)
        {
            IntPtr hInstance = LoadLibraryExA(dllPath, 0, DontResolveDllReferences | LoadLibraryAsDatafile);
            _vDlLs.Add(hInstance);
        }

        #endregion



        private readonly Dictionary<uint, uint> _infocardMap = new Dictionary<uint, uint>();

        private readonly string _flDataPath;

        public readonly GameInfoSet.BasesDataTable Bases = new GameInfoSet.BasesDataTable();

        /// <summary>
        /// Initiates the Universe DB. FLPath is the path to the Freelancer directory, not DATA\EXE etc.
        /// </summary>
        /// <param name="flPath"></param>
        public Universe(string flPath)
        {
            if (!File.Exists(flPath + @"\EXE\Freelancer.ini"))
            {
                throw new Exception("Can't load Freelancer.ini!");
            }
            var flIni = new DataFile(flPath + @"\EXE\Freelancer.ini");
            _flDataPath = Path.GetFullPath(Path.Combine(flPath + @"\EXE", flIni.GetSetting("Freelancer", "data path")[0]));

            // Load the infocard map
                var ini = new DataFile(_flDataPath + @"\interface\infocardmap.ini");
                foreach (var set in ini.Sections.SelectMany(section => section.Settings))
                {
                    uint map0, map1;
                    if (!uint.TryParse(set[0], out map0) || !uint.TryParse(set[1], out map1)) 
                        //TODO: change to log
                        throw new Exception("Can't parse infocard map: " + set.String());
                    _infocardMap[map0] = map1;
                }


                // Load the string dlls.
                LoadLibrary(flPath + @"\EXE\" + @"resources.dll");
                foreach (var flResName in flIni.GetSettings("Resources", "DLL"))
                    LoadLibrary(flPath + @"\EXE\" + flResName[0]);

            //Scan INIs and bases
            //TODO there's only one universe entry, so do we need foreach universe?
            foreach (var entry in flIni.GetSettings("Data", "universe"))
            {
                //TODO: halt backgroundworker if needed

                var universeIni = new DataFile(_flDataPath + @"\" + entry[0]);

                //Load bases
                foreach (var baseSection in universeIni.GetSections("Base"))
                {
                    LoadBase(baseSection);
                }

                //Load systems
                foreach (var sysSection in universeIni.GetSections("system"))
                {
                    LoadSystem(sysSection);
                }

            }


        }


        private void LoadBase(Section sec)
        {
            var nickname = sec.GetFirstOf("nickname")[0];
            var file = _flDataPath + sec.GetFirstOf("file");

            var stIDSName = GetIDSParm(sec.GetAnySetting("ids_name", "strid_name")[0]);

            Bases.AddBasesRow(nickname, stIDSName);

            //TODO: do we rly need room data?
            //FLGameData:Ln 191
        }

        private void LoadSystem(Section sec)
        {
            var sysNick = sec.GetFirstOf("nickname")[0].ToLowerInvariant();
            var stIDSName = GetIDSParm(sec.GetAnySetting("ids_name", "strid_name")[0]);
            //string file = Directory.GetParent(ini.filePath).FullName + "\\" + section.GetSetting("file").Str(0);
            //ParseSystem(file, log);
            //TODO: Warning, hardcoded system path! :S
            var sysFile = new DataFile(_flDataPath + @"\Universe\" + sec.GetFirstOf("file")[0]);

            foreach (var obj in sysFile.GetSections("object"))
            {
                
            }

        }


        /// <summary>
        /// Gets ID String (description from DLL) 
        /// </summary>
        /// <param name="idName"></param>
        /// <returns></returns>
        private string GetIDSParm(string idName)
        {
            var stInfo = "";
            if (idName == null) return stInfo;

            uint idsInfo;

            if (!uint.TryParse(idName,out idsInfo)) 
                throw new Exception("Couldn't parse idName " + idName);

            stInfo = GetIDString(idsInfo);

            if (stInfo == null)
                throw new Exception("ids_info not found " + idsInfo);

            stInfo = stInfo.Trim();
            return stInfo;
        }


        /// <summary>
        /// Return the string for the specified IDS. Note that this function
        /// works only for ascii strings.
        /// </summary>
        /// <param name="iIDS"></param>
        /// <returns>The string or null if it cannot be found.</returns>
        private string GetIDString(uint iIDS)
        {
            //TODO: i think it can be optimized as well, leaving the legacy version for now
            var iDLL = (int)(iIDS / 0x10000);
            var resId = (int)iIDS - (iDLL * 0x10000);

            if (_vDlLs.Count <= iDLL) return null;


            var hInstance = _vDlLs[iDLL];
            if (hInstance == IntPtr.Zero) return null;

            var bufName = new byte[10000];
            var len = LoadString(hInstance, resId, bufName, bufName.Length);
            if (len > 0)
            {
                return System.Text.Encoding.ASCII.GetString(bufName, 0, len);
            }

            var hFindRes = FindResource(hInstance, resId, 23);
            if (hFindRes == IntPtr.Zero) return null;

            var resContent = LoadResource(hInstance, hFindRes);
            if (resContent == IntPtr.Zero) return null;

            var size = SizeofResource(hInstance, hFindRes);
            var bufInfo = new byte[size];
            Marshal.Copy(resContent, bufInfo, 0, size);
            return System.Text.Encoding.Unicode.GetString(bufInfo, 0, size);
        }

    }
}
