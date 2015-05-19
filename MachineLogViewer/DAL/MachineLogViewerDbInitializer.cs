using System;
using System.Collections.Generic;
using MachineLogViewer.Models;

namespace MachineLogViewer.DAL
{
    public class MachineLogViewerDbInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<MachineLogViewerContext>
    {
        protected override void Seed(MachineLogViewerContext context)
        {
            var machines = new List<Machine>
            {
                new Machine {Description = "Machine 1", ExpiryDate = new DateTime(2015, 9, 30)},
                new Machine {Description = "Machine 2", ExpiryDate = new DateTime(2015, 4, 30)},
                new Machine {Description = "Machine 3", ExpiryDate = new DateTime(2015, 11, 30)},
                new Machine {Description = "Machine 4", ExpiryDate = new DateTime(2015, 12, 31)},
                new Machine {Description = "Machine 5", ExpiryDate = new DateTime(2016, 1, 31)},
                new Machine {Description = "Machine 6", ExpiryDate = new DateTime(2015, 1, 31)},
                new Machine {Description = "Machine 7", ExpiryDate = new DateTime(2016, 7, 31)},

            };

            machines.ForEach(m => context.Machines.Add(m));
            context.SaveChanges();

            var logEntries = new List<LogEntry>
            {
                new LogEntry {MachineId = 1, Time = new DateTime(2015, 1, 30, 13, 23, 02), Category = Category.A},
                new LogEntry {MachineId = 1, Time = new DateTime(2015, 2, 12, 10, 13, 29), Category = Category.B},
                new LogEntry {MachineId = 1, Time = new DateTime(2015, 2, 28, 09, 11, 30), Category = Category.A},
                new LogEntry {MachineId = 2, Time = new DateTime(2015, 3, 30, 08, 02, 56), Category = Category.C},
                new LogEntry {MachineId = 2, Time = new DateTime(2015, 4, 11, 11, 44, 38), Category = Category.A},
                new LogEntry {MachineId = 2, Time = new DateTime(2015, 4, 13, 23, 11, 52), Category = Category.D},
                new LogEntry {MachineId = 2, Time = new DateTime(2015, 4, 16, 03, 25, 36), Category = Category.A},
                new LogEntry {MachineId = 3, Time = new DateTime(2015, 3, 14, 21, 25, 02), Category = Category.A},
                new LogEntry {MachineId = 3, Time = new DateTime(2015, 3, 20, 21, 03, 43), Category = Category.C},
                new LogEntry {MachineId = 4, Time = new DateTime(2015, 2, 16, 16, 13, 24), Category = Category.A},
                new LogEntry {MachineId = 4, Time = new DateTime(2015, 4, 12, 10, 43, 10), Category = Category.D},
                new LogEntry {MachineId = 5, Time = new DateTime(2015, 4, 09, 21, 52, 05), Category = Category.A},
                new LogEntry {MachineId = 6, Time = new DateTime(2015, 1, 30, 22, 00, 28), Category = Category.A},
                new LogEntry {MachineId = 7, Time = new DateTime(2015, 3, 19, 04, 03, 24), Category = Category.B},
                new LogEntry {MachineId = 7, Time = new DateTime(2015, 3, 28, 05, 52, 11), Category = Category.A},
                new LogEntry {MachineId = 7, Time = new DateTime(2015, 5, 03, 11, 20, 10), Category = Category.C},
            };

            logEntries.ForEach(le => context.LogEntries.Add(le));
            context.SaveChanges();



            base.Seed(context);
        }
    }
}