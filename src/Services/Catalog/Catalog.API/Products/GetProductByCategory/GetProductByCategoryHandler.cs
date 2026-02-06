namespace Catalog.API.Products.GetProductByCategory;

public record GerProductByCategoryQuery(string Category) : IQuery<GetProductByCategoryResult>;
public record GetProductByCategoryResult(IEnumerable<Product> Products);
internal class GetProductByCategoryHandler
    (IDocumentSession session)
    : IQueryHandler<GerProductByCategoryQuery, GetProductByCategoryResult>
{
    public async Task<GetProductByCategoryResult> Handle(GerProductByCategoryQuery query, CancellationToken cancellationToken)
    {

        var products = await session.Query<Product>()
            .Where(p => p.Category.Contains(query.Category)).ToListAsync();

       
        return new GetProductByCategoryResult(products);
    }
}
