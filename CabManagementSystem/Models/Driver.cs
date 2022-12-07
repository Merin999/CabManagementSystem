using System.ComponentModel.DataAnnotations.Schema;

namespace CabManagementSystem.Models
{
    public class Driver
    {
        public string City { get; set; }
        //public DateTime Dob { get; set; }
        public string LicenceNumber { get; set; }
        public DateTime LicenceValidity { get; set; }
        public string CabType { get; set; }
        public string ModelName { get; set; }
        public int NoOfSeats { get; set; }

        public ApplicationUser ApplicationUsers { get; set; }
        [ForeignKey(nameof(ApplicationUser))]
        public string DriverId { get; set; }

    }
}
