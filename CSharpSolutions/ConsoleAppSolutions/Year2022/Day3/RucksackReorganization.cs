namespace ConsoleAppSolutions.Year2022.Day3
{
    public static class RucksackReorganization
    {
        public static void PlayForStar1(bool useExampleInput = false)
        {
            var textFile = Play2022Solutions.GetInputFile(3, useExampleInput);

            if (File.Exists(textFile))
            {
                using (StreamReader file = new StreamReader(textFile))
                {
                    string ln;

                    var priorityOfDuplicateItemPerRucksack = new List<int>();

                    while ((ln = file.ReadLine()) != null)
                    {
                        var half = ln.Length / 2;
                        var firstCompartment = ln.Substring(0, half);
                        var secondCompartment = ln.Substring(half);
                        var duplicate = firstCompartment.FirstOrDefault(f => secondCompartment.Contains(f, StringComparison.Ordinal));

                        if (!string.IsNullOrEmpty(duplicate.ToString()))
                        {
                            var isLowerCase = duplicate.ToString().Equals(duplicate.ToString().ToLower(), StringComparison.Ordinal);
                            var priority = isLowerCase ? duplicate - 96 : duplicate - 38;
                            priorityOfDuplicateItemPerRucksack.Add(priority);

                            Console.WriteLine($"{duplicate}: {priority}");
                        }
                    }

                    file.Close();

                    var result = priorityOfDuplicateItemPerRucksack.Sum();
                    Console.WriteLine($"star 1 result: {result}");
                }
            }
        }

        public static void PlayForStar2(bool useExampleInput = false)
        {
            var textFile = Play2022Solutions.GetInputFile(3, useExampleInput);

            if (File.Exists(textFile))
            {
                using (StreamReader file = new StreamReader(textFile))
                {
                    string ln;

                    var priorityOfBadgesPerGroup = new List<int>();
                    var groupCounter = 0;
                    var rucksackPerGroup = new List<string>();

                    while ((ln = file.ReadLine()) != null)
                    {
                        groupCounter++;

                        if (groupCounter % 3 == 0)
                        {
                            var duplicate = ln.FirstOrDefault(i => rucksackPerGroup.All(r => r.Contains(i, StringComparison.Ordinal)));

                            var isLowerCase = duplicate.ToString().Equals(duplicate.ToString().ToLower(), StringComparison.Ordinal);
                            var priority = isLowerCase ? duplicate - 96 : duplicate - 38;
                            priorityOfBadgesPerGroup.Add(priority);
                            
                            Console.WriteLine($"{duplicate}: {priority}");
                            groupCounter = 0;
                            rucksackPerGroup.Clear();
                        }
                        else
                        {
                            rucksackPerGroup.Add(ln);
                        }
                    }

                    file.Close();

                    var result = priorityOfBadgesPerGroup.Sum();
                    Console.WriteLine($"star 2 result: {result}");
                }
            }
        }
    }
}
