using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace SimplexInvoice.Infra.Data
{
    public static class DefaultInitialData
    {
        public static void AddDefaultData(MigrationBuilder migrationBuilder)
        {              
            AddDefaultUserData(migrationBuilder);
            AddDefaultIdDocumentTypeData(migrationBuilder);
            AddDefaultTaxRateData(migrationBuilder);
        }

        private static void AddDefaultUserData(MigrationBuilder migrationBuilder)
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

        private static void AddDefaultIdDocumentTypeData(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"INSERT INTO simplexinvoice.IdDocumentType (Id, Name)
                                   VALUES('25cdf776-e147-4898-92b7-a3280cbe9a34', 'NIE'),
                                         ('2dda9061-4e0a-4c8a-bc10-cd998bb9946f', 'CIF'),
                                         ('8b751cfd-fefc-4a6c-8733-9e4fdb68decd', 'NIF'),
                                         ('ae2e1245-885b-477f-a79d-cb273e7c2bc1', 'DNI')");
        }

        private static void AddDefaultTaxRateData(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"INSERT INTO simplexinvoice.TaxRate (Id, Name, Value)
                                   VALUES('3248b83b-990e-4627-ac82-c4a146e81754', '0%', '0'),
                                         ('ab6aecb2-d180-414d-8658-3ac383e73cdd', '4%', '4'),
                                         ('93ba2608-89cb-493f-848e-65169261d813', '10%', '10'),
                                         ('5dbd86d1-f989-47fd-ac4d-ab2bf4ea3564', '21%', '21')");
        }

    }
}
