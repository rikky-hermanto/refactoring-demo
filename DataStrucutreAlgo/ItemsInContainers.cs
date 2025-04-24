namespace DataStrucutreAlgo;

public class ItemsInContainers
{
    public static int[] TestNumberOfItems(string s, int[] startIndices, int[]  endIndices) {
        var n = s.Length;
        if (n == 0)
            return new int[]{0};

        //build prev closers indexes
        var prevClosers = new int[s.Length];
        var prevCloser = -1;
        for (var i = 0; i < s.Length; i++) {
            if (s[i] == '|')
                prevCloser = i;

            prevClosers[i] = prevCloser;
        }

        //build next openers indexes
        var nextOpeners = new int[s.Length];
        var nextOpener = -1;
        for (var i = s.Length - 1 ; i >= 0; i--) {
            if (s[i] == '|')
                nextOpener = i;

            nextOpeners[i] = nextOpener;
        }

        // Build prefix sum of items
        var prefixSum = new int[s.Length];
        var itemCount = 0;
        var opened = false;
        for (var i = 0; i < s.Length; i++) {
            if (s[i] == '|') {
                opened = true;
            }
            else if (s[i] == '*' && opened) {
                itemCount++;
            }

            prefixSum[i] = itemCount;
        }

        // Query answers
        var ans = new int[startIndices.Length];
        for (var i = 0; i < startIndices.Length; i++) {
            var fromStartIndex = startIndices[i] - 1;
            var fromEndIndex = endIndices[i] - 1;

            var leftBound = nextOpeners[fromStartIndex];
            var rightBound  = prevClosers[fromEndIndex];
            if (leftBound != -1 && rightBound != -1 && leftBound < rightBound)
            {
                ans[i] = prefixSum[rightBound] - prefixSum[leftBound];
            }
            else
            {
                //none other wise
                ans[i] = 0;
            }
        }

        return ans;
    }
}


public class ItemsInContainersTests
{
    [Fact]
    public void TestCase1_BasicExample()
    {
        var result = ItemsInContainers.TestNumberOfItems("|**|*|*", new int[]{1, 1,3}, new int[]{5, 6,5});
        Assert.Equal(new int[]{2, 3,0}, result);
    }

    [Fact]
    public void TestCase2_SingleCompartment()
    {
        var result = ItemsInContainers.TestNumberOfItems("|**|", new int[]{1}, new int[]{4});
        Assert.Equal(new int[]{2}, result);
    }

    [Fact]
    public void TestCase3_NoCompartment()
    {
        var result = ItemsInContainers.TestNumberOfItems("****", new int[]{1}, new int[]{4});
        Assert.Equal(new int[]{0}, result);
    }

    [Fact]
    public void TestCase4_OnlyPipesNoItems()
    {
        var result = ItemsInContainers.TestNumberOfItems("||||", new int[]{1}, new int[]{4});
        Assert.Equal(new int[]{0}, result);
    }

    [Fact]
    public void TestCase5_CompartmentAtEdges()
    {
        var result = ItemsInContainers.TestNumberOfItems("|*|*|", new int[]{1}, new int[]{5});
        Assert.Equal(new int[]{2}, result); // only * between first pair of pipes
    }

    [Fact]
    public void TestCase6_PartialCompartmentOutsideRange()
    {
        var result = ItemsInContainers.TestNumberOfItems("*|*|*", new int[]{2}, new int[]{4});
        Assert.Equal(new int[]{1}, result); // only one compartment between indices 2-4
    }

    [Fact]
    public void TestCase7_EmptyString()
    {
        var result = ItemsInContainers.TestNumberOfItems("", new int[]{1}, new int[]{1});
        Assert.Equal(new int[]{0}, result);
    }

    [Fact]
    public void TestCase8_OneCharacterString()
    {
        var result = ItemsInContainers.TestNumberOfItems("|", new int[]{1}, new int[]{1});
        Assert.Equal(new int[]{0}, result);
    }

    [Fact]
    public void TestCase9_LongStringWithMultipleCompartments()
    {
        var result = ItemsInContainers.TestNumberOfItems("|*|*|**|*|", new int[]{1, 1, 2}, new int[]{10, 7, 5});
        Assert.Equal(new int[]{5, 2, 1}, result);
    }

    [Fact]
    public void TestCase10_CompartmentEndsExactlyOnRangeEnd()
    {
        var result = ItemsInContainers.TestNumberOfItems("*|**|", new int[]{2}, new int[]{5});
        Assert.Equal(new int[]{2}, result); // compartment starts at 2, ends at 5
    }

    [Fact]
    public void TestCase11_AllItemsNoPipes()
    {
        var result = ItemsInContainers.TestNumberOfItems("*****", new int[]{1, 2}, new int[]{5, 4});
        Assert.Equal(new int[]{0, 0}, result);
    }

    [Fact]
    public void TestCase12_SingleItemBetweenPipes()
    {
        var result = ItemsInContainers.TestNumberOfItems("|*|", new int[]{1, 2, 3}, new int[]{3, 3, 3});
        Assert.Equal(new int[]{1, 0, 0}, result);
    }

    [Fact]
    public void TestCase13_NestedCompartments()
    {
        var result = ItemsInContainers.TestNumberOfItems("|*|**|***|", new int[]{1, 2, 4}, new int[]{10, 7, 9});
        Assert.Equal(new int[]{6, 2, 0}, result);
    }

    [Fact]
    public void TestCase14_AlternatingPipesAndItems()
    {
        var result = ItemsInContainers.TestNumberOfItems("*|*|*|*|*", new int[]{1, 3, 5}, new int[]{9, 7, 5});
        Assert.Equal(new int[]{3, 1, 0}, result);
    }

    [Fact]
    public void TestCase15_LargeRangeWithSingleCompartment()
    {
        var result = ItemsInContainers.TestNumberOfItems("***|*****|***", new int[]{1, 4, 9}, new int[]{13, 8, 13});
        Assert.Equal(new int[]{5, 0, 0}, result);
    }

    [Fact]
    public void TestCase16_QueryStartAndEndOnSamePipe()
    {
        var result = ItemsInContainers.TestNumberOfItems("|**|*|*|", new int[]{1, 3, 5}, new int[]{1, 3, 5});
        Assert.Equal(new int[]{0, 0, 0}, result);
    }

    [Fact]
    public void TestCase17_QueryStartOnItemEndOnPipe()
    {
        var result = ItemsInContainers.TestNumberOfItems("|**|*|*|", new int[]{2, 5}, new int[]{3, 7});
        Assert.Equal(new int[]{0, 0}, result);
    }

    [Fact]
    public void TestCase18_QueryStartOnPipeEndOnItem()
    {
        var result = ItemsInContainers.TestNumberOfItems("|**|*|*|", new int[]{1, 4}, new int[]{3, 6});
        Assert.Equal(new int[]{0, 1}, result);
    }

    [Fact]
    public void TestCase19_MultipleCompartmentsWithEmptyOnes()
    {
        var result = ItemsInContainers.TestNumberOfItems("||*||*|*||", new int[]{1, 3, 6}, new int[]{10, 5, 9});
        Assert.Equal(new int[]{3, 0, 1}, result);
    }

    [Fact]
    public void TestCase20_ComplexPatternWithMultipleQueries()
    {
        var result = ItemsInContainers.TestNumberOfItems("*|**|***|****|*****|",
            new int[]{1, 2, 3, 4, 5},
            new int[]{20, 19, 18, 17, 16});

        Assert.Equal(new int[]{14, 9, 7, 7, 7}, result);
    }
}