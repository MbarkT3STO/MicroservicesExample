using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Models;

public class SucceededLoginModel
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }
    public string Username { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime ExpiresOn { get; set; }

}
