# Take-Home Technical Task

## Task Overview

You are tasked with creating a basic credit risk management application. This application should evaluate customer credit scores and generate alerts for high-risk customers. Please upload to a public github repo and send me the link.s

## Requirements

### 1. Input Data

- A list of customers, each with the following properties:
  - **CustomerId** (integer)
  - **Name** (string)
  - **PaymentHistory** (percentage of payments made on time, 0 to 100)
  - **CreditUtilization** (percentage of credit limit used, 0 to 100)
  - **AgeOfCreditHistory** (in years)

### 2. Features

#### a. Calculate Credit Score

Implement a method to calculate a credit score for each customer using the following formula:

**CreditScore = (0.4 _ PaymentHistory) + (0.3 _ (100 - CreditUtilization)) + (0.3 \* Min(AgeOfCreditHistory, 10))**

- The score should be an integer.

#### b. High-Risk Customers

Identify customers with a credit score below 50. These are considered "high risk."

#### c. Output Reports

- Generate a summary report listing each customer's name, credit score, and risk status (High Risk or Low Risk).
- Log the report to the console.
- Write unit tests for the `CalculateCreditScore` method.
- Save the report to a JSON file.

## Deliverables

- A .NET Console Application (or .NET Core) that fulfills the above requirements.
- Source code with clear comments.
- (Optional) A test project with at least one unit test.

## Evaluation Criteria

1. **Code Quality**:
   - Readability, structure, and use of best practices.
2. **Correctness**:
   - Accurate implementation of the credit score formula and risk assessment.
3. **Edge Cases**:
   - Handling of invalid or out-of-range input values.
4. **Bonus Features**:
   - Implementation of unit tests and report saving.

---

## Example Input & Output

### Input:

```json
[
  {
    "CustomerId": 1,
    "Name": "Alice",
    "PaymentHistory": 90,
    "CreditUtilization": 40,
    "AgeOfCreditHistory": 5
  },
  {
    "CustomerId": 2,
    "Name": "Bob",
    "PaymentHistory": 70,
    "CreditUtilization": 90,
    "AgeOfCreditHistory": 15
  },
  {
    "CustomerId": 3,
    "Name": "Charlie",
    "PaymentHistory": 60,
    "CreditUtilization": 30,
    "AgeOfCreditHistory": 2
  }
]
```
