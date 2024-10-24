using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawFund.Contract.DTOs.Adopt.Response
{
    public static class GetApplicationByIdResponseDTO
    {
        public class AccountDto
        {
            public Guid Id { get; set; }
            public string FirstName { get; set; } = string.Empty;
            public string LastName { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public string PhoneNumber { get; set; } = string.Empty;
        }

        public class CatDto
        {
            public Guid Id { get; set; }
            public string Sex { get; set; } = string.Empty;
            public string Name { get; set; } = string.Empty;
            public string Age { get; set; } = string.Empty;
            public string Breed { get; set; } = string.Empty;
            public decimal Weight { get; set; } = 0;
            public string Color { get; set; } = string.Empty;
            public string Description { get; set; } = string.Empty;
            public bool Sterilization { get; set; }
            public string ImageUrl { get; set; }
            public string PublicImageId { get; set; }
        }
    }
}
