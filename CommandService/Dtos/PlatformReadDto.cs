using System.ComponentModel.DataAnnotations;
using CommandService.Models;

namespace CommandService.Dtos
{
    public class PlatformReadDto
    {
       public int Id {get; set;}
       public string Name {get; set;} 
    }
}