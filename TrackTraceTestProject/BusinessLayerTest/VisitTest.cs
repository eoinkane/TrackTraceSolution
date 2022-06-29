/* VisitTest.cs
 * VisitTest.cs is a unit test for BusinessLayer/Visit.cs
 * 
 * Written By Eoin K 08/12/20
 */
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrackTraceProject.BusinessLayer;

namespace TrackTraceTestProject.BusinessLayerTest
{
    /* The tests to run for Visit
    * Should assert that Visit is a child class of event
    * Should assert that Visit has two declared properties, List<User> Individuals & Location Place
    */
    [TestClass]
    public class VisitTest
    {
        // Mock Data to be used in the tests.
        private int MockEventID = 0;
        private DateTime MockDateAndTime = DateTime.Now;
        private User MockIndividual = new User(1, "+447221 523641");
        private Location MockPlace = new Location(1, "Eat Cafe", "1 Road, Town", "BB66 7LL", "United Kingdom");

        /* Test 1
        *  Test that Visit successfully builds
        *  Added by Eoin K 08/12/20
        */
        [TestMethod]
        public void VisitBuildsSuccessfully()
        {
            Visit v = new Visit(MockEventID, MockDateAndTime, MockIndividual, MockPlace);

            Assert.IsInstanceOfType(v, typeof(Visit));
        }

        /* Test 2
        *  Test that Visit is a child class of Event
        *  Added by Eoin K 08/12/20
        */
        [TestMethod]
        public void VisitInheritsFromEvent()
        {
            Assert.AreEqual(typeof(Visit).BaseType, typeof(Event));
        }

        /* Test 3
        *  Test that Visit has two declared properties, Individual & Place
        *  Added by Eoin K 08/12/20
        */
        [TestMethod]
        public void VisitHasTwoDeclaredProperties()
        {
            Assert.IsTrue(typeof(Visit).GetProperty("Individual").DeclaringType == typeof(Visit));
            Assert.IsTrue(typeof(Visit).GetProperty("Individual") != null);

            Assert.IsTrue(typeof(Visit).GetProperty("Place").DeclaringType == typeof(Visit));
            Assert.IsTrue(typeof(Visit).GetProperty("Place") != null);
        }

        /* Test 4
        *  Test that Visit's properties, Individual & Place are the correct type: User & Location respectively
        *  Added by Eoin K 08/12/20
        */
        [TestMethod]
        public void VisitPropertyTypes()
        {
            Assert.IsTrue(typeof(Visit).GetProperty("Individual").PropertyType == typeof(User));
            Assert.IsTrue(typeof(Visit).GetProperty("Place").PropertyType == typeof(Location));
        }

        /* Test 5
        *  Test that Visit's property is assigned the correct value from the constructor
        *  Added by Eoin K 08/12/20
        */
        [TestMethod]
        public void VisitConstructorAssignment()
        {
            Visit v = new Visit(MockEventID, MockDateAndTime, MockIndividual, MockPlace);

            Assert.AreEqual(v.Individual, MockIndividual);
            Assert.AreEqual(v.Place, MockPlace);
        }
    }
}
