using System.Collections.Generic;
using System.Data.Common;

namespace ConsoleAppSolutions.Year2022.Day12
{
    public class HillClimbingAlgorithm : DayQuizBase
    {
        public override int Year => 2022;
        public override int Day => 12;

        private enum Direction
        {
            Up, Down, Left, Right,
        }

        public override void PlayForStar1(bool useExampleInput = false)
        {
            var (heightMap, end, currentPosition) = GetHeightMap(useExampleInput);
            var rowLength = heightMap.GetLength(0);
            var columnLength = heightMap.GetLength(1);
            
            var shortestPath = new List<ElevationItem> {currentPosition};

            while (end.Row != currentPosition.Row || end.Column != currentPosition.Column)
            {
                var row = currentPosition.Row;
                var column = currentPosition.Column;

                var preferredDirection = Direction.Right;
                var prioritizeRow = Math.Abs(end.Row - row) < Math.Abs(end.Column - column);
                if (!prioritizeRow)
                {
                    if (row < end.Row)
                    {
                        preferredDirection = Direction.Down;
                    }
                    else if (row > end.Row)
                    {
                        preferredDirection = Direction.Up;
                    }
                }
                else
                {
                    if (column > end.Column)
                    {
                        preferredDirection = Direction.Right;
                    }
                    else if (column < end.Column)
                    {
                        preferredDirection = Direction.Left;
                    }
                }

                var potentials = new List<(ElevationItem element, Direction direction)>();

                if (row > 0)
                {
                    potentials.Add((heightMap[row - 1, column], Direction.Up));
                }

                if (row < rowLength - 1)
                {
                    potentials.Add((heightMap[row + 1, column], Direction.Down));
                }

                if (column > 0)
                {
                    potentials.Add((heightMap[row, column - 1], Direction.Left));
                }

                if (column < columnLength - 1)
                {
                    potentials.Add((heightMap[row, column + 1], Direction.Right));
                }

                var currentElevation = currentPosition.Elevation;
                var nextElevation = currentPosition.Elevation + 1;
                var next = potentials.Where(p => !shortestPath.Contains(p.element)
                                                 && (p.element.Elevation == currentElevation
                                                     || p.element.Elevation == nextElevation))
                    .OrderByDescending(p => p.direction == preferredDirection)
                    .ThenByDescending(p => p.element.Elevation == nextElevation)
                    .First();

                //if (next.element == null)
                //{
                //    next = potentials.Where(p => (p.element.Elevation == currentElevation
                //                                     || p.element.Elevation == nextElevation))
                //        .OrderByDescending(p => p.direction == preferredDirection)
                //        .FirstOrDefault();
                //}

                Console.WriteLine(next.element);
                
                shortestPath.Add(next.element);
                currentPosition = next.element;
            }
            //for (var row = 1; row < rowLength - 1; row++)
            //{
            //    for (var column = 1; column < columnLength - 1; column++)
            //    {
            //        var current = heightMap[row, column];
            //        var preferredDirection = Direction.Right;
            //        if (row < end.row)
            //        {
            //            preferredDirection = Direction.Up;
            //        }
            //        else if (column < end.column)
            //        {
            //            preferredDirection = Direction.Right;
            //        }
            //        else if (column > end.column)
            //        {
            //            preferredDirection = Direction.Left;
            //        }
            //        else if (row > end.row)
            //        {
            //            preferredDirection = Direction.Down;
            //        }

            //        var (above, below, toLeft, toRight) = (
            //            (heightMap[row - 1, column], Direction.Up),
            //            (heightMap[row + 1, column], Direction.Down),
            //            (heightMap[row, column - 1], Direction.Left),
            //            (heightMap[row, column + 1], Direction.Right));



            //        if (IsVisibleByDirection(above, current)
            //            || IsVisibleByDirection(below, current)
            //            || IsVisibleByDirection(toLeft, current)
            //            || IsVisibleByDirection(toRight, current))
            //        {
            //            current.isStart = true;
            //        }

            //        shortestPath.Add(current);
            //    }
            //}

            var result = shortestPath.Count - 1; // subtract starting point
            Console.WriteLine($"star 1 result: {result}");
        }

        public override void PlayForStar2(bool useExampleInput = false)
        {
            //var treePatch = GetHeightMap(useExampleInput);
            //var rowLength = treePatch.GetLength(0);
            //var columnLength = treePatch.GetLength(1);

            //var flattenedTreeListWithScores = new List<(int treeHeight, int scenicScore)>();

            //for (var row = 0; row < rowLength; row++)
            //{
            //    for (var column = 0; column < columnLength; column++)
            //    {
            //        var current = treePatch[row, column];
                    
            //        var (above, below, toLeft, toRight) = GetTreesToGridEdge(treePatch, row, column);

            //        var score = GetScenicScoreForDirection(above, current)
            //                    * GetScenicScoreForDirection(below, current)
            //                    * GetScenicScoreForDirection(toLeft, current)
            //                    * GetScenicScoreForDirection(toRight, current);

            //        flattenedTreeListWithScores.Add((treeHeight: current.elevation, score));
            //    }
            //}
            
            //var result = flattenedTreeListWithScores.Max(t => t.scenicScore);
            //Console.WriteLine($"star 2 result: {result}");
        }

