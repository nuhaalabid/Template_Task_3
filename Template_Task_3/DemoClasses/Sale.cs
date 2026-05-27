namespace Template_Task_3.DemoClasses;

public class Sale
{
    public string ProductCode { get; set; }
    public string ProductName { get; set; }
    public decimal Price { get; set; }
    public string CustomerName { get; set; }

    public Sale(string productCode, string productName, decimal price, string customerName)
    {
        ProductCode = productCode;
        ProductName = productName;
        Price = price;
        CustomerName = customerName;
    }

    public override string ToString()
    {
        return $"{CustomerName} köpte {ProductName} för {Price} kr";
    }
}

