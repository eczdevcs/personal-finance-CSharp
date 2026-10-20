using Microsoft.AspNetCore.Mvc;
using PersonalFinanceMng.Application.DTOs.Request;
using PersonalFinanceMng.Application.UseCases.PaymentMethods.CreatePaymentMethod;
using PersonalFinanceMng.Application.UseCases.PaymentMethods.FindAllByUserIdUseCse;
using PersonalFinanceMng.Application.UseCases.PaymentMethods.FindPaymentMethodByIdUseCase;
using PersonalFinanceMng.Application.UseCases.PaymentMethods.HardDeletePaymentMethodUseCase;
using PersonalFinanceMng.Application.UseCases.PaymentMethods.SoftDeletePaymentMehtodUseCase;
using PersonalFinanceMng.Application.UseCases.PaymentMethods.UpdatePaymentMethodUseCase.cs;

namespace PersonalFinanceMng.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PaymentMethodController : ControllerBase
    {
        private readonly ICreatePaymentMethodUseCase _createPaymentMethodUseCase;
        private readonly IFindPaymentMethodByIdUseCase _findPaymentMethodByIdUseCase;
        private readonly IFindAllByUserIdUseCase _findAllByUserIdUseCase;
        private readonly IUpdatePaymentMethodUseCase _updatePaymentMethodUseCase;
        private readonly ISoftDeletePaymentMethodUseCase _softDeletePaymentMethodUseCase;

        private readonly IHardDeletePaymentMethod _hardDeletePaymentMethod;

        public PaymentMethodController
            (
                ICreatePaymentMethodUseCase createPaymentMethodUseCase,
                IFindPaymentMethodByIdUseCase findPaymentMethodByIdUseCase,
                IFindAllByUserIdUseCase findAllByUserIdUseCase,
                IUpdatePaymentMethodUseCase updatePaymentMethodUseCase,
                ISoftDeletePaymentMethodUseCase softDeletePaymentMethodUseCase,
                IHardDeletePaymentMethod hardDeletePaymentMethod
            )
        {
            _createPaymentMethodUseCase = createPaymentMethodUseCase;
            _findPaymentMethodByIdUseCase = findPaymentMethodByIdUseCase;
            _findAllByUserIdUseCase = findAllByUserIdUseCase;
            _updatePaymentMethodUseCase = updatePaymentMethodUseCase;
            _softDeletePaymentMethodUseCase = softDeletePaymentMethodUseCase;
            _hardDeletePaymentMethod = hardDeletePaymentMethod;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePaymentMethodRequestDTO requestDTO)
        {
            try
            {
                var result = await _createPaymentMethodUseCase.ExecuteAsync(requestDTO);
                return StatusCode(StatusCodes.Status201Created, result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
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

        [HttpGet]
        public async Task<IActionResult> FindAllByUserId([FromQuery] Guid userId)
        {
            try
            {
                var result = await _findAllByUserIdUseCase.ExecuteAsync(userId);
                return StatusCode(StatusCodes.Status200OK, result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        [HttpPut("{paymentMehodId:int}")]
        public async Task<IActionResult> UpdatePaymentMethod(
            [FromQuery] Guid userId,
            [FromBody] PaymentMethodRequestDTO paymentMethod
        )
        {
            try
            {
                var result = await _updatePaymentMethodUseCase.ExecuteAsync(userId, paymentMethod);

                return StatusCode(StatusCodes.Status200OK, result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        [HttpDelete("{paymentMethodId:int}")]
        public async Task < IActionResult > SoftDeletePaymentMethod([FromQuery] Guid userId, [FromRoute] int paymentMethodId)
        {
            try
            {
                await _softDeletePaymentMethodUseCase.ExecuteAsync(userId, paymentMethodId);
                return StatusCode(StatusCodes.Status204NoContent);
            }

            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
    }

}
