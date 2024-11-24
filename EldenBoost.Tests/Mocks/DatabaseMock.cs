using EldenBoost.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EldenBoost.Tests.Mocks
{
    public static class DatabaseMock
    {
        public static EldenBoostDbContext Instance
        {
            get
            {
                var dbContextOptions = new DbContextOptionsBuilder<EldenBoostDbContext>()
                    .UseInMemoryDatabase("HouseRentingInMemoryDb" + DateTime.Now.Ticks.ToString())
                    .Options;

                return new EldenBoostDbContext(dbContextOptions);
            }

        }
    }
}
