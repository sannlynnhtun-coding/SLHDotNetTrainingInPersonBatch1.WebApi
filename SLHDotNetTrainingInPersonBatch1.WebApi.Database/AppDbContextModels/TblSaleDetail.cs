using System;
using System.Collections.Generic;

namespace SLHDotNetTrainingInPersonBatch1.WebApi.Database.AppDbContextModels;

public partial class TblSaleDetail
{
    public int SaleDetailId { get; set; }

    public int SaleId { get; set; }

    public string ProductId { get; set; } = null!;

    public int Quantity { get; set; }

    public decimal Price { get; set; }
}
