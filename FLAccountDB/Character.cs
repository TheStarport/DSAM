using System;
using System.Collections.Generic;

namespace FLAccountDB
{
    public class Character
    {
        public string AccountID;
        public string CharID;
        public string Name;
        public bool IsAdmin;
        public bool IsBanned;
        public byte Rank;
        public uint Money;
        public Dictionary<string, float> Reputation = new Dictionary<string, float>();

        /// <summary>
        /// Player's primary IFF\Faction
        /// </summary>
        public string ReputationIFF;
        public Dictionary<uint, byte> Visits = new Dictionary<uint, byte>();
        public List<uint> VisitedBases = new List<uint>();
        public List<uint> VisitedSystems = new List<uint>();

        public uint ShipArch;
        public float Health;
        public Dictionary<uint, uint> Cargo = new Dictionary<uint, uint>();
        
        /// <summary>
        /// Stores player's equipment. Tuple: ID, Hardpoint name, Health
        /// </summary>
        public List<Tuple<uint,string,float>> Equipment = new List<Tuple<uint, string, float>>();
        public string System;
        public string Base;
        public string LastBase;
        public float[] Position;
        public float[] Rotation;




        public Character()
        {
            
        }

        public static Character ParsePlayer(string path)
        {
            return NoSQL.AccountRetriever.GetAccount(path);
        }
    }
}
