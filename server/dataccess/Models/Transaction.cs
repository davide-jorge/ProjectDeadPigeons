using System;
using System.Collections.Generic;

namespace dataccess.Models;

public partial class Transaction
{
    public Guid Id { get; set; }

    public Guid PlayerId { get; set; }

    public decimal Amount { get; set; }

    public string MpReference { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual Player Player { get; set; } = null!;
}
