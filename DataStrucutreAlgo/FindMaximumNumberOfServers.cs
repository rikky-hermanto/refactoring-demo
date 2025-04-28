using System.ComponentModel;

namespace DataStrucutreAlgo;

public class ServerSelection
{
    //https://github.com/perixtar/2025-Tech-OA-by-FastPrep?tab=readme-ov-file
    //https://www.fastprep.io/problems/amazon-find-maximum-number-of-servers
    //https://leetcode.com/problems/longest-harmonious-subsequence/description/

    // Given a list of n servers, with the computing power of ith server at powers[i]. A client wants to buy some K servers out of these n servers. The condition is that when these K servers are rearranged, the absolute difference between the computing powers of two adjacent servers should be less then or equal to 1. Also, these servers will form a circular network. So the first and last servers will also be considered adjacent. Find the maximum number of servers K, which the client can buy.
    //
    //     Example 1:
    //
    // Input:  powers = [4, 3, 5, 1, 2, 2, 1]
    // Output: 5
    // Explanation:
    //
    //
    // Client can buy 5 servers -> {3, 1, 2, 2, 1} and rearrange them to {2, 1, 1, 2, 3}.

    // DSA Mapping & Intuition
    //     Pattern: Counting frequencies → grouping by value → check adjacent value pairs.
    //
    //     DSA Chosen: / Hash-map frequency (or count array) → count[v] = number of servers with power v.
    //
    //     For each key v, consider groupSize = count[v] + count[v+1]. Also consider count[v] alone.
    //
    //     Answer = max(groupSize) over all v.
    //
    //     Intuition: Most cases you only care about two adjacent integer values, so aggregating counts and taking the best pair is O(1) per distinct value.
    //     Three-value window: when there are enough middle-value servers (powerValue+1) to interleave both the lower (powerValue) and higher (powerValue+2) ones—and
    //      you require at least 2 of the middle value to actually separate extremes in a circle.

    public static int FindMaximumNumberOfServers(int[] powers)
    {
        // 1) Build a frequency map: powerValue → how many servers have that power
        var frequencyByPower = new Dictionary<int,int>();
        foreach (var power in powers) {
            if (!frequencyByPower.ContainsKey(power))
                frequencyByPower[power] = 0;

            frequencyByPower[power]++;
        }

        var maxServers = 0;

        // 2) For each distinct power value…
        foreach (var server in frequencyByPower) {
            var powerValue        = server.Key;
            var countAtPower      = server.Value;
            var bestForThisPower  = countAtPower;  // at least all servers with this exact power

            // 2a) Two-value window: handle the two-value window (powerValue and powerValue+1) in all cases.
            if (frequencyByPower.TryGetValue(powerValue + 1, out var countAtNextPower)) {
                bestForThisPower = Math.Max(bestForThisPower, countAtPower + countAtNextPower);
            }

            // 2b) Three-value window: You only allow a three‐value window (powerValue, powerValue+1, powerValue+2)
            // when there are enough middle-value servers (powerValue+1) to interleave both the lower (powerValue) and higher (powerValue+2) ones—and
            // you require at least 2 of the middle value to actually separate extremes in a circle.

            // Only valid if there are enough “middle” servers to interleave both ends
            if (frequencyByPower.TryGetValue(powerValue + 1, out countAtNextPower)
                && frequencyByPower.TryGetValue(powerValue + 2, out var countAtNextNextPower)) {
                // need at least 2 of the middle power, and enough to separate both extremes
                var enoughMiddleToSeparate = countAtNextPower > 1
                                             && countAtNextPower >= Math.Max(countAtPower, countAtNextNextPower);
                if (enoughMiddleToSeparate) {
                    var threeValueGroupSize = countAtPower + countAtNextPower + countAtNextNextPower;
                    bestForThisPower = Math.Max(bestForThisPower, threeValueGroupSize);
                }
            }

            maxServers = Math.Max(maxServers, bestForThisPower);
        }

        return maxServers;
    }
}

public class FindMaximumNumberOfServersTests
{
    [Fact]
    public void Test1()
    {
        int[] powers = { 4, 3, 5, 1, 2, 2, 1 };
        var expected = 5;
        var actual = ServerSelection.FindMaximumNumberOfServers(powers);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Test2()
    {
        int[] powers = { 1, 2, 3, 4, 5 };
        var expected = 2;
        var actual = ServerSelection.FindMaximumNumberOfServers(powers);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Test3()
    {
        int[] powers = { 1, 1, 1, 1, 1 };
        var expected = 5;
        var actual = ServerSelection.FindMaximumNumberOfServers(powers);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Test4()
    {
        int[] powers = { 1, 3, 5, 7, 9 };
        var expected = 1;
        var actual = ServerSelection.FindMaximumNumberOfServers(powers);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Test5()
    {
        int[] powers = { 2, 2, 2, 3, 3, 3, 3 };
        var expected = 7;
        var actual = ServerSelection.FindMaximumNumberOfServers(powers);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Test6()
    {
        int[] powers = { 1 };
        var expected = 1;
        var actual = ServerSelection.FindMaximumNumberOfServers(powers);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Test7()
    {
        int[] powers = { 1, 2, 2, 2, 3 };
        var expected = 5;
        var actual = ServerSelection.FindMaximumNumberOfServers(powers);
        Assert.Equal(expected, actual);
    }
}