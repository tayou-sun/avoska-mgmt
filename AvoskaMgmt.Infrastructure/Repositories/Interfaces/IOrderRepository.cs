using System.Collections.Generic;

public interface IOrderRepository {
    IEnumerable<OrderProductDto> Get(int id);
}