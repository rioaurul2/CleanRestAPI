﻿using Microsoft.AspNetCore.Identity;

namespace TutorialDomain.Entities
{
    public class User : IdentityUser
    {
        public DateTime? DateOfBirth { get; set; }
        public string? Nationality { get; set; }

        public List<Restaurant> OwnedRestaurants { get; set; } = [];
    }
}
