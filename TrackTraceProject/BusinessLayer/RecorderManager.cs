/* BusinessLayer/RecorderManager.cs
 * RecorderManager.cs is a wrapper class for Recorder
 * RecorderManager provides ensure there is only one instance of a Recorder
 * 
 * 
 * Written By Eoin K 10/12/20
 */
using System;
using System.Collections.Generic;
using TrackTraceProject.DataLayer;

namespace TrackTraceProject.BusinessLayer
{
    // Define class as public and add Serializable attribute 
    // RecorderManager uses the Singleton design pattern
    public class RecorderManager
    {
        /* private static field to hold the single instance of a RecorderManager within the track-and-trace system
        *
        *  Added by Eoin K 10/12/20
        */
        private static RecorderManager _Instance;

        /* private field to store the recorder within the track-and-trace system
        * _Recorder is used in the RecordEvent, ListContacts & ListVisits methods
        *
        *  Added by Eoin K 10/12/20
        */
        private Recorder _Recorder;

        /* private field to store the data controller
        *
        *  Added by Eoin K 10/12/20
        */
        private DataController _DataController;

        /* private constructor to ensure no new instances of RecorderManager can be created
        *  this constructor is also parameterless so it is used for serialization
        *
        *  Added by Eoin K 10/12/20
        */
        private RecorderManager()
        {
            _Recorder = new Recorder();
            _DataController = DataController.Instance;
        }

        /* public property Instance to hold the single instance of RecorderManager
        *  this implements the Singleton pattern
        *
        *  Added by Eoin K 10/12/20
        */
        public static RecorderManager Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new RecorderManager();
                }
                return _Instance;
            }
        }

        /* public method RecordEvent creates a new contact in the track-and-trace system
        *  RecordEvent method signature 1 creates an event of type contact, a contact object only requires DateAndTime & Individuals
        *
        *  Added by Eoin K 10/12/20
        */
        public void RecordEvent(DateTime l_DateAndTime, List<User> l_Individuals)
        {
            if (l_Individuals.Count != 2)
            {
                throw new ArgumentException($"l_Individuals holds more than 2 Users, invalid length: {l_Individuals.Count}");
            }
            else if (l_Individuals.Count < 2)
            {
                throw new ArgumentException($"l_Individuals holds less than 2 Users, invalid length: {l_Individuals.Count}");
            }

            _Recorder.RecordEvent(l_DateAndTime, l_Individuals);
        }

        /* public method RecordEvent creates a new visit in the track-and-trace system
        *  RecordEvent method signature 2 creates an event of type visit, a visit object requires a DateAndTime, Individual and a Place
        *
        *  Added by Eoin K 10/12/20
        */
        public void RecordEvent(DateTime l_DateAndTime, User l_Individual, Location l_Place)
        {
            _Recorder.RecordEvent(l_DateAndTime, l_Individual, l_Place);
        }

        /* public method ListContacts searches for contacts
        *  ListContacts searches the contact collection where an specified individual is mentioned after a specified date
        *
        *  Added by Eoin K 10/12/20
        */
        public List<string> ListContacts(DateTime l_DateAndTime, User l_Individual)
        {
            return _Recorder.ListContacts(l_DateAndTime, l_Individual);
        }

        /* public method ListVisits searches for visits
        *  ListContacts searches the visit collection where an specified location is mentioned between two specified dates
        *
        *  Added by Eoin K 10/12/20
        */
        public List<string> ListVisits(DateTime l_StartDateAndTime, DateTime l_FinishDateAndTime , Location l_Place)
        {
            return _Recorder.ListVisits(l_StartDateAndTime, l_FinishDateAndTime, l_Place);
        }

        /* public method ContactCount returns the number of contacts stored in the visit list
        *
        *  Added by Eoin K 11/12/20
        */
        public int ContactCount()
        {
            return _Recorder.ContactCount();
        }

        /* public method VisitCount returns the number of contacts stored in the visit list
        *
        *  Added by Eoin K 11/12/20
        */
        public int VisitCount()
        {
            return _Recorder.VisitCount();
        }

        /* public method Save to persist the Recorder through the DataController
        *
        *  Added by Eoin K 10/12/20
        */
        public void Save()
        {
            _DataController.Save(_Recorder);
        }

        /* public method Load to retrive the Recorder from storage through the DataController
        *
        *  Added by Eoin K 10/12/20
        */
        public void Load()
        {
            _Recorder = _DataController.LoadRecorder();
        }
    }
}