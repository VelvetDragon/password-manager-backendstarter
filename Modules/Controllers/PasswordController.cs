/*
    Program Author:  Suwan Aryal
    USM ID: w10168297
    Assignment: Password Manager, Part 2, Back-End
    
    Description:
        This class handles all CRUD operations for password entries.
*/
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSC317PassManagerP2Starter.Modules.Controllers;
using CSC317PassManagerP2Starter.Modules.Models;
using CSC317PassManagerP2Starter.Modules.Views;


namespace CSC317PassManagerP2Starter.Modules.Controllers
{
    public class PasswordController
    {
        //Stores a list of sample passwords for the test user.
        public List<PasswordModel> _passwords = new List<PasswordModel>();
        private int counter = 1;


        /*
         * The following functions need to be completed.
         */
        //Used to copy the passwords over to the Row Binders.

        // Constructor
        public PasswordController()
        {
            
        }
        public void PopulatePasswordView(ObservableCollection<PasswordRow> source, string search_criteria = "")
        {
            //Complete definition of PopulatePasswordView here.
            source.Clear();

            foreach (var password in _passwords)
            {
                if (string.IsNullOrEmpty(search_criteria) || password.PlatformName.ToLower().Contains(search_criteria.ToLower()))
                {
                    source.Add(new PasswordRow(password));
                }

                
            }
        }

        //CRUD operations for the password list.
        public void AddPassword(string platform, string username, string password)
        {
            //Complete definition of AddPassword here.
            var curr = App.LoginController.GetCurrentUser();
            if (curr != null)
            {
                _passwords.Add(new PasswordModel
                {
                    ID = counter++,
                    UserID = curr.ID,
                    PlatformName = platform,
                    PlatformUserName = username,
                    PasswordText = PasswordCrypto.Encrypt(password, Tuple.Create(curr.Key, curr.IV))
                });
            }
        }

        public PasswordModel? GetPassword(int id)
        {
            //Complete definition of GetPassword here.
            
            foreach (var password in _passwords)
            {
                if (password.ID == id)
                {
                    return password;
                }
            }
            return null;   
        }

        public bool UpdatePassword(PasswordModel changes)
        {
            //Complete definition of Update Password here.

            for (int i = 0; i < _passwords.Count; i++)
            {
                if (_passwords[i].ID == changes.ID)
                {
                    _passwords[i].PlatformName = changes.PlatformName;
                    _passwords[i].PasswordText = changes.PasswordText;
                    return true;
                }
            }
            return false;
        }

        public bool RemovePassword(int ID)
        {
            //Complete definition of Remove Password here.

           for (int i=0; i < _passwords.Count; i++)
            {
                if (_passwords[i].ID == ID)
                {   //removing the password from the list at the index
                    _passwords.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        public void GenTestPasswords()
        {
            //Generate a set of random passwords for the test user.
            //Called in Password List Page.
            var curr = App.LoginController.GetCurrentUser();
            //returning nothing if the length is > 0 to avoid duplicating everytime.

            if (_passwords.Count > 0)
            {
                return;
            }

            if (curr != null)
            {
                _passwords.Add(new PasswordModel
                {
                    ID = counter++,
                    UserID = curr.ID,
                    PlatformName = "USM Soar",
                    PlatformUserName = "su1usm.edu",
                    PasswordText = PasswordCrypto.Encrypt("su123", Tuple.Create(curr.Key, curr.IV))
                });

                _passwords.Add(new PasswordModel
                {
                    ID = counter++,
                    UserID = curr.ID,
                    PlatformName = "Facebook",
                    PlatformUserName = "suwanFB",
                    PasswordText = PasswordCrypto.Encrypt("pass123fb", Tuple.Create(curr.Key, curr.IV))
                });
                _passwords.Add(new PasswordModel
                {
                    ID = counter++,
                    UserID = curr.ID,
                    PlatformName = "Instagram",
                    PlatformUserName = "instaSu1",
                    PasswordText = PasswordCrypto.Encrypt("su1234", Tuple.Create(curr.Key, curr.IV))
                });

                _passwords.Add(new PasswordModel
                {
                    ID = counter++,
                    UserID = curr.ID,
                    PlatformName = "Pinterest",
                    PlatformUserName = "pin1",
                    PasswordText = PasswordCrypto.Encrypt("pin1234", Tuple.Create(curr.Key, curr.IV))
                });

                

            }
        }
    }
}
