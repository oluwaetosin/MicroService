using System.ComponentModel.DataAnnotations;

namespace CommandService.Dtos
{
    public class CommandCreateDtos
    {
 
        [Required]
        public string HowTo  { get; set; }  
        [Required]
        public string CommandLines {get;set;}
       

  
        
    }
}