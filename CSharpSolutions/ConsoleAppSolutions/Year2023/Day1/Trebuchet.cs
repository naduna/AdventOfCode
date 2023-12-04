namespace ConsoleAppSolutions.Year2023.Day1
{
    public class Trebuchet : DayQuizWithDifferentInputPerStarBase
    {
        public override int Year => 2023;
        public override int Day => 1;

        public override void PlayForStar1(bool useExampleInput = false)
        {
            var lines = GetInputTextByLine(1, useExampleInput);

            var counter = 0;

            foreach (var line in lines)
            {
                int i = 0;
                while (i < line.Length && !char.IsNumber(line[i]))
                {
                    i++;
                }

                int j = line.Length - 1;
                while (j >= 0 && !char.IsNumber(line[j]))
                {
                    j--;
                }

                if (int.TryParse(line[i].ToString() + line[j], out var number))
                {
                    counter += number;
                }
            }

            Console.WriteLine($"1: sum of all of the calibration values: {counter}.");
        }

        public override void PlayForStar2(bool useExampleInput = false)
        {
            var lines = GetInputTextByLine(2, useExampleInput);

            var counter = 0;

            foreach (var line in lines)
            {
                var number = GetCalibrationValue(line);

                counter += number;
            }

            Console.WriteLine($"2: sum of all of the calibration values: {counter}.");
        }

        private static readonly Dictionary<int, string> NumberValuePairs = new()
        {
            { 1, "one" },
            { 2, "two" },
            { 3, "three" },
            { 4, "four" },
            { 5, "five" },
            { 6, "six" },
            { 7, "seven" },
            { 8, "eight" },
            { 9, "nine" },
        };

        private int GetCalibrationValue(string line)
        {
            var numbersWithIndices = NumberValuePairs.Keys.Select(s =>
                {
                    if (!line.Contains(NumberValuePairs[s]) && !line.Contains(s.ToString()))
                    {
                        return null;
                    }

                    var indices = new List<int>();

                    int i = 0;
                    while (i > -1 && i < line.Length)
                    {
                        var index = line.IndexOf(NumberValuePairs[s], i);
                        if (index > -1 && index < line.Length)
                        {
                            indices.Add(index);
                        }
                        else
                        {
                            break;
                        }

                        i = index + 1;
                    }

                    i = 0;
                    while (i > -1 && i < line.Length)
                    {
                        var index = line.IndexOf(s.ToString(), i);
                        if (index > -1 && index < line.Length)
                        {
                            indices.Add(index);
                        }
                        else
                        {
                            break;
                        }

                        i = index + 1;
                    }
                    
                    return new
                    {
                        Key = s,
                        Indices = indices,
                    };
                })
                .Where(s => s != null)
                .ToList();

            var min = numbersWithIndices.MinBy(s => s.Indices.Min());
            var max = numbersWithIndices.MaxBy(s => s.Indices.Max());

            return int.Parse(min.Key.ToString() + max.Key);
        }
    }
}
