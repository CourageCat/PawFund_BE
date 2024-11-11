using PawFund.Contract.Enumarations.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawFund.Contract.DTOs.Account
{
    public static class GetUserByYearAndMonthDTO
    {
        public class CustomerDTO
        {
            public string Email { get; set; } = string.Empty;
            public RoleType RoleId { get; set; }
            public DateTime CreatedDate { get; set; }
        }

        public class DayDTO
        {
            public double DayOfWeek { get; set; }
            public int CustomersCount { get; set; }
            public List<CustomerDTO> Customers { get; set; }
        }

        public class WeekDTO
        {
            public double Week { get; set; }
            public int CustomersCount { get; set; }
            public List<DayDTO> Days { get; set; } = new List<DayDTO>();
        }
    }
}
