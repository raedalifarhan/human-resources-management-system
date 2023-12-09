using Domain;
using MediatR;
using Persistence;

namespace Application.Employees
{
    public class Create
    {
        public class Command : IRequest
        {
            public Employee Employee { get; set; } = default!;
        }
        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                _context.Employees.Add(request.Employee);

                await _context.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}
