using System.Collections.Generic;
using CommandsService.Models;

namespace CommandsService.Data
{
    public interface ICommandRepo
    {
        bool SaveChanges();

        //platforms
        IEnumerable<Platform> GetAllPlatform();
        void CreatePlatform(Platform platform);
        bool PlatformExists(int platformId);

        bool ExternalPlatformExists(int PlatformId);

        //Commands
        IEnumerable<Command> GetCommandsByPlatform(int platformId);
        Command GetCommand(int platformId, int commandId);
        void CreateCommand(int platformId, Command command);
    }
}