
# Internship Learning Repository

In this repository, I share what I'm learning during my internship program. This repository is based on ASP.NET Core Web API. I have created some APIs to read from and write to the database.

## Project Description

This project is designed to demonstrate my learning and skills in ASP.NET Core Web API development. It includes various APIs for managing data related to partners, items, purchases, and sales. Below is a brief overview of the functionalities implemented:

### Features

1. **Partner Management**
   - Create, update, and retrieve partner types (e.g., Customer, Supplier).
   - Create, update, and retrieve partners linked to their respective partner types.

2. **Item Management**
   - Create, update, and retrieve items with stock quantity and active status.

3. **Purchase Management**
   - Record purchases from suppliers, including purchase details such as item quantities and unit prices.
   - Update item stock quantities based on purchases.

4. **Sales Management**
   - Record sales to customers, including sales details such as item quantities and unit prices.
   - Ensure stock quantities are checked before recording sales.

5. **Reports**
   - Generate daily and monthly reports for purchase and sales data.
   - Calculate total purchase and sales amounts, and determine profit or loss status.

### Database Schema

- **tblItem**
  - `intItemId` (Primary Key)
  - `strItemName`
  - `numStockQuantity`
  - `isActive`

- **tblPartnerType**
  - `intPartnerTypeId` (Primary Key)
  - `strPartnerTypeName`
  - `isActive`

- **tblPartner**
  - `intPartnerId` (Primary Key)
  - `strPartnerName`
  - `intPartnerTypeId` (Foreign Key)
  - `isActive`

- **tblPurchase**
  - `intPurchaseId` (Primary Key)
  - `intSupplierId`
  - `dtePurchaseDate`
  - `isActive`

- **tblPurchaseDetails**
  - `intDetailsId` (Primary Key)
  - `intPurchaseId` (Foreign Key)
  - `intItemId` (Foreign Key)
  - `numItemQuantity`
  - `numUnitPrice`
  - `isActive`

- **tblSales**
  - `intSalesId` (Primary Key)
  - `intCustomerId`
  - `dteSalesDate`
  - `isActive`

- **tblSalesDetails**
  - `intDetailsId` (Primary Key)
  - `intSalesId` (Foreign Key)
  - `intItemId` (Foreign Key)
  - `numItemQuantity`
  - `numUnitPrice`
  - `isActive`

### How to Run

1. Clone the repository.
2. Open the solution in Visual Studio.
3. Configure the database connection string in `appsettings.json`.
4. Run the database migrations to set up the schema.
5. Start the project and use tools like Postman to test the API endpoints.

### Learning Outcomes

- Understanding of ASP.NET Core Web API development.
- Experience with Entity Framework Core for database operations.
- Practical knowledge of creating and managing APIs for CRUD operations.
- Skills in generating and handling reports based on database queries.

### Future Enhancements

- Implement authentication and authorization.
- Add more comprehensive error handling and logging.
- Enhance the reporting features with more detailed analytics.

---

Feel free to explore the project, test the APIs, and provide feedback!

