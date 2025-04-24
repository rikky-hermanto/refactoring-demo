namespace DataStrucutreAlgo;

public class NMusicShow
{
    public static int MaxVolume(int[] startTime, int[] duration, int[] volume)
    {
        int n = startTime.Length;

        // Step 1: Prepare intervals and sort by end time
        var shows = new (int start, int end, int volume)[n];
        for (int i = 0; i < n; i++)
        {
            shows[i] = (startTime[i], startTime[i] + duration[i], volume[i]);
        }

        // Sort shows by end time
        var sortedShows = shows.OrderBy(s => s.end).ToArray();

        // Step 2: Dynamic programming array
        var dp = new int[n];
        dp[0] = sortedShows[0].volume;  // First show volume

        // Step 3: Function to find the latest non-overlapping show
        int FindLastNonOverlapping(int i)
        {
            int low = 0, high = i - 1;
            while (low <= high)
            {
                int mid = (low + high) / 2;
                if (sortedShows[mid].end <= sortedShows[i].start)
                {
                    if (sortedShows[mid + 1].end <= sortedShows[i].start)
                        low = mid + 1;
                    else
                        return mid;
                }
                else
                {
                    high = mid - 1;
                }
            }
            return -1;
        }

        // Step 4: Fill DP array
        for (int i = 1; i < n; i++)
        {
            // Case 1: Exclude the current show
            dp[i] = dp[i - 1];

            // Case 2: Include the current show
            int prev = FindLastNonOverlapping(i);
            int includeVolume = sortedShows[i].volume + (prev != -1 ? dp[prev] : 0);

            dp[i] = Math.Max(dp[i], includeVolume);
        }

        // The last value in dp will be the maximum volume achievable
        return dp[n - 1];
    }


    public class NMusicShowTests
    {
        [Fact]
        public void TestCase1()
        {
            int[] startTime = { 10, 5, 15, 18, 30 };
            int[] duration = { 30, 12, 20, 35, 35 };
            int[] volume = { 50, 51, 20, 25, 10 };

            var result = NMusicShow.MaxVolume(startTime, duration, volume);

            Assert.Equal(76, result);  // Expected output is 76
        }

        [Fact]
        public void TestCase3()
        {
            int[] startTime = { 1, 5, 10, 15 };
            int[] duration = { 10, 5, 5, 5 };
            int[] volume = { 10, 20, 30, 40 };

            var result = NMusicShow.MaxVolume(startTime, duration, volume);

            Assert.Equal(70, result);  // Best choice is to take shows at 1st and 4th indices
        }

        [Fact]
        public void TestCase4()
        {
            int[] startTime = { 1, 3, 6, 10 };
            int[] duration = { 5, 3, 4, 5 };
            int[] volume = { 50, 60, 30, 40 };

            var result = NMusicShow.MaxVolume(startTime, duration, volume);

            Assert.Equal(100, result);  // Best combination: 1st and 4th shows
        }

        [Fact]
        public void TestCase5()
        {
            int[] startTime = { 1, 3, 7, 10, 15 };
            int[] duration = { 5, 4, 2, 6, 7 };
            int[] volume = { 10, 20, 30, 40, 50 };

            var result = NMusicShow.MaxVolume(startTime, duration, volume);

            Assert.Equal(90, result);  // Best combination: 1st and 5th shows
        }
    }

}