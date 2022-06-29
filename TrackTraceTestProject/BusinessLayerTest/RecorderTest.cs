/* RecorderTest.cs
 * RecorderTest.cs is a unit test for BusinessLayer/Recorder.cs
 * 
 * Written By Eoin K 09/12/20
 * Updated By Eoin K 10/12/20
 */
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrackTraceProject.BusinessLayer;

namespace TrackTraceTestProject.BusinessLayerTest
{
    /* The tests to run for Recorder
    * 
    * Should assert that Recorder can create a new event of type Contact in the track-and-trace system
    * Should assert that Recorder can create a new event of type Visit in the track-and-trace system
    * 
    * Should assert that Recorder can search its contact collection for a contact that mentions a specified individual after a specified date
    * Should assert that Recorder can search its visit collection for a visit at a specified place between two dates
    * 
    * Should assert that Recorder object is serializable
    */
    [TestClass]
    public class RecorderTest
    {
        // Mock Data to be used in the tests.
        private DateTime MockEventDate1 = DateTime.Now.AddDays(-2);
        private DateTime MockEventDate2 = DateTime.Now.AddDays(-1);
        private DateTime MockEventDate3 = DateTime.Now;
        private DateTime MockEventDate4 = DateTime.Now.AddDays(1);
        private DateTime MockEventDate5 = DateTime.Now.AddDays(2);

        private User MockUser1 = new User(1, "+447348 442988");
        private User MockUser2 = new User(2, "+447571 361522");
        private User MockUser3 = new User(3, "+447226 115877");
        private User MockUser4 = new User(4, "+447538 289456");
        private User MockUser5 = new User(5, "+447879 455873");
        private User MockUser6 = new User(5, "+442558 342581");

        private Location MockLocation1 = new Location(1, "Eat Cafe", "1 Road, Town", "BB4 3CD", "United Kingdom");

        /* Test 1
        *  Test that Recorder can be created through the constructor
        *  Added by Eoin K 09/12/20
        *  Updated By Eoin K 10/12/20
        */
        [TestMethod]
        public void RecorderBuildsSuccessFully()
        {
            Recorder r = new Recorder();

            Assert.IsInstanceOfType(r, typeof(Recorder));
        }

        /* Test 2
        *  Test that Recorder can be created by the public constructor with existing data
        *  Added by Eoin K 13/12/20
        */
        [TestMethod]
        public void ExistingRecorderBuildsSuccessFully()
        {
            Recorder r = new Recorder(
                new List<Contact>()
                {
                    new Contact (1, MockEventDate3, new List<User>()
                    {
                        MockUser1,
                        MockUser2
                    })
                },
                new List<Visit>()
                {
                    new Visit (2, MockEventDate5, MockUser1, MockLocation1)
                }
            );

            Assert.IsInstanceOfType(r, typeof(Recorder));
        }

        /* Test 3
        *  Test that Recorder.RecordEvent() method signature 1 will create a new event of type Contact and add the created Contact to the Contact collection
        *  The assert is not as simple as checking the result of the collection so the phone numbers are compared as an assert
        *  
        *  This test will test that the class implements the Factory Design Pattern
        *  Added by Eoin K 09/12/20
        *  Updated By Eoin K 10/12/20
        */
        [TestMethod]
        public void RecorderCreateContact()
        {
            // Arranging the test
            Recorder r = new Recorder();

            // Acting out the test
            r.RecordEvent(MockEventDate3, new List<User>()
            {
                MockUser1,
                MockUser2
            });

            // Asserting the expected result
            // Find the contacts with user 1 after mock date 1. This will return a phone number from the contact the test just created by calling r.RecordEvent
            List<string> ContactPhoneNumbers = r.ListContacts(MockEventDate2, MockUser1);

            Assert.AreEqual(ContactPhoneNumbers[0], MockUser2.PhoneNumber);
        }

