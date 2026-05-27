namespace Template_Task_3.Helpers;

public class MenuConstants
{
    public const string Title = "===== ÖVNING 4 - MINNE, KOLLEKTIONER OCH REKURSION =====";

    public const string ShowProducts = "1";
    public const string FindProduct = "2";
    public const string AddProduct = "3";
    public const string ChangeStock = "4";
    public const string AddCustomerToQueue = "5";
    public const string ServeNextCustomer = "6";
    public const string PrintCustomerQueue = "7";
    public const string SellProduct = "8";
    public const string UndoLastSale = "9";
    public const string PrintLog = "10";
    public const string ArrayLab = "11";
    public const string ListLab = "12";
    public const string ReverseTextLab = "13";
    public const string WordCountLab = "14";
    public const string ParenthesesLab = "15";
    public const string MemoryLab = "16";
    public const string RecursionLab = "17";
    public const string SaveLogToFile = "18";
    public const string GetBetterPrice = "19";
    public const string Exit = "0";

    // Alla menyval samlade i en List — lägg till nya rader här, PrintMenu sköter resten.
    public static readonly List<MenuItem> Items = new List<MenuItem>
    {
        new MenuItem(ShowProducts,       "Visa produkter"),
        new MenuItem(FindProduct,        "Sök produkt"),
        new MenuItem(AddProduct,         "Lägg till produkt"),
        new MenuItem(ChangeStock,        "Ändra lagersaldo"),
        new MenuItem(GetBetterPrice,     "Refakturera if/else → Dictionary"),
        new MenuItem(AddCustomerToQueue, "Lägg kund i kö"),
        new MenuItem(ServeNextCustomer,  "Betjäna nästa kund"),
        new MenuItem(PrintCustomerQueue, "Visa kundkö"),
        new MenuItem(SellProduct,        "Sälj produkt"),
        new MenuItem(UndoLastSale,       "Ångra senaste försäljning"),
        new MenuItem(PrintLog,           "Visa logg"),
        new MenuItem(ArrayLab,           "Array-labb"),
        new MenuItem(ListLab,            "List-labb"),
        new MenuItem(ReverseTextLab,     "Stack-labb: vänd text"),
        new MenuItem(WordCountLab,       "Dictionary-labb: räkna ord"),
        new MenuItem(ParenthesesLab,     "Stack + Dictionary: kontrollera parenteser"),
        new MenuItem(MemoryLab,          "Stack och heap-labb"),
        new MenuItem(RecursionLab,       "Rekursion och iteration"),
        new MenuItem(SaveLogToFile,      "Spara logg till fil"),
        new MenuItem(Exit,               "Avsluta"),
    };
}
