namespace ConsoleAppSolutions.Year2022.Day9
{
    public class RopeBridge : DayQuizBase
    {
        public override int Year => 2022;
        public override int Day => 9;

        public override void PlayForStar1(bool useExampleInput = false)
        {
            var lines = GetInputTextByLine(useExampleInput);
            List<(string command, int movementAmount)> commands = lines.Select(l =>
                {
                    var parts = l.Split(' ');
                    return (parts[0], int.Parse(parts[1]));
                })
                .ToList();

            var head = new Position2(2);
            var init = head.CurrentTailPosition;
            foreach (var command in commands)
            {
                head.MoveHead(command);
            }

            var result = head.TailPositions.DistinctBy(t => new { t.X, t.Y }).Count();
            Console.WriteLine($"star 1 result: {result}");
        }

        public override void PlayForStar2(bool useExampleInput = false)
        {
            var lines = GetInputTextByLine(useExampleInput);
            List<(string command, int movementAmount)> commands = lines.Select(l =>
                {
                    var parts = l.Split(' ');
                    return (parts[0], int.Parse(parts[1]));
                })
                .ToList();

            var head = new Position4(10);
            foreach (var command in commands)
            {
                head.MoveHead(command);
            }
            //var head = new Position2(10);
            //foreach (var command in commands)
            //{
            //    head.MoveHead(command);
            //}

            //var lastKnot = head.NextKnot;
            //while (lastKnot?.NextKnot != null)
            //{
            //    lastKnot = lastKnot.NextKnot;
            //}
            //var lastKnot = head.Knots.Single(t => t.KnotNumber == 9);
            var lastKnot = head.CurrentTailPositionWithKnot(9);
            var result = head.TailPositionsWithKnot.DistinctBy(t => new { t.X, t.Y }).Count(p => p.KnotNumber == 9);
            //var lastKnot = head.LastInitialTail;
            //var result = lastKnot.TheseTailPositions.DistinctBy(t => new { t.X, t.Y }).Count();
            //var result = head.Knots.Where(t => t.KnotNumber == 9).DistinctBy(t => new { t.X, t.Y }).Count();
            Console.WriteLine($"star 2 result: {result}");
        }

        private class Position2
        {
            public Position2(int knotCount)
            {
                TotalKnotCount = knotCount;
                X = 0;
                Y = 0;

                for (int k = 1; k < knotCount; k++)
                {
                    TailPositions.Add(new Position2(0, 0, k));
                }
            }

            public Position2(int x, int y, int knotNumber)
            {
                X = x;
                Y = y;
                KnotNumber = knotNumber;
            }

            public int X { get; set; }
            public int Y { get; set; }
            public int KnotNumber { get; set; }
            public int TotalKnotCount { get; set; }

            public Position2 CurrentTailPosition => TailPositions.Last();

            public List<Position2> TailPositions { get; set; } = new List<Position2>();

            private List<string> MovementsSinceLastTailMovement { get; set; } = new List<string>();

            public void MoveHead((string command, int movementAmount) command)
            {
                for (var i = 0; i < command.movementAmount; i++)
                {
                    var oldHeadPosition = (X, Y);
                    switch (command.command)
                    {
                        case "U":
                            Y--;
                            break;
                        case "D":
                            Y++;
                            break;
                        case "L":
                            X--;
                            break;
                        case "R":
                            X++;
                            break;
                    }

                    Console.WriteLine($"Head Position: X {X} . {Y} Y");
                    for (int k = 1; k < TotalKnotCount; k++)
                    {
                        oldHeadPosition = CheckIfNeedToMoveTail(k, oldHeadPosition);
                    }
                }
            }

            private (int x, int y) CheckIfNeedToMoveTail(int knotNumber, (int x, int y) oldHeadPosition)
            {
                //MovementsSinceLastTailMovement.Add(command.command);

                var tail = TailPositions.Last(i => i.KnotNumber == knotNumber);
                var tailBeforeThis = TailPositions.LastOrDefault(i => i.KnotNumber == (knotNumber - 1)) ?? this;
                var x = Math.Abs(tailBeforeThis.X - tail.X);
                var y = Math.Abs(tailBeforeThis.Y - tail.Y);
                var isTailAdjacent = x <= 1 && y <= 1;

                if (isTailAdjacent)
                {
                    return (tail.X, tail.Y);
                }

                MoveTailFromCurrentToNewPosition(oldHeadPosition.x, oldHeadPosition.y, knotNumber);
                return oldHeadPosition;
                //foreach (var moveCommand in MovementsSinceLastTailMovement)
                //{
                //    switch (moveCommand)
                //    {
                //        case "U":
                //            MoveTailFromCurrentToNewPosition(X, Y + 1);
                //            break;
                //        case "D":
                //            MoveTailFromCurrentToNewPosition(X, Y - 1);
                //            break;
                //        case "L":
                //            MoveTailFromCurrentToNewPosition(X + 1, Y);
                //            break;
                //        case "R":
                //            MoveTailFromCurrentToNewPosition(X - 1, Y);
                //            break;
                //    }
                //}

                //MovementsSinceLastTailMovement.Clear();
            }

