using System;
using System.Collections.Generic;

namespace BookingSystem.DataAccsess.Models;

public partial class MstRoomResource
{
    public int Id { get; set; }

    public int RoomId { get; set; }

    public int ResourceId { get; set; }

    public DateTime CreateDate { get; set; }

    public int CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    public DateTime? DelDate { get; set; }

    public int? DelBy { get; set; }

    public virtual MstResource Resource { get; set; } = null!;

    public virtual MstRoom Room { get; set; } = null!;
}
