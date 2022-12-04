namespace ConsoleAppSolutions.Year2022.Day3
{
    public static class CampCleanup
    {
        public static void PlayForStar1(bool useExampleInput = false)
        {
            var textFile = Play2022Solutions.GetInputFile(4, useExampleInput);

            if (File.Exists(textFile))
            {
                using (StreamReader file = new StreamReader(textFile))
                {
                    string ln;

                    var countOfPairsWithOverlappingRange = 0;

                    while ((ln = file.ReadLine()) != null)
                    {
                        var pairs = ln.Split(',');
                        var first = GetRange(pairs[0]);
                        var second = GetRange(pairs[1]);

                        if (GetIsContained(first, second) || GetIsContained(second, first))
                        {
                            countOfPairsWithOverlappingRange++;
                        }
                    }

                    file.Close();

                    Console.WriteLine($"star 1 result: {countOfPairsWithOverlappingRange}");
                }
            }
        }

        public static void PlayForStar2(bool useExampleInput = false)
        {
            var textFile = Play2022Solutions.GetInputFile(4, useExampleInput);

            if (File.Exists(textFile))
            {
                using (StreamReader file = new StreamReader(textFile))
                {
                    string ln;

                    var countOfPairsWithOverlappingRange = 0;

                    while ((ln = file.ReadLine()) != null)
                    {
                        var pairs = ln.Split(',');
                        var first = GetRange(pairs[0]);
                        var second = GetRange(pairs[1]);

                        if (GetIsOverlapping(first, second) || GetIsOverlapping(second, first))
                        {
                            countOfPairsWithOverlappingRange++;
                        }
                    }

                    file.Close();

                    Console.WriteLine($"star 2 result: {countOfPairsWithOverlappingRange}");
                }
            }
        }

        private static (int from, int to) GetRange(string input)
        {
            var numbers = input.Split('-').Select(s => int.Parse(s));
            return (numbers.First(), numbers.Last());
        }

        private static bool GetIsContained((int from, int to) toBeContained, (int from, int to) container)
        {
            return toBeContained.from >= container.from && toBeContained.to <= container.to;
        }

        private static bool GetIsOverlapping((int from, int to) toBeContained, (int from, int to) container)
        {
            return toBeContained.from <= container.to && toBeContained.to >= container.from;
        }
    }
}
