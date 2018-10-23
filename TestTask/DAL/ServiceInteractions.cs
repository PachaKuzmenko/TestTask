using System;

namespace TestTask.DAL
{
  public class ServiceInteractions
  {
    public DateTime Date { get; set; }
    public Service FromNode { get; set; }
    public Service ToNode { get; set; }

    public ServiceInteractions() { }

    public ServiceInteractions(DateTime date, Service fromNode, Service toNode)
    {
      Date = date;
      FromNode = fromNode;
      ToNode = toNode;
    }

    public override string ToString()
    {
      return $"{Date.Date.ToString("yyyy-MM-dd")} {Date.TimeOfDay.ToString("HH:mm:ss.ms")} {FromNode} {ToNode}";
    }
  }
}
