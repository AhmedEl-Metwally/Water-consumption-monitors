﻿using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;

namespace Water_consumption_monitors.Models
{
    [Owned]
    public class RefreshToken
    {
        public string Token { get; set; }
        public DateTime Expireson { get; set; }
        public bool IsExpired => DateTime.UtcNow >= Expireson;
        public DateTime CreatedOn { get; set; }
        public DateTime? RevokedOn { get; set; }
        public bool IsActive => RevokedOn == null && !IsExpired;
    }
}
