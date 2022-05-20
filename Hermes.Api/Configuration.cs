namespace Hermes.Api;

public class Configuration
{
    public Configuration(int[] ports, string name)
    {
        this.Ports = ports;
        this.Name = name;
    }

    public int[] Ports { get; init; }
    public string Name { get; init; }
}