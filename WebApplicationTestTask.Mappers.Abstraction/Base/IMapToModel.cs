using System;
using System.Collections.Generic;
using System.Text;

namespace WebApplicationTestTask.Mappers.Abstraction.Base
{
    public interface IMapToModel<in TEntity, out TModel>
    {
        TModel MapToModel(TEntity entity);
    }
}
