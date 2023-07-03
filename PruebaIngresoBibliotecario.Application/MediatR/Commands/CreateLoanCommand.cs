using MediatR;
using PruebaIngresoBibliotecario.Domain.Dtos;

namespace PruebaIngresoBibliotecario.Application.MediatR.Commands
{
    public class CreateLoanCommand : CreateLoanDto, IRequest<CreateLoanResponseDto> { }
}
