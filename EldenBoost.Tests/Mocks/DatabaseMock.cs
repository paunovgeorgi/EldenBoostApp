using EldenBoost.Data;
using Microsoft.EntityFrameworkCore;

namespace EldenBoost.Tests.Mocks
{
    public static class DatabaseMock
    {
        public static EldenBoostDbContext Instance
        {
            get
            {
                var dbContextOptions = new DbContextOptionsBuilder<EldenBoostDbContext>()
                    .UseInMemoryDatabase("EldenBoostInMemoryDb" + DateTime.Now.Ticks.ToString())
                    .Options;

                return new EldenBoostDbContext(dbContextOptions);
            }

        }
    }
}
