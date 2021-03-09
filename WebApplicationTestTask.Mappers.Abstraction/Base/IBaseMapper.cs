using System;
using System.Collections.Generic;
using System.Text;

namespace WebApplicationTestTask.Mappers.Abstraction.Base
{
    public interface IBaseMapper<TIn, TOut> : IMapToModel<TIn, TOut>, IMapFromModel<TOut, TIn>
    {
    }
}
