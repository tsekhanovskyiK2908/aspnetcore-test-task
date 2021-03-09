using System;
using System.Collections.Generic;
using System.Text;

namespace WebApplicationTestTask.Mappers.Abstraction.Base
{
    public interface IMapForUpdate<in TModel, TEntity>
    {
        TEntity MapBack(TModel model, TEntity existing);
    }
}
