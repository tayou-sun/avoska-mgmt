using System.Collections.Generic;

public interface IResultRepository {
    IEnumerable<ResultDto> Get();
    void Create(ResultSaveDto res);
}