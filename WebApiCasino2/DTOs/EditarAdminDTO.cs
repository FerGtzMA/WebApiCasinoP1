﻿using System.ComponentModel.DataAnnotations;

namespace WebApiCasino2.DTOs
{
    public class EditarAdminDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
