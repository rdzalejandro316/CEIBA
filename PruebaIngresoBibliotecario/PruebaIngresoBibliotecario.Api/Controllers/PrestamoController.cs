using MediatR;
using Microsoft.AspNetCore.Mvc;
using PruebaIngresoBibliotecario.Application.Exceptions;
using PruebaIngresoBibliotecario.Application.MediatR.Commands;
using PruebaIngresoBibliotecario.Application.MediatR.Queries;
using PruebaIngresoBibliotecario.Domain.Dtos;
using System;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace PruebaIngresoBibliotecario.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrestamoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PrestamoController(IMediator mediator) => _mediator = mediator;


        [HttpPost]        
        public async Task<ActionResult<CreateLoanResponseDto>> CreateLoan([FromBody] CreateLoanCommand createLoanCommand)
        {
            try
            {                
                if (!ModelState.IsValid)
                {
                    string messages = String.Join(Environment.NewLine, ModelState.Values.SelectMany(v => v.Errors).Select(v => v.ErrorMessage + " " + v.Exception));
                    ResponseService response = new ResponseService() { Mensaje = messages };
                    return BadRequest(response);
                }
                else
                {
                    var loan = await _mediator.Send(createLoanCommand);
                    return Ok(loan);
                }                
            }            
            catch (BadRequestException e)
            {
                ResponseService response = new ResponseService() { Mensaje = e.Message };
                return BadRequest(response);
            }
            catch (Exception)
            {                
                return BadRequest();
            }
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<LoanDto>> GetLoan(Guid id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    string messages = String.Join(Environment.NewLine, ModelState.Values.SelectMany(v => v.Errors).Select(v => v.ErrorMessage + " " + v.Exception));
                    ResponseService response = new ResponseService() { Mensaje = messages };
                    return BadRequest(response);
                }

                GetLoanQuery getLoanQuery = new GetLoanQuery { Id = id };
                var loan = await _mediator.Send(getLoanQuery);
                return Ok(loan);
            }
            catch (NotFoundException e)
            {
                ResponseService response = new ResponseService() { Mensaje = e.Message };
                return NotFound(response);
            }
        }

    }
}
