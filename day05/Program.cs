namespace day05;

public static class Program
{
    public static void Main()
    {
        
        var input = File.ReadAllText("input.txt").Split("\n\n");
        
        var rulesInput = input[0].Split("\n").ToList();
        var updatesInput = input[1].Split("\n").ToList();
        
        // Parse the rules and updates
        var rules = ParseRules(rulesInput);
        var updates = ParseUpdates(updatesInput);

        // Find the correctly ordered updates and calculate the sum of their middle values
        var sumOfCorrectUpdates = CalculateMiddleSumOfCorrectUpdates(rules, updates);
        var sumReorderedUpdates = CalculateMiddleSumOfReorderedUpdates(rules, updates);
        Console.WriteLine(sumOfCorrectUpdates);
        Console.WriteLine(sumReorderedUpdates);
    }

    // Parses the rules from the input into a dictionary
    public static Dictionary<int, HashSet<int>> ParseRules(List<string> rulesInput)
    {
        var rules = new Dictionary<int, HashSet<int>>();
        foreach (var parts in rulesInput.Select(rule => rule.Split('|').Select(int.Parse).ToArray()))
        {
            if (!rules.ContainsKey(parts[0]))
                rules[parts[0]] = [];
            rules[parts[0]].Add(parts[1]);
        }
        return rules;
    }

    // Parses the updates from the input into a list of lists
    public static List<List<int>> ParseUpdates(List<string> updatesInput)
    {
        return updatesInput
            .Select(update => update.Split(',').Select(int.Parse).ToList())
            .ToList();
    }

    // Validates if an update is in the correct order based on the rules
    private static bool IsUpdateCorrect(List<int> update, Dictionary<int, HashSet<int>> rules)
    {
        var positionMap = update
            .Select((value, index) => new { value, index })
            .ToDictionary(x => x.value, x => x.index);

        return !(from rule in rules
            let firstPage = rule.Key
            where rule.Value.Any(secondPage =>
                positionMap.ContainsKey(firstPage) && positionMap.ContainsKey(secondPage) &&
                positionMap[firstPage] >= positionMap[secondPage])
            select rule).Any();
    }

    // Finds the middle page number of a sorted update
    private static int FindMiddlePage(List<int> update)
    {
        var middleIndex = update.Count / 2;
        return update[middleIndex];
    }

    // Calculates the sum of the middle page numbers of correctly ordered updates
    public static int CalculateMiddleSumOfCorrectUpdates(
        Dictionary<int, HashSet<int>> rules, List<List<int>> updates)
    {
        return updates.Where(update => IsUpdateCorrect(update, rules)).Sum(FindMiddlePage);
    }
    
    // Calculates the sum of the middle page numbers of reordered updates
    public static int CalculateMiddleSumOfReorderedUpdates(
        Dictionary<int, HashSet<int>> rules, List<List<int>> updates)
    {
        return (from update in updates
            where !IsUpdateCorrect(update, rules)
            select ReorderUpdate(update, rules)
            into reorderedUpdate
            select FindMiddlePage(reorderedUpdate)).Sum();
    }
    
    // Reorders an update using topological sort
    private static List<int> ReorderUpdate(List<int> update, Dictionary<int, HashSet<int>> rules)
    {
        var graph = new Dictionary<int, List<int>>();
        var inDegree = new Dictionary<int, int>();

        // Initialize graph and in-degree map for the update
        foreach (var page in update)
        {
            graph[page] = [];
            inDegree[page] = 0;
        }

        // Add edges for rules that are relevant to the update
        foreach (var rule in rules)
        {
            if (!update.Contains(rule.Key)) continue;
            foreach (var dependentPage in rule.Value.Where(update.Contains))
            {
                graph[rule.Key].Add(dependentPage);
                inDegree[dependentPage]++;
            }
        }

        // Perform topological sort (Kahn's Algorithm)
        var sorted = new List<int>();
        var queue = new Queue<int>(inDegree.Where(x => x.Value == 0).Select(x => x.Key));

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            sorted.Add(current);

            foreach (var neighbor in graph[current])
            {
                inDegree[neighbor]--;
                if (inDegree[neighbor] == 0)
                {
                    queue.Enqueue(neighbor);
                }
            }
        }

        return sorted;
    }
}