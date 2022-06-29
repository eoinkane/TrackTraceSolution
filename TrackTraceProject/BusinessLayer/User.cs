/* BusinessLayer/User.cs
 * User.cs is a class User
 * User represents an individual in the track-and-trace system 
 * User has 2 properties, int UserID & string PhoneNumber
 * 
 * Written By Eoin K 06/12/20
 */
using System;
using System.Text.RegularExpressions;

namespace TrackTraceProject.BusinessLayer
{
    // Define class as public 
    public class User
    {
        /* private field to store the id of the user
        *
        *  Added by Eoin K 06/12/20
        */
        private int _UserID;

        /* private field to store the phone number of the user
        *
        *  Added by Eoin K 06/12/20
        */
        private string _PhoneNumber;

        /* public constructor with no parameters for a User object to be serialized
        *
        *  Added by Eoin K 07/12/20
        */
        public User() { }

        /* public constructor for a User object
        *
        *  Added by Eoin K 06/12/20
        */
        public User(int l_UserID, string l_PhoneNumber)
        {
            if (!Regex.Match(l_PhoneNumber, @"^(\+[4]{2}[0-9]{4}[ ][0-9]{6})$").Success)
            {
                throw new ArgumentException($"l_PhoneNumber {l_PhoneNumber} is not a valid phone number");
            }

            _UserID = l_UserID;
            _PhoneNumber = l_PhoneNumber;
        }

        /* public property that can be used to access the private _UserID field
        *
        *  Added by Eoin K 06/12/20
        */
        public int UserID { get => _UserID; set => _UserID = value; }

        /* public property that can be used to access the private _PhoneNumber field
        *
        *  Added by Eoin K 06/12/20
        */
        public string PhoneNumber { get => _PhoneNumber; set
            {
                if (!Regex.Match(value, @"^(\+[4]{2}[0-9]{4}[ ][0-9]{6})$").Success)
                {
                    throw new ArgumentException($"l_PhoneNumber ${value} is not a valid phone number");
                }
                else
                {
                    _PhoneNumber = value;
                }
            }
        }
    }
}
