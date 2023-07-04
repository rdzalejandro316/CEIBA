using AutoMapper;
using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PruebaIngresoBibliotecario.Api.UnitTest.Stubs.Prestamo;
using PruebaIngresoBibliotecario.Application.Common.Mapper;
using PruebaIngresoBibliotecario.Application.Exceptions;
using PruebaIngresoBibliotecario.Application.MediatR.Handlers;
using PruebaIngresoBibliotecario.Domain.Dtos;
using PruebaIngresoBibliotecario.Domain.Entities;
using PruebaIngresoBibliotecario.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PruebaIngresoBibliotecario.Api.UnitTest.MediatR
{
    [TestClass]
    public class GetLoanQueryHandlerTest
    {
        private Mock<IUnitOfWork> _unitOfWork;
        private Mock<IMapper> _mapperMock;
        private static IMapper _mapper;

        [TestInitialize]
        public void TestInitialize()
        {
            //AutoMapperProfile.

            //Mapper.(cfg =>
            //{
            //    cfg.AddProfile<AutoMapperProfile>();
            //});

            _unitOfWork = new Mock<IUnitOfWork>();
            //_mapper = new Mock<IMapper>();
            //_mapper.Setup(c => c.ConfigurationProvider.)

            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new AutoMapperProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
                
                //_mapperMock = new Mock<IMapper>();
                //_mapperMock.Setup(x => x.ConfigurationProvider. )

            }
        }



        [TestMethod]
        public async Task GetLoanOk()
        {

            //Arrange
            var mediator = new Mock<IMediator>();
            GetLoanQueryHandler handler = new GetLoanQueryHandler(_unitOfWork.Object, _mapper);

            _unitOfWork
                .Setup(i => i.LoanRepository.FirstOrDefaultAsync(It.IsAny<Expression<Func<Loan, bool>>>()))
                .ReturnsAsync(PrestamoControllerStub.Loan);

            //Act
            var x = await handler.Handle(PrestamoControllerStub.GetLoanQuery, new System.Threading.CancellationToken());
            Assert.IsNotNull(x);
        }


        [TestMethod]
        public async Task GetLoanFail()
        {
            try
            {
                //Arrange
                var mediator = new Mock<IMediator>();
                GetLoanQueryHandler handler = new GetLoanQueryHandler(_unitOfWork.Object, _mapper);


                _unitOfWork
                    .Setup(i => i.LoanRepository.FirstOrDefaultAsync(It.IsAny<Expression<Func<Loan, bool>>>()))
                    .ReturnsAsync(PrestamoControllerStub.Loan);


                //Act
                var x = await handler.Handle(PrestamoControllerStub.GetLoanQuery, new System.Threading.CancellationToken());

            }
            catch (NotFoundException w)
            {
                Assert.AreEqual(w.Message, $"El prestamo con id {PrestamoControllerStub.GetLoanQuery.Id} no existe");
            }
        }


    }
}
