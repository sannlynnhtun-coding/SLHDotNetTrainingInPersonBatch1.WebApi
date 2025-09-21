using System;
using System.Collections.Generic;

namespace SLHDotNetTrainingInPersonBatch1.WebApi.Database.AppDbContextModels;

public partial class TblClass
{
    public int ClassId { get; set; }

    public string ClassNo { get; set; } = null!;

    public string ClassName { get; set; } = null!;
}
