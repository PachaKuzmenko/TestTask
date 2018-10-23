namespace TestTask.DAL
{
  public class Service
  {
    public string Name { get; set; }

    public Service() { }

    public Service(string name)
    {
      Name = name;
    }

    public override string ToString()
    {
      return Name;
    }
  }
}
