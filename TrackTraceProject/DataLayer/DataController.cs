/* DataLayer/DataController.cs
 * DataController.cs is a class DataController
 * DataController represents any contact the system has with storage
 * DataController provides a way to persist all of the following objects:
 *  UserCollection
 *  LocationCollection
 *  Recorder
 * 
 * Written By Eoin K 10/12/20
 * Modified By Eoin K 13/12/20
 */
using System;
using System.IO;
using TrackTraceProject.BusinessLayer;
using System.Collections.Generic;

namespace TrackTraceProject.DataLayer
{
    public enum LoadMode
    {
        UserCollection,
        LocationCollection,
        Recorder,
    }
    // Define class as public
    // UserCollection uses the Singleton design pattern
    public class DataController
    {
        /* private static field to hold the single instance of a DataController within the track-and-trace system
        *
        *  Added by Eoin K 10/12/20
        */
        private static DataController _Instance;

        /*
        /* private field to store the instance of the UserCollectionManager
        *
        *  Added by Eoin K 13/12/20
        *
        private UserCollectionManager _UserCollectionManager;

        /* private field to store the instance of the LocationCollectionManager
        *
        *  Added by Eoin K 13/12/20
        *
        private LocationCollectionManager _LocationCollectionManager;

        /* private field to store the instance of the RecorderManager
        *
        *  Added by Eoin K 13/12/20
        *
        private RecorderManager _RecorderManager;
        */

        /* private field to store the UserCollection serialization path
        *  _UserCollectionPath is used as part of the loading and saving function of DataController for a UserCollection
        *
        *  Added by Eoin K 10/12/20
        */
        private string _UserCollectionPath;

        /* private field to store the LocationCollection serialization path
        *  _LocationCollectionPath is used as part of the loading and saving function of DataController for a LocationCollection
        *
        *  Added by Eoin K 10/12/20
        */
        private string _LocationCollectionPath;

        /* private field to store the Recorder visit list serialization path
        *  _RecorderVisitsPath is used as part of the loading and saving function of DataController for a Recorder
        *
        *  Added by Eoin K 13/12/20
        */
        private string _RecorderVisitsPath;

        /* private field to store the Recorder contact list serialization path
       *  _RecorderContactsPath is used as part of the loading and saving function of DataController for a Recorder
       *
       *  Added by Eoin K 13/12/20
       */
        private string _RecorderContactsPath;

        /* private constructor to ensure no new instances of DataController can be created
        *  this constructor is also parameterless so it is used for serialization
        *
        *  Added by Eoin K 10/12/20
        *  Modifed by Eoin K 13/12/20
        */
        private DataController()
        {
            // _UserCollectionManager = UserCollectionManager.Instance;
            // _LocationCollectionManager = LocationCollectionManager.Instance;
            // _RecorderManager = RecorderManager.Instance;

            _UserCollectionPath = @"UserCollection.csv";

            _LocationCollectionPath = @"LocationCollection.csv";

            _RecorderVisitsPath = @"RecorderVisits.csv";

            _RecorderContactsPath = @"RecorderContacts.csv";
        }

