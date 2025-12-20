using System;
using System.Collections.Generic;

namespace dataccess.Models;

public partial class Board
{
    public Guid Id { get; set; }

    public Guid PlayerId { get; set; }

    public Guid GameId { get; set; }

    public int NumberOfFields { get; set; }

    public decimal Price { get; set; }

    public List<int> Numbers { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual Game Game { get; set; } = null!;

    public virtual Player Player { get; set; } = null!;

    public virtual ICollection<Winner> Winners { get; set; } = new List<Winner>();
}
