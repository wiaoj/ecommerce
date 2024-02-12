
namespace ecommerce.Application.Common;
public interface IPagination<T> {
    Int32 Page { get; }
    Int32 Size { get; }
    Int32 Count { get; }
    Int32 TotalPage { get; }
    IEnumerable<T> Items { get; }
    Boolean HasPrevious { get; }
    Boolean HasNext { get; }
}