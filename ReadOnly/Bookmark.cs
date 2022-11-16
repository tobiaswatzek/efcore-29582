using System;
using System.Collections.Generic;

namespace ReadOnly;

public partial class Bookmark
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public virtual ICollection<CalculationBookmark> CalculationBookmarks { get; } = new List<CalculationBookmark>();
}
