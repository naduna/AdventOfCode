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
                var allToSecondLast = line[..^2];
                var exactlySecondLast = line[^2];
                var firstCompartment = line[..half];
                var secondCompartment = line[half..];

                var duplicateAlt = firstCompartment.Intersect(secondCompartment);
                var duplicate = firstCompartment.FirstOrDefault(secondCompartment.Contains);
                priorityOfDuplicateItemPerRucksack.Add(GetPriority(duplicate));
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
                    var duplicate = line.FirstOrDefault(i => rucksackPerGroup.All(r => r.Contains(i)));
                    priorityOfBadgesPerGroup.Add(GetPriority(duplicate));

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

        private static int GetPriority(char duplicate)
        {
            var priority = char.IsLower(duplicate) ? duplicate - 96 : duplicate - 38;
            Console.WriteLine($"{duplicate}: {priority}");

            return priority;
        }
    }
}
