using MediatR;
using PruebaIngresoBibliotecario.Domain.Dtos;

namespace PruebaIngresoBibliotecario.Application.MediatR.Queries
{
    public class GetLoanQuery : GetLoanDto, IRequest<LoanDto> { }

}
