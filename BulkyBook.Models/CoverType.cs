﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BulkyBook.Models
{
    //Είναι ο πίνακας CoverType μέσα στην db
    public class CoverType
    {
        [Key]
        public int Id { get; set; }
        [Display(Name= "Cover Type")]
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}