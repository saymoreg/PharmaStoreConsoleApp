using System;
using Core.Entities;
using Data.Contexts;
using Data.Repositories.Abstract;

namespace Data.Repositories.Concrete
{
    public class DrugstoreRepository : IDrugstoreRepository
    {
        static int id;

        public List<Drugstore> GetAll()
        {
            return DbContext.Drugstores;
        }

        public Drugstore GetById(int id)
        {
            return DbContext.Drugstores.FirstOrDefault(ds => ds.Id == id);
        }

        public void Create(Drugstore drugstore)
        {
            ++id;
            drugstore.Id = id;
            DbContext.Drugstores.Add(drugstore);
        }

        public void Delete(Drugstore drugstore)
        {
            DbContext.Drugstores.Remove(drugstore);
        }

        public void Update(Drugstore drugstore)
        {
            var dbDrugStore = DbContext.Drugstores.FirstOrDefault(ds => ds.Id == drugstore.Id);

            dbDrugStore.Name = drugstore.Name;
            dbDrugStore.Address = drugstore.Address;
            dbDrugStore.ContactNumber = drugstore.ContactNumber;
            dbDrugStore.Email = drugstore.Email;
            dbDrugStore.Druggists = drugstore.Druggists;
            dbDrugStore.Drugs = drugstore.Drugs;
            dbDrugStore.Owner = drugstore.Owner;
        }

        public bool IsDublicatedEmail(string email)
        {
            return DbContext.Drugstores.Any(ds => ds.Email == email);
        }
    }
}