            private void MoveTailFromCurrentToNewPosition(int newX, int newY, int knotNumber)
            {
                //var tail = CurrentTailPosition;
                //var amountOfXToTraverse = newX - tail.X;
                //var amountOfYToTraverse = newY - tail.Y;

                //for (var x = tail.X; Math.Abs(newX - x) > 1; x = newX - x)
                //{
                //    Console.WriteLine($"test x {x}");
                //}

                if (knotNumber + 1 == TotalKnotCount)
                {
                    Console.WriteLine($"Tail Position: X {newX} . {newY} Y");
                }
                TailPositions.Add(new Position2(newX, newY, knotNumber));
            }

            //private IEnumerable<Position> GetAllAbovePositions(int currentX, int currentY, int newY, int newX)
            //{
            //    for (newY--; newY >= 0; newY--)
            //    {
            //        //yield return treePatch[newY, newX];
            //    }
            //}
        }

        private class Position3
        {
            public Position3(int knotCount)
            {
                X = 0;
                Y = 0;
                TotalKnotCount = knotCount;
                InitializeFor(knotCount);
            }

            public Position3(int x, int y, int knotNumber)
            {
                X = x;
                Y = y;
                KnotNumber = knotNumber;
            }

            public int X { get; set; }
            public int Y { get; set; }
            public int KnotNumber { get; set; }
            public int TotalKnotCount { get; set; }

            private void InitializeFor(int knotCount)
            {
                var currentKnot = this;
                for (var k = 1; k < knotCount; k++)
                {
                    var newKnot = new Position3(0, 0, k);
                    currentKnot.NextKnot = newKnot;
                    newKnot.TheseTailPositions.Push(newKnot);
                    currentKnot = newKnot;
                }

                LastInitialTail = currentKnot;
            }

            public Position3 CurrentTailPosition => TheseTailPositions.Peek();
            public Position3? NextKnot { get; set; }
            //public Position3 NextKnot => Knots.Peek();

            public Position3 LastInitialTail { get; set; }

            //public Stack<Position3> Knots { get; set; } = new Stack<Position3>();
            public Stack<Position3> TheseTailPositions { get; set; } = new Stack<Position3>();

            private List<string> MovementsSinceLastTailMovement { get; set; } = new List<string>();

            public void MoveHead((string command, int movementAmount) command)
            {
                for (var i = 0; i < TotalKnotCount; i++)
                {
                    var oldHeadPosition = (X, Y);
                    switch (command.command)
                    {
                        case "U":
                            Y--;
                            break;
                        case "D":
                            Y++;
                            break;
                        case "L":
                            X--;
                            break;
                        case "R":
                            X++;
                            break;
                    }

                    Console.WriteLine($"Head Position: X {X} . {Y} Y");
                    NextKnot.CheckIfNeedToMoveTail((X, Y), oldHeadPosition);
                }
            }

            public void MoveKnot((string command, int movementAmount) command)
            {
                for (var i = 0; i < command.movementAmount; i++)
                {
                    var oldHeadPosition = (X, Y);
                    MoveToNextPosition(command.command);
                    
                    Console.WriteLine($"Head Position: X {X} . {Y} Y");
                    CheckIfNeedToMoveTail((X, Y), oldHeadPosition);
                }
            }

            private void MoveToNextPosition(string command)
            {
                switch (command)
                {
                    case "U":
                        Y--;
                        break;
                    case "D":
                        Y++;
                        break;
                    case "L":
                        X--;
                        break;
                    case "R":
                        X++;
                        break;
                }
            }

            private void CheckIfNeedToMoveTail((int x, int y) newHeadPosition, (int x, int y) oldHeadPosition)
            {
                //if (NextKnot == null)
                ////if (!Knots.Any())
                //{
                //    return;
                //}
                
                var tail = CurrentTailPosition;
                var currentPosition = (X, Y);
                var x = Math.Abs(newHeadPosition.x - X);
                var y = Math.Abs(newHeadPosition.y - Y);
                //var tail = CurrentTailPosition;
                //var x = Math.Abs(newHeadPosition.x - tail.X);
                //var y = Math.Abs(newHeadPosition.y - tail.Y);
                var isTailAdjacent = x <= 1 && y <= 1;

                //var positionToCheckNext = 
                //NextKnot.CheckIfNeedToMoveTail(command, (tail.X, tail.Y));
                if (!isTailAdjacent)
                {
                    MoveTailFromCurrentToNewPosition(X, Y, tail.KnotNumber);
                    this.X = oldHeadPosition.x;
                    this.Y = oldHeadPosition.y;
                    //MoveTailFromCurrentToNewPosition(oldHeadPosition.x, oldHeadPosition.y, tail.KnotNumber);
                }

                if (NextKnot != null)
                {
                    NextKnot.CheckIfNeedToMoveTail(oldHeadPosition, currentPosition);
                }
            }

