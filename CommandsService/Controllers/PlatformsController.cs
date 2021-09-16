using System;
using System.Collections.Generic;
using AutoMapper;
using CommandsService.Data;
using CommandsService.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace CommandsServices.Controllers
{
    [Route("api/c/{controller}")]
    [ApiController]
    public class PlatformsController: ControllerBase{
        private readonly ICommandRepo _repository;

        public IMapper _mapper { get; }

        public PlatformsController(ICommandRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetAllPlatform()
        {
            Console.WriteLine("---> Getting Platform for Command Service");
            var platformItems=_repository.GetAllPlatform();
            return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platformItems));

        }
        [HttpPost]
        public ActionResult TestInBoundConnection()
        {
            Console.WriteLine("---> Inbound Post # Command Service");
            return Ok("Inbound test fo from platforms controller");
        }
    }

}