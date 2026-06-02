using Template_Task_3.DemoClasses;
using Template_Task_3.Helpers;
using Template_Task_3.StackAndHeap;

namespace Template_Task_3;

internal class Program
{
    // Dictionary: snabb uppslagning av produkter via produktkod (key = kod, value = produkt)
    static Dictionary<string, Product> products = new Dictionary<string, Product>();

    // List: enkel logg över vad som hänt i programmet — ordnad och växer dynamiskt
    static List<string> logMessages = new List<string>();

    // Queue: FIFO — kunder betjänas i den ordning de ställde sig i kön
    static Queue<Customer> customerQueue = new Queue<Customer>();

    // Stack: LIFO — används för att kunna ångra den senaste försäljningen
    static Stack<Sale> saleHistory = new Stack<Sale>();

    static string ReadLine => Console.ReadLine() ?? string.Empty;

    static void Main(string[] args)
    {
        //ToDo implementera 
        SeedProducts();

        bool running = true;

       do
        {
            PrintMenu();

            Console.Write("Välj: ");
            string choice = ReadLine;

            Console.WriteLine();

            switch (choice)
            {
                case MenuConstants.ShowProducts:
                    PrintProducts();
                    break;

                case MenuConstants.FindProduct:
                    FindProduct();
                    break;

                case MenuConstants.AddProduct:
                    AddProduct();
                    break;

                case MenuConstants.ChangeStock:
                    ChangeStock();
                    break;

                case MenuConstants.GetBetterPrice:
                    Console.Write("Ange produktkod: ");
                    GetPriceBetter(ReadLine.ToUpper());
                    break;

                case MenuConstants.AddCustomerToQueue:
                    AddCustomerToQueue();
                    break;

                case MenuConstants.ServeNextCustomer:
                    ServeNextCustomer();
                    break;

                case MenuConstants.PrintCustomerQueue:
                    PrintCustomerQueue();
                    break;

                case MenuConstants.SellProduct:
                    SellProduct();
                    break;

                case MenuConstants.UndoLastSale:
                    UndoLastSale();
                    break;

                case MenuConstants.PrintLog:
                    PrintLog();
                    break;

                case MenuConstants.ArrayLab:
                    ArrayLab();
                    break;

                case MenuConstants.ListLab:
                    ListLab();
                    break;

                case MenuConstants.ReverseTextLab:
                    ReverseTextLab();
                    break;

                case MenuConstants.WordCountLab:
                    WordCountLab();
                    break;

                case MenuConstants.ParenthesesLab:
                    ParenthesesLab();
                    break;

                case MenuConstants.MemoryLab:
                    MemoryLab();
                    break;

                case MenuConstants.RecursionLab:
                    RecursionLab();
                    break;

                case MenuConstants.SaveLogToFile:
                    SaveLogToFile();
                    break;

                case MenuConstants.Exit:
                    running = false;
                    break;

                default:
                    Console.WriteLine("Felaktigt val.");
                    break;
            }

            Console.WriteLine();
            Console.WriteLine("Tryck på valfri tangent för att fortsätta...");
            Console.ReadKey();
            Console.Clear();
        }
        while(running);
    }

    static void PrintMenu()
    {
        Console.WriteLine(MenuConstants.Title);
        Console.WriteLine();

        foreach (MenuItem item in MenuConstants.Items)
        {
            Console.WriteLine(item);
        }

        Console.WriteLine();
    }

    // start of assignment
    #region Dictionary

    // ============================================================
    // DEL 1 - PRODUKTER OCH DICTIONARY
    // ============================================================

    static void SeedProducts()
    {
        // Exempel på hur du lägger till en produkt i dictionaryn:
        // products["KAFFE"] = new Product("KAFFE", "Kaffe", 15.00m, 50);
        // TODO:

        products["KAFFE"] = new Product("KAFFE", "Kaffe", 15.00m, 50);
        products["TE"] = new Product("TE", "Te", 12.00m, 40);
        products["MJOLK"] = new Product("MJOLK", "Mjölk", 18.50m, 30);
        products["BROD"] = new Product("BROD", "Bröd", 25.00m, 20);
        products["SMOR"] = new Product("SMOR", "Smör", 35.00m, 15);
        products["OST"] = new Product("OST", "Ost", 45.00m, 10);
        products["AGG"] = new Product("AGG", "Ägg", 32.00m, 25);
        products["RIS"] = new Product("RIS", "Ris", 28.00m, 18);
        products["PASTA"] = new Product("PASTA", "Pasta", 22.00m, 35);
        products["SOCKER"] = new Product("SOCKER", "Socker", 19.00m, 12);
    }

