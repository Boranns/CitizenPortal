using CitizenPortal.Models;
using Microsoft.EntityFrameworkCore;

namespace CitizenPortal.Repositories;

public class ApplicationRepository : IApplicationRepository
{
    private readonly AppDbContext _context;

    public ApplicationRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Application?> GetByIdAsync(int id)
    {
        return await _context.Applications
            .Include(a => a.User)
            .Include(a => a.Documents)
            .Include(a => a.Comments)
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<IEnumerable<Application>> GetAllAsync()
    {
        return await _context.Applications
            .Include(a => a.User)
            .ToListAsync();
    }

    public async Task<IEnumerable<Application>> GetByUserIdAsync(int userId)
    {
        return await _context.Applications
            .Where(a => a.UserId == userId)
            .Include(a => a.User)
            .ToListAsync();
    }

    public async Task<IEnumerable<Application>> GetByStatusAsync(string status)
    {
        return await _context.Applications
            .Where(a => a.Status == status)
            .Include(a => a.User)
            .ToListAsync();
    }

    public async Task<Application> CreateAsync(Application application)
    {
        _context.Applications.Add(application);
        await _context.SaveChangesAsync();
        return application;
    }

    public async Task<Application> UpdateAsync(Application application)
    {
        application.UpdatedAt = DateTime.UtcNow;
        _context.Applications.Update(application);
        await _context.SaveChangesAsync();
        return application;
    }

    public async Task DeleteAsync(int id)
    {
        var application = await _context.Applications.FindAsync(id);
        if (application != null)
        {
            _context.Applications.Remove(application);
            await _context.SaveChangesAsync();
        }
    }
}
