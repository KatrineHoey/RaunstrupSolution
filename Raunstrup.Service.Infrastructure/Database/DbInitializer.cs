using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Raunstrup.Service.Infrastructure.Entities;

namespace Raunstrup.Service.Infrastructure.Database
{
    public class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new RaunstrupContext(serviceProvider.GetRequiredService<DbContextOptions<RaunstrupContext>>()))
            {
                context.Database.EnsureCreated();

                // Look for any customers osv..
                if (!context.Customers.Any())
                {
                    var customers = new Customer[]
 {
            new Customer{Name="Carson B",PhoneNo=23232323,Email="Carson@bob.com",Address="Bobvej 5", City="7100 Vejle", DiscountGroup=0},
            new Customer{Name="Carson C",PhoneNo=24232323,Email="Carson@bob2.com",Address="Bobvej 6", City="7100 Vejle", DiscountGroup=2}
 };
                    foreach (Customer c in customers)
                    {
                        context.Customers.Add(c);
                    }
                    context.SaveChanges();
                }

                if (!context.Items.Any())
                {
                    var items = new Item[]
{
            new Item{ItemNo = 455, ItemName="Træstamme", PurchasePrice=7,SalePrice=49, MeasuringUnit="N/A", Active = true},
            new Item{ItemNo = 456, ItemName="Træstamme 2", PurchasePrice=7,SalePrice=49, MeasuringUnit="N/A", Active = true},
};
                    foreach (Item i in items)
                    {
                        context.Items.Add(i);
                    }
                    context.SaveChanges();
                }

                if (!context.Professions.Any())
                {
                    var professions = new Profession[]
                    {
                        new Profession{Type = "Lærling", HourPrice = 70 },
                        new Profession { Type = "Udlært", HourPrice = 150 },
                        new Profession { Type = "Projektleder", HourPrice = 170 },
                        new Profession { Type = "Kontor", HourPrice = 150 }
                    };
                    foreach (Profession item in professions)
                    {
                        context.Professions.Add(item);
                    }
                    context.SaveChanges();
                }

                if (!context.Employees.Any())
                {

                    var employees = new Employee[]
                    {
             new Employee{Cpr = 1412821112 ,Name="Torben", PhoneNo=66141448, Email = "to@to.dk", Address = "bagervej 12", City="Vejle", PostalCode = 7100, ProfessionRefID = 1,Specialisation ="Velux", Username = "torben@raunstrup.dk"},
             new Employee{Cpr = 1412821112 ,Name="Torben 2", PhoneNo=66941448, Email = "to@to6.dk", Address = "bagervej 13", City="Vejle", PostalCode = 7100, ProfessionRefID = 3,Specialisation ="Velux", Username = "torben2@raunstrup.dk"}
                    };
                    foreach (Employee m in employees)
                    {
                        context.Employees.Add(m);
                    }
                    context.SaveChanges();
                }


               
            }
        }
    }
}