        /* Test 4
        *  Test that Recorder.RecordEvent() method signature 1 will run validation on the number of individuals before creating a Contact 
        *  
        *  Added by Eoin K 09/12/20
        *  Updated By Eoin K 10/12/20
        */
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "l_Individuals holds less than 2 Users, invalid length: 1")]
        public void RecorderCreateVisitValidation1()
        {
            // Arranging the test
            Recorder r = new Recorder();

            // Acting out the test
            r.RecordEvent(MockEventDate1, new List<User>()
            {
                MockUser1,
            });
            ArgumentException InvalidArgument = Assert.ThrowsException<ArgumentException>(() => r.RecordEvent(MockEventDate1, new List<User>()
            {
                MockUser1
            }));

            Assert.AreEqual(InvalidArgument.Message, "l_Individuals holds less than 2 Users, invalid length: 1");
        }

        /* Test 5
        *  Test that Recorder.RecordEvent() method signature 1 will run validation on the number of individuals before creating a Contact 
        *  
        *  Added by Eoin K 09/12/20
        *  Updated By Eoin K 10/12/20
        */
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "l_Individuals holds more than 2 Users, invalid length: 3")]
        public void RecorderCreateVisitValidation2()
        {
            // Arranging the test
            Recorder r = new Recorder();

            // Acting out the test
            r.RecordEvent(MockEventDate1, new List<User>()
            {
                MockUser1,
                MockUser2,
                MockUser3
            });
            ArgumentException InvalidArgument = Assert.ThrowsException<ArgumentException>(() => r.RecordEvent(MockEventDate1, new List<User>()
            {
                MockUser1,
                MockUser2,
                MockUser3
            }));

            Assert.AreEqual(InvalidArgument.Message, "l_Individuals holds more than 2 Users, invalid length: 3");
        }

        /* Test 6
        *  Test that Recorder.RecordEvent() method signature 2 will create a new event of type Visit and add the created Visit to the Visit collection
        *  The assert is not as simple as checking the result of the collection so the phone numbers are compared as an assert
        *  
        *  This test will test that the class implements the Factory Design Pattern
        *  Added by Eoin K 09/12/20
        *  Updated By Eoin K 10/12/20
        */
        [TestMethod]
        public void RecorderCreateVisit()
        {
            // Arranging the test
            Recorder r = new Recorder();

            // Acting out the test
            r.RecordEvent(MockEventDate3, MockUser1, MockLocation1);

            // Asserting the expected result
            // Find the visits at location 1 between mock date 2 & 4. This will return a phone number from the visit the test just created by calling r.RecordEvent
            List<string> VisitPhoneNumbers = r.ListVisits(MockEventDate2, MockEventDate4, MockLocation1);

            Assert.AreEqual(VisitPhoneNumbers[0], MockUser1.PhoneNumber);
        }

        /* Test 7
        *  Test that Recorder.ListContacts() will find & return a list of string phone numbers
        *   , belonging to individuals that have been in contact with a  specified individual after specified date & time
        *  
        *  Added by Eoin K 09/12/20
        *  Updated By Eoin K 10/12/20
        */
        [TestMethod]
        public void RecorderListContacts()
        {
            // Arranging the test
            Recorder r = new Recorder();
            r.RecordEvent(MockEventDate1, new List<User>()
            {
                MockUser1,
                MockUser2
            });
            r.RecordEvent(MockEventDate3, new List<User>()
            {
                MockUser3,
                MockUser4
            });
            r.RecordEvent(MockEventDate5, new List<User>()
            {
                MockUser5,
                MockUser6
            });

            // Acting out the test
            // Find the visits at location 1 between mock date 2 & 4. This will return a phone number from the visit the test just created by calling r.RecordEvent
            List<string> VisitPhoneNumbers = r.ListContacts(MockEventDate4, MockUser5);

            // Asserting the expected result

            Assert.AreEqual(VisitPhoneNumbers[0], MockUser6.PhoneNumber);
        }

        /* Test 8
        *  Test that Recorder.ListVisits() will find & return a list of string phone numbers
        *   , belonging to individuals that have visited a specified place during a specified time period
        *  
        *  Added by Eoin K 09/12/20
        *  Updated By Eoin K 10/12/20
        */
        [TestMethod]
        public void RecorderListVisits()
        {
            // Arranging the test
            Recorder r = new Recorder();
            r.RecordEvent(MockEventDate1, MockUser1, MockLocation1);
            r.RecordEvent(MockEventDate3, MockUser2, MockLocation1);
            r.RecordEvent(MockEventDate5, MockUser3, MockLocation1);

            // Acting out the test
            // Find the visits at location 1 between mock date 2 & 4. This will return a phone number from the visit the test just created by calling r.RecordEvent
            List<string> VisitPhoneNumbers = r.ListVisits(MockEventDate2, MockEventDate4, MockLocation1);

            // Asserting the expected result

            Assert.AreEqual(VisitPhoneNumbers[0], MockUser2.PhoneNumber);
        }

        /* Test 9
        *  Test that Recorder.ContactCount() will return the number of Contacts stored in the system
        *  
        *  Added by Eoin K 11/12/20
        */
        [TestMethod]
        public void RecorderContactCount()
        {
            // Arranging the test
            Recorder r = new Recorder();
            r.RecordEvent(MockEventDate1, new List<User>()
            {
                MockUser1,
                MockUser2
            });
            r.RecordEvent(MockEventDate3, new List<User>()
            {
                MockUser3,
                MockUser4
            });
            r.RecordEvent(MockEventDate5, new List<User>()
            {
                MockUser5,
                MockUser6
            });

            // Acting out the test
            int ActualContactCount = r.ContactCount();

            // Asserting the expected result
            int ExpectedContactCount = 3;
            Assert.AreEqual(ActualContactCount, ExpectedContactCount);
        }

        /* Test 10
        *  Test that Recorder.VisitCount() will return the number of Visits stored in the system
        *  
        *  Added by Eoin K 11/12/20
        */
        [TestMethod]
        public void RecorderVisitCount()
        {
            // Arranging the test
            Recorder r = new Recorder();
            r.RecordEvent(MockEventDate1, MockUser1, MockLocation1);
            r.RecordEvent(MockEventDate3, MockUser2, MockLocation1);
            r.RecordEvent(MockEventDate5, MockUser3, MockLocation1);

            // Acting out the test
            int ActualVisitCount = r.VisitCount();

            // Asserting the expected result
            int ExpectedVisitCount = 3;
            Assert.AreEqual(ActualVisitCount, ExpectedVisitCount);
        }

        /* Test 11
        *  Test that Recorder.FindContactAtIndex() will return the Contact at a given index
        *  
        *  Added by Eoin K 13/12/20
        */
        [TestMethod]
        public void RecorderFindContactAtIndex()
        {
            // Arranging the test
            Recorder r = new Recorder();
            r.RecordEvent(MockEventDate1, new List<User>()
            {
                MockUser1,
                MockUser2
            });
            r.RecordEvent(MockEventDate3, new List<User>()
            {
                MockUser3,
                MockUser4
            });
            r.RecordEvent(MockEventDate5, new List<User>()
            {
                MockUser5,
                MockUser6
            });

            // Acting out the test
            Contact ActualContactResult = r.FindContactAtIndex(1);

            // Asserting the expected result
            int ExpectedVisitResultID = 2;
            DateTime ExpectedVisitResultDate = MockEventDate3;
            User ExpectedVisitResultUser1 = MockUser3;
            User ExpectedVisitResultUser2 = MockUser4;

            Assert.AreEqual(ActualContactResult.EventID, ExpectedVisitResultID);
            Assert.AreEqual(ActualContactResult.DateAndTime, ExpectedVisitResultDate);
            Assert.AreEqual(ActualContactResult.Individuals[0], ExpectedVisitResultUser1);
            Assert.AreEqual(ActualContactResult.Individuals[1], ExpectedVisitResultUser2);
        }

        /* Test 12
        *  Test that Recorder.FindVisitAtIndex() will return the Visit at a given index
        *  
        *  Added by Eoin K 13/12/20
        */
        [TestMethod]
        public void RecorderFindVisitAtIndex()
        {
            // Arranging the test
            Recorder r = new Recorder();
            r.RecordEvent(MockEventDate1, MockUser1, MockLocation1);
            r.RecordEvent(MockEventDate3, MockUser2, MockLocation1);
            r.RecordEvent(MockEventDate5, MockUser3, MockLocation1);

            // Acting out the test
            Visit ActualVisitResult = r.FindVisitAtIndex(2);

            // Asserting the expected result
            int ExpectedVisitResultID = 3;
            DateTime ExpectedVisitResultDate = MockEventDate5;
            User ExpectedVisitResultUser = MockUser3;
            Location ExpectedVisitResultLocation = MockLocation1;

            Assert.AreEqual(ActualVisitResult.EventID, ExpectedVisitResultID);
            Assert.AreEqual(ActualVisitResult.DateAndTime, ExpectedVisitResultDate);
            Assert.AreEqual(ActualVisitResult.Individual, ExpectedVisitResultUser);
            Assert.AreEqual(ActualVisitResult.Place, ExpectedVisitResultLocation);
        }
    }
}
