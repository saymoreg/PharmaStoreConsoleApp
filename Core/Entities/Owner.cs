using System;
namespace Core.Entities
{
	public class Owner : BaseEntity
	{
        public Owner()
        {
            Drugstores = new List<Drugstore>();
        }
        public string Surname { get; set; }
        public List<Drugstore> Drugstores { get; set; }
    }
}

