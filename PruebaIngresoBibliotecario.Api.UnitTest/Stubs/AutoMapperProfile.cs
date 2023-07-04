using AutoMapper;
using PruebaIngresoBibliotecario.Domain.Dtos;
using PruebaIngresoBibliotecario.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PruebaIngresoBibliotecario.Api.UnitTest.Stubs
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreateLoanDto, Loan>();
            CreateMap<Loan, CreateLoanResponseDto>();
            CreateMap<Loan, LoanDto>();
        }
    }
}
