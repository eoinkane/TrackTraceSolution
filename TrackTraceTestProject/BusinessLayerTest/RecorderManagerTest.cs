/* RecorderManagerTest.cs
 * RecorderManagerTest.cs is a unit test for BusinessLayer/RecorderManager.cs
 * 
 * Written By Eoin K 10/12/20
 */
using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrackTraceProject.BusinessLayer;

namespace TrackTraceTestProject.BusinessLayerTest
{
    /* The tests to run for Recorder
    * Should assert that RecorderManager is a singleton class
    * 
    * Should assert that RecorderManager can create a new event of type Contact in the track-and-trace system
    * Should assert that RecorderManager can create a new event of type Visit in the track-and-trace system
    * 
    * Should assert that RecorderManager can search its contact collection for a contact that mentions a specified individual after a specified date
    * Should assert that RecorderManager can search its visit collection for a visit at a specified place between two dates
    * 
    */
    [TestClass]
    public class RecorderManagerTest
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
        *  Test that RecorderManager can be accessed by the instance property
        *  This test will test that the class implements the Singleton Design Pattern
        *  Added by Eoin K 10/12/20
        */
        [TestMethod]
        public void RecorderManagerBuildsSuccessFully()
        {
            RecorderManager r = RecorderManager.Instance;

            Assert.IsInstanceOfType(r, typeof(RecorderManager));
        }

        /* Test 2
        *  Test that multiple assocations to RecorderManager.Instance will access the same instance
        *  This test will test that the class implements the Singleton Design Pattern
        *  Added by Eoin K 10/12/20
        */
        [TestMethod]
        public void RecorderManagerHasOnlyOneInstance()
        {
            RecorderManager rm1 = RecorderManager.Instance;
            RecorderManager rm2 = RecorderManager.Instance;

            Assert.AreSame(rm1, rm2);
        }

        /* Test 3
        *  Test that RecorderManager.RecordEvent() method signature 1 will create a new event of type Contact and add the created Contact to the Contact collection
        *  The assert is not as simple as checking the result of the collection so the phone numbers are compared as an assert
        *  
        *  Added by Eoin K 10/12/20
        */
        [TestMethod]
        public void RecorderManagerCreateContact()
        {
            // Arranging the test
            // Reset the RecorderManager as it has been used in previous tests
            new PrivateType(typeof(RecorderManager)).SetStaticField("_Instance", null);

            RecorderManager rm = RecorderManager.Instance;

            // Acting out the test
            rm.RecordEvent(MockEventDate3, new List<User>()
            {
                MockUser1,
                MockUser2
            });

            // Asserting the expected result
            // Find the contacts with user 1 after mock date 1. This will return a phone number from the contact the test just created by calling r.RecordEvent
            List<string> ContactPhoneNumbers = rm.ListContacts(MockEventDate2, MockUser1);

            Assert.AreEqual(ContactPhoneNumbers[0], MockUser2.PhoneNumber);
        }

