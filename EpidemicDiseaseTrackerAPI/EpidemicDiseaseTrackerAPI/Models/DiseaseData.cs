using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EpidemicDiseaseTrackerAPI.Models;

public partial class DiseaseData
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string? EpiWeek { get; set; }

    [StringLength(100)]
    public string? Disease { get; set; }

    public int? NoOfCases { get; set; }
}
