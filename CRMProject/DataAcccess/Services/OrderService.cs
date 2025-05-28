using DataAcccess.DBContext;
using DataAcccess.DTO;
using DataAcccess.IRepositories;
using DataAcccess.IServices;
using DataAcccess.RequestData;
using DataAcccess.UnitOfWork;

namespace DataAcccess.Services
{
    public class OrderService : GenericService<Order, int>, IOrderService
    {
        private IOrderRepository _orderRepository;
        private IProductRepository _productRepository;
        private IUnitOfWork _unitOfWork;

        public OrderService(IOrderRepository orderRepository, IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _genericRepository = orderRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> IsProductInStock(CreateOrder createOrder)
        {
            bool flag = true;
            foreach (var item in createOrder.OrderDetails)
            {
                flag = _productRepository.CheckProductStock(item.ProductId, item.Quantity);
                if (flag == false)
                    break;
            }
            return flag;
        }

        public async Task<int> Insert(CreateOrder createOrder)
        {
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {
                    double totalAmount = 0;
                    var ordersDetails = new List<OrdersDetail>();
                    createOrder.OrderDetails.ForEach(x =>
                    {
                        var priceOfOrderDetail = _productRepository.GetPriceOfOrderDetail(x.ProductId, x.Quantity);
                        ordersDetails.Add(new OrdersDetail()
                        {
                            ProductId = x.ProductId,
                            Quantity = x.Quantity,
                            UnitPrice = priceOfOrderDetail
                        });
                        _unitOfWork._productRepository.UpdateStockQuantity(x.ProductId, x.Quantity);
                        totalAmount += priceOfOrderDetail;
                    });
                    var order = new Order()
                    {
                        CustomerId = createOrder.CustomerId,
                        OrderDate = createOrder.OrderDate,
                        TotalAmount = totalAmount,
                        OrdersDetails = ordersDetails
                    };
                    _unitOfWork._orderRepository.Insert(order);
                    var result = await _unitOfWork.SaveChangesAsync();
                    _unitOfWork.CommitTransaction();
                    return result;
                }
                catch (Exception ex)
                {
                    _unitOfWork.RollbackTransaction();
                    return -1;
                }
            }
        }
        public async Task<OrderDTO> GetDetail(int Id)
        {
            Order order = await _orderRepository.GetDetail(Id);
            if (order != null)
            {
                OrderDTO orderDTO = new OrderDTO();
                orderDTO.OrderId = order.OrderId;
                orderDTO.CustomerId = order.CustomerId.Value;
                orderDTO.OrderDate = order.OrderDate;
                orderDTO.TotalAmount = order.TotalAmount;
                orderDTO.orderDetails = new List<OrderDetailDTO>();
                foreach (var item in order.OrdersDetails)
                {
                    orderDTO.orderDetails.Add(new OrderDetailDTO()
                    {
                        OrderDetailId = item.OrderId.Value,
                        OrderId = item.OrderId.Value,
                        ProductId = item.ProductId.Value,
                        UnitPrice = item.UnitPrice.Value,
                        Quantity = item.Quantity.Value,
                    });
                }
                return orderDTO;
            }
            return null;
        }
        public async Task<int> DeleteOrderTEST(int id)
        {
            return await _orderRepository.DeleteOrderTEST(id);
        }
        public async Task<int> DeleteOrder(int id)
        {
            var order = _unitOfWork._orderRepository.GetById(id);
            if (order != null)
            {
                _unitOfWork._orderRepository.Remove(order);
                _unitOfWork._orderDetailRepository.DeleteByOrderId(id);
                return await _unitOfWork.SaveChangesAsync();
            }
            return await Task.FromResult(0);
        }
    }
}
