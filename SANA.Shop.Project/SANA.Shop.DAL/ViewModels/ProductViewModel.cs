using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SANA.Shop.DAL.ViewModels
{
    public class ProductViewModel
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
        public int Value { get; set; }
        public bool Status { get; set; }

    }

    public class ProductDatabase : List<ProductViewModel>
    {
        public ProductDatabase()
        {
            Add(new ProductViewModel() { Amount = 1, Name = "A", Value = 1, Description = "A", Id = 1, Status = true });
            Add(new ProductViewModel() { Amount = 2, Name = "B", Value = 1, Description = "A", Id = 2, Status = true });
            Add(new ProductViewModel() { Amount = 3, Name = "C", Value = 1, Description = "A", Id = 3, Status = true });
            Add(new ProductViewModel() { Amount = 4, Name = "D", Value = 1, Description = "A", Id = 4, Status = true });
            Add(new ProductViewModel() { Amount = 5, Name = "E", Value = 1, Description = "A", Id = 5, Status = true });
            Add(new ProductViewModel() { Amount = 6, Name = "F", Value = 1, Description = "A", Id = 6, Status = true });
        }
    }

    public class DataAccess
    {
        public List<ProductViewModel> Get()
        {
            return new ProductDatabase();
        }
    }
}
