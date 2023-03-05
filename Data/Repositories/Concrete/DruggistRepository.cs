using System;
using Core.Entities;
using Data.Contexts;
using Data.Repositories.Abstract;

namespace Data.Repositories.Concrete
{
    public class DruggistRepository : IDruggistRepository
    {
        static int id;

        public List<Druggist> GetAll()
        {
            return DbContext.Druggists;
        }

        public Druggist GetById(int id)
        {
            return DbContext.Druggists.FirstOrDefault(dr => dr.Id == id);
        }

        public void Create(Druggist druggist)
        {
            ++id;
            druggist.Id = id;
            DbContext.Druggists.Add(druggist);
        }

        public void Delete(Druggist druggist)
        {
            DbContext.Druggists.Remove(druggist);
        }


        public void Update(Druggist druggist)
        {
            var dbDrugists = DbContext.Druggists.FirstOrDefault(dr => dr.Id == druggist.Id);

            dbDrugists.Id = druggist.Id;
            dbDrugists.Name = druggist.Name;
            dbDrugists.Surname = druggist.Surname;
            dbDrugists.Age = druggist.Age;
            dbDrugists.Experience = druggist.Experience;
            dbDrugists.Drugstore = druggist.Drugstore;
        }
    }
}

