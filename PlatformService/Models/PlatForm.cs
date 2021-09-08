using System.ComponentModel.DataAnnotations;
namespace PlatformService.Models.Models{
    Public class PlatForm{
        [key]
        [request]
        public int Id { get; set; }
        
        [request]
        public string Name { get; set;}
        
        [request]
        public string Cost { get; set; }
        
        [request]
        public string Publisher { get; set; }
    }
}