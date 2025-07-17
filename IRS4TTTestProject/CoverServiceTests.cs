using IRS4TT.Data;
using IRS4TT.Domains;
using Microsoft.EntityFrameworkCore;

namespace IRS4TT.Tests
{
    public class CoverServiceTests
    {
        private AppDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("TestDb_Covers")
                .Options;

            return new AppDbContext(options);
        }

        [Fact]
        public void Add_AddsCover()
        {
            var db = GetDbContext();
            var service = new CoverService(db);

            var cover = new Cover { TitleEn = "Fire", TitleFa = "آتش", GroupId = 1 };
            service.Add(cover);

            Assert.Single(db.Covers);
        }

        [Fact]
        public void Delete_RemovesCover()
        {
            var db = GetDbContext();
            //Error
            db.Covers.Add(new Cover { Id = 1, TitleEn = "Fire" , GroupId = 1 });
            //OK
            //db.Covers.Add(new Cover { Id = 1, TitleEn = "Fire", TitleFa="آتش" , GroupId = 1 });
            db.SaveChanges();

            var service = new CoverService(db);
            service.Delete(1);

            Assert.Empty(db.Covers);
        }
    }
}
