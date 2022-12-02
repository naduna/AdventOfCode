using ConsoleAppSolutions.Year2022.Day1;
using ConsoleAppSolutions.Year2022.Day2;

namespace ConsoleAppSolutions.Year2022
{
    public class Play2022Solutions
    {
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
            }
        }
    }
}
