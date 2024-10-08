using Bowling.Domain;
using FluentAssertions;
using NUnit.Framework;

namespace Bowling.Tests;

[TestFixture]
public class BowlingGameTests
{
    private BowlingGame _game;

    [OneTimeSetUp]
    public void Setup()
    {
        _game = new BowlingGame();
    }

    [OneTimeTearDown]
    public void Teardown()
    {
        _game.Dispose();
    }

    [TestCase(4, "1 | [(4)4]")]
    [TestCase(5, "1 | [(4,5)9]")]
    [TestCase(6, "2 | [(4,5)9], [(6)15]")]
    [TestCase(3, "2 | [(4,5)9], [(6,3)18]")]
    [TestCase(10, "3 | [(4,5)9], [(6,3)18], [(10)28]")]
    [TestCase(4, "4 | [(4,5)9], [(6,3)18], [(10)32], [(4)36]")]
    [TestCase(6, "4 | [(4,5)9], [(6,3)18], [(10)38], [(4,6)48]")]
    [TestCase(7, "5 | [(4,5)9], [(6,3)18], [(10)38], [(4,6)55], [(7)62]")]
    [TestCase(2, "5 | [(4,5)9], [(6,3)18], [(10)38], [(4,6)57], [(7,2)66]")]
    [TestCase(8, "6 | [(4,5)9], [(6,3)18], [(10)38], [(4,6)57], [(7,2)66], [(8)74]")]
    [TestCase(1, "6 | [(4,5)9], [(6,3)18], [(10)38], [(4,6)57], [(7,2)66], [(8,1)75]")]
    [TestCase(10, "7 | [(4,5)9], [(6,3)18], [(10)38], [(4,6)57], [(7,2)66], [(8,1)75], [(10)85]")]
    [TestCase(9, "8 | [(4,5)9], [(6,3)18], [(10)38], [(4,6)57], [(7,2)66], [(8,1)75], [(10)94], [(9)103]")]
    [TestCase(0, "8 | [(4,5)9], [(6,3)18], [(10)38], [(4,6)57], [(7,2)66], [(8,1)75], [(10)94], [(9,0)103]")]
    [TestCase(5, "9 | [(4,5)9], [(6,3)18], [(10)38], [(4,6)57], [(7,2)66], [(8,1)75], [(10)94], [(9,0)103], [(5)108]")]
    [TestCase(4, "9 | [(4,5)9], [(6,3)18], [(10)38], [(4,6)57], [(7,2)66], [(8,1)75], [(10)94], [(9,0)103], [(5,4)112]")]
    [TestCase(10, "10 | [(4,5)9], [(6,3)18], [(10)38], [(4,6)57], [(7,2)66], [(8,1)75], [(10)94], [(9,0)103], [(5,4)112], [(10)122]")]
    [TestCase(10, "10 | [(4,5)9], [(6,3)18], [(10)38], [(4,6)57], [(7,2)66], [(8,1)75], [(10)94], [(9,0)103], [(5,4)112], [(10,10)132]")]
    public void PlayFullGame_ShouldReturnCorrectScoresForAllFrames(int pins, string expectedResult)
    {
        // Arrange
        // Act
        var result = _game.AddThrow(pins);

        // Assert
        result.Should().Be(expectedResult);
    }

    [Test]
    public void AddThrow_ShouldThrowException_WhenFrameScoreExceedsTen()
    {
        _game = new BowlingGame();

        _game.AddThrow(6);

        var ex = Assert.Throws<InvalidOperationException>(() => _game.AddThrow(6));
        Assert.That(ex.Message, Is.EqualTo("Die Summe der Würfe in einem Frame darf nicht größer als 10 sein."));
    }
}