    static void PrintProducts()
    {
        Console.WriteLine("=== Produkter ===");

        // TODO:
        // Loopa igenom dictionaryn products.
        // Skriv ut varje produkt.
        // Räkna även ut totalt lagervärde.
        // Lagervärde för en produkt:
        // product.Price * product.Stock
        decimal totalValue = 0;

        foreach (KeyValuePair<string, Product> pair in products)
        {
            Product product = pair.Value;
            Console.WriteLine(product);

            totalValue += product.Price * product.Stock;
        }
        Console.WriteLine($"Totalt lagervärde: {totalValue} kr");

        // Fråga:
        // Varför passar Dictionary bra för ett produktregister?
        //Svara: Dictionary passar bra för ett produktregister eftersom man snabbt kan
        // hitta en produkt med hjälp av produktkoden.
    }

    static void FindProduct()
    {
        Console.Write("Ange produktkod: ");
        string code = Console.ReadLine().ToUpper();

        if (products.TryGetValue(code, out Product product))
        {
            Console.WriteLine(product);
        }
        else
        {
            Console.WriteLine("Produkten hittades inte.");
        }

        // Fråga:
        // Varför är TryGetValue bättre än att skriva products[code] direkt?
        // Svara:TryGetValue är säkrare eftersom programmet inte kraschar om koden
        // inte finns. Metoden returnerar false istället.
    }

    static void AddProduct()
    {
        Console.WriteLine("Ange produktkod");

        string code = Console.ReadLine().ToUpper();

        if (products.ContainsKey(code))
        {
            Console.WriteLine("Produkten finns redan.");
            return;
        }

        Console.Write("Ange namn: ");
        string name = Console.ReadLine();

        decimal price = InputHelpers.ReadDecimal("Ange pris: ");
        int stock = InputHelpers.ReadInt("Ange lagersaldo: ");

        Product product = new Product(code, name, price, stock);

        products.Add(code, product);

        logMessages.Add($"Produkt tillagd: {code} - {name}");

        Console.WriteLine("Produkten har lagts till.");

        // Fråga:
        // Vad är nyckeln och vad är värdet i products?
       // Svara:Nyckeln är produktkoden (code) och värdet är Product-objektet (product).
    }

    static void ChangeStock()
    {
        Console.WriteLine("Ange produktkod");
        // TODO:
        string code = Console.ReadLine().ToUpper();

        if (products.TryGetValue(code, out Product product))
        {
            int newStock = InputHelpers.ReadInt("Ange nytt lagersaldo: ");

            product.Stock = newStock;

            logMessages.Add($"Lagersaldo ändrat för {code}. Nytt saldo: {newStock}");

            Console.WriteLine("Lagersaldo uppdaterat.");
        }
        else
        {
            Console.WriteLine("Produkten hittades inte.");
        }
    }

    static decimal GetPriceBad(string code)
    {
        if (code == "KAF")
        {
            return 15;
        }
        else if (code == "TE")
        {
            return 12;
        }
        else if (code == "BUL")
        {
            return 18;
        }
        else if (code == "MCK")
        {
            return 35;
        }
        else
        {
            return -1;
        }
    }

    static decimal GetPriceBetter(string code)
    {
        // TODO:
        Dictionary<string, decimal> prices = new()
    {
        { "KAF", 15 },
        { "TE", 12 },
        { "BUL", 18 },
        { "MCK", 35 }
    };

        if (prices.TryGetValue(code.ToUpper(), out decimal price))
        {
            Console.WriteLine($"pris:{price} kr");
            return price;
        }
        return -1;

        // Fråga:
        // Varför är Dictionary-lösningen bättre än många if/else-satser?
       // Svara:Dictionary är bättre eftersom den är enklare att utöka,
       // ger snabbare sökningar och gör koden renare.    
    }

    #endregion

    #region Queue

    // ============================================================
    // DEL 2 - QUEUE
    // ============================================================

