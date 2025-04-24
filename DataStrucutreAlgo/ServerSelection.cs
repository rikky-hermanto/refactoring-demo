namespace DataStrucutreAlgo;

public class ServerSelection
{
    public static int MaxServers(int[] powers)
    {
        if (powers == null || powers.Length == 0)
            return 0;

        Array.Sort(powers);
        int maxK = 1;
        int left = 0;

        for (int right = 1; right < powers.Length; right++)
        {
            while (powers[right] - powers[left] > 1)
            {
                left++;
            }
            maxK = Math.Max(maxK, right - left + 1);
        }

        return maxK;
    }
}

public class ServerSelectionTests
{
    [Fact]
    public void Test1()
    {
        int[] powers = { 4, 3, 5, 1, 2, 2, 1 };
        int expected = 5;
        int actual = ServerSelection.MaxServers(powers);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Test2()
    {
        int[] powers = { 1, 2, 3, 4, 5 };
        int expected = 2;
        int actual = ServerSelection.MaxServers(powers);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Test3()
    {
        int[] powers = { 1, 1, 1, 1, 1 };
        int expected = 5;
        int actual = ServerSelection.MaxServers(powers);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Test4()
    {
        int[] powers = { 1, 3, 5, 7, 9 };
        int expected = 1;
        int actual = ServerSelection.MaxServers(powers);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Test5()
    {
        int[] powers = { 2, 2, 2, 3, 3, 3, 3 };
        int expected = 7;
        int actual = ServerSelection.MaxServers(powers);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Test6()
    {
        int[] powers = { 1 };
        int expected = 1;
        int actual = ServerSelection.MaxServers(powers);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Test7()
    {
        int[] powers = { 1, 2, 2, 2, 3 };
        int expected = 4;
        int actual = ServerSelection.MaxServers(powers);
        Assert.Equal(expected, actual);
    }
}