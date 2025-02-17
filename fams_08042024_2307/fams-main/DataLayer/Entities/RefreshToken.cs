﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    public class RefreshToken
    {
        [Key]
        public Guid UserId { get; set; }

        [Required]
        [MaxLength(100)]
        public string? TokenId { get; set; }

        [Required]
        public string? RefreshTokenString { get; set; }

        [Required]
        public DateTime ExpireAt { get; set; }

        [Required]
        public ReStatuses Statuses { get; set; }
    }
    public enum ReStatuses
    {
        Enable,
        Disable
    }
}
