using System.Collections.Generic;
using System.Linq;
using PlatformService.Models;

namespace PlatformService.Data
{
    public class PlatformRepos : IPlatformRepo
    {
        private readonly AppDbContext _context;

        public PlatformRepos(AppDbContext context)
        {
            _context = context;
        }
        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }

        public IEnumerable<Platform> GetAllPlatforms()
        {
            return _context.Platforms.ToList();
        }

        public Platform GetPlatformById(int id)
        {
            return _context.Platforms.FirstOrDefault<Platform>(p => p.Id == id);
        }

        public void CreatePlatform(Platform platformObj)
        {
            if (platformObj == null)
            {
                throw new System.ArgumentNullException(nameof(platformObj));
            }
            else
            {
                _context.Platforms.Add(platformObj);
            }
        }
    }
}