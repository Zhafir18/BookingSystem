using System;
using System.Collections.Generic;

namespace BookingSystem.DataAccsess.Models;

public partial class MstRoom
{
    public int RoomId { get; set; }

    public int LocationId { get; set; }

    public string RoomName { get; set; } = null!;

    public int Floor { get; set; }

    public string? Description { get; set; }

    public int Capacity { get; set; }

    public string Color { get; set; } = null!;

    public int CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int? DeletedBy { get; set; }

    public DateTime? DeletedDate { get; set; }

    public virtual MstLocation Location { get; set; } = null!;

    public virtual ICollection<MstRoomResource> MstRoomResources { get; set; } = new List<MstRoomResource>();
}
