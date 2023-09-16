using System.ComponentModel.DataAnnotations;
using CommandService.Models;

namespace CommandService.Dtos
{
    public class CommandReadDto
    {
    
      
        public int Id { get; set; }

      
        public int ExternalID {get; set; }

      
        public string Name { get; set; }

        public ICollection<Command> Commands { get; set; } = new List<Command>();
    }
}