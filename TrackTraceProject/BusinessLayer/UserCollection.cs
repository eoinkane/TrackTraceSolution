/* BusinessLayer/UserCollection.cs
 * UserCollection.cs is a class UserCollection
 * UserCollection represents an all of the individuals in the track-and-trace system 
 * UserCollection provides a way to create and find individuals
 * 
 * UserCollection has 3 properties, int NextUserID, List<User> UserList, DataController DataController
 * 
 * Written By Eoin K 08/12/20
 * Updated By Eoin k 10/12/20
 */
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace TrackTraceProject.BusinessLayer
{
    // Define class as public
    // UserCollection uses the Factory design pattern
    public class UserCollection
    {
        /* private field to store the next user ID going to be used when creating a user object
        *  _NextUserID is used as part of the factory function of UserCollection
        *
        *  Added by Eoin K 08/12/20
        */
        private int _NextUserID;

        /* private field to store the collection of individuals within the track-and-trace system
        *
        *  Added by Eoin K 08/12/20
        */
        private List<User> _UserList;

        /* public constructor for UserCollection to be created
        *  this constructor is also parameterless so it is used for serialization
        *
        *  Added by Eoin K 08/12/20
        */
        public UserCollection()
        {
            _NextUserID = 1;
            _UserList = new List<User>();
        }

        /* public constructor for UserCollection to be created from existing data
        *  this constructor is used when data is being deserialization
        *
        *  Added by Eoin K 08/12/20
        */
        public UserCollection(List<User> l_ExistingUsers)
        {
            _NextUserID = l_ExistingUsers.Count + 1;
            _UserList = l_ExistingUsers;
        }

        /* public property to get the count of users in the user collection
        *  this is used when UserCollectionManager runs validation before calling Find
        *  
        *  Added by Eoin K 10/12/20
        */
        public int Count { get => _UserList.Count; set { } }

        /* public method Add to create a new individual in the track-and-trace system
        *  this implements the Factory pattern
        *
        *  Added by Eoin K 08/12/20
        */
        public void Add(string l_PhoneNumber)
        {
            if (!Regex.Match(l_PhoneNumber, @"^(\+[4]{2}[0-9]{4}[ ][0-9]{6})$").Success)
            {
                throw new ArgumentException($"l_PhoneNumber {l_PhoneNumber} is not a valid phone number");
            }

            User CreatedUser = new User(_NextUserID, l_PhoneNumber);

            _UserList.Add(CreatedUser);

            _NextUserID++;
        }

        /* public method Find to find an existing individual in the track-and-trace system by UserID
        *  searches the collection for a user object with the user id matching the parameter passed to the method
        *
        *  Added by Eoin K 08/12/20
        */
        public User Find(int l_UserID)
        {
            if (l_UserID > _UserList.Count)
            {
                throw new ArgumentException($"l_UserID {l_UserID} is out of range of the user collection");
            }

            User FoundUser = _UserList.Find(x => x.UserID == l_UserID);

            return FoundUser;
        }

        /* public method ListIDs to list the current IDs in the user list
        *
        *  Added by Eoin K 11/12/20
        */
        public List<int> ListIDs()
        {
            List<int> IDs = new List<int>();

            foreach (User item in _UserList)
            {
                IDs.Add(item.UserID);
            }

            return IDs;
        }
    }
}
