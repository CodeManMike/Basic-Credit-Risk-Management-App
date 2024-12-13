using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic_Credit_Risk_Management_App.Models;
public class Customer
{
    public Guid CustomerId { get; set; } = Guid.NewGuid();
    public string? Name { get; set; }
    public int PaymentHistory { get; set; } // 0-100
    public int CreditUtilization { get; set; } // 0-100
    public int AgeOfCreditHistory { get; set; } // in years
    public int CreditScore { get; set; } // Calculated later
    public string? RiskStatus { get; set; } // High Risk or Low Risk
}
