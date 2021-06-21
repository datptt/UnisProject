using System;
using Unis.Domain;
using Unis.Repository;

namespace Unis.API
{
    public class BaseBL<T> where T : AuditEntity<long>
    {
        private readonly IUnitOfWork _unitOfWork;

        public BaseBL(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public void BeforeSave(T entity)
        {
        }

        public void AfterSave(T entity)
        {

        }

    }
}
