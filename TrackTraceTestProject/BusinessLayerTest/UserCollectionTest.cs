/* UserCollectionTest.cs
 * UserCollectionTest.cs is a unit test for BusinessLayer/UserCollection.cs
 * 
 * Written By Eoin K 08/12/20
 * Updated By Eoin K 10/12/20
 */
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrackTraceProject.BusinessLayer;

namespace TrackTraceTestProject.BusinessLayerTest
{
    /* The tests to run for UserCollection
    * Should assert that UserCollection has one declared property, UserCollection Instance
    * Should assert that UserCollection can create a new individual in the track-and-trace system
    * Should assert that UserCollection can search its collection for a individual with a specified UserID
    */
    [TestClass]
    public class UserCollectionTest
    {
        // Mock Data to be used in the tests.
        private int MockUserID = 1;
        private string MockUserValidPhoneNumber = "+447226 115877";
        private string MockUserValidPhoneNumber2 = "+447348 442988";
        private string MockUserValidPhoneNumber3 = "+447571 361522";
        private string MockUserInvalidPhoneNumber = "+4476  11 57";

        /* Test 1
        *  Test that UserCollection can be created by the public constructor
        *  Added by Eoin K 08/12/20
        *  Updated By Eoin K 10/12/20
        */
        [TestMethod]
        public void UserCollectionBuildsSuccessFully()
        {
            UserCollection uc = new UserCollection();

            Assert.IsInstanceOfType(uc, typeof(UserCollection));
        }

        /* Test 2
        *  Test that UserCollection can be created by the public constructor with existing data
        *  Added by Eoin K 13/12/20
        */
        [TestMethod]
        public void ExistingUserCollectionBuildsSuccessFully()
        {
            UserCollection uc = new UserCollection(new List<User>()
            {
                new User(1,MockUserValidPhoneNumber)
            });

            Assert.IsInstanceOfType(uc, typeof(UserCollection));
        }

        /* Test 3
        *  Test that UserCollection.Add() will create a new User and add the created user to the collection
        *  This test will test that the class implements the Factory Design Pattern
        *  Added by Eoin K 08/12/20
        *  Updated By Eoin K 10/12/20
        */
        [TestMethod]
        public void UserCollectionCreateUser()
        {
            // Arranging the test
            UserCollection uc = new UserCollection();

            // Acting out the test
            uc.Add(MockUserValidPhoneNumber);

            // Asserting the expected result
            // Find User 1 as the List will only have 1 user. This will return the user the test just created by calling uc.Add
            User FoundUser = uc.Find(1);

            Assert.AreEqual(FoundUser.UserID, MockUserID);
            Assert.AreEqual(FoundUser.PhoneNumber, MockUserValidPhoneNumber);
        }

        /* Test 4
        *  Test that UserCollection.Add() will run validation on the phone number before creating a user
        *  Added by Eoin K 08/12/20
        *  Updated By Eoin K 10/12/20
        */
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "l_PhoneNumber + 4476  11 57 is not a valid phone number")]
        public void UserCollectionCreateUserValidation()
        {
            // Reset the UserCollection as it has been used in previous tests
            UserCollection uc = new UserCollection();

            uc.Add(MockUserInvalidPhoneNumber);

            ArgumentException InvalidArgument = Assert.ThrowsException<ArgumentException>(() => uc.Add(MockUserInvalidPhoneNumber));

            Assert.AreEqual(InvalidArgument.Message, "l_PhoneNumber + 4476  11 57 is not a valid phone number");
        }

        /* Test 5
        *  Test that UserCollection.Find() will find & return a User instance with the matching UserID
        *  Added by Eoin K 08/12/20
        *  Updated By Eoin K 10/12/20
        */
        [TestMethod]
        public void UserCollectionFindUser()
        {
            // Arranging the Test
            UserCollection uc = new UserCollection();
            uc.Add(MockUserValidPhoneNumber);
            uc.Add(MockUserValidPhoneNumber2);
            uc.Add(MockUserValidPhoneNumber3);

            // Acting out the test
            // Find User 2 as this user is in the middle of the collection
            User FoundUser = uc.Find(2);

            // Asserting the test
            Assert.AreEqual(FoundUser.UserID, 2);
            Assert.AreEqual(FoundUser.PhoneNumber, MockUserValidPhoneNumber2);
        }

        /* Test 6
        *  Test that UserCollection.Find() will run validation on the target UserID before searching the collection
        *  Added by Eoin K 08/12/20
        *  Updated By Eoin K 10/12/20
        */
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "l_UserID 2 is out of range of the user collection")]
        public void UserCollectionFindUserValidation()
        {
            // Arranging the test
             UserCollection uc = new UserCollection();

            // Acting out the test
            // Find User 2 as this user is not in the collection as no calls to uc.Add have been made
            User FoundUser = uc.Find(2);
            ArgumentException InvalidArgument = Assert.ThrowsException<ArgumentException>(() => uc.Find(2));

            // Asserting the test
            Assert.AreEqual(InvalidArgument.Message, "l_UserID 2 is out of range of the user collection");
        }

        /* Test 7
        *  Test that UserCollection.List() will  return a List of IDs in the UserCollection
        *  Added by Eoin K 11/12/20
        */
        [TestMethod]
        public void UserCollectionListIDs()
        {
            // Arranging the Test
            UserCollection uc = new UserCollection();

            uc.Add(MockUserValidPhoneNumber);
            uc.Add(MockUserValidPhoneNumber2);
            uc.Add(MockUserValidPhoneNumber3);

            // Acting out the test
            List<int> IDs = uc.ListIDs();

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
