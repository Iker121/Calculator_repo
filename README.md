# Calculator_repo

## Description
A RESTful calculator service with operation history tracking, featuring:
- Basic math operations (add, subtract, multiply, divide, square root)
- Journal system to track operations by ID
- Console client interface
- Swagger API documentation

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

## Installation & Setup
1. Clone the repository:
   ```bash
   git clone [repository-url]
   cd CalculatorService

## How to Run
1. Start the Server (API)
- cd CalculatorService.Server
- dotnet run
2. Start the Console Client (Separate Terminal)
- cd CalculatorService.Client
- dotnet run
## Usage
Console Client Interface

CALCULATOR
====================
Enable operation tracking? (y/n):

Choose whether to enable tracking (optional)
Select operations from the menu:
1: Add (2+ numbers)
2: Subtract
3: Multiply (2+ numbers)
4: Divide (with remainder)
5: Square root
6: Query journal by tracking ID
7: Exit
## Example Flow
1. Operations
Select an option: 1
Enter two numbers separated by space: 5 3
Result: 8
2. Query History
Select an option: 6
Enter tracking ID: test123
OPERATION HISTORY
=======================
[12:30:45] Add: 5+3 = 8
## API Endpoints
POST /calculator/add - Addition

POST /calculator/sub - Subtraction

POST /calculator/mul - Multiplication

POST /calculator/div - Division

POST /calculator/sqrt - Square root

POST /calculator/journal - Query journal


