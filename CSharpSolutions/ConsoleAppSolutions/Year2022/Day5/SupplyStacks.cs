namespace ConsoleAppSolutions.Year2022.Day5
{
    public class SupplyStacks : DayQuizBase
    {
        public override int Year => 2022;
        public override int Day => 5;

        public override void PlayForStar1(bool useExampleInput = false)
        {
            var (crateBuilding, stackingCommands) = GetCleanedInput(useExampleInput);
            var stacks = BuildCrateStacks(crateBuilding);

            foreach (var command in stackingCommands)
            {
                var (countOfCratesToMove, from, to) = ReadInstructions(command);

                for (var i = 0; i < countOfCratesToMove; i++)
                {
                    stacks[to].Push(stacks[from].Pop());
                }

                Console.WriteLine($"{to}: {stacks[to].Peek()}");
            }

            Console.WriteLine($"star 1 result: {string.Join(' ', stacks.Values.Select(v => v.Peek()))}");
        }

        public override void PlayForStar2(bool useExampleInput = false)
        {
            var (crateBuilding, stackingCommands) = GetCleanedInput(useExampleInput);
            var stacks = BuildCrateStacks(crateBuilding);

            foreach (var command in stackingCommands)
            {
                var (countOfCratesToMove, from, to) = ReadInstructions(command);

                var tempStack = new Stack<string>();
                for (var i = 0; i < countOfCratesToMove; i++)
                {
                    tempStack.Push(stacks[from].Pop());
                }

                foreach (var crate in tempStack)
                {
                    stacks[to].Push(crate);
                }

                Console.WriteLine($"{to}: {stacks[to].Peek()}");
            }

            Console.WriteLine($"star 2 result: {string.Join(' ', stacks.Values.Select(v => v.Peek()))}");
        }

        private (string crateBuilding, string[] stackingCommands) GetCleanedInput(bool useExampleInput)
        {
            var allText = GetInputTextComplete(useExampleInput);
            var parts = allText.Split("\r\n\r\n");

            return (parts[0], parts[1].Split("\r\n"));
        }

        private static Dictionary<int, Stack<string>> BuildCrateStacks(string crateStackText)
        {
            var crateStacks = crateStackText.Split("\r\n");

            var stacks = crateStacks.Last()
                .Where(s => !string.IsNullOrWhiteSpace(s.ToString()))
                .ToDictionary(s => int.Parse(s.ToString()), _ => new Stack<string>());

            foreach (var crateColumn in crateStacks.Except(new List<string> { crateStacks.Last() }).Reverse())
            {
                foreach (var stack in stacks.Keys)
                {
                    var startIndex = (stack - 1) * 4;
                    var crate = crateColumn.Substring(startIndex, 3);
                    if (!string.IsNullOrWhiteSpace(crate))
                    {
                        stacks[stack].Push(crate.Trim());
                    }
                }
            }

            return stacks;
        }

        private static (int countOfCratesToMove, int moveCrateFromStack, int moveCrateToStack) ReadInstructions(string command)
        {
            var instructions = command.Split(' ')
                .Where(c => int.TryParse(c, out _))
                .Select(int.Parse)
                .ToArray();

            return (instructions[0], instructions[1], instructions[2]);
        }
    }
}
