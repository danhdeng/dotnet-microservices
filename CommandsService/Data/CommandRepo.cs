using System;
using System.Collections.Generic;
using System.Linq;
using CommandsService.Models;

namespace CommandsService.Data
{
    public class CommandRepo : ICommandRepo
    {
        private readonly AppDbContext _context;
        public CommandRepo(AppDbContext context)
        {
            _context = context;
            Console.WriteLine($"the db name is {_context.ContextId}");
        }
        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }

        //platforms
        public IEnumerable<Platform> GetAllPlatform()
        {
            return _context.Platforms.ToList();
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

        public bool PlatformExists(int platformId)
        {
            return _context.Platforms.Any(p => p.Id == platformId);
        }

        public bool ExternalPlatformExists(int externalPlatformId)
        {
            return _context.Platforms.Any<Platform>(p => p.ExternalID == externalPlatformId);
        }

        //Commands
        public IEnumerable<Command> GetCommandsForPlatform(int platformId)
        {
            return _context.Commands.Where(c => c.PlatformId == platformId).OrderBy(c => c.Platform.Name);
        }
        public Command GetCommand(int platformId, int commandId)
        {
            return _context.Commands.Where(c => c.PlatformId == platformId && c.Id == commandId).FirstOrDefault();
        }
        public void CreateCommand(int platformId, Command commandObj)
        {
            if (commandObj == null)
            {
                throw new System.ArgumentNullException(nameof(commandObj));
            }
            else
            {
                commandObj.PlatformId = platformId;
                _context.Commands.Add(commandObj);
            }
        }
    }
}