        /* Test 4
        *  Test that RecorderManager.RecordEvent() method signature 1 will run validation on the number of individuals before creating a Contact 
        *  
        *  Added by Eoin K 10/12/20
        */
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "l_Individuals holds less than 2 Users, invalid length: 1")]
        public void RecorderManagerCreateVisitValidation1()
        {
            // Arranging the test
            // Reset the RecorderManager as it has been used in previous tests
            new PrivateType(typeof(RecorderManager)).SetStaticField("_Instance", null);

            RecorderManager rm = RecorderManager.Instance;

            // Acting out the test
            rm.RecordEvent(MockEventDate1, new List<User>()
            {
                MockUser1,
            });
            ArgumentException InvalidArgument = Assert.ThrowsException<ArgumentException>(() => rm.RecordEvent(MockEventDate1, new List<User>()
            {
                MockUser1
            }));

            Assert.AreEqual(InvalidArgument.Message, "l_Individuals holds less than 2 Users, invalid length: 1");
        }

        /* Test 5
        *  Test that RecorderManager.RecordEvent() method signature 1 will run validation on the number of individuals before creating a Contact 
        *  
        *  Added by Eoin K 10/12/20
        */
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "l_Individuals holds more than 2 Users, invalid length: 3")]
        public void RecorderManagerCreateVisitValidation2()
        {
            // Arranging the test
            // Reset the RecorderManager as it has been used in previous tests
            new PrivateType(typeof(RecorderManager)).SetStaticField("_Instance", null);

            RecorderManager rm = RecorderManager.Instance;

            // Acting out the test
            rm.RecordEvent(MockEventDate1, new List<User>()
            {
                MockUser1,
                MockUser2,
                MockUser3
            });
            ArgumentException InvalidArgument = Assert.ThrowsException<ArgumentException>(() => rm.RecordEvent(MockEventDate1, new List<User>()
            {
                MockUser1,
                MockUser2,
                MockUser3
            }));

            Assert.AreEqual(InvalidArgument.Message, "l_Individuals holds more than 2 Users, invalid length: 3");
        }

        /* Test 6
        *  Test that RecorderManager.RecordEvent() method signature 2 will create a new event of type Visit and add the created Visit to the Visit collection
        *  The assert is not as simple as checking the result of the collection so the phone numbers are compared as an assert
        *  
        *  Added by Eoin K 10/12/20
        */
        [TestMethod]
        public void RecorderManagerCreateVisit()
        {
            // Arranging the test
            // Reset the RecorderManager as it has been used in previous tests
            new PrivateType(typeof(RecorderManager)).SetStaticField("_Instance", null);

            RecorderManager rm = RecorderManager.Instance;

            // Acting out the test
            rm.RecordEvent(MockEventDate3, MockUser1, MockLocation1);

            // Asserting the expected result
            // Find the visits at location 1 between mock date 2 & 4. This will return a phone number from the visit the test just created by calling r.RecordEvent
            List<string> VisitPhoneNumbers = rm.ListVisits(MockEventDate2, MockEventDate4, MockLocation1);

            Assert.AreEqual(VisitPhoneNumbers[0], MockUser1.PhoneNumber);
        }

        /* Test 7
        *  Test that RecorderManager.ListContacts() will find & return a list of string phone numbers
        *   , belonging to individuals that have been in contact with a  specified individual after specified date & time
        *  
        *  Added by Eoin K 10/12/20
        */
        [TestMethod]
        public void RecorderManagerListContacts()
        {
            // Arranging the test
            // Reset the RecorderManager as it has been used in previous tests
            new PrivateType(typeof(RecorderManager)).SetStaticField("_Instance", null);

            RecorderManager rm = RecorderManager.Instance;
            rm.RecordEvent(MockEventDate1, new List<User>()
            {
                MockUser1,
                MockUser2
            });
            rm.RecordEvent(MockEventDate3, new List<User>()
            {
                MockUser3,
                MockUser4
            });
            rm.RecordEvent(MockEventDate5, new List<User>()
            {
                MockUser5,
                MockUser6
            });

            // Acting out the test
            // Find the visits at location 1 between mock date 2 & 4. This will return a phone number from the visit the test just created by calling r.RecordEvent
            List<string> VisitPhoneNumbers = rm.ListContacts(MockEventDate4, MockUser5);

            // Asserting the expected result

            Assert.AreEqual(VisitPhoneNumbers[0], MockUser6.PhoneNumber);
        }

        /* Test 8
        *  Test that RecorderManager.ListVisits() will find & return a list of string phone numbers
        *   , belonging to individuals that have visited a specified place during a specified time period
        *  
        *  Added by Eoin K 10/12/20
        */
        [TestMethod]
        public void RecorderManagerListVisits()
        {
            // Arranging the test
            // Reset the RecorderManager as it has been used in previous tests
            new PrivateType(typeof(RecorderManager)).SetStaticField("_Instance", null);

            RecorderManager rm = RecorderManager.Instance;
            rm.RecordEvent(MockEventDate1, MockUser1, MockLocation1);
            rm.RecordEvent(MockEventDate3, MockUser2, MockLocation1);
            rm.RecordEvent(MockEventDate5, MockUser3, MockLocation1);

            // Acting out the test
            // Find the visits at location 1 between mock date 2 & 4. This will return a phone number from the visit the test just created by calling r.RecordEvent
            List<string> VisitPhoneNumbers = rm.ListVisits(MockEventDate2, MockEventDate4, MockLocation1);

            // Asserting the expected result

            Assert.AreEqual(VisitPhoneNumbers[0], MockUser2.PhoneNumber);
        }

        /* Test 9
        *  Test that RecorderManager.ContactCount()  will return the number of Contacts stored in the system
        *  
        *  Added by Eoin K 11/12/20
        */
        [TestMethod]
        public void RecorderManagerContactCount()
        {
            // Arranging the test
            // Reset the RecorderManager as it has been used in previous tests
            new PrivateType(typeof(RecorderManager)).SetStaticField("_Instance", null);

            RecorderManager rm = RecorderManager.Instance;
            rm.RecordEvent(MockEventDate1, new List<User>()
            {
                MockUser1,
                MockUser2
            });
            rm.RecordEvent(MockEventDate3, new List<User>()
            {
                MockUser3,
                MockUser4
            });
            rm.RecordEvent(MockEventDate5, new List<User>()
            {
                MockUser5,
                MockUser6
            });

            // Acting out the test
            int ActualContactCount = rm.ContactCount();

            // Asserting the expected result
            int ExpectedContactCount = 3;
            Assert.AreEqual(ActualContactCount, ExpectedContactCount);
        }

        /* Test 10
        *  Test that RecorderManager.VisitCount()  will return the number of Visits stored in the system
        *  
        *  Added by Eoin K 11/12/20
        */
        [TestMethod]
        public void RecorderManagerVisitCount()
        {
            // Arranging the test
            // Reset the RecorderManager as it has been used in previous tests
            new PrivateType(typeof(RecorderManager)).SetStaticField("_Instance", null);

            RecorderManager rm = RecorderManager.Instance;
            rm.RecordEvent(MockEventDate1, MockUser1, MockLocation1);
            rm.RecordEvent(MockEventDate3, MockUser2, MockLocation1);
            rm.RecordEvent(MockEventDate5, MockUser3, MockLocation1);

            // Acting out the test
            int ActualVisitCount = rm.VisitCount();

            // Asserting the expected result
            int ExpectedVisitCount = 3;
            Assert.AreEqual(ActualVisitCount, ExpectedVisitCount);
        }
    }
}
