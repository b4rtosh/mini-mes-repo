using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniMesTrainApi.Models;
using MiniMesTrainApi.Repositories;

namespace MiniMesTrainApi.Controllers;

public class StatusController : GenericController<ProcessStatus>
{
    private readonly DatabaseRepo<ProcessStatus> _repo;

    public StatusController(DatabaseRepo<ProcessStatus> repo) : base(repo)
    {
        _repo = repo;
    }

    public override async Task<IActionResult> GetOne([FromRoute] int id)
    {
        var status = await _repo.GetByIdWithIncludes(x => x.Id == id,
            query => query
                .Include(m => m.Processes));
        return Ok(status);
    }

    public override async Task<IActionResult> UpdateOne([FromBody] ProcessStatus updated)
    {
        var saved = await _repo.GetById(updated.Id);
        if (saved == null) return BadRequest("Object not found");
        try
        {
            if (updated.Name != "")
            {
                if (Validation.CheckString(updated.Name))
                    saved.Name = updated.Name;
                else throw new Exception("Provided name was invalid");
            }
            await _repo.Update(saved);
            return Ok($"Updated object:\n{saved}");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}