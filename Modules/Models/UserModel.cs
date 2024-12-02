/*
    Program Author:  Suwan Aryal
    USM ID: w10168297
    Assignment: Password Manager, Part 2, Back-End
    
    Description:
        This class defines the UserModel class with user's data
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSC317PassManagerP2Starter.Modules.Models
{
    public class User
    {

        //Implement the User Model here.
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] Key { get; set; }
        public byte[] IV { get; set; }

      
    }
}