        /* public property Instance to hold the single instance of DataController
        *  this implements the Singleton pattern
        *
        *  Added by Eoin K 10/12/20
        */
        public static DataController Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new DataController();
                }
                return _Instance;
            }
        }

        /* public method to persist a UserCollection
        * 
        *  Added by Eoin K 10/12/20
        */
        public void Save(UserCollection uc)
        {
            StreamWriter stw = new StreamWriter(_UserCollectionPath, false);
            string headers = "UserID,PhoneNumber";

            stw.WriteLine(headers);
            for (int i = 1; i <= uc.Count; i++)
            {
                User user = uc.Find(i);
                stw.WriteLine($"{user.UserID},{user.PhoneNumber}");
            }
            stw.WriteLine(string.Empty);
            stw.Close();
        }

        /* public method to persist a LocationCollection
        * 
        *  Added by Eoin K 10/12/20
        */
        public void Save(LocationCollection lc)
        {
            StreamWriter stw = new StreamWriter(_LocationCollectionPath, false);
            string headers = "LocationID,Name,Address,PostalCode,Country";

            stw.WriteLine(headers);
            for (int i = 1; i <= lc.Count; i++)
            {
                Location location = lc.Find(i);
                stw.WriteLine($"{location.LocationID};{location.Name};{location.Address};{location.PostalCode};{location.Country}");
            }
            stw.WriteLine(string.Empty);
            stw.Close();
        }

        /* public method to persist a Recorder
        * 
        *  Added by Eoin K 10/12/20
        */
        public void Save(Recorder r)
        {

            StreamWriter stw = new StreamWriter(_RecorderContactsPath, false);
            string headers = "EventID,DateAndTime,UserID1,UserID2";
            stw.WriteLine(headers);
            for (int i = 1; i <= r.ContactCount(); i++)
            {
                Contact contact = r.FindContactAtIndex(i - 1);
                stw.WriteLine($"{contact.EventID},{contact.DateAndTime},{contact.Individuals[0].UserID},{contact.Individuals[1].UserID}");
            }
            stw.WriteLine(string.Empty);
            stw.Close();

            stw = new StreamWriter(_RecorderVisitsPath, false);
            headers = "EventID,DateAndTime,UserID,LocationID";
            stw.WriteLine(headers);
            for (int i = 1; i <= r.VisitCount(); i++)
            {
                Visit visit = r.FindVisitAtIndex(i - 1);
                stw.WriteLine($"{visit.EventID},{visit.DateAndTime},{visit.Individual.UserID},{visit.Place.LocationID}");
            }
            stw.WriteLine(string.Empty);
            stw.Close();
        }

        /* public method to retrieve a UserCollection from persisted storage
        * 
        *  Added by Eoin K 10/12/20
        */
        public UserCollection LoadUserCollection()
        {
            if (File.Exists(_UserCollectionPath))
            {
                string[] lines = File.ReadAllLines(_UserCollectionPath);

                // if the lines in the file only contain the headers then there are no existing users
                if (lines.Length == 1)
                {
                    return new UserCollection();
                }
                else
                {
                    List<User> ExistingUsers = new List<User>();

                    // start loop at 1 as the first line is the header
                    for (int i = 1; i < lines.Length - 1; i++)
                    {
                        string[] values = lines[i].Split(',');
                        ExistingUsers.Add(new User(int.Parse(values[0]), values[1]));
                    }

                    // pass the existing users
                    UserCollection uc = new UserCollection(ExistingUsers);

                    return uc;
                }
            }
            else
            {
                return new UserCollection();
            }
        }

        /* public method to retrieve a LocationCollection from persisted storage
        * 
        *  Added by Eoin K 10/12/20
        */
        public LocationCollection LoadLocationCollection()
        {
            if (File.Exists(_LocationCollectionPath))
            {
                string[] lines = File.ReadAllLines(_LocationCollectionPath);

                // if the lines in the file only contain the headers then there are no existing locations
                if (lines.Length == 1)
                {
                    return new LocationCollection();
                }
                else
                {
                    List<Location> ExistingLocations = new List<Location>();

                    // start loop at 1 as the first line is the header
                    for (int i = 1; i < lines.Length -1; i++)
                    {
                        string[] values = lines[i].Split(';');
                        ExistingLocations.Add(new Location(int.Parse(values[0]), values[1], values[2], values[3], values[4]));
                    }

                    // pass the existing locations
                    LocationCollection lc = new LocationCollection(ExistingLocations);

                    return lc;
                }
            }
            else
            {
                return new LocationCollection();
            }            
        }

        /* public method to retrieve a Recorder from persisted storage
        * 
        *  Added by Eoin K 10/12/20
        */
        public Recorder LoadRecorder()
        {
            /*
            if (File.Exists(_RecorderPath))
            {
                XmlReader xr = XmlReader.Create(_RecorderPath, _XmlReaderSettings);

                Recorder r = (Recorder)_RecorderSerializer.ReadObject(xr);

                xr.Close();

                return r;
            }
            else
            {
                return new Recorder();
            }
            */
            if (File.Exists(_RecorderContactsPath) && File.Exists(_RecorderVisitsPath))
            {
                string[] ContactsLines = File.ReadAllLines(_RecorderContactsPath);
                string[] VisitsLines = File.ReadAllLines(_RecorderVisitsPath);

                // if the lines in the file only contain the headers then there are no existing events
                if (ContactsLines.Length == 1 && VisitsLines.Length == 1)
                {
                    return new Recorder();
                }
                else
                {
                    List<Contact> ExistingContacts = new List<Contact>();
                    List<Visit> ExistingVisits = new List<Visit>();

                    // start loop at 1 as the first line is the header
                    for (int i = 1; i < ContactsLines.Length - 1; i++)
                    {
                        string[] values = ContactsLines[i].Split(',');
                        ExistingContacts.Add(new Contact(int.Parse(values[0]), DateTime.Parse(values[1]), new List<User>()
                            {
                                UserCollectionManager.Instance.Find(int.Parse(values[2])),
                                UserCollectionManager.Instance.Find(int.Parse(values[3]))
                        }));
                    }
                    
                    // start loop at 1 as the first line is the header
                    for (int i = 1; i < VisitsLines.Length - 1; i++)
                    {
                        string[] values = VisitsLines[i].Split(',');
                        ExistingVisits.Add(
                            new Visit(
                                int.Parse(values[0]),
                                DateTime.Parse(values[1]),
                                UserCollectionManager.Instance.Find(int.Parse(values[2])),
                                LocationCollectionManager.Instance.Find(int.Parse(values[3]))
                                )
                        );
                    }

                    // pass the length of lines ( minus 1 to exclude the header line) to the location collection as the number of existing locations
                    Recorder r = new Recorder(ExistingContacts, ExistingVisits);

                    return r;
                }
            }
            else
            {
                return new Recorder();
            }
        }
    }
}