    static void AddCustomerToQueue()
    {
        Console.WriteLine("Registrera ny kund");

        // TODO:
        string name = InputHelpers.ReadString("Ange kundens namn: ");

        Customer customer = new Customer(name);

        customerQueue.Enqueue(customer);

        Console.WriteLine($"Kunden lades till. Plats i kön: {customerQueue.Count}");

        logMessages.Add($"Kund tillagd i kön: {name}");
        // Fråga: Vad betyder FIFO?
        // Svara:FIFO betyder First In, First Out.
        // Den kund som kommer först i kön blir också betjänad först.
    }

    static void ServeNextCustomer()
    {
        Console.WriteLine("Betjäna nästa kund");

        if (customerQueue.Count == 0)
        {
            Console.WriteLine("Kön är tom.");
            return;
        }

        Customer customer = customerQueue.Dequeue();

        Console.WriteLine($"Kund betjänad: {customer.Name}");

        logMessages.Add($"Kund betjänad: {customer.Name}");

        // Fråga:Varför passar Queue bättre än Stack för en kundkö?
        // Svara:Queue passar bättre eftersom den använder FIFO
        // First In, First Out. Den kund som kommer först
        // blir också betjänad först.
    }

    static void PrintCustomerQueue()
    {
        Console.WriteLine("==Kundkö ==");

        // TODO:
        // Om customerQueue är tom, skriv att kön är tom.
        // Annars: loopa igenom customerQueue med en räknare.
        // Skriv ut platsnummer, namn och tidsstämpel för varje kund.
        //
        // Exempel:
        // 1. Kalle (2026-05-26 10:01)
        // 2. Greta (2026-05-26 10:02)
        // 3. Stina (2026-05-26 10:03)
        //
        // Tips: foreach fungerar på Queue utan att ta bort elementen.
        if (customerQueue.Count == 0)
        {
            Console.WriteLine("Kön är tom.");
            return;
        }

        int place = 1;

        foreach (Customer customer in customerQueue)
        {
            Console.WriteLine($"{place}. {customer}");
            place++;
        }
    }


    #endregion

    #region Stack

    // ============================================================
    // DEL 3 - STACK OCH FÖRSÄLJNING
    // ============================================================

    static void SellProduct()
    {
        // TODO:
        // Kontrollera om customerQueue är tom — skriv meddelande om den är det.
        // Använd Peek för att se vilken kund som står först (utan att ta bort dem).
        // Läs in produktkod.
        // Slå upp produkten med TryGetValue.
        // Kontrollera att produkten finns i lager (Stock > 0).
        // Minska produktens Stock med 1.
        // Skapa ett Sale-objekt med produktinfo och kundens namn.
        // Lägg Sale-objektet på saleHistory med Push.
        // Lägg till ett loggmeddelande i logMessages.
        //
        // Extra:
        // Bestäm om kunden ska tas bort från kön efter köp eller inte.
        // Motivera ditt val i kommentar.

        //Svara:Jag väljer att inte ta bort kunden från kön efter köp.
        // Kunden kan då köpa flera produkter innan den betjänas klart.

        if (customerQueue.Count == 0)
        {
            Console.WriteLine("Det finns ingen kund i kön.");
            return;
        }

        Customer customer = customerQueue.Peek();

        Console.Write("Ange produktkod: ");
        string code = Console.ReadLine().ToUpper();

        if (!products.TryGetValue(code, out Product product))
        {
            Console.WriteLine("Produkten hittades inte.");
            return;
        }

        if (product.Stock <= 0)
        {
            Console.WriteLine("Produkten finns inte i lager.");
            return;
        }

        product.Stock--;

        Sale sale = new Sale(product.Code, product.Name, product.Price, customer.Name);

        saleHistory.Push(sale);

        logMessages.Add($"Försäljning: {product.Name} såldes till {customer.Name}");

        Console.WriteLine($"{product.Name} såldes till {customer.Name}.");

        //Fråga: Varför sparar vi försäljningar i en Stack?
       //Svar:Vi sparar försäljningar i en Stack eftersom Stack använder LIFO.
       // Då kan vi enkelt ångra den senaste försäljningen först.
    }

