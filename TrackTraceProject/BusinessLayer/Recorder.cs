/* BusinessLayer/Recorder.cs
 * Recorder.cs is a class Recorder
 * Recorder represents an all of the locations in the track-and-trace system 
 * Recorder provides a way to create and search though both types of Events
 * 
 * Recorder has 4 properties, int NextEventID, List<Contact> Contacts, List<Visit> Visits
 * 
 * Written By Eoin K 08/12/20
 * Updated By Eoin K 10/12/20
 */
using System;
using System.Collections.Generic;

namespace TrackTraceProject.BusinessLayer
{
    // Define class as public
    // Recorder uses the Factory design pattern
    public class Recorder
    {

        /* private field to store the next event ID going to be used when creating a event object
        *  _NextEventID is used as part of the factory function of Recorder
        *
        *  Added by Eoin K 08/12/20
        */
        private int _NextEventID;

        /* private field to store the collection of contacts within the track-and-trace system
        *
        *  Added by Eoin K 08/12/20
        */
        private List<Contact> _Contacts;

        /* private field to store the collection of visits within the track-and-trace system
        *
        *  Added by Eoin K 08/12/20
        */
        private List<Visit> _Visits;

        /* public constructor to create a new Recorder
        *
        *  Added by Eoin K 08/12/20
        *  Updated by Eoin K 10/12/20
        */
        public Recorder()
        {
            _Contacts = new List<Contact>();
            _Visits = new List<Visit>();
            _NextEventID = 1;
        }

        /* public constructor for Recorder to be created from existing data
        *  this constructor is used when data is being deserialization
        *
        *  Added by Eoin K 08/12/20
        */
        public Recorder(List<Contact> l_ExistingContacts, List<Visit> l_ExistingVisits)
        {
            _Contacts = l_ExistingContacts;
            _Visits = l_ExistingVisits;
            _NextEventID = l_ExistingContacts.Count + l_ExistingVisits.Count + 1;
        }

        /* public method RecordEvent creates a new contact in the track-and-trace system
        *  RecordEvent method signature 1 creates an event of type contact, a contact object only requires DateAndTime & Individuals
        *  this implements the Factory pattern
        *
        *  Added by Eoin K 08/12/20
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
            Contact CreatedContact = new Contact(_NextEventID, l_DateAndTime, l_Individuals);

            _Contacts.Add(CreatedContact);

            _NextEventID += 1;
        }

        /* public method ListContacts creates a new visit in the track-and-trace system
        *  RecordEvent method signature 2 creates an event of type visit, a visit object requires a DateAndTime, Individual and a Place
        *  this implements the Factory pattern
        *
        *  Added by Eoin K 08/12/20
        */
        public void RecordEvent(DateTime l_DateAndTime, User l_Individual, Location l_Place)
        {
            Visit CreatedVisit= new Visit(_NextEventID, l_DateAndTime, l_Individual, l_Place);

            _Visits.Add(CreatedVisit);

            _NextEventID++;
        }

        /* public method ListContacts searches for contacts
        *  ListContacts searches the contact collection where an specified individual is mentioned after a specified date
        *
        *  Added by Eoin K 08/12/20
        */
        public List<string> ListContacts(DateTime l_DateAndTime, User l_Individual)
        {
            // Create a list to store all the users that have come into contact with the specified individual
            List<User> IndividualsInContact = new List<User>();
            // Create a list to store all the phone numbers that have come into contact with the specified individual for the method to return
            List<string> PhoneNumbersInContact = new List<string>();

            // Store the specified individuals ID to use for comparison
            int SpecifiedIndividualID = l_Individual.UserID;

            /* I used .FindAll() in this statement as it is already known that all the results will be needed in a list.
            *  So the perfomance benefit of using .Where() would be lost as I would then have to copy the results of the IEnumerable to a list.
            *
            *  Since I will need a list rather than an IEnumerable later on, I think there is no benefit to use .Where()
            *  
            *  The Predicate is to match any Contact in _Contacts where either of the 2 individuals have the specified individual ID
            */
            List<Contact> MatchingContacts = _Contacts.FindAll(x => 
                (x.Individuals[0].UserID == SpecifiedIndividualID || x.Individuals[1].UserID == SpecifiedIndividualID) &&
                x.DateAndTime > l_DateAndTime
            );

            /* If the result of the .FindAll returns nothing then the method can return on the next line
            *  No more sorting is needed
            */
            if (MatchingContacts.Count == 0) return new List<string>();

            /* In the MatchingContacts list, one of the individuals is the specified user and the other individual is the individual in contact
            *  This for each loop extracts the individuals in contact
            *  
            *  For example if MatchingContacts had 1 contact, the entire list would have 2 individuals
            *  The method needs to discard the individual already specified. so IndividualsInContact would then have 1 user in this example.
            */
            foreach (Contact Contact in MatchingContacts)
            {
                if (Contact.Individuals[0].UserID == SpecifiedIndividualID)
                {
                    IndividualsInContact.Add(Contact.Individuals[1]);
                }
                else
                {
                    IndividualsInContact.Add(Contact.Individuals[0]);
                }
            }

            /* Each individual that has not been discarded has a phone number that the method needs to return
            *  This foreach loop adds every phone number to a string list  
            */
            foreach (User Individual in IndividualsInContact)
            {
                /* dedupe the list
                *  if a phone number already exists in the list it will not be added again
                */
                if (!PhoneNumbersInContact.Contains(Individual.PhoneNumber))
                {
                    PhoneNumbersInContact.Add(Individual.PhoneNumber);
                }
            }
            return PhoneNumbersInContact;
        }

