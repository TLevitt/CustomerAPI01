using System.ComponentModel.DataAnnotations;

namespace CustomerAPI01.Data.DTO
{
    public class CustomerDto
    {
        [Required]
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }

        public List<PhoneNumberDto> PhoneNumbers { get; set; }
    }
}
