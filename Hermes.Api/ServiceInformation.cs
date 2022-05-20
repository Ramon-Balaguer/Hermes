namespace Hermes.Api;

public class ServiceInformation
{
    public ServiceInformation(int[] ports, string name)
    {
        this.Ports = ports;
        this.Name = name;
    }

    public int[] Ports { get; init; }
    public string Name { get; init; }
}