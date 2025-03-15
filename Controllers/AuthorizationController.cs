using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebNotes.Data;
using WebNotes.Models;
using WebNotes.Models.User;

namespace WebNotes.Controllers;

public class AuthorizationController : Controller
{
    private readonly NotesDbContext _db;
    public AuthorizationController(NotesDbContext db)  
    {  
        _db = db;  
    }

    [HttpGet]
    public IActionResult Main(BaseResponse<VMUserRegister>? baseResponse)
    {
        var obj = new BaseResponse<VMUserRegister>
        {
            Error = new Message()
        };

        if (baseResponse != null && baseResponse.Error != null)
            obj.Error = baseResponse.Error;
        
        return View(obj);
    }

    [HttpGet]
    public IActionResult Login()
    {
        var obj = new BaseResponse<VMUserLogin>
        {
            Error = new Message()
        };

        return View(obj);
    }
        
    [HttpPost]
    public async Task<IActionResult> Login(VMUserLogin user)
    {
        var result = new BaseResponse<VMUserLogin>();
        var mes = new Message();
            
        if (ModelState.IsValid)
        {
            var obj = await _db.Users
                .FirstOrDefaultAsync(x => x.Login == user.Login && 
                                          x.Password == user.Password);

            if (obj != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, obj.Login)
                };
                var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                return RedirectToAction("Grid", "Collection");
            }
        }

        mes.Description = "Неверный логин или пароль";
            
        result = new BaseResponse<VMUserLogin>
        {
            User = user,
            Error = mes
        };

        return View(result);
    }

    [HttpGet]
    public IActionResult Register()
    {
        var obj = new BaseResponse<VMUserRegister>()
        {
            Error = new Message()
        };

        return View(obj);
    }

    [HttpPost]
    public async Task<IActionResult> Register(VMUserRegister user, CancellationToken cancellationToken = default)
    {
        var result = new BaseResponse<VMUserRegister>();
        var response = new User()
        {
            DateOfCreate = DateTime.Now,
            Login = user.Login,
            Password = user.Password
        };

        if (ModelState.IsValid)
        {
            var obj = await _db.Users
                .FirstOrDefaultAsync(x => x.Login == response.Login, cancellationToken);

            if (obj != null)
            {
                result = new BaseResponse<VMUserRegister>()
                {
                    User = user,
                    Error = new Message()
                    {
                        Description = "Пользователь уже существует"
                    }
                };
                    
                return View(result);
            }
                
            response.DateOfCreate = DateTime.Now;
                
            var entity = _db.Users.Add(response).Entity;
            await _db.SaveChangesAsync(cancellationToken);
                
            result = new BaseResponse<VMUserRegister>
            {
                User = new VMUserRegister(),
                Error = new Message
                {
                    Description = "Пользователь успешно создан"
                }
            };

            return View("Main", result);
        }
            
        result = new BaseResponse<VMUserRegister>()
        {
            User = user,
            Error = new Message()
            {
                Description = "Произошла ошибка"
            }
        };
            
        return View(result);
    }
}