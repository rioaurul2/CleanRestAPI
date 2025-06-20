using TutorialDomain.Entities;
using TutorialDomain.Repositories;
using TutorialInfrastructure.Context;

namespace TutorialInfrastructure.Repositories
{
    internal class DishesRepository : IDishesRepository
    {
        private readonly TutorialDbContext _context;

        public DishesRepository(TutorialDbContext context)
        {
            _context = context;
        }

        public async Task<int> Create(Dish entity)
        {
            _context.Dishes.Add(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }
    }
}
