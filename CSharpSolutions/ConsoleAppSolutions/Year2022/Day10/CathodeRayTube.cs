namespace ConsoleAppSolutions.Year2022.Day10
{
    public class CathodeRayTube : DayQuizBase
    {
        public override int Year => 2022;
        public override int Day => 10;

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
            var lines = GetInputTextByLine(useExampleInput);

            var cycle = 0;
            var x = 1;
            var signalStrength = 0;
            var cycleList = new List<(int x, int c)>();
            var signalCycle = new List<int>();

            foreach (var line in lines)
            {
                var cycles = line.Contains("noob") ? 1 : 2;

                for (var i = 0; i < cycles; i++)
                {
                    cycle++;
                    cycleList.Add((x, cycleList.Count + 1));

                    //if ((cycle - 20) % 40 == 0)
                    if (cycle % 40 == 20)
                    {
                        signalCycle.Add(cycle * x);
                        signalStrength += cycle * x;
                    }
                }
                
                if (line.Contains("addx"))
                {
                    var number = line.Split(' ')[1];
                    x += int.Parse(number);
                }
            }

            //var test = cycleList.

            //return signalStrength;
            var cyclesForSignalCheck = cycleList.Where(c => c.c % 40 == 20);
            //var cyclesForSignalCheck = cycleWithCurrentX.Where(c => (c.cycle - 20) % 40 == 0);
            var test = cyclesForSignalCheck.Select(c => c.c * c.x);
            var result = cyclesForSignalCheck.Sum(c => c.c * c.x);

            return result;
        }

        //private int GetIndexForFirstStartOfPacketMarker(bool useExampleInput, int neededConsecutiveMarkers)
        //{
        //    var lines = GetInputTextByLine(useExampleInput);

        //    var cycleWithCurrentX = new List<(int cycle, int x)>();
        //    cycleWithCurrentX.Add((0, 1));

        //    var nextNewX = 1;

        //    foreach (var line in lines)
        //    {
        //        var lastCycle = cycleWithCurrentX.Last();
        //        if (line.Contains("noob"))
        //        {
        //            cycleWithCurrentX.Add((lastCycle.cycle + 1, nextNewX));
        //        }
        //        else if (line.Contains("addx"))
        //        {
        //            var number = line.Split(' ')[1];
        //            cycleWithCurrentX.Add((lastCycle.cycle + 1, nextNewX));
        //            cycleWithCurrentX.Add((lastCycle.cycle + 2, nextNewX));
        //            nextNewX = nextNewX + int.Parse(number);
        //        }
        //    }

        //    var cyclesForSignalCheck = cycleWithCurrentX.Where(c => c.cycle % 40 == 20);
        //    //var cyclesForSignalCheck = cycleWithCurrentX.Where(c => (c.cycle - 20) % 40 == 0);
        //    var result = cyclesForSignalCheck.Sum(c => c.cycle * c.x);

        //    return result;
        //}

        //private int GetIndexForFirstStartOfPacketMarker(bool useExampleInput, int neededConsecutiveMarkers)
        //{
        //    var lines = GetInputTextByLine(useExampleInput);

        //    var cycleWithCurrentX = new Stack<(int cycle, int x)>();
        //    cycleWithCurrentX.Push((0, 1));

        //    var nextNewX = 1;

        //    foreach (var line in lines)
        //    {
        //        var lastCycle = cycleWithCurrentX.Peek();
        //        if (line.Contains("noob"))
        //        {
        //            cycleWithCurrentX.Push((lastCycle.cycle + 1, nextNewX));
        //        }
        //        else if (line.Contains("addx"))
        //        {
        //            var number = line.Split(' ')[1];
        //            cycleWithCurrentX.Push((lastCycle.cycle + 1, nextNewX));
        //            cycleWithCurrentX.Push((lastCycle.cycle + 2, nextNewX));
        //            nextNewX = lastCycle.x + int.Parse(number);
        //        }
        //    }

        //    var cyclesForSignalCheck = cycleWithCurrentX.Where(c => c.cycle % 40 == 20);
        //    //var cyclesForSignalCheck = cycleWithCurrentX.Where(c => (c.cycle - 20) % 40 == 0);
        //    var result = cyclesForSignalCheck.Sum(c => c.cycle * c.x);

        //    return result;
        //}
    }
}
