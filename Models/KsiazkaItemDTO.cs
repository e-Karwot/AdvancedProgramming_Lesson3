﻿using System.ComponentModel.DataAnnotations;
namespace AdvancedProgramming_Lesson3.Models
{
    public class KsiazkaItemDTO
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "To pole nie może być puste!")]
        public string Bookname { get; set; }
        [Required(ErrorMessage = "To pole nie może być puste!")]
        public string Author { get; set; }
    }
}
