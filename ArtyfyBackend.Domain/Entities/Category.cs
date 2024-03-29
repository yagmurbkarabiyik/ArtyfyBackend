﻿namespace ArtyfyBackend.Domain.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }

        /// <summary>
        /// Relations
        /// </summary>
        public List<Post> Posts{ get; set; }
    }
}