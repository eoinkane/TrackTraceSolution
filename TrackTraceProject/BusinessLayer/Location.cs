/* BusinessLayer/Locatioin.cs
 * Location.cs is a class Location
 * a Location represents a business such as shops, cafes, etc.
 * Location has 5 properties,
 *  int LocationID to identify the Location object,
 *  string Name that has to hold the name of the location,
 *  string Address that has to hold the address of the location,
 *  string PostalCode that has to hold the post code of the location,
 *  string Country that has to hold the country of the location
 *  
 * Contact is Serializable as the properites need to be persisted
 * 
 * Written By Eoin K 07/12/20
 */
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace TrackTraceProject.BusinessLayer
{
    // Define class as public and add Serializable attribute 
    [Serializable]
    public class Location
    {
        /* private field to store the location ID of the location
        *
        *  Added by Eoin K 07/12/20
        */
        private int _LocationID;

        /* private field to store the name of the location
        *
        *  Added by Eoin K 07/12/20
        */
        private string _Name;

        /* private field to store the address of the location
        *
        *  Added by Eoin K 07/12/20
        */
        private string _Address;

        /* private field to store the postcode of the location
        *
        *  Added by Eoin K 07/12/20
        */
        private string _PostalCode;

        /* private field to store the country of the location
        *
        *  Added by Eoin K 07/12/20
        */
        private string _Country;

        /* public constructor with no parameters for a Location object to be serialized
        *
        *  Added by Eoin K 07/12/20
        */
        public Location() { }

        /* public constructor to initialise a Contact
        *  calls the Event constructor as a base constructor
        *
        *  Added by Eoin K 07/12/20
        */
        public Location(int l_LocationID, string l_Name, string l_Address,
            string l_PostalCode, string l_Country)
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

            _LocationID = l_LocationID;
            _Name = l_Name;
            _Address = l_Address;
            _PostalCode = l_PostalCode;
            _Country = l_Country;
        }

        /* public property that can be used to access the private _LocationID field
        *
        *  Added by Eoin K 07/12/20
        */
        public int LocationID { get => _LocationID; set => _LocationID = value; }

        /* public property that can be used to access the private _Name field
        *
        *  Added by Eoin K 07/12/20
        */
        public string Name { get => _Name; set => _Name = value; }

        /* public property that can be used to access the private _Address field
        *
        *  Added by Eoin K 07/12/20
        */
        public string Address { get => _Address; set => _Address = value; }

        /* public property that can be used to access the private _Country field
        *
        *  Added by Eoin K 07/12/20
        */
        public string PostalCode { get => _PostalCode; set
            {
                if (
                    !Regex.Match(value, @"^([A-Z]{2}[0-9][A-Z][ ][0-9][A-Z]{2})$").Success &&
                    !Regex.Match(value, @"^([A-Z][0-9][A-Z][ ][0-9][A-Z]{2})$").Success &&
                    !Regex.Match(value, @"^([A-Z][0-9][ ][0-9][A-Z]{2})$").Success &&
                    !Regex.Match(value, @"^([A-Z][0-9]{2}[ ][0-9][A-Z]{2})$").Success &&
                    !Regex.Match(value, @"^([A-Z]{2}[0-9][ ][0-9][A-Z]{2})$").Success &&
                    !Regex.Match(value, @"^([A-Z]{2}[0-9]{2}[ ][0-9][A-Z]{2})$").Success
                )
                {
                    throw new ArgumentException($"PostalCode {value} is not" +
                        " in the any of the following formats: " +
                        "AA9A 9AA; A9A 9AA; A9 9AA; A99 9AA; AA9 9AA; AA99 9AA.");
                }
                else
                {
                    _PostalCode = value;
                }
            }
        }

        /* public property that can be used to access the private _Country field
        *
        *  Added by Eoin K 07/12/20
        */
        public string Country { get => _Country; set => _Country = value; }
    }
}
