using System.Text.Json;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
/*1.1. The student needs to implement a login system for an admin user. This will lay the basic fundament for authorization to our application. 
• The login system will consist of a POST call that receives a username and password and checks if the password is correct with what is in the database. 
This endpoint will also register a session on the server. 
• The POST endpoint should return a success message if the password is correct, else it should return reasonable feedback of what went wrong. 
• Create a GET endpoint that returns a Boolean value based on if the session is registered or not. Additionally, return the name of the admin user that is logged in. 
• The login logic and endpoints need to be separated in a Service and a Controller.
*/





[ApiController]
[Route("api/[controller]")]
public class AuthController : Controller
{
    private readonly ILoginService _loginService;

    public AuthController(ILoginService loginService)
    {
        _loginService = loginService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] User user)
    {
        (bool check, User userFound) = _loginService.Login(user);
        if (check)
        {
            HttpContext.Session.SetString("UserMail", user.Email!);
            HttpContext.Session.SetString("Role", userFound.Role);

            return Ok("Login successful.");
        }
        return Unauthorized("Invalid username or password.");
    }

    [HttpPost("Register")]
    public IActionResult Register([FromBody] User user)
    {
        if (HttpContext.Session.GetString("Role") != "Admin") return Unauthorized("your are not an admin.\n you dont have acces to this");


        bool check = _loginService.Register(user);
        if (check) return Ok(user.First_Name + " " + user.Last_Name + " has been registered");
        else if (check == false) return BadRequest("Email and Password are required.");


        return BadRequest("Registration failed");
    }



    [HttpGet("CheckSession")]
    public IActionResult CheckSession()
    {
        var UserMail = HttpContext.Session.GetString("UserMail");
        if (UserMail != null)
        {
            return Ok($"{UserMail} is currently logged in");
        }
        return BadRequest("no one  is logged in");
    }

    [HttpGet("CheckUser")]
    public IActionResult CheckUser()
    {
        var UserMail = HttpContext.Session.GetString("UserMail");
        if (UserMail != null) 
        {
            var jsonString = System.IO.File.ReadAllText("Data/Users.json");
            var users = JsonSerializer.Deserialize<List<User>>(jsonString);

            var user = users.Find(a => a.Email == UserMail);
            return Ok(new Product { Id = 3, Name = "Manu", Price = 199.99m, Description = "A monitor for viewing" });

        }
        return BadRequest("no login");
    }

    [HttpGet("LogOut")]
    public IActionResult LogOut()
    {
        var UserMail = HttpContext.Session.GetString("UserMail");
        if (UserMail != null)
        {
            HttpContext.Session.Remove("UserMail");
            HttpContext.Session.Remove("Role");
            return Ok("Logged out successfully");

        }
        return BadRequest("no one is logged in");

    }
}