using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nepal.Abstraction.Model;
using Nepal.Abstraction.Service.Business;

namespace Nepal.Backend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CreditController : BaseApiController
    {
        private ICreditService _creditService;
        private readonly ILogger<CreditController> _logger;
        public CreditController(ICreditService creditService, ILogger<CreditController> logger)
        {
            this._creditService = creditService;
            this._logger = logger;
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var userId = HttpContext.User.Identity.Name;
                var credits = await _creditService.GetCredits();
                return Ok(credits);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return CreateApiException(ex);
            }
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        [Route("BankDeposits")]
        public async Task<IActionResult> BankDeposits()
        {
            try
            {
                var userId = HttpContext.User.Identity.Name;
                var credits = await _creditService.GetBankDeposits();
                return Ok(credits);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return CreateApiException(ex);
            }
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        [Route("iPMANCredits")]
        public async Task<IActionResult> iPMANCredits()
        {
            try
            {
                var userId = HttpContext.User.Identity.Name;
                var credits = await _creditService.GetIPMANCredits();
                return Ok(credits);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return CreateApiException(ex);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<string>), 400)]
        public async Task<IActionResult> Post([FromBody] CreditModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.Select(x => x.Errors.FirstOrDefault().ErrorMessage));
            try
            {
                var creditId = await _creditService.Create(model);
                return Ok(creditId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return CreateApiException(ex);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(IEnumerable<string>), 400)]
        public async Task<IActionResult> Put([FromBody] CreditModel model, int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.Select(x => x.Errors.FirstOrDefault().ErrorMessage));
            try
            {
                await _creditService.UpdateCredit(model, id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return CreateApiException(ex);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var credit = await _creditService.GetCredit(id);
                return Ok(credit);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return CreateApiException(ex);
            }
        }
        [HttpGet]
        [Route("approve/{id}")]
        public async Task<IActionResult> Approve(int id)
        {
            try
            {
                await _creditService.ApproveCredit(id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return CreateApiException(ex);
            }
        }

        [HttpGet]
        [Route("reject/{id}")]
        public async Task<IActionResult> Reject(int id)
        {
            try
            {
                await _creditService.RejectCredit(id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return CreateApiException(ex);
            }
        }
    }
}