    static void UndoLastSale()
    {
        Console.WriteLine("Ångra senaste försäljning ");

        // TODO:
        // Kontrollera om saleHistory är tom — skriv meddelande om den är det.
        // Om den inte är tom:
        // Använd Pop för att hämta och ta bort senaste försäljningen.
        // Slå upp produkten i products med försäljningens ProductCode.
        // Öka produktens Stock med 1.
        // Logga vad som ångrades i logMessages.

        if (saleHistory.Count == 0)
        {
            Console.WriteLine("Det finns inga försäljningar att ångra.");
            return;
        }

        Sale sale = saleHistory.Pop();

        if (products.TryGetValue(sale.ProductCode, out Product product))
        {
            product.Stock++;

            Console.WriteLine($"Försäljningen av {product.Name} har ångrats.");

            logMessages.Add($"Ångrad försäljning: {product.Name}");
        }

        // Fråga:
        // Vad betyder LIFO?
        // Svar:LIFO betyder Last In, First Out.
        // Den senaste försäljningen som lades i stacken tas bort först.
    }

    static void ReverseTextLab()
    {
        Console.WriteLine("==Stack-labb: vänd text =");
        Console.Write("Ange en text: ");
        string text = Console.ReadLine();

        Stack<char> chars = new Stack<char>();

        foreach (char c in text)
        {
            chars.Push(c);
        }

        while (chars.Count > 0)
        {
            Console.Write(chars.Pop());
        }

        Console.WriteLine();
    }

        // TODO:
        // Läs in en text från användaren.
        // Skriv ut texten bakofram använd en lämplig collektion.
    

    #endregion

    #region List

    // ============================================================
    // DEL 4 - LIST
    // ============================================================

    static void PrintLog()
    {
        Console.WriteLine("=== Logg ===");

        // TODO:
        // Om logMessages är tom, skriv "Inga loggmeddelanden finns."
        // Annars: loopa igenom logMessages och skriv ut varje meddelande.

        if (logMessages.Count == 0)
        {
            Console.WriteLine("Inga loggmeddelanden finns.");
            return;
        }

        foreach (string message in logMessages)
        {
            Console.WriteLine(message);
        }

        // Fråga:
        // Varför passar List bra för loggmeddelanden?
        // Svar: List passar bra för loggmeddelanden eftersom nya meddelanden
        // enkelt kan läggas till i slutet och visas i ordning.
    }

    static void ListLab()
    {
        Console.WriteLine("=== List-labb ===");

        List<string> shoppingList = new List<string>();

        PrintListInfo(shoppingList, "Start");

        shoppingList.Add("Mjölk");
        PrintListInfo(shoppingList, "Efter Mjölk");

        shoppingList.Add("Bröd");
        PrintListInfo(shoppingList, "Efter Bröd");

        shoppingList.Add("Smör");
        PrintListInfo(shoppingList, "Efter Smör");

        shoppingList.Add("Ost");
        PrintListInfo(shoppingList, "Efter Ost");

        shoppingList.Add("Yoghurt");
        PrintListInfo(shoppingList, "Efter Yoghurt");

        shoppingList.Remove("Smör");
        PrintListInfo(shoppingList, "Efter Remove");

        // TODO:
        // Lägg till minst 4 egna varor med en loop.
        // Skriv ut hela listan.
        for (int i = 1; i <= 4; i++)
        {
            shoppingList.Add($"Vara {i}");
        }

        Console.WriteLine("\nHela listan:");

        foreach (string item in shoppingList)
        {
            Console.WriteLine(item);
        }

        // Fråga 1:
        // Vad betyder Count?
        // Svar: Count är antalet element som finns i listan just nu.

        // Fråga 2:
        // Vad betyder Capacity?
        // Svar :Capacity är hur många element listan kan lagra innan den behöver utöka minnet.

        // Fråga 3:
        // Varför ökar inte Capacity med exakt 1 varje gång?
        // Svar 3:Capacity ökar inte med 1 varje gång eftersom List reserverar extra minne
        // för att förbättra prestandan.

        // Fråga 4:
        // Minskar Capacity automatiskt när element tas bort?
        // Svar:Nej, Capacity minskar inte automatiskt när element tas bort.
        // Count minskar men Capacity är oftast oförändrad.
    }

    static void PrintListInfo(List<string> list, string message)
    {
        Console.WriteLine($"{message}: Count = {list.Count}, Capacity = {list.Capacity}");
    }

    #endregion

    #region Array

    // ============================================================
    // DEL 5 - ARRAY
    // ============================================================

