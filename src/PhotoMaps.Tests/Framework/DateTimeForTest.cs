using Framework.Library;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace PhotoMaps.Tests.Framework
{
    [TestClass]
    public class DateTimeForTest
    {
        [TestMethod]
        public void Display()
        {
            DateTime dt1 = new DateTime(2015, 10, 4);
            DateTime dt2 = DateTime.Now;

            DateTimeFor dtFor = new DateTimeFor();
            string result = dtFor.From(dt1).To(dt2).Display();
            Assert.AreEqual(result, "4月前");
        }
    }
}