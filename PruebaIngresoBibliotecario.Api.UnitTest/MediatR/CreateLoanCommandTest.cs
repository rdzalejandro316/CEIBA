using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Namotion.Reflection;
using PruebaIngresoBibliotecario.Api.UnitTest.Stubs;
using PruebaIngresoBibliotecario.Api.UnitTest.Stubs.Prestamo;
using PruebaIngresoBibliotecario.Application.Exceptions;
using PruebaIngresoBibliotecario.Application.MediatR.Commands;
using PruebaIngresoBibliotecario.Application.MediatR.Handlers;
using PruebaIngresoBibliotecario.Domain.Dtos;
using PruebaIngresoBibliotecario.Domain.Entities;
using PruebaIngresoBibliotecario.Infrastructure.DataAccess;
using PruebaIngresoBibliotecario.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PruebaIngresoBibliotecario.Api.UnitTest.MediatR
{
    [TestClass]
    public class CreateLoanCommandTest
    {
        private Mock<IUnitOfWork> _unitOfWork;
        private Mock<IMapper> _mapperMock;
        private static IMapper _mapper;

        [TestInitialize]
        public void TestInitialize()
        {
            //AutoMapper.Mapper.Reset();
            //AutoMapperConfig.CreateMaps();
            
            /////
            //AutoMapper.Mapper.Initialize(cfg =>
            //{
            //    cfg.CreateMissingTypeMaps = true;
            //    cfg.ValidateInlineMaps = false;
            //});

            _unitOfWork = new Mock<IUnitOfWork>();
            //_mapper = new Mock<IMapper>();
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new AutoMapperProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;                
            }


        }


        [TestMethod]
        public async Task CreateLoanOk()
        {
            //Arrange
            var mediator = new Mock<IMediator>();

            //CreateLoanCommand command = new CreateLoanCommand();
            CreateLoanHandler handler = new CreateLoanHandler(_unitOfWork.Object, _mapper);


            _unitOfWork
                .Setup(i => i.LoanRepository.FirstOrDefaultAsync(It.IsAny<Expression<Func<Loan, bool>>>()))
                .ReturnsAsync(PrestamoControllerStub.Loan);



            //Act
            var x = await handler.Handle(PrestamoControllerStub.CreateLoanTipoUsuarioMalCommand, new System.Threading.CancellationToken());

            //Assert
            //Do the assertion

            Assert.IsNotNull(x);
            

        }


        [TestMethod]
        public async Task CreateLoanFailTypeUser()
        {
            try
            {
                //Arrange
                var mediator = new Mock<IMediator>();
                CreateLoanHandler handler = new CreateLoanHandler(_unitOfWork.Object, _mapper);


                _unitOfWork
                    .Setup(i => i.LoanRepository.FirstOrDefaultAsync(It.IsAny<Expression<Func<Loan, bool>>>()))
                    .ReturnsAsync(PrestamoControllerStub.Loan);


                //Act
                var x = await handler.Handle(PrestamoControllerStub.CreateLoanTipoUsuarioInvitadoCommand, new System.Threading.CancellationToken());

            }
            catch (BadRequestException w)
            {
                Assert.AreEqual(w.Message, $"El usuario con identificacion {PrestamoControllerStub.CreateLoanTipoUsuarioInvitadoCommand.IdentificacionUsuario} ya tiene un libro prestado por lo cual no se le puede realizar otro prestamo");
            }
        }

    }
}
