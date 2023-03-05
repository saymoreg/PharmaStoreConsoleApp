using System;
using Core.Entities;
using Data.Contexts;
using Data.Repositories.Abstract;

namespace Data.Repositories.Concrete
{
    public class DrugRepository : IDrugRepository
    {
        static int id;

        public List<Drug> GetAll()
        {
            return DbContext.Drugs;
        }

        public Drug GetById(int id)
        {
            return DbContext.Drugs.FirstOrDefault(d => d.Id == id);
        }

        public void Create(Drug drug)
        {
            ++id;
            drug.Id = id;
            DbContext.Drugs.Add(drug);
        }

        public void Delete(Drug drug)
        {
            DbContext.Drugs.Remove(drug);
        }


        public void Update(Drug drug)
        {
            var dbDrugs = DbContext.Drugs.FirstOrDefault(d => d.Id == drug.Id);

            dbDrugs.Id = drug.Id;
            dbDrugs.Name = drug.Name;
            dbDrugs.Price = drug.Price;
            dbDrugs.Count = drug.Count;
            dbDrugs.Drugstore = drug.Drugstore;
        }
    }
}

