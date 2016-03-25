using Framework.Infrastructure.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhotoMaps.Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhotoMaps.Tests.DapperTest
{
    [TestClass]
    public class UserInfoRepositoryTest
    {
        [TestMethod]
        public void UnitOfWorkTest()
        {
            IUnitOfWork unit = new DbUnitOfWork("", true);
            UserInfoRepository rep = new UserInfoRepository(unit);
            rep.Save(null);
            rep.Save(nul);
            unit.Commit();

        }
    }
}
