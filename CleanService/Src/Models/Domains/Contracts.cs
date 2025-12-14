using System.ComponentModel.DataAnnotations;

using CleanService.Src.Common;

namespace CleanService.Src.Models.Domains;

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
