/* BusinessLayer/UserCollectionManager.cs
 * UserCollectionManager.cs is a wrapper class for UserCollection
 * UserCollectionManager provides ensure there is only one instance of a UserCollection
 * 
 * 
 * Written By Eoin K 10/12/20
 */
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TrackTraceProject.DataLayer;

namespace TrackTraceProject.BusinessLayer
{
    // Define class as public
    // UserCollection uses the Singleton design pattern
    public class UserCollectionManager
    {
        /* private static field to hold the single instance of a UserCollectionManager within the track-and-trace system
        *
        *  Added by Eoin K 10/12/20
        */
        private static UserCollectionManager _Instance;

        /* private field to store the user collection
        *  _UserCollection is used as part of add and find methods in the UserCollectionManager
        *
        *  Added by Eoin K 10/12/20
        */
        private UserCollection _UserCollection;

        /* private field to store the data controller
        *
        *  Added by Eoin K 10/12/20
        */
        private DataController _DataController;

        /* private constructor to ensure no new instances of UserCollectionManager can be created
        *
        *  Added by Eoin K 10/12/20
        */
        private UserCollectionManager()
        {
            _UserCollection = new UserCollection();
            _DataController = DataController.Instance;
        }

        /* public property Instance to hold the single instance of UserCollectionManager
        *  this implements the Singleton pattern
        *
        *  Added by Eoin K 10/12/20
        */
        public static UserCollectionManager Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new UserCollectionManager();
                }
                return _Instance;
            }
        }

        /* public method Add to create a new individual in the track-and-trace system
        *  this implements the Factory pattern
        *
        *  Added by Eoin K 10/12/20
        */
        public void Add(string l_PhoneNumber)
        {
            if (!Regex.Match(l_PhoneNumber, @"^(\+[4]{2}[0-9]{4}[ ][0-9]{6})$").Success)
            {
                throw new ArgumentException($"l_PhoneNumber {l_PhoneNumber} is not a valid phone number");
            }

            _UserCollection.Add(l_PhoneNumber);
        }

        /* public method Find to find an existing individual in the track-and-trace system by UserID
        *  searches the collection for a user object with the user id matching the parameter passed to the method
        *
        *  Added by Eoin K 10/12/20
        */
        public User Find(int l_UserID)
        {
            if (l_UserID > _UserCollection.Count)
            {
                throw new ArgumentException($"l_UserID {l_UserID} is out of range of the user collection");
            }

            User FoundUser = _UserCollection.Find(l_UserID);

            return FoundUser;
        }

        /* public method ListIDs to list the current IDs in the user list
        *
        *  Added by Eoin K 11/12/20
        */
        public List<int> ListIDs()
        {
            return _UserCollection.ListIDs();
        }

        /* public method Save to persist the UserCollection through the DataController
        *
        *  Added by Eoin K 10/12/20
        */
        public void Save()
        {
            _DataController.Save(_UserCollection);
        }

        /* public method Load to retrive the UserCollection from storage through the DataController
        *
        *  Added by Eoin K 10/12/20
        */
        public void Load()
        {
            _UserCollection = _DataController.LoadUserCollection();
        }
    }
}
