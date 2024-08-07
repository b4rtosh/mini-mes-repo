using MediatR;
using MiniMesTrainApi.Application.ParameterMed.Queries;
using MiniMesTrainApi.Domain.Entities;
using MiniMesTrainApi.Infrastructure.Persistence.Repositories;

namespace MiniMesTrainApi.Application.ParameterMed.Handlers;

public class GetAllParamsHandler (DatabaseRepo<Parameter> repo) : IRequestHandler<GetAllParamsQuery, IEnumerable<Parameter>>
{
   public async Task<IEnumerable<Parameter>> Handle(GetAllParamsQuery request, CancellationToken token)
   {
      return await repo.GetAll();
   }
}