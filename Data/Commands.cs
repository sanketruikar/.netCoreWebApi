using System.Collections.Generic;
using Commander.Models;
using System;
using System.Linq;
namespace Commander.Data
{
   public class Commands:ICommanderRepo
   {    private readonly CommanderContext _context;


        public Commands(CommanderContext conte)
        {
           _context=conte;
        }

        public IEnumerable<Command> GetAllCommands()
        {
             return _context.commander.ToList();
        }
      public Command GetCommandById(int id)
      {
            return _context.commander.FirstOrDefault(p =>p.Id==id);

      }
      public void CreateCommand(Command cmd)
        {
            if(cmd == null)
            {
                throw new ArgumentNullException(nameof(cmd));
            }

            _context.commander.Add(cmd);
        }
        public void DeleteCommand(Command cmd)
        {
            if(cmd == null)
            {
                throw new ArgumentNullException(nameof(cmd));
            }
            _context.commander.Remove(cmd);
        }
        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateCommand(Command cmd)
        {
            //Nothing
        }
   }


}