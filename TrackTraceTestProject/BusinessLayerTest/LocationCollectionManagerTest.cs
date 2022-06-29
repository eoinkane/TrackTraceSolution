/* LocationCollectionManagerTest.cs
 * LocationCollectionManagerTest.cs is a unit test for BusinessLayer/LocationCollectionManager.cs
 * 
 * Written By Eoin K 07/12/20
 * Updated By Eoin K 10/12/20
 */
using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrackTraceProject.BusinessLayer;

namespace TrackTraceTestProject.BusinessLayerTest
{
    /* The tests to run for LocationCollectionManager
    * Should assert that LocationCollectionManager is a singleton class
    * Should assert that LocationCollectionManager has one declared property, LocationCollection Instance
    * Should assert that LocationCollectionManager can create a new location in the track-and-trace system
    * Should assert that LocationCollectionManager can search its collection for a location with a specified LocationID
    */
    [TestClass]
    public class LocationCollectionManagerTest
    {
        // Mock Data to be used in the tests.
        private int MockLocationID = 1;

        private string MockLocationName = "Eat Cafe";
        private string MockLocationName2 = "Buy Shop";
        private string MockLocationName3 = "Drink Restaurant";

        private string MockLocationAddress = "1 Road, Town";
        private string MockLocationAddress2 = "2 Street, Village";
        private string MockLocationAddress3 = "3 Loan, City";

        private string MockLocationValidPostalCode = "BB4 3CD";
        private string MockLocationValidPostalCode2 = "U2J 7LL";
        private string MockLocationValidPostalCode3 = "WQ12 8UK";

        private string MockLocationCountry = "United Kingdom";

        private string MockLocationInvalidPostalCode = "JJ00J I 8YY";

        /* Test 1
        *  Test that LocationCollectionManager can be accessed by the instance property
        *  This test will test that the class implements the Singleton Design Pattern
        *  Added by Eoin K 08/12/20
        *  Updated By Eoin K 10/12/20
        */
        [TestMethod]
        public void LocationCollectionManagerBuildsSuccessFully()
        {
            LocationCollectionManager lcm = LocationCollectionManager.Instance;

            Assert.IsInstanceOfType(lcm, typeof(LocationCollectionManager));
        }

        /* Test 2
        *  Test that multiple assocations to LocationCollectionManager.Instance will access the same instance
        *  This test will test that the class implements the Singleton Design Pattern
        *  Added by Eoin K 08/12/20
        *  Updated By Eoin K 10/12/20
        */
        [TestMethod]
        public void LocationCollectionManagerHasOnlyOneInstance()
        {
            LocationCollectionManager lcm1 = LocationCollectionManager.Instance;
            LocationCollectionManager lcm2 = LocationCollectionManager.Instance;

            Assert.AreSame(lcm1, lcm2);
        }

        /* Test 3
        *  Test that LocationCollectionManager.Add() will create a new Location and add the created location to the collection
        *  Added by Eoin K 08/12/20
        *  Updated By Eoin K 10/12/20
        */
        [TestMethod]
        public void LocationCollectionManagerCreateLocation()
        {
            // Arranging the test
            // Reset the LocationCollection as it has been used in previous tests
            new PrivateType(typeof(LocationCollectionManager)).SetStaticField("_Instance", null);

            LocationCollectionManager lcm = LocationCollectionManager.Instance;

            // Acting out the test
            lcm.Add(MockLocationName, MockLocationAddress, MockLocationValidPostalCode, MockLocationCountry);

            // Asserting the expected result
            // Find Location 1 as the List will only have 1 location. This will return the location the test just created by calling lc.Add
            Location FoundLocation = lcm.Find(1);

            Assert.AreEqual(FoundLocation.LocationID, MockLocationID);
            Assert.AreEqual(FoundLocation.Name, MockLocationName);
            Assert.AreEqual(FoundLocation.Address, MockLocationAddress);
            Assert.AreEqual(FoundLocation.PostalCode, MockLocationValidPostalCode);
            Assert.AreEqual(FoundLocation.Country, MockLocationCountry);
        }

