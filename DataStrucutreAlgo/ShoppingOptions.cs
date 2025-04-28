namespace DataStrucutreAlgo;

public class ShoppingOptions
{
    //https://www.notion.so/Shopping-Options-1e1b578e7a1c80038d59f55756e73a3a
    public static long GetNumberOfOptions(List<int> priceOfJeans,
        List<int> priceOfShoes,
        List<int> priceOfSkirts,
        List<int> priceOfTops,
        int dollars) {

        //Core Problem: Pair integers of 4 sets, sums to dollars. Similar to 4sum case
        //Constraints Analysis: -
        //Mental Flow cart Example:
        //DSA Flowchart Analysis: Inputs unsorted, not unique. Sorting might help.
        //Intuition/Approach: Meet in the middle, this similar to 4 sums.
        // - 4 sets, pair 2 sets each, sums: Jeans x Shoes, Skirts x Tops
        // - Now, do TwoSums trick,
        //      sort Skirts x Tops,
        //      do binary search two sums

        // DOABLE BUT NOT OPTIMAL:   assign each value Jeans x Shoes, compare againts sorted SkirtsTop,
        //          define complement by nums[i] to target, track nums[i] to complements HashMap
        //          return the pair.

        //Choosen DSA: Meet in the middle, Sort + HashMap

        //Semi binary: two sums of two-pairs
        List<int> jeansShoesPrices = new List<int>();
        foreach (var priceOfJean in priceOfJeans) {
            foreach (var priceOfShoe in priceOfShoes) {
                if (priceOfJean + priceOfShoe <= dollars) {
                    jeansShoesPrices.Add(priceOfJean + priceOfShoe);
                }

            }
        }

        List<int> skirtsTopsPrices = new List<int>();
        foreach (var priceOfSkirt in priceOfSkirts) {
            foreach (var priceOfTop in priceOfTops) {
                if (priceOfSkirt + priceOfTop <= dollars) {
                    skirtsTopsPrices.Add(priceOfSkirt + priceOfTop);
                }

            }
        }

        //Do two sums binary, sort most item.
        var options = 0;
        if (skirtsTopsPrices.Count > jeansShoesPrices.Count) {
            skirtsTopsPrices.Sort();
            foreach (var jeansShoesPrice in jeansShoesPrices) {
                var complement = dollars - jeansShoesPrice;
                options += TwoSums(skirtsTopsPrices, complement);
            }
        }
        else {
            jeansShoesPrices.Sort();
            foreach (var skirtsTopsPrice in skirtsTopsPrices) {
                var complement = dollars - skirtsTopsPrice;
                options += TwoSums(jeansShoesPrices, complement);
            }
        }


        return options;
    }

    private static int TwoSums(List<int> combo2, int target) {
        int left = 0;
        int right = combo2.Count - 1;

        while (left <= right) {
            int mid = left + (right - left) / 2;
            if (combo2[mid] <= target) {
                //find upper bound if price similar
                while (mid<combo2.Count && combo2[mid] <= target) {
                    mid++;
                }

                return mid; //this mid is the max options we can choose for price less than target to match the dollars
            }
            else if(combo2[mid] > target) { //masih kebesaran geser kiri
                right = mid - 1;
            }
        }

        return left;
    }
}

public class ShoppingOptionsTests
{
    [Fact]
    public void ExampleCase()
    {
        Assert.Equal(4, ShoppingOptions.GetNumberOfOptions(
            new List<int>{2,3},
            new List<int>{4},
            new List<int>{2,3},
            new List<int>{1,2},
            10));
    }

    [Fact]
    public void ExactMatch()
    {
        Assert.Equal(1, ShoppingOptions.GetNumberOfOptions(
            new List<int>{5},
            new List<int>{5},
            new List<int>{5},
            new List<int>{5},
            20));
    }

    [Fact]
    public void NoCombinations()
    {
        Assert.Equal(0, ShoppingOptions.GetNumberOfOptions(
            new List<int>{10},
            new List<int>{10},
            new List<int>{10},
            new List<int>{10},
            5));
    }

    [Fact]
    public void OneListEmpty()
    {
        Assert.Equal(0, ShoppingOptions.GetNumberOfOptions(
            new List<int>{1},
            new List<int>{1},
            new List<int>(),
            new List<int>{1},
            5));
    }

    [Fact]
    public void OnlyOneWay()
    {
        Assert.Equal(1, ShoppingOptions.GetNumberOfOptions(
            new List<int>{1},
            new List<int>{1},
            new List<int>{1},
            new List<int>{1},
            4));
    }

    [Fact]
    public void DuplicatePrices()
    {
        Assert.Equal(16, ShoppingOptions.GetNumberOfOptions(
            new List<int>{2,2},
            new List<int>{2,2},
            new List<int>{2,2},
            new List<int>{2,2},
            10));
    }

    [Fact]
    public void LargeBudget()
    {
        Assert.Equal(16, ShoppingOptions.GetNumberOfOptions(
            new List<int>{1,2},
            new List<int>{1,2},
            new List<int>{1,2},
            new List<int>{1,2},
            100));
    }

    [Fact]
    public void AllZeroPrices()
    {
        Assert.Equal(16, ShoppingOptions.GetNumberOfOptions(
            new List<int>{0,0},
            new List<int>{0,0},
            new List<int>{0,0},
            new List<int>{0,0},
            0));
    }

    [Fact]
    public void HighPrices()
    {
        Assert.Equal(0, ShoppingOptions.GetNumberOfOptions(
            new List<int>{1000},
            new List<int>{1000},
            new List<int>{1000},
            new List<int>{1000},
            100));
    }

    [Fact]
    public void OneItemPerCategory()
    {
        Assert.Equal(1, ShoppingOptions.GetNumberOfOptions(
            new List<int>{1},
            new List<int>{1},
            new List<int>{1},
            new List<int>{1},
            4));
    }
}