            private void MoveTailFromCurrentToNewPosition(int newX, int newY, int knotNumber)
            {
                //var tail = CurrentTailPosition;
                //var amountOfXToTraverse = newX - tail.X;
                //var amountOfYToTraverse = newY - tail.Y;

                //for (var x = tail.X; Math.Abs(newX - x) > 1; x = newX - x)
                //{
                //    Console.WriteLine($"test x {x}");
                //}

                if (knotNumber == 9)
                {
                    Console.WriteLine($"Tail Position: X {newX} . {newY} Y");
                }
                TheseTailPositions.Push(new Position3(newX, newY, knotNumber));
            }

            //private IEnumerable<Position> GetAllAbovePositions(int currentX, int currentY, int newY, int newX)
            //{
            //    for (newY--; newY >= 0; newY--)
            //    {
            //        //yield return treePatch[newY, newX];
            //    }
            //}
        }

        private class Position
        {
            public Position()
            {
                X = 0;
                Y = 0;
                TailPositions.Push(new Position(0, 0, true));
            }

            public Position(int x, int y, bool initialize = false)
            {
                X = x;
                Y = y;

                if (initialize)
                {
                    TailPositions.Push(new Position(x, y));
                }
            }

            public int X { get; set; }
            public int Y { get; set; }

            public Position CurrentTailPosition => TailPositions.Peek();

            public Stack<Position> TailPositions { get; set; } = new Stack<Position>();

            private List<string> MovementsSinceLastTailMovement { get; set; } = new List<string>();

            public void MoveHead((string command, int movementAmount) command)
            {
                for (var i = 0; i < command.movementAmount; i++)
                {
                    var oldHeadPosition = (X, Y);
                    switch (command.command)
                    {
                        case "U":
                            Y--;
                            break;
                        case "D":
                            Y++;
                            break;
                        case "L":
                            X--;
                            break;
                        case "R":
                            X++;
                            break;
                    }

                    Console.WriteLine($"Head Position: X {X} . {Y} Y");
                    CheckIfNeedToMoveTail(command, oldHeadPosition);
                }
            }

            private void CheckIfNeedToMoveTail((string command, int movementAmount) command, (int x, int y) oldHeadPosition)
            {
                //MovementsSinceLastTailMovement.Add(command.command);

                var tail = CurrentTailPosition;
                var x = Math.Abs(X - tail.X);
                var y = Math.Abs(Y - tail.Y);
                var isTailAdjacent = x <= 1 && y <= 1;

                if (isTailAdjacent)
                {
                    return;
                }

                MoveTailFromCurrentToNewPosition(oldHeadPosition.x, oldHeadPosition.y);
                //foreach (var moveCommand in MovementsSinceLastTailMovement)
                //{
                //    switch (moveCommand)
                //    {
                //        case "U":
                //            MoveTailFromCurrentToNewPosition(X, Y + 1);
                //            break;
                //        case "D":
                //            MoveTailFromCurrentToNewPosition(X, Y - 1);
                //            break;
                //        case "L":
                //            MoveTailFromCurrentToNewPosition(X + 1, Y);
                //            break;
                //        case "R":
                //            MoveTailFromCurrentToNewPosition(X - 1, Y);
                //            break;
                //    }
                //}

                //MovementsSinceLastTailMovement.Clear();
            }

            private void MoveTailFromCurrentToNewPosition(int newX, int newY)
            {
                //var tail = CurrentTailPosition;
                //var amountOfXToTraverse = newX - tail.X;
                //var amountOfYToTraverse = newY - tail.Y;

                //for (var x = tail.X; Math.Abs(newX - x) > 1; x = newX - x)
                //{
                //    Console.WriteLine($"test x {x}");
                //}

                Console.WriteLine($"Tail Position: X {newX} . {newY} Y");
                TailPositions.Push(new Position(newX, newY));
            }

            //private IEnumerable<Position> GetAllAbovePositions(int currentX, int currentY, int newY, int newX)
            //{
            //    for (newY--; newY >= 0; newY--)
            //    {
            //        //yield return treePatch[newY, newX];
            //    }
            //}
        }

