using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace SimplexInvoice.Infra.Data
{
    public static class DefaultInitialData
    {
        public static void AddDefaultInitialData(MigrationBuilder migrationBuilder)
        {              
            AddDefaultUserData(migrationBuilder);
            AddDefaultIdDocumentTypeData(migrationBuilder);
            AddDefaultTaxRateData(migrationBuilder);
            AddDefaultProductData(migrationBuilder);
            AddDefaultCompanyData(migrationBuilder);
            AddDefaultCustomerData(migrationBuilder);
            AddDefaultInvoiceData(migrationBuilder);
        }

        public static void AddDefaultUserData(MigrationBuilder migrationBuilder)
        {
            string creationDate = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");

            migrationBuilder.Sql(@$"INSERT INTO simplexinvoice.User 
                                  (Id, 
                                   EmailAddress, 
                                   Password, 
                                   FirstName, 
                                   LastName,
                                   CreationDate)
                                  VALUES('88a9df1d-ca1a-43d8-857b-782e057b6985', 
                                         'admin@gmail.com', 
                                         'ddz4RPMmrctg9HEUd7xx2w==,kincfsSHNqMOF9SroDS+koqPrd4=', 
                                         'Admin', 
                                         'Admin',
                                         '{creationDate}');");
        }

        public static void AddDefaultIdDocumentTypeData(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"INSERT INTO simplexinvoice.IdDocumentType (Id, Name)
                                   VALUES('25cdf776-e147-4898-92b7-a3280cbe9a34', 'NIE'),
                                         ('2dda9061-4e0a-4c8a-bc10-cd998bb9946f', 'CIF'),
                                         ('8b751cfd-fefc-4a6c-8733-9e4fdb68decd', 'NIF'),
                                         ('ae2e1245-885b-477f-a79d-cb273e7c2bc1', 'DNI')");
        }

        public static void AddDefaultTaxRateData(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"INSERT INTO simplexinvoice.TaxRate (Id, Name, Value)
                                   VALUES('3248b83b-990e-4627-ac82-c4a146e81754', '0%', '0'),
                                         ('ab6aecb2-d180-414d-8658-3ac383e73cdd', '4%', '4'),
                                         ('93ba2608-89cb-493f-848e-65169261d813', '10%', '10'),
                                         ('5dbd86d1-f989-47fd-ac4d-ab2bf4ea3564', '21%', '21')");
        }

        public static void AddDefaultProductData(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"INSERT INTO simplexinvoice.Product 
                                   (Id, 
                                    Code, 
                                    Name, 
                                    Description, 
                                    PackageQuantity, 
                                    Currency, 
                                    UnitPrice, 
                                    TaxRateId) 
                                   VALUES ('81f197d3-9260-4d28-933c-8540ff1a4c2d', 
                                           '1', 'DEFAULT PRODUCT', 
                                           'DEFAULT PRODUCT', 
                                           '1', 
                                           'EUR', 
                                           '1', 
                                           '3248b83b-990e-4627-ac82-c4a146e81754')");
        }

        public static void AddDefaultCompanyData(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"INSERT INTO simplexinvoice.Company 
                                   (Id, 
                                    Name, 
                                    IdDocumentTypeId, 
                                    IdDocumentNumber, 
                                    Street, 
                                    City, 
                                    State, 
                                    Country, 
                                    PostalCode, 
                                    Phone, 
                                    EmailAddress) 
                                   VALUES('efea1745-469d-4382-b68b-4d1cc3d49cff', 
                                          'Default Company', 
                                          '2dda9061-4e0a-4c8a-bc10-cd998bb9946f', 
                                          'B37232972', 
                                          'Default Company Street', 
                                          'Default Company City', 
                                          'Default Company State', 
                                          'Default Company Country', 
                                          '90210', 
                                          '+34 689 45 96 34', 
                                          'defaultcompany@company.com');");
        }

        public static void AddDefaultCustomerData(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"INSERT INTO simplexinvoice.Customer
                                   (Id, 
                                    FirstName, 
                                    LastName,
                                    IdDocumentTypeId, 
                                    IdDocumentNumber, 
                                    Street, 
                                    City, 
                                    State, 
                                    Country, 
                                    PostalCode, 
                                    Phone, 
                                    EmailAddress) 
                                   VALUES('1ce71b42-47eb-4d0c-819c-cc2efc31fedc', 
                                          'Default Customer FirstName', 
                                          'Default Customer LastName', 
                                          '2dda9061-4e0a-4c8a-bc10-cd998bb9946f', 
                                          '03282511C2', 
                                          'Default Customer Street', 
                                          'Default Customer City', 
                                          'Default Customer State', 
                                          'Default Customer Country', 
                                          '90211', 
                                          '+34 657 22 17 56', 
                                          'defaultcustomer@customer.com');");
        }

        public static void AddDefaultInvoiceData(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"INSERT INTO simplexinvoice.Invoice 
                                   (Id, 
                                    Number, 
                                    Description, 
                                    Date, 
                                    CompanyName, 
                                    CompanyIdDocumentType, 
                                    CompanyDocumentNumber, 
                                    CompanyStreet, 
                                    CompanyCity, 
                                    CompanyState, 
                                    CompanyCountry, 
                                    CompanyPostalCode, 
                                    CompanyPhone, 
                                    CompanyEmailAddress, 
                                    CustomerFullName, 
                                    CustomerIdDocumentType, 
                                    CustomerDocumentNumber, 
                                    CustomerStreet, 
                                    CustomerCity, 
                                    CustomerState, 
                                    CustomerCountry, 
                                    CustomerPostalCode, 
                                    CustomerPhone, 
                                    CustomerEmailAddress) 
                                   VALUES('b3d9bb06-9514-45ed-a028-022867727d0a',
                                          '1',
                                          'Test Invoice',
                                          '2023-10-19',
                                          'Default Company',
                                          'CIF',
                                          'B37232972',
                                          'Default Company Street', 
                                          'Default Company City', 
                                          'Default Company State', 
                                          'Default Company Country', 
                                          '90210',
                                          '+34 689 45 96 34',
                                          'defaultcompany@company.com',
                                          'Default Customer',
                                          'NIF',
                                          '03282511C2',
                                          'Default Customer Street', 
                                          'Default Customer City', 
                                          'Default Customer State', 
                                          'Default Customer Country', 
                                          '90211',
                                          '+34 657 22 17 56',
                                          'defaultcustomer@customer.com');");

            migrationBuilder.Sql(@"INSERT INTO simplexinvoice.InvoiceLine
                                   (Id, 
                                    InvoiceId, 
                                    LineNumber, 
                                    ProductCode, 
                                    ProductName, 
                                    ProductDescription, 
                                    Packages, 
                                    Quantity, 
                                    Currency, 
                                    Price, 
                                    TaxName, 
                                    TaxRate, 
                                    DiscountRate) 
                                   VALUES ('65fb5618-33c1-4849-bdc6-c2beab5e6ef1',
                                           'b3d9bb06-9514-45ed-a028-022867727d0a', 
                                           '1', 
                                           '1', 
                                           'DEFAULT PRODUCT', 
                                           'DEFAULT PRODUCT', 
                                           2, 
                                           2, 
                                           'EUR', 
                                           1, 
                                           '10%', 
                                           10, 
                                           0);");

            migrationBuilder.Sql(@"INSERT INTO simplexinvoice.InvoiceLine
                                   (Id, 
                                    InvoiceId, 
                                    LineNumber, 
                                    ProductCode, 
                                    ProductName, 
                                    ProductDescription, 
                                    Packages, 
                                    Quantity, 
                                    Currency, 
                                    Price, 
                                    TaxName, 
                                    TaxRate, 
                                    DiscountRate) 
                                   VALUES ('ae67fb39-a323-4dea-a862-c03f895c9249',
                                           'b3d9bb06-9514-45ed-a028-022867727d0a', 
                                           '1', 
                                           '1', 
                                           'DEFAULT PRODUCT', 
                                           'DEFAULT PRODUCT', 
                                           5, 
                                           5, 
                                           'EUR', 
                                           1.2, 
                                           '10%', 
                                           10, 
                                           5);");
        }

    }
}