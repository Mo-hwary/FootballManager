using FootballManager.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManager.Infrastructure.Repositories
{
    class TeamsRepo : Repository<Core.Entities.Player>
    {
        public TeamsRepo(ApplicationDbContext context) : base(context)
        {

        }

    }
}
