using System;
using System.Collections.Generic;

namespace BookingSystem.DataAccsess.Models;

public partial class MstBook
{
    public int Id { get; set; }

    public int RoomId { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public bool IsVip { get; set; }

    public DateTime CreatedDate { get; set; }

    public int CreatedBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    public DateTime? DelDate { get; set; }

    public int? DelBy { get; set; }
}
