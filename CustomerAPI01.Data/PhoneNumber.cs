using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerAPI01.Data
{
    public class PhoneNumber
    {
        [Key]
        public int PhoneNumberId { get; set; }

        [Required]
        public string Number { get; set; }

        [Required]
        public string Type { get; set; }

        public Guid CustomerId { get; set; }

        public Customer Customer { get; set; }
    }
}
