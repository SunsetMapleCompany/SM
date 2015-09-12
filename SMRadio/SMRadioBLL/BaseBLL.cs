using SM.Radio.DAL;
using SM.Radio.IBLL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using SM.Radio.Entity;

namespace SM.Radio.BLL
{
    public class BaseBLL<T> : IBaseBLL<T> where T : BaseEntity
    {
        //protected ObjectContext _ctx = ((IObjectContextAdapter)new SMRadioEntities()).ObjectContext;
        protected ObjectContext _dbContext = ((IObjectContextAdapter)new SMRadioDbContext()).ObjectContext;

        public List<T> GetListBy<T>(Expression<Func<T, bool>> expression) where T : class
        {
            //SMRadioEntities _context = new SMRadioEntities();
            //return _ctx.CreateObjectSet<T>().Where(expression).ToList();
            return _dbContext.CreateObjectSet<T>().Where(expression).ToList();
        }

        public List<T> GetAll<T>() where T : class
        {
            return _dbContext.CreateObjectSet<T>().ToList();
        }

        public T Create<T>(T t) where T : class
        {
            //_ctx.CreateObjectSet<T>().AddObject(t);
            //_ctx.SaveChanges();

            _dbContext.CreateObjectSet<T>().AddObject(t);
            _dbContext.SaveChanges();
            return t;
        }

        public Boolean Edit<T>(T t) where T : class
        {
            _dbContext.CreateObjectSet<T>().Attach(t);
            _dbContext.ObjectStateManager.ChangeObjectState(t, EntityState.Modified);
            return _dbContext.SaveChanges() > 0;

            //Int32 result = 0;
            //using (SMRadioEntities db = new SMRadioEntities())
            //{
                
            //    db.Entry(t).State = System.Data.EntityState.Modified;
            //    result = db.SaveChanges();
            //}
            //return result > 0;
        }

        public Boolean Delete<T>(T t) where T : class
        {
            _dbContext.CreateObjectSet<T>().DeleteObject(t);
            _dbContext.ObjectStateManager.ChangeObjectState(t, EntityState.Deleted);
            return _dbContext.SaveChanges() > 0;

            //_ctx.CreateObjectSet<T>().DeleteObject(t);
            //return _ctx.SaveChanges();
        }
    }
}
