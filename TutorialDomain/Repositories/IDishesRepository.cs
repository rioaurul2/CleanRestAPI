using TutorialDomain.Entities;

namespace TutorialDomain.Repositories
{
    public interface IDishesRepository
    {
        Task<int> Create(Dish entity);
    }
}