        private class Position4
        {
            public Position4()
            {
                X = 0;
                Y = 0;
                TailPositionsWithKnot.Push(new Position4(0, 0, 1, true));
            }
            public Position4(int knotCount)
            {
                X = 0;
                Y = 0;
                TotalKnots = knotCount;

                for (int i = 1; i < knotCount; i++)
                {
                    TailPositionsWithKnot.Push(new Position4(0, 0, i, true));
                }
            }

            public Position4(int x, int y, int knotNumber, bool initialize = false)
            {
                X = x;
                Y = y;
                KnotNumber = knotNumber;

                if (initialize)
                {
                    TailPositionsWithKnot.Push(new Position4(x, y, knotNumber));
                }
            }

            public int X { get; set; }
            public int Y { get; set; }
            public int KnotNumber { get; set; }
            public int TotalKnots { get; set; } = 1;

            public Position CurrentTailPosition => TailPositions.Peek();

            public Stack<Position> TailPositions { get; set; } = new Stack<Position>();
            public Stack<Position4> TailPositionsWithKnot { get; set; } = new Stack<Position4>();
            public List<Position4> CurrentTailPositionsWithKnot(int knot) => TailPositionsWithKnot.Where(k => k.KnotNumber == knot).ToList();
            public Position4 CurrentTailPositionWithKnot(int knot) => CurrentTailPositionsWithKnot(knot).First();

            private List<string> MovementsSinceLastTailMovement { get; set; } = new List<string>();

            public void MoveHead((string command, int movementAmount) command)
            {
                for (var i = 0; i < command.movementAmount; i++)
                {
                    var oldHeadPosition = (X, Y);
                    switch (command.command)
                    {
                        case "U":
                            Y--;
                            break;
                        case "D":
                            Y++;
                            break;
                        case "L":
                            X--;
                            break;
                        case "R":
                            X++;
                            break;
                    }

                    Console.WriteLine($"Head Position: X {X} . {Y} Y");

                    var current = CurrentTailPositionWithKnot(1);
                    CheckIfNeedToMoveTail(this, current, oldHeadPosition);
                    current = CurrentTailPositionWithKnot(1);

                    for (int k = 2; k < TotalKnots; k++)
                    {
                        var currentThisTail = CurrentTailPositionWithKnot(k);
                        var newOldKnotBefore = (current.X, current.Y);
                        CheckIfNeedToMoveTail(current, currentThisTail, newOldKnotBefore);
                        current = CurrentTailPositionWithKnot(k);
                    }
                }
            }

            private void CheckIfNeedToMoveTail(Position4 head, Position4 tail, (int x, int y) oldHeadPosition)
            {
                var x = Math.Abs(head.X - tail.X);
                var y = Math.Abs(head.Y - tail.Y);
                var isTailAdjacent = x <= 1 && y <= 1;

                if (isTailAdjacent)
                {
                    return;
                }

                int newX = tail.X, newY = tail.Y;

                var beforeIsBelow = x <= 1 && (head.Y - tail.Y) > 1;
                var beforeIsAbove = x <= 1 && (head.Y - tail.Y) < 1;

                var beforeIsLeft = y <= 1 && (head.X - tail.X) > 1;
                var beforeIsRight = y <= 1 && (head.X - tail.X) < 1;

                if (beforeIsBelow)
                {
                    newY++;
                }
                
                if (beforeIsAbove)
                {
                    newY--;
                }
                
                if (beforeIsLeft)
                {
                    newX++;
                }
                
                if (beforeIsRight)
                {
                    newX--;
                }

                Console.WriteLine($"   {tail.KnotNumber} Position: X {newX} . {newY} Y");
                TailPositionsWithKnot.Push(new Position4(newX, newY, tail.KnotNumber));
                //MoveTailFromCurrentToNewPosition(oldHeadPosition.x, oldHeadPosition.y, tail.KnotNumber);
            }

            private void MoveTailFromCurrentToNewPosition(int newX, int newY, int knot)
            {
                //var tail = CurrentTailPosition;
                //var amountOfXToTraverse = newX - tail.X;
                //var amountOfYToTraverse = newY - tail.Y;

                //for (var x = tail.X; Math.Abs(newX - x) > 1; x = newX - x)
                //{
                //    Console.WriteLine($"test x {x}");
                //}

                Console.WriteLine($"   {knot} Position: X {newX} . {newY} Y");
                TailPositionsWithKnot.Push(new Position4(newX, newY, knot));
            }

            //private IEnumerable<Position> GetAllAbovePositions(int currentX, int currentY, int newY, int newX)
            //{
            //    for (newY--; newY >= 0; newY--)
            //    {
            //        //yield return treePatch[newY, newX];
            //    }
            //}
        }
    }
}
