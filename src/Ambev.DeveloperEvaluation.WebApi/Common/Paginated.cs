namespace Ambev.DeveloperEvaluation.WebApi.Common;
public class Paginated<T>
{
    public int TotalCount { get; set; }
    public List<T> Items { get; set; } = [];
}
