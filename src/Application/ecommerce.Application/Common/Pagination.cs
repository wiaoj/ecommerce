
namespace ecommerce.Application.Common;
public sealed record Pagination<T> : IPagination<T> {
    public Int32 Page { get; }
    public Int32 Size { get; }
    public Int32 Count { get; }
    public Int32 TotalPage { get; }
    public IEnumerable<T> Items { get; }
    public Boolean HasPrevious => this.Page > 1;
    public Boolean HasNext => this.Page < this.TotalPage;

    public Pagination(Int32 page,
                      Int32 size,
                      Int32 count,
                      Int32 totalPage,
                      IEnumerable<T> items) {
        this.Page = page;
        this.Size = size;
        this.Count = count;
        this.TotalPage = totalPage;
        this.Items = items;
    }
}
