using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic_Credit_Risk_Management_App.Services;
/// <summary>
/// Provides functionality to calculate credit scores for customers.
/// </summary>
public class CreditScoreCalculator
{
    /// <summary>
    /// Calculates the credit score based on payment history, credit utilization, and age of credit history.
    /// </summary>
    /// <param name="paymentHistory">The percentage of payments made on time (0-100).</param>
    /// <param name="creditUtilization">The percentage of credit limit used (0-100).</param>
    /// <param name="ageOfCreditHistory">The age of credit history in years.</param>
    /// <returns>The calculated credit score as an integer.</returns>
    public int CalculateCreditScore(int paymentHistory, int creditUtilization, int ageOfCreditHistory)
    {
        return (int)((0.4 * paymentHistory) + (0.3 * (100 - creditUtilization)) + (0.3 * Math.Min(ageOfCreditHistory, 10)));
    }
}
