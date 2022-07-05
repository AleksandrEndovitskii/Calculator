using System;
using System.Collections.Generic;
using System.Linq;

public static class ListExtensions
{
    private static readonly Random _random = new Random();

    public static string ToString<T>(this List<T> list)
    {
        var result = string.Join(", ", list);
        result = "[" + result + "]";

        return result;
    }

    /// <summary>
    /// Does a list contain all values of another list?
    /// </summary>
    /// <remarks>Needs .NET 3.5 or greater.  Source:  https://stackoverflow.com/a/1520664/1037948 </remarks>
    /// <typeparam name="T">list value type</typeparam>
    /// <param name="containingList">the larger list we're checking in</param>
    /// <param name="lookupList">the list to look for in the containing list</param>
    /// <returns>true if it has everything</returns>
    public static bool ContainsAll<T>(this IEnumerable<T> containingList, IEnumerable<T> lookupList)
    {
        return !lookupList.Except(containingList).Any();
    }

    public static T GetRandomItem<T>(this List<T> list)
    {
        var randomIndex = list.GetRandomIndex();
        var randomElement = list[randomIndex];
        return randomElement;
    }

    public static IEnumerable<T> GetRandomItems<T>(this IEnumerable<T> source, int randomItemsCount)
    {
        if (!source.Any())
        {
            return source;
        }

        var shuffledList = source.Shuffle().ToList();
        if (shuffledList.Count < randomItemsCount)
        {
            randomItemsCount = shuffledList.Count;
        }

        var truncatedShuffledList = shuffledList.GetRange(0, randomItemsCount);

        return truncatedShuffledList;
    }

    public static int GetRandomIndex<T>(this IEnumerable<T> source)
    {
        var randomIndex = UnityEngine.Random.Range(0, source.Count());
        return randomIndex;
    }

    public static IEnumerable<int> GetRandomIndexes<T>(this IEnumerable<T> source, int randomIndexesCount)
    {
        var indexes = new List<int>();
        for (var i = 0; i < source.Count(); i++)
        {
            indexes.Add(i);
        }

        var shuffledIndexes = indexes.Shuffle();
        var truncatedShuffledIndexes = shuffledIndexes.ToList().GetRange(0, randomIndexesCount);

        return truncatedShuffledIndexes;
    }

    public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
    {
        var result = source.OrderBy((item) => _random.Next());
        return result;
    }

    /// <summary>
    /// Splits a <see cref="List{T}"/> into multiple chunks.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list">The list to be chunked.</param>
    /// <param name="chunkSize">The size of each chunk.</param>
    /// <returns>A list of chunks.</returns>
    public static List<List<T>> SplitIntoChunks<T>(this List<T> list, int chunkSize)
    {
        if (chunkSize <= 0)
        {
            throw new ArgumentException("chunkSize must be greater than 0.");
        }

        var retVal = new List<List<T>>();
        var index = 0;
        while (index < list.Count)
        {
            var count = list.Count - index > chunkSize ? chunkSize : list.Count - index;
            retVal.Add(list.GetRange(index, count));

            index += chunkSize;
        }

        return retVal;
    }

    public static IEnumerable<T> TakeLast<T>(this IEnumerable<T> source, int itemsCount)
    {
        return source.Skip(Math.Max(0, source.Count() - itemsCount));
    }
}
