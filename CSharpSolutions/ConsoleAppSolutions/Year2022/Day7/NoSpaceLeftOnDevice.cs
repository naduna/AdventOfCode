namespace ConsoleAppSolutions.Year2022.Day7
{
    public class NoSpaceLeftOnDevice : DayQuizBase
    {
        public override int Year => 2022;
        public override int Day => 7;

        public override void PlayForStar1(bool useExampleInput = false)
        {
            var rootDirectory = GetFullyDiscoveredRootDirectory(useExampleInput);

            var result = rootDirectory.GetFilteredDirectories(100000, DirectoryFilterForSizeBy.Max).Sum(d => d.GetTotalSize());
            Console.WriteLine($"star 1 result: {result}");
        }

        public override void PlayForStar2(bool useExampleInput = false)
        {
            var rootDirectory = GetFullyDiscoveredRootDirectory(useExampleInput);

            var totalUsed = rootDirectory.GetTotalSize();
            Console.WriteLine($"total used: {totalUsed}");

            const int totalAvailableDiskSpace = 70000000;
            const int minNeededDiskSpace = 30000000;

            var freeDiskSpace = totalAvailableDiskSpace - totalUsed;
            var toBeDeleted = minNeededDiskSpace - freeDiskSpace;
            var possibleDirectories = rootDirectory.GetFilteredDirectories(toBeDeleted, DirectoryFilterForSizeBy.Min);

            var result = possibleDirectories.MinBy(d => d.GetTotalSize());
            Console.WriteLine($"star 2 result: {result?.Name} {result?.GetTotalSize()}");
        }

        private DeviceDirectory GetFullyDiscoveredRootDirectory(bool useExampleInput)
        {
            var rootDirectory = new DeviceDirectory("/");
            var currentDirectory = rootDirectory;

            var commands = GetInputTextComplete(useExampleInput).Split('$').Skip(1);

            foreach (var command in commands)
            {
                var lines = command.Split("\r\n");
                var commandLine = lines.First().Trim();
                if (commandLine.StartsWith("cd"))
                {
                    currentDirectory = currentDirectory.ParseChangeDirectoryInput(commandLine, rootDirectory);
                }
                else if (commandLine.StartsWith("ls"))
                {
                    currentDirectory.FillDirectoryWithListedItems(lines.Skip(1).Where(l => !string.IsNullOrEmpty(l)));
                }
            }

            return rootDirectory;
        }

        private class DeviceDirectory
        {
            public DeviceDirectory(string name, DeviceDirectory? parent = null)
            {
                Name = name;
                Parent = parent;
            }

            public string Name { get; }

            private DeviceDirectory? Parent { get; }

            private List<(long size, string name)> Files { get; } = new();

            private List<DeviceDirectory> Children { get; } = new();

            public DeviceDirectory ParseChangeDirectoryInput(string command, DeviceDirectory rootDirectory)
            {
                var commandInput = command.Replace("cd", "").Trim();
                return commandInput switch
                {
                    ".." => MoveOut() ?? rootDirectory,
                    "/" => rootDirectory,
                    _ => MoveInToChild(commandInput)
                };
            }

            private DeviceDirectory? MoveOut()
            {
                return Parent;
            }

            private DeviceDirectory MoveInToChild(string name)
            { 
                var child = Children.FirstOrDefault(c => c.Name == name);
                if (child == null)
                {
                    return new DeviceDirectory(name, this);
                }

                return child;
            }

            public void FillDirectoryWithListedItems(IEnumerable<string> listedItems)
            {
                foreach (var item in listedItems)
                {
                    var infos = item.Split(' ');
                    if (infos[0] == "dir")
                    {
                        Children.Add(new DeviceDirectory(infos[1], this));
                    }
                    else
                    {
                        Files.Add((long.Parse(infos[0]), infos[1]));
                    }
                }
            }

            public long GetTotalSize()
            {
                var filesSize = Files.Sum(f => f.size);
                foreach (var child in Children)
                {
                    filesSize += child.GetTotalSize();
                }

                return filesSize;
            }

            public List<DeviceDirectory> GetFilteredDirectories(long size, DirectoryFilterForSizeBy filterForSizeBy)
            {
                var filteredDirectories = new List<DeviceDirectory>();

                if (FilterForSize(size, filterForSizeBy))
                {
                    filteredDirectories.Add(this);
                }

                foreach (var child in Children)
                {
                    filteredDirectories.AddRange(child.GetFilteredDirectories(size, filterForSizeBy));
                }

                return filteredDirectories;
            }

            private bool FilterForSize(long size, DirectoryFilterForSizeBy filterForSizeBy)
            {
                return filterForSizeBy switch
                {
                    DirectoryFilterForSizeBy.Max => GetTotalSize() <= size,
                    DirectoryFilterForSizeBy.Min => GetTotalSize() >= size,
                    _ => throw new ArgumentException()
                };
            }
        }

        private enum DirectoryFilterForSizeBy
        {
            Max,
            Min
        }
    }
}
