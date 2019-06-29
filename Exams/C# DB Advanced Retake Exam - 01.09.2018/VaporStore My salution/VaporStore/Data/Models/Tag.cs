namespace VaporStore.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public  class Tag:ClassWithIdBase
    {
        public Tag()
        {
            GameTags = new HashSet<GameTag>();
        }

        [Required, MinLength(1)]
        public string Name { get; set; }

        public virtual ICollection<GameTag> GameTags { get; set; }
    }
}