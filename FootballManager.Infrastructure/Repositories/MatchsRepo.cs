using FootballManager.Core.InterfacesRepo;
using FootballManager.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManager.Infrastructure.Repositories
{
    public class MatchsRepo : Repository<Core.Entities.Match>
    {
        public MatchsRepo(ApplicationDbContext context) : base(context)
        {
        }
    }
}
