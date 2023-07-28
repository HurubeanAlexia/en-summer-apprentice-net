using endavaPractica.Net.Models;

namespace endavaPractica.Net.Repository
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetAllOrders();
        Task<Order> GetOrderById(int id);
        int AddOrder(Order @order);

        void Update(Order @order);

        void Delete(Order @order);


    }
}
