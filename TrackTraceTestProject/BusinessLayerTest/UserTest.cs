/* UserTest.cs
 * UserTest.cs is a unit test for BusinessLayer/User.cs
 * 
 * Written By Eoin K 06/12/20
 */
using System;
using TrackTraceProject.BusinessLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace TrackTraceTestProject.BusinessLayerTest
{
    /* The tests to run for User
    * Should assert that User has 2 properties, UserID and PhoneNumber
    */
    [TestClass]
    public class UserTest
    {
        // Mock Data to be used in the tests.
        private int MockUserID = 0;
        private string MockValidPhoneNumber = "+447123 554244";
        private string MockInvalidPhoneNumber = "071235542";
        private string MockInvalidPhoneNumberExceptionMessage = "l_PhoneNumber 071235542 is not a valid phone number";

        /* Test 1
        *  Test that User successfully builds
        *  Added by Eoin K 06/12/20
        */
        [TestMethod]
        public void UserBuildsSuccessfully()
        {
            User u = new User(MockUserID, MockValidPhoneNumber);

            Assert.IsInstanceOfType(u, typeof(User));
        }

        /* Test 2
        *  Test that User has two properties, UserID and PhoneNumber
        *  Added by Eoin K 06/12/20
        */
        [TestMethod]
        public void UserHasTwoProperties()
        {
            User u = new User(MockUserID, MockValidPhoneNumber);

            Assert.IsTrue(u.GetType().GetProperty("UserID") != null);
            Assert.IsTrue(u.GetType().GetProperty("PhoneNumber") != null);
        }

        /* Test 3
        *  Test that User's two properties, UserID and PhoneNumber are the correct type: int & string respectively
        *  Added by Eoin K 06/12/20
        */
        [TestMethod]
        public void UserPropertyTypes()
        {
            User u = new User(MockUserID, MockValidPhoneNumber);

            Assert.IsTrue(u.UserID.GetTypeCode() == TypeCode.Int32);
            Assert.IsTrue(u.PhoneNumber.GetTypeCode() == TypeCode.String);
        }

        /* Test 4
        *  Test that User's two properties are assigned their correct values from the constructor
        *  Added by Eoin K 06/12/20
        */
        [TestMethod]
        public void UserConstructorAssignment()
        {
            User u = new User(MockUserID, MockValidPhoneNumber);

            Assert.AreEqual(u.UserID, MockUserID);
            Assert.AreEqual(u.PhoneNumber, MockValidPhoneNumber);
        }

        /* Test 5
        *  Test that User only accepts valid Phone Numbers in the constructor
        *  Added by Eoin K 06/12/20
        */
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "l_PhoneNumber 071235542 is not a valid phone number")]
        public void UserPhoneNumberValidation()
        {
            User u = new User(MockUserID, MockInvalidPhoneNumber);

            ArgumentException InvalidArgument = Assert.ThrowsException<ArgumentException>(() => new User(MockUserID, MockInvalidPhoneNumber));

            Assert.AreEqual(InvalidArgument.Message, MockInvalidPhoneNumberExceptionMessage);
        }
    }
}
