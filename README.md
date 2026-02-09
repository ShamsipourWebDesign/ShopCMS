
# Project README

## Overview
Mahdi Imenipour & Ali Vahedi Yousef Zadeh
This project is divided into two main roles: **Mahdi Imenipour (Core & Decision Logic)** and **Ali Vahedi yousef Zadeh (Infrastructure, Security & Integration)**. Each person has a set of tasks, and the dependencies between these tasks are also clearly defined. Below is a breakdown of the tasks for each person, along with their respective dependencies.

## Person A : Mahdi Imenipour(مهدی ایمنی پور ) — Core & Decision Logic (Business Logic)

### Role Overview
Responsible for pricing logic, rules, eligibility, and the explainable outputs of the system.

### Tasks and Dependencies

1. **Design Product/Rule/Pricing Domain Model**
    - **Short Description**: Design the logical entities for Product/Rule/Pricing.
    - **Dependency**: Linked to DbContext (Person B).

2. **Define Service Contracts (Interfaces)**
    - **Short Description**: Define method signatures for Pricing/Eligibility/Rule Engine.
    - **Dependency**: Dependent on Person B.

3. **Pricing & Discount Engine**
    - **Short Description**: Calculate the final price by applying discounts and campaigns.
    - **Dependency**: Requires CurrencyProvider (Person B).

4. **Promotion Rule Engine**
    - **Short Description**: Define and apply promotion and coupon rules.
    - **Independent Task**.

5. **Rule Versioning**
    - **Short Description**: Maintain versions of pricing rules.
    - **Independent Task**.

6. **Inventory Rule**
    - **Short Description**: Check inventory limits and purchase caps.
    - **Independent Task**.

7. **Sale Eligibility Logic/API**
    - **Short Description**: Check if the user is eligible for a sale.
    - **Dependency**: Requires Auth/UserContext (Person B).

8. **Multi-Currency Price Calculation**
    - **Short Description**: Convert prices between currencies.
    - **Dependency**: Requires CurrencyRateProvider (Person B).

9. **Price Snapshot Logic**
    - **Short Description**: Record price and rule status at the moment of decision.
    - **Dependency**: Requires Database (Person B).

10. **Guard Volatility**
    - **Short Description**: Prevent sales during high currency fluctuation.
    - **Dependency**: Requires CurrencyHistory (Person B).

11. **Unit Tests**
    - **Short Description**: Test Rule/Eligibility/Pricing logic.
    - **Independent Task**.

---

## Person B : Ali Vahedi Yousef Zadeh (علی واحدی یوسف زاده ) — Infrastructure, Security & Integration

### Role Overview
Responsible for database, infrastructure, security, authentication, providers, and API hosting.

### Tasks and Dependencies

1. **Clean Architecture Solution Setup**
    - **Short Description**: Set up the solution and 4-layered projects.
    - **Independent Task**.

2. **EF Core + DbContext + Migration**
    - **Short Description**: Create the database and the main context.
    - **Dependency**: Required for Person A's domain models.

3. **Repository / UnitOfWork**
    - **Short Description**: Data access layer using EF.
    - **Dependency**: Required for Snapshot and Rule logic in Person A.

4. **Swagger**
    - **Short Description**: API documentation for testing.
    - **Independent Task**.

5. **JWT Authentication**
    - **Short Description**: Set up login, token, and claims.
    - **Dependency**: Required for Sale Eligibility logic in Person A.

6. **Role & Permission System**
    - **Short Description**: Define Admin/User/Auditor roles.
    - **Dependency**: Required for Admin features in Person A.

7. **UserContext Provider**
    - **Short Description**: Retrieve UserId/Role from JWT.
    - **Dependency**: Needed for Eligibility logic in Person A.

8. **CurrencyRateProvider (Mock)**
    - **Short Description**: Provide mock currency rate for development.
    - **Dependency**: Input for Pricing in Person A.

9. **Rate Limiting**
    - **Short Description**: Limit the number of requests.
    - **Independent Task**.

10. **Logging + Serilog**
    - **Short Description**: Log errors and requests/responses.
    - **Independent Task**.

11. **Decision Audit Logging**
    - **Short Description**: Save pricing decision reasons.
    - **Dependency**: Follows the completion of Eligibility in Person A.

12. **External API Integration**
    - **Short Description**: Connect to external currency/payment services.
    - **Dependency**: Needed by Person A when required.

---

## Dependencies Between Person A and Person B

### Person A depends on Person B for:
- DbContext
- JWT/UserContext
- CurrencyProvider
- Logging Infrastructure
- Snapshot Persistence
- CurrencyHistory Store for Volatility

### Person B depends on Person A for:
- Service Interfaces for Domain
- Pricing Service Contract
- Rule Engine Contract
- Eligibility Service Contract
