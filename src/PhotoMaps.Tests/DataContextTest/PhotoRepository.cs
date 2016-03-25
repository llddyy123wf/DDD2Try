using Framework.Infrastructure.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhotoMaps.Domain;
using PhotoMaps.Domain.Repository;
using PhotoMaps.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PhotoMaps.Tests.DataContextTest
{
    [TestClass]
    public class PhotoRepositoryTest
    {
        [TestMethod]
        public void SaveWithTwoRepository()
        {
            IDbContext context = new PhotoContext();
            IDbUnitOfWork unit = new UnitOfWork(context);
            IPhotoRepository res = new PhotoRepository(unit);
            IPhotoCategoryRepository catRes = new PhotoCategoryRepository(unit);
            

            UserInfo autor = res.Users.Where(p => p.UserName == "Jimmy").First();
            PhotoCategory category = res.Categories.Where(p => p.Title == "Normal").First();

            Photo photo = new Photo()
            {
                Access = AccessType.Friend,
                Author = autor,
                Categories = new List<PhotoCategory> { category },
                Coordinate = new PhotoCoordinate() { Latitude = 10.2365412523m, Longitude = 135.1241526352m },
                CreateTime = DateTime.Now,
                Description = "描述1",
                PhotoUrl = "#1",
                Title = "测试图片1"
            };

            PhotoCategory saveCategory = new PhotoCategory()
            {
                Description = "类别",
                Title = "类别"
            };

            res.Save(photo);
            catRes.Save(saveCategory);
            int changes = unit.Commit();
            // photo, photo_category, photoCategory,
            Assert.IsTrue(changes == 6);
        }
    }
}