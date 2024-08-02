using MediatR;
using Microsoft.AspNetCore.Mvc;
using MiniMesTrainApi.Application.ParameterMed.Commands;
using MiniMesTrainApi.Application.ProcessMed.Commands;
using MiniMesTrainApi.Domain.Entities;
using MiniMesTrainApi.Infrastructure.Persistence.Repositories;

namespace MiniMesTrainApi.Application.ProcessMed.Handlers;

public class DeleteProcessHandler (DatabaseRepo<Process> _repo) : IRequestHandler<DeleteProcessCommand, IActionResult>
{
    public async Task<IActionResult> Handle(DeleteProcessCommand request, CancellationToken token)
    {
        try
        {
            await _repo.DelById(request.Id);
            return new OkObjectResult("Instance deleted");
        }
        catch (Exception e)
        {
            return new BadRequestObjectResult(e.Message);
        }
    }
}