    static void ArrayLab()
    {
        Console.WriteLine("=== Array-labb ===");

        string[] weekdays = ["Måndag", "Tisdag", "Onsdag", "Torsdag", "Fredag"];

        // TODO:
        // Skriv ut alla veckodagar med en for-loop.
        // Tips: använd weekdays.Length för att veta hur många element det finns.

        // TODO:
        // Skriv ut alla veckodagar med foreach.


        Console.WriteLine("\nFor-loop:");

        for (int i = 0; i < weekdays.Length; i++)
        {
            Console.WriteLine(weekdays[i]);
        }

        Console.WriteLine("\nForeach:");

        foreach (string day in weekdays)
        {
            Console.WriteLine(day);
        }

        // Fråga 1:
        // När passar en array bättre än en List?
        // Svar 1:En array passar bra när storleken inte behöver ändras.


        // Fråga 2:
        // Vad händer om du försöker skriva weekdays[5]?
        //Svar 2: Programmet ger ett fel eftersom index 5 inte finns i arrayen.

        // Fråga 3:
        // Varför måste arrayens storlek anges från början?
        // Svar 3:Storleken måste bestämmas från början eftersom den inte kan ändras senare.
    }

    #endregion

    #region Blandat_Stack_Heap_mm

    // ============================================================
    // DEL 6 - DICTIONARY SOM RÄKNARE
    // ============================================================

    static void WordCountLab()
    {
        Console.WriteLine("=== Dictionary-labb: räkna ord ===");

        Console.WriteLine("Skriv en mening:");
        string text = ReadLine;

        //ToDo: Skriv koden för CountWords
        Dictionary<string, int> wordCounts = CountWords(text);

        Console.WriteLine("Resultat:");

        foreach (KeyValuePair<string, int> pair in wordCounts)
        {
            Console.WriteLine($"{pair.Key}: {pair.Value}");
        }

        // Fråga:
        // Varför passar Dictionary bra när vi ska räkna ord?
        Console.WriteLine("Svar: TODO - skriv ditt svar här");
    }

    static Dictionary<string, int> CountWords(string text)
    {
        Dictionary<string, int> wordCounts = new Dictionary<string, int>();

        // TODO:
        // Dela upp text i ord med string.Split.
        // Separera på: mellanslag (ett eller flera), punkt, !, ?, :, ;
        // Tips: string[] words = text.Split(new char[] { ' ', '.', '!', '?', ':', ';' },
        //                                   StringSplitOptions.RemoveEmptyEntries);
        //
        // Loopa igenom orden.
        // Gör varje ord till gemener med .ToLower() så att "Hej" och "hej" räknas som samma.
        // Om ordet redan finns i wordCounts → öka värdet med 1.
        // Annars → lägg till ordet med värdet 1.

        // Fråga:
        // Vad är nyckeln och vad är värdet i wordCounts?
        Console.WriteLine("Svar: TODO - skriv ditt svar här");

        return wordCounts;
    }

    // ============================================================
    // DEL 7 - PARENTESKONTROLL - Använd lämpliga datastrukturer
    // ============================================================

    static void ParenthesesLab()
    {
        Console.WriteLine("=== Kontrollera parenteser ===");

        // Testfall att prova:
        // ([{}])                         true
        // ({)}                           false
        // List<int> lista = new();       true
        // (]                             false
        // ((()))                         true
        // (()                            false
        // (                              false
        // )                              false
        Console.WriteLine("Skriv en kodrad eller parentessträng:");
        string input = ReadLine;

        //ToDo skriv koden för CheckParantheses
        bool isCorrect = CheckParentheses(input);

        if (isCorrect)
        {
            Console.WriteLine("Strängen är välformad.");
        }
        else
        {
            Console.WriteLine("Strängen är INTE välformad.");
        }

        
    }

    static bool CheckParentheses(string text)
    {
        // TODO:
        // Använd en Stack<char> och en Dictionary<char, char>.
        //
        // Tips Dictionary:
        // Låt dictionaryn mappa varje stängande parentes till sin matchande öppnare.
        // Det gör matchningskontrollen till en enkel uppslagning istället för flera if-satser.
        //
        // Tips Stack:
        // Stacken håller reda på vilka öppnare du sett men ännu inte stängt.
        // Tänk på vad LIFO innebär här — varför är det precis rätt egenskap för det här problemet?
        //
        // Fråga:
        // Varför är Dictionary + Stack bättre än bara Stack med if/else för matchningen?
        Console.WriteLine("Svar: TODO - skriv ditt svar här");

        return false;
    }

    // ============================================================
    // DEL 8 - STACKEN OCH HEAPEN
    // ============================================================

