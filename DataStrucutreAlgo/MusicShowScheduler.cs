namespace DataStrucutreAlgo;

// **Problem 1:**
//
// There is a list of N music shows. Each show has a start time, duration and volume of people attending the show.
// You need to find the maximum total volume of non overlapping intervals.
//
// Ex.
// startTime = {10,5,15,18,30}
// duration = {30,12,20,35,35}
// volume = {50,51,20,25,10}
//
// Output: 76
// Here, we can select 2 intervals: 1st starting at 5 with duration 15, 2nd starting at 18 with duration 35
// Total volume of them is 51+25 = 76, which is the maximum possible solution.

public class MusicShowScheduler
{
    public int MaxTotalVolume(int[] startTime, int[] duration, int[] volume)
    {
        var shows = new List<Show>();
        for (var i = 0; i < startTime.Length; i++)
        {
            shows.Add(new Show
            {
                StartTime = startTime[i],
                Duration = duration[i],
                Volume = volume[i]
            });
        }

        // Sort by end time
        shows.Sort((a, b) => a.EndTime.CompareTo(b.EndTime));

        // dp[i]: max volume up to show[i]
        var n = shows.Count;
        var maxVolumeUpToShow = new int[n];
        maxVolumeUpToShow[0] = shows[0].Volume;

        for (var i = 1; i < n; i++)
        {
            var volumeIfTakingThisShow  = shows[i].Volume;

            // Find last non-overlapping show
            var lastNonOverlappingIndex = FindLastNonOverlapping(shows, i);
            if (lastNonOverlappingIndex != -1)
                volumeIfTakingThisShow  += maxVolumeUpToShow[lastNonOverlappingIndex];

            maxVolumeUpToShow[i] = Math.Max(maxVolumeUpToShow[i - 1], volumeIfTakingThisShow );
        }

        return maxVolumeUpToShow[n - 1];
    }

    private int FindLastNonOverlapping(List<Show> shows, int currentIndex)
    {
        int low = 0, high = currentIndex - 1;
        while (low <= high)
        {
            var mid = (low + high) / 2;
            if (shows[mid].EndTime <= shows[currentIndex].StartTime)
            {
                if (mid == high || shows[mid + 1].EndTime > shows[currentIndex].StartTime)
                    return mid;
                low = mid + 1;
            }
            else
            {
                high = mid - 1;
            }
        }
        return -1;
    }
}

public class Show
{
    public int StartTime { get; set; }
    public int Duration { get; set; }
    public int Volume { get; set; }

    public int EndTime => StartTime + Duration;
}

public class MusicShowSchedulerTests
{
    [Fact]
    public void TestExampleCase()
    {
        var scheduler = new MusicShowScheduler();
        int[] startTime = { 10, 5, 15, 18, 30 };
        int[] duration = { 30, 12, 20, 35, 35 };
        int[] volume = { 50, 51, 20, 25, 10 };
        var result = scheduler.MaxTotalVolume(startTime, duration, volume);
        Assert.Equal(76, result);
    }

    [Fact]
    public void TestNonOverlappingAllSelected()
    {
        var scheduler = new MusicShowScheduler();
        int[] startTime = { 0, 10, 20 };
        int[] duration = { 5, 5, 5 };
        int[] volume = { 10, 20, 30 };
        var result = scheduler.MaxTotalVolume(startTime, duration, volume);
        Assert.Equal(60, result);
    }

    [Fact]
    public void TestAllOverlapPickBest()
    {
        var scheduler = new MusicShowScheduler();
        int[] startTime = { 0, 1, 2 };
        int[] duration = { 10, 10, 10 };
        int[] volume = { 10, 50, 30 };
        var result = scheduler.MaxTotalVolume(startTime, duration, volume);
        Assert.Equal(50, result);
    }

    [Fact]
    public void TestSomeOverlap()
    {
        var scheduler = new MusicShowScheduler();
        int[] startTime = { 1, 3, 6, 2 };
        int[] duration = { 3, 4, 5, 4 };
        int[] volume = { 20, 50, 60, 30 };
        var result = scheduler.MaxTotalVolume(startTime, duration, volume);
        Assert.Equal(90, result); // correct expected value
    }

    [Fact]
    public void TestSingleShow()
    {
        var scheduler = new MusicShowScheduler();
        int[] startTime = { 5 };
        int[] duration = { 10 };
        int[] volume = { 100 };
        var result = scheduler.MaxTotalVolume(startTime, duration, volume);
        Assert.Equal(100, result);
    }

    [Fact]
    public void TestOthersOverlappedWithBiggestInMiddle()
    {
        var scheduler = new MusicShowScheduler();
        int[] startTime = { 1, 4, 6 };
        int[] duration = { 5, 3, 2  };
        int[] volume = {  1, 100, 1  };
        var result = scheduler.MaxTotalVolume(startTime, duration, volume);
        Assert.Equal(100, result);
    }
}
