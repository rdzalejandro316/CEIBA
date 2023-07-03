using PruebaIngresoBibliotecario.Application.Exceptions;
using PruebaIngresoBibliotecario.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PruebaIngresoBibliotecario.Api.UnitTest.Stubs.Prestamo
{
    public static class PrestamoControllerStub
    {
        public static GetLoanDto getLoanDto() => new GetLoanDto
        {
            Id = Guid.NewGuid()            
        };

        public static LoanDto ResponseLoanDto => new LoanDto
        {
            Id = Guid.NewGuid(),
            Isbn = Guid.NewGuid(),
            IdentificacionUsuario = "",
            TipoUsuario = 1,
            FechaMaximaDevolucion = new DateTime()
        };


    }
}
