﻿using SkiServiceModels.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace SkiServiceModels
{
    public class Priority : IGenericModel
    {
        [Key]
        public int Id { get; set; }

        [StringLength(20)]
        public string Name { get; set; }

        public int Days { get; set; }

        public bool IsDeleted { get; set; } = false;

    }
}
