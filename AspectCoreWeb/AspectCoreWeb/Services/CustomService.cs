using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspectCoreWeb.Services
{
    public class CustomService : ICustomService
    {
        public void Call()
        {
            Console.WriteLine("......");
        }
    }
}
