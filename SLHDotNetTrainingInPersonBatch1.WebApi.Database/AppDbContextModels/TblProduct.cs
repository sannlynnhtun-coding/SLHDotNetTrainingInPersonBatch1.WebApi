using System;
using System.Collections.Generic;

namespace SLHDotNetTrainingInPersonBatch1.WebApi.Database.AppDbContextModels;

public partial class TblProduct
{
    public string ProductId { get; set; } = null!;

    public string ProductCode { get; set; } = null!;

    public string ProductName { get; set; } = null!;

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public bool DeleteFlag { get; set; }
}
