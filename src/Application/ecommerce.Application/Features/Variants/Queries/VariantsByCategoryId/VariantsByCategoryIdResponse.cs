namespace ecommerce.Application.Features.Variants.Queries.VariantsByCategoryId;
public sealed record VariantsByCategoryIdResponse(Guid Id, String Name, IEnumerable<VariantsByCategoryIdOptions> Options);
public sealed record VariantsByCategoryIdOptions(Guid Id, String Value);