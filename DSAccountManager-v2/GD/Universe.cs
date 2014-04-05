using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using DSAccountManager_v2.FileTypes;
using DSAccountManager_v2.GD.DB;
using FLDataFile;
using System.Linq;
using LogDispatcher;

namespace DSAccountManager_v2.GD
{
    class Universe : IDisposable
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
// ReSharper disable once InconsistentNaming
        readonly List<IntPtr> _vDLLs = new List<IntPtr>();

        private void LoadLibrary(string dllPath)
        {
            var hInstance = LoadLibraryExA(dllPath, 0, DontResolveDllReferences | LoadLibraryAsDatafile);
            _vDLLs.Add(hInstance);
        }

        #endregion



        private readonly Dictionary<uint, uint> _infocardMap = new Dictionary<uint, uint>();

        private readonly string _flDataPath;

        private readonly Dictionary<string,EquipTypes> _hpMap = new Dictionary<string, EquipTypes>();

        /// <summary>
        /// Mapper for DataMaps.ini. Used for determining hardpoint types and gun types.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private EquipTypes MapStringToType(string str)
        {
            switch (str)
            {
                case "misc":
                    return EquipTypes.Misc;
                case "countermeasuredropper":
                    return EquipTypes.CountermeasureDropper;
                case "turret":
                    return EquipTypes.Turret;
                case "minedropper":
                    return EquipTypes.MineDropper;
                case "projectile":
                    return EquipTypes.Projectile;
                case "thruster":
                    return EquipTypes.Thruster;
                case "shield":
                    return EquipTypes.ShieldGen;
                case "gun":
                    return EquipTypes.Gun;
                case "engine":
                    return EquipTypes.Engine;
                case "light":
                    return EquipTypes.Light;
                case "power":
                    return EquipTypes.Powerplant;
            }
            return EquipTypes.Misc;
        }

