namespace DataStrucutreAlgo;
public class DistributionHubOptimizer
{
    //https://www.fastprep.io/problems/amazon-get-minimum-number-of-unique-distribution-centers
    // Get Minimum Number of Unique Distribution Centers
    // 👩‍🎓 NEW GRAD
    // 🏷️ OA
    //
    // RELATED PROBLEMS
    //
    // A well-known consumer brand selling everyday products on Amazon is facing a supply issue due to daily changes in product demand. To manage this, Amazon has set up n distribution hubs, each labeled with a unique number from 1 to n.
    //
    //     Amazon decides which hub to use each day by comparing the current day’s demand with the previous day’s:
    //
    // If today’s demand is higher, a hub with a larger number than yesterday’s is chosen.
    //     If today’s demand is lower, a hub with a smaller number than yesterday’s is used.
    //     If today’s demand is the same, the same hub as yesterday is selected.
    //     The total cost for the brand is based on how many different hubs are used over time. Your task is to find the minimum number of unique distribution hubs needed to handle the demand across all n days.
    //
    //     Example 1:
    //
    // Input:  n = 5, dailyTrend = [10, 20, 30, 15, 10]
    // Output: 3
    // Explanation:
    //
    // We can assign distribution hubs in the order: [1, 2, 3, 2, 1], which leads to a total cost of 3, since the distinct hubs used are [1, 2, 3].
    //
    // It’s important to note that other valid sequences of hubs can also satisfy the demand pattern. For instance, the sequence [4, 5, 8, 5, 4] is also valid and involves the unique hubs [4, 5, 8], which again results in a cost of 3.
    //
    // However, no valid arrangement can reduce the cost below 3, as that’s the minimum number of unique distribution hubs needed to match the demand changes across all days.
    //
    //     Therefore, the correct answer is 3.

    public int getMinimumNumberOfUniqueDistributionCenters(int n, int[] dailyTrend)
    {
        if (n == 0 || dailyTrend.Length == 0)
            return 0;

        int count = 1; // Start with 1 unique hub
        int prevDirection = 0; // 0 = start, 1 = up, -1 = down

        for (int i = 1; i < dailyTrend.Length; i++)
        {
            int diff = dailyTrend[i] - dailyTrend[i - 1];
            int currDirection = diff == 0 ? 0 : (diff > 0 ? 1 : -1);

            if (currDirection != 0 && currDirection != prevDirection)
            {
                count++;
                prevDirection = currDirection;
            }
        }

        return count;
    }
}

public class DistributionHubTests
{
    [Fact]
    public void Test_ExampleCase()
    {
        var sol = new DistributionHubOptimizer();
        int[] trend = { 10, 20, 30, 15, 10 };
        Assert.Equal(3, sol.getMinimumNumberOfUniqueDistributionCenters(5, trend));
    }

    [Fact]
    public void Test_ConstantTrend()
    {
        var sol = new DistributionHubOptimizer();
        int[] trend = { 50, 50, 50, 50 };
        Assert.Equal(1, sol.getMinimumNumberOfUniqueDistributionCenters(4, trend));
    }

    [Fact]
    public void Test_AllIncreasing()
    {
        var sol = new DistributionHubOptimizer();
        int[] trend = { 1, 2, 3, 4 };
        Assert.Equal(2, sol.getMinimumNumberOfUniqueDistributionCenters(4, trend));
    }

    [Fact]
    public void Test_AllDecreasing()
    {
        var sol = new DistributionHubOptimizer();
        int[] trend = { 9, 8, 7, 6 };
        Assert.Equal(2, sol.getMinimumNumberOfUniqueDistributionCenters(4, trend));
    }

    [Fact]
    public void Test_ZigZagPattern()
    {
        var sol = new DistributionHubOptimizer();
        int[] trend = { 1, 3, 1, 3, 1 };
        Assert.Equal(5, sol.getMinimumNumberOfUniqueDistributionCenters(5, trend));
    }

    [Fact]
    public void Test_SingleDay()
    {
        var sol = new DistributionHubOptimizer();
        int[] trend = { 10 };
        Assert.Equal(1, sol.getMinimumNumberOfUniqueDistributionCenters(1, trend));
    }
}
