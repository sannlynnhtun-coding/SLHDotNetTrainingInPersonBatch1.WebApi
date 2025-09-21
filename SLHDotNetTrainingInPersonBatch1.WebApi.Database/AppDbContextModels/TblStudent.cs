using System;
using System.Collections.Generic;

namespace SLHDotNetTrainingInPersonBatch1.WebApi.Database.AppDbContextModels;

public partial class TblStudent
{
    public int StudentId { get; set; }

    public string StudentNo { get; set; } = null!;

    public string StudentName { get; set; } = null!;
}