        //public readonly GameInfoSet.BasesDataTable Bases = new GameInfoSet.BasesDataTable();
        //public readonly GameInfoSet.SystemsDataTable Systems = new GameInfoSet.SystemsDataTable();
        //public readonly GameInfoSet.EquipmentDataTable Equipment = new GameInfoSet.EquipmentDataTable();
        //public readonly GameInfoSet.FactionDataTable Factions = new GameInfoSet.FactionDataTable();
        public readonly GameInfoSet Gis = new GameInfoSet();
        /// <summary>
        /// Initiates the Universe DB. FLPath is the path to the Freelancer directory, not DATA\EXE etc.
        /// </summary>
        /// <param name="flPath"></param>
        public Universe(string flPath)
        {

            LogDispatcher.LogDispatcher.NewMessage(LogType.Debug, "Parsing file {0} ...", flPath + @"\DataMaps.ini");
            if (!File.Exists(flPath + @"\DataMaps.ini"))
            {
                LogDispatcher.LogDispatcher.NewMessage(LogType.Fatal, "Can't find DataMaps.ini!");
                throw new Exception("Can't load DataMaps.ini!");
            }
            var dMapIni = new DataFile(flPath + @"\DataMaps.ini");

            foreach (var set in dMapIni.GetSettings("HPMap", "map"))
            {
                _hpMap.Add(set[0], MapStringToType(set[1]));
            }


            LogDispatcher.LogDispatcher.NewMessage(LogType.Debug, "Parsing file {0} ...", flPath + @"\EXE\Freelancer.ini");
            if (!File.Exists(flPath + @"\EXE\Freelancer.ini"))
            {
                LogDispatcher.LogDispatcher.NewMessage(LogType.Fatal, "Can't find Freelancer.ini!");
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
                    {
                        LogDispatcher.LogDispatcher.NewMessage(LogType.Error, "Can't parse infocard map: {0}", set.String());
                        continue;
                    }
                        
                        //throw new Exception("Can't parse infocard map: " + set.String());
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

            foreach (var entry in flIni.GetSettings("Data", "equipment"))
            {
                var equipFile = new DataFile(_flDataPath + @"\" + entry[0]);

                foreach (var eqSection in equipFile.Sections)
                {
                    LoadEquipSection(eqSection);
                }
            }

            foreach (var entry in flIni.GetSettings("Data", "groups"))
            {
                var facFile = new DataFile(_flDataPath + @"\" + entry[0]);

                foreach (var faSection in facFile.GetSections("Group"))
                {
                    LoadFaction(faSection);
                }
            }

            foreach (var entry in flIni.GetSettings("Data", "ships"))
            {
                var shFile = new DataFile(_flDataPath + @"\" + entry[0]);

                foreach (var shSection in shFile.GetSections("Ship"))
                {
                    LoadShip(shSection);
                }
            }

            //TODO: Line 473, default loadouts

            LogDispatcher.LogDispatcher.NewMessage(LogType.Debug, "Parsing loadouts...");
            var loFile = new DataFile(_flDataPath + @"\equipment\goods.ini");

            foreach (var loSection in loFile.GetSections("Good").Where(w => w.GetFirstOf("category")[0] == "ship"))
            {
                LoadLoadout(loSection,loFile);
            }

        }

        private void LoadLoadout(Section sec,DataFile file)
        {
            //if (sec.GetFirstOf("category")[0] != "ship") return;
            //done in LINQ
            var hullNickName = sec.GetFirstOf("hull")[0];

            //TODO: foreach or FirstOrDefault?
            foreach (var shSection in file.GetSections("good").
                                            Where(w => 
                                                  w.GetFirstOf("category")[0] == "shiphull" && w.GetFirstOf("nickname")[0] == hullNickName))
            {
                var shipNickname = shSection.GetFirstOf("ship")[0];
                var shiphash = CreateID(shipNickname);

                uint defaultSound = 0;
                uint defaultPowerPlant = 0;
                uint defaultEngine = 0;

                foreach (var set in sec.GetSettings("addon"))
                {
                    if (set.Count != 3)
                    {
                        LogDispatcher.LogDispatcher.NewMessage(LogType.Warning,
                            "Package for ship {0}: addon {1} has wrong arg count", shipNickname, set[0]);
                        continue;
                    }

                    var equipNick = set[0];
                    var item = Gis.Equipment.FindByHash(CreateID(equipNick));
                    if (item == null)
                    {
                        LogDispatcher.LogDispatcher.NewMessage(LogType.Warning, @"Can't find {0} in DB: from loadout {1} \ {2}", equipNick,
                            hullNickName, shipNickname);
                        continue;
                    }

                    EquipTypes eqType;
                    Enum.TryParse(item.Type, out eqType);

                    switch (eqType)
                    {
                        case EquipTypes.Engine:
                            defaultEngine = item.Hash;
                            break;
                        case EquipTypes.Powerplant:
                            defaultPowerPlant = item.Hash;
                            break;
                        case EquipTypes.InternalFX:
                            defaultSound = item.Hash;
                            break;
                            case EquipTypes.Light:
                            if (set[1] == "internal")
                                LogDispatcher.LogDispatcher.NewMessage(LogType.Warning, "Invalid hardpoint for light {0} (internal): {1}",equipNick,hullNickName);
                            break;
                        case EquipTypes.AttachedFX:
                             if (set[1] == "internal")
                                LogDispatcher.LogDispatcher.NewMessage(LogType.Warning, "Invalid hardpoint for attachedFX {0} (internal): {1}",equipNick,hullNickName);
                            break;
                    }

                }
                Gis.ShipDefaultInternals.AddShipDefaultInternalsRow(Gis.Ships.FindByHash(shiphash), defaultEngine,
                    defaultSound, defaultPowerPlant);
            }

        }

        private void LoadBase(Section sec)
        {
            var nickname = sec.GetFirstOf("nickname")[0];
            //var file = _flDataPath + sec.GetFirstOf("file");
            LogDispatcher.LogDispatcher.NewMessage(LogType.Debug, "Parsing base {0}", nickname);
            var stIDSName = GetIDSParm(sec.GetAnySetting("ids_name", "strid_name")[0]);

            Gis.Bases.AddBasesRow(nickname, stIDSName,"");

            //TODO: do we rly need room data?
            //FLGameData:Ln 191
        }

        private void LoadSystem(Section sec)
        {
            var sysNick = sec.GetFirstOf("nickname")[0].ToLowerInvariant();
            LogDispatcher.LogDispatcher.NewMessage(LogType.Debug, "Parsing system {0}", sysNick);

            var stIDSName = GetIDSParm(sec.GetAnySetting("ids_name", "strid_name")[0]);
            Gis.Systems.AddSystemsRow(sysNick, stIDSName);


            //string file = Directory.GetParent(ini.filePath).FullName + "\\" + section.GetSetting("file").Str(0);
            //TODO: Warning, hardcoded system path! :S
            var sysFile = new DataFile(_flDataPath + @"\Universe\" + sec.GetFirstOf("file")[0]);

            foreach (var obj in sysFile.GetSections("Object"))
            {
                Setting curset;
                //string pos;
                var idsInfo = "";
                var idsInfo1 = "";

                //TODO: do we need pos?
                //if (obj.TryGetFirstOf("pos", out curset))
                //    pos = string.Join(", ", curset);

                //get infocard for the object
                if (obj.TryGetFirstOf("ids_info", out curset))
                {
                    uint id;
                    if (!uint.TryParse(curset[0], out id))
                    {
                        LogDispatcher.LogDispatcher.NewMessage(LogType.Warning, "Can't find ID for object: {0}",curset[0]);
                    }

                    idsInfo = GetIDString(id);
                    if (_infocardMap.ContainsKey(id))
                        idsInfo1 = GetIDString(_infocardMap[id]);
                }

                //add the icard to the base if object is base
                if (obj.TryGetFirstOf("base", out curset))
                    //TODO: really join the stuff, infos are in XML format.
                    Gis.Bases.FindByNickname(curset[0]).Infocard = idsInfo + idsInfo1;
                
            }

            //TODO: zones needed?

        }

        private void LoadEquip(Section sec, EquipTypes equipType)
        {
            var nickname = sec.GetFirstOf("nickname")[0];
            LogDispatcher.LogDispatcher.NewMessage(LogType.Debug, "Parsing equipment {0}", nickname);
            var hash = CreateID(nickname.ToLowerInvariant());

            // fail if equip already presents
            if (Gis.Equipment.FindByHash(hash) != null) return;

            var stIDSName = "";
            if (sec.ContainsAnyOf("ids_name", "strid_name"))
                stIDSName = GetIDSParm(sec.GetAnySetting("ids_name", "strid_name")[0]);

            // TODO: ignore empty IDS based on setting
            if (stIDSName == "") return;

           
            var infocard = "";
            if (sec.ContainsAnyOf("ids_info"))
                infocard = GetIDSParm(sec.GetFirstOf("ids_info")[0]);

            // Tested on Disco 4.87.6, no hits beyond this line.
            //var stIDSInfo1 = "";
            //if (sec.ContainsAnyOf("ids_info1","ids_short_name"))
            //    stIDSInfo1 = GetIDSParm(sec.GetAnySetting("ids_info1", "ids_short_name")[0]);

            // Careful with this one below!
            //if (stIDSInfo1 == "") return;
            //var stIDSInfo2 = "";
            //if (sec.ContainsAnyOf("ids_info2"))
            //    stIDSInfo2 = GetIDSParm(sec.GetFirstOf("ids_info2")[0]);

            //var stIDSInfo3 = "";
            //if (sec.ContainsAnyOf("ids_info3"))
            //    stIDSInfo3 = GetIDSParm(sec.GetFirstOf("ids_info3")[0]);

            //string keys = hash.ToString() + " 0x" + hash.ToString("X");

            var hpType = "";
            switch (equipType)
            {
                case EquipTypes.Gun:

                        hpType = sec.GetFirstOf("hp_gun_type")[0];
                    equipType = _hpMap[hpType];
                    break;
               case EquipTypes.ShieldGen:
                    hpType = sec.GetFirstOf("hp_type")[0];
                    equipType = _hpMap[hpType];
                    break;
                default:
                    break;
            }

            Gis.Equipment.AddEquipmentRow(hash, equipType.ToString(), hpType, nickname, infocard);
        }

        private void LoadEquipSection(Section eqSection)
        {
            switch (eqSection.Name)
            {
                case "Engine":
                    LoadEquip(eqSection, EquipTypes.Engine);
                    break;
                case "Power":
                    LoadEquip(eqSection, EquipTypes.Powerplant);
                    break;
                case "Scanner":
                    LoadEquip(eqSection, EquipTypes.Scanner);
                    break;
                case "Tractor":
                    LoadEquip(eqSection, EquipTypes.Tractor);
                    break;
                case "CloakingDevice":
                    LoadEquip(eqSection, EquipTypes.Cloak);
                    break;
                case "Armor":
                    LoadEquip(eqSection, EquipTypes.Armor);
                    break;
                case "InternalFX":
                    if (eqSection.ContainsAnyOf("use_sound"))
                    {
                        LoadEquip(eqSection, EquipTypes.InternalFX);
                    }
                    break;
                case "AttachedFX":
                    LoadEquip(eqSection, EquipTypes.AttachedFX);
                    break;
                case "Light":
                    LoadEquip(eqSection, EquipTypes.Light);
                    break;
                case "Gun":
                    if (eqSection.ContainsAnyOf("hp_gun_type"))
                    {
                        LoadEquip(eqSection, EquipTypes.Gun);
                    }

                    //if (section.SettingExists("hp_gun_type"))
                    //{
                    //    string hpType = section.GetSetting("hp_gun_type").Str(0);
                    //    AddGameData(DataStore.HashList, section, HardpointClassToGameDataClass(hpType), true);
                    //    DataStore.EquipInfoList.AddEquipInfoListRow(
                    //        FLUtility.CreateID(section.GetSetting("nickname").Str(0)),
                    //        HardpointClassToGameDataClass(hpType), hpType);
                    //}
                    //// Probably an npc gun
                    //else
                    //{
                    //    AddGameData(DataStore.HashList, section, GAMEDATA_GEN, false);
                    //}
                    break;
                case "ShieldGenerator":
                    if (eqSection.ContainsAnyOf("hp_type"))
                    {
                        LoadEquip(eqSection, EquipTypes.ShieldGen);
                    }
                    //if (section.SettingExists("hp_type"))
                    //{
                    //    string hpType = section.GetSetting("hp_type").Str(0);
                    //    AddGameData(DataStore.HashList, section, HardpointClassToGameDataClass(hpType), true);
                    //    DataStore.EquipInfoList.AddEquipInfoListRow(
                    //        FLUtility.CreateID(section.GetSetting("nickname").Str(0)),
                    //        HardpointClassToGameDataClass(hpType), hpType);
                    //}
                    //// Probably an npc shield
                    //else
                    //{
                    //    AddGameData(DataStore.HashList, section, GAMEDATA_GEN, false);
                    //}
                    break;
                case "CounterMeasureDropper":
                    LoadEquip(eqSection, EquipTypes.CountermeasureDropper);
                    break;
                case "Thruster":
                    LoadEquip(eqSection, EquipTypes.Thruster);
                    break;
                case "MineDropper":
                    LoadEquip(eqSection, EquipTypes.MineDropper);
                    break;
                case "Munition":
                    LoadEquip(eqSection, EquipTypes.Munition);
                    break;
                case "RepairKit":
                    LoadEquip(eqSection, EquipTypes.RepairKit);
                    break;
                case "CounterMeasure":
                    LoadEquip(eqSection, EquipTypes.Countermeasure);
                    break;
                case "ShieldBattery":
                    LoadEquip(eqSection, EquipTypes.ShieldBattery);
                    break;
                case "Mine":
                    LoadEquip(eqSection, EquipTypes.Mine);
                    break;
                case "Commodity":
                    LoadEquip(eqSection, EquipTypes.Cargo);
                    break;
                case "CargoPod":
                    break;
                case "LootCrate":
                    break;
                case "TradeLane":
                    break;
                case "Shield":
                    break;
                case "LOD":
                    break;
                case "Motor":
                    break;
                case "Explosion":
                    break;
                //default:
                //break;

            }
        }

        private void LoadFaction(Section sec)
        {
            var nickname = sec.GetFirstOf("nickname")[0];
            LogDispatcher.LogDispatcher.NewMessage(LogType.Debug, "Parsing faction {0}", nickname);
            var hash = CreateFactionID(nickname);


            if (Gis.Factions.FindByHash(hash) != null) return;

            var factionName = "";
            if (sec.ContainsAnyOf("ids_name", "strid_name"))
                factionName = GetIDSParm(sec.GetAnySetting("ids_name", "strid_name")[0]);

            // TODO: ignore empty IDS based on setting
            if (factionName == "") return;

            var shortName = "";
            if (sec.ContainsAnyOf("ids_info1", "ids_short_name"))
                shortName = GetIDSParm(sec.GetAnySetting("ids_info1", "ids_short_name")[0]);

            var infocard = "";
            if (sec.ContainsAnyOf("ids_info"))
                infocard = GetIDSParm(sec.GetFirstOf("ids_info")[0]);

            Gis.Factions.AddFactionsRow(hash, nickname, factionName, shortName, infocard);
        }

        private void LoadShip(Section sec)
        {
            var nickname = sec.GetFirstOf("nickname")[0];
            LogDispatcher.LogDispatcher.NewMessage(LogType.Debug, "Parsing equipment {0}", nickname);
            var hash = CreateID(nickname.ToLowerInvariant());

            // fail if equip already presents
            if (Gis.Ships.FindByHash(hash) != null) return;


            var shipReadableName = "";
            if (sec.ContainsAnyOf("ids_name", "strid_name"))
                shipReadableName = GetIDSParm(sec.GetAnySetting("ids_name", "strid_name")[0]);

            var infocard = "";
            if (sec.ContainsAnyOf("ids_info"))
                infocard = GetIDSParm(sec.GetFirstOf("ids_info")[0]);

            Gis.Ships.AddShipsRow(hash, nickname,shipReadableName, infocard);

            foreach (var set in sec.GetSettings("da_archetype"))
            {
                var utf = new UtfFile(_flDataPath + Path.DirectorySeparatorChar + set[0]);
                foreach (var hp in utf.Hardpoints.Where(hp => hp.ToLowerInvariant().Contains("cloak")))
                {
                    var ghr = Gis.Ships.FindByHash(hash).GetHardpointsRows();
                    if (ghr.Any(hpS => hpS.Name == hp)) continue;
                    Gis.Hardpoints.AddHardpointsRow(Gis.Ships.FindByHash(hash), hp, EquipTypes.Cloak.ToString());
                }
            }

            foreach (var hpSet in sec.GetSettings("hp_type"))
            {
                var type = _hpMap[hpSet[0]];

                
                foreach (var hp in hpSet.Skip(1))
                {
                    var ghr = Gis.Ships.FindByHash(hash).GetHardpointsRows();
                    if (ghr.Any(hpS => hpS.Name == hp)) continue;
                    Gis.Hardpoints.AddHardpointsRow(Gis.Ships.FindByHash(hash), hp, type.ToString());
                }
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

            if (_vDLLs.Count <= iDLL) return null;


            var hInstance = _vDLLs[iDLL];
            if (hInstance == IntPtr.Zero) return null;

            var bufName = new byte[10000];
            var len = LoadString(hInstance, resId, bufName, bufName.Length);
            if (len > 0)
            {
                return Encoding.ASCII.GetString(bufName, 0, len);
            }

            var hFindRes = FindResource(hInstance, resId, 23);
            if (hFindRes == IntPtr.Zero) return null;

            var resContent = LoadResource(hInstance, hFindRes);
            if (resContent == IntPtr.Zero) return null;

            var size = SizeofResource(hInstance, hFindRes);
            var bufInfo = new byte[size];
            Marshal.Copy(resContent, bufInfo, 0, size);
            return Encoding.Unicode.GetString(bufInfo, 0, size);
        }

        public void Dispose()
        {
            Gis.Dispose();
        }


        /// <summary>
        /// Look up table for id creation.
        /// </summary>
        private static uint[] _crcIDTable;

        public static uint CreateID(string nickName)
        {
            const uint flhashPolynomial = 0xA001;
            const int logicalBits = 30;
            const int physicalBits = 32;

            // Build the crc lookup table if it hasn't been created
            if (_crcIDTable == null)
            {
                _crcIDTable = new uint[256];
                for (uint i = 0; i < 256; i++)
                {
                    uint x = i;
                    for (uint bit = 0; bit < 8; bit++)
                        x = ((x & 1) == 1) ? (x >> 1) ^ (flhashPolynomial << (logicalBits - 16)) : x >> 1;
                    _crcIDTable[i] = x;
                }
                //TODO: move that to unit tests
                if (2926433351 != CreateID("st01_to_st03_hole")) throw new Exception("Create ID hash algoritm is broken!");
                if (2460445762 != CreateID("st02_to_st01_hole")) throw new Exception("Create ID hash algoritm is broken!");
                if (2263303234 != CreateID("st03_to_st01_hole")) throw new Exception("Create ID hash algoritm is broken!");
                if (2284213505 != CreateID("li05_to_li01")) throw new Exception("Create ID hash algoritm is broken!");
                if (2293678337 != CreateID("li01_to_li05")) throw new Exception("Create ID hash algoritm is broken!");
            }

            byte[] tNickName = Encoding.ASCII.GetBytes(nickName.ToLowerInvariant());

            // Calculate the hash.
            uint hash = 0;
            for (int i = 0; i < tNickName.Length; i++)
                hash = (hash >> 8) ^ _crcIDTable[(byte)hash ^ tNickName[i]];
            // b0rken because byte swapping is not the same as bit reversing, but 
            // that's just the way it is; two hash bits are shifted out and lost
            hash = (hash >> 24) | ((hash >> 8) & 0x0000FF00) | ((hash << 8) & 0x00FF0000) | (hash << 24);
            hash = (hash >> (physicalBits - logicalBits)) | 0x80000000;

            return hash;
        }

        /// <summary>
        /// Look up table for faction id creation.
        /// </summary>
        private static uint[] _crcFactionIDTable;

        /// <summary>
        /// Function for calculating the Freelancer data nickname hash.
        /// Algorithm from flfachash.c by Haenlomal (October 2006).
        /// </summary>
        /// <param name="nickName"></param>
        /// <returns></returns>
        public static uint CreateFactionID(string nickName)
        {
            const uint FLFACHASH_POLYNOMIAL = 0x1021;
            const uint NUM_BITS = 8;
            const int HASH_TABLE_SIZE = 256;

            if (_crcFactionIDTable == null)
            {
                // The hash table used is the standard CRC-16-CCITT Lookup table 
                // using the standard big-endian polynomial of 0x1021.
                _crcFactionIDTable = new uint[HASH_TABLE_SIZE];
                for (uint i = 0; i < HASH_TABLE_SIZE; i++)
                {
                    uint x = i << (16 - (int)NUM_BITS);
                    for (uint j = 0; j < NUM_BITS; j++)
                    {
                        x = ((x & 0x8000) == 0x8000) ? (x << 1) ^ FLFACHASH_POLYNOMIAL : (x << 1);
                        x &= 0xFFFF;
                    }
                    _crcFactionIDTable[i] = x;
                }
            }

            byte[] tNickName = Encoding.ASCII.GetBytes(nickName.ToLowerInvariant());

            uint hash = 0xFFFF;
            for (uint i = 0; i < tNickName.Length; i++)
            {
                uint y = (hash & 0xFF00) >> 8;
                hash = y ^ (_crcFactionIDTable[(hash & 0x00FF) ^ tNickName[i]]);
            }

            return hash;
        }

    }
}
