namespace day04;

public class Program
{
    public static void Main()
    {
        var lines = File.ReadAllLines("input.txt");
        var array = lines.Select(line => line.ToCharArray()).ToArray();

        Console.WriteLine(ReturnNumberOfXmasInArray(array));
        Console.WriteLine(ReturnNumberOfXInArray(array));
    }

    public static long ReturnNumberOfXmasInArray(char[][] array)
    {
        var count = 0;
        var rows = array.Length;
        var cols = array[0].Length;
        const string word = "XMAS";
        
        // Define all 8 possible directions
        var directions = new int[][]
        {
            [0, 1], // Right
            [1, 0], // Down
            [1, 1], // Down-Right
            [1, -1], // Down-Left
            [0, -1], // Left
            [-1, 0], // Up
            [-1, -1], // Up-Left
            [-1, 1] // Up-Right
        };

        for (var row = 0; row < rows; row++)
        {
            for (var col = 0; col < cols; col++)
            {
                count += directions.Count(direction
                    => IsWordFound(array, row, col, direction, word));
            }
        }

        return count;
    }

    private static bool IsWordFound(char[][] array, int row, int col, int[] direction, string word)
    {
        var wordLength = word.Length;
        var rows = array.Length;
        var cols = array[0].Length;

        for (var i = 0; i < wordLength; i++)
        {
            var newRow = row + i * direction[0];
            var newCol = col + i * direction[1];

            if (newRow < 0 || newRow >= rows || newCol < 0 || newCol >= cols || array[newRow][newCol] != word[i])
            {
                return false;
            }
        }

        return true;
    }

    public static int ReturnNumberOfXInArray(char[][] array)
    {
        var rows = array.Length - 1;
        var cols = array[0].Length - 1;
        var results = new List<Xmas>();
        
        for (var r = 0; r < rows; r++)
        {
            for (var c = 0; c < cols; c++)
            {
                if (array[r][c] == 'A')
                {
                    results.Add(new Xmas(r, c, array));
                }
            }
        }

        results.RemoveAll(xmas => xmas.Row == 0 || xmas.Row == rows || xmas.Col == 0 || xmas.Col == cols);
        results.ForEach(xmas => xmas.IsXMasPatternFound());
        return results.Count(xmas => xmas.IsXmas);
    }
}

internal class Xmas(int row, int col, char[][] array)
{
    public int Row { get; set; } = row;
    public int Col { get; set; } = col;
    public bool IsXmas { get; set; }
    private char TopLeft => array[Row - 1][Col - 1];
    private char TopRight => array[Row - 1][Col + 1];
    private char BottomLeft => array[Row + 1][Col - 1];
    private char BottomRight => array[Row + 1][Col + 1];

    private char[] Chars => [TopLeft, TopRight, BottomRight, BottomLeft];
    // check if chars are mmss, smms, ssmm or mssm
    public void IsXMasPatternFound()
    {
        if (Chars.SequenceEqual(['M', 'M', 'S', 'S']) ||
            Chars.SequenceEqual(['S', 'M', 'M', 'S']) ||
            Chars.SequenceEqual(['S', 'S', 'M', 'M']) ||
            Chars.SequenceEqual(['M', 'S', 'S', 'M']))
        {
            IsXmas = true;
        }
    }
}

