/* ContactTest.cs
 * ContactTest.cs is a unit test for BusinessLayer/Contact.cs
 * 
 * Written By Eoin K 07/12/20
 */
using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrackTraceProject.BusinessLayer;

namespace TrackTraceTestProject.BusinessLayerTest
{
    /* The tests to run for Contact
    * Should assert that Contact is a child class of event
    * Should assert that Contact has one declared property, List<User> Individuals
    */
    [TestClass]
    public class ContactTest
    {
        // Mock Data to be used in the tests.
        private int MockEventID = 0;
        private DateTime MockDateAndTime = DateTime.Now;
        private List<User> MockIndividuals = new List<User>(2){
             new User(1, "+447221 523641"),
             new User(2, "+447335 774855")
        };
        private List<User> MockInvalidIndividuals1 = new List<User>(2){
             new User(1, "+447221 523641"),
        };
        private List<User> MockInvalidIndividuals2 = new List<User>(2){
             new User(1, "+447221 523641"),
             new User(2, "+447335 774855"),
             new User(3, "+447446 885944")
        };

        /* Test 1
        *  Test that Contact successfully builds
        *  Added by Eoin K 07/12/20
        */
        [TestMethod]
        public void ContactBuildsSuccessfully()
        {
            Contact c = new Contact(MockEventID, MockDateAndTime, MockIndividuals);

            Assert.IsInstanceOfType(c, typeof(Contact));
        }

        /* Test 2
        *  Test that Contact is a child class of Event
        *  Added by Eoin K 07/12/20
        */
        [TestMethod]
        public void ContactInheritsFromEvent()
        {
            Assert.AreEqual(typeof(Contact).BaseType, typeof(Event));
        }

        /* Test 3
        *  Test that Contact has one declared property, Individuals
        *  Added by Eoin K 07/12/20
        */
        [TestMethod]
        public void ContactHasTwoDeclaredProperties()
        {
            Assert.IsTrue(typeof(Contact).GetProperty("Individuals").DeclaringType == typeof(Contact));
            Assert.IsTrue(typeof(Contact).GetProperty("Individuals") != null);
        }

        /* Test 4
        *  Test that Contact's property, Individuals is the correct type: List<User>
        *  Added by Eoin K 07/12/20
        */
        [TestMethod]
        public void ContactPropertyTypes()
        {
            Assert.IsTrue(typeof(Contact).GetProperty("Individuals").PropertyType == typeof(List<User>));
        }

        /* Test 5
        *  Test that Contact's property is assigned the correct value from the constructor
        *  Added by Eoin K 07/12/20
        */
        [TestMethod]
        public void ContactConstructorAssignment()
        {
            Contact c = new Contact(MockEventID, MockDateAndTime, MockIndividuals);

            CollectionAssert.AreEqual(c.Individuals, MockIndividuals);
        }

        /* Test 6
        *  Test that User only accepts a valid list for Individuals in the constructor
        *  Individuals has to have a count of 2, MockInvalidIndividuals1 has a count of 1
        *  Added by Eoin K 07/12/20
        */
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "l_Individuals holds less than 2 Users, invalid length: 1")]
        public void ContactIndividualsTooSmallValidation()
        {
            Contact c = new Contact(MockEventID, MockDateAndTime, MockInvalidIndividuals1);

            ArgumentException InvalidArgument = Assert.ThrowsException<ArgumentException>(() => new Contact(MockEventID, MockDateAndTime, MockInvalidIndividuals1));

            Assert.AreEqual(InvalidArgument.Message, "l_Individuals holds less than 2 Users, invalid length: 1");
        }

        /* Test 7
        *  Test that User only accepts a valid list for Individuals in the constructor
        *  Individuals has to have a count of 2, MockInvalidIndividuals1 has a count of 3
        *  Added by Eoin K 07/12/20
        */
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "l_Individuals holds less than 2 Users, invalid length: 1")]
        public void ContactIndividualsTooLargeValidation()
        {
            Contact c = new Contact(MockEventID, MockDateAndTime, MockInvalidIndividuals2);

            ArgumentException InvalidArgument = Assert.ThrowsException<ArgumentException>(() => new Contact(MockEventID, MockDateAndTime, MockInvalidIndividuals2));

            Assert.AreEqual(InvalidArgument.Message, "l_Individuals holds less than 2 Users, invalid length: 2");
        }
    }
}
