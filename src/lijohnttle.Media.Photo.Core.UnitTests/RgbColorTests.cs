using NUnit.Framework;

namespace lijohnttle.Media.Photo.Core.UnitTests
{
    public class RgbColorTests
    {
        [TestCase(-1, 0, 0, 1)]
        [TestCase(0, -1, 0, 1)]
        [TestCase(0, 0, -1, 1)]
        [TestCase(0, 0, 0, -1)]
        [TestCase(300, 0, 0, 1)]
        [TestCase(0, 300, 0, 1)]
        [TestCase(0, 0, 300, 1)]
        [TestCase(0, 0, 0, 2)]
        public void CreatingRgbColorWithOutOfRangeComponents(int red, int green, int blue, float alpha)
        {
            Assert.Throws<System.ArgumentOutOfRangeException>(() => new RgbColor(red, green, blue, alpha));
        }
    }
}
