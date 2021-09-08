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

        public IEnumerable<PlatForm> GetAllPlatforms()
        {
            return _context.PlatForms.ToList();
        }

        public PlatForm GetPlatformById(int id)
        {
            return _context.PlatForms.FirstOrDefault<PlatForm>(p => p.Id == id);
        }

        public void CreatePlatform(PlatForm platformObj)
        {
            if (platformObj == null)
            {
                throw new System.ArgumentNullException(nameof(platformObj));
            }
            else
            {
                _context.PlatForms.Add(platformObj);
            }
        }
    }
}