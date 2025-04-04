using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Models.ViewModel
{
	public class ShoppingCardVM 
	{
		public IEnumerable<ShoppingCard> ShoppingCardList { get; set; }
		public double Total {  get; set; }
		
	}
}
