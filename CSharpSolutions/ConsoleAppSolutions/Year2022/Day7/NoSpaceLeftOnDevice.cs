using System.Text.RegularExpressions;

namespace ConsoleAppSolutions.Year2022.Day7
{
    public static class NoSpaceLeftOnDevice
    {
        public static void PlayForStar1(bool useExampleInput = false)
        {
            var textFile = Play2022Solutions.GetInputFile(7, useExampleInput);

            if (File.Exists(textFile))
            {
                using (StreamReader file = new StreamReader(textFile))
                {
                    var rootDirectory = new Directory("/", null);
                    var currentDirectory = rootDirectory;

                    var commands = file.ReadToEnd().Split('$').Skip(1);

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
                            currentDirectory.FillDirectory(lines.Skip(1).Where(l => !string.IsNullOrEmpty(l)));
                        }
                    }

                    file.Close();

                    var totalTotal = rootDirectory.GetTotalSize();
                    Console.WriteLine($"total total: {totalTotal}");

                    var result = rootDirectory.GetFilteredDirectories(100000, true).Sum(d => d.GetTotalSize());
                    Console.WriteLine($"star 1 result: {result}");
                }
            }
        }

        public static void PlayForStar2(bool useExampleInput = false)
        {
            var textFile = Play2022Solutions.GetInputFile(7, useExampleInput);

            if (File.Exists(textFile))
            {
                using (StreamReader file = new StreamReader(textFile))
                {
                    var rootDirectory = new Directory("/", null);
                    var currentDirectory = rootDirectory;

                    var commands = file.ReadToEnd().Split('$').Skip(1);

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
                            currentDirectory.FillDirectory(lines.Skip(1).Where(l => !string.IsNullOrEmpty(l)));
                        }
                    }

                    file.Close();

                    var totalUsed = rootDirectory.GetTotalSize();
                    Console.WriteLine($"total used: {totalUsed}");

                    var totalAvailableDiskSpace = 70000000;
                    var minNeededDiskSpace = 30000000;
                    var freeDiskSpace = totalAvailableDiskSpace - totalUsed;
                    var toBeDeleted = minNeededDiskSpace - freeDiskSpace;
                    var possibleDirectories = rootDirectory.GetFilteredDirectories(toBeDeleted, false);

                    var result = possibleDirectories.MinBy(d => d.GetTotalSize());
                    Console.WriteLine($"star 2 result: {result.Name} {result.GetTotalSize()}");
                }
            }
        }

        private class Directory
        {
            public Directory(string name, Directory parent)
            {
                Name = name;
                Parent = parent;
            }

            public Directory Parent { get; set; }

            public string Name { get; set; }

            public List<(long size, string name)> Files { get; set; } = new();

            public List<Directory> Children { get; set; } = new();

            public Directory MoveOut()
            {
                return Parent;
            }

            public Directory GoToChild(string name)
            { 
                var child = Children.FirstOrDefault(c => c.Name == name);
                if (child == null)
                {
                    return new Directory(name, this);
                }

                return child;
            }

            public Directory ParseChangeDirectoryInput(string input, Directory rootDirectory)
            {
                var stripped = Regex.Replace(input, @"cd", "").Trim();
                if (stripped == "..")
                {
                    return MoveOut();
                }

                if (stripped == "/")
                {
                    return rootDirectory;
                }

                return GoToChild(stripped);
            }

            public void FillDirectory(IEnumerable<string> listedThings)
            {
                foreach (var listedThing in listedThings)
                {
                    var infos = listedThing.Split(' ');
                    if (infos[0] == "dir")
                    {
                        Children.Add(new Directory(infos[1], this));
                    }
                    else
                    {
                        Files.Add((long.Parse(infos[0]), infos[1]));
                    }
                }
            }

            public long GetTotalSize()
            {
                var filesSize = GetDirectFilesSize();
                foreach (var child in Children)
                {
                    filesSize += child.GetTotalSize();
                }

                return filesSize;
            }

            public long GetDirectFilesSize() => Files.Sum(f => f.size);

            public bool FilterForSize(long size, bool lookForMaxElseMin)
            {
                if (lookForMaxElseMin)
                {
                    return GetTotalSize() <= size;
                }

                return GetTotalSize() >= size;
            }

            public List<Directory> GetFilteredDirectories(long size, bool lookForMaxElseMin)
            {
                var filteredDirectories = new List<Directory>();

                if (FilterForSize(size, lookForMaxElseMin))
                {
                    filteredDirectories.Add(this);
                }

                foreach (var child in Children)
                {
                    filteredDirectories.AddRange(child.GetFilteredDirectories(size, lookForMaxElseMin));
                }

                return filteredDirectories;
            }
        }
    }
}
