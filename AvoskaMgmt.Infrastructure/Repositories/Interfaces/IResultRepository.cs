using System.Collections.Generic;

public interface IResultRepository {
    IEnumerable<ResultDto> Get();
}