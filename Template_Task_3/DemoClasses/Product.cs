namespace Template_Task_3.DemoClasses;

public class Product
{
    public string Code { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }

    public Product(string code, string name, decimal price, int stock)
    {
        Code = code;
        Name = name;
        Price = price;
        Stock = stock;
    }

    public override string ToString()
    {
        return $"{Code}: {Name}, {Price} kr, saldo: {Stock}";
    }
}
