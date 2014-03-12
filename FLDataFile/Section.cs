using System;
using System.Collections.Generic;
using System.Linq;

namespace FLDataFile
{
    public class Section
    {
        public List<Setting> Settings;
        public string Name { get; set; }

        public Section(string name)
        {
            Name = name;
        }

        public Section(string name, string bufBytes)
        {
            Name = name;

            var sets = bufBytes.Split(new []{'\n'},StringSplitOptions.RemoveEmptyEntries);

            foreach (var str in sets.Select(set => set.Split('=')).Where(str => str[0].Trim()[0] != ';'))
            {
                Settings.Add(new Setting(str[1],str[0].Trim()));
            }
        }

    }



}
