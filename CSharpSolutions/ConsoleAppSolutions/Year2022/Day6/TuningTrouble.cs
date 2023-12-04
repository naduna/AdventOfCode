namespace ConsoleAppSolutions.Year2022.Day6
{
    public class TuningTrouble : DayQuizBase
    {
        public override int Year => 2022;
        public override int Day => 6;

        public override void PlayForStar1(bool useExampleInput = false)
        {
            var result = GetIndexForFirstStartOfPacketMarker(useExampleInput, 4);
            Console.WriteLine($"star 1 result: {result}");
        }

        public override void PlayForStar2(bool useExampleInput = false)
        {
            var result = GetIndexForFirstStartOfPacketMarker(useExampleInput, 14);
            Console.WriteLine($"star 2 result: {result}");
        }

        private int GetIndexForFirstStartOfPacketMarker(bool useExampleInput, int neededConsecutiveMarkers)
        {
            var allText = GetInputTextComplete(useExampleInput);

            var codes = new Queue<char>();
            var currentIndex = neededConsecutiveMarkers;

            for (; currentIndex < allText.Length; currentIndex++)
            {
                var current = allText[currentIndex];
                Console.WriteLine(current);

                if (codes.Count == neededConsecutiveMarkers)
                {
                    if (codes.Distinct().Count() == neededConsecutiveMarkers)
                    {
                        // possibility to use HashSet: each element can only be contained once
                        break;
                    }

                    codes.Dequeue();
                    Console.WriteLine(string.Join(", ", codes));
                }

                codes.Enqueue(current);
            }

            return currentIndex;
        }
    }
}
