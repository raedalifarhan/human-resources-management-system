using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using Persistence;

namespace Application.Departments
{
    public class List
    {
        public class Query : IRequest<Result<PagedList<DepartmentQueryDto>>>
        {
            public DepartmentParams? Params { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<PagedList<DepartmentQueryDto>>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<PagedList<DepartmentQueryDto>>> Handle(Query request,
                CancellationToken cancellationToken)
            {
                var query = _context.Departments
                    .ProjectTo<DepartmentQueryDto>(_mapper.ConfigurationProvider)
                    .AsQueryable();

                if (!string.IsNullOrEmpty(request.Params!.BranchId))
                {
                    query = query.Where(x => x.BranchId.ToString() == request.Params!.BranchId);
                }

                return Result<PagedList<DepartmentQueryDto>>
                    .Success(await PagedList<DepartmentQueryDto>.CreateAsync(query,
                        request.Params!.PageNumber, request.Params.PageSize));
            }
        }
    }
}
