using Application.Core;
using Application.Departments;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using MediatR;
using Persistence;

namespace Application.Employees
{
    public class List
    {
        public class Query : IRequest<Result<PagedList<EmployeeQueryDto>>> 
        {
            public PagingParams? Params { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<PagedList<EmployeeQueryDto>>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<PagedList<EmployeeQueryDto>>> Handle(Query request,
                CancellationToken cancellationToken)
            {
                var query = _context.Employees
                    .ProjectTo<EmployeeQueryDto>(_mapper.ConfigurationProvider)
                    .AsQueryable();

                return Result<PagedList<EmployeeQueryDto>>
                    .Success(await PagedList<EmployeeQueryDto>.CreateAsync(query,
                        request.Params!.PageNumber, request.Params.PageSize));
            }
        }
    }
}
