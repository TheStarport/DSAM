using System;
using System.Data.SQLite;
using System.Timers;
using LogDispatcher;

namespace FLAccountDB
{
    class DBQueue
    {
        public SQLiteConnection Conn;
        public SQLiteTransaction Transaction;
        private int _count;
        public DBQueue(SQLiteConnection conn, int timeout = 15000)
        {
            Conn = conn;
            Transaction = Conn.BeginTransaction();
            var timer = new Timer(timeout);
            timer.Elapsed += _timer_Elapsed;
            timer.Enabled = true;
            timer.Start();
            GC.KeepAlive(timer);

        }

        /// <summary>
        /// Dump all the unsaved changes before closing.
        /// </summary>
        ~DBQueue()
        {
            _timer_Elapsed(null,null);
        }
        public void Execute(SQLiteCommand cmd)
        {
            cmd.Connection = Conn;
            lock (Transaction)
            cmd.Transaction = Transaction;
            
            cmd.ExecuteNonQuery();
            _count++;
            if (_count > 1000)
            {
                _timer_Elapsed(null,null);
                _count = 0;
            }
        }

        void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (_count > 0)
            {
                
                lock (Transaction)
                {
                    if (Transaction != null)
                    {
                        LogDispatcher.LogDispatcher.NewMessage(LogType.Garbage, "DB Committed, changes: " + _count);
                        Transaction.Commit();
                        Transaction = Conn.BeginTransaction();
                    }

                }

            }

            _count = 0;
        }
    }
}
