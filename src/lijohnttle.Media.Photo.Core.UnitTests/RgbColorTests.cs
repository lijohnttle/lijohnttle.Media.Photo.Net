using NUnit.Framework;

namespace lijohnttle.Media.Photo.Core.UnitTests
{
    public class RgbColorTests
    {
        [TestCase(0, 0, 0, -1)]
        [TestCase(0, 0, 0, 2)]
        public void CreatingRgbColorWithOutOfRangeComponents(byte red, byte green, byte blue, float alpha)
        {
            Assert.Throws<System.ArgumentOutOfRangeException>(() => new RgbColor(red, green, blue, alpha));
        }
    }
}
