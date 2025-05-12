using MediatR;

namespace TutorialApplication.Services.Commands
{
    public class CreateRestaurantCommand : IRequest<int>
    {
        public string Name { get; set; } = default!;
        public string Category { get; set; } = default!;
        public bool HasDelivery { get; set; }
        public string Street { get; set; } = default!;
        public string City { get; set; } = default!;
        public string PostalCode { get; set; } = default!;
        public string? ContactEmail { get; set; }
        public string? ContactNumber { get; set; }
    }
}
