using MediatR;

namespace TutorialApplication.Services.Commands
{
    public class UpdateRestaurantCommand(int id) : IRequest<bool>
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public bool HasDelivery { get; set; }

    }
}
