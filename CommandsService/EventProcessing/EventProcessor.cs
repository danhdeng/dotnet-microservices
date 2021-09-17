using System;
using System.Text.Json;
using AutoMapper;
using CommandsService.Data;
using CommandsService.Dtos;
using CommandsService.Models;
using Microsoft.Extensions.DependencyInjection;

namespace CommandsService.EventProcessing
{
    public class EventProcessor : IEventProcessor
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IMapper _mapper;

        public EventProcessor(IServiceScopeFactory serviceScopeFactory, IMapper mapper)
        {
            _scopeFactory = serviceScopeFactory;
            _mapper = mapper;
        }
        public void ProcessEvent(string message)
        {
            var eventType = DetermineEventType(message);
            switch (eventType)
            {
                case EventType.PlatformPublished:
                    AddPlatform(message);
                    break;
                default:
                    break;
            }
        }

        private EventType DetermineEventType(string notificationMessage)
        {
            var eventType = JsonSerializer.Deserialize<GenericEventDto>(notificationMessage);
            switch (eventType.Event)
            {
                case "Platform_Published":
                    Console.WriteLine("----> Platform_Published event detected");
                    return EventType.PlatformPublished;
                default:
                    Console.WriteLine("----> Cannot determine the event");
                    return EventType.Undetermined;
            }
        }

        private void AddPlatform(string publishPlatformMessage)
        {
            using (var _scope = _scopeFactory.CreateScope())
            {
                var repo = _scope.ServiceProvider.GetRequiredService<ICommandRepo>();
                var platformPublishDto = JsonSerializer.Deserialize<PlatformPublishDto>(publishPlatformMessage);
                try
                {
                    var platformObj = _mapper.Map<Platform>(platformPublishDto);
                    repo.CreatePlatform(platformObj);
                    repo.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"----> Cannot add platform with error {ex.Message}");
                }
            }
        }
    }
    public enum EventType
    {
        PlatformPublished,
        Undetermined
    }


}