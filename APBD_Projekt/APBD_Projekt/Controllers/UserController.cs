using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using APBD_Projekt.Context;
using APBD_Projekt.Helpers;
using APBD_Projekt.Models;
using APBD_Projekt.Models.DTO_s;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace APBD_Projekt.Controllers;


[AllowAnonymous]
[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly CustomerDbContext _context;

    public UserController(CustomerDbContext context)
    {
        _context = context;
    }
    
    
    
    
    [AllowAnonymous]
    [HttpPost("register")]
    public IActionResult RegisterStudent(RegisterUser model)
    {
        var hashedPasswordAndSalt = SecurityHelpers.GetHashedPasswordAndSalt(model.Password);

        var user = new AppUser()
        {
            Login = model.Login,
            Password = hashedPasswordAndSalt.Item1,
            Salt = hashedPasswordAndSalt.Item2,
            Rola = model.Rola,
            RefreshToken = SecurityHelpers.GenerateRefreshToken(),
            RefreshTokenExp = DateTime.Now.AddDays(1)
        };

        _context.Uzytkownicy.Add(user);
        _context.SaveChanges();

        return Ok();
    }
    
    
    [AllowAnonymous]
    [HttpPost("login")]
    public IActionResult Login(LoginUser loginRequest)
    {
        AppUser user = _context.Uzytkownicy.Where(u => u.Login == loginRequest.Login).First();

        string passwordHashFromDb = user.Password;
        string curHashedPassword = SecurityHelpers.GetHashedPasswordWithSalt(loginRequest.Password, user.Salt);

        if (passwordHashFromDb != curHashedPassword)
        {
            return Unauthorized();
        }


        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Login)
        };
        
        if (user.Rola == "admin")
        {
            claims.Add(new Claim(ClaimTypes.Role, "admin"));
        }
        else
        {
            claims.Add(new Claim(ClaimTypes.Role, "user"));
        }

        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("8hTfGvUWfZXNz7Dk5JH7fF3sDq8fJ9x2"));

        SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken token = new JwtSecurityToken(
            issuer: "https://localhost:5001",
            audience: "https://localhost:5001",
            claims: claims,
            expires: DateTime.Now.AddMinutes(10),
            signingCredentials: creds
        );

        user.RefreshToken = SecurityHelpers.GenerateRefreshToken();
        user.RefreshTokenExp = DateTime.Now.AddDays(1);
        _context.SaveChanges();

        return Ok(new
        {
            accessToken = new JwtSecurityTokenHandler().WriteToken(token),
            refreshToken = user.RefreshToken
        });
    }


}