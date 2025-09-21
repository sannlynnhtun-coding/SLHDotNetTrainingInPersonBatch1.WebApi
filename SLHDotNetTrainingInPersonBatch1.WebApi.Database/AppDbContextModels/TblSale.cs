using System;
using System.Collections.Generic;

namespace SLHDotNetTrainingInPersonBatch1.WebApi.Database.AppDbContextModels;

public partial class TblSale
{
    public int SaleId { get; set; }

    public string VoucherNo { get; set; } = null!;

    public DateTime SaleDate { get; set; }

    public decimal TotalAmount { get; set; }
}
