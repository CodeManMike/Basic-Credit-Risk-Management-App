using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Basic_Credit_Risk_Management_App.Models;
using Basic_Credit_Risk_Management_App.Services;
using OfficeOpenXml;



namespace Basic_Credit_Risk_Management_App.Util;
/// <summary>
/// Handles data processing tasks such as loading customers, processing customer data, and generating reports.
/// </summary>
public class DataProcessor
{
    /// <summary>
    /// Loads customer data from a JSON file.
    /// </summary>
    /// <param name="filePath">The path to the JSON file containing customer data.</param>
    /// <returns>A list of customers.</returns>
    public List<Customer> LoadCustomers(string filePath)
    {
        var jsonData = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<List<Customer>>(jsonData) ?? new List<Customer>();
    }

    /// <summary>
    /// Processes customer data to calculate credit scores and determine risk status.
    /// </summary>
    /// <param name="customers">The list of customers to process.</param>
    public void ProcessCustomerData(List<Customer> customers)
    {
        var calculator = new CreditScoreCalculator();
        foreach(var customer in customers)
        {
            customer.CreditScore = calculator.CalculateCreditScore(customer.PaymentHistory, customer.CreditUtilization, customer.AgeOfCreditHistory);
            customer.RiskStatus = customer.CreditScore < 50 ? "High Risk" : "Low Risk";
        }
    }

    /// <summary>
    /// Generates a report of customer data and saves it as a JSON file.
    /// </summary>
    /// <param name="customers">The list of customers to include in the report.</param>
    /// <returns>The file path of the generated JSON report.</returns>
    public string GenerateReport(List<Customer> customers)
    {
        foreach(var customer in customers)
        {
            Console.WriteLine($"Name: {customer.Name}, Credit Score: {customer.CreditScore}, Risk Status: {customer.RiskStatus}");
        }
        var reportJson = JsonSerializer.Serialize(customers);

        DateTime date = DateTime.UtcNow;
        string formattedDate = date.ToString("yyyyMMdd_HHmmss");
        string jsonFilePath = $"Data/CustomersCreditReport{formattedDate}.json";

        File.WriteAllText(jsonFilePath, reportJson);
        return jsonFilePath;
    }

    /// <summary>
    /// Converts the customer report to an Excel document.
    /// </summary>
    /// <param name="customers">The list of customers to include in the Excel report.</param>
    /// <returns>The file path of the generated Excel report.</returns>
    public string ConvertReportToExcel(List<Customer> customers)
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        using(var package = new ExcelPackage())
        {
            var worksheet = package.Workbook.Worksheets.Add("Customer Report");
            worksheet.Cells[1, 1].Value = "CustomerId";
            worksheet.Cells[1, 2].Value = "Name";
            worksheet.Cells[1, 3].Value = "Payment History";
            worksheet.Cells[1, 4].Value = "Credit Utilization";
            worksheet.Cells[1, 5].Value = "Age of Credit History";
            worksheet.Cells[1, 6].Value = "Credit Score";
            worksheet.Cells[1, 7].Value = "Risk Status";

            for(int i = 0; i < customers.Count; i++)
            {
                worksheet.Cells[i + 2, 1].Value = customers[i].CustomerId;
                worksheet.Cells[i + 2, 2].Value = customers[i].Name;
                worksheet.Cells[i + 2, 3].Value = customers[i].PaymentHistory;
                worksheet.Cells[i + 2, 4].Value = customers[i].CreditUtilization;
                worksheet.Cells[i + 2, 5].Value = customers[i].AgeOfCreditHistory;
                worksheet.Cells[i + 2, 6].Value = customers[i].CreditScore;
                worksheet.Cells[i + 2, 7].Value = customers[i].RiskStatus;
            }
            DateTime date = DateTime.UtcNow;
            string formattedDate = date.ToString("yyyyMMdd_HHmmss");
            var excelFile = new FileInfo($"Data/CustomersCreditReport{formattedDate}.xlsx");
            package.SaveAs(excelFile);
            Console.WriteLine($"Excel report saved at: {excelFile.FullName}");
            return excelFile.FullName;
        }
    }
}
