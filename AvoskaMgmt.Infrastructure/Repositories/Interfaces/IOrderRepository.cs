using System.Collections.Generic;

public interface IOrderRepository {
    IEnumerable<OrderProductTagDto> Get(int id);
}