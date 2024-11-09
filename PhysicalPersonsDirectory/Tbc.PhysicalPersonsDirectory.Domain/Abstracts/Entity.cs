namespace Tbc.PhysicalPersonsDirectory.Domain.Abstracts;

public abstract class Entity<TKey>
{
    public TKey Id { get; set; }
}