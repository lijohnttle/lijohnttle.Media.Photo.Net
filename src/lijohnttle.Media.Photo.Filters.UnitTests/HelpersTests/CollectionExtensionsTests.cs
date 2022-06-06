using lijohnttle.Media.Photo.Filters.Internal.Helpers;
using NUnit.Framework;

namespace lijohnttle.Media.Photo.Filters.UnitTests.HelpersTests
{
    public class CollectionExtensionsTests
    {
        [TestCase(new [] { 5 }, ExpectedResult = 5)]
        [TestCase(new [] { 1, 2 }, ExpectedResult = 2)]
        [TestCase(new[] { 2, 1 }, ExpectedResult = 2)]
        [TestCase(new [] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, ExpectedResult = 5)]
        [TestCase(new [] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }, ExpectedResult = 5)]
        [TestCase(new [] { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 0 }, ExpectedResult = 5)]
        [TestCase(new [] { 9, 8, 7, 6, 5, 4, 3, 2, 1, 0 }, ExpectedResult = 5)]
        [TestCase(new [] { 9, 1, 0, 2, 3, 4, 6, 8, 7, 10, 5 }, ExpectedResult = 5)]
        [TestCase(new[] { 1, 2, 3, 2, 1 }, ExpectedResult = 2)]
        public int QuickSelect(int[] list)
        {
            return list.QuickSelect(null);
        }
    }
}