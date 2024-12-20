﻿namespace day03.tests;

public class ProgramTests
{
    [Fact]
    public void TestReturnMultiplications()
    {
        // Arrange
        var input = "xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))";
        
        // Act
        var result = Program.ReturnMultiplications(input);
        
        // Assert
        Assert.Equal(161, result);
    }
    
    [Fact]
    public void TestReturnEnabledMultiplications()
    {
        // Arrange
        var input = "xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))";
        
        // Act
        var result = Program.ReturnEnabledMultiplications(input);
        
        // Assert
        Assert.Equal(48, result);
    }
}