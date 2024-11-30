using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSC317PassManagerP2Starter.Modules.Models
{
    public class PasswordModel
    {
        //Implement the Password Model here.
        public int ID { get; set; }
        public int UserID { get; set; }
        public string PlatformName { get; set; }
        public byte[] PasswordText { get; set; }

        public PasswordModel(int id, int userId, string platformName, string password, Tuple<byte[], byte[]> keyIV)
        {
            ID = id;
            UserID = userId;
            PlatformName = platformName;
            PasswordText = PasswordCrypto.Encrypt(password, keyIV);
        }

    }
}
