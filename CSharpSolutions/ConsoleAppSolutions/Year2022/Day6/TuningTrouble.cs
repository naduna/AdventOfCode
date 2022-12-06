namespace ConsoleAppSolutions.Year2022.Day6
{
    public static class TuningTrouble
    {
        public static void PlayForStar1(bool useExampleInput = false)
        {
            var textFile = Play2022Solutions.GetInputFile(6, useExampleInput);

            if (File.Exists(textFile))
            {
                using (StreamReader file = new StreamReader(textFile))
                {
                    var allText = file.ReadToEnd();

                    var codes = new Queue<char>();
                    int currentIndex = 0;

                    for (; currentIndex < allText.Length; currentIndex++)
                    {
                        var current = allText[currentIndex];
                        Console.WriteLine(current);

                        if (codes.Count == 4)
                        {
                            if (codes.Distinct().Count() == 4)
                            {
                                break;
                            }
                            else
                            {
                                codes.Dequeue();
                            }

                            Console.WriteLine(string.Join(", ", codes));
                        }

                        codes.Enqueue(current);
                    }

                    file.Close();

                    Console.WriteLine($"star 1 result: {currentIndex}");
                }
            }
        }

        public static void PlayForStar2(bool useExampleInput = false)
        {
            var textFile = Play2022Solutions.GetInputFile(6, useExampleInput);

            if (File.Exists(textFile))
            {
                using (StreamReader file = new StreamReader(textFile))
                {
                    var allText = file.ReadToEnd();

                    var codes = new Queue<char>();
                    int currentIndex = 0;

                    for (; currentIndex < allText.Length; currentIndex++)
                    {
                        var current = allText[currentIndex];
                        Console.WriteLine(current);

                        if (codes.Count == 14)
                        {
                            if (codes.Distinct().Count() == 14)
                            {
                                break;
                            }
                            else
                            {
                                codes.Dequeue();
                            }

                            Console.WriteLine(string.Join(", ", codes));
                        }

                        codes.Enqueue(current);
                    }

                    file.Close();

                    Console.WriteLine($"star 2 result: {currentIndex}");
                }
            }
        }

        private static (int from, int to) GetRange(string input)
        {
            var numbers = input.Split('-').Select(s => int.Parse(s));
            return (numbers.First(), numbers.Last());
        }

        private static bool GetIsContained((int from, int to) toBeContained, (int from, int to) container)
        {
            return toBeContained.from >= container.from && toBeContained.to <= container.to;
        }

        private static bool GetIsOverlapping((int from, int to) toBeContained, (int from, int to) container)
        {
            return toBeContained.from <= container.to && toBeContained.to >= container.from;
        }
    }
}
