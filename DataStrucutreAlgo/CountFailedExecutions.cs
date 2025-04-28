namespace DataStrucutreAlgo;

public class ProcessExecution
{
    public static int CountFailedExecutions(int[] requiredSequence, int[] actualSequence)
    {
        var indexes = new Dictionary<int, int>();
        for (var i = 0; i < requiredSequence.Length; i++)
            indexes[requiredSequence[i]] = i;

        var failedExecutions = 0;
        //count desc
        for (var i = 1; i < actualSequence.Length; i++) {
            var pointer = actualSequence[i];
            var currIndex = indexes[pointer];
            var prevIndex = indexes[actualSequence[i-1]];

            //desceding
            if (prevIndex > currIndex) {
                failedExecutions++;
            }
        }

        return failedExecutions;
    }
}


public class CountFailedExecutionsTests
{
    [Fact]
    public void Test1_ExampleCase_MultipleFailures()
    {
        int[] requiredSequence = {4, 2, 3, 5, 1, 6};
        int[] actualSequence = {2, 3, 5, 1, 6, 4};
        var expectedFailures = 5;
        Assert.Equal(expectedFailures, ProcessExecution.CountFailedExecutions(requiredSequence, actualSequence));
    }

    [Fact]
    public void Test2_ExampleCase_NoFailure()
    {
        int[] requiredSequence = {3, 2, 1};
        int[] actualSequence = {3, 2, 1};
        var expectedFailures = 0;
        Assert.Equal(expectedFailures, ProcessExecution.CountFailedExecutions(requiredSequence, actualSequence));
    }

    [Fact]
    public void Test3_ExampleCase_SomeFailures()
    {
        int[] requiredSequence = {2, 3, 5, 1, 4};
        int[] actualSequence = {5, 2, 3, 4, 1};

        var expectedFailures = 2;
        Assert.Equal(expectedFailures, ProcessExecution.CountFailedExecutions(requiredSequence, actualSequence));
    }

    [Fact]
    public void Test4_AllReversed_AllFailExceptFirst()
    {
        int[] requiredSequence = {1, 2, 3, 4, 5};
        int[] actualSequence = {5, 4, 3, 2, 1};
        var expectedFailures = 4;
        Assert.Equal(expectedFailures, ProcessExecution.CountFailedExecutions(requiredSequence, actualSequence));
    }

    [Fact]
    public void Test5_MinimalCase_SingleProcess()
    {
        int[] requiredSequence = {1};
        int[] actualSequence = {1};
        var expectedFailures = 0;
        Assert.Equal(expectedFailures, ProcessExecution.CountFailedExecutions(requiredSequence, actualSequence));
    }
}
