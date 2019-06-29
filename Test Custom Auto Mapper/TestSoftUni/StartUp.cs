using Microsoft.Extensions.DependencyInjection;
using TestSoftUni.Core;
using TestSoftUni.Core.Contracts;

namespace TestSoftUni
{
    public class StartUp
    {
        static void Main()
        {
            ServicesBox.ServiceProvider.GetService<IEngine>().Run();
        }
    }
}