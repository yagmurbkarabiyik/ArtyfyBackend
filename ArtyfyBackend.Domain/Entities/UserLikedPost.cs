﻿namespace ArtyfyBackend.Domain.Entities
{
    public class UserLikedPost : BaseEntity
    {
        public string UserAppId { get; set; } = null!;
        public UserApp UserApp { get; set; } = null!;
		public int PostId { get; set; } 
		public Post Post { get; set; } = null!;
	}
}