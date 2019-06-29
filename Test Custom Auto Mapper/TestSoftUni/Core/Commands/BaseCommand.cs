namespace TestSoftUni.Core.Commands
{
    using AutoMapper;
    using Contracts;
    using TestSoftUni.Infrastructure.Data;

    public abstract class BaseCommand : ICommand
    {
        protected Mapper mapper;
        protected TContext context;


        public BaseCommand(TContext context,Mapper mapper)
        {
            this.mapper = mapper;
            this.context = context;
        }
        
        public abstract string Execute(string[] input);
    }
}