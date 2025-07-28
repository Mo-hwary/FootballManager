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
        return await _context.PlayerStats
            .Include(s => s.Player) 
            .ThenInclude(p => p.Team) 
            .Where(s => s.TeamId == teamId)
            .ToListAsync();
    }

}