        /* public method ListVisits searches for visits
        *  ListContacts searches the visit collection where an specified location is mentioned between two specified dates
        *
        *  Added by Eoin K 08/12/20
        */
        public List<string> ListVisits(DateTime l_StartDateAndTime, DateTime l_FinishDateAndTime , Location l_Place)
        {
            // Create a list to store all the users that have come into contact with the specified location
            List<User> IndividualsWhoVisited = new List<User>();
            // Create a list to store all the phone numbers that have come into contact with the specified individual for the method to return
            List<string> PhoneNumbersWhoVisited = new List<string>();

            // Store the specified individuals ID to use for comparison
            int SpecifiedLocationID = l_Place.LocationID;

            /* I used .FindAll() in this statement as it is already known that all the results will be needed in a list.
            *  So the perfomance benefit of using .Where() would be lost as I would then have to copy the results of the IEnumerable to a list.
            *
            *  Since I will need a list rather than an IEnumerable later on, I think there is no benefit to use .Where()
            *  
            *  The Predicate is to match any Visit in _Visits where the place has the specified individual ID
            *   , and the date-and-time is in between the start & finish date-and-time
            */
            List<Visit> MatchingVisits = _Visits.FindAll(x =>
                x.DateAndTime > l_StartDateAndTime &&
                x.DateAndTime < l_FinishDateAndTime
            );

            /* If the result of the .FindAll returns nothing then the method can return on the next line
            *  No more sorting is needed
            */
            if (MatchingVisits.Count == 0) return new List<string>();

            /* In the MatchingVisits list, each visit has an individual who has been to the visit's location
            *  This for each loop extracts the individuals who has visited the specified place
            *  
            *  For example if MatchingVisits had 1 visit, the entire list would have 1 individuals
            *  The method needs to extract the individual so IndividualsWhoVisited would then have 1 user in this example.
            */
            foreach (Visit Visit in MatchingVisits)
            {
                IndividualsWhoVisited.Add(Visit.Individual);
            }

            /* Each individual has a phone number that the method needs to return
            *  This foreach loop adds every phone number to a string list 
            */
            foreach (User Individual in IndividualsWhoVisited)
            {
                PhoneNumbersWhoVisited.Add(Individual.PhoneNumber);
            }
            return PhoneNumbersWhoVisited;
        }

        /* public method to get the number of visits recorded in the _Contacts list
        *
        *  Added by Eoin K 11/12/20
        */
        public int ContactCount()
        {
            return _Contacts.Count;
        }

        /* public method to get the number of visits recorded in the _Visits list
        *
        *  Added by Eoin K 11/12/20
        */
        public int VisitCount()
        {
            return _Visits.Count;
        }

        /* public method to return a Contact in the _Contacts list at a given index
        *  used when recorder is being persisted
        *
        *  Added by Eoin K 13/12/20
        */
        public Contact FindContactAtIndex(int l_ContactIndex)
        {
            return _Contacts[l_ContactIndex];
        }

        /* public method to return a Visit in the _Visits list at a given index
        *  used when recorder is being persisted
        *
        *  Added by Eoin K 13/12/20
        */
        public Visit FindVisitAtIndex(int l_VisitIndex)
        {
            return _Visits[l_VisitIndex];
        }
    }
}