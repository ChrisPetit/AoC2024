using System.Text.RegularExpressions;

namespace day03;

public class Program
{
    public static void Main()
    {
        //read input.txt file
        var lines = File.ReadAllText("input.txt");
        Console.WriteLine(ReturnMultiplications(lines));
        Console.WriteLine(ReturnEnabledMultiplications(lines));
        
    }

    public static long ReturnMultiplications(string memory)
    {
        // retrieve all pieces of the string that are in the format mul(a,b)
        // and multiply a by b and sum all the results
        var matches = Regex.Matches(memory, @"mul\((\d+),(\d+)\)");
        return matches.Select(match => int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value)).Sum();
    }
    
    public static long ReturnEnabledMultiplications(string memory)
    {
        var totalSum = 0L;
        var disabled = memory.Split("don't()");
        totalSum += ReturnMultiplications(disabled[0]);
        for (var i = 1; i < disabled.Length; i++)
        {
            var enabled = disabled[i].Split("do()");
            if (enabled.Length <= 1) continue;
            for (var j = 1; j < enabled.Length; j++)
            {
                totalSum += ReturnMultiplications(enabled[j]);
            }
        }
        return totalSum;
    }
}