using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using CleanService.Src.Common;

using Microsoft.EntityFrameworkCore;

namespace CleanService.Src.Models;

public class Contracts : BaseEntity
{
    [Key] public Guid Id { get; set; }

    public string Description { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}

public class PartialContracts : BaseEntity
{
    public Guid? Id { get; set; }

    public string? Description { get; set; }
}
