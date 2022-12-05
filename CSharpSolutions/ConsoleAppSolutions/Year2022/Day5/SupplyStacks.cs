namespace ConsoleAppSolutions.Year2022.Day5
{
    public static class SupplyStacks
    {
        public static void PlayForStar1(bool useExampleInput = false)
        {
            var textFile = Play2022Solutions.GetInputFile(5, useExampleInput);

            if (File.Exists(textFile))
            {
                using (StreamReader file = new StreamReader(textFile))
                {
                    var allText = file.ReadToEnd();
                    var parts = allText.Split("\r\n\r\n");

                    var crateStacks = parts[0].Split("\r\n");

                    var stacks = crateStacks.Last()
                        .Where(s => !string.IsNullOrWhiteSpace(s.ToString()))
                        .ToDictionary(s => int.Parse(s.ToString()), _ => new Stack<string>());

                    foreach (var crateColumn in crateStacks.Except(new List<string> { crateStacks.Last() }).Reverse())
                    {
                        foreach (var stack in stacks.Keys)
                        {
                            var startIndex = (stack - 1) * 4;
                            var endIndex = stack * 4 - 1;
                            var crate = crateColumn.Substring(startIndex, 3);
                            if (!string.IsNullOrWhiteSpace(crate))
                            {
                                stacks[stack].Push(crate.Trim());
                            }
                        }
                    }

                    foreach (var command in parts[1].Split("\r\n"))
                    {
                        var instructions = command.Split(' ')
                            .Where(c => int.TryParse(c, out _))
                            .Select(c => int.Parse(c))
                            .ToArray();

                        var from = instructions[1];
                        var to = instructions[2];
                        for (var i = 0; i < instructions[0]; i++)
                        {
                            stacks[to].Push(stacks[from].Pop());
                        }

                        Console.WriteLine($"{to}: {stacks[to].Peek()}");
                    }

                    file.Close();

                    Console.WriteLine($"star 1 result: {string.Join(' ', stacks.Values.Select(v => v.Peek()))}");
                }
            }
        }

        public static void PlayForStar2(bool useExampleInput = false)
        {
            var textFile = Play2022Solutions.GetInputFile(5, useExampleInput);

            if (File.Exists(textFile))
            {
                using (StreamReader file = new StreamReader(textFile))
                {
                    var allText = file.ReadToEnd();
                    var parts = allText.Split("\r\n\r\n");

                    var crateStacks = parts[0].Split("\r\n");

                    var stacks = crateStacks.Last()
                        .Where(s => !string.IsNullOrWhiteSpace(s.ToString()))
                        .ToDictionary(s => int.Parse(s.ToString()), _ => new Stack<string>());

                    foreach (var crateColumn in crateStacks.Except(new List<string> { crateStacks.Last() }).Reverse())
                    {
                        foreach (var stack in stacks.Keys)
                        {
                            var startIndex = (stack - 1) * 4;
                            var endIndex = stack * 4 - 1;
                            var crate = crateColumn.Substring(startIndex, 3);
                            if (!string.IsNullOrWhiteSpace(crate))
                            {
                                stacks[stack].Push(crate.Trim());
                            }
                        }
                    }

                    foreach (var command in parts[1].Split("\r\n"))
                    {
                        var instructions = command.Split(' ')
                            .Where(c => int.TryParse(c, out _))
                            .Select(c => int.Parse(c))
                            .ToArray();

                        var from = instructions[1];
                        var to = instructions[2];

                        var betweenStack = new Stack<string>();
                        for (var i = 0; i < instructions[0]; i++)
                        {
                            betweenStack.Push(stacks[from].Pop());
                        }

                        foreach (var crate in betweenStack)
                        {
                            stacks[to].Push(crate);
                        }

                        Console.WriteLine($"{to}: {stacks[to].Peek()}");
                    }

                    file.Close();

                    Console.WriteLine($"star 1 result: {string.Join(' ', stacks.Values.Select(v => v.Peek()))}");
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