        private (ElevationItem[,] heightMap,
            ElevationItem end,
            ElevationItem currentPosition) GetHeightMap(bool useExampleInput = false)
        {
            var lines = GetInputTextByLine(useExampleInput);
            var firstLineLength = lines.First().Length;
            ElevationItem end = null, currentPosition = null;

            var heightMap = new ElevationItem[lines.Length, firstLineLength];

            for (var i = 0; i < lines.Length; i++)
            {
                var row = lines[i];
                for (var j = 0; j < firstLineLength; j++)
                {
                    var elevationChar = row[j];
                    bool isStart = false, isEnd = false;
                    if (char.IsUpper(elevationChar))
                    {
                        isStart = elevationChar.Equals('S');
                        isEnd = elevationChar.Equals('E');
                    }

                    var element = new ElevationItem(elevationChar, isStart, isEnd, i, j);
                    heightMap[i, j] = element;

                    if (isStart)
                    {
                        element.Elevation = (int)'a' - (int)'a';
                        currentPosition = element;
                    }
                    else if (isEnd)
                    {
                        element.Elevation = (int)'z' - (int)'a';
                        end = element;
                    }
                }
            }

            return (heightMap, end, currentPosition);
        }

        private static (
            IEnumerable<(int treeHeight, bool isVisible)> above,
            IEnumerable<(int treeHeight, bool isVisible)> below,
            IEnumerable<(int treeHeight, bool isVisible)> toLeft,
            IEnumerable<(int treeHeight, bool isVisible)> toRight)
            GetNeighboringElevations((int treeHeight, bool isStart)[,] treePatch, int row, int column)
        {
            var above = GetAllAboveRows(treePatch, row, column);
            var below = GetAllBelowRows(treePatch, row, column);
            var toLeft = GetAllLeftHandColumns(treePatch, row, column);
            var toRight = GetAllRightHandColumns(treePatch, row, column);

            return (above, below, toLeft, toRight);
        }

        private static (
            IEnumerable<(int treeHeight, bool isVisible)> above,
            IEnumerable<(int treeHeight, bool isVisible)> below,
            IEnumerable<(int treeHeight, bool isVisible)> toLeft,
            IEnumerable<(int treeHeight, bool isVisible)> toRight)
            GetTreesToGridEdge((int treeHeight, bool isStart)[,] treePatch, int row, int column)
        {
            var above = GetAllAboveRows(treePatch, row, column);
            var below = GetAllBelowRows(treePatch, row, column);
            var toLeft = GetAllLeftHandColumns(treePatch, row, column);
            var toRight = GetAllRightHandColumns(treePatch, row, column);

            return (above, below, toLeft, toRight);
        }

        private static bool IsVisibleByDirection(IEnumerable<(int treeHeight, bool isVisible)> treesBetweenThisAndEdgeOfGrid, (int treeHeight, bool isStart) current)
        {
            return treesBetweenThisAndEdgeOfGrid.Max(t => t.treeHeight) < current.treeHeight;
        }

        private static int GetScenicScoreForDirection(IEnumerable<(int treeHeight, bool isVisible)> treesBetweenThisAndEdgeOfGrid, (int treeHeight, bool isStart) current)
        {
            var array = treesBetweenThisAndEdgeOfGrid.ToList();
            var firstHigherTree = array.FindIndex(t => t.treeHeight >= current.treeHeight);

            return firstHigherTree < 0 ? array.Count : firstHigherTree + 1;
        }

        private static IEnumerable<(int treeHeight, bool isStart)> GetAllAboveRows((int treeHeight, bool isStart)[,] treePatch, int row, int column)
        {
            for (row--; row >= 0; row--)
            {
                yield return treePatch[row, column];
            }
        }

        private static IEnumerable<(int treeHeight, bool isStart)> GetAllBelowRows((int treeHeight, bool isStart)[,] treePatch, int row, int column)
        {
            var maxRowIndex = treePatch.GetLength(0) - 1;
            for (row++; row <= maxRowIndex; row++)
            {
                yield return treePatch[row, column];
            }
        }

        private static IEnumerable<(int treeHeight, bool isStart)> GetAllLeftHandColumns((int treeHeight, bool isStart)[,] treePatch, int row, int column)
        {
            for (column--; column >= 0; column--)
            {
                yield return treePatch[row, column];
            }
        }

        private static IEnumerable<(int treeHeight, bool isStart)> GetAllRightHandColumns((int treeHeight, bool isStart)[,] treePatch, int row, int column)
        {
            var maxColumnIndex = treePatch.GetLength(1) - 1;
            for (column++; column <= maxColumnIndex; column++)
            {
                yield return treePatch[row, column];
            }
        }

        private class ElevationItem
        {
            public ElevationItem(char elevation, bool isStart, bool isEnd, int row, int column)
            {
                ItemName = elevation;
                Elevation = (int)elevation - (int)'a';
                IsStart = isStart;
                IsEnd = isEnd;
                Row = row;
                Column = column;
            }

            public char ItemName { get; }
            public int Elevation { get; set; }
            public bool IsStart { get; }
            public bool IsEnd { get; }
            public int Row { get; }
            public int Column { get; }

            public override string ToString()
            {
                return $"{ItemName} [{Row}.{Column}]";
            }
        }
    }
}
