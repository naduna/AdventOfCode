using ConsoleAppSolutions.Year2022;
using ConsoleAppSolutions.Year2022.Day1;
using ConsoleAppSolutions.Year2022.Day2;
using ConsoleAppSolutions.Year2022.Day3;
using ConsoleAppSolutions.Year2022.Day4;
using ConsoleAppSolutions.Year2022.Day5;
using ConsoleAppSolutions.Year2022.Day6;
using ConsoleAppSolutions.Year2022.Day7;

namespace ConsoleAppSolutions
{
    public static class SolutionPlayer
    {
        public static void PlaySolutionsByYearAndDay(int year, int day)
        {
            switch (year)
            {
                case 2022:
                    Play2022SolutionsByDay(day);
                    break;
                default:
                    throw new NotImplementedException(); // not yet :D
            }
        }

        public static void Play2022SolutionsByDay(int day)
        {
            switch (day)
            {
                case 1:
                    PlayBothStars<CalorieCounting>();
                    break;
                case 2:
                    PlayBothStars<RockPaperScissors>();
                    break;
                case 3:
                    PlayBothStars<RucksackReorganization>();
                    break;
                case 4:
                    PlayBothStars<CampCleanup>();
                    break;
                case 5:
                    PlayBothStars<SupplyStacks>();
                    break;
                case 6:
                    PlayBothStars<TuningTrouble>();
                    break;
                case 7:
                    PlayBothStars<NoSpaceLeftOnDevice>();
                    break;
                default:
                    throw new NotImplementedException(); // not yet :D
            }
        }

        private static void PlayBothStars<TDay>() where TDay : DayQuizBase, new()
        {
            new TDay().PlayBothStars();
        }
    }
}
