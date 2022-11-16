using System;
using System.Collections.Generic;

namespace ReadOnly;

public partial class CalculationBookmark
{
    public int Id { get; set; }

    public string CalculationId { get; set; } = null!;

    public DateTimeOffset CreatedAt { get; set; }

    public int BookmarksId { get; set; }

    public virtual Bookmark Bookmarks { get; set; } = null!;
}
