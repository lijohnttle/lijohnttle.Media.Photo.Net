using NUnit.Framework;
using System;

namespace lijohnttle.Media.Photo.Core.UnitTests
{
    public class BitmapImageTests
    {
        [Test]
        public void GetPixelColorReturnsCorrectColors()
        {
            RgbColor[,] data = new[,]
            {
                {
                    new RgbColor(0, 0, 0),
                    new RgbColor(16, 32, 48, 0.9f),
                    new RgbColor(66, 77, 88),
                },
                {
                    new RgbColor(100, 101, 102),
                    new RgbColor(200, 220, 255, 0),
                    new RgbColor(255, 255, 255),
                }
            };

            var image = new BitmapImage(data);

            Assert.AreEqual(image.GetPixelColor(0, 0), data[0, 0]);
            Assert.AreEqual(image.GetPixelColor(0, 1), data[0, 1]);
            Assert.AreEqual(image.GetPixelColor(0, 2), data[0, 2]);
            Assert.AreEqual(image.GetPixelColor(1, 0), data[1, 0]);
            Assert.AreEqual(image.GetPixelColor(1, 1), data[1, 1]);
            Assert.AreEqual(image.GetPixelColor(1, 2), data[1, 2]);
            Assert.AreNotEqual(image.GetPixelColor(0, 0), data[1, 1]);
        }
    }
}