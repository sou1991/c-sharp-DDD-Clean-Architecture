using Entities;
using Entities.Books;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Valueobject.Books;

namespace Infrastructure.Books
{
    public class BooksDBServiceRepository : IBooksRepository
    {
        private IDataBaseService _dataBaseService;

        public BooksDBServiceRepository(IDataBaseService dataBaseService)
        {
            _dataBaseService = dataBaseService;
        }

        public void Create(BooksEntity memberEntity)
        {
            _dataBaseService.Books.Add(ToModel(memberEntity));
        }

        public void Update(BooksDataModelBuilder dataModel)
        {
            _dataBaseService.Books.Update(dataModel.Build()).State = EntityState.Modified;
        }

        public void Save()
        {
            _dataBaseService.Save();
        }

        public BooksEntity FindSingle(string targetID, DateTime targetDate)
        {
            var dtoModel = _dataBaseService.Books
                              .Where(p => p.m_no == targetID && p.registDate == targetDate)
                              .AsNoTracking()
                              .FirstOrDefault();

            return Transfer(dtoModel);
        }

        public IQueryable<BooksEntity> Find(string targetID, int year, int month)
        {
            var dtoModel = _dataBaseService.Books
                .OrderByDescending(p => p.registDate)
                .Where(p => p.m_no == targetID
                && p.registDate.Year == year
                && p.registDate.Month == month);

            return Transfer(dtoModel, new List<BooksEntity>());

        }

        private BooksEntity Transfer(BooksData model)
        {
            if (model == null) return null;

            var registDate = new RegistDateValueObject(model.registDate);
            var entity = new BooksEntity(model.id, model.m_no, model.amountUsed, model.intime, registDate);

            return entity;
        }

        private IQueryable<BooksEntity> Transfer(IQueryable<BooksData> models, List<BooksEntity> entitis)
        {
            foreach (var model in models)
            {
                var registDate = new RegistDateValueObject(model.registDate);
                entitis.Add(new BooksEntity(model.id, model.m_no, model.amountUsed, model.intime, registDate));
            }

            return entitis.AsQueryable();
        }

        private BooksData ToModel(BooksEntity entity)
        {
            var dataModel = new BooksDataModelBuilder();

            entity.Notice(dataModel);

            return dataModel.Build();

        }
    }
}
