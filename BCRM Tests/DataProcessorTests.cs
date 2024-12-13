using Basic_Credit_Risk_Management_App.Models;
using Basic_Credit_Risk_Management_App.Util;
using NUnit.Framework;
using System.IO;
using System.Text.Json;

namespace BCRM_Tests;

/// <summary>
/// Unit tests for the DataProcessor class.
/// </summary>
public class DataProcessorTests
{
    private DataProcessor _dataProcessor;
    private List<Customer> _customers;

    /// <summary>
    /// Sets up the test environment by initializing the DataProcessor and sample customer data.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        _dataProcessor = new DataProcessor();
        _customers = new List<Customer>
        {
            new Customer { Name = "Alice", PaymentHistory = 90, CreditUtilization = 40, AgeOfCreditHistory = 5 },
            new Customer { Name = "Bob", PaymentHistory = 70, CreditUtilization = 90, AgeOfCreditHistory = 15 }
        };
    }

    /// <summary>
    /// Tests that the LoadCustomers method returns the correct number of customers.
    /// </summary>
    [Test]
    public void LoadCustomers_ShouldReturnCorrectNumberOfCustomers()
    {
        var json = JsonSerializer.Serialize(_customers);
        File.WriteAllText("MockData.json", json);

        var loadedCustomers = _dataProcessor.LoadCustomers("MockData.json");

        Assert.That(loadedCustomers.Count, Is.EqualTo(_customers.Count));
    }

    /// <summary>
    /// Tests that the ProcessCustomerData method calculates credit scores and risk status correctly.
    /// </summary>
    [Test]
    public void ProcessCustomerData_ShouldCalculateCreditScoresAndRiskStatus()
    {
        _dataProcessor.ProcessCustomerData(_customers);

        foreach (var customer in _customers)
        {
            Assert.That(customer.CreditScore, Is.GreaterThan(0));
            Assert.That(customer.RiskStatus, Is.Not.Null);
        }
    }

    /// <summary>
    /// Tests that the GenerateReport method creates a JSON file with the correct data.
    /// </summary>
    [Test]
    public void GenerateReport_ShouldCreateJsonFile()
    {
        var filePath = _dataProcessor.GenerateReport(_customers);

        Assert.That(File.Exists(filePath), Is.True);

        var jsonData = File.ReadAllText(filePath);
        var reportCustomers = JsonSerializer.Deserialize<List<Customer>>(jsonData);

        Assert.That(reportCustomers.Count, Is.EqualTo(_customers.Count));
    }
}
