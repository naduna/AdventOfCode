namespace ConsoleAppSolutions.Year2022.Day19
{
    public class NotEnoughMinerals : DayQuizBase
    {
        public override int Year => 2022;
        public override int Day => 19;

        public override void PlayForStar1(bool useExampleInput = false)
        {
            var lines = GetInputTextByLine(useExampleInput);

            foreach (var line in lines)
            {
                var parts = line.Split(':');
                var blueprintNumber = int.Parse(parts[0].Replace("Blueprint", "").Trim());
                var robotTypesString = parts[1].Split(".");
                var list = new List<(RobotType, IEnumerable<(int cost, RobotType type)>)>();
                foreach (var s in robotTypesString)
                {
                    var robotTypeWithCost = (GetRobotType(s), GetCostByType(s).ToList());
                    list.Add(robotTypeWithCost);
                    //Console.WriteLine(robotTypeWithCost);
                }

            }

            var result = 0;
            Console.WriteLine($"star 1 result: {result}");
        }

        public override void PlayForStar2(bool useExampleInput = false)
        {
            //var lines = GetInputTextByLine(useExampleInput);

            //var scoresPerRound = lines
            //    .Select(line => GetScoreForRound(line, GetNeededPlayFromInput))
            //    .ToList();

            //var result = scoresPerRound.Sum();
            //Console.WriteLine($"star 2 result: {result}");
        }

        private static RobotType GetRobotType(string input)
        {
            var typeString = input.Split("robot")[0].Replace("Each", "");
            return GetRobotTypeByTypeString(typeString);
        }

        private static RobotType GetRobotTypeByTypeString(string typeString)
        {
            Console.WriteLine("robot type for" + typeString);
            switch (typeString.Trim())
            {
                case "ore":
                    return RobotType.Ore;
                case "clay":
                    return RobotType.Clay;
                case "obsidian":
                    return RobotType.Obsidian;
                case "geode":
                    return RobotType.Geode;
            }

            return RobotType.Ore;
        }

        private static IEnumerable<(int cost, RobotType type)> GetCostByType(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                yield break;
            }
            Console.WriteLine("cost type for" + input);
            var strippedInput = input.Split("robot costs")[1];

            List<string> costs = new List<string>();
            if (strippedInput.Contains("and"))
            {
                costs.AddRange(strippedInput.Split("and"));
            }
            else
            {
                costs.Add(strippedInput);
            }
            foreach (var costString in costs)
            {
                var parts = costString.Trim().Split(" ");
                var cost = int.Parse(parts[0].Trim());
                var type = GetRobotTypeByTypeString(parts[1]);
                Console.WriteLine("cost and robot type for" + input + " --> " + cost + type);
                yield return (cost, type);
            }
        }

        private static RobotType GetNeededPlayFromInput(char input, RobotType other)
        {
            switch (input)
            {
                // lose
                case 'X':
                    return Enum.GetValues<RobotType>().First(me => GetWin(me, other) == 0);
                // draw
                case 'Y':
                    return Enum.GetValues<RobotType>().First(me => GetWin(me, other) == 3);
                // win
                case 'Z':
                    return Enum.GetValues<RobotType>().First(me => GetWin(me, other) == 6);
            }

            return RobotType.Ore;
        }

        private static int GetScore(RobotType e)
        {
            switch (e)
            {
                case RobotType.Ore:
                    return 1;
                case RobotType.Obsidian:
                    return 2;
                case RobotType.Clay:
                    return 3;
            }

            return 0;
        }

        private static int GetWin(RobotType me, RobotType other)
        {
            if (me == RobotType.Ore && other == RobotType.Obsidian)
            {
                return 0;
            }

            if (me < other || me == RobotType.Obsidian && other == RobotType.Ore)
            {
                return 6;
            }

            if (me == other)
            {
                return 3;
            }

            return 0;
        }

        private enum RobotType
        {
            Ore,
            Clay,
            Obsidian,
            Geode
        }
    }
}
