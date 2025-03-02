using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EpidemicDiseaseTrackerAPI.Models;

[Table("DiseaseCasesByYear")]
[Index("ReportYear", Name = "UQ__DiseaseC__0F2C1527A991D000", IsUnique = true)]
public partial class DiseaseCasesByYear
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    public int? CasesReported { get; set; }

    public int ReportYear { get; set; }
}
