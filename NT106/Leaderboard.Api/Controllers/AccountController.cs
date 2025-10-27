using Microsoft.AspNetCore.Mvc;
using Leaderboard.Api.Models;
using Leaderboard.Api.Services; // [THAY THẾ]

namespace Leaderboard.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AccountRepository _repository; // [THAY THẾ]

        public AccountController(AccountRepository repository) // [THAY THẾ]
        {
            _repository = repository;
        }

        // POST: api/Account/register
        [HttpPost("register")]
        public IActionResult Register([FromBody] LoginRequestModel request)
        {
            // Kiểm tra username đã tồn tại
            if (_repository.GetAccount(request.Username) != null)
            {
                return Conflict("Tên đăng nhập đã tồn tại.");
            }

            var newAccount = new AccountModel
            {
                Username = request.Username,
                Password = request.Password,
                // Các chỉ số khác đã được đặt trong Repository/Model
            };

            if (_repository.RegisterAccount(newAccount))
            {
                return StatusCode(201, "Đăng ký thành công.");
            }
            return StatusCode(500, "Lỗi đăng ký không xác định.");
        }

        // POST: api/Account/login
        [HttpPost("login")]
        public ActionResult<AccountModel> Login([FromBody] LoginRequestModel request)
        {
            var account = _repository.GetAccount(request.Username);

            if (account == null || account.Password != request.Password)
            {
                return Unauthorized("Tên đăng nhập hoặc mật khẩu sai.");
            }

            // Trả về dữ liệu chỉ số game (bỏ mật khẩu)
            account.Password = null!;
            return Ok(account);
        }

        // GET: api/Account/{username}
        [HttpGet("{username}")]
        public ActionResult<AccountModel> GetAccountData(string username)
        {
            var account = _repository.GetAccount(username);

            if (account == null)
            {
                return NotFound("Không tìm thấy tài khoản.");
            }

            account.Password = null!;
            return Ok(account);
        }

        // PUT: api/Account/update
        [HttpPut("update")]
        public IActionResult UpdateAccountData([FromBody] AccountModel updatedData)
        {
            if (_repository.UpdateAccount(updatedData))
            {
                return NoContent();
            }
            return NotFound("Cập nhật thất bại hoặc tài khoản không tồn tại.");
        }
    }
}