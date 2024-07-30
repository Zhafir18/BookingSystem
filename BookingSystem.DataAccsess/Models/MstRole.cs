using System;
using System.Collections.Generic;

namespace BookingSystem.DataAccsess.Models;

public partial class MstRole
{
    public int RoleId { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string RoleName { get; set; } = null!;

    public int CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? DeletedDate { get; set; }

    public int? DeletedBy { get; set; }

    public virtual ICollection<MstRoleMenu> MstRoleMenus { get; set; } = new List<MstRoleMenu>();

    public virtual ICollection<MstUser> MstUsers { get; set; } = new List<MstUser>();
}
