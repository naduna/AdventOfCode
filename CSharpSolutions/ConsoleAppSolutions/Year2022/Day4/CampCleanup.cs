namespace ConsoleAppSolutions.Year2022.Day4
{
    public class CampCleanup : DayQuizBase
    {
        public override int Year => 2022;
        public override int Day => 4;

        public override void PlayForStar1(bool useExampleInput = false)
        {
            var countContaining = GetCountOfOverlappingPairsByCondition(useExampleInput, GetIsContained);
            
            Console.WriteLine($"star 1 result: {countContaining}");
        }

        public override void PlayForStar2(bool useExampleInput = false)
        {
            var countOverlapping = GetCountOfOverlappingPairsByCondition(useExampleInput, GetIsOverlapping);

            Console.WriteLine($"star 2 result: {countOverlapping}");
        }

        private int GetCountOfOverlappingPairsByCondition(bool useExampleInput, Func<(int from, int to), (int from, int to), bool> shouldIncreaseFunc)
        {
            var lines = GetInputTextByLine(useExampleInput);

            var countOfPairsWithOverlappingRange = 0;

            foreach (var line in lines)
            {
                var pairs = line.Split(',');
                var first = GetRange(pairs[0]);
                var second = GetRange(pairs[1]);

                if (shouldIncreaseFunc(first, second) || shouldIncreaseFunc(second, first))
                {
                    countOfPairsWithOverlappingRange++;
                }
            }

            return countOfPairsWithOverlappingRange;
        }

        private static (int from, int to) GetRange(string input)
        {
            var numbers = input.Split('-').Select(int.Parse).ToArray();
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
