﻿namespace Cinema.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    public abstract class BaseId
    {
        [Key]
        public int Id { get; set; }
    }
}