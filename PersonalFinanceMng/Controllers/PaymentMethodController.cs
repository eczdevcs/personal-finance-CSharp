using Microsoft.AspNetCore.Mvc;
using PersonalFinanceMng.Application.DTOs.Request;
using PersonalFinanceMng.Application.UseCases.PaymentMethods.CreatePaymentMethod;
using PersonalFinanceMng.Application.UseCases.PaymentMethods.FindPaymentMethodByIdUseCase;

namespace PersonalFinanceMng.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]

    public class PaymentMethodController : ControllerBase
    {
        private readonly ICreatePaymentMethodUseCase _createPaymentMethodUseCase;
        private readonly IFindPaymentMethodByIdUseCase _findPaymentMethodByIdUseCase;

        public PaymentMethodController
            (
                ICreatePaymentMethodUseCase createPaymentMethodUseCase,
                IFindPaymentMethodByIdUseCase findPaymentMethodByIdUseCase
            )
        {
            _createPaymentMethodUseCase = createPaymentMethodUseCase;
            _findPaymentMethodByIdUseCase = findPaymentMethodByIdUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePaymentMethodRequestDTO requestDTO)
        {
            try
            {
                var result = await _createPaymentMethodUseCase.ExecuteAsync(requestDTO);
                return StatusCode(StatusCodes.Status201Created, result);
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(new {Message = ex.Message});
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new {message = ex.Message });
            }
        }

        [HttpGet("{paymentMethodId:int}")]
        public async Task<IActionResult> FindById([FromQuery] Guid userId, [FromRoute] int paymentMethodId)
        {
            try
            {
                var result = await _findPaymentMethodByIdUseCase.ExecuteAsync(userId, paymentMethodId);
                return StatusCode(StatusCodes.Status200OK, result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }
    }
}
