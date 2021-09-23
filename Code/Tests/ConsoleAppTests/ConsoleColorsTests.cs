using ConsoleApp;
using NUnit.Framework;

namespace Tests;

internal class ConsoleColorsTests
{
    [TestCase(0UL)]
    [TestCase(2UL)]
    [TestCase(4UL)]
    [TestCase(8UL)]
    [TestCase(16UL)]
    [TestCase(32UL)]
    [TestCase(64UL)]
    [TestCase(1024UL)]
    [TestCase(131072UL)]
    [TestCase(8388608UL)]
    // If number is power of two or zero, method must returns System.Drawing.Color
    public void FontColor_BasicFunctionality(ulong num)
    {
        var color = Colors.FontColor(num, new ColorSettings());
        Assert.IsNotNull(color);
    }

    [TestCase(3UL)]
    [TestCase(5UL)]
    [TestCase(99UL)]
    [TestCase(1000001UL)]
    // If number is not power of two or zero, method must returns ArgumentOutOfRangeException
    public void FontColor_NotPowerOfTwoThrowsException(ulong num)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => Colors.FontColor(num, new ColorSettings()));
    }
}
