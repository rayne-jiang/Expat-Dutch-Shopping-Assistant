// Entities/Product.cs
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public Product()
    {
        Name = string.Empty;
    }
}
