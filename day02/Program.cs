namespace day02;

public class Program
{
    public static void Main()
    {
        //read input.txt file
        var lines = File.ReadAllLines("input.txt");
        Console.WriteLine(ReturnSafeReports(lines));
        Console.WriteLine(ReturnSafeReportsWithProblemDampener(lines));

    }

    public static long ReturnSafeReports(string[] reports)
    {
        return reports.Select(report => report.Split(" ")
            .Select(int.Parse)
            .ToList())
            .Count(levels =>
            (AllLevelsAreIncreasing(levels) || AllLevelsAreDecreasing(levels)) && DifferenceIsSafe(levels));
    }
    
    public static long ReturnSafeReportsWithProblemDampener(string[] reports)
    {
        var count = 0;
        foreach (var report in reports)
        {
            var levels = report.Split(" ").Select(int.Parse).ToList();
            if((AllLevelsAreIncreasing(levels) || AllLevelsAreDecreasing(levels)) && DifferenceIsSafe(levels))
            {
                count++;
                continue;
            } 
            //remove one element from the list and check if the levels are increasing or decreasing
            for (var j = 0; j < levels.Count; j++)
            {
                var tempList = new List<int>(levels);
                tempList.RemoveAt(j);
                if((AllLevelsAreIncreasing(tempList) || AllLevelsAreDecreasing(tempList)) && DifferenceIsSafe(tempList))
                {
                    count++;
                    break;
                }
            }
        }
        return count;
    }


    private static bool AllLevelsAreIncreasing(List<int> levels)
    {
        for (var i = 0; i < levels.Count - 1; i++)
        {
            if (levels[i] >= levels[i + 1])
            {
                return false;
            }
        }
        return true;
    }
    
    private static bool AllLevelsAreDecreasing(List<int> levels)
    {
        for (var i = 0; i < levels.Count - 1; i++)
        {
            if (levels[i] <= levels[i + 1])
            {
                return false;
            }
        }
        return true;
    }
    
    private static bool DifferenceIsSafe(List<int> levels)
    {
        // modulo of the difference between levels should be equal or less than 3 and equal or more than 1
        for (var i = 0; i < levels.Count - 1; i++)
        {
            if (Math.Abs(levels[i] - levels[i + 1]) > 3 || Math.Abs(levels[i] - levels[i + 1]) < 1)
            {
                return false;
            }
        }
        return true;
    }
}