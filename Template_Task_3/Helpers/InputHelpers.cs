namespace Template_Task_3.Helpers;

// ============================================================
// HJÄLPMETODER, VALFRITT ATT ANVÄNDA
// ============================================================
public static class InputHelpers
{
    public static int ReadInt(string message)
    {
        do
        {
            Console.Write(message);
            string input = Console.ReadLine() ?? string.Empty;

            if (int.TryParse(input, out int result))
            {
                return result;
            }

            Console.WriteLine("Du måste skriva ett heltal.");
        }
        while (true);

    }

    public static decimal ReadDecimal(string message)
    {
        do
        {
            Console.Write(message);
            string input = Console.ReadLine() ?? string.Empty;

            if (decimal.TryParse(input, out decimal result))
            {
                return result;
            }

            Console.WriteLine("Du måste skriva ett tal.");
        }
        while (true);

    }

    public static string ReadString(string message)
    {
        do
        {
            Console.Write(message);
            string input = Console.ReadLine() ?? string.Empty;

            if (!string.IsNullOrWhiteSpace(input))
            {
                return input.Trim();
            }

            Console.WriteLine("Du måste skriva något.");
        }
        while (true);

    }
}
