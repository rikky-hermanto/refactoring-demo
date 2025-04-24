namespace DataStrucutreAlgo;

public class ShoppingOptions
{
    public static long GetNumberOfOptions(List<int> priceOfJeans,
        List<int> priceOfShoes,
        List<int> priceOfSkirts,
        List<int> priceOfTops,
        int dollars) {
        var options = 0;



        // //Bruteforce solution
        // foreach (var jeans in priceOfJeans) {
        //     foreach (var shoes in priceOfShoes) {
        //         foreach (var skirts in priceOfSkirts) {
        //             foreach (var tops in priceOfTops) {
        //                 var prevPairs = jeans + shoes + skirts;
        //
        //                 //two sums?  complement = dollars - prevPairs
        //                 var complement = dollars - prevPairs;
        //                 if (prevPairs + tops <= dollars) {
        //                     options++;
        //                 }
        //             }
        //         }
        //     }
        // }

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
        if (skirtsTopsPrices.Count > jeansShoesPrices.Count) {
            skirtsTopsPrices.Sort();
            foreach (var jeansShoesPrice in jeansShoesPrices) {
                var complement = dollars - jeansShoesPrice;
                options += Binary(skirtsTopsPrices, complement);
            }
        }
        else {
            jeansShoesPrices.Sort();
            foreach (var skirtsTopsPrice in skirtsTopsPrices) {
                var complement = dollars - skirtsTopsPrice;
                options += Binary(jeansShoesPrices, complement);
            }
        }


        return options;
    }

    private static int Binary(List<int> nums, int target) {
        int left = 0;
        int right = nums.Count - 1;

        while (left <= right) {
            int mid = left + (right - left) / 2;
            if (nums[mid] <= target) {
                //find upbound if price similar
                while (mid<nums.Count && nums[mid] <= target) {
                    mid++;
                }
                return mid;
            }
            else if(nums[mid] > target) { //masih kebesaran geser kiri
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
