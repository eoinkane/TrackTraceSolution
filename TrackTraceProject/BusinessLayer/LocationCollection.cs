/* BusinessLayer/LocationCollection.cs
 * LocationCollection.cs is a class LocationCollection
 * LocationCollection represents an all of the locations in the track-and-trace system 
 * LocationCollection provides a way to create and find locations
 * 
 * LocationCollection has 3 properties, int NextLocationID, List<Location> LocationList
 * 
 * Written By Eoin K 08/12/20
 * Updated by Eoin K 10/12/20
 */
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;

namespace TrackTraceProject.BusinessLayer
{
    // Define class as public
    // LocationCollection uses the Factory design pattern
    public class LocationCollection
    {
        /* private field to store the next location ID going to be used when creating a location object
        *  _NextLocationID is used as part of the factory function of LocationCollection
        *
        *  Added by Eoin K 08/12/20
        */
        private int _NextLocationID;

        /* private field to store the collection of individuals within the track-and-trace system
        *
        *  Added by Eoin K 08/12/20
        */
        private List<Location> _LocationList;

        /* private constructor to ensure no new instances of LocationCollection can be created
        *  this constructor is also parameterless so it is used for serialization
        *
        *  Added by Eoin K 08/12/20
        *  Updated by Eoin K 10/12/20
        */
        public LocationCollection()
        {
            _NextLocationID = 1;
            _LocationList = new List<Location>();
        }

        /* public constructor for UserCollection to be created from existing data
        *  this constructor is used when data is being deserialization
        *
        *  Added by Eoin K 08/12/20
        */
        public LocationCollection(List<Location> l_ExistingLocations)
        {
            _NextLocationID = l_ExistingLocations.Count + 1;
            _LocationList = l_ExistingLocations;
        }

        /* public property to get the count of users in the location collection
        *  this is used when LocationCollectionManager runs validation before calling Find
        *  
        *  Added by Eoin K 10/12/20
        */
        public int Count { get => _LocationList.Count; set { } }

        /* public method Add to create a new location in the track-and-trace system
        *  this implements the Factory pattern
        *
        *  Added by Eoin K 08/12/20
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

            Location CreatedLocation = new Location(_NextLocationID, l_Name, l_Address, l_PostalCode, l_Country);

            _LocationList.Add(CreatedLocation);

            _NextLocationID++;
        }

        /* public method Find to find an existing location in the track-and-trace system by LocationID
        *  searches the collection for a location object with the location id matching the parameter passed to the method
        *
        *  Added by Eoin K 08/12/20
        */
        public Location Find(int l_LocationID)
        {
            if (l_LocationID > _LocationList.Count)
            {
                throw new ArgumentException($"l_LocationID {l_LocationID} is out of range of the location collection");
            }

            Location FoundLocation = _LocationList.Find(x => x.LocationID == l_LocationID);

            return FoundLocation;
        }

        /* public method ListIDs to list the current IDs in the location list
        *
        *  Added by Eoin K 11/12/20
        */
        public List<int> ListIDs()
        {
            List<int> IDs = new List<int>();

            foreach (Location item in _LocationList)
            {
                IDs.Add(item.LocationID);
            }

            return IDs;
        }
    }
}
