# Test Automation Project

## Overview

### This repository contains an automated test suite for both UI and API testing:

- UI Automation: Built with C# Playwright and NUnit for web UI testing.

- API Automation: Developed using C# RestSharp and xUnit for testing RESTful APIs.

- GitHub Actions Pipeline: Integrated for continuous integration and test execution.

## Project Structure

```
TestAutomationProject/
│── UI_Playwright_NUnit/          # UI automation using Playwright & NUnit
│   ├── Pages/                    # Page Object Model (POM) implementation
│   ├── Tests/                    # NUnit test cases
│   ├── Utils/                    # Helper utilities
│   ├── playwright.config.ts       # Playwright configuration
│   └── README.md                  # UI test documentation
│
│── API_RestSharp_xUnit/          # API automation using RestSharp & xUnit
│   ├── Endpoints/                 # API endpoints and request models
│   ├── Tests/                     # xUnit test cases
│   ├── Utils/                     # Common utilities
│   ├── appsettings.json           # API test configurations
│   └── README.md                  # API test documentation
│
│── .github/workflows/             # GitHub Actions pipeline for CI/CD
│   ├── ui-tests.yml                # UI test execution pipeline
│   ├── api-tests.yml               # API test execution pipeline
│
│── TestAutomationProject.sln      # Visual Studio solution file
│── .gitignore                      # Ignore files configuration
│── README.md                       # Main project documentation
```

## 🛠️ Setup & Installation

### Prerequisites

Ensure you have the following installed:

- .NET SDK 8 or higher

- Visual Studio (with NUnit and xUnit test support)

- Node.js (for Playwright installation)

- Playwright CLI (for UI testing)

### Install Dependencies

Clone the repository:

- git clone https://github.com/your-repo/TestAutomationProject.git
cd TestAutomationProject

### Install Playwright dependencies:

- cd UI_Playwright_NUnit
- pwsh bin\Debug\netX.X\playwright.ps1 install

### Restore NuGet packages:

- dotnet restore

## 🚀 Running Tests

Run UI Tests (Playwright + NUnit)

- cd UUI_Playwright_Project
 dotnet test

Run API Tests (RestSharp + xUnit)

- cd API_RestSharp_Project
 dotnet test

## 🏗️ GitHub Actions - CI/CD Pipeline

This project includes a GitHub Actions pipeline for automated test execution on push and pull requests.

UI and API Tests Workflow (.github/workflows/test-pipeline.yml)

## 📊 Test Reports

### UI Test Reports

Playwright Traces: playwright show-trace trace.zip

Allure Reports (if configured): allure serve allure-results

### API Test Reports

xUnit HTML Reports (if configured): dotnet test --logger:"html;LogFileName=TestResults.html"

## 🔧 Contributing

- Fork the repo and create a new branch.

- Make your changes and commit them.

- Push your branch and submit a pull request.

## 📜 License

This project is licensed under the MIT License.