namespace ConsoleAppSolutions.Year2022.Day1
{
    public class CalorieCounting : DayQuizBase
    {
        public override int Year => 2022;
        public override int Day => 1;

        public override void PlayForStar1(bool useExampleInput = false)
        {
            var elfNumberWithItems = GetElvesWithItems(useExampleInput);

            var resultOfTopThree = elfNumberWithItems.Select(e => e.Value.Sum()).OrderByDescending(e => e).Take(3);
            Console.WriteLine($"Top three elves have in total calories: {resultOfTopThree.Sum()}");
        }

        public override void PlayForStar2(bool useExampleInput = false)
        {
            var elfNumberWithItems = GetElvesWithItems(useExampleInput);

            var result = elfNumberWithItems.MaxBy(i => i.Value.Sum());
            Console.WriteLine($"Elf {result.Key} has the most calories: {result.Value.Sum()}");
        }

        private Dictionary<int, List<int>> GetElvesWithItems(bool useExampleInput)
        {
            var lines = GetInputTextByLine(useExampleInput);

            var elfCounter = 1;
            var elfNumberWithItems = new Dictionary<int, List<int>> { { elfCounter, new List<int>() } };

            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line))
                {
                    Console.WriteLine(elfNumberWithItems[elfCounter].Sum());

                    elfCounter++;
                    elfNumberWithItems.Add(elfCounter, new List<int>());
                }
                else
                {
                    elfNumberWithItems[elfCounter].Add(int.Parse(line));
                }
            }

            Console.WriteLine($"File has {elfCounter} elves.");

            foreach (var elf in elfNumberWithItems.Select(e => e.Value.Sum()).OrderBy(e => e))
            {
                Console.WriteLine(elf);
            }

            return elfNumberWithItems;
        }
    }
}
