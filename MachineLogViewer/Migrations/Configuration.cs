using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using MachineLogViewer.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MachineLogViewer.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            roleManager.Create(new IdentityRole("admin"));

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            var adminUser = new ApplicationUser
            {
                UserName = "admin",
                Description = "Administrator",
                IsActive = true
            };
            userManager.Create(adminUser, "Passw0rdAdmin");
            userManager.AddToRole(adminUser.Id, "admin");

            var user1 = new ApplicationUser { UserName = "user1", Description = "User 1", IsActive = true };
            userManager.Create(user1, "Passw0rd1");

            var user2 = new ApplicationUser { UserName = "user2", Description = "User 2", IsActive = false };
            userManager.Create(user2, "Passw0rd2");

            var user3 = new ApplicationUser { UserName = "user3", Description = "User 3", IsActive = true };
            userManager.Create(user3, "Passw0rd3");


            var machines = new List<Machine>
            {
                new Machine { Code = "MACHINE00001", Description = "Machine 1", ExpiryDate = new DateTime(2015, 9, 30), User = user1 },
                new Machine { Code = "MACHINE00002", Description = "Machine 2", ExpiryDate = new DateTime(2015, 4, 30), User = user1 },
                new Machine { Code = "MACHINE00003", Description = "Machine 3", ExpiryDate = new DateTime(2015, 11, 30), User = user1 },
                new Machine { Code = "MACHINE00004", Description = "Machine 4", ExpiryDate = new DateTime(2015, 12, 31), User = user2 },
                new Machine { Code = "MACHINE00005", Description = "Machine 5", ExpiryDate = new DateTime(2016, 1, 31), User = user3 },
                new Machine { Code = "MACHINE00006", Description = "Machine 6", ExpiryDate = new DateTime(2015, 1, 31), User = user3 },
                new Machine { Code = "MACHINE00007", Description = "Machine 7", ExpiryDate = new DateTime(2016, 7, 31), User = user3 },

            };

            machines.ForEach(m => context.Machines.Add(m));
            context.SaveChanges();

            var logEntries = new List<LogEntry>
            {
                new LogEntry { MachineId = 1, EventTime = new DateTime(2015, 1, 30, 13, 23, 02), ReceivedTime = new DateTime(2015, 2, 1, 14, 12, 09), Category = 4, Description = "temp. error II cycle(run)" },
                new LogEntry { MachineId = 1, EventTime = new DateTime(2015, 2, 12, 10, 13, 29), ReceivedTime = new DateTime(2015, 2, 13, 10, 22, 29), Category = 9, Description = "mdb: cashflow error 60 sec." },
                new LogEntry { MachineId = 1, EventTime = new DateTime(2015, 2, 28, 09, 11, 30), ReceivedTime = new DateTime(2015, 2, 28, 14, 39, 19), Category = 5, Description = "temp. not reach after long timeout" },
                new LogEntry { MachineId = 2, EventTime = new DateTime(2015, 3, 30, 08, 02, 56), ReceivedTime = new DateTime(2015, 3, 30, 14, 37, 01), Category = 2, Description = "motor blocked" },
                new LogEntry { MachineId = 2, EventTime = new DateTime(2015, 4, 11, 11, 44, 38), ReceivedTime = new DateTime(2015, 4, 11, 18, 42, 29), Category = 11, Description = "Another description" },
                new LogEntry { MachineId = 2, EventTime = new DateTime(2015, 4, 13, 23, 11, 52), ReceivedTime = new DateTime(2015, 4, 14, 10, 02, 27), Category = 5, Description = "temp. not reach after long timeout" },
                new LogEntry { MachineId = 2, EventTime = new DateTime(2015, 4, 16, 03, 25, 36), ReceivedTime = new DateTime(2015, 4, 17, 21, 02, 26), Category = 18, Description = "motor not connected" },
                new LogEntry { MachineId = 3, EventTime = new DateTime(2015, 3, 14, 21, 25, 02), ReceivedTime = new DateTime(2015, 3, 16, 20, 05, 23), Category = 8, Description = "mdb: bill reader error 60 sec." },
                new LogEntry { MachineId = 3, EventTime = new DateTime(2015, 3, 20, 21, 03, 43), ReceivedTime = new DateTime(2015, 3, 21, 10, 34, 26), Category = 98, Description = "temp. error low sensor 2" },
                new LogEntry { MachineId = 4, EventTime = new DateTime(2015, 2, 16, 16, 13, 24), ReceivedTime = new DateTime(2015, 2, 16, 05, 13, 41), Category = 2, Description = "motor blocked" },
                new LogEntry { MachineId = 4, EventTime = new DateTime(2015, 4, 12, 10, 43, 10), ReceivedTime = new DateTime(2015, 4, 12, 14, 11, 49), Category = 146, Description = "mdb: cashflow error 146" },
                new LogEntry { MachineId = 5, EventTime = new DateTime(2015, 4, 09, 21, 52, 05), ReceivedTime = new DateTime(2015, 4, 10, 04, 52, 28), Category = 63, Description = "Bla bla" },
                new LogEntry { MachineId = 6, EventTime = new DateTime(2015, 1, 30, 22, 00, 28), ReceivedTime = new DateTime(2015, 1, 31, 06, 40, 15), Category = 130, Description = "mdb: bill reader checksum error" },
                new LogEntry { MachineId = 7, EventTime = new DateTime(2015, 3, 19, 04, 03, 24), ReceivedTime = new DateTime(2015, 3, 19, 14, 36, 00), Category = 27, Description = "Descr descr..." },
                new LogEntry { MachineId = 7, EventTime = new DateTime(2015, 3, 28, 05, 52, 11), ReceivedTime = new DateTime(2015, 3, 30, 09, 37, 04), Category = 17, Description = "motor in cc" },
                new LogEntry { MachineId = 7, EventTime = new DateTime(2015, 5, 03, 11, 20, 10), ReceivedTime = new DateTime(2015, 5, 10, 12, 01, 35), Category = 147, Description = "mdb: cashflow tube jam" },
            };

            logEntries.ForEach(le => context.LogEntries.Add(le));
            context.SaveChanges();

            var takings = new List<Takings>
            {
                new Takings { MachineId = 1, Date = new DateTime(2015, 1, 30, 13, 23, 02), Currency = "EUR", SumTotal = 120.0m, SumCash = 80.0m, SumCashless = 40.0m, SumProduct1 = 10.0m, SumProduct2 = 10.0m, SumProduct3 = 10.0m, SumProduct4 = 10.0m, SumProduct5 = 10.0m, SumProduct6 = 10.0m, SumProduct7 = 10.0m, SumProduct8 = 10.0m, SumProduct9 = 10.0m, SumProduct10 = 10.0m, SumProduct11 = 10.0m, SumProduct12 = 10.0m },
                new Takings { MachineId = 1, Date = new DateTime(2015, 2, 12, 10, 13, 29), Currency = "EUR", SumTotal = 120.0m, SumCash = 80.0m, SumCashless = 40.0m, SumProduct1 = 10.0m, SumProduct2 = 10.0m, SumProduct3 = 10.0m, SumProduct4 = 10.0m, SumProduct5 = 10.0m, SumProduct6 = 10.0m, SumProduct7 = 10.0m, SumProduct8 = 10.0m, SumProduct9 = 10.0m, SumProduct10 = 10.0m, SumProduct11 = 10.0m, SumProduct12 = 10.0m },
                new Takings { MachineId = 1, Date = new DateTime(2015, 2, 28, 09, 11, 30), Currency = "EUR", SumTotal = 120.0m, SumCash = 80.0m, SumCashless = 40.0m, SumProduct1 = 10.0m, SumProduct2 = 10.0m, SumProduct3 = 10.0m, SumProduct4 = 10.0m, SumProduct5 = 10.0m, SumProduct6 = 10.0m, SumProduct7 = 10.0m, SumProduct8 = 10.0m, SumProduct9 = 10.0m, SumProduct10 = 10.0m, SumProduct11 = 10.0m, SumProduct12 = 10.0m },
                new Takings { MachineId = 2, Date = new DateTime(2015, 3, 30, 08, 02, 56), Currency = "USD", SumTotal = 120.0m, SumCash = 80.0m, SumCashless = 40.0m, SumProduct1 = 10.0m, SumProduct2 = 10.0m, SumProduct3 = 10.0m, SumProduct4 = 10.0m, SumProduct5 = 10.0m, SumProduct6 = 10.0m, SumProduct7 = 10.0m, SumProduct8 = 10.0m, SumProduct9 = 10.0m, SumProduct10 = 10.0m, SumProduct11 = 10.0m, SumProduct12 = 10.0m },
                new Takings { MachineId = 2, Date = new DateTime(2015, 4, 11, 11, 44, 38), Currency = "USD", SumTotal = 120.0m, SumCash = 80.0m, SumCashless = 40.0m, SumProduct1 = 10.0m, SumProduct2 = 10.0m, SumProduct3 = 10.0m, SumProduct4 = 10.0m, SumProduct5 = 10.0m, SumProduct6 = 10.0m, SumProduct7 = 10.0m, SumProduct8 = 10.0m, SumProduct9 = 10.0m, SumProduct10 = 10.0m, SumProduct11 = 10.0m, SumProduct12 = 10.0m },
                new Takings { MachineId = 2, Date = new DateTime(2015, 4, 13, 23, 11, 52), Currency = "USD", SumTotal = 120.0m, SumCash = 80.0m, SumCashless = 40.0m, SumProduct1 = 10.0m, SumProduct2 = 10.0m, SumProduct3 = 10.0m, SumProduct4 = 10.0m, SumProduct5 = 10.0m, SumProduct6 = 10.0m, SumProduct7 = 10.0m, SumProduct8 = 10.0m, SumProduct9 = 10.0m, SumProduct10 = 10.0m, SumProduct11 = 10.0m, SumProduct12 = 10.0m },
                new Takings { MachineId = 2, Date = new DateTime(2015, 4, 16, 03, 25, 36), Currency = "USD", SumTotal = 120.0m, SumCash = 80.0m, SumCashless = 40.0m, SumProduct1 = 10.0m, SumProduct2 = 10.0m, SumProduct3 = 10.0m, SumProduct4 = 10.0m, SumProduct5 = 10.0m, SumProduct6 = 10.0m, SumProduct7 = 10.0m, SumProduct8 = 10.0m, SumProduct9 = 10.0m, SumProduct10 = 10.0m, SumProduct11 = 10.0m, SumProduct12 = 10.0m },
                new Takings { MachineId = 3, Date = new DateTime(2015, 3, 14, 21, 25, 02), Currency = "GBP", SumTotal = 120.0m, SumCash = 80.0m, SumCashless = 40.0m, SumProduct1 = 10.0m, SumProduct2 = 10.0m, SumProduct3 = 10.0m, SumProduct4 = 10.0m, SumProduct5 = 10.0m, SumProduct6 = 10.0m, SumProduct7 = 10.0m, SumProduct8 = 10.0m, SumProduct9 = 10.0m, SumProduct10 = 10.0m, SumProduct11 = 10.0m, SumProduct12 = 10.0m },
                new Takings { MachineId = 3, Date = new DateTime(2015, 3, 20, 21, 03, 43), Currency = "GBP", SumTotal = 120.0m, SumCash = 80.0m, SumCashless = 40.0m, SumProduct1 = 10.0m, SumProduct2 = 10.0m, SumProduct3 = 10.0m, SumProduct4 = 10.0m, SumProduct5 = 10.0m, SumProduct6 = 10.0m, SumProduct7 = 10.0m, SumProduct8 = 10.0m, SumProduct9 = 10.0m, SumProduct10 = 10.0m, SumProduct11 = 10.0m, SumProduct12 = 10.0m },
                new Takings { MachineId = 4, Date = new DateTime(2015, 2, 16, 16, 13, 24), Currency = "AUD", SumTotal = 120.0m, SumCash = 80.0m, SumCashless = 40.0m, SumProduct1 = 10.0m, SumProduct2 = 10.0m, SumProduct3 = 10.0m, SumProduct4 = 10.0m, SumProduct5 = 10.0m, SumProduct6 = 10.0m, SumProduct7 = 10.0m, SumProduct8 = 10.0m, SumProduct9 = 10.0m, SumProduct10 = 10.0m, SumProduct11 = 10.0m, SumProduct12 = 10.0m },
                new Takings { MachineId = 4, Date = new DateTime(2015, 4, 12, 10, 43, 10), Currency = "AUD", SumTotal = 120.0m, SumCash = 80.0m, SumCashless = 40.0m, SumProduct1 = 10.0m, SumProduct2 = 10.0m, SumProduct3 = 10.0m, SumProduct4 = 10.0m, SumProduct5 = 10.0m, SumProduct6 = 10.0m, SumProduct7 = 10.0m, SumProduct8 = 10.0m, SumProduct9 = 10.0m, SumProduct10 = 10.0m, SumProduct11 = 10.0m, SumProduct12 = 10.0m },
                new Takings { MachineId = 5, Date = new DateTime(2015, 4, 09, 21, 52, 05), Currency = "CAD", SumTotal = 120.0m, SumCash = 80.0m, SumCashless = 40.0m, SumProduct1 = 10.0m, SumProduct2 = 10.0m, SumProduct3 = 10.0m, SumProduct4 = 10.0m, SumProduct5 = 10.0m, SumProduct6 = 10.0m, SumProduct7 = 10.0m, SumProduct8 = 10.0m, SumProduct9 = 10.0m, SumProduct10 = 10.0m, SumProduct11 = 10.0m, SumProduct12 = 10.0m },
                new Takings { MachineId = 6, Date = new DateTime(2015, 1, 30, 22, 00, 28), Currency = "CHF", SumTotal = 120.0m, SumCash = 80.0m, SumCashless = 40.0m, SumProduct1 = 10.0m, SumProduct2 = 10.0m, SumProduct3 = 10.0m, SumProduct4 = 10.0m, SumProduct5 = 10.0m, SumProduct6 = 10.0m, SumProduct7 = 10.0m, SumProduct8 = 10.0m, SumProduct9 = 10.0m, SumProduct10 = 10.0m, SumProduct11 = 10.0m, SumProduct12 = 10.0m },
                new Takings { MachineId = 7, Date = new DateTime(2015, 3, 19, 04, 03, 24), Currency = "JPY", SumTotal = 120.0m, SumCash = 80.0m, SumCashless = 40.0m, SumProduct1 = 10.0m, SumProduct2 = 10.0m, SumProduct3 = 10.0m, SumProduct4 = 10.0m, SumProduct5 = 10.0m, SumProduct6 = 10.0m, SumProduct7 = 10.0m, SumProduct8 = 10.0m, SumProduct9 = 10.0m, SumProduct10 = 10.0m, SumProduct11 = 10.0m, SumProduct12 = 10.0m },
                new Takings { MachineId = 7, Date = new DateTime(2015, 3, 28, 05, 52, 11), Currency = "JPY", SumTotal = 120.0m, SumCash = 80.0m, SumCashless = 40.0m, SumProduct1 = 10.0m, SumProduct2 = 10.0m, SumProduct3 = 10.0m, SumProduct4 = 10.0m, SumProduct5 = 10.0m, SumProduct6 = 10.0m, SumProduct7 = 10.0m, SumProduct8 = 10.0m, SumProduct9 = 10.0m, SumProduct10 = 10.0m, SumProduct11 = 10.0m, SumProduct12 = 10.0m },
                new Takings { MachineId = 7, Date = new DateTime(2015, 5, 03, 11, 20, 10), Currency = "JPY", SumTotal = 120.0m, SumCash = 80.0m, SumCashless = 40.0m, SumProduct1 = 10.0m, SumProduct2 = 10.0m, SumProduct3 = 10.0m, SumProduct4 = 10.0m, SumProduct5 = 10.0m, SumProduct6 = 10.0m, SumProduct7 = 10.0m, SumProduct8 = 10.0m, SumProduct9 = 10.0m, SumProduct10 = 10.0m, SumProduct11 = 10.0m, SumProduct12 = 10.0m },
            };

            takings.ForEach(t => context.Takings.Add(t));
            context.SaveChanges();

            var logDescriptions = new List<LogDescription>
            {
                new LogDescription { Value = 2, Description = "motor blocked"},
                new LogDescription { Value = 17, Description = "motor in cc"},
                new LogDescription { Value = 18, Description = "motor not connected"},
                new LogDescription { Value = 3, Description = "temp. error I cycle(startup)"},
                new LogDescription { Value = 4, Description = "temp. error II cycle(run)"},
                new LogDescription { Value = 5, Description = "temp. not reach after long timeout"},
                new LogDescription { Value = 7, Description = "mdb: tag reader error 60 sec." },
                new LogDescription { Value = 8, Description = "mdb: bill reader error 60 sec."},
                new LogDescription { Value = 9, Description = "mdb: cashflow error 60 sec."},
                new LogDescription { Value = 97, Description = "temp. error low sensor 1"},
                new LogDescription { Value = 98, Description = "temp. error low sensor 2"},
                new LogDescription { Value = 99, Description = "temp. error high sensor 1"},
                new LogDescription { Value = 100, Description = "temp. error high sensor 2"},
                new LogDescription { Value = 128, Description = "mdb: bill reader motor defective"},
                new LogDescription { Value = 129, Description = "mdb: bill reader sensor defective"},
                new LogDescription { Value = 130, Description = "mdb: bill reader checksum error"},
                new LogDescription { Value = 131, Description = "mdb: bill reader bill jam"},
                new LogDescription { Value = 132, Description = "mdb: bill reader cash box out of position"}, // originally 131
                new LogDescription { Value = 144, Description = "mdb: cashflow defective tube sensor"},
                new LogDescription { Value = 145, Description = "mdb: cashflow error 145"},
                new LogDescription { Value = 146, Description = "mdb: cashflow error 146"},
                new LogDescription { Value = 147, Description = "mdb: cashflow tube jam"},
                new LogDescription { Value = 148, Description = "mdb: cashflow error 148"},
                new LogDescription { Value = 149, Description = "mdb: cashflow error 149"},
                new LogDescription { Value = 150, Description = "mdb: cashflow error 150"},
                new LogDescription { Value = 151, Description = "mdb: cashflow error 151"},
                new LogDescription { Value = 152, Description = "mdb: cashflow error 152"},
                new LogDescription { Value = 153, Description = "mdb: cashflow error 153"}
            };

            logDescriptions.ForEach(d => context.LogDescriptions.Add(d));
            context.SaveChanges();

            base.Seed(context);

        }
    }
}
