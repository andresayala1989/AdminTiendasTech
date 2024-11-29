using SANA.Shop.BLL.Model;
using SANA.Shop.DAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SANA.Shop.BLL.DAO
{
    public class SQLDAOProducts
    {

        private SanaShopEntities db = new SanaShopEntities();

        #region Singleton
        /// <summary>
        /// Se crea una instancia privada para la clase SQLDAOCronograma.
        /// Ha sido declarada como "volatile" para asegurar que la asignación de la instancia de la variable se complete luego de que ha sido accedida.
        /// </summary>
        private static volatile SQLDAOProducts instance;

        /// <summary>
        /// Permite asegurar que solo una instancia ha sido creada y solo cuando sea necesaria.
        /// En lugar de bloquearse así mismo, evita que ocurran otros bloqueos.
        /// </summary>
        private static object syncRoot = new Object();

        /// <summary>
        /// Se crea una instancia publica de la clase SQLDAOCronograma.
        /// Permitiendo una conexión unica y accesible por cualquiera.
        /// </summary>
        public static SQLDAOProducts Instance
        {
            get
            {
                //Corrige varios erros que podrían ocurrir de no tener un único hilo.
                if (instance == null)
                {
                    //Permite un unico hilo, por lo cual se bloquea, cuando la instancia de la clase no se ha creado.
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new SQLDAOProducts();
                        }
                    }
                }
                return instance;
            }
        }
        /// <summary>
        /// Constructor de la clase SQLDAOCronograma
        /// </summary>
        private SQLDAOProducts() { }
        #endregion
        /// <summary>
        /// Obtiene todos los productos
        /// </summary>
        /// <param name="Search"></param>
        /// <param name="IdCentroServicio"></param>
        /// <returns></returns>
        public List<ProductViewModel> GetAll()
        {
            List<ProductViewModel> lstProducts = new List<ProductViewModel>();

            lstProducts = db.Products.ToList().Select(n => new ProductViewModel
            {
                Name = n.Name,
                Id = n.Id,
                Amount = n.Amount,
                Value = Convert.ToInt32(n.Value),
                Status = n.Status,
                Description = n.Description
            }).ToList(); 

            return lstProducts;
        }

        public bool Create(ProductViewModel products)
        {
            try
            {
                Products oProduct = new Products();
                oProduct.Name = products.Name;
                oProduct.Description = products.Description;
                oProduct.Status = products.Status;
                oProduct.Amount = products.Amount;
                oProduct.Value = products.Value;
                db.Products.Add(oProduct);
                db.SaveChanges();
                return true;
            } catch (Exception ex)
            {
                return false;   
            }
            
        }

        public ProductViewModel GetbyId(int id)
        {
            Products oProduct = new Products();
            ProductViewModel oProductViewModel = new ProductViewModel();

            oProduct = db.Products.Find(id);

            oProductViewModel.Name = oProduct.Name;
            oProductViewModel.Id = oProduct.Id;
            oProductViewModel.Amount = oProduct.Amount;
            oProductViewModel.Value = Convert.ToInt32(oProduct.Value);
            oProductViewModel.Status = oProduct.Status;
            oProductViewModel.Description = oProduct.Description; 

            return oProductViewModel;
        }

        public bool Edit(ProductViewModel products)
        {
            try
            {
                Products oProducts = db.Products.Find(products.Id);
                oProducts.Status = products.Status;
                oProducts.Name = products.Name; 
                oProducts.Description = products.Description;   
                oProducts.Amount = products.Amount;
                oProducts.Value = products.Value;   
                db.Entry(oProducts).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                Products products = db.Products.Find(id);
                db.Products.Remove(products);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
