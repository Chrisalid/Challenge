using Challenge.Application.DTOs;
using Challenge.Application.DTOs.Request;
using Challenge.Application.DTOs.Response;
using Challenge.Application.Interfaces.Services;
using Challenge.Application.Mapping;
using Challenge.Domain.Entities;
using Challenge.Domain.Enums;
using Challenge.Domain.Interfaces.Repositories;

namespace Challenge.Application.Services;

public class OrderService(IUnitOfWorkRepository unitOfWorkRepository) : IOrderService
{
    private readonly IUnitOfWorkRepository _unitOfWorkRepository = unitOfWorkRepository;
    private readonly OrderMapper _mapper = new();

    public async Task<CreateOrderResponse> CreateOrder(CreateOrderRequest createOrderRequest, long loggedUserId)
    {
        var userRepository = _unitOfWorkRepository.Users;
        var clientRepository = _unitOfWorkRepository.Clients;
        var productRepository = _unitOfWorkRepository.Products;
        var orderRepository = _unitOfWorkRepository.Orders;
        var orderItemRepository = _unitOfWorkRepository.OrderItems;

        var loggedUser = await userRepository.GetById(loggedUserId) ?? throw new Exception();

        if (!loggedUser.IsActive)
            throw new Exception();

        var client = await clientRepository.GetById(createOrderRequest.ClientId);

        if (loggedUser.Id != client.UserId)
            throw new Exception();

        List<OrderItemData> orderItemDatas = [];
        decimal totalAmount = 0.00m;
        foreach (var orderItem in createOrderRequest.OrderItems)
        {
            var product = await productRepository.GetById(orderItem.ProductId);
            totalAmount += product.Value * orderItem.Quantity;
            orderItemDatas.Add(new OrderItemData() { Product = product, Quantity = orderItem.Quantity });
        }

        if (orderItemDatas.Count == 0)
            throw new Exception();

        await _unitOfWorkRepository.Begin();
        try
        {
            var orderModel = new Order.OrderModel(
                client.Id,
                createOrderRequest.Name,
                OrderStatus.Created,
                totalAmount,
                loggedUserId
            );

            var order = Order.Create(orderModel);

            order.Id = await orderRepository.CreateAsync(order);

            foreach (var item in orderItemDatas)
            {
                var orderItemModel = new OrderItem.OrderItemModel(
                    order.Id,
                    item.Product.Id,
                    item.Quantity,
                    item.Product.Value,
                    loggedUserId
                );

                var orderItem = OrderItem.Create(orderItemModel);

                await orderItemRepository.CreateAsync(orderItem);
            }
            await _unitOfWorkRepository.Commit();

            return _mapper.CreateMap(order);
        }
        catch
        {
            await _unitOfWorkRepository.Rollback();
            throw;
        }
    }

    public async Task<UpdateOrderResponse> UpdateOrder(UpdateOrderRequest updateOrderRequest, long loggedUserId)
    {
        var userRepository = _unitOfWorkRepository.Users;
        var clientRepository = _unitOfWorkRepository.Clients;
        var orderRepository = _unitOfWorkRepository.Orders;

        var loggedUser = await userRepository.GetById(loggedUserId) ?? throw new Exception();

        if (!loggedUser.IsActive)
            throw new Exception();

        var order = await orderRepository.GetById(updateOrderRequest.OrderId) ?? throw new Exception();

        var client = await clientRepository.GetById(order.ClientId) ?? throw new Exception();

        if (loggedUser.Id != client.UserId)
            throw new Exception();

        await _unitOfWorkRepository.Begin();
        try
        {
            order.SetStatus(updateOrderRequest.Paid ? OrderStatus.Paid : OrderStatus.Canceled);

            await orderRepository.UpdateAsync(order);
            _unitOfWorkRepository.Commit();

            return _mapper.UpdateMap(order);
        }
        catch
        {
            _unitOfWorkRepository.Rollback();
            throw;
        }
    }

    public async Task DeleteOrder(long id, long loggedUserId)
    {
        var userRepository = _unitOfWorkRepository.Users;
        var orderRepository = _unitOfWorkRepository.Orders;

        var loggedUser = await userRepository.GetById(loggedUserId) ?? throw new Exception();

        if (!loggedUser.IsActive || !Equals(UserRole.Master, loggedUser.Role))
            throw new Exception();

        var order = await orderRepository.GetById(id) ?? throw new Exception();

        await _unitOfWorkRepository.Begin();
        try
        {
            order.SetDeleted(DateTime.UtcNow);
            order.SetUpdated(DateTime.UtcNow);
            order.SetUpdatedBy(loggedUser.Id);

            await orderRepository.DeleteAsync(order);
            await _unitOfWorkRepository.Commit();
        }
        catch
        {
            await _unitOfWorkRepository.Rollback();
        }
    }

    public async Task<GetOrderResponse> GetOrderByIdAsync(long orderId, long loggedUserId)
    {
        var userRepository = _unitOfWorkRepository.Users;
        var clientRepository = _unitOfWorkRepository.Clients;
        var orderRepository = _unitOfWorkRepository.Orders;

        var loggedUser = await userRepository.GetById(loggedUserId) ?? throw new Exception();

        if (!loggedUser.IsActive)
            throw new Exception();

        var order = await orderRepository.GetById(orderId) ?? throw new Exception();

        var client = await clientRepository.GetById(order.ClientId) ?? throw new Exception();

        if (loggedUser.Id != client.UserId && Equals(UserRole.User, loggedUser.Role))
            throw new Exception();

        return _mapper.GetMap(order);
    }

    public async Task<IEnumerable<GetOrderResponse>> GetOrdersAsync(long loggedUserId)
    {
        var userRepository = _unitOfWorkRepository.Users;
        var orderRepository = _unitOfWorkRepository.Orders;

        var loggedUser = await userRepository.GetById(loggedUserId) ?? throw new Exception();

        if (!loggedUser.IsActive || !Equals(UserRole.Master, loggedUser.Role))
            throw new Exception();

        var orders = await orderRepository.GetList();

        return _mapper.GetListMap(orders);
    }
}
