
using System.ComponentModel.DataAnnotations.Schema;

namespace CabServiceManagement.Models
{
        //public enum CarModel
        //{
        //    Auto,
        //    Sedan,
        //    CUV,
        //    SUV
        //}
        public enum Location
        {
            Kakkanad,
            Edappally,
            Palarivattom,
            Kalamassery,
            Infopark,
            [Display(Name = "North Railway Station")]
            NorthRailwayStation,
            Kaloor
        }

        public class Booking
        {
            public int Id { get; set; }


            [Required]
            [Display(Name = "Pick Up")]
            public Location From { get; set; }


            [Required]
            [Display(Name = "Destination")]
            public Location To { get; set; }


            [Required]
            public DateTime Date { get; set; } = DateTime.Now;

            //public CarModel CarModel { get; set; }

            public bool DriverConfirmed { get; set; } = false;

            public ApplicationUser ApplicationUsers { get; set; }
             [ForeignKey(nameof(ApplicationUser))]
            public string UserId { get; set; }


            public Driver Drivers { get; set; }
            [ForeignKey(nameof(Driver))]
            public string? DriverId { get; set; }


    }

}

