using Microsoft.EntityFrameworkCore;
using MySql.Data.EntityFrameworkCore.Extensions;
using Commander.Models;

namespace Commander.Data
{
    public class CommanderContext:DbContext
    {
        public CommanderContext(DbContextOptions<CommanderContext> opt):base(opt)
        {

        }
        public DbSet<Command> commander{get;set;}
    }

      

    
   


}