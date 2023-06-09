using Domain.Common;

namespace Domain.Entities;

public class Dog : IBaseEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Color { get; set; }
    public int TailLength { get; set; }
    public int Weight { get; set; }
}