namespace ConsoleAppSolutions.Year2022
{
    public abstract class DayQuizBase
    {
        private const string AbsoluteBasePathForInputs = @"C:\Code\for fun\AdventOfCode\CSharpSolutions\ConsoleAppSolutions\Year{0}\Day{1}\Input\{2}";
        private const string ExampleFile = "Example.txt";
        private const string ActualFile = "Actual.txt";

        public abstract int Year { get; }

        public abstract int Day { get; }

        public abstract void PlayForStar1(bool useExampleInput = false);

        public abstract void PlayForStar2(bool useExampleInput = false);

        public void PlayBothStars()
        {
            PlayForStar1();
            PlayForStar2();
        }

        protected string GetInputTextComplete(bool useExampleInput = false)
        {
            return File.ReadAllText(GetInputFile(useExampleInput));
        }

        protected string[] GetInputTextByLine(bool useExampleInput = false)
        {
            return File.ReadAllLines(GetInputFile(useExampleInput));
        }

        private string GetInputFile(bool useExampleInput = false)
        {
            var file = useExampleInput ? ExampleFile : ActualFile;
            return string.Format(AbsoluteBasePathForInputs, Year, Day, file);
        }
    }
}
