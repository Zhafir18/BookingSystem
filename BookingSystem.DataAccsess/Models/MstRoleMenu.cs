using System;
using System.Collections.Generic;

namespace BookingSystem.DataAccsess.Models;

public partial class MstRoleMenu
{
    public int Roleid { get; set; }

    public int Menuid { get; set; }

    public int CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int? DeletedBy { get; set; }

    public DateTime? DeletedDate { get; set; }

    public virtual MstUser CreatedByNavigation { get; set; } = null!;

    public virtual MstUser? DeletedByNavigation { get; set; }

    public virtual MstMenu Menu { get; set; } = null!;

    public virtual MstRole Role { get; set; } = null!;

    public virtual MstUser? UpdatedByNavigation { get; set; }
}
