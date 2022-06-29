/* LocationCollectionTest.cs
 * LocationCollectionTest.cs is a unit test for BusinessLayer/LocationCollection.cs
 * 
 * Written By Eoin K 07/12/20
 */
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrackTraceProject.BusinessLayer;

namespace TrackTraceTestProject.BusinessLayerTest
{
    /* The tests to run for LocationCollection
    * Should assert that LocationCollection is a singleton class
    * Should assert that LocationCollection has one declared property, LocationCollection Instance
    * Should assert that LocationCollection can create a new location in the track-and-trace system
    * Should assert that LocationCollection can search its collection for a location with a specified LocationID
    */
    [TestClass]
    public class LocationCollectionTest
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
        *  Test that LocationCollection can be accessed by the instance property
        *  This test will test that the class implements the Singleton Design Pattern
        *  Added by Eoin K 08/12/20
        */
        [TestMethod]
        public void LocationCollectionBuildsSuccessFully()
        {
            LocationCollection lc = new LocationCollection();

            Assert.IsInstanceOfType(lc, typeof(LocationCollection));
        }

        /* Test 2
        *  Test that LocationCollection can be created by the public constructor with existing data
        *  Added by Eoin K 13/12/20
        */
        [TestMethod]
        public void ExistingLocationCollectionBuildsSuccessFully()
        {
            LocationCollection lc = new LocationCollection(new List<Location>()
            {
                new Location (1, MockLocationName, MockLocationAddress, MockLocationValidPostalCode, MockLocationCountry)
            });

            Assert.IsInstanceOfType(lc, typeof(LocationCollection));
        }

        /* Test 3
        *  Test that LocationCollection.Add() will create a new Location and add the created location to the collection
        *  This test will test that the class implements the Factory Design Pattern
        *  Added by Eoin K 08/12/20
        *  Updated By Eoin K 10/12/20
        */
        [TestMethod]
        public void LocationCollectionCreateLocation()
        {
            // Arranging the test
            LocationCollection lc = new LocationCollection();

            // Acting out the test
            lc.Add(MockLocationName, MockLocationAddress, MockLocationValidPostalCode, MockLocationCountry);

            // Asserting the expected result
            // Find Location 1 as the List will only have 1 location. This will return the location the test just created by calling lc.Add
            Location FoundLocation = lc.Find(1);

            Assert.AreEqual(FoundLocation.LocationID, MockLocationID);
            Assert.AreEqual(FoundLocation.Name, MockLocationName);
            Assert.AreEqual(FoundLocation.Address, MockLocationAddress);
            Assert.AreEqual(FoundLocation.PostalCode, MockLocationValidPostalCode);
            Assert.AreEqual(FoundLocation.Country, MockLocationCountry);
        }

        /* Test 4
        *  Test that LocationCollection.Add() will run validation on the postal code before creating a location
        *  Added by Eoin K 08/12/20
        *  Updated By Eoin K 10/12/20
        */
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "PostalCode JJ00J I 8YY is not" +
                    " in the any of the following formats: " +
                    "AA9A 9AA; A9A 9AA; A9 9AA; A99 9AA; AA9 9AA; AA99 9AA.")]
        public void LocationCollectionCreateUserValidation()
        {
            // Reset the LocationCollection as it has been used in previous tests
            LocationCollection lc = new LocationCollection();

            lc.Add(MockLocationName, MockLocationAddress, MockLocationInvalidPostalCode, MockLocationCountry);

            ArgumentException InvalidArgument = Assert.ThrowsException<ArgumentException>(() =>
                lc.Add(MockLocationName, MockLocationAddress, MockLocationInvalidPostalCode, MockLocationCountry));

            Assert.AreEqual(InvalidArgument.Message, "PostalCode JJ00J I 8YY is not" +
                    " in the any of the following formats: " +
                    "AA9A 9AA; A9A 9AA; A9 9AA; A99 9AA; AA9 9AA; AA99 9AA.");
        }

        /* Test 5
        *  Test that LocationCollection.Find() will find & return a Location instance with the matching LocationID
        *  Added by Eoin K 08/12/20
        *  Updated By Eoin K 10/12/20
        */
        [TestMethod]
        public void LocationCollectionFindLocation()
        {
            // Arranging the Test
            LocationCollection lc = new LocationCollection();

            lc.Add(MockLocationName, MockLocationAddress, MockLocationValidPostalCode, MockLocationCountry);
            lc.Add(MockLocationName2, MockLocationAddress2, MockLocationValidPostalCode2, MockLocationCountry);
            lc.Add(MockLocationName3, MockLocationAddress3, MockLocationValidPostalCode3, MockLocationCountry);

            // Acting out the test
            // Find Location 2 as this location is in the middle of the collection
            Location FoundLocation = lc.Find(2);

            // Asserting the test
            Assert.AreEqual(FoundLocation.LocationID, 2);
            Assert.AreEqual(FoundLocation.Name, MockLocationName2);
            Assert.AreEqual(FoundLocation.Address, MockLocationAddress2);
            Assert.AreEqual(FoundLocation.PostalCode, MockLocationValidPostalCode2);
            Assert.AreEqual(FoundLocation.Country, MockLocationCountry);
        }

        /* Test 6
        *  Test that LocationCollection.Find() will run validation on the target LocationID before searching the collection
        *  Added by Eoin K 08/12/20
        */
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "l_LocationID 2 is out of range of the location collection")]
        public void LocationCollectionFindUserValidation()
        {
            // Arranging the test
            LocationCollection lc = new LocationCollection();

            // Acting out the test
            // Find Location 2 as this location is not in the collection as no calls to lc.Add have been made
            Location FoundLocation = lc.Find(2);
            ArgumentException InvalidArgument = Assert.ThrowsException<ArgumentException>(() => lc.Find(2));

            // Asserting the test
            Assert.AreEqual(InvalidArgument.Message, "l_LocationID 2 is out of range of the location collection");
        }

        /* Test 7
        *  Test that LocationCollection.List() will  return a List of IDs in the LocationCollection
        *  Added by Eoin K 11/12/20
        */
        [TestMethod]
        public void LocationCollectionListIDs()
        {
            // Arranging the Test
            LocationCollection lc = new LocationCollection();

            lc.Add(MockLocationName, MockLocationAddress, MockLocationValidPostalCode, MockLocationCountry);
            lc.Add(MockLocationName2, MockLocationAddress2, MockLocationValidPostalCode2, MockLocationCountry);
            lc.Add(MockLocationName3, MockLocationAddress3, MockLocationValidPostalCode3, MockLocationCountry);

            // Acting out the test
            List<int> IDs = lc.ListIDs();

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
