using AutoMapper;
using MediatR;
using PruebaIngresoBibliotecario.Application.Exceptions;
using PruebaIngresoBibliotecario.Infrastructure.Interfaces;
using System.Threading.Tasks;
using System.Threading;
using PruebaIngresoBibliotecario.Application.MediatR.Queries;
using PruebaIngresoBibliotecario.Domain.Dtos;
using PruebaIngresoBibliotecario.Domain.Entities;

namespace PruebaIngresoBibliotecario.Application.MediatR.Handlers
{
    public class GetLoanQueryHandler : IRequestHandler<GetLoanQuery, LoanDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetLoanQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<LoanDto> Handle(GetLoanQuery request, CancellationToken cancellationToken)
        {
            Loan loan = await _unitOfWork.LoanRepository.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (loan == null) throw new NotFoundException($"El prestamo con id {request.Id} no existe");
            return _mapper.Map<LoanDto>(loan);
        }

    }
}
