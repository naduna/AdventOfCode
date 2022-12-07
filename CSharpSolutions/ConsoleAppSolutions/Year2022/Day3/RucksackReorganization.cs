namespace ConsoleAppSolutions.Year2022.Day3
{
    public class RucksackReorganization : DayQuizBase
    {
        public override int Year => 2022;
        public override int Day => 3;

        public override void PlayForStar1(bool useExampleInput = false)
        {
            var lines = GetInputTextByLine(useExampleInput);

            var priorityOfDuplicateItemPerRucksack = new List<int>();
            foreach (var line in lines)
            {
                var half = line.Length / 2;
                var firstCompartment = line[..half];
                var secondCompartment = line[half..];
                var duplicate = firstCompartment.FirstOrDefault(f => secondCompartment.Contains(f, StringComparison.Ordinal));

                if (!string.IsNullOrEmpty(duplicate.ToString()))
                {
                    var isLowerCase = duplicate.ToString().Equals(duplicate.ToString().ToLower(), StringComparison.Ordinal);
                    var priority = isLowerCase ? duplicate - 96 : duplicate - 38;
                    priorityOfDuplicateItemPerRucksack.Add(priority);

                    Console.WriteLine($"{duplicate}: {priority}");
                }
            }

            var result = priorityOfDuplicateItemPerRucksack.Sum();
            Console.WriteLine($"star 1 result: {result}");
        }

        public override void PlayForStar2(bool useExampleInput = false)
        {
            var lines = GetInputTextByLine(useExampleInput);

            var priorityOfBadgesPerGroup = new List<int>();
            var groupCounter = 0;
            var rucksackPerGroup = new List<string>();

            foreach (var line in lines)
            {
                groupCounter++;

                if (groupCounter % 3 == 0)
                {
                    var duplicate = line.FirstOrDefault(i => rucksackPerGroup.All(r => r.Contains(i, StringComparison.Ordinal)));

                    var isLowerCase = duplicate.ToString().Equals(duplicate.ToString().ToLower(), StringComparison.Ordinal);
                    var priority = isLowerCase ? duplicate - 96 : duplicate - 38;
                    priorityOfBadgesPerGroup.Add(priority);

                    Console.WriteLine($"{duplicate}: {priority}");
                    groupCounter = 0;
                    rucksackPerGroup.Clear();
                }
                else
                {
                    rucksackPerGroup.Add(line);
                }
            }

            var result = priorityOfBadgesPerGroup.Sum();
            Console.WriteLine($"star 2 result: {result}");
        }
    }
}
