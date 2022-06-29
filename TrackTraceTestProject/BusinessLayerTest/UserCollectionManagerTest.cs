/* UserCollectionManagerTest.cs
 * UserCollectionManagerTest.cs is a unit test for BusinessLayer/UserCollectionManager.cs
 * 
 * UserCollectionManager is a wrapper for UserCollection
 * 
 * Written By Eoin K 10/21/20
 */
using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrackTraceProject.BusinessLayer;

namespace TrackTraceTestProject.BusinessLayerTest
{    
    /* The tests to run for UserCollectionManager
    * Should assert that UserCollectionManager is a singleton class
    * Should assert that UserCollectionManager has one declared field, UserCollection _UserCollection
    * Should assert that UserCollectionManager can create a new individual in the track-and-trace system
    * Should assert that UserCollectionManager can search its collection for a individual with a specified UserID
    */
    [TestClass]
    public class UserCollectionManagerTest
    {
        // Mock Data to be used in the tests.
        private int MockUserID = 1;
        private string MockUserValidPhoneNumber = "+447226 115877";
        private string MockUserValidPhoneNumber2 = "+447348 442988";
        private string MockUserValidPhoneNumber3 = "+447571 361522";
        private string MockUserInvalidPhoneNumber = "+4476  11 57";

        /* Test 1
        *  Test that UserCollection can be accessed by the instance property
        *  This test will test that the class implements the Singleton Design Pattern
        *  Added by Eoin K 10/21/20
        */
        [TestMethod]
        public void UserCollectionManagerBuildsSuccessFully()
        {
            UserCollectionManager ucm = UserCollectionManager.Instance;

            Assert.IsInstanceOfType(ucm, typeof(UserCollectionManager));
        }

        /* Test 2
        *  Test that multiple assocations to UserCollectionManager.Instance will access the same instance
        *  This test will test that the class implements the Singleton Design Pattern
        *  Added by Eoin K 10/21/20
        */
        [TestMethod]
        public void UserCollectionManagerHasOnlyOneInstance()
        {
            UserCollectionManager ucm1 = UserCollectionManager.Instance;
            UserCollectionManager ucm2 = UserCollectionManager.Instance;

            Assert.AreSame(ucm1, ucm2);
        }

        /* Test 3
        *  Test that UserCollectionManager.Add() will create a new User and add the created user to the collection
        *  Added by Eoin K 10/21/20
        */
        [TestMethod]
        public void UserCollectionManagerCreateUser()
        {
            // Arranging the test
            // Reset the UserCollectionManager as it has been used in previous tests
            new PrivateType(typeof(UserCollectionManager)).SetStaticField("_Instance", null);

            UserCollectionManager ucm = UserCollectionManager.Instance;

            // Acting out the test
            ucm.Add(MockUserValidPhoneNumber);

            // Asserting the expected result
            // Find User 1 as the List will only have 1 user. This will return the user the test just created by calling uc.Add
            User FoundUser = ucm.Find(1);

            Assert.AreEqual(FoundUser.UserID, MockUserID);
            Assert.AreEqual(FoundUser.PhoneNumber, MockUserValidPhoneNumber);
        }

        /* Test 4
        *  Test that UserCollectionManager.Add() will run validation on the phone number before creating a user
        *  Added by Eoin K 10/21/20
        */
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "l_PhoneNumber + 4476  11 57 is not a valid phone number")]
        public void UserCollectionManagerCreateUserValidation()
        {
            // Reset the UserCollectionManager as it has been used in previous tests
            new PrivateType(typeof(UserCollectionManager)).SetStaticField("_Instance", null);

            UserCollectionManager ucm = UserCollectionManager.Instance;

            ucm.Add(MockUserInvalidPhoneNumber);

            ArgumentException InvalidArgument = Assert.ThrowsException<ArgumentException>(() => ucm.Add(MockUserInvalidPhoneNumber));

            Assert.AreEqual(InvalidArgument.Message, "l_PhoneNumber + 4476  11 57 is not a valid phone number");
        }

        /* Test 5
        *  Test that UserCollectionManager.Find() will find & return a User instance with the matching UserID
        *  Added by Eoin K 10/21/20
        */
        [TestMethod]
        public void UserCollectionManagerFindUser()
        {
            // Arranging the Test
            // Reset the UserCollectionManager as it has been used in previous tests
            new PrivateType(typeof(UserCollectionManager)).SetStaticField("_Instance", null);

            UserCollectionManager ucm = UserCollectionManager.Instance;
            ucm.Add(MockUserValidPhoneNumber);
            ucm.Add(MockUserValidPhoneNumber2);
            ucm.Add(MockUserValidPhoneNumber3);

            // Acting out the test
            // Find User 2 as this user is in the middle of the collection
            User FoundUser = ucm.Find(2);

            // Asserting the test
            Assert.AreEqual(FoundUser.UserID, 2);
            Assert.AreEqual(FoundUser.PhoneNumber, MockUserValidPhoneNumber2);
        }

        /* Test 6
        *  Test that UserCollectionManager.Find() will run validation on the target UserID before searching the collection
        *  Added by Eoin K 10/21/20
        */
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "l_UserID 2 is out of range of the user collection")]
        public void UserCollectionManagerFindUserValidation()
        {
            // Arranging the test
            // Reset the UserCollectionManager as it has been used in previous tests
            new PrivateType(typeof(UserCollectionManager)).SetStaticField("_Instance", null);

            UserCollectionManager ucm = UserCollectionManager.Instance;

            // Acting out the test
            // Find User 2 as this user is not in the collection as no calls to uc.Add have been made
            User FoundUser = ucm.Find(2);
            ArgumentException InvalidArgument = Assert.ThrowsException<ArgumentException>(() => ucm.Find(2));

            // Asserting the test
            Assert.AreEqual(InvalidArgument.Message, "l_UserID 2 is out of range of the user collection");
        }

        /* Test 7
        *  Test that UserCollectionManager.List() will  return a List of IDs in the UserCollection
        *  Added by Eoin K 11/12/20
        */
        [TestMethod]
        public void UserCollectionListIDs()
        {
            // Arranging the Test
            // Reset the UserCollectionManager as it has been used in previous tests
            new PrivateType(typeof(UserCollectionManager)).SetStaticField("_Instance", null);

            UserCollectionManager ucm = UserCollectionManager.Instance;

            ucm.Add(MockUserValidPhoneNumber);
            ucm.Add(MockUserValidPhoneNumber2);
            ucm.Add(MockUserValidPhoneNumber3);

            // Acting out the test
            List<int> IDs = ucm.ListIDs();

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
