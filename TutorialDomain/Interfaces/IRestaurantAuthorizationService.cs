using TutorialDomain.Constants;
using TutorialDomain.Entities;

namespace TutorialDomain.Interfaces
{
    public interface IRestaurantAuthorizationService
    {
        bool Authorize(Restaurant restaurant, ResourceOperation resourceOperation);
    }
}