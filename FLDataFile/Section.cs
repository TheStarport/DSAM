using System;
using System.Collections.Generic;
using System.Linq;

namespace FLDataFile
{
    public class Section
    {
        public List<Setting> Settings;
        private readonly Dictionary<string, Setting> _setDictionary = new Dictionary<string, Setting>(); 
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


        /// <summary>
        /// Returns first setting with this name. Speeds up consequentive calls if used.
        /// </summary>
        /// <param name="name">Name of the setting.</param>
        /// <returns>Setting class.</returns>
        public Setting GetFirstOf(string name)
        {
            if (_setDictionary[name] == null) 
                _setDictionary[name] = Settings.First(a => a.Name == name);

            return _setDictionary[name];
        }

    }



}
