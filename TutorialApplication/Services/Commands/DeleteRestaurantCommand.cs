using MediatR;

namespace TutorialApplication.Services.Commands
{
    public class DeleteRestaurantCommand(int id) : IRequest<bool>
    {
        public int Id { get; } = id;
    }
}
