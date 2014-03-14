using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DSAccountManager_v2.FileTypes
{
    /// <summary>
    /// Class representing a single player account.
    /// </summary>
    class PlayerAccount
    {

        PlayerAccount(string path)
        {
            var file = new FLDataFile.DataFile(path);

        }

    }
}
