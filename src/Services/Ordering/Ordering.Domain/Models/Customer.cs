using Ordering.Domain.Abstractions;

namespace Ordering.Domain.Models;

public class Customer : Entity<Guid>
{
    public string Name { get; private set; } = default!;

    public string Emaul { get; private set; } = default!;
}
