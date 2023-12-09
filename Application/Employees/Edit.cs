using AutoMapper;
using Domain;
using MediatR;
using Persistence;

namespace Application.Employees
{
    public class Edit
    {
        public class Command : IRequest
        {
            public EmployeeCommandDto Employee { get; set; } = default!;

            public Guid Id { get; set; }
        }
        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            
            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var employee = await _context.Employees.FindAsync(request.Id,
                    cancellationToken);

                _mapper.Map(request.Employee, employee);

                // update properties
                employee!.LastUpdateDate = DateTime.Now.ToString("dd-MM-yyyy hh:mm tt");

                await _context.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}
