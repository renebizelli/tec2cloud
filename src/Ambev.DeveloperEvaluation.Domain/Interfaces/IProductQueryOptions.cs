namespace Ambev.DeveloperEvaluation.Domain.Interfaces;

public interface IProductQueryOptions
{
    List<(string, bool)> OrderCriteria { get; set; }

    string? Category { get; set; }
    string? Title { get; set; }
    decimal MinPrice { get; set; }
    decimal MaxPrice { get; set; }

    int Page { get; set; }
    int PageSize { get; set; }
}
