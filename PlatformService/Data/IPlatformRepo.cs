using PlatformService.Models;
using System.Collections.Generic;

namespace PlatformService.Data
{
    public interface IPlatformRepo
    {
        bool SaveChanges();

        IEnumerable<PlatForm> GetAllPlatforms();

        PlatForm GetPlatformById(int id);

        void CreatePlatform(PlatForm platformObj);
    }
}