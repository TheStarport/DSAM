using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace FLDataFile
{
    /// <summary>
    /// Generic INI file.
    /// </summary>
    public class DataFile
    {
        /// <summary>
        ///     The super duper microsoft encryption key
        /// </summary>
        private static readonly byte[] Gene = { (byte)'G', (byte)'e', (byte)'n', (byte)'e' };

        

        public List<Section> Sections;

        public string Path;

        /// <summary>
        /// Loads INI and tries to deGene if it's coded.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static byte[] LoadBytes(string path)
        {
            byte[] buf;
            using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                buf = new byte[fs.Length];
                fs.Read(buf, 0, (int)fs.Length);
                fs.Close();
            }

            if (buf.Length < 4 || buf[0] != 'F' || buf[1] != 'L' || buf[2] != 'S' || buf[3] != '1') return buf;

            // If this is an encrypted FL ini file then decypt it.
            var dbuf = new byte[buf.Length - 4];

            for (var i = 4; i < buf.Length; i++)
            {
                var k = (Gene[i % 4] + (i - 4)) % 256;
                dbuf[i - 4] = (byte)(buf[i] ^ (k | 0x80));
            }
            return dbuf;
        }

        public DataFile(string path)
        {
            if (path == string.Empty)
                throw new ArgumentException("Bad filename.");

            Path = path;

            var buf = LoadBytes(path);

            //checks for empty file
            if (buf.Length == 0) throw new Exception(string.Format("Empty file: {0}",path));


            // If this is a bini file then decode it
            if (buf.Length >= 12 && buf[0] == 'B' && buf[1] == 'I' && buf[2] == 'N' && buf[3] == 'I')
            {
                ParseBinary(buf);
            }
            else
            {
                Parse(buf);
            }

        }

        private static readonly char[] SectionHeaders = { '[', ']' };

        private void Parse(byte[] buf)
        {
            //Watch it go!

            using (var ms = new MemoryStream(buf))
            using (var sr = new StreamReader(ms))
            {
                string s;
                Section curSection = null;
                while ((s = sr.ReadLine()) != null)
                {
                    s = s.Trim();

                    if (s[0] == ';') continue;

                    if (s[0] == '[')
                    {
                        if (curSection != null)
                        {
                            Sections.Add(curSection);
                        }
                        curSection = new Section(s.Trim(SectionHeaders));
                    }
                    else
                    {
                        curSection.AddSetting(s);
                    }
                }

                Sections.Add(curSection);
            }
        }

        //TODO: clean dis
        private void ParseBinary(byte[] buf)
        {
            var p = 8;
            //var version = BitConverter.ToInt32(buf, p); p += 4;
            //not used, change the first offset to 4 if reenabled

            var strTableOffset = BitConverter.ToInt32(buf, p); p += 4;

            while (p < buf.Length && p < strTableOffset)
            {
                int sectionStrOffset = BitConverter.ToInt16(buf, p); p += 2;
                int sectionNumEntries = BitConverter.ToInt16(buf, p); p += 2;
                string sectionName = BufToString(buf, strTableOffset + sectionStrOffset);

                var section = new Section(sectionName);
                Sections.Add(section);

                while (sectionNumEntries-- > 0)
                {
                    int entryStrOffset = BitConverter.ToInt16(buf, p); p += 2;
                    int entryNumValues = buf[p++];

                    
                    var set = new Setting(BufToString(buf, strTableOffset + entryStrOffset));
                    //string desc = fileName + ":0x" + p.ToString("x") + " '" + settingName + "'";
                    //object[] values = new object[entryNumValues];

                    for (var currentValue = 0; currentValue < entryNumValues; currentValue++)
                    {
                        int valueType = buf[p++];
                        var value = BitConverter.ToInt32(buf, p); p += 4;
                        switch (valueType)
                        {
                            case 1: // Integer
                                set.AddValue(value.ToString(CultureInfo.InvariantCulture));
                                break;
                            case 2: // Float
                                set.AddValue(BitConverter.ToString(buf, p - 4));
                                break;
                            case 3: // String
                                set.AddValue(BufToString(buf, strTableOffset + value));
                                break;
                            default:
                                throw new Exception("Unexpected value type at offset=" + (p - 1));
                        }
                    }
                    section.Settings.Add(set);
                }
            }

        }

        /// <summary>
        /// Return the string ending with a null byte.
        /// </summary>
        /// <param name="buf"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static string BufToString(byte[] buf, int offset)
        {
            var strLen = 0;
            while (buf[strLen] != 0) strLen++;
            return System.Text.Encoding.ASCII.GetString(buf, offset, strLen);
        }


    }
}
