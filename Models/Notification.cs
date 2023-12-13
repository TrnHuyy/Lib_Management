using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lib2.Models;

namespace Lib2.Models;
public class Notification
{
    public int Id { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    // Các thuộc tính khác cho thông báo, ví dụ: Sender, Receiver, Status, v.v.

    // Constructor để tạo mới một thông báo
    public Notification()
    {
        CreatedAt = DateTime.UtcNow; // Gán thời điểm tạo thông báo là thời điểm hiện tại
    }

    // Phương thức ToString để trả về chuỗi biểu diễn thông báo
    public override string ToString()
    {
        return $"Id: {Id}, Content: {Content}, CreatedAt: {CreatedAt}";
    }
}
