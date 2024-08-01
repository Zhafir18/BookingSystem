using System;
using System.Collections.Generic;

namespace BookingSystem.DataAccsess.Models;

public partial class MstRoomResource
{
    public int Id { get; set; }

    public int RoomId { get; set; }

    public int ResourceId { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int? DeletedBy { get; set; }

    public DateTime? DeletedDate { get; set; }

    public virtual MstResource Resource { get; set; } = null!;

    public virtual MstRoom Room { get; set; } = null!;
}
