using FootballManager.Core.Entities;
using FootballManager.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManager.Infrastructure.Repositories
{
    public class PlayerRepo : Repository<Core.Entities.Player>
    {
        public PlayerRepo(ApplicationDbContext context) : base(context)
        {
        }
    } 
}
