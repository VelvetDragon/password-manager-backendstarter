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
        public void PopulatePasswordView(ObservableCollection<PasswordRow> source, string search_criteria = "")
        {
            //Complete definition of PopulatePasswordView here.
            source.Clear();
            foreach (var password in _passwords)
            {
                if (string.IsNullOrEmpty(search_criteria) || password.PlatformName.ToLower() == search_criteria.ToLower())
                {
                    source.Add(new PasswordRow(password));
                }

                
            }
        }

        //CRUD operations for the password list.
        public void AddPassword(string platform, string username, string password)
        {
            //Complete definition of AddPassword here.
            var currentUser = App.LoginController.GetCurrentUser();
            if (currentUser != null)
            {
                _passwords.Add(new PasswordModel(counter++, currentUser.ID, platform, password, Tuple.Create(currentUser.Key, currentUser.IV)));
            }
        }

        public PasswordModel? GetPassword(int ID)
        {
            //Complete definition of GetPassword here.
            
            foreach (var password in _passwords)
            {
                if (password.ID == ID)
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
            if (curr != null)
            {
                _passwords.Add(new PasswordModel(counter++, curr.ID, "USM Soar", "usm123", Tuple.Create(curr.Key, curr.IV)));
                _passwords.Add(new PasswordModel(counter++, curr.ID, "Facebook", "su123", Tuple.Create(curr.Key, curr.IV)));
                _passwords.Add(new PasswordModel(counter++, curr.ID, "Google", "aryal123", Tuple.Create(curr.Key, curr.IV)));
                _passwords.Add(new PasswordModel(counter++, curr.ID, "Pinterest", "mypass123", Tuple.Create(curr.Key, curr.IV)));

            }
        }
    }
}
