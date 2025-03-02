using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EpidemicDiseaseTrackerAPI.Models;

[Table("DiseaseCasesByWeek")]
public partial class DiseaseCasesByWeek
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    public int? CasesReported { get; set; }

    public int ReportYear { get; set; }

    [StringLength(3)]
    public string ReportWeek { get; set; } = null!;
}
