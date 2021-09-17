using System;
using System.Collections.Generic;
using AutoMapper;
using CommandsService.Data;
using CommandsService.Dtos;
using CommandsService.Models;
using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controllers
{
    [Route("api/c/platforms/{platformId}/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommandRepo _repository;
        private readonly IMapper _mapper;

        public CommandsController(ICommandRepo repository, IMapper mapper)
        {   
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDto>> GetCommandsForPlatform(int platformId)
        {
            Console.WriteLine($"---> GetCommandsForPlatform {platformId}");
            if(!_repository.PlatformExists(platformId))
            {   
                return NotFound();
            }
            var commands = _repository.GetCommandsForPlatform(platformId);
            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commands));
        }

         [HttpGet("{commandId}", Name="GetCommandsForPlatform")]
        public ActionResult<IEnumerable<CommandReadDto>> GetCommandsForPlatform(int platformId, int commandId)
        {
            Console.WriteLine($"---> GetCommandsForPlatform {platformId} / {commandId}");
            if(!_repository.PlatformExists(platformId))
            {   
                return NotFound();
            }
            var command = _repository.GetCommand(platformId, commandId);
            if(command == null){
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(command));
        }

        [HttpPost]
        public ActionResult<CommandReadDto> CreateCommandForPlatform(int platformId, CommandCreateDto commandObj)
        {
             Console.WriteLine($"---> CreateCommandForPlatform for {platformId}");
            if(!_repository.PlatformExists(platformId))
            {   
                return NotFound();
            }
            var command=_mapper.Map<Command>(commandObj);
            _repository.CreateCommand(platformId, command);
            _repository.SaveChanges();

            var commandReadDto=_mapper.Map<CommandReadDto>(command);
            return CreatedAtRoute(nameof(GetCommandsForPlatform), new {platformId=platformId, commandId=commandReadDto.Id}, commandReadDto);
        }
    }
}