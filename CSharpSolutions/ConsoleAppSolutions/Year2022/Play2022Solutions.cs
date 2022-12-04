using ConsoleAppSolutions.Year2022.Day1;
using ConsoleAppSolutions.Year2022.Day2;
using ConsoleAppSolutions.Year2022.Day3;

namespace ConsoleAppSolutions.Year2022
{
    public static class Play2022Solutions
    {
        private static string _basepathForInputs = @"C:\Code\for fun\AdventOfCode\CSharpSolutions\ConsoleAppSolutions\Year2022";
        
        public static string ExampleInputFileForDay(int day) => @$"{_basepathForInputs}\Day{day}\InputExample.txt";
        
        public static string ActualInputFileForDay(int day) => @$"{_basepathForInputs}\Day{day}\InputActual.txt";

        public static string GetInputFile(int day, bool useExampleInput = false)
        {
            return useExampleInput ? ExampleInputFileForDay(day) : ActualInputFileForDay(day);
        }

        public static void PlayByDay(int day)
        {
            switch (day)
            {
                case 1:
                    CalorieCounting.Play();
                    break;
                case 2:
                    RockPaperScissors.PlayForStar1();
                    RockPaperScissors.PlayForStar2();
                    break;
                case 3:
                    RucksackReorganization.PlayForStar1();
                    RucksackReorganization.PlayForStar2();
                    break;
                case 4:
                    CampCleanup.PlayForStar1();
                    CampCleanup.PlayForStar2();
                    break;
            }
        }
    }
}
