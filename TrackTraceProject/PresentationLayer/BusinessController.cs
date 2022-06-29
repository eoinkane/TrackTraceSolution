/* PresentationLayer/BusinessController.cs
 * BusinessController is a Facade Class that provides a go to point for the presentational layer to interact with the business layer
 * 
 * Written By Eoin K 10/12/20
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TrackTraceProject.BusinessLayer;

namespace TrackTraceProject.PresentationLayer
{
    // Define class as public
    // RecorderManager uses the Singleton and Facade design pattern
    public class BusinessController
    {

        /* private static field to hold the single instance of a BusinessController within the track-and-trace system
        *
        *  Added by Eoin K 10/12/20
        */
        private static BusinessController _Instance;

        /* private field to store the recorder within the track-and-trace system
        * _Recorder is used in the CreateContact, CreateVisit, GenerateContacts & GenerateVisits methods
        *
        *  Added by Eoin K 10/12/20
        */
        private RecorderManager _RecorderManager;

        /* private field to store the user collection within the track-and-trace system
        * _Recorder is used in the CreateUser, CreateContact, CreateVisit, GenerateContacts & GenerateVisits methods
        *
        *  Added by Eoin K 10/12/20
        */
        private UserCollectionManager _UserCollectionManager;

        /* private field to store the location collection within the track-and-trace system
        * _Recorder is used in the CreateLocatoin, CreateVisit, & GenerateVisits methods
        *
        *  Added by Eoin K 10/12/20
        */
        private LocationCollectionManager _LocationCollectionManager;

        /* private constructor to ensure no new instances of BusinessController can be created
        *
        *  Added by Eoin K 10/12/20
        */
        private BusinessController()
        {
            _RecorderManager = RecorderManager.Instance;
            _UserCollectionManager = UserCollectionManager.Instance;
            _LocationCollectionManager = LocationCollectionManager.Instance;
        }

        /* public property Instance to hold the single instance of BusinessController
        *  this implements the Singleton pattern
        *
        *  Added by Eoin K 10/12/20
        */
        public static BusinessController Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new BusinessController();
                }
                return _Instance;
            }
        }

        public void ConTest()
        {
            _UserCollectionManager.Save();
        }

        /* public method to save all data in the system
        *  uses the save method on the RecorderManager, UserCollectionManager & LocationCollectionManager
        *
        *  Added by Eoin K 10/12/20
        *  Modified by Eoin K 13/12/20
        */
        public void Save()
        {
            _UserCollectionManager.Save();
            _LocationCollectionManager.Save();
            _RecorderManager.Save();
        }

        /* public method to load all data in the system
        *  uses the load method on the RecorderManager, UserCollectionManager & LocationCollectionManager
        *
        *  Added by Eoin K 10/12/20
        */
        public void Load()
        {
            _UserCollectionManager.Load();
            _LocationCollectionManager.Load();
            _RecorderManager.Load();
        }

        /* public method to create a new user through UserCollectionManager
        *
        * Added by Eoin K 10/12/20
        */
        public void CreateUser(string l_PhoneNumber)
        {
            _UserCollectionManager.Add(l_PhoneNumber);
        }

        /* public method to create a new location through LocationCollectionManager
        *
        * Added by Eoin K 10/12/20
        */
        public void CreateLocation(string l_Name, string l_Address, string l_PostalCode, string l_Country)
        {
            _LocationCollectionManager.Add(l_Name, l_Address, l_PostalCode, l_Country);
        }

        /* public method to create a new contact through RecorderManager
        *
        * Added by Eoin K 10/12/20
        */
        public void RecordContact(int l_UserID1, int l_UserID2, DateTime l_DateAndTime)
        {
            User user1 = _UserCollectionManager.Find(l_UserID1);
            User user2 = _UserCollectionManager.Find(l_UserID2);

            List<User> individuals = new List<User>()
            {
                user1,
                user2
            };

            _RecorderManager.RecordEvent(l_DateAndTime, individuals);
        }

        /* public method to create a new visit through RecorderManager
        *
        * Added by Eoin K 10/12/20
        */
        public void RecordVisit(int l_UserID1, int l_LocationID2, DateTime l_DateAndTime)
        {
            User user = _UserCollectionManager.Find(l_UserID1);
            Location location = _LocationCollectionManager.Find(l_LocationID2);

            _RecorderManager.RecordEvent(l_DateAndTime, user, location);
        }

        /* public method to generate a list of contacts through RecorderManager
        *
        *  Added by Eoin K 13/12/20
        */
        public List<string> GenerateContacts(DateTime l_AfterDate, int l_UserID)
        {
            User user = _UserCollectionManager.Find(l_UserID);

            return _RecorderManager.ListContacts(l_AfterDate, user);
        }

        /* public method to generate a list of visit through RecorderManager
        *
        *  Added by Eoin K 13/12/20
        */
        public List<string> GenerateVisits(DateTime l_StartDate, DateTime l_FinishDate,  int l_LocationID)
        {
            Location location = _LocationCollectionManager.Find(l_LocationID);

            return _RecorderManager.ListVisits(l_StartDate, l_FinishDate, location);
        }

        /* public method to check if a given string is a valid phone number
        *
        * Added by Eoin K 10/12/20
        */
        public bool ValidPhoneNumber(string l_PhoneNumber)
        {
            return Regex.Match(l_PhoneNumber, @"^(\+[4]{2}[0-9]{4}[ ][0-9]{6})$").Success;
        }

        /* public method to check if a given string is a valid postal code
        *
        * Added by Eoin K 10/12/20
        */
        public bool ValidPostalCode(string l_PostalCode)
        {
            return (
                Regex.Match(l_PostalCode, @"^([A-Z]{2}[0-9][A-Z][ ][0-9][A-Z]{2})$").Success ||
                Regex.Match(l_PostalCode, @"^([A-Z][0-9][A-Z][ ][0-9][A-Z]{2})$").Success ||
                Regex.Match(l_PostalCode, @"^([A-Z][0-9][ ][0-9][A-Z]{2})$").Success ||
                Regex.Match(l_PostalCode, @"^([A-Z][0-9]{2}[ ][0-9][A-Z]{2})$").Success ||
                Regex.Match(l_PostalCode, @"^([A-Z]{2}[0-9][ ][0-9][A-Z]{2})$").Success ||
                Regex.Match(l_PostalCode, @"^([A-Z]{2}[0-9]{2}[ ][0-9][A-Z]{2})$").Success
           );
        }

        /* public method to get a user object by ID
        *
        *  Added by Eoin K 10/12/20
        */
        public User FindUser(int l_UserID)
        {
            return _UserCollectionManager.Find(l_UserID);
        }

        /* public method to get a location object by ID
        *
        *  Added by Eoin K 10/12/20
        */
        public Location FindLocation(int l_LocationID)
        {
            return _LocationCollectionManager.Find(l_LocationID);
        }

        /* public method to get the number of user objects stored
        *
        *  Added by Eoin K 11/12/20
        */
        public List<int> ListUserIDs()
        {
            return _UserCollectionManager.ListIDs();
        }

        /* public method to get the number of location objects stored
        *
        *  Added by Eoin K 11/12/20
        */
        public List<int> ListLocationIDs()
        {
            return _LocationCollectionManager.ListIDs();
        }

        /* public method to check if there is enough data in the system to create a contact
        *
        *  Added by Eoin K 11/12/20
        */
        public bool EnoughContactData()
        {
            return (_UserCollectionManager.ListIDs().Count >= 2);
        }

        /* public method to check if there is enough data in the system to create a visit
        *
        *  Added by Eoin K 11/12/20
        */
        public bool EnoughVisitData()
        {
            return ((_UserCollectionManager.ListIDs().Count >= 1) && (_LocationCollectionManager.ListIDs().Count >= 1));
        }

        /* public method to check if there is enough data in the system to generate a list of phone numbers from the contacts 
        *
        *  Added by Eoin K 11/12/20
        */
        public bool EnoughContactListData()
        {
            return (_RecorderManager.ContactCount() >= 1);
        }

        /* public method to check if there is enough data in the system to generate a list of phone numbers from a the visits 
        *
        *  Added by Eoin K 11/12/20
        */
        public bool EnoughVisitListData()
        {
            return (_RecorderManager.VisitCount() >= 1);
        }
    }
}
