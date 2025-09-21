using System;
using System.Collections.Generic;

namespace SLHDotNetTrainingInPersonBatch1.WebApi.Database.AppDbContextModels;

public partial class TblStudentClass
{
    public int StudentClassId { get; set; }

    public string ClassNo { get; set; } = null!;

    public string StudentNo { get; set; } = null!;
}
