/* BusinessLayer/Visit.cs
 * Visit.cs is a class Visit that inherits from Event
 * Visit has 2 properties:
 *  User Individual that holds the person who made the visit
 *  Location Place that holds where the visit occured
 * 
 * 
 * Written By Eoin K 08/12/20
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackTraceProject.BusinessLayer
{
    // Define class as public
    // Define class as a child class of Event
    public class Visit: Event
    {
        /* private field to store the individual of the visit
        *
        *  Added by Eoin K 08/12/20
        */
        private User _Individual;

        /* private field to store the location of the visit
        *
        *  Added by Eoin K 08/12/20
        */
        private Location _Place;

        /* public constructor with no parameters for a Visit object to be serialized
        *
        *  Added by Eoin K 08/12/20
        */
        public Visit() { }

        /* public constructor to initialise a Visit
        *  calls the Event constructor as a base constructor
        *
        *  Updated by Eoin K 08/12/20
        */
        public Visit(int l_EventID, DateTime l_DateAndTime, User l_Individual, Location l_Place) : base(l_EventID, l_DateAndTime)
        {
            _Individual = l_Individual;
            _Place = l_Place;
        }

        /* public property that can be used to access the private _Individual field
        *
        *  Added by Eoin K 08/12/20
        */
        public User Individual { get => _Individual; set => _Individual = value; }

        /* public property that can be used to access the private _Place field
        *
        *  Added by Eoin K 08/12/20
        */
        public Location Place { get => _Place; set => _Place= value; }
    }
}
