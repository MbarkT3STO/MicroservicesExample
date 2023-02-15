using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Models;

public class RefreshTokenRequestSucceededModel
{
    public string NewToken { get; set; }
    public string RefreshToken { get; set; }
    public string Username { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime ExpiresOn { get; set; }
}
