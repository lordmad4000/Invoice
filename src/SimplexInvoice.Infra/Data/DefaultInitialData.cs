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
                                         ('ae2e1245-885b-477f-a79d-cb273e7c2bc1', 'DNI');");
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
                                   VALUES('93cb9570-7f16-4c26-aafc-3b96b2bba055', 'AG', 'Aguacate', 'Aguacate', '4', 'EUR','6.15','93ba2608-89cb-493f-848e-65169261d813'),
                                         ('3a4a4ade-c0eb-498d-869a-2073f003c602', 'ALB', 'Albaricoque', 'Albaricoque', '6.5', 'EUR','1.8','ab6aecb2-d180-414d-8658-3ac383e73cdd'),
                                         ('ace55ab1-2cb4-4b9a-8a1a-a06bf1561fa2', 'ALBM', 'Albaricoque Moniqui', 'Albaricoque Moniqui', '6.2', 'EUR','1.9','ab6aecb2-d180-414d-8658-3ac383e73cdd'),
                                         ('8c598dec-b949-40e6-95f6-df08d61adbe9', 'CE', 'Cereza', 'Cereza', '2', 'EUR','4.5','93ba2608-89cb-493f-848e-65169261d813'),
                                         ('5d272ec5-fbce-4d33-b5cc-8b6ddd29ec6f', 'CB', 'Ciruela Blanca', 'Ciruela Blanca', '7.1', 'EUR','2.1','ab6aecb2-d180-414d-8658-3ac383e73cdd'),
                                         ('3b22852e-dbca-4187-ba22-7a0e6721a6e2', 'CN', 'Ciruela Negra', 'Ciruela Negra', '6.8', 'EUR','2.05','ab6aecb2-d180-414d-8658-3ac383e73cdd'),
                                         ('93a4e10d-7450-4cf2-aa43-9465c8c6d8ef', 'FRE', 'Fresa', 'Fresa', '2', 'EUR','3.5','ab6aecb2-d180-414d-8658-3ac383e73cdd'),
                                         ('57f8be6a-beb3-4998-bf1b-33deb9906c59', 'FR', 'Freson', 'Freson', '2', 'EUR','3','ab6aecb2-d180-414d-8658-3ac383e73cdd'),
                                         ('ea19f811-3b32-465c-8239-59ce76ea0758', 'G', 'Granada', 'Granada', '5.4', 'EUR','1.35','93ba2608-89cb-493f-848e-65169261d813'),
                                         ('ccf80700-2df3-4634-b713-a1398d104f6e', 'HB', 'Higo Blanco', 'Higo Blanco', '2.4', 'EUR','3.55','ab6aecb2-d180-414d-8658-3ac383e73cdd'),
                                         ('0ce73894-d28f-4888-a9af-d3ccf05f95b3', 'HN', 'Higo Negro', 'Higo Negro', '2.55', 'EUR','3.85','ab6aecb2-d180-414d-8658-3ac383e73cdd'),
                                         ('2cdeb863-3055-4334-a170-8db1f16c0dc2', 'L', 'Limon', 'Limon', '10.5', 'EUR','1','ab6aecb2-d180-414d-8658-3ac383e73cdd'),
                                         ('adf15f2c-f793-4a18-ab10-3549e917e68e', 'LBO', 'Limon en Bolsa', 'Limon en Bolsa', '12', 'EUR','1.1','93ba2608-89cb-493f-848e-65169261d813'),
                                         ('84827b65-7cbb-4823-9017-9b5868b75b25', '3MCP', 'Mahou 5 Clasica Bote', 'Mahou 5 Clasica Bote', '24', 'EUR','0.35','5dbd86d1-f989-47fd-ac4d-ab2bf4ea3564'),
                                         ('ede1105e-8118-4cd8-8f86-aa4ec25fdc3d', '3MCB', 'Mahou 5 Clasica Pack', 'Mahou 5 Clasica Pack', '24', 'EUR','0.38','5dbd86d1-f989-47fd-ac4d-ab2bf4ea3564'),
                                         ('05530f15-c5b4-4839-bb79-5767b4134db3', '3MB', 'Mahou 5 Estrellas Bote', 'Mahou 5 Estrellas Bote', '24', 'EUR','0.4','5dbd86d1-f989-47fd-ac4d-ab2bf4ea3564'),
                                         ('2b4a0cd5-4be1-4a8c-9770-47c86a86fea5', '3MP', 'Mahou 5 Estrellas Pack', 'Mahou 5 Estrellas Pack', '24', 'EUR','0.44','5dbd86d1-f989-47fd-ac4d-ab2bf4ea3564'),
                                         ('cace1f1c-f402-41a9-9501-67931ee7094e', 'MF', 'Manzana Fuji', 'Manzana Fuji', '4.2', 'EUR','2.75','ab6aecb2-d180-414d-8658-3ac383e73cdd'),
                                         ('a00e0dba-486c-4124-bc18-3b2a5b98dde9', 'MG', 'Manzana Golden', 'Manzana Golden', '8.5', 'EUR','0.9','ab6aecb2-d180-414d-8658-3ac383e73cdd'),
                                         ('eac71ed2-a73d-4b85-9a53-eae506a06a48', 'MGF', 'Manzana Golden Frances', 'Manzana Golden Frances', '4.2', 'EUR','1','ab6aecb2-d180-414d-8658-3ac383e73cdd'),
                                         ('89f0d2eb-9a2b-4c9a-a175-0d8e4771c0fe', 'MGR', 'Manzana Granny Smith', 'Manzana Granny Smith', '4.2', 'EUR','1.2','ab6aecb2-d180-414d-8658-3ac383e73cdd'),
                                         ('c3b6adf3-7b60-4f0d-98dc-a28ce2e30c31', 'MR', 'Manzana Reineta', 'Manzana Reineta', '4.5', 'EUR','1.35','ab6aecb2-d180-414d-8658-3ac383e73cdd'),
                                         ('6a97413c-c033-4498-84af-b096305fd43e', 'MRG', 'Manzana Royal Gala', 'Manzana Royal Gala', '6.8', 'EUR','0.8','ab6aecb2-d180-414d-8658-3ac383e73cdd'),
                                         ('7565a988-fe5f-4629-a25b-b3899bf3aa91', 'MS', 'Manzana Starking', 'Manzana Starking', '9.1', 'EUR','0.7','ab6aecb2-d180-414d-8658-3ac383e73cdd'),
                                         ('89078a56-c56e-4434-9edf-b0bc62bf8420', 'MSS', 'Manzana Starkingson', 'Manzana Starkingson', '4.3', 'EUR','0.95','ab6aecb2-d180-414d-8658-3ac383e73cdd'),
                                         ('c0abf048-5b6f-40e8-b13f-65fceb11a929', 'ML', 'Melon de Sapo', 'Melon de Sapo', '14.6', 'EUR','0.8','ab6aecb2-d180-414d-8658-3ac383e73cdd'),
                                         ('0fd4949c-f0b8-4844-9b04-4f064b15be7e', 'MLG', 'Melon Galia', 'Melon Galia', '10.2', 'EUR','1.1','ab6aecb2-d180-414d-8658-3ac383e73cdd'),
                                         ('0e21ec78-7179-44ca-9e3b-d7d369f7a41c', 'N', 'Naranja', 'Naranja', '16.4', 'EUR','1.35','ab6aecb2-d180-414d-8658-3ac383e73cdd'),
                                         ('1eceb773-b641-4e2e-b30a-0e7a54401c3d', 'NB2K', 'Naranja en Bolsa 2 Kg', 'Naranja en Bolsa 2 Kg', '18', 'EUR','1.55','93ba2608-89cb-493f-848e-65169261d813'),
                                         ('19bb9441-4e0f-4592-9178-4f19e748987a', 'NES', 'Naranja Estrio', 'Naranja Estrio', '20.5', 'EUR','0.9','ab6aecb2-d180-414d-8658-3ac383e73cdd'),
                                         ('84453e9f-3b07-47aa-96fa-5a855f131d17', 'NV', 'Naranja Navelina', 'Naranja Navelina', '15.8', 'EUR','1.85','ab6aecb2-d180-414d-8658-3ac383e73cdd'),
                                         ('eae283cb-add8-42e0-86e5-c1437a2de7be', 'PCO', 'Pera Comicio', 'Pera Comicio', '8.6', 'EUR','1.65','ab6aecb2-d180-414d-8658-3ac383e73cdd'),
                                         ('4e2c114f-3434-4eaa-b0a5-1bd0c095f003', 'PEC', 'Pera Conferencia', 'Pera Conferencia', '8.2', 'EUR','1.95','ab6aecb2-d180-414d-8658-3ac383e73cdd'),
                                         ('ded10794-4c13-493b-8fa2-ef88843e9873', 'PA', 'Pera de Agua', 'Pera de Agua', '10.8', 'EUR','1.25','ab6aecb2-d180-414d-8658-3ac383e73cdd'),
                                         ('71c796b7-e81e-4400-840f-3cc38f8cf5a0', 'PE', 'Pera Ercolina', 'Pera Ercolina', '8.5', 'EUR','1.45','ab6aecb2-d180-414d-8658-3ac383e73cdd'),
                                         ('a940727d-375e-4e5d-ba15-7513ba7d72c3', 'PL', 'Pera Limonera', 'Pera Limonera', '11.3', 'EUR','1.35','ab6aecb2-d180-414d-8658-3ac383e73cdd'),
                                         ('0103e948-da56-48cb-b499-6b412766dca2', 'PC', 'Picota', 'Picota', '2', 'EUR','4.6','93ba2608-89cb-493f-848e-65169261d813'),
                                         ('ff14c3ab-fb84-4892-bd8a-95d9185f33d8', 'SAN', 'Sandia', 'Sandia', '18.8', 'EUR','0.75','ab6aecb2-d180-414d-8658-3ac383e73cdd'),
                                         ('cd1a61d1-e3bf-4d10-a0a2-41672ad47958', 'SANS', 'Sandia sin Pepita', 'Sandia sin Pepita', '19.6', 'EUR','0.9','ab6aecb2-d180-414d-8658-3ac383e73cdd'),
                                         ('4db3c999-ef05-42bc-97e1-5bdf717033e5', 'UB', 'Uva Blanca', 'Uva Blanca', '10.1', 'EUR','1.1','ab6aecb2-d180-414d-8658-3ac383e73cdd'),
                                         ('f41f6e55-1ebf-4c23-a242-59b824b6689d', 'UM', 'Uva Moscatel', 'Uva Moscatel', '10.2', 'EUR','1.35','ab6aecb2-d180-414d-8658-3ac383e73cdd'),
                                         ('0224a814-82a0-4d16-ab75-0829bb9b9696', 'UR', 'Uva Rosada', 'Uva Rosada', '10.3', 'EUR','1.5','ab6aecb2-d180-414d-8658-3ac383e73cdd'),
                                         ('d1c58266-4f28-4d70-b16d-5b77a4063b81', 'UV', 'Uva Villlanueva', 'Uva Villlanueva', '10.3', 'EUR','1.8','ab6aecb2-d180-414d-8658-3ac383e73cdd'),
                                         ('584fb877-a56d-4de4-8fba-1cfa4ffe09ba', '1GDB', 'Vino Gran Duque Blanco', 'Vino Gran Duque Blanco', '12', 'EUR','1.25','5dbd86d1-f989-47fd-ac4d-ab2bf4ea3564'),
                                         ('379b784f-e36d-403b-99ba-b669e4c2da6c', '1GDR', 'Vino Gran Duque Rosado', 'Vino Gran Duque Rosado', '12', 'EUR','1.4','5dbd86d1-f989-47fd-ac4d-ab2bf4ea3564'),
                                         ('f3050d20-de57-45bb-874b-1423f4c73608', '1GDT', 'Vino Gran Duque Tinto', 'Vino Gran Duque Tinto', '12', 'EUR','1.3','5dbd86d1-f989-47fd-ac4d-ab2bf4ea3564');");
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
                                          'Company, S.L.',
                                          '2dda9061-4e0a-4c8a-bc10-cd998bb9946f',
                                          'B37232972',
                                          'Ibiza, 55',
                                          'Madrid',
                                          'Madrid',
                                          'España',
                                          '28024',
                                          '+34 634 98 34 85',
                                          'companysl@company.com');");
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
                                          'Jose Antonio',
                                          'Vazquez Gutierrez',
                                          'ae2e1245-885b-477f-a79d-cb273e7c2bc1',
                                          '16935404K',
                                          'Pirotecnia, 12',
                                          'Madrid',
                                          'Madrid',
                                          'España',
                                          '28016',
                                          '+34 617 41 89 34',
                                          'joseantoniovazquezgutierrez@customer.com'),
                                         ('d9d34497-296a-470f-b0d8-196d9eb5f010',
                                          'Jose Luis',
                                          'Fernandez Cortes',
                                          '8b751cfd-fefc-4a6c-8733-9e4fdb68decd',
                                          '07769593D',
                                          'Granja, 21',
                                          'Madrid',
                                          'Madrid',
                                          'España',
                                          '28012',
                                          '+34 657 22 17 56',
                                          'joseluisfernandezcortes@customer.com');");
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
                                          '',
                                          'null',
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
                                   VALUES('65fb5618-33c1-4849-bdc6-c2beab5e6ef1',
                                           'b3d9bb06-9514-45ed-a028-022867727d0a',
                                           '1',
                                           '1',
                                           'DEFAULT PRODUCT',
                                           'DEFAULT PRODUCT',
                                           2,
                                           2,
                                           'EUR'',
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
                                   VALUES('ae67fb39-a323-4dea-a862-c03f895c9249',
                                           'b3d9bb06-9514-45ed-a028-022867727d0a',
                                           '1',
                                           '1',
                                           'DEFAULT PRODUCT',
                                           'DEFAULT PRODUCT',
                                           5,
                                           5,
                                           'EUR'',
                                           1.2,
                                           '10%',
                                           10,
                                           5);");
        }

    }
}