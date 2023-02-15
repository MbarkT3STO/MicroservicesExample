using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AuthService.Database;
using AuthService.Database.Entities;
using AuthService.Models;
using AuthService.Options;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AuthService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    private readonly JwtOptions _jwtOptions;
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;

    public AuthController(AppDbContext context, IMapper mapper, IOptions<JwtOptions> jwtOptions, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
    {
        _context = context;
        _mapper = mapper;
        _jwtOptions = jwtOptions.Value;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpGet("GetUsers")]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _context.AppUsers.ToListAsync();
        return Ok(users);
    }

    [HttpGet("GetUser/{id}")]
    public async Task<IActionResult> GetUser(string id)
    {
        var user = await _context.AppUsers.FindAsync(id);
        return Ok(user);
    }

    [HttpGet("GetRefreshTokens")]
    public async Task<IActionResult> GetRefreshTokens()
    {
        var refreshTokens = await _context.RefreshTokens.ToListAsync();
        var refreshTokensGetModel = _mapper.Map<List<RefreshTokenGetModel>>(refreshTokens);
        return Ok(refreshTokensGetModel);
    }

    [HttpGet("GetRefreshToken/{id}")]
    public async Task<IActionResult> GetRefreshToken(int id)
    {
        var refreshToken = await _context.RefreshTokens.FindAsync(id);
        var refreshTokenGetModel = _mapper.Map<RefreshTokenGetModel>(refreshToken);
        return Ok(refreshTokenGetModel);
    }


    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginModel model)
    {

        var signInResult = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);

        if (!signInResult.Succeeded) return BadRequest("Invalid username or password");

        var user = await _userManager.FindByNameAsync(model.Username);
        var jwt = GenerateJwtToken(model.Username);
        var refreshToken = await GenerateRefreshToken(user.Id);
        var createdJwtModel = new SucceededLoginModel
        {
            Token = jwt,
            RefreshToken = refreshToken.Token,
            Username = model.Username,
            CreatedOn = DateTime.Now,
            ExpiresOn = DateTime.Now.AddMinutes(5)
        };

        return Ok(createdJwtModel);
    }


    [HttpPost("CreateRefreshToken")]
    public async Task<IActionResult> CreateRefreshToken(RefreshTokenCreateModel model)
    {
        var user = await _userManager.FindByNameAsync(model.Username);

        if (user == null) return BadRequest("Invalid username");

        var refreshToken = GenerateRefreshToken(user.Id);

        var refreshTokenGetModel = _mapper.Map<RefreshTokenGetModel>(refreshToken);

        return Ok(refreshTokenGetModel);
    }


    [HttpPost("RefreshToken")]
    public async Task<IActionResult> RefreshToken(RefreshTokenRequestModel model)
    {
        var refreshToken = await _context.RefreshTokens.FirstOrDefaultAsync(x => x.Token == model.RefreshToken);

        if (refreshToken == null) return BadRequest("Invalid refresh token");

        if (refreshToken.IsExpired) return BadRequest("Refresh token expired");

        if (refreshToken.IsRevoked) return BadRequest("Refresh token revoked");

        if (refreshToken.IsInvalidated) return BadRequest("Refresh token invalidated");

        var user = await _userManager.FindByNameAsync(model.Username);

        var jwt = GenerateJwtToken(model.Username);

        var result = new RefreshTokenRequestSucceededModel
        {
            NewToken = jwt,
            RefreshToken = refreshToken.Token,
            Username = model.Username,
            CreatedOn = DateTime.Now,
            ExpiresOn = DateTime.Now.AddMinutes(5)
        };

        return Ok(result);
    }


    private string GenerateJwtToken(string username)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("Role", "Admin")
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(

            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(5),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private async Task<RefreshToken> GenerateRefreshToken(string userId)
    {
        var token = Guid.NewGuid().ToString();

        var refreshToken = new RefreshToken
        {
            Token = token,
            CreatedOn = DateTime.Now,
            ExpiresOn = DateTime.Now.AddHours(1),
            AppUserId = userId
        };

        await _context.RefreshTokens.AddAsync(refreshToken);
        await _context.SaveChangesAsync();
        await _context.Entry(refreshToken).ReloadAsync();

        return refreshToken;
    }
}
