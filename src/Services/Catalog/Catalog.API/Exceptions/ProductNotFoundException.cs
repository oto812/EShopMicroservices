using BuildingBlocks.Exceptions;

    public class ProductNotFoundException : NotFoundException
    {
        public ProductNotFoundException(Guid id) : base("product", id)
        {
        }
    }

