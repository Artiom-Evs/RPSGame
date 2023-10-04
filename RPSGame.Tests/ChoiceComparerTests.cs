using NUnit.Framework;
using RPSGame.Infrastructure;
using RPSGame.Models;

namespace RPSGame.Tests;

[TestFixture]
public class ChoiceComparerTests
{
    [TestCase(0, 3)]
    [TestCase(1, 3)]
    [TestCase(2, 3)]
    [TestCase(0, 5)]
    [TestCase(2, 5)]
    [TestCase(4, 5)]
    public void CheckComparisonOfEqualChoicesWithDifferentChoicesCount(int choice, int choicesCount)
    {
        var result = ChoiceComparer.Compare(choice, choice, choicesCount);

        Assert.That(result, Is.EqualTo(RoundResults.Draw));
    }

    [TestCase(0, 0, RoundResults.Draw)]
    [TestCase(0, 1, RoundResults.PCWin)]
    [TestCase(0, 2, RoundResults.PlayerWin)]
    [TestCase(1, 0, RoundResults.PlayerWin)]
    [TestCase(1, 1, RoundResults.Draw)]
    [TestCase(1, 2, RoundResults.PCWin)]
    [TestCase(2, 0, RoundResults.PCWin)]
    [TestCase(2, 1, RoundResults.PlayerWin)]
    [TestCase(2, 2, RoundResults.Draw)]
    public void CheckComparisonWithThreeChoices(int choice1, int choice2, RoundResults expected)
    {
        var result = ChoiceComparer.Compare(choice1, choice2, 3);

        Assert.That(result, Is.EqualTo(expected));
    }

    [TestCase(0, 3, RoundResults.PCWin)]
    [TestCase(0, 4, RoundResults.PlayerWin)]
    [TestCase(6, 2, RoundResults.PCWin)]
    [TestCase(6, 3, RoundResults.PlayerWin)]
    public void CheckComparisonOnRangeBoundariesWithSevenChoices(int choice1, int choice2, RoundResults expected)
    {
        var result = ChoiceComparer.Compare(choice1, choice2, 7);

        Assert.That(result, Is.EqualTo(expected));
    }
}