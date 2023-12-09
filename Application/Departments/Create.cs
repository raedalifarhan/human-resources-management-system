using Application.Core;
using AutoMapper;
using Domain;
using MediatR;
using Persistence;

namespace Application.Departments
{
    public class Create
    {
        public class Command : IRequest<Result<Unit>>
        {
            public DepartmentCommandDto Department { get; set; } = default!;
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
                var dept = _mapper.Map<Department>(request.Department);

                dept.CreateDate = DateTime.Now.ToString("dd-MM-yyyy hh:mm tt");

                _context.Departments.Add(dept);

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to create department");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}