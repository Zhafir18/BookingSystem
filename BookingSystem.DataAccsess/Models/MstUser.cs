using System;
using System.Collections.Generic;

namespace BookingSystem.DataAccsess.Models;

public partial class MstUser
{
    public int UserId { get; set; }

    public string LoginName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int? RoleId { get; set; }

    public string FirstName { get; set; } = null!;

    public string? MiddleName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public DateTime CreatedDate { get; set; }

    public int CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? DeletedDate { get; set; }

    public int? DeletedBy { get; set; }

    public virtual MstUser CreatedByNavigation { get; set; } = null!;

    public virtual MstUser? DeletedByNavigation { get; set; }

    public virtual ICollection<MstUser> InverseCreatedByNavigation { get; set; } = new List<MstUser>();

    public virtual ICollection<MstUser> InverseDeletedByNavigation { get; set; } = new List<MstUser>();

    public virtual ICollection<MstUser> InverseUpdatedByNavigation { get; set; } = new List<MstUser>();

    public virtual ICollection<MstMenu> MstMenuCreatedByNavigations { get; set; } = new List<MstMenu>();

    public virtual ICollection<MstMenu> MstMenuDeletedByNavigations { get; set; } = new List<MstMenu>();

    public virtual ICollection<MstMenu> MstMenuUpdatedByNavigations { get; set; } = new List<MstMenu>();

    public virtual ICollection<MstRoleMenu> MstRoleMenuCreatedByNavigations { get; set; } = new List<MstRoleMenu>();

    public virtual ICollection<MstRoleMenu> MstRoleMenuDeletedByNavigations { get; set; } = new List<MstRoleMenu>();

    public virtual ICollection<MstRoleMenu> MstRoleMenuUpdatedByNavigations { get; set; } = new List<MstRoleMenu>();

    public virtual MstRole? Role { get; set; }

    public virtual MstUser? UpdatedByNavigation { get; set; }
}
