using MediatR;

namespace TutorialApplication.Services.Commands
{
    public class UpdateRestaurantCommand(int id) : IRequest
    {
        public int Id { get; set; } = id;
        public string Name { get; set; } = default!;
        public bool HasDelivery { get; set; }

    }
}
