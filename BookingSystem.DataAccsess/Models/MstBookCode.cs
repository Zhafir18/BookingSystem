using System;
using System.Collections.Generic;

namespace BookingSystem.DataAccsess.Models;

public partial class MstBookCode
{
    public int Id { get; set; }

    public string BookingCode { get; set; } = null!;

    public bool IsActive { get; set; }

    public DateTime CreateDate { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    public DateTime? DelDate { get; set; }

    public int? DelBy { get; set; }
}
