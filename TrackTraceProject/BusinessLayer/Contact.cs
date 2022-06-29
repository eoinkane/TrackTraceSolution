/* BusinessLayer/Contact.cs
 * Contact.cs is a class Contact that inherits from Event
 * Contact has 1 property, List<User> Individuals that has to hold 2 Users
 * Contact is Serializable as the Individuals need to be persisted
 * 
 * Written By Eoin K 06/12/20
 */
using System;
using System.Collections.Generic;

namespace TrackTraceProject.BusinessLayer
{
    // Define class as public and add Serializable attribute 
    [Serializable]
    public class Contact: Event
    {
        /* private field to store the individuals of the contact
        *
        *  Added by Eoin K 06/12/20
        */
        private List<User> _Individuals = new List<User>(2);

        /* public constructor with no parameters for a Contact object to be serialized
        *
        *  Added by Eoin K 07/12/20
        */
        public Contact() { }

        /* public constructor to initialise a Contact
        *  calls the Event constructor as a base constructor
        *
        *  Updated by Eoin K 07/12/20
        */
        public Contact(int l_EventID, DateTime l_DateAndTime, List<User> l_Individuals): base(l_EventID, l_DateAndTime)
        {
            if (l_Individuals.Count != 2)
            {
                throw new ArgumentException($"l_Individuals holds more than 2 Users, invalid length: {l_Individuals.Count}");
            }
            else if (l_Individuals.Count < 2)
            {
                throw new ArgumentException($"l_Individuals holds less than 2 Users, invalid length: {l_Individuals.Count}");
            }
            else
            {
                _Individuals.Add(l_Individuals[0]);
                _Individuals.Add(l_Individuals[1]);
            }
        }

        /* public property that can be used to access the private _Individuals field
        *
        *  Added by Eoin K 06/12/20
        */        
        public List<User> Individuals { get => _Individuals; set => _Individuals = value; }
    }
}
