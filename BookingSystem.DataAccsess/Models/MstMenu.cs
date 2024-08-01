using System;
using System.Collections.Generic;

namespace BookingSystem.DataAccsess.Models;

public partial class MstMenu
{
    public int MenuId { get; set; }

    public string MenuName { get; set; } = null!;

    public int CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int? DeletedBy { get; set; }

    public DateTime? DeletedDate { get; set; }

    public virtual ICollection<MstRoleMenu> MstRoleMenus { get; set; } = new List<MstRoleMenu>();
}
