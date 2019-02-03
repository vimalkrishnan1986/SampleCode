using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Education.Domains.School.Entities;

namespace Education.Services.Api.Models
{
    public class RegistrationModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Name is Mandatory")]
        [MaxLength(100)]
        public string Name { get; set; }
        public string Address { get; set; }
        public byte[] Image { get; set; }

        public RegistrationModel()
        {

        }
        public RegistrationRequest ToDomain()
        {
            return new RegistrationRequest
            {
                Name = Name,
                Address = Address,
                Image = Image
            };
        }

        public RegistrationModel(RegistrationRequest request)
        {
            Name = request.Name;
            Address = request.Address;
            Image = request.Image;
        }

    }
}
