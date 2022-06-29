/* EventTest.cs
 * EventTest.cs is a unit test for BusinessLayer/Event.cs
 * 
 * Written By Eoin K 06/12/20
 */
using System;
using TrackTraceProject.BusinessLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// TestEventObject is a mock implmentation of BusinessLayer.Event as Event is an abstract class
// TestEventObject will be used to set up the tests
class TestEventObject : Event
{
    public TestEventObject(int l_EventID, DateTime l_DateAndTime ): base(l_EventID, l_DateAndTime)
    {
        
    }
}

namespace TrackTraceTestProject.BusinessLayerTest
{
    /* The tests to run for Event
    * Should assert that Event allows other classes to inherit from it
    * Should assert that objects built with Event as a base have 2 properties, EventID and DateAndTime
    */
    [TestClass]
    public class EventTest
    {
        // Mock Data to be used in the tests.
        private int MockEventID = 0;
        private static DateTime now = DateTime.Now;
        private DateTime MockDateAndTime = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);

        /* Test 1
        *  Test that Event successfully builds
        *  Added by Eoin K 06/12/20
        */
        [TestMethod]
        public void EventBuildsSuccessfully()
        {
            TestEventObject t = new TestEventObject(MockEventID, MockDateAndTime);

            Assert.IsInstanceOfType(t, typeof(Event));
        }

        /* Test 2
        *  Test that Event has two properties, EventID and DateAndTime
        *  Added by Eoin K 06/12/20
        */
        [TestMethod]
        public void EventHasTwoProperties()
        {
            TestEventObject t = new TestEventObject(MockEventID, MockDateAndTime);

            Assert.IsTrue(typeof(Event).GetProperty("EventID") != null);
            Assert.IsTrue(typeof(Event).GetProperty("DateAndTime") != null);

            Assert.IsTrue(t.GetType().GetProperty("EventID") != null);
            Assert.IsTrue(t.GetType().GetProperty("DateAndTime") != null);
        }

        /* Test 3
        *  Test that Event's two properties, EventID and DateAndTime are the correct type: int & DateTime respectively
        *  Added by Eoin K 06/12/20
        */
        [TestMethod]
        public void EventPropertyTypes()
        {
            TestEventObject t = new TestEventObject(MockEventID, MockDateAndTime);

            
            Assert.IsTrue(typeof(Event).GetProperty("EventID").PropertyType == typeof(int));
            Assert.IsTrue(typeof(Event).GetProperty("DateAndTime").PropertyType == typeof(DateTime));

            Assert.IsTrue(t.EventID.GetTypeCode() == TypeCode.Int32);
            Assert.IsTrue(t.DateAndTime.GetTypeCode() == TypeCode.DateTime);
        }

        /* Test 4
        *  Test that Event's two properties are assigned their correct values from the constructor
        *  Added by Eoin K 06/12/20
        */
        [TestMethod]
        public void EventConstructorAssignment()
        {
            TestEventObject t = new TestEventObject(MockEventID, MockDateAndTime);

            Assert.AreEqual(t.EventID, MockEventID);
            Assert.AreEqual(t.DateAndTime, MockDateAndTime);
        }
    }
}
