using FootballManager.Core.InterfacesRepo;
using FootballManager.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManager.Infrastructure.Repositories;

public class StatsRepo: Repository<Core.Entities.Statistics>, IStatsRepo
{
    public StatsRepo(ApplicationDbContext context) : base(context)
    {
    }
    public async Task<IEnumerable<Core.Entities.Statistics>> GetAllStatAsync(int teamId)
    {
        return await _context.PlayerStats.Where(t=>t.TeamId == teamId).ToListAsync();
    }
}

