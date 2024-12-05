namespace day02.tests;

public class ProgramTests
{
    [Fact]
    public void TestReturnSafeReports()
    {
        string[] reports =
        [
            "7 6 4 2 1",
            "1 2 7 8 9",
            "9 7 6 2 1",
            "1 3 2 4 5",
            "8 6 4 4 1",
            "1 3 6 7 9"
        ];

        var result = Program.ReturnSafeReports(reports);
        Assert.Equal(2, result); // Adjust the expected value based on the logic
    }
    
    [Fact]
    public void TestReturnSafeReportsWithProblemDampener()
    {
        string[] reports =
        [
            "7 6 4 2 1",
            "1 2 7 8 9",
            "9 7 6 2 1",
            "1 3 2 4 5",
            "8 6 4 4 1",
            "1 3 6 7 9"
        ];

        var result = Program.ReturnSafeReportsWithProblemDampener(reports);
        Assert.Equal(4, result); // Adjust the expected value based on the logic
    }
}