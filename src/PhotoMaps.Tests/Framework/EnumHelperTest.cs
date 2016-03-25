using Framework.Library;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhotoMaps.Domain;

namespace PhotoMaps.Tests.Framework
{
    [TestClass]
    public class EnumHelperTest
    {
        [TestMethod]
        public void GetDescription()
        {
            UserStatus us = UserStatus.Activated;
            string result = us.GetDescription();
            Assert.AreEqual(result, "激活");
        }

        [TestMethod]
        public void GetFlagDescription()
        {
            AccessType access = AccessType.Friend | AccessType.Private;
            string result = access.GetDescription();
            Assert.AreEqual(result, "朋友,私有");
        }
    }
}