    static void MemoryLab()
    {
        Console.WriteLine("=== Value type: int ===");

        int number1 = 10;
        int number2 = number1;

        number2 = 99;

        Console.WriteLine($"number1: {number1}");
        Console.WriteLine($"number2: {number2}");

        Console.WriteLine();
        Console.WriteLine("=== Value type: struct ===");

        ScoreValue score1 = new ScoreValue(10);
        ScoreValue score2 = score1;

        score2.Points = 99;

        Console.WriteLine($"score1.Points: {score1.Points}");
        Console.WriteLine($"score2.Points: {score2.Points}");

        Console.WriteLine();
        Console.WriteLine("=== Reference type: class ===");

        ScoreReference refScore1 = new ScoreReference(10);
        ScoreReference refScore2 = refScore1;

        refScore2.Points = 99;

        Console.WriteLine($"refScore1.Points: {refScore1.Points}");
        Console.WriteLine($"refScore2.Points: {refScore2.Points}");

        Console.WriteLine();
        Console.WriteLine("=== Reference type: Product ===");

        Product product1 = new Product("KAF", "Kaffe", 15, 20);
        Product product2 = product1;

        product2.Stock = 0;

        Console.WriteLine(product1);
        Console.WriteLine(product2);

        // Fråga 1:
        // Varför ändras inte number1 när number2 ändras?
        Console.WriteLine("Svar 1: TODO - skriv ditt svar här");

        // Fråga 2:
        // Varför ändras inte score1.Points när score2.Points ändras?
        Console.WriteLine("Svar 2: TODO - skriv ditt svar här");

        // Fråga 3:
        // Varför ändras product1.Stock när product2.Stock ändras?
        Console.WriteLine("Svar 3: TODO - skriv ditt svar här");

        // Fråga 4:
        // Är Product en value type eller reference type?
        Console.WriteLine("Svar 4: TODO - skriv ditt svar här");

        // Fråga 5:
        // Vad ligger på heapen i Product-exemplet?
        Console.WriteLine("Svar 5: TODO - skriv ditt svar här");

        // Fråga 6:
        // Vad innebär det att två variabler kan peka på samma objekt?
        Console.WriteLine("Svar 6: TODO - skriv ditt svar här");

        // Fråga 7:
        // Vad är skillnaden mellan stacken i minnet och Stack<T> som datastruktur?
        Console.WriteLine("Svar 7: TODO - skriv ditt svar här");
    }

    #endregion

    #region ExtraUppgifter


    // ============================================================
    // DEL 9 - REKURSION OCH ITERATION EXTRA om tid finns
    // ============================================================

    static void RecursionLab()
    {
        Console.WriteLine("=== Rekursion och iteration ===");

        Console.Write("Ange n: ");

        if (!int.TryParse(ReadLine, out int n))
        {
            Console.WriteLine("Du måste skriva ett heltal.");
            return;
        }

        if (n <= 0)
        {
            Console.WriteLine("n måste vara större än 0.");
            return;
        }

        Console.WriteLine();
        Console.WriteLine($"RecursiveOdd({n}) = {RecursiveOdd(n)}");

        // TODO:
        // När du har implementerat metoderna nedan kan du avkommentera raderna.

        // Console.WriteLine($"RecursiveEven({n}) = {RecursiveEven(n)}");
        // Console.WriteLine($"IterativeEven({n}) = {IterativeEven(n)}");
        // Console.WriteLine($"FactorialRecursive({n}) = {FactorialRecursive(n)}");
        // Console.WriteLine($"SumRecursive({n}) = {SumRecursive(n)}");
        // Console.WriteLine($"SumIterative({n}) = {SumIterative(n)}");
        // Console.WriteLine($"FibonacciRecursive({n}) = {FibonacciRecursive(n)}");
        // Console.WriteLine($"FibonacciIterative({n}) = {FibonacciIterative(n)}");

        Console.WriteLine();
        Console.WriteLine("Trace av rekursion:");
        RecursiveOddWithTrace(n);

        Console.WriteLine();
        Console.WriteLine("Trace med indrag (visar rekursionsdjup):");
        RecursiveOddWithDepth(n, 0);

        // Fråga 1:
        // Vad är ett basfall?
        Console.WriteLine("Svar 1: TODO - skriv ditt svar här");

        // Fråga 2:
        // Varför måste en rekursiv metod ha ett basfall?
        Console.WriteLine("Svar 2: TODO - skriv ditt svar här");

        // Fråga 3:
        // Vad händer på stacken när en metod anropar sig själv?
        Console.WriteLine("Svar 3: TODO - skriv ditt svar här");

        // Fråga 4:
        // Vilken version är mest minnesvänlig: rekursion eller iteration? Varför?
        Console.WriteLine("Svar 4: TODO - skriv ditt svar här");
    }

