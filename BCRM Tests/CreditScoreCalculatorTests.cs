namespace BCRM_Tests;
using Basic_Credit_Risk_Management_App.Services;
using NUnit.Framework;

/// <summary>
/// Unit tests for the CreditScoreCalculator class.
/// </summary>
public class CreditScoreCalculatorTests
{
    /// <summary>
    /// Tests that the CalculateCreditScore method returns the correct score based on the input parameters.
    /// </summary>
    [Test]
    public void CalculateCreditScore_ShouldReturnCorrectScore()
    {
        var calculator = new CreditScoreCalculator();
        int paymentHistory = 90;
        int creditUtilization = 40;
        int ageOfCreditHistory = 5;
        int expectedScore = (int)((0.4 * paymentHistory) + (0.3 * (100 - creditUtilization)) + (0.3 * Math.Min(ageOfCreditHistory, 10)));
        int actualScore = calculator.CalculateCreditScore(paymentHistory, creditUtilization, ageOfCreditHistory);
        Assert.That(actualScore, Is.EqualTo(expectedScore));
    }
}
