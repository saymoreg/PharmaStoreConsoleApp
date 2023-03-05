using System;
using Core.Entities;

namespace Data.Contexts
{
	public class DbContext
	{
		static DbContext()
		{
			Admins = new List<Admin>();
			Druggists = new List<Druggist>();
			Drugs = new List<Drug>();
			Owners = new List<Owner>();
			Drugstores = new List<Drugstore>();

		}

		public static List<Admin> Admins { get; set; }
		public static List<Druggist> Druggists { get; set; }
		public static List<Drug> Drugs { get; set; }
		public static List<Owner> Owners { get; set; }
		public static List<Drugstore> Drugstores { get; set; }
	}
}
