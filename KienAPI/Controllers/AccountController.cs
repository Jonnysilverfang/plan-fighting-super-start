using Microsoft.AspNetCore.Mvc;
using KienAPI.Models;
using KienAPI.Services;

namespace KienAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly DatabaseService _db;
        public AccountController(DatabaseService db) => _db = db;

        // POST: /api/Account/register
        [HttpPost("register")]
        public IActionResult Register(AccountData acc)
        {
            bool ok = _db.RegisterAccount(acc);
            return ok ? Ok("Đăng ký thành công") : BadRequest("Tài khoản đã tồn tại");
        }

        // POST: /api/Account/login
        [HttpPost("login")]
        public IActionResult Login(AccountData acc)
        {
            bool ok = _db.CheckLogin(acc.Username, acc.Password);
            if (!ok) return Unauthorized("Sai thông tin đăng nhập");
            return Ok(_db.GetAccount(acc.Username));
        }

        // PUT: /api/Account/update
        [HttpPut("update")]
        public IActionResult Update(AccountData acc)
        {
            bool ok = _db.UpdateAccount(acc);
            return ok ? Ok("Cập nhật thành công") : BadRequest("Cập nhật thất bại");
        }

        // GET: /api/Account/{username}
        [HttpGet("{username}")]
        public IActionResult Get(string username)
        {
            var data = _db.GetAccount(username);
            return data == null ? NotFound() : Ok(data);
        }
    }
}
