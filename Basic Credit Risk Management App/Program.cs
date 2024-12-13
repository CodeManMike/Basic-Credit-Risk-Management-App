using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Basic_Credit_Risk_Management_App.Models;
using Basic_Credit_Risk_Management_App.Services;
using Basic_Credit_Risk_Management_App.Util;

namespace Basic_Credit_Risk_Management_App
{
    /// <summary>
    /// Main program class for the Basic Credit Risk Management App.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main entry point of the application.
        /// </summary>
        static void Main(string[] args)
        {
            var dataProcessor = new DataProcessor();
            var customers = dataProcessor.LoadCustomers("Data/MockData.json");
            EnsureCreditData(customers);

            while(true)
            {
                Console.WriteLine("Select an option:");
                Console.WriteLine("1. View and Save Report");
                Console.WriteLine("2. Add New Customer");
                Console.WriteLine("3. Search for a Customer");
                Console.WriteLine("4. Exit");
                var choice = Console.ReadLine();

                switch(choice)
                {
                    case "1":
                        dataProcessor.ProcessCustomerData(customers);
                        string jsonFilePath = dataProcessor.GenerateReport(customers);
                        string excelFilePath = dataProcessor.ConvertReportToExcel(customers);
                        Console.WriteLine($"JSON report saved at: {jsonFilePath}");
                        Console.WriteLine($"Excel report saved at: {excelFilePath}");
                        Console.WriteLine("Choose an option:");
                        Console.WriteLine("1. Open Excel report and close application");
                        Console.WriteLine("2. Back to main menu");
                        var postChoice = Console.ReadLine();
                        if (postChoice == "1")
                        {
                            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                            {
                                FileName = excelFilePath,
                                UseShellExecute = true
                            });
                            return;
                        }
                        break;
                    case "2":
                        AddNewCustomer(customers);
                        break;
                    case "3":
                        SearchCustomer(customers);
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        /// <summary>
        /// Ensures all customers have a credit score and risk status.
        /// </summary>
        static void EnsureCreditData(List<Customer> customers)
        {
            var calculator = new CreditScoreCalculator();
            foreach(var customer in customers)
            {
                if(customer.CreditScore == 0 && customer.RiskStatus == null)
                {
                    customer.CreditScore = calculator.CalculateCreditScore(customer.PaymentHistory, customer.CreditUtilization, customer.AgeOfCreditHistory);
                    customer.RiskStatus = customer.CreditScore < 50 ? "High Risk" : "Low Risk";
                }
            }
            File.WriteAllText("Data/MockData.json", JsonSerializer.Serialize(customers));
        }

        /// <summary>
        /// Adds a new customer to the list and updates the data file.
        /// </summary>
        static void AddNewCustomer(List<Customer> customers)
        {
            Console.WriteLine("Enter customer name and surname:");
            var name = Console.ReadLine();

            int paymentHistory = GetValidatedInput("Enter payment history (0%-100%):", 0, 100);
            int creditUtilization = GetValidatedInput("Enter credit utilization (0%-100%):", 0, 100);
            int ageOfCreditHistory = GetValidatedInput("Enter age of credit history in years:", 0, 100);

            var newCustomer = new Customer
            {
                Name = name,
                PaymentHistory = paymentHistory,
                CreditUtilization = creditUtilization,
                AgeOfCreditHistory = ageOfCreditHistory
            };

            var calculator = new CreditScoreCalculator();
            newCustomer.CreditScore = calculator.CalculateCreditScore(newCustomer.PaymentHistory, newCustomer.CreditUtilization, newCustomer.AgeOfCreditHistory);
            newCustomer.RiskStatus = newCustomer.CreditScore < 50 ? "High Risk" : "Low Risk";

            customers.Add(newCustomer);
            File.WriteAllText("Data/MockData.json", JsonSerializer.Serialize(customers));
            Console.WriteLine("Customer added successfully.");
        }

        /// <summary>
        /// Searches for customers by name and displays matching results.
        /// </summary>
        static void SearchCustomer(List<Customer> customers)
        {
            Console.WriteLine("Enter part of the customer's name to search:");
            var searchTerm = Console.ReadLine() ?? string.Empty;
            var matchingCustomers = customers.Where(c => c.Name != null && c.Name.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0).ToList();

            if(matchingCustomers.Any())
            {
                foreach(var customer in matchingCustomers)
                {
                    Console.WriteLine($"Name: {customer.Name}, Credit Score: {customer.CreditScore}, Risk Status: {customer.RiskStatus}");
                }
            }
            else
            {
                Console.WriteLine("No matching customers found.");
            }
        }

        /// <summary>
        /// Validates user input to ensure it is within a specified range.
        /// </summary>
        static int GetValidatedInput(string prompt, int min, int max)
        {
            int value;
            while(true)
            {
                Console.WriteLine(prompt);
                if(int.TryParse(Console.ReadLine(), out value) && value >= min && value <= max)
                {
                    break;
                }
                Console.WriteLine($"Please enter a valid number between {min} and {max}.");
            }
            return value;
        }
    }
}
