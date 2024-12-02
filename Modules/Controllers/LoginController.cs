/*
    Program Author:  Suwan Aryal
    USM ID: w10168297
    Assignment: Password Manager, Part 2, Back-End
    
    Description:
        This class manages user authentication by validating inputted text in the login form.
*/

using CSC317PassManagerP2Starter.Modules.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSC317PassManagerP2Starter.Modules.Controllers
{
    public enum AuthenticationError { NONE, INVALIDUSERNAME, INVALIDPASSWORD }
    public class LoginController
    {

        /*
         * This class is incomplete.  Fill in the method definitions below.
         */
        private User _user = new User();
        private bool _loggedIn = false;

        //creating a dummy user to test
        public LoginController()
        {
            var keyReturned = PasswordCrypto.GenKey(); // from the problem doc genkey returns tuple containing the key and IV's
            _user = new User
            {
                ID = 1,
                UserName = "test",
                PasswordHash = PasswordCrypto.GetHash("ab1234"), 
                FirstName = "John",
                LastName = "Doe",
                Key = keyReturned.Item1, 
                IV = keyReturned.Item2  
            };
        }
        public User? GetCurrentUser()
        {
            
                if (_loggedIn) // returning values after checking if the user is logged in
                {
                    return new User { 
                        ID = _user.ID,
                        FirstName = _user.FirstName,
                        LastName = _user.LastName,
                        UserName = _user.UserName,
                        Key = _user.Key,
                        IV = _user.IV
                    };
                }
                else
                {
                    return null;
                }
            
        }

        public AuthenticationError Authenticate(string username, string password)
        {
            //determines whether the inputted username/password matches the stored
            //username/password.  currently returns a NONE error status.

            if (_user.UserName != username)
            {
                return AuthenticationError.INVALIDUSERNAME;
            } 
            else if (!PasswordCrypto.CompareBytes(_user.PasswordHash, PasswordCrypto.GetHash(password)))
            {
                return AuthenticationError.INVALIDPASSWORD;
            } 
            else
            {
                _loggedIn = true;
                return AuthenticationError.NONE; //here the login is verified
            }  
            
        }

        
    }

}
