namespace day01.tests;

public class ProgramTests
{
    [Fact]
    public void TestReturnTotalDistance()
    {
        // Arrange
        var listA = new List<int> { 3, 4, 2, 1, 3, 3 };
        var listB = new List<int> { 4, 3, 5, 3, 9, 3 };
        
        // Act
        var result = Program.ReturnTotalDistance(listA, listB);
        
        // Assert
        Assert.Equal(11, result);
    }
}