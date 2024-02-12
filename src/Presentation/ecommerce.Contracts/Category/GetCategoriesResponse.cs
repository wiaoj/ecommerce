using System.Diagnostics.CodeAnalysis;

namespace ecommerce.Contracts.Category;
public partial class ExternalResponse {
    public sealed record GetCategoriesResponse(Guid Id, String Name, [MaybeNull] Guid? ParentId, IEnumerable<Guid> ChildIds);
}