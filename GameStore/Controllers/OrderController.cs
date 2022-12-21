using GameStore.Dtos;
using GameStore.Models;
using GameStore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameStore.Controllers
{
    //[Authorize]
    public class OrderController : Controller
    {
        private readonly OrderService _orderService;

        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet, Route("GetAllOrders/{userId}")]
        public async Task<IActionResult> GetAllOrders(int userId)
        {
            return new OkObjectResult(await _orderService.GetAllOrders(userId));
        }

        [HttpPost, Route("EditCart")]
        public async Task<IActionResult> EditCart(OrderDto dto)
        {
            return new OkObjectResult(await _orderService.EditCart(dto));
        }

        [HttpPost("Purchase/{userId}")]
        public async Task<IActionResult> Purchase(int userId, bool purchaseStatus)
        {
            return new OkObjectResult(await _orderService.Purchase(userId, purchaseStatus));
        }
    }
}
