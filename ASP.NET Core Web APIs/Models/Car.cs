using System.ComponentModel.DataAnnotations;

namespace ASP.NET_Core_Web_APIs.Models
{
    public class Car
    {
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        public string MakeName { get; set; }

        [Required]
        [MinLength(2)]
        public string ModelName { get; set; }
    }
}