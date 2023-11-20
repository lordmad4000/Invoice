using Microsoft.EntityFrameworkCore;
using SimplexInvoice.Domain.Companies;
using SimplexInvoice.Domain.Customers;
using SimplexInvoice.Domain.Entities;
using SimplexInvoice.Domain.IdDocumentTypes;
using SimplexInvoice.Domain.Invoices;
using SimplexInvoice.Domain.Products;
using SimplexInvoice.Domain.TaxRates;
using SimplexInvoice.Domain.Users;
using SimplexInvoice.Domain.ValueObjects;
using System;
using System.Linq;

namespace SimplexInvoice.Infra.Data;

public static class SeedData
{
    public static void DefaultData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasData(GetUsersData());

        modelBuilder.Entity<AppConfiguration>().HasData(GetAppConfigurationsData());

        var idDocumentTypes = GetIdDocumentTypesData();
        modelBuilder.Entity<IdDocumentType>().HasData(idDocumentTypes);

        var taxRates = GetTaxRatesData();
        modelBuilder.Entity<TaxRate>().HasData(taxRates);

        var company = GetCompanyData(idDocumentTypes);
        modelBuilder.Entity<Company>().HasData(company);

        var customers = GetCustomersData(idDocumentTypes);
        modelBuilder.Entity<Customer>().HasData(customers);

        var products = GetProductsData(taxRates);
        modelBuilder.Entity<Product>().HasData(products);

