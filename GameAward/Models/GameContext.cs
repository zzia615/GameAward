using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GameAward.Models;
using System.Data.Entity;

namespace GameAward
{
    public class RxjhGameContext : DbContext
    {
        public RxjhGameContext()
            : base("rxjhgame")
        {
            Database.SetInitializer<RxjhGameContext>(null);
        }

        public DbSet<TBL_XWWL_Char> TBL_XWWL_Char { get; set; }
    }

    public class RxjhAccountContext : DbContext
    {
        public RxjhAccountContext()
            : base("rxjhacount")
        {
            Database.SetInitializer<RxjhAccountContext>(null);
        }

        public DbSet<TBL_ACCOUNT> TBL_ACCOUNT { get; set; }
    }
    public class GameCjContext : DbContext
    {
        public GameCjContext()
            : base("txkjCj")
        {
            Database.SetInitializer<GameCjContext>(null);
        }

        public DbSet<GamingUser> GamingUser { get; set; }
        public DbSet<GamingGoods> GamingGoods { get; set; }
        public DbSet<GamingRecord> GamingRecord { get; set; }
    }
}