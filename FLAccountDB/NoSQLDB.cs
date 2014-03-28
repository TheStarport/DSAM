using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FLAccountDB.NoSQL;
using LogDispatcher;

namespace FLAccountDB
{
    public class NoSQLDB
    {

        private SQLiteConnection _conn;
        private readonly DBQueue _queue;
        private readonly string _accPath;

        /// <summary>
        /// Initiate the legacy NoSQL Freelancer storage.
        /// </summary>
        /// <param name="dbPath">Path to the SQLite database file. DB will be created if file is nonexistent.</param>
        /// <param name="accPath">Path to accounts' directory.</param>
        public NoSQLDB(string dbPath, string accPath)
        {
            _accPath = accPath;

            if (!File.Exists(dbPath))
            {
                SQLiteConnection.CreateFile(dbPath);

                _conn = new SQLiteConnection();
                var conString = new SQLiteConnectionStringBuilder
                {
                    DataSource = dbPath
                };
                _conn.ConnectionString = conString.ToString();
                using (_conn)
                    {
                    try
                    {
                        _conn.Open();
                    }
                    catch (Exception e)
                    {
                        LogDispatcher.LogDispatcher.NewMessage(LogType.Fatal, "Can't connect to new data base. Reason: " + e.Message);
                        throw;
                    }
 
                    // Create data base structure
                    var createDataBase = _conn.CreateCommand();    // Useful method
                        createDataBase.CommandText = @"CREATE TABLE Accounts(
         CharPath TEXT PRIMARY KEY ON CONFLICT REPLACE,
         CharName TEXT NOT NULL,
         AccID TEXT NOT NULL,
         CharCode TEXT NOT NULL,
         Money INTEGER NOT NULL,
         Ship TEXT,
         Location TEXT NOT NULL,
         Base TEXT,
         Created DATETIME,
         LastOnline DATETIME
);";
                    createDataBase.ExecuteNonQuery();
                    createDataBase.CommandText = "CREATE INDEX CharLookup ON Accounts(CharPath ASC)";
                    createDataBase.ExecuteNonQuery();
                    LogDispatcher.LogDispatcher.NewMessage(LogType.Info,"Created new database");
                    }
            }

            // Base created fo sho
            _conn = new SQLiteConnection();
            var cs = new SQLiteConnectionStringBuilder {DataSource = dbPath};
            _conn.ConnectionString = cs.ToString();
            _conn.Open();
            _queue = new DBQueue(_conn);
        }

        /// <summary>
        /// Initializes the AccDB.
        /// </summary>
        /// <param name="aggressive">Use aggressive scan? Very CPU-hungry but about falf faster.</param>
        public void LoadDB(bool aggressive = false)
        {
            var accDirs = new DirectoryInfo(_accPath).GetDirectories("??-????????").OrderByDescending(d => d.LastAccessTime);
            //Directory.GetDirectories(path, "??-????????").OrderByDescending(d => d.La);
            if (aggressive) Parallel.ForEach(accDirs, account => LoadAccountDirectory(account.FullName));
            else foreach (var acc in accDirs) LoadAccountDirectory(acc.FullName);
        }

        private const string InsertText = "INSERT INTO Accounts " + "(CharPath,CharName,AccID,CharCode,Money,Ship,Location,Base,Created,LastOnline) " + "VALUES(@CharPath,@CharName,@AccID,@CharCode,@Money,@Ship,@Location,@Base,@Created,@LastOnline)";

        private void LoadAccountDirectory(string path)
        {
            //var accountID = AccountRetriever.GetAccountID(path);
            var accountID = path.Substring(path.Length - 11);
            var isBanned = File.Exists(path + Path.DirectorySeparatorChar + "banned");

            var charFiles = Directory.GetFiles(path, "??-????????.fl");

            //remove the account dir if there's no charfiles
            if (charFiles.Length == 0) Directory.Delete(path,true);

            var dbChars = GetCharCodesByAccount(accountID);

            //var comm = new SQLiteCommand(_conn);
            //var trans = _conn.BeginTransaction();
                using (var comm = new SQLiteCommand(InsertText,_queue.Conn))
                    foreach (var md in charFiles.Select(AccountRetriever.GetMeta).Where(md => md != null))
                    {
                        md.IsBanned = isBanned;
                        comm.Parameters.AddWithValue("@CharPath", md.CharPath);
                        comm.Parameters.AddWithValue("@CharName", md.Name);
                        comm.Parameters.AddWithValue("@AccID", accountID);
                        comm.Parameters.AddWithValue("@CharCode", md.CharID);
                        comm.Parameters.AddWithValue("@Money", md.Money);
                        comm.Parameters.AddWithValue("@Ship", md.ShipArch);
                        comm.Parameters.AddWithValue("@Location", md.System);
                        comm.Parameters.AddWithValue("@Base", md.Base);
                        comm.Parameters.AddWithValue("@Created", DateTime.Now);
                        comm.Parameters.AddWithValue("@LastOnline", md.LastOnline);
                        _queue.Execute(comm);

                        dbChars.Remove(md.CharID);
                    }

            if (dbChars.Count == 0) return;

            foreach (var acc in dbChars)
                RemoveAccountFromDB(accountID,acc);
        }

        public void Update(DateTime lastModTime)
        {
            var len = _accPath.Length + 12;
            
            // find all the newer savefiles, get the directory path, get unique directories
            // LINQ magic ;)
            var accDirs =
                new DirectoryInfo(_accPath).GetFiles("??-????????.fl", SearchOption.AllDirectories)
                    .Where(d => d.LastWriteTime > lastModTime)
                    .Select(w => w.FullName.Substring(0,len))
                    .Distinct();

            // add there all the directories whose content had changed (new\del accounts, bans etc)
            accDirs = accDirs.Union(
                new DirectoryInfo(_accPath).GetDirectories("??-????????")
                .Where(w => w.LastWriteTime > lastModTime)
                .Select(w => w.FullName));

            var enumerable = accDirs as IList<string> ?? accDirs.ToList();


            LogDispatcher.LogDispatcher.NewMessage(LogType.Info,
                "Update: found " + enumerable.Count() + " changed accounts.");

            // rescan stuff
            foreach (var accDir in enumerable)
            {
                LoadAccountDirectory(accDir);
            }

        }

        private List<string> GetCharCodesByAccount(string accID)
        {
            var str = new List<string>();
            using (var cmd = new SQLiteCommand(
                "SELECT CharCode FROM Accounts WHERE AccID = @AccID",
                _conn))
            {
                cmd.Parameters.AddWithValue("@AccID", accID);
                using (var rdr = cmd.ExecuteReader())
                    while (rdr.Read())
                        str.Add(rdr.GetString(0));
            }
                
            return str;
        }

        private void RemoveAccountFromDB(string accID, string charID)
        {
            using (var cmd = new SQLiteCommand(
                "DELETE FROM Accounts WHERE AccID = @AccID And CharCode = @CharCode",
                _conn))
            {
                cmd.Parameters.AddWithValue("@AccID", accID);
                cmd.Parameters.AddWithValue("@CharCode", charID);
                _queue.Execute(cmd);
            }
            
        }
    }
}
