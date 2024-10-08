using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawFund.Domain.Exceptions
{
    public static class AdoptApplicationException
    {
        public class AdoptApplicationNotFoundException : NotFoundException
        {
            public AdoptApplicationNotFoundException(Guid Id) : base($"Can not found application with ID: {Id}")
            {
            }
        }
        public class AdoptApplicationNotBelongToAdopterException : BadRequestException
        {
            public AdoptApplicationNotBelongToAdopterException() : base($"This adopt application does not belong to this adopter!")
            {
            }
        }
        public class AdopterHasAlreadyRegisteredWithCatException : BadRequestException
        {
            public AdopterHasAlreadyRegisteredWithCatException() : base($"This adopter has already registered adopt application with this cat!")
            {
            }
        }
        public class AdoptApplicationEmptyException : NotFoundException
        {
            public AdoptApplicationEmptyException() : base("Can not found any adopt applications!")
            {

            }
        }
    }
}
