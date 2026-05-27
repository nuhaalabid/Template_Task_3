namespace Template_Task_3.DemoClasses;

public class Customer
{
    public string Name { get; set; }
    public DateTime AddedAt { get; set; }

    public Customer(string name)
    {
        Name = name;
        AddedAt = DateTime.Now;
    }

    public override string ToString()
    {
        return $"{Name} ({AddedAt:yyyy-MM-dd HH:mm})";
    }
}
