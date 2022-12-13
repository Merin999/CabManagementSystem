namespace CabManagementSystem.Models.ViewModels
{
    public class DriverEditViewModel
    {
        [Required]
        [StringLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }


        [Required]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [StringLength(25)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "City")]
        public string City { get; set; }


        [Required]
        [StringLength(50)]
        [Display(Name = "Cab Type")]
        public string CabType { get; set; }

        [Required]
        [StringLength(25)]
        [Display(Name = "Model")]
        public string ModelName { get; set; }

        [Required]
        [Display(Name = "Number of seats")]
        public string NoOfSeats { get; set; }

    }
}
