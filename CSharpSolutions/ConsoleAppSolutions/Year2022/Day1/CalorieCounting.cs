namespace ConsoleAppSolutions.Year2022.Day1
{
    internal class CalorieCounting
    {
        public static void Play(bool useExampleInput = false)
        {
            var textFile = Play2022Solutions.GetInputFile(1, useExampleInput);

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
