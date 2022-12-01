using ConsoleAppSolutions.Day1;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        CalorieCounting.GetElfWithMostCalories(useExampleInput: true);
        CalorieCounting.GetElfWithMostCalories(useExampleInput: false);
    }
}