using SM.Radio.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace SM.Radio.IBLL
{
    public interface IBaseBLL<T> where T : BaseEntity
    {
        List<T> GetListBy<T>(Expression<Func<T, bool>> expression) where T : class;

        List<T> GetAll<T>() where T: class;

        T Create<T>(T t) where T : class;
        Boolean Edit<T>(T t) where T : class;
        Boolean Delete<T>(T t) where T : class;
    }
}
