using System;
using System.Collections.Generic;

namespace BookingSystem.DataAccsess.Models;

public partial class MstLocation
{
    public int LocationId { get; set; }

    public string LocationName { get; set; } = null!;

    public int CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int? DeletedBy { get; set; }

    public DateTime? DeletedDate { get; set; }

    public virtual ICollection<MstRoom> MstRooms { get; set; } = new List<MstRoom>();
}
