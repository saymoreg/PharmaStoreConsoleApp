using System;
namespace Core.Entities
{
	public class Druggist : BaseEntity
	{
        public string Surname { get; set; }
        public int Age { get; set; }
        public int Experience { get; set; }
        public Drugstore Drugstore { get; set; }
    }
}

