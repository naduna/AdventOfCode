namespace ConsoleAppSolutions.Day1
{
    internal class CalorieCounting
    {
        static readonly string exampleInput = @"C:\Code\for fun\AdventOfCode\CSharpSolutions\ConsoleAppSolutions\Day1\Star1\InputExample.txt";
        static readonly string actualInput = @"C:\Code\for fun\AdventOfCode\CSharpSolutions\ConsoleAppSolutions\Day1\Star1\InputActual.txt";

        public static void GetElfWithMostCalories(bool useExampleInput)
        {
            var textFile = useExampleInput ? exampleInput : actualInput;

            if (File.Exists(textFile))
            {
                using (StreamReader file = new StreamReader(textFile))
                {
                    int elfCounter = 1;
                    string ln;

                    var elfNumberWithItems = new Dictionary<int, List<int>>();
                    elfNumberWithItems.Add(elfCounter, new List<int>());

                    while ((ln = file.ReadLine()) != null)
                    {
                        if (string.IsNullOrEmpty(ln))
                        {
                            Console.WriteLine(elfNumberWithItems[elfCounter].Sum());

                            elfCounter++;
                            elfNumberWithItems.Add(elfCounter, new List<int>());
                        }
                        else
                        {
                            elfNumberWithItems[elfCounter].Add(int.Parse(ln));
                        }
                    }

                    file.Close();
                    Console.WriteLine($"File has {elfCounter} elves.");

                    foreach (var elf in elfNumberWithItems.Select(e => e.Value.Sum()).OrderBy(e => e))
                    {
                        Console.WriteLine(elf);
                    }

                    var result = elfNumberWithItems.MaxBy(i => i.Value.Sum());
                    Console.WriteLine($"Elf {result.Key} has the most calories: {result.Value.Sum()}");

                    var resultOfTopThree = elfNumberWithItems.Select(e => e.Value.Sum()).OrderByDescending(e => e).Take(3);
                    Console.WriteLine($"Top three elves have in total calories: {resultOfTopThree.Sum()}");
                }
            }
        }
    }
}
