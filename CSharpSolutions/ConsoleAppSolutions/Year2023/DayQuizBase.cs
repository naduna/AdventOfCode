using ConsoleAppSolutions.Year2022;

namespace ConsoleAppSolutions.Year2023
{
    public abstract class DayQuizWithDifferentInputPerStarBase : DayQuizBase
    {
        private const string AbsoluteBasePathForInputs = @"C:\Code\for fun\AdventOfCode\CSharpSolutions\ConsoleAppSolutions\Year{0}\Day{1}\Input{2}\{3}";
        private const string ExampleFile = "Example.txt";
        private const string ActualFile = "Actual.txt";

        protected string GetInputTextComplete(int starNo, bool useExampleInput = false)
        {
            return File.ReadAllText(GetInputFile(starNo, useExampleInput));
        }

        protected string[] GetInputTextByLine(int starNo, bool useExampleInput = false)
        {
            return File.ReadAllLines(GetInputFile(starNo, useExampleInput));
        }

        private string GetInputFile(int starNo, bool useExampleInput = false)
        {
            var file = useExampleInput ? ExampleFile : ActualFile;
            return string.Format(AbsoluteBasePathForInputs, Year, Day, starNo, file);
        }
    }
}
