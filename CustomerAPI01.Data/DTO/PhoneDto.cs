using CustomerAPI01.Data;
using System.ComponentModel.DataAnnotations;

namespace CustomerAPI01.Data.DTO
{
    public class PhoneNumberDto
    {
        [Required]
        public string Number { get; set; }

        [Required]
        public string Type { get; set; }
    }
}
