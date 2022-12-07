namespace ConsoleAppSolutions.Year2022.Day2
{
    public class RockPaperScissors : DayQuizBase
    {
        public override int Year => 2022;
        public override int Day => 2;

        public override void PlayForStar1(bool useExampleInput = false)
        {
            var lines = GetInputTextByLine(useExampleInput);

            var scoresPerRound = lines
                .Select(line => GetScoreForRound(line, (myInput, _) => GetActionFromInput(myInput)))
                .ToList();

            var result = scoresPerRound.Sum();
            Console.WriteLine($"star 1 result: {result}");
        }

        public override void PlayForStar2(bool useExampleInput = false)
        {
            var lines = GetInputTextByLine(useExampleInput);

            var scoresPerRound = lines
                .Select(line => GetScoreForRound(line, GetNeededPlayFromInput))
                .ToList();

            var result = scoresPerRound.Sum();
            Console.WriteLine($"star 2 result: {result}");
        }

        private static int GetScoreForRound(string line, Func<char, RockPaperScissorsAction, RockPaperScissorsAction> getMyActionFromInputAndOpponentAction)
        {
            Console.WriteLine(line);

            var opponentInput = line.First(c => c is 'A' or 'B' or 'C');
            var other = GetActionFromInput(opponentInput);

            var myInput = line.First(c => c is 'X' or 'Y' or 'Z');
            var me = getMyActionFromInputAndOpponentAction(myInput, other);

            return GetScore(me) + GetWin(me, other);
        }

        private static RockPaperScissorsAction GetActionFromInput(char input)
        {
            switch (input)
            {
                case 'A':
                case 'X':
                    return RockPaperScissorsAction.Rock;
                case 'B':
                case 'Y':
                    return RockPaperScissorsAction.Paper;
                case 'C':
                case 'Z':
                    return RockPaperScissorsAction.Scissors;
            }

            return RockPaperScissorsAction.Rock;
        }

        private static RockPaperScissorsAction GetNeededPlayFromInput(char input, RockPaperScissorsAction other)
        {
            switch (input)
            {
                // lose
                case 'X':
                    return Enum.GetValues<RockPaperScissorsAction>().First(me => GetWin(me, other) == 0);
                // draw
                case 'Y':
                    return Enum.GetValues<RockPaperScissorsAction>().First(me => GetWin(me, other) == 3);
                // win
                case 'Z':
                    return Enum.GetValues<RockPaperScissorsAction>().First(me => GetWin(me, other) == 6);
            }

            return RockPaperScissorsAction.Rock;
        }

        private static int GetScore(RockPaperScissorsAction e)
        {
            switch (e)
            {
                case RockPaperScissorsAction.Rock:
                    return 1;
                case RockPaperScissorsAction.Paper:
                    return 2;
                case RockPaperScissorsAction.Scissors:
                    return 3;
            }

            return 0;
        }

        private static int GetWin(RockPaperScissorsAction me, RockPaperScissorsAction other)
        {
            if (me == RockPaperScissorsAction.Rock && other == RockPaperScissorsAction.Paper)
            {
                return 0;
            }

            if (me < other || me == RockPaperScissorsAction.Paper && other == RockPaperScissorsAction.Rock)
            {
                return 6;
            }

            if (me == other)
            {
                return 3;
            }

            return 0;
        }

        private enum RockPaperScissorsAction
        {
            Rock,
            Scissors,
            Paper,
        }
    }
}
