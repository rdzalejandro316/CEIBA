using AutoMapper;
using PruebaIngresoBibliotecario.Domain.Dtos;
using PruebaIngresoBibliotecario.Domain.Entities;

namespace PruebaIngresoBibliotecario.Application.Common.Mapper
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
