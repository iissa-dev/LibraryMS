namespace LibraryMS.Application.Common.Interfaces;

public interface ICodeGeneratorService
{
    string GenerateIsbn();
    string GenerateLibraryCardNumber();
    string GenerateEmployeeNumber();
    string GenerateSerialNumber(string isbn);
}