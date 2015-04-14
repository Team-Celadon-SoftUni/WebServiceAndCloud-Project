﻿namespace SoftUniFAQSystem.Web.Models.Answers
{
    using System.ComponentModel.DataAnnotations;

    public class AnswerBindingModels
    {
        [Required]
        [MaxLength(200, ErrorMessage = "The answer is bigger than 200 symbols.")]
        [MinLength(1, ErrorMessage = "The answer is smaller than 1 symbol")]
        public string Text { get; set; }

        public string UserId { get; set; }
    }
}