using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PruebaIngresoBibliotecario.Api.Controllers;
using PruebaIngresoBibliotecario.Api.UnitTest.Stubs.Prestamo;
using PruebaIngresoBibliotecario.Application.Exceptions;
using PruebaIngresoBibliotecario.Application.MediatR.Queries;
using PruebaIngresoBibliotecario.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PruebaIngresoBibliotecario.Api.UnitTest.Controllers
{
    [TestClass]
    public class PrestamoControllerTest
    {
        private Mock<IMediator> _mockMediator;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockMediator = new Mock<IMediator>();
        }

        private PrestamoController Controller()
        {

            PrestamoController controllerContext = new PrestamoController(_mockMediator.Object)
            {
                ControllerContext = new ControllerContext()
            };

            return controllerContext;
        }


        [TestMethod]
        public async Task GetByIdExpectedSetupOk()
        {
            // Act
            _mockMediator
                .Setup(i => i.Send(It.IsAny<GetLoanQuery>(), It.IsAny<System.Threading.CancellationToken>()))
                .ReturnsAsync(PrestamoControllerStub.ResponseLoanDto);

            var controller = Controller();
            var result = await controller.GetLoan(PrestamoControllerStub.ResponseLoanDto.Id);

            // Assert
            var statusResult = result.Result as ObjectResult;
            Assert.IsNotNull(result);
            Assert.IsTrue(statusResult is OkObjectResult);
            Assert.AreEqual(StatusCodes.Status200OK, statusResult.StatusCode);
            var model = statusResult.Value as LoanDto;
            Assert.IsNotNull(model);
        }

        [TestMethod]
        public async Task GetByIdExpectedSetuNotFound()
        {

            GetLoanQuery getLoanQuery = new GetLoanQuery() { Id = Guid.NewGuid() };
            _mockMediator
                .Setup(i => i.Send(getLoanQuery, It.IsAny<System.Threading.CancellationToken>()))
                .Throws(new NotFoundException($"El prestamo con id {getLoanQuery.Id} no existe"));
                //.ThrowsAsync(new NotFoundException($"El prestamo con id {getLoanQuery.Id} no existe"));

            var controller = Controller();
            var result = await controller.GetLoan(getLoanQuery.Id);


            var statusResult = result.Result as ObjectResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(StatusCodes.Status404NotFound, statusResult.StatusCode);
        }


        [TestMethod]
        public async Task GetByIdExpectedSetuBadRequest()
        {
            // Arrange
            Guid newGuid = Guid.NewGuid();

            // Act
            var controller = Controller();            
            controller.ModelState.AddModelError("id", "El 'Id' es obligatorio.");
            var result = await controller.GetLoan(newGuid);
            var statusResult = result.Result as ObjectResult;
            var model = statusResult.Value as ResponseService;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(StatusCodes.Status400BadRequest, statusResult.StatusCode);
            Assert.AreEqual(model.Mensaje, "El 'Id' es obligatorio. ");
        }

    }
}
