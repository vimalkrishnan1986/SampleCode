using System;
using System.ComponentModel.DataAnnotations;

namespace Education.Domains.School.Entities
{
    public class RegistrationRequest
    {
        [Required]
        public string Name { get; set; }
        public string Address { get; set; }
        public byte[] Image { get; set; }
        public long Id { get; set; }
    }
}
