namespace TestSoftUni.Core
{
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using TestSoftUni.Core.Contracts;
    using TestSoftUni.Infrastructure.Data;
    public class ServicesBox
    {
        private static ServiceCollection serviceCollection;
        static ServicesBox()
        {
            serviceCollection = new ServiceCollection();

            serviceCollection.AddSingleton<IEngine, Engine>();
            serviceCollection.AddSingleton<ICommandInterpreter, CommandIntepreter>();
            serviceCollection.AddSingleton<Mapper>();
            serviceCollection.AddDbContext<TContext>(options =>
                                   {
                                       options.UseSqlServer(@"Server=ZEVS-PC\SQLEXPRESS;Database=T3STing;Integrated Security=true",
                                                    s => s.MigrationsAssembly("TestSoftUni.Infrastructure.Data"));

                                   });
            ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        public static ServiceProvider ServiceProvider { get; }
               
    }
}
