# Calculator_repo

## Description
A RESTful calculator service that performs basic math operations and tracks operation history using tracking IDs.

## Features
- **Math Operations**:
  - Add multiple numbers
  - Subtract
  - Multiply multiple numbers
  - Divide with quotient and remainder
  - Square root

- **Operation Tracking**:
  - Log operations with tracking ID
  - Query operation history by ID

## Project Structure
CalculatorService
  CalculatorServer.Library # Shared library with models and DTOs
  CalculatorService.Server # API server
  CalculatorService.ServerTests # Unit tests
  CalculatorService.Client # Console client

## Requirements
- .NET 6.0 or higher
- Visual Studio 2022 or VS Code (optional)

## API Endpoints
POST /calculator/add - Addition

POST /calculator/sub - Subtraction

POST /calculator/mul - Multiplication

POST /calculator/div - Division

POST /calculator/sqrt - Square root

POST /calculator/journal - Query journal
