namespace CabManagementSystem.Models.ViewModels
{
    public class RegisterViewModelDriver
    {
        
        [Required]
        [StringLength(15)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(15)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [StringLength(50)]
        public string City { get; set; }

        //public DateTime Dob { get; set; }

        [Required]
        [Display(Name = "Licence Number")]
        public string LicenceNumber { get; set; }

        [Required]
        [Display(Name = "Licence Validity")]
        public DateTime LicenceValidity { get; set; }

        [Required]
        [StringLength(15)]
        [Display(Name = "Cab Type")]
        public string CabType { get; set; }


        [Required]
        [StringLength(50)]
        [Display(Name = "Model Name")]
        public string ModelName { get; set; }

        [Required]
        [Display(Name = "Number of seats")]
        public int NoOfSeats { get; set; }

        [Required]
        [StringLength(25)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [StringLength(25)]
        [Compare(nameof(Password))]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

    }
}



