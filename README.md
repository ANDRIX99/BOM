# BOM (Bill of Materials) - Advanced Bill of Materials Generator with .NET 9, EF Core, and SQL Views

This project provides a comprehensive and customizable solution for generating Bills of Materials (BOMs) from an SQL Server database, leveraging the power of C# with .NET 9, Entity Framework (EF) Core, and SQL Views for enhanced flexibility and performance.

## Detailed Description

The application is designed to meet the needs of businesses requiring precise and efficient BOM management. Using EF Core, the application enables object-oriented interaction with the SQL Server database, simplifying data extraction and manipulation. The integration with SQL Views allows for pre-processing data within the database, improving query performance and offering greater flexibility in generating custom reports in various formats.

## Advanced Features

* **.NET 9 Optimized Architecture:**
    * Utilizes the latest .NET 9 features for superior performance and resource management.
    * Implements asynchronous programming principles for application responsiveness.
* **Advanced Entity Framework Core Integration:**
    * Employs advanced EF Core techniques to optimize queries and reduce database load.
    * Supports complex relationships between database tables.
    * Manages transactions.
* **SQL Views for Optimization:**
    * Creates SQL Views to pre-process data in the database, enhancing query performance.
    * Uses Views to simplify complex queries and aggregate data.
    * Exposes Views as EF Core entities.
* **Highly Customizable BOM Generation:**
    * Defines custom templates for report generation, enabling advanced data formatting.
    * Supports report generation in various formats (CSV, TXT, JSON, XML).
    * Exports data to Excel.
* **Advanced Data Filtering and Aggregation:**
    * Implements filters based on complex and customizable LINQ expressions.
    * Supports data aggregation (sum, average, count) for generating summary reports.
    * Creates custom Views.
* **Advanced Configuration and Customization:**
    * Uses `appsettings.json` configuration files for advanced application settings.
    * Supports configuration via environment variables.
    * Creates multiple configurations for different environments.
* **Error Handling and Logging:**
    * Implements an advanced logging system to track application execution and identify issues.
    * Robust error handling to ensure application stability.

## Advanced Installation and Configuration

1.  **Clone the Repository:**

    ```bash
    git clone [https://github.com/ANDRIX99/BOM.git](https://github.com/ANDRIX99/BOM.git)
    ```

2.  **.NET 9 SDK Installation:**
    * Download and install the .NET 9 SDK from the official Microsoft website.

3.  **Database Configuration:**
    * Modify the `appsettings.json` file with the SQL Server database connection string and other relevant settings.
    * Ensure the Database has the correct tables, relationships and views.

4.  **Database Migration:**
    * Run EF Core migrations to create or update the database schema:

    ```bash
    dotnet ef database update
    ```

## Advanced Usage and Customization

1.  **Database Preparation:**
    * Verify that the SQL Server database contains the necessary data for BOM generation and that the sql views are present.

2.  **Application Execution:**
    * Run the .NET 9 application via the command line or IDE.
    * Possibility to pass parameters via command line.

3.  **Report Customization:**
    * Use configuration parameters or custom templates to define report format and content.
    * Take advantage of sql views to create more performant reports.

## Advanced Examples

* **LINQ Query with Aggregation and Views:**

    ```csharp
    var report = context.VistaComponentiRiassuntivi
        .Where(v => v.QuantitàTotale > 10)
        .OrderBy(v => v.Categoria)
        .ToList();
    ```

* **Custom Report Template:**
    * Possibility to create .txt, or csv files, as templates, for report creation.

## Contributions and Support

Contributions are welcome! If you find bugs or have suggestions for new features, please open an issue or submit a pull request.
