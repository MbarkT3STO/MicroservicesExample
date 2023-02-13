using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Database.Entities;

public class RefreshToken
{
    public int Id { get; set; }
    public string Token { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime ExpiresOn { get; set; }
    public bool IsExpired => DateTime.UtcNow >= ExpiresOn;
    public bool IsUsed { get; set; }
    public bool IsRevoked { get; set; }
    public DateTime? RevokedOn { get; set; }
    public bool IsInvalidated => IsExpired || IsRevoked;

    public string AppUserId { get; set; }
    public AppUser AppUser { get; set; }
}
