using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Daisy.Dal.Context;
using Daisy.Dal.Context.Interfaces;
using Daisy.Dal.Domain;
using Daisy.Dal.Repository.Interfaces;


namespace Daisy.Dal.Repository
{
    public abstract class Repository<TEntity> : IRepository<TEntity>
        where TEntity : BaseEntity
    {
        protected Repository(IContext context)
        {
            _context = context;
        }


        private IContext _context { get; set; }


        public void AddOrUpdate(TEntity entity)
        {
            ValidateBeforeSave(entity);
            _context.AddOrUpdate(entity);
        }


        public virtual List<TEntity> GetAll()
        {
            return _context.GetAll<TEntity>();
        }


        public virtual TEntity GetById(Guid id)
        {
            return _context.GetById<TEntity>(id);
        }


        public void Remove(Guid id)
        {
            _context.Remove<TEntity>(id);
        }


        private void ValidateBeforeSave(TEntity entity)
        {
            var validationContext = new ValidationContext(entity, null, null);
            var validationResults = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(entity, validationContext, validationResults, true);

            if (isValid)
            {
                return;
            }

            ValidationResult result = validationResults[0];
            throw new ArgumentException(result.ErrorMessage, string.Join(", ", result.MemberNames));
        }
    }
}
