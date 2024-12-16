

using NuvemManageSystem.SupportFunctions;

class Program
{
    static void Main()
    {
        InputFunctions input = new InputFunctions();
        Console.WriteLine("--------------{Nuvem Manage System}--------------");
        Double? opt = input.InputDouble("Teste: ", "Caracter");
        Console.WriteLine($"Você digitou: {opt *2}");
        Console.ReadLine();
    }
}