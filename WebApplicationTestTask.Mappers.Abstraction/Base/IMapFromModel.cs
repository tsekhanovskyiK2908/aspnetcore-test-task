using System;
using System.Collections.Generic;
using System.Text;

namespace WebApplicationTestTask.Mappers.Abstraction.Base
{
    public interface IMapFromModel<in TModel, out TEntity>
    {
        TEntity MapBackToEntity(TModel model);
    }
}
