using endavaPractica.Net.Exceptions;
using endavaPractica.Net.Models;
using Microsoft.EntityFrameworkCore;

namespace endavaPractica.Net.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly Event2Context _dbContext;

        public OrderRepository()
        {
            _dbContext = new Event2Context();
        }


        public int AddOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public void Delete(Order order)
        {
            _dbContext.Remove(@order);
            _dbContext.SaveChanges();
        }

        public IEnumerable<Order> GetAllOrders()
        {
            var orders = _dbContext.Orders;
            return orders;
        }

        public async Task<Order> GetOrderById(int id)
        {
            var @order = await _dbContext.Orders.Where(e => e.OrderId == id).FirstOrDefaultAsync();

            if (@order == null)
                throw new EntityNotFoundException(id, nameof(Order));

            return @order;
        }

        public void Update(Order order)
        {
            _dbContext.Entry(@order).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }



    }
}
