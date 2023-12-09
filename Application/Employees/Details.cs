using AutoMapper;
using Domain;
using MediatR;
using Persistence;

namespace Application.Employees
{
    public class Details
    {
        public class Query : IRequest<Employee> 
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Employee>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Employee> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.Employees.FindAsync(request.Id, cancellationToken);
            }
        }
    }
}
