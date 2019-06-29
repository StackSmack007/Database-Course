using System;
using System.Collections.Generic;
using System.Text;

namespace TestSoftUni.Core.Commands.Contracts
{
   public interface ICommand
    {
        string Execute(string[] input);
    }
}
