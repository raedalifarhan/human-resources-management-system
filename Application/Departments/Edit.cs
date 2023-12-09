using Application.Core;
using AutoMapper;
using Domain;
using MediatR;
using Persistence;

namespace Application.Departments
{
    public class Edit
    {
        public class Command : IRequest<Result<Unit>>
        {
            public DepartmentCommandDto Department { get; set; } = default!;

            public Guid Id { get; set; }
        }
        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var department = await _context.Departments.FindAsync(request.Id);

                if (department == null) return null!;

                _mapper.Map(request.Department, department);

                // update properties
                department!.LastUpdateDate = DateTime.Now.ToString("dd-MM-yyyy hh:mm tt");

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to update the department.");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
