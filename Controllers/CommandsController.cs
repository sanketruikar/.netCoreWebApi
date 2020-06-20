using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Commander.Data;
using Commander.Models;
using AutoMapper;
using Commander.Dtos;
using Microsoft.AspNetCore.JsonPatch;


namespace Commander.Controllers
{
   
  
  
  [ApiController][Route("/command")]public class CommandsController:ControllerBase
  {   private readonly ICommanderRepo _repo;
   private readonly IMapper _mapper;

    public CommandsController(ICommanderRepo repository,IMapper  mapper)
  {
    _repo=repository;
    _mapper=mapper;
  }
      [HttpGet]public ActionResult <IEnumerable<CommandReadDto>> GetAllCommands()
      {

    var com=_repo.GetAllCommands();
    return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(com));
      }
      [HttpGet("{id}",Name="GetCommandById")]public ActionResult <CommandReadDto> GetCommandById(int id)
      {
            var comd=_repo.GetCommandById(id);
            if(comd!=null)
            {
            return Ok(_mapper.Map<CommandReadDto>(comd));
            }
            return NotFound();

      }
      [HttpPost]public ActionResult <CommandCreateDto> CreateCommand(CommandCreateDto cm)
      {
                var cmdmodel=_mapper.Map<Command>(cm);
                _repo.CreateCommand(cmdmodel);
                _repo.SaveChanges();

                var commandReadDto=_mapper.Map<CommandReadDto>(cmdmodel);
                return CreatedAtRoute(nameof(GetCommandById),new{Id = commandReadDto.Id},
                commandReadDto);
      }

      //put 
      [HttpPut("{id}")]
        public ActionResult UpdateCommand(int Id, CommandUpdateDto commandUpdateDto)
        {
            var commandModelFromRepo = _repo.GetCommandById(Id);
            if(commandModelFromRepo == null)
            {
                return NotFound();
            }
            _mapper.Map(commandUpdateDto, commandModelFromRepo);

            _repo.UpdateCommand(commandModelFromRepo);

            _repo.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteCommand(int Id)
        {
            var commandModelFromRepo = _repo.GetCommandById(Id);
            if(commandModelFromRepo == null)
            {
                return NotFound();
            }
            

            _repo.DeleteCommand(commandModelFromRepo);

            _repo.SaveChanges();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public ActionResult PartialUpdate(int Id,JsonPatchDocument<CommandUpdateDto> patchDoc)
        {
                 var commandModelFromRepo = _repo.GetCommandById(Id);
            if(commandModelFromRepo == null)
            {
                return NotFound();
            }

            var commandToPatch = _mapper.Map<CommandUpdateDto>(commandModelFromRepo);
            patchDoc.ApplyTo(commandToPatch, ModelState);

            if(!TryValidateModel(commandToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(commandToPatch, commandModelFromRepo);

            _repo.UpdateCommand(commandModelFromRepo);

            _repo.SaveChanges();

            return NoContent();
          

        }

      


  }


}