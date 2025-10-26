using Leaderboard.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Leaderboard.Api.Controllers;

/// <summary>API Bảng xếp hạng: lấy / tạo / xóa mục xếp hạng.</summary>
[ApiController]
[Route("api/bảng-xếp-hạng")] // URL có dấu OK
public class BangXepHangController : ControllerBase
{
    private static readonly List<MucXepHang> _duLieu =
    [
        new(1, "Alice", 1200, DateTime.UtcNow),
        new(2, "Bob",     950, DateTime.UtcNow),
    ];

    /// <summary>Lấy danh sách theo điểm giảm dần.</summary>
    [HttpGet]
    public IEnumerable<MucXepHang> LayDanhSach()
        => _duLieu.OrderByDescending(x => x.DiemSo);

    /// <summary>Lấy 1 mục theo Id.</summary>
    [HttpGet("{id:int}")]
    public ActionResult<MucXepHang> LayTheoId(int id)
        => _duLieu.FirstOrDefault(x => x.Id == id) is { } f ? f : NotFound();

    /// <summary>Tạo mới 1 mục xếp hạng.</summary>
    [HttpPost]
    public ActionResult<MucXepHang> TaoMoi(MucXepHang yeuCau)
    {
        var idMoi = _duLieu.Count == 0 ? 1 : _duLieu.Max(x => x.Id) + 1;
        var muc = yeuCau with { Id = idMoi, CapNhatLuc = DateTime.UtcNow };
        _duLieu.Add(muc);
        return CreatedAtAction(nameof(LayTheoId), new { id = muc.Id }, muc);
    }

    /// <summary>Xóa theo Id.</summary>
    [HttpDelete("{id:int}")]
    public IActionResult XoaTheoId(int id)
    {
        var idx = _duLieu.FindIndex(x => x.Id == id);
        if (idx < 0) return NotFound();
        _duLieu.RemoveAt(idx);
        return NoContent();
    }
}
