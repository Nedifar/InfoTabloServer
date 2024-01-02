using InfoTablo.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoTablo.Persistence
{
    public class DbInitializer
    {
        public static void Initialize(InfoTabloDbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
