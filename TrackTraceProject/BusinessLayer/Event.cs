/* BusinessLayer/Event.cs
 * Event.cs is a abstract class Event
 * Event is the base class for Visit & Contact
 * Event has 2 properties, int EventID & DateTime DateAndTime
 * 
 * Written By Eoin K 06/12/20
 */
using System;

namespace TrackTraceProject.BusinessLayer
{
    // Define class as public 
    public abstract class Event
    {
        /* private field to store the id of the event
        *
        *  Added by Eoin K 06/12/20
        */
        private int _EventID;

        /* private field to store the date & time of the event
        *
        *  Added by Eoin K 06/12/20
        */
        private DateTime _DateAndTime;

        /* public constructor with no parameters for a Event object to be serialized
        *
        *  Added by Eoin K 07/12/20
        */
        public Event() { }

        /* public constructor to be used as the base constructor
        *
        *  Added by Eoin K 06/12/20
        */
        public Event(int l_EventID, DateTime l_DateAndTime)
        {
            _EventID = l_EventID;
            _DateAndTime = l_DateAndTime;
        }

        /* public property that can be used to access the private _EventID field
        *
        *  Added by Eoin K 06/12/20
        */
        public int EventID { get => _EventID; set => _EventID = value; }

        /* public property that can be used to access the private _DateAndTime field
        *
        *  Added by Eoin K 06/12/20
        */
        public DateTime DateAndTime { get => _DateAndTime; set => _DateAndTime = value; }
    }
}
