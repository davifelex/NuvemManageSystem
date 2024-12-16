

using NuvemManageSystem.SupportFunctions;

class Program
{
    static void Main()
    {
        InputFunctions input = new InputFunctions();
        Console.WriteLine("--------------{Nuvem Manage System}--------------");
        string? opt = input.InputString(false, "Teste: ", "Caracter");
        Console.WriteLine($"Você digitou: {opt}");
        Console.ReadLine();
    }
}