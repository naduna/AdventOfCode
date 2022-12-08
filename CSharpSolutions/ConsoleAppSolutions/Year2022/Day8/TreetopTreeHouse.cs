namespace ConsoleAppSolutions.Year2022.Day8
{
    public class TreetopTreeHouse : DayQuizBase
    {
        public override int Year => 2022;
        public override int Day => 8;

        public override void PlayForStar1(bool useExampleInput = false)
        {
            var treePatch = GetTreePatch(useExampleInput);
            var rowLength = treePatch.GetLength(0);
            var columnLength = treePatch.GetLength(1);

            var flattenedInnerTreeList = new List<(int treeHeight, bool isVisible)>();

            for (var row = 1; row < rowLength - 1; row++)
            {
                for (var column = 1; column < columnLength - 1; column++)
                {
                    var current = treePatch[row, column];

                    var (above, below, toLeft, toRight) = GetTreesToGridEdge(treePatch, row, column);

                    if (IsVisibleByDirection(above, current)
                        || IsVisibleByDirection(below, current)
                        || IsVisibleByDirection(toLeft, current)
                        || IsVisibleByDirection(toRight, current))
                    {
                        current.isVisible = true;
                    }

                    flattenedInnerTreeList.Add(current);
                }
            }

            var countOfVisibleTreesOnEdge = treePatch.GetLength(0) * 2 + treePatch.GetLength(1) * 2 - 4; // outer edge trees double counted
            var result = flattenedInnerTreeList.Count(t => t.isVisible) + countOfVisibleTreesOnEdge;
            Console.WriteLine($"star 1 result: {result}");
        }

        public override void PlayForStar2(bool useExampleInput = false)
        {
            var treePatch = GetTreePatch(useExampleInput);
            var rowLength = treePatch.GetLength(0);
            var columnLength = treePatch.GetLength(1);

            var flattenedTreeListWithScores = new List<(int treeHeight, int scenicScore)>();

            for (var row = 0; row < rowLength; row++)
            {
                for (var column = 0; column < columnLength; column++)
                {
                    var current = treePatch[row, column];
                    
                    var (above, below, toLeft, toRight) = GetTreesToGridEdge(treePatch, row, column);

                    var score = GetScenicScoreForDirection(above, current)
                                * GetScenicScoreForDirection(below, current)
                                * GetScenicScoreForDirection(toLeft, current)
                                * GetScenicScoreForDirection(toRight, current);

                    flattenedTreeListWithScores.Add((current.treeHeight, score));
                }
            }
            
            var result = flattenedTreeListWithScores.Max(t => t.scenicScore);
            Console.WriteLine($"star 2 result: {result}");
        }

        private (int treeHeight, bool isVisible)[,] GetTreePatch(bool useExampleInput = false)
        {
            var lines = GetInputTextByLine(useExampleInput);
            var firstLineLength = lines.First().Length;

            var treePatch = new (int treeHeight, bool isVisible)[lines.Length, firstLineLength];

            for (var i = 0; i < lines.Length; i++)
            {
                var row = lines[i];
                for (var j = 0; j < firstLineLength; j++)
                {
                    if (int.TryParse(row[j].ToString(), out var tree))
                    {
                        treePatch[i, j] = (tree, false);
                    }
                }
            }

            return treePatch;
        }

        private static (
            IEnumerable<(int treeHeight, bool isVisible)> above,
            IEnumerable<(int treeHeight, bool isVisible)> below,
            IEnumerable<(int treeHeight, bool isVisible)> toLeft,
            IEnumerable<(int treeHeight, bool isVisible)> toRight)
            GetTreesToGridEdge((int treeHeight, bool isVisible)[,] treePatch, int row, int column)
        {
            var above = GetAllAboveRows(treePatch, row, column);
            var below = GetAllBelowRows(treePatch, row, column);
            var toLeft = GetAllLeftHandColumns(treePatch, row, column);
            var toRight = GetAllRightHandColumns(treePatch, row, column);

            return (above, below, toLeft, toRight);
        }

        private static bool IsVisibleByDirection(IEnumerable<(int treeHeight, bool isVisible)> treesBetweenThisAndEdgeOfGrid, (int treeHeight, bool isVisible) current)
        {
            return treesBetweenThisAndEdgeOfGrid.Max(t => t.treeHeight) < current.treeHeight;
        }

        private static int GetScenicScoreForDirection(IEnumerable<(int treeHeight, bool isVisible)> treesBetweenThisAndEdgeOfGrid, (int treeHeight, bool isVisible) current)
        {
            var array = treesBetweenThisAndEdgeOfGrid.ToList();
            var firstHigherTree = array.FindIndex(t => t.treeHeight >= current.treeHeight);

            return firstHigherTree < 0 ? array.Count : firstHigherTree + 1;
        }

        private static IEnumerable<(int treeHeight, bool isVisible)> GetAllAboveRows((int treeHeight, bool isVisible)[,] treePatch, int row, int column)
        {
            for (row--; row >= 0; row--)
            {
                yield return treePatch[row, column];
            }
        }

        private static IEnumerable<(int treeHeight, bool isVisible)> GetAllBelowRows((int treeHeight, bool isVisible)[,] treePatch, int row, int column)
        {
            var maxRowIndex = treePatch.GetLength(0) - 1;
            for (row++; row <= maxRowIndex; row++)
            {
                yield return treePatch[row, column];
            }
        }

        private static IEnumerable<(int treeHeight, bool isVisible)> GetAllLeftHandColumns((int treeHeight, bool isVisible)[,] treePatch, int row, int column)
        {
            for (column--; column >= 0; column--)
            {
                yield return treePatch[row, column];
            }
        }

        private static IEnumerable<(int treeHeight, bool isVisible)> GetAllRightHandColumns((int treeHeight, bool isVisible)[,] treePatch, int row, int column)
        {
            var maxColumnIndex = treePatch.GetLength(1) - 1;
            for (column++; column <= maxColumnIndex; column++)
            {
                yield return treePatch[row, column];
            }
        }
    }
}
