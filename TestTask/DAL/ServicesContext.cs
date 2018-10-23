using System;
using System.Collections.Generic;
using System.IO;

namespace TestTask.DAL
{
  public class ServicesContext : IDisposable
  {
    private readonly string _filePath;
    private readonly DateTime _dateFrom;
    private readonly DateTime _dateTo;

    public ServicesContext(string filePath, DateTime dateFrom, DateTime dateTo)
    {
      if (String.IsNullOrEmpty(filePath))
      {
        throw new ApplicationException("File path can not be NULL or Empty");
      }

      if (dateFrom == null || dateTo == null)
      {
        throw new ApplicationException("DateFrom or DateTo can not be null");
      }

      _filePath = filePath;
      _dateFrom = dateFrom;
      _dateTo = dateTo;
    }

    private List<ServiceInteractions> _serviceInteractions;

    public List<ServiceInteractions> GetServiceInteractions
    {
      get
      {
        if (_serviceInteractions != null)
        {
          return _serviceInteractions;
        }

        _serviceInteractions = new List<ServiceInteractions>();
        using (StreamReader reader = new StreamReader(_filePath))
        {
          string line;
          while ((line = reader.ReadLine()) != null)
          {
            var splitLine = line.Split(' ');
            if (splitLine.Length == 4)
            {
              ServiceInteractions serviceIteract;
              try
              {
                serviceIteract = new ServiceInteractions
                {
                  Date = DateTime.Parse($"{splitLine[0]} {splitLine[1]}"),
                  FromNode = new Service(splitLine[2]),
                  ToNode = new Service(splitLine[3])
                };
              }
              catch (Exception ex)
              {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
                continue;
              }
              _serviceInteractions.Add(serviceIteract);
            }
          }
        }
        return _serviceInteractions;
      }
    }

    public void Dispose()
    {
      GC.SuppressFinalize(this);
    }
  }
}
