/*
    Program Author:  Suwan Aryal
    USM ID: w10168297
    Assignment: Password Manager, Part 2, Back-End
    
    Description:
        This class defines the PasswordModel class with password entry and details like platformnames, usernames, IDs.
*/
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
        
        public string PlatformUserName { get; set; } // to fill the username tab in the passwordlist view
        public byte [] PasswordText { get; set; }    

    }
}
