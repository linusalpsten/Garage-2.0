using Garage_2._0.Enums;
using Garage_2._0.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Garage_2._0.ViewModels
{
    public class CheckInVM
    {
        public int Id { get; set; }
        [Required]
        [RegularExpression(@"[a-zA-Z]{3}\d{3}|[a-zA-Z]{1,8}", ErrorMessage = "Registration number should have 3 letters followed by 3 numbers or 1 to 8 letters")]
        [DisplayName("Registration Number")]
        public string RegistrationNumber { get; set; }
        public Color Color { get; set; }
        public Brand Brand { get; set; }
        [Range(1, 20, ErrorMessage = "The number of wheels should be between 1 and 20")]
        [DisplayName("Number of Wheels")]
        public int NumberOfWheels { get; set; }
        public int TypeId { get; set; }
        public int MemberId { get; set; }

        public List<VehicleType> Types { get; set; }
        public List<Member> Members { get; set; }
    }
}