using System.ComponentModel.DataAnnotations;

namespace ASP.NET_Core_Web_APIs.Models
{
    public class Car
    {
        public int Id { get; set; }

        [Required]
        public string MakeName { get; set; }

        [Required]
        public string ModelName { get; set; }
    }
}