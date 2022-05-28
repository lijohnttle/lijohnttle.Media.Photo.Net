using NUnit.Framework;
using System;

namespace lijohnttle.Media.Photo.Core.UnitTests
{
    public class BitmapImageTests
    {
        [Test]
        public void CreatingBitmapImageWithInconsistentWidthThrowsException()
        {
            RgbColor[] data = new[]
            {
                new RgbColor(0, 0, 0),
                new RgbColor(0, 0, 0),
                new RgbColor(0, 0, 0),
                new RgbColor(0, 0, 0),
            };

            Assert.Throws<InvalidOperationException>(() => new BitmapImage(data, 5, 2));
        }

        [Test]
        public void CreatingBitmapImageWithInconsistentHeightThrowsException()
        {
            RgbColor[] data = new[]
            {
                new RgbColor(0, 0, 0),
                new RgbColor(0, 0, 0),
                new RgbColor(0, 0, 0),
                new RgbColor(0, 0, 0),
            };

            Assert.Throws<InvalidOperationException>(() => new BitmapImage(data, 2, 5));
        }

        [Test]
        public void GetPixelColorReturnsCorrectColors()
        {
            RgbColor[] data = new[]
            {
                new RgbColor(0, 0, 0),
                new RgbColor(16, 32, 48, 0.9f),
                new RgbColor(100, 101, 102),
                new RgbColor(200, 220, 255, 0)
            };

            var image = new BitmapImage(data, 2, 2);

            Assert.AreEqual(image.GetPixelColor(0, 0), data[0]);
            Assert.AreEqual(image.GetPixelColor(1, 0), data[1]);
            Assert.AreEqual(image.GetPixelColor(0, 1), data[2]);
            Assert.AreEqual(image.GetPixelColor(1, 1), data[3]);
            Assert.AreNotEqual(image.GetPixelColor(0, 0), data[1]);
        }
    }
}