using System.ComponentModel.DataAnnotations;

namespace TutorialApplication.DTO
{
    public class CreateRetaurantDto
    {
        [StringLength(100,MinimumLength = 3)]
        public string Name { get; set; } = default!;
        [Required(ErrorMessage = "Insert a valid category")]
        public string Category { get; set; } = default!;
        public bool HasDelivery { get; set; }
        public string Street { get; set; } = default!;
        public string City { get; set; } = default!;
        [RegularExpression(@"^\d{2}-\d{3}$", ErrorMessage = "Provide a valid postal code")]
        public string PostalCode { get; set; } = default!;
        [EmailAddress(ErrorMessage = "Please provide a valid email addres")]
        public string? ContactEmail { get; set; }
        [Phone(ErrorMessage = "Please provide a valid phone number")]
        public string? ContactNumber { get; set; }
    }
}