        modelBuilder.Entity<Invoice>().HasData(GetInvoiceData(company, customers, products));
    }

    private static User[] GetUsersData()
    {
        var users = new User[]
        {
            User.Create("admin@gmail.com",                            
                        "ddz4RPMmrctg9HEUd7xx2w==,kincfsSHNqMOF9SroDS+koqPrd4=",
                        "Admin",
                        "Admin"),
        };

        return users;
    }

    private static AppConfiguration GetAppConfigurationsData()
    {
        var appConfiguration = new AppConfiguration();

        return appConfiguration;
    }

    private static IdDocumentType[] GetIdDocumentTypesData()
    {
        var idDocumentTypes = new IdDocumentType[]
        {
            IdDocumentType.Create("NIE"),
            IdDocumentType.Create("CIF"),
            IdDocumentType.Create("NIF"),
            IdDocumentType.Create("DNI"),
        };

        return idDocumentTypes;
    }

    private static TaxRate[] GetTaxRatesData()
    {
        var taxRatess = new TaxRate[]
        {
            TaxRate.Create("0", 0),
            TaxRate.Create("4", 4),
            TaxRate.Create("10", 10),
            TaxRate.Create("21", 21),
        };

        return taxRatess;
    }

    private static Company GetCompanyData(IdDocumentType[] idDocumentTypes)
    {
        var idDocumentTypeCIF = idDocumentTypes.First(c => c.Name.Equals("CIF"));

        var company = Company.Create("Company, S.L.",
                                     idDocumentTypeCIF.Id,
                                     "B37232972",
                                     new Address("Ibiza, 55",
                                                 "Madrid",
                                                 "Madrid",
                                                 "España",
                                                 "28016"),
                                     new PhoneNumber("+34 689 45 96 34"),
                                     new EmailAddress("company@company.com"));

        return company;
    }

    private static Customer[] GetCustomersData(IdDocumentType[] idDocumentTypes)
    {
        var idDocumentTypeNIF = idDocumentTypes.First(c => c.Name.Equals("NIF"));
        var idDocumentTypeDNI = idDocumentTypes.First(c => c.Name.Equals("DNI"));

        var customers = new Customer[]
        {
            Customer.Create("Jose Luis",
                            "Fernandez Cortes",
                            idDocumentTypeNIF.Id,
                            "07769593D",
                            new Address("Granja, 21",
                                        "Madrid",
                                        "Madrid",
                                        "España",
                                        "28012"),
                            new PhoneNumber("+34 657 22 17 56"),
                            new EmailAddress("joseluisfernandezcortes@customer.com")),
            Customer.Create("Jose Antonio",
                            "Vazquez Gutierrez",
                            idDocumentTypeDNI.Id,
                            "07769593D",
                            new Address("Granja, 21",
                                        "Madrid",
                                        "Madrid",
                                        "España",
                                        "28012"),
                            new PhoneNumber("+34 657 22 17 56"),
                            new EmailAddress("joseluisfernandezcortes@customer.com")),
        };

        return customers;
    }

    private static Product[] GetProductsData(TaxRate[] taxRates)
    {
        var taxRate4 = taxRates.First(c => c.Value == 4);
        var taxRate10 = taxRates.First(c => c.Value == 10);
        var taxRate21 = taxRates.First(c => c.Value == 21);
        var products = new Product[]
        {
            Product.Create("AG", "Aguacate", "Aguacate", 4, new Money("EUR", 6.15), taxRate10.Id),
            Product.Create("ALB", "Albaricoque", "Albaricoque", 6.5, new Money("EUR", 1.8), taxRate4.Id),
            Product.Create("ALBM", "Albaricoque Moniqui", "Albaricoque Moniqui", 6.2, new Money("EUR", 1.9), taxRate4.Id),
            Product.Create("CE", "Cereza", "Cereza", 2, new Money("EUR", 4.5), taxRate10.Id),
            Product.Create("CB", "Ciruela Blanca", "Ciruela Blanca", 7.1, new Money("EUR", 2.1), taxRate4.Id),
            Product.Create("CN", "Ciruela Negra", "Ciruela Negra", 6.8, new Money("EUR", 2.05), taxRate4.Id),
            Product.Create("FRE", "Fresa", "Fresa", 2, new Money("EUR", 3.5), taxRate4.Id),
            Product.Create("FR", "Freson", "Freson", 2, new Money("EUR", 3), taxRate4.Id),
            Product.Create("G", "Granada", "Granada", 5.4, new Money("EUR", 1.35), taxRate10.Id),
            Product.Create("HB", "Higo Blanco", "Higo Blanco", 2.4, new Money("EUR", 2.4), taxRate4.Id),
            Product.Create("HN", "Higo Negro", "Higo Negro", 2.6, new Money("EUR", 2.6), taxRate4.Id),
            Product.Create("L", "Limon", "Limon", 10.5, new Money("EUR", 1), taxRate4.Id),
            Product.Create("LBO", "Limon en Bolsa", "Limon en Bolsa", 12, new Money("EUR", 1.1), taxRate10.Id),
            Product.Create("3MCP", "Mahou 5 Clasica Bote", "Mahou 5 Clasica Bote", 24, new Money("EUR", 0.35), taxRate21.Id),
            Product.Create("3MCB", "Mahou 5 Clasica Pack", "Mahou 5 Clasica Pack", 24, new Money("EUR", 0.38), taxRate21.Id),
            Product.Create("3MB", "Mahou 5 Estrellas Bote", "Mahou 5 Estrellas Bote", 24, new Money("EUR", 0.4), taxRate21.Id),
            Product.Create("3MP", "Mahou 5 Estrellas Pack", "Mahou 5 Estrellas Pack", 24, new Money("EUR", 0.44), taxRate21.Id),
            Product.Create("MF", "Manzana Fuji", "Manzana Fuji", 4.2, new Money("EUR", 2.75), taxRate4.Id),
            Product.Create("MG", "Manzana Golden", "Manzana Golden", 8.5, new Money("EUR", 0.9), taxRate4.Id),
            Product.Create("MGF", "Manzana Golden Frances", "Manzana Golden Frances", 4.2, new Money("EUR", 1), taxRate4.Id),
            Product.Create("MGR", "Manzana Granny Smith", "Manzana Granny Smith", 4.2, new Money("EUR", 1.2), taxRate4.Id),
            Product.Create("MR", "Manzana Reineta", "Manzana Reineta", 4.5, new Money("EUR", 1.35), taxRate4.Id),
            Product.Create("MRG", "Manzana Royal Gala", "Manzana Royal Gala", 6.8, new Money("EUR", 0.8), taxRate4.Id),
            Product.Create("MS", "Manzana Starking", "Manzana Starking", 9.1, new Money("EUR", 0.7), taxRate4.Id),
            Product.Create("MSS", "Manzana Starkingson", "Manzana Starkingson", 4.3, new Money("EUR", 0.95), taxRate4.Id),
            Product.Create("ML", "Melon de Sapo", "Melon de Sapo", 14.6, new Money("EUR", 0.8), taxRate4.Id),
            Product.Create("MLG", "Melon Galia", "Melon Galia", 10.2, new Money("EUR", 1.1), taxRate4.Id),
            Product.Create("N", "Naranja", "Naranja", 16.4, new Money("EUR", 1.35), taxRate4.Id),
            Product.Create("NB2K", "Naranja en Bolsa 2 Kg", "Naranja en Bolsa 2 Kg", 18, new Money("EUR", 1.55), taxRate10.Id),
            Product.Create("NES", "Naranja Estrio", "Naranja Estrio", 20.5, new Money("EUR", 0.9), taxRate4.Id),
            Product.Create("NV", "Naranja Navelina", "Naranja Navelina", 15.8, new Money("EUR", 1.85), taxRate4.Id),
            Product.Create("PCO", "Pera Comicio", "Pera Comicio", 8.6, new Money("EUR", 1.65), taxRate4.Id),
            Product.Create("PEC", "Pera Conferencia", "Pera Conferencia", 8.2, new Money("EUR", 1.95), taxRate4.Id),
            Product.Create("PA", "Pera de Agua", "Pera de Agua", 10.8, new Money("EUR", 1.25), taxRate4.Id),
            Product.Create("PE", "Pera Ercolina", "Pera Ercolina", 8.5, new Money("EUR", 1.45), taxRate4.Id),
            Product.Create("PL", "Pera Limonera", "Pera Limonera", 11.3, new Money("EUR", 1.35), taxRate4.Id),
            Product.Create("PC", "Picota", "Picota", 2, new Money("EUR", 4.6), taxRate10.Id),
            Product.Create("SAN", "Sandia", "Sandia", 18.8, new Money("EUR", 0.75), taxRate4.Id),
            Product.Create("SANS", "Sandia sin Pepita", "Sandia sin Pepita", 19.6, new Money("EUR", 0.9), taxRate4.Id),
            Product.Create("UB", "Uva Blanca", "Uva Blanca", 10.1, new Money("EUR", 1.1), taxRate4.Id),
            Product.Create("UM", "Uva Moscatel", "Uva Moscatel", 10.2, new Money("EUR", 1.35), taxRate4.Id),
            Product.Create("UR", "Uva Rosada", "Uva Rosada", 10.3, new Money("EUR", 1.5), taxRate4.Id),
            Product.Create("UV", "Uva Villlanueva", "Uva Villlanueva", 10.3, new Money("EUR", 1.8), taxRate4.Id),
            Product.Create("1GDB", "Vino Gran Duque Blanco", "Vino Gran Duque Blanco", 12, new Money("EUR", 1.25), taxRate21.Id),
            Product.Create("1GDR", "Vino Gran Duque Rosado", "Vino Gran Duque Rosado", 12, new Money("EUR", 1.4), taxRate21.Id),
            Product.Create("1GDT", "Vino Gran Duque Tinto", "Vino Gran Duque Tinto", 12, new Money("EUR", 1.3), taxRate21.Id),
        };

        return products;
    }

    private static Invoice GetInvoiceData(Company company, Customer[] customers, Product[] products)
    {
        var product1 = products.First(c => c.Code.Equals("AG"));
        var product2 = products.First(c => c.Code.Equals("PA"));
        var customer1 = customers.First();

        var invoiceLines = new InvoiceLine[]
        {
            InvoiceLine.Create(Guid.NewGuid(),
                               1,
                               product1.Code,
                               product1.Name,
                               product1.Description,
                               2,
                               product1.PackageQuantity * 2,
                               product1.UnitPrice,
                               product1.TaxRate.Name,
                               product1.TaxRate.Value,
                               0),
            InvoiceLine.Create(Guid.NewGuid(),
                               1,
                               product2.Code,
                               product2.Name,
                               product2.Description,
                               5,
                               product2.PackageQuantity * 5,
                               product2.UnitPrice,
                               product2.TaxRate.Name,
                               product2.TaxRate.Value,
                               0),
        };

        Invoice invoice = Invoice.Create("FFFFFFFFFF",
                                         "Test Invoice",
                                         company.Name,
                                         company.IdDocumentType.Name,
                                         company.IdDocumentNumber,
                                         company.Address,
                                         company.Phone,
                                         company.EmailAddress,
                                         $"{customer1.FirstName} {customer1.LastName}",
                                         customer1.IdDocumentType.Name,
                                         customer1.IdDocumentNumber,
                                         customer1.Address,
                                         customer1.Phone,
                                         customer1.EmailAddress,
                                         invoiceLines);


        return invoice;
    }

}
