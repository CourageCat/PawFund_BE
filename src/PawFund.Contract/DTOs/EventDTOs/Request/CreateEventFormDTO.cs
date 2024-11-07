using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawFund.Contract.DTOs.EventDTOs.Request
{
    public class CreateEventFormDTO
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        public int MaxAttendees { get; set; }
        public IFormFile ThumbHeroUrl { get; set; }
        public IFormFile ImagesUrl { get; set; }
    }
}
