namespace LibraryMS.Infrastructure.Services;

public class CodeGeneratorService : ICodeGeneratorService
{
    private readonly Random random = new();
    public string GenerateEmployeeNumber()
    {
        var year = DateTime.UtcNow.Year;
        return $"{year}-{Guid.NewGuid().ToString()[..8]}";
    }

    public string GenerateIsbn()
    {
        // For more info visit https://en.wikipedia.org/wiki/ISBN
        // Global prefix, so far 978 or 979 have been made availabe by GS1
        var prefix = "978";

        // the registration group element (language-sharing country group, individual country or territory)
        var group = random.Next(1, 10).ToString();

        // Publisher code (2 digits)
        var publisher = random.Next(10, 100).ToString();

        // Title (5 digits) 
        var title = random.Next(100000, 1000000).ToString();

        string first12Digits = $"{prefix}{group}{publisher}{title}";
        var checkDigit = CheckISBN(first12Digits);

        return $"{prefix}-{group}-{publisher}-{title}-{checkDigit}";
    }

    public string GenerateLibraryCardNumber()
    {
        var year = DateTime.UtcNow.Year;
        return $"{year}-{Guid.NewGuid().ToString()[..8]}";
    }

    public string GenerateSerialNumber(string isbn)
    {
        // Generate a unique serial number for the copy
        return $"{isbn}-{Guid.NewGuid().ToString()[..8].ToUpper()}";
    }

    private static int CheckISBN(string digits)
    {
        int sum = 0;
        for (int i = 0; i < 12; i++)
        {
            int digit = digits[i] - '0';

            sum += (i % 2 == 0) ? digit * 1 : digit * 3;
        }

        int remainder = sum % 10;
        return (remainder == 0) ? 0 : 10 - remainder;
    }
}