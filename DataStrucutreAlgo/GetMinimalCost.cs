namespace DataStrucutreAlgo;

using System;
using System.Collections.Generic;

public class GetMinimalCost
 {
     public static long Handle(int[] size, int[] cost)
     {
         var n = size.Length;
         var minimalCost = 0;

        //Approach: Greedy by cost
        //Merge pair size and cost
        //Track unique size size
        //Loop thru each size
        //Find next available size, for current size adjust the size to this available adjacent

        //Merge pair size and cost
        var products = new List<(int size, int cost)>();
        for (int i = 0; i < size.Length; i++)
            products.Add((size[i], cost[i]));

        products.Sort((p1, p2) =>  p2.cost.CompareTo(p1.cost));

        Console.WriteLine(products);

        //Track used size
        var uniqueSize = new HashSet<int>();

        //Loop thru each size
        foreach (var product in products) {
            //Find next available size
            var nextAvailableSize = FindAvailableSize(uniqueSize, product.size);

            //for current size adjust the size to this next targetSize, how many times must the current size be multiplied to reach the target size
            var scaleFactor = nextAvailableSize - product.size;

            //evaluate cost
            minimalCost += scaleFactor * product.cost;

            //register available size to uniqueSize
            uniqueSize.Add(nextAvailableSize);
        }

         return minimalCost;
     }

     private static int FindAvailableSize(HashSet<int> uniqueSize, int checkSize) {
         while (uniqueSize.Contains(checkSize)) {
             checkSize++;
         }

         return checkSize;
     }
 }

public class GetMinimalCostTests
{
    [Fact]
    public void SampleCase0()
    {
        int[] size = { 3, 7, 9, 7, 8 };
        int[] cost = { 5, 2, 5, 7, 5 };
        long expected = 6;
        Assert.Equal(expected, GetMinimalCost.Handle(size, cost));
    }

    [Fact]
    public void SampleCase1()
    {
        int[] size = { 3, 3, 4, 5 };
        int[] cost = { 5, 2, 2, 1 };
        long expected = 5;
        Assert.Equal(expected, GetMinimalCost.Handle(size, cost));
    }

    [Fact]
    public void CustomCaseMinimal()
    {
        int[] size = { 2, 3, 3, 2 };
        int[] cost = { 2, 4, 5, 1 };
        long expected = 7;
        Assert.Equal(expected, GetMinimalCost.Handle(size, cost));
    }
}
