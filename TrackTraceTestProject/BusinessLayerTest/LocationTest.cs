/* LocationTest.cs
 * LocationTest.cs is a unit test for BusinessLayer/Location.cs
 * 
 * Written By Eoin K 07/12/20
 */
using System;
using TrackTraceProject.BusinessLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace TrackTraceTestProject.BusinessLayerTest
{
    /* The tests to run for Location
    * Should assert Location has 5 properties: LocationID; Name; Address; PostalCode; and Country;
    */
    [TestClass]
    public class LocationTest
    {
        // Mock Data to be used in the tests.
        private int MockLocationID = 0;
        private string MockName = "Eat Cafe";
        private string MockAddress = "1 Road, Town";
        private string MockValidPostalCode = "BB66 7LL";
        private string MockCountry = "United Kingdom";
        private string MockInvalidPostalCode = "8JJ99JJ";
        private string MockInvalidPostalCodeExceptionMessage = "PostalCode 8JJ99JJ is not" +
                    " in the any of the following formats: " +
                    "AA9A 9AA; A9A 9AA; A9 9AA; A99 9AA; AA9 9AA; AA99 9AA.";

        /* Test 1
        *  Test that Location successfully builds
        *  Added by Eoin K 07/12/20
        */
        [TestMethod]
        public void LocationBuildsSuccessfully()
        {
            Location l = new Location(MockLocationID, MockName, MockAddress, MockValidPostalCode,  MockCountry);

            Assert.IsInstanceOfType(l, typeof(Location));
        }

        /* Test 2
        *  Test that User has five properties: LocationID; Name; Address; PostalCode; and Country.
        *  Added by Eoin K 07/12/20
        */
        [TestMethod]
        public void LocationHasFiveProperties()
        {
            Location l = new Location(MockLocationID, MockName, MockAddress, MockValidPostalCode, MockCountry);

            Assert.IsTrue(l.GetType().GetProperty("LocationID") != null);
            Assert.IsTrue(l.GetType().GetProperty("Name") != null);
            Assert.IsTrue(l.GetType().GetProperty("Address") != null);
            Assert.IsTrue(l.GetType().GetProperty("PostalCode") != null);
            Assert.IsTrue(l.GetType().GetProperty("Country") != null);
        }

        /* Test 3
        *  Test that Locations's five properties have the correct type
        *   LocationID: int
        *   Name: string
        *   Address: string
        *   PostalCode: string
        *   Country: string
        *  Added by Eoin K 07/12/20
        */
        [TestMethod]
        public void LocationPropertyTypes()
        {
            Location l = new Location(MockLocationID, MockName, MockAddress, MockValidPostalCode, MockCountry);

            Assert.AreEqual(l.LocationID.GetTypeCode(), TypeCode.Int32);
            Assert.AreEqual(l.Name.GetTypeCode(), TypeCode.String);
            Assert.AreEqual(l.Address.GetTypeCode(), TypeCode.String);
            Assert.AreEqual(l.PostalCode.GetTypeCode(), TypeCode.String);
            Assert.AreEqual(l.Country.GetTypeCode(), TypeCode.String);
        }

        /* Test 4
        *  Test that Locations's five properties are assigned their correct values from the constructor
        *  Added by Eoin K 07/12/20
        */
        [TestMethod]
        public void LocationConstructorAssignment()
        {
            Location l = new Location(MockLocationID, MockName, MockAddress, MockValidPostalCode, MockCountry);

            Assert.AreEqual(l.LocationID, MockLocationID);
            Assert.AreEqual(l.Name, MockName);
            Assert.AreEqual(l.Address, MockAddress);
            Assert.AreEqual(l.PostalCode, MockValidPostalCode);
            Assert.AreEqual(l.Country, MockCountry);
        }

        /* Test 5
        *  Test that Location only accepts a valid  PostalCode in the constructor
        *  Added by Eoin K 07/12/20
        */
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "PostalCode 8JJ99JJ is not" +
                    " in the any of the following formats: " +
                    "AA9A 9AA; A9A 9AA; A9 9AA; A99 9AA; AA9 9AA; AA99 9AA.")]
        public void LocationPostalCodeConstructorValidation()
        {
            Location l = new Location(MockLocationID, MockName, MockAddress, MockInvalidPostalCode, MockCountry);

            ArgumentException InvalidArgument = Assert.ThrowsException<ArgumentException>(() => new Location(MockLocationID, MockName, MockAddress, MockInvalidPostalCode, MockCountry));

            Assert.AreEqual(InvalidArgument.Message, MockInvalidPostalCodeExceptionMessage);
        }

        /* Test 6
        *  Test that Location only accepts a valid PostalCode through the property setter
        *  Added by Eoin K 08/12/20
        */
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "PostalCode 8JJ99JJ is not" +
                    " in the any of the following formats: " +
                    "AA9A 9AA; A9A 9AA; A9 9AA; A99 9AA; AA9 9AA; AA99 9AA.")]
        public void LocationPostalCodePropertyValidation()
        {
            Location l = new Location(MockLocationID, MockName, MockAddress, MockValidPostalCode, MockCountry);
            l.PostalCode = MockInvalidPostalCode;
            l.PostalCode = MockValidPostalCode;

            ArgumentException InvalidArgument = Assert.ThrowsException<ArgumentException>(() => l.PostalCode = MockInvalidPostalCode);

            Assert.AreEqual(InvalidArgument.Message, MockInvalidPostalCodeExceptionMessage);
        }
    }
}