        /* Test 4
        *  Test that LocationCollectionManager.Add() will run validation on the postal code before creating a location
        *  Added by Eoin K 08/12/20
        *  Updated By Eoin K 10/12/20
        */
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "PostalCode JJ00J I 8YY is not" +
                    " in the any of the following formats: " +
                    "AA9A 9AA; A9A 9AA; A9 9AA; A99 9AA; AA9 9AA; AA99 9AA.")]
        public void LocationCollectionManagerCreateUserValidation()
        {
            // Reset the LocationCollection as it has been used in previous tests
            new PrivateType(typeof(LocationCollectionManager)).SetStaticField("_Instance", null);

            LocationCollectionManager lcm = LocationCollectionManager.Instance;

            lcm.Add(MockLocationName, MockLocationAddress, MockLocationInvalidPostalCode, MockLocationCountry);

            ArgumentException InvalidArgument = Assert.ThrowsException<ArgumentException>(() =>
                lcm.Add(MockLocationName, MockLocationAddress, MockLocationInvalidPostalCode, MockLocationCountry));

            Assert.AreEqual(InvalidArgument.Message, "PostalCode JJ00J I 8YY is not" +
                    " in the any of the following formats: " +
                    "AA9A 9AA; A9A 9AA; A9 9AA; A99 9AA; AA9 9AA; AA99 9AA.");
        }

        /* Test 5
        *  Test that LocationCollectionManager.Find() will find & return a Location instance with the matching LocationID
        *  Added by Eoin K 08/12/20
        *  Updated By Eoin K 10/12/20
        */
        [TestMethod]
        public void LocationCollectionManagerFindLocation()
        {
            // Arranging the Test
            // Reset the LocationCollectionManager as it has been used in previous tests
            new PrivateType(typeof(LocationCollectionManager)).SetStaticField("_Instance", null);

            LocationCollectionManager lcm = LocationCollectionManager.Instance;
            lcm.Add(MockLocationName, MockLocationAddress, MockLocationValidPostalCode, MockLocationCountry);
            lcm.Add(MockLocationName2, MockLocationAddress2, MockLocationValidPostalCode2, MockLocationCountry);
            lcm.Add(MockLocationName3, MockLocationAddress3, MockLocationValidPostalCode3, MockLocationCountry);

            // Acting out the test
            // Find Location 2 as this location is in the middle of the collection
            Location FoundLocation = lcm.Find(2);

            // Asserting the test
            Assert.AreEqual(FoundLocation.LocationID, 2);
            Assert.AreEqual(FoundLocation.Name, MockLocationName2);
            Assert.AreEqual(FoundLocation.Address, MockLocationAddress2);
            Assert.AreEqual(FoundLocation.PostalCode, MockLocationValidPostalCode2);
            Assert.AreEqual(FoundLocation.Country, MockLocationCountry);
        }

        /* Test 6
        *  Test that LocationCollectionManager.Find() will run validation on the target LocationID before searching the collection
        *  Added by Eoin K 08/12/20
        *  Updated By Eoin K 10/12/20
        */
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "l_LocationID 2 is out of range of the location collection")]
        public void LocationCollectionManagerFindUserValidation()
        {
            // Arranging the test
            // Reset the LocationCollection as it has been used in previous tests
            new PrivateType(typeof(LocationCollectionManager)).SetStaticField("_Instance", null);

            LocationCollectionManager lcm = LocationCollectionManager.Instance;

            // Acting out the test
            // Find Location 2 as this location is not in the collection as no calls to lc.Add have been made
            Location FoundLocation = lcm.Find(2);
            ArgumentException InvalidArgument = Assert.ThrowsException<ArgumentException>(() => lcm.Find(2));

            // Asserting the test
            Assert.AreEqual(InvalidArgument.Message, "l_LocationID 2 is out of range of the location collection");
        }

        /* Test 7
        *  Test that LocationCollection.List() will  return a List of IDs in the LocationCollection
        *  Added by Eoin K 11/12/20
        */
        [TestMethod]
        public void LocationCollectionManagerListIDs()
        {
            // Arranging the Test
            // Reset the LocationCollectionManager as it has been used in previous tests
            new PrivateType(typeof(LocationCollectionManager)).SetStaticField("_Instance", null);

            LocationCollectionManager lcm = LocationCollectionManager.Instance;
            lcm.Add(MockLocationName, MockLocationAddress, MockLocationValidPostalCode, MockLocationCountry);
            lcm.Add(MockLocationName2, MockLocationAddress2, MockLocationValidPostalCode2, MockLocationCountry);
            lcm.Add(MockLocationName3, MockLocationAddress3, MockLocationValidPostalCode3, MockLocationCountry);

            // Acting out the test
            List<int> IDs = lcm.ListIDs();

            // Asserting the test
            List<int> ExpectedIDs = new List<int>
            {
                1,
                2,
                3
            };
            CollectionAssert.AreEqual(IDs, ExpectedIDs);
        }
    }
}
