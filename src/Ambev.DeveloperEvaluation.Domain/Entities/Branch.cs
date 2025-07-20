namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class Branch
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public ICollection<Sale> Sales { get; set; } = [];
}
