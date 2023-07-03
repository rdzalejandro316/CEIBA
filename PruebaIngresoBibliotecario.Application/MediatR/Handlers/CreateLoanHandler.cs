using AutoMapper;
using MediatR;
using PruebaIngresoBibliotecario.Application.Common.Enums;
using PruebaIngresoBibliotecario.Application.Exceptions;
using PruebaIngresoBibliotecario.Application.MediatR.Commands;
using PruebaIngresoBibliotecario.Domain.Dtos;
using PruebaIngresoBibliotecario.Domain.Entities;
using PruebaIngresoBibliotecario.Infrastructure.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PruebaIngresoBibliotecario.Application.MediatR.Handlers
{
    public class CreateLoanHandler : IRequestHandler<CreateLoanCommand, CreateLoanResponseDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateLoanHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CreateLoanResponseDto> Handle(CreateLoanCommand request, CancellationToken cancellationToken)
        {
            Loan loan = await _unitOfWork.LoanRepository.FirstOrDefaultAsync(x => x.IdentificacionUsuario == request.IdentificacionUsuario);
            if (loan != null && request.TipoUsuario == (int)TiposUsuarioEnum.Invitado)
            {
                throw new BadRequestException($"El usuario con identificacion {request.IdentificacionUsuario} ya tiene un libro prestado por lo cual no se le puede realizar otro prestamo");
            }

            loan = _mapper.Map<Loan>(request);
            loan.Id = Guid.NewGuid();
            loan.FechaMaximaDevolucion = CalcularFechaEntrega(request.TipoUsuario);
            await _unitOfWork.LoanRepository.InsertAsync(loan);
            await _unitOfWork.CommitTransactionAsync();

            return _mapper.Map<CreateLoanResponseDto>(loan);
        }

        private DateTime CalcularFechaEntrega(int tipoUsuario)
        {
            DayOfWeek[] weekend = new[] { DayOfWeek.Saturday, DayOfWeek.Sunday };
            DateTime fechaDevolucion = DateTime.Now;
            int diasPrestamo = tipoUsuario switch
            {
                (int)TiposUsuarioEnum.Afiliado => 10,
                (int)TiposUsuarioEnum.Empleado => 8,
                (int)TiposUsuarioEnum.Invitado => 7,
                _ => -1,
            };

            for (int i = 0; i < diasPrestamo;)
            {
                fechaDevolucion = fechaDevolucion.AddDays(1);
                i = (!weekend.Contains(fechaDevolucion.DayOfWeek)) ? ++i : i;
            }

            return fechaDevolucion;
        }
    }
}
