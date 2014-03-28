using System;
using System.Collections.Generic;

namespace FLAccountDB.NoSQL
{
    public class Metadata
    {
        public string AccountID;
        public string CharID;
        public string Name;
        public bool IsAdmin;
        public bool IsBanned;
        public byte Rank;
        public uint Money;

        public uint ShipArch;
        public string System;
        public string Base;

        public DateTime LastOnline;

        public string CharPath;

        public Metadata()
        {

        }

        public static Metadata ParseMeta(string path)
        {
            return AccountRetriever.GetMeta(path);
        }
    }
}
