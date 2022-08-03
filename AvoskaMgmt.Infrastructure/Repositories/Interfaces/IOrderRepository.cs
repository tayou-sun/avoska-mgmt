using System.Collections.Generic;

public interface IOrderRepository
{
    IEnumerable<OrderProductTagDto> Get(int id);
    IEnumerable<OrderProductTagDto> Get();

    StatusDto GetStatus(int id);
    StatusDto SetStatus(int id);
}