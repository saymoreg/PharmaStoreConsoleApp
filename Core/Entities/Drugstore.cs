using System;
namespace Core.Entities
{
	public class Drugstore : BaseEntity
	{
        //public Drugstore()
        //{
        //    Druggists = new List<Druggist>();
        //}

        public string Address { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public List<Druggist> Druggists { get; set; }
        public List<Drug> Drugs { get; set; }
        public Owner Owner { get; set; }
    }
}

