using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Models;

public class RefreshTokenRequestModel
{
    public string Username { get; set; }
    public string RefreshToken { get; set; }
}
