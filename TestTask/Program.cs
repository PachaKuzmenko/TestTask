using System;
using System.IO;
using TestTask.DAL;
using System.Linq;

namespace TestTask
{
  class Program
  {
    static void Main(string[] args)
    {
      while (true)
      {
        try
        {
          string filePath = String.Empty;
          DateTime dateFrom = DateTime.UtcNow;
          DateTime dateTo = DateTime.UtcNow;

          Console.WriteLine("Enter the file path:");
          filePath = Console.ReadLine();
          if (!File.Exists(filePath))
          {
            throw new ApplicationException("File does not exist.");
          }
          Console.WriteLine("Enter DateFrom ('DateTime' format):");
          dateFrom = DateTime.Parse(Console.ReadLine());
          Console.WriteLine("Enter DateTo ('DateTime' format):");
          dateTo = DateTime.Parse(Console.ReadLine());

          using (var context = new ServicesContext(filePath, dateFrom, dateTo))
          {
            Console.WriteLine($"Count: {context.GetServiceInteractions.Count}");
            var services = context.GetServiceInteractions.Select(x => x.FromNode.Name).Union(context.GetServiceInteractions.Select(x => x.ToNode.Name));
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Services Statistic:");
            Console.ResetColor();
            foreach (var item in services)
            {
              var countFrom = context.GetServiceInteractions.Where(x => String.Equals(x.FromNode.Name, item, StringComparison.Ordinal)).Count();
              var countTo = context.GetServiceInteractions.Where(x => String.Equals(x.ToNode.Name, item, StringComparison.Ordinal)).Count();
              Console.WriteLine($"{item}| From: {countFrom} To: {countTo}");
            }
          }
        }
        catch (Exception ex)
        {
          Console.ForegroundColor = ConsoleColor.Red;
          Console.WriteLine(ex.Message);
          Console.ResetColor();
          continue;
        }
        Console.ReadKey();
      }
    }
  }
}
