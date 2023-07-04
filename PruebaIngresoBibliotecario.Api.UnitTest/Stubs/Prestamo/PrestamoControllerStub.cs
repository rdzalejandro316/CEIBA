using PruebaIngresoBibliotecario.Application.Exceptions;
using PruebaIngresoBibliotecario.Application.MediatR.Commands;
using PruebaIngresoBibliotecario.Application.MediatR.Queries;
using PruebaIngresoBibliotecario.Domain.Dtos;
using PruebaIngresoBibliotecario.Domain.Entities;
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
            IdentificacionUsuario = "alejo",
            TipoUsuario = 1,
            FechaMaximaDevolucion = new DateTime()
        };


        public static Loan Loan => new Loan
        {            
            Isbn = Guid.NewGuid(),
            IdentificacionUsuario = "alejo",
            TipoUsuario = 1,
            FechaMaximaDevolucion = new DateTime()
        };


        public static CreateLoanResponseDto CreateLoanResponseDto => new CreateLoanResponseDto
        {
            Id = Guid.NewGuid(),
            FechaMaximaDevolucion = new DateTime()
        };

        public static CreateLoanCommand CreateLoanCommand => new CreateLoanCommand
        {
            Isbn = Guid.NewGuid(),
            IdentificacionUsuario = "alejo",
            TipoUsuario = 1
        };

        public static CreateLoanCommand CreateLoanTipoUsuarioMalCommand => new CreateLoanCommand
        {
            Isbn = Guid.NewGuid(),
            IdentificacionUsuario = "alejo",
            TipoUsuario = 4
        };

        public static CreateLoanCommand CreateLoanTipoUsuarioInvitadoCommand => new CreateLoanCommand
        {
            Isbn = Guid.NewGuid(),
            IdentificacionUsuario = "alejo",
            TipoUsuario = 3
        };


        public static GetLoanQuery GetLoanQuery => new GetLoanQuery
        {
            Id = Guid.NewGuid()            
        };
        

    }
}
