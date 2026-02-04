namespace Catalog.API.Products.GetProductByCategory;

public record GerProductByCategoryQuery(string Category) : IQuery<GetProductByCategoryResult>;
public record GetProductByCategoryResult(IEnumerable<Product> Products);
internal class GetProductByCategoryHandler
    (IDocumentSession session, ILogger<GetProductByCategoryHandler> logger)
    : IQueryHandler<GerProductByCategoryQuery, GetProductByCategoryResult>
{
    public async Task<GetProductByCategoryResult> Handle(GerProductByCategoryQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation($"GetProductByCategoryHandler. handle called with {query}");

        var products = await session.Query<Product>()
            .Where(p => p.Category.Contains(query.Category)).ToListAsync();

        if(products is null)
        {
            throw new ProductNotFoundException();
        }
        return new GetProductByCategoryResult(products);
    }
}
