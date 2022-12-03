namespace ConsoleAppSolutions.Year2022.Day2
{
    public static class RockPaperScissors
    {
        public static void PlayForStar1(bool useExampleInput = false)
        {
            var textFile = Play2022Solutions.GetInputFile(2, useExampleInput);

            if (File.Exists(textFile))
            {
                using (StreamReader file = new StreamReader(textFile))
                {
                    string ln;

                    var scoresPerRound = new List<int>();

                    while ((ln = file.ReadLine()) != null)
                    {
                        Console.WriteLine(ln);

                        var opponentInput = ln.First(c => c is 'A' or 'B' or 'C');
                        var other = GetEnumFromInput(opponentInput);

                        var myInput = ln.First(c => c is 'X' or 'Y' or 'Z');
                        var me = GetEnumFromInput(myInput);

                        var score = GetScore(me) + GetWin(me, other);
                        scoresPerRound.Add(score);
                    }

                    file.Close();


                    var result = scoresPerRound.Sum();
                    Console.WriteLine($"result: {result}");

                }
            }
        }

        public static void PlayForStar2(bool useExampleInput = false)
        {
            var textFile = Play2022Solutions.GetInputFile(2, useExampleInput);

            if (File.Exists(textFile))
            {
                using (StreamReader file = new StreamReader(textFile))
                {
                    string ln;

                    var scoresPerRound = new List<int>();

                    while ((ln = file.ReadLine()) != null)
                    {
                        Console.WriteLine(ln);

                        var opponentInput = ln.First(c => c is 'A' or 'B' or 'C');
                        var other = GetEnumFromInput(opponentInput);

                        var myInput = ln.First(c => c is 'X' or 'Y' or 'Z');
                        var me = GetNeededPlayFromInput(myInput, other);

                        var score = GetScore(me) + GetWin(me, other);
                        scoresPerRound.Add(score);
                    }

                    file.Close();


                    var result = scoresPerRound.Sum();
                    Console.WriteLine($"result: {result}");

                }
            }
        }

        private static RockPaperScissorsEnum GetEnumFromInput(char input)
        {
            switch (input)
            {
                case 'A':
                case 'X':
                    return RockPaperScissorsEnum.Rock;
                case 'B':
                case 'Y':
                    return RockPaperScissorsEnum.Paper;
                case 'C':
                case 'Z':
                    return RockPaperScissorsEnum.Scissors;
            }

            return RockPaperScissorsEnum.Rock;
        }

        private static RockPaperScissorsEnum GetNeededPlayFromInput(char input, RockPaperScissorsEnum other)
        {
            switch (input)
            {
                // lose
                case 'X':
                    return Enum.GetValues<RockPaperScissorsEnum>().First(me => GetWin(me, other) == 0);
                // draw
                case 'Y':
                    return Enum.GetValues<RockPaperScissorsEnum>().First(me => GetWin(me, other) == 3);
                // win
                case 'Z':
                    return Enum.GetValues<RockPaperScissorsEnum>().First(me => GetWin(me, other) == 6);
            }

            return RockPaperScissorsEnum.Rock;
        }

        private static int GetScore(RockPaperScissorsEnum e)
        {
            switch (e)
            {
                case RockPaperScissorsEnum.Rock:
                    return 1;
                case RockPaperScissorsEnum.Paper:
                    return 2;
                case RockPaperScissorsEnum.Scissors:
                    return 3;
            }

            return 0;
        }

        private static int GetWin(RockPaperScissorsEnum me, RockPaperScissorsEnum other)
        {
            if (me == RockPaperScissorsEnum.Rock && other == RockPaperScissorsEnum.Paper)
            {
                return 0;
            }

            if (me < other || me == RockPaperScissorsEnum.Paper && other == RockPaperScissorsEnum.Rock)
            {
                return 6;
            }

            if (me == other)
            {
                return 3;
            }

            return 0;
        }

        private enum RockPaperScissorsEnum
        {
            Rock,
            Scissors,
            Paper,
        }
    }
}
