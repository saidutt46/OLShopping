using System;
using System.Threading.Tasks;
using DCommerce.Dto.Requests.AddToCart;
using DCommerce.Service.Interfaces;
using DCommerce.WebApi.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace DCommerce.WebApi.Controllers
{
    [Route("/api/[controller]")]
    public class ShoppingCartController : Controller
    {
        private readonly ICartItemService _cartItemService;

        public ShoppingCartController(ICartItemService cartItemService)
        {
            _cartItemService = cartItemService;
        }

        [HttpGet]
        [Route("user/{id}")]
        public async Task<IActionResult> GetByUserId(string id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            try
            {
                var response = await _cartItemService.ListAllByUserId(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetByCartId(Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            try
            {
                var response = await _cartItemService.GetById(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddToCartRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            try
            {
                var response = await _cartItemService.Add(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCartRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            try
            {
                var response = await _cartItemService.Update(id, request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            try
            {
                var response = await _cartItemService.Delete(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
