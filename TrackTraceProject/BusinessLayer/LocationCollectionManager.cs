/* BusinessLayer/LocationCollectionManager.cs
 * LocationCollectionManager.cs is a wrapper class for LocationCollection
 * LocationCollectionManager provides ensure there is only one instance of a LocationCollection
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
    // LocationCollection uses the Singleton design pattern
    public class LocationCollectionManager
    {
        /* private static field to hold the single instance of a LocationCollectionManager within the track-and-trace system
        *
        *  Added by Eoin K 10/12/20
        */
        private static LocationCollectionManager _Instance;

        /* private field to store the next user ID going to be used when creating a user object
        *  _UserCollection is used as part of add and find methods in the LocationCollectionManager
        *
        *  Added by Eoin K 10/12/20
        */
        private LocationCollection _LocationCollection;

        /* private field to store the data controller
        *
        *  Added by Eoin K 10/12/20
        */
        private DataController _DataController;

        /* private constructor to ensure no new instances of LocationCollectionManager can be created
        *
        *  Added by Eoin K 10/12/20
        */
        private LocationCollectionManager()
        {
            _LocationCollection = new LocationCollection();
            _DataController = DataController.Instance;
        }

        /* public property Instance to hold the single instance of LocationCollectionManager
        *  this implements the Singleton pattern
        *
        *  Added by Eoin K 10/12/20
        */
        public static LocationCollectionManager Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new LocationCollectionManager();
                }
                return _Instance;
            }
        }

        /* public method Add to create a new location in the track-and-trace system
        *
        *  Added by Eoin K 10/12/20
        */
        public void Add(string l_Name, string l_Address, string l_PostalCode, string l_Country)
        {
            if (
                !(
                    Regex.Match(l_PostalCode, @"^([A-Z]{2}[0-9][A-Z][ ][0-9][A-Z]{2})$").Success ||
                    Regex.Match(l_PostalCode, @"^([A-Z][0-9][A-Z][ ][0-9][A-Z]{2})$").Success ||
                    Regex.Match(l_PostalCode, @"^([A-Z][0-9][ ][0-9][A-Z]{2})$").Success ||
                    Regex.Match(l_PostalCode, @"^([A-Z][0-9]{2}[ ][0-9][A-Z]{2})$").Success ||
                    Regex.Match(l_PostalCode, @"^([A-Z]{2}[0-9][ ][0-9][A-Z]{2})$").Success ||
                    Regex.Match(l_PostalCode, @"^([A-Z]{2}[0-9]{2}[ ][0-9][A-Z]{2})$").Success
                )
            )
            {
                throw new ArgumentException($"PostalCode {l_PostalCode} is not" +
                    " in the any of the following formats: " +
                    "AA9A 9AA; A9A 9AA; A9 9AA; A99 9AA; AA9 9AA; AA99 9AA.");
            }

            _LocationCollection.Add(l_Name, l_Address, l_PostalCode, l_Country);
        }

        /* public method Find to find an existing location in the track-and-trace system by LocationID
        *  searches the collection for a location object with the location id matching the parameter passed to the method
        *
        *  Added by Eoin K 08/12/20
        */
        public Location Find(int l_LocationID)
        {
            if (l_LocationID > _LocationCollection.Count)
            {
                throw new ArgumentException($"l_LocationID {l_LocationID} is out of range of the location collection");
            }

            Location FoundLocation = _LocationCollection.Find(l_LocationID);

            return FoundLocation;
        }

        /* public method ListIDs to list the current IDs in the location collection
        *
        *  Added by Eoin K 11/12/20
        */
        public List<int> ListIDs()
        {
            return _LocationCollection.ListIDs();
        }

        /* public method Save to persist the LocationCollection through the DataController
       *
       *  Added by Eoin K 10/12/20
       */
        public void Save()
        {
            _DataController.Save(_LocationCollection);
        }

        /* public method Load to retrive the LocationCollection from storage through the DataController
        *
        *  Added by Eoin K 10/12/20
        */
        public void Load()
        {
            _LocationCollection = _DataController.LoadLocationCollection();
        }
    }
}
