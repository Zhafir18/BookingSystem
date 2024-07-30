using System;
using System.Collections.Generic;

namespace BookingSystem.DataAccsess.Models;

public partial class MstResource
{
    public int ResourceId { get; set; }

    public string ResourceName { get; set; } = null!;

    public bool Status { get; set; }

    public string ResourceIcon { get; set; } = null!;

    public int CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int? DeletedBy { get; set; }

    public DateTime? DeletedDate { get; set; }

    public virtual ICollection<MstResourceCode> MstResourceCodes { get; set; } = new List<MstResourceCode>();
}
