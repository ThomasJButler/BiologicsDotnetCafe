# BiologicsDotnetCafe

A .NET coding assessment for Waters Corporation. 

## Overview

When the app is run in the terminal, the user will experience a simple, clear, and intuitive ordering system, just like walking into a cozy digital cafe.

## Images 

Here are images of the project solution running. 

![image](https://github.com/user-attachments/assets/12e8af59-1391-4a8f-bae5-8bc3bf0b493c)

![image](https://github.com/user-attachments/assets/7a67019f-3379-4f6f-8e73-c0809500d0f5)

## Project Goals

- Friendly CLI experience
- Clear prompts and responses
- Automatic application of the most favourable discount
- Proper rounding and formatting (£)
- No visual UI required—just solid terminal interaction

## Installation and Usage

### Prerequisites

- .NET 8 SDK or later
- Compatible operating system (Windows, macOS, or Linux)

### Installation

1. Clone the repository:
   ```
   git clone https://github.com/ThomasJButler/BiologicsDotnetCafe.git
   cd BiologicsDotnetCafe
   ```

2. Build the solution:
   ```
   dotnet build
   ```

### Running the Application

1. Navigate to the project directory:
   ```
   cd BiologicsCafe
   ```

2. Run the application:
   ```
   dotnet run
   ```

3. Follow the on-screen prompts to:
   - Select menu items by number
   - Enter quantities for each item
   - Type 'done' when you've finished your order
   - View your receipt with applicable discounts

### Running Tests (please ensure you are inside the BiologicsCafe.Tests directory before running the command)

1. Execute all tests:
   ```
   dotnet test
   ```

2. Run tests for a specific component:
   ```
   dotnet test --filter "FullyQualifiedName~BiologicsCafe.Tests.DiscountEngineTests"
   ```

## Full Implementation Plan Breakdown

### Core Components Outlined - DONE in initial commit
- MenuItem model created
- Menu.cs static data source for menu items
- Project ready for logic implementation & testing

### Logic Implementation Plan

#### OrderService.cs
Handles all order-related logic:
- Store selected items + quantities (Dictionary or list)
- Calculate subtotal (sum of item price × quantity)
- Expose order total and breakdown
- Interface with DiscountEngine to apply best discount

#### DiscountEngine.cs
Encapsulates all offer rules:
- Detect if both food and drink are ordered (excluding water)
- Check if subtotal ≥ £20
- Decide which discount to apply:
  - 10% for food+drink (not water)
  - 20% if total ≥ £20
  - Do NOT stack discounts
  - Apply discount cap at £6.00
- Return the applied discount and final total

### Main App Flow (Program.cs)
Handles terminal interactions and drives the app:
1. Display menu
2. Prompt user to choose items (loop until "done")
3. Ask for quantity after each item
4. Pass items/quantities to OrderService
5. Calculate subtotal and discounts
6. Print full receipt with:
   - Items + quantity + prices
   - Subtotal
   - Discount description and amount
   - Final total
7. Exit cleanly with thank you message

### Unit Tests

#### MenuItem + Menu Data
- Validate correct item names, prices, types
- Check total item count (6)

#### OrderService
- Subtotal accuracy for multiple quantities
- Handling of unknown items (if implemented)
- Edge case: Empty order = £0

#### DiscountEngine
- 10% off for food + non-water drink
- 0% off for food + water only
- 20% off if total ≥ £20
- Only better discount applied (never both)
- Cap discount to £6 max (e.g., 20% off £40 = £8 → only £6 applied)
