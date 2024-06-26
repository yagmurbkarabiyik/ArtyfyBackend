﻿using Microsoft.AspNetCore.Identity;

namespace ArtyfyBackend.Domain.Entities
{
    public class UserApp : IdentityUser
    {
        public string FullName { get; set; } = null!;
        public string City { get; set; } = null!;
        public string? VerificationCode { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? ImageUrl { get; set; } //user profile image
        public string? ResetPasswordVerificationCode { get; set; }

        /// <summary>
        /// Relations
        /// </summary>
        public List<Post> Posts { get; set; }
        public List<Comment> Comments { get; set; }
		public ICollection<UserLikedPost> UserLikedPosts { get; set; } = new List<UserLikedPost>(); //empty list
	}
}

//todo name/surname, user name will be added