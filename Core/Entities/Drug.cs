using System;
namespace Core.Entities
{
	public class Drug : BaseEntity
	{
        public double Price { get; set; }
        public int Count { get; set; }
        public Drugstore Drugstore { get; set; }
    }
}