    static int RecursiveOdd(int n)
    {
        if (n <= 0)
        {
            throw new ArgumentException("n måste vara större än 0.");
        }

        if (n == 1)
        {
            return 1;
        }

        return RecursiveOdd(n - 1) + 2;
    }

    static int RecursiveEven(int n)
    {
        // TODO:
        // Om n <= 0, kasta ArgumentException med meddelandet "n måste vara större än 0."
        // Om n == 1, returnera 2.
        // Annars returnera RecursiveEven(n - 1) + 2.
        //
        // Exempel:
        // RecursiveEven(1) = 2
        // RecursiveEven(2) = 4
        // RecursiveEven(3) = 6

        return 0;
    }

    static int IterativeEven(int n)
    {
        // TODO:
        // Om n <= 0, kasta ArgumentException.
        // Använd en for-loop för att räkna fram det n:te jämna talet.
        //
        // Exempel:
        // IterativeEven(1) = 2
        // IterativeEven(2) = 4
        // IterativeEven(3) = 6

        return 0;
    }

    static int FactorialRecursive(int n)
    {
        // TODO:
        // Fakultet:
        // 5! = 5 * 4 * 3 * 2 * 1 = 120
        //
        // Om n < 0, kasta ArgumentException.
        // Om n == 0 eller n == 1, returnera 1.
        // Annars returnera n * FactorialRecursive(n - 1).

        return 0;
    }

    static int SumRecursive(int n)
    {
        // TODO:
        // Summera alla tal från 1 till n med rekursion.
        //
        // SumRecursive(5)
        // = 5 + 4 + 3 + 2 + 1
        // = 15

        return 0;
    }

    static int SumIterative(int n)
    {
        // TODO:
        // Summera alla tal från 1 till n med en loop.

        return 0;
    }

    static int FibonacciRecursive(int n)
    {
        // TODO:
        // Fibonacci:
        // 0, 1, 1, 2, 3, 5, 8, 13 ...
        //
        // Om n < 0, kasta ArgumentException.
        // Om n == 0, returnera 0.
        // Om n == 1, returnera 1.
        // Annars returnera FibonacciRecursive(n - 1) + FibonacciRecursive(n - 2).

        return 0;
    }

    static int FibonacciIterative(int n)
    {
        // TODO:
        // Implementera Fibonacci med loop.
        // Denna version ska vara mer minnesvänlig än den rekursiva.

        return 0;
    }

    static int RecursiveOddWithTrace(int n)
    {
        Console.WriteLine($"Anropar RecursiveOddWithTrace({n})");

        if (n == 1)
        {
            Console.WriteLine("Basfall nått. Returnerar 1.");
            return 1;
        }

        int result = RecursiveOddWithTrace(n - 1) + 2;

        Console.WriteLine($"RecursiveOddWithTrace({n}) returnerar {result}");

        return result;
    }

    static int RecursiveOddWithDepth(int n, int depth)
    {
        string indentation = new string(' ', depth * 2);

        Console.WriteLine($"{indentation}RecursiveOddWithDepth({n})");

        // TODO:
        // Lägg till basfall: om n == 1, skriv ut med indrag att basfallet nåtts och returnera 1.
        // Annars: anropa RecursiveOddWithDepth(n - 1, depth + 1) rekursivt.
        // Spara resultatet, skriv ut med indrag vad metoden returnerar, och returnera resultatet.
        // Jämför utskriften med RecursiveOddWithTrace — vad tillför indraget?

        return 0;
    }

    // ============================================================
    // DEL 10 - FILHANTERING, EXTRA
    // ============================================================

    static void SaveLogToFile()
    {
        // TODO:
        // Kontrollera om logMessages är tom — skriv meddelande om den är det.
        // Annars: spara alla loggmeddelanden till en fil som heter "logg.txt".
        // Skriv ut hur många rader som sparades och var filen finns.
        //
        // Tips:
        // File.WriteAllLines("logg.txt", logMessages);
        // Console.WriteLine($"Sparade {logMessages.Count} rader till logg.txt");

        Console.WriteLine("TODO: Implementera SaveLogToFile.");
    }

    #endregion
}
