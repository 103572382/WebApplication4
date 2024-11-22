using Microsoft.AspNetCore.Mvc;
using WebApplication4.Dtos;
using WebApplication4.Services;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    // Lấy thông tin người dùng
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(Guid id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        if (user == null)
            return NotFound();

        return Ok(user);
    }

    // Tạo người dùng mới
    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserDto dto)
    {
        // Tính toán Age từ DateOfBirth trước khi tạo người dùng
        var age = CalculateAge(dto.DateOfBirth);

        var user = new CreateUserDto
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            DateOfBirth = dto.DateOfBirth,
            Address = dto.Address,
            Ethnicity = dto.Ethnicity,
            Age = age // Gán Age đã tính được
        };

        var createdUser = await _userService.CreateUserAsync(user);

        return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, createdUser);
    }

    // Cập nhật thông tin người dùng
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UpdateUserDto dto)
    {
        // Nếu người dùng gửi DateOfBirth mới, tính lại Age
        if (dto.DateOfBirth.HasValue)
        {
            var age = CalculateAge(dto.DateOfBirth.Value);
            dto.Age = age;  // Gán lại Age đã tính
        }

        await _userService.UpdateUserAsync(id, dto);
        return NoContent();
    }

    // Hàm tính tuổi từ DateOfBirth
    private int CalculateAge(DateTime dateOfBirth)
    {
        var today = DateTime.Today;
        int age = today.Year - dateOfBirth.Year;
        if (dateOfBirth.Date > today.AddYears(-age)) age--;  // Điều chỉnh nếu chưa qua sinh nhật trong năm nay
        return age;
    }
}
