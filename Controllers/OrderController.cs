using AutoMapper;
using endavaPractica.Net.Models.Dto;
using endavaPractica.Net.Repository;
using Microsoft.AspNetCore.Mvc;


namespace endavaPractica.Net.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]


    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public OrderController(IOrderRepository orderRepository, IMapper mapper, ILogger<OrderController> logger)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _logger = logger;

        }

        [HttpGet]
        public ActionResult<List<OrderDto>> GetAllOrders()
        {
            var orders = _orderRepository.GetAllOrders();

            var dtoOrders = orders.Select(o => new OrderDto()
            {
                OrderId = o.OrderId,
                OrderedAt = o.OrderedAt,
                TicketCategory = o.TicketCategory?.TicketCategoryDescription ?? string.Empty,
                NumberOfTickets = o.NumberOfTickets ?? 0,
                TotalPrice = o.TotalPrice ?? 0.0f

            });

            return Ok(dtoOrders);

        }


        [HttpGet]
        public async Task<ActionResult<OrderDto>> GetOrderById(int id)
        {

            var @order = await _orderRepository.GetOrderById(id);

            var eventDto = _mapper.Map<OrderDto>(@order);

            return Ok(eventDto);

        }

        [HttpPatch]
        public async Task<ActionResult<OrderPatchDto>> Patch(OrderPatchDto orderPatch)
        {
            if (orderPatch == null) throw new ArgumentNullException(nameof(orderPatch));
            var orderEntity = await _orderRepository.GetOrderById(orderPatch.OrderId);
            if (orderEntity == null)
            {
                return NotFound();
            }

            if (orderPatch.NumberOfTickets != null || orderPatch.NumberOfTickets != 0)
            {
                orderEntity.NumberOfTickets = orderPatch.NumberOfTickets;
            }


            if (orderPatch.TicketCategoryId != null || orderPatch.TicketCategoryId != 0)
            {
                orderEntity.TicketCategory.TicketCategoryId = orderEntity.TicketCategory.TicketCategoryId;
            }

            _orderRepository.Update(orderEntity);
            return NoContent();


        }
    

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            var orderEntity = await _orderRepository.GetOrderById(id);
            if (orderEntity == null)
            {
                return NotFound();
            }
            _orderRepository.Delete(orderEntity);
            return NoContent();
        }



        }

    } 

