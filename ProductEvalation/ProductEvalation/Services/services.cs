using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductEvalation.DataBase;
using System.IO;
using System.Windows.Media.Imaging;

namespace ProductEvalation.Services
{
    class services
    {
        public List<V_Product_V> getView(int ID) {

            try
            {
                using (ProductEva_Entities1 product = new ProductEva_Entities1())
                {
                    var cName = product.Company.Where(x=> x.CompanyID==ID).FirstOrDefault();
                    var products = product.V_Product_V.Where(x=> x.CompanyName==cName.CompanyName);
                    return products.ToList();
                    
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public List<Sells> GetSells(int companyID) {

            try
            {
                using (ProductEva_Entities1 product=new ProductEva_Entities1())
                {
                    int counter = 0;
                    var CompanySells = product.Products.Where(x=> x.CompanyID==companyID);
                    var productID = CompanySells.Select(x=> x.ProductID);
                    List<Sells> selList = new List<Sells>();
                    foreach (var ID in productID)
                    {
                        counter = 0;
                        var sells = product.Sells.Where(x=> x.ProductID==ID);
                        foreach (var ID2 in sells)
                        {
                            var sells2 = sells.Where(x=> x.Sell_Id==x.Sell_Id).OrderBy(x=> x.Sell_Id).Skip(counter).FirstOrDefault();
                            selList.Add(sells2);
                            counter++;
                        }
                    }

                    return selList.ToList();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }
        public List<V_Categories> GetCategories()
        {
            try
            {
                using (ProductEva_Entities1 product = new ProductEva_Entities1())
                {
                    var a= product.V_Categories.ToList();
                    return a;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public List<V_FeedBakDetail> GetViewFeedBak(int ID) {
            try
            {
                using (ProductEva_Entities1 product= new ProductEva_Entities1())
                {
                    int skip = 0;
                    var productID = product.Products.Where(x => x.CompanyID == ID);

                    var products = productID.Select(x => x.ProductID).ToList();
                    List<V_FeedBakDetail> ff = new List<V_FeedBakDetail>();
                    foreach (var item in products)
                    {
                        skip = 0;
                        var v_FeedBak = product.V_FeedBakDetail.Where(x => x.ProductID == item);
                        foreach (var item2 in v_FeedBak)
                        {
                            var v_back = v_FeedBak.Where(x => x.UserReview == x.UserReview).OrderBy(x=> x.CompanyReview).Skip(skip).FirstOrDefault();
                            if (v_FeedBak != null)
                            {
                                ff.Add(v_back);
                            }
                            skip++;
                        }

                    }
                    return ff.ToList();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void NewProduct(Products products) {

            using (ProductEva_Entities1 entiti = new ProductEva_Entities1())
            {
                entiti.Products.Add(products);
                entiti.SaveChanges();
            }

        }
        public int CategoryID(string categoryName,string productCategory) {
            try
            {
                using (ProductEva_Entities1 product =new ProductEva_Entities1())
                {
                    var name = product.Category.Where(x=> x.CategoryName==categoryName).FirstOrDefault();
                    int categoryID = name.CategoryID;
                    var productc = product.ProductCategory.Where(X=> X.ProductCategoryID== categoryID).Where(x=> x.ProductCategoryName==productCategory).FirstOrDefault();
                    int ProductCategoryID = productc.ProductCategoryID;
                    return ProductCategoryID;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }
        public void NewCategory(Category category) {
            try
            {
                using (ProductEva_Entities1 product =new ProductEva_Entities1())
                {
                    var serach = product.V_Categories.Where(x=> x.CategoryName==category.CategoryName).Select(x=> x.CategoryName).FirstOrDefault();
                    if (serach==null)
                    {
                        product.Category.Add(category);
                        product.SaveChanges(); 
                    }

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public void NewPerCategory(ProductCategory productCategory ,string categoryName) {

            try
            {
                using (ProductEva_Entities1 product = new ProductEva_Entities1())
                {
                    var category = product.Category.Where(x=> x.CategoryName==categoryName).FirstOrDefault();
                    int id = category.CategoryID;
                    productCategory.CategoryID = id;
                    product.ProductCategory.Add(productCategory);
                    product.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public string GetUserName(int? id) {

            using (ProductEva_Entities1 product=new ProductEva_Entities1())
            {
                var name = product.Users.Where(x=> x.UserID==id).Select(x=> x.UserName).FirstOrDefault();
                return name;
            }

        }

        public string GetProductName(int? id) {
            using (ProductEva_Entities1 product=new ProductEva_Entities1())
            {
                var productName = product.Products.Where(x => x.ProductID == id).Select(x=> x.PName).FirstOrDefault();
                return productName;
            }

        }

        public void flag(Sells sells) {
            try
            {
                using (ProductEva_Entities1 product= new ProductEva_Entities1())
                {
                    var sel = product.Sells.Where(x=> x.Sell_Id==sells.Sell_Id).FirstOrDefault();
                    sel.DeliveryFlag = sells.DeliveryFlag;
                    product.SaveChanges();

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }



        }


        public void feedFlag(FeedBackDetail feedBackDetail) {

            try
            {
                using (ProductEva_Entities1 product = new ProductEva_Entities1())
                {
                    var feedbackflag = product.FeedBackDetail.Where(x => x.FeedBackDetail_ID == feedBackDetail.FeedBackDetail_ID).FirstOrDefault();
                    feedbackflag.Flag = feedBackDetail.Flag;
                    if (feedBackDetail.CompanyReview!=null&& feedBackDetail.CompanyReview!="")
                    {
                        feedbackflag.CompanyReview = feedBackDetail.CompanyReview;
                    }
                    product.SaveChanges();

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public List<string> ReturnCategory() {

            try
            {
                using (ProductEva_Entities1 product = new ProductEva_Entities1())
                {
                    return product.Category.Select(x=> x.CategoryName).ToList();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        public List<string> returnProductCategory() {

            try
            {
                using (ProductEva_Entities1 product = new ProductEva_Entities1())
                {
                    return product.ProductCategory.Select(x => x.ProductCategoryName).ToList();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }


        public List<V_Product_V> ProductList(string name) {
            try
            {
                using (ProductEva_Entities1 product= new ProductEva_Entities1())
                {
                    var source = product.V_Product_V.Where(x => x.CompanyName == name);

                    return source.ToList();
                 }
            }
            catch (Exception ex)
            {

                throw ex;
            }



        }

        public List<V_Product_V> ListProductCategory(string CategoryName) {

            try
            {
                using (ProductEva_Entities1 product = new ProductEva_Entities1())
                {
                    var source = product.V_Product_V.Where(x => x.CategoryName == CategoryName);
                    return source.ToList();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public void log(int UserID) {
            try
            {
                StreamWriter streamWriter=null;
                streamWriter = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory+"\\file.txt", true);
                streamWriter.Write("User ID='"+UserID.ToString()+"'     Time='"+DateTime.Now.ToString()+"' \n");
                streamWriter.Flush();
                streamWriter.Close();
            }
            catch (Exception ex)
            {

                throw ex;
            }



        }

        public BitmapImage Image(int ID) {
            try
            {
                using (ProductEva_Entities1 product = new ProductEva_Entities1())
                {
                    var imgFilePath = product.Products.Where(x=> x.ProductID==ID).Select(x=> x.IMG).FirstOrDefault();

                    BitmapImage bitmapImage = new BitmapImage();
                    if (imgFilePath!=null)
                        {
                            bitmapImage.BeginInit();
                            bitmapImage.UriSource = new Uri(imgFilePath);
                            bitmapImage.EndInit();

                            return bitmapImage;
                        }

                    else
                    {
                        bitmapImage.BeginInit();
                        bitmapImage.UriSource = new Uri("C:\\b\\resimyok.png");
                        bitmapImage.EndInit();

                        return bitmapImage;
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }



        }

        //public void UpdatePro(Products product) {

        //    using (ProductEva_Entities1 productEva = new ProductEva_Entities1())
        //    {
        //        var row = productEva.Products.Where(x=> x.ProductID==product.ProductID).FirstOrDefault();
        //        row.PName = product.PName;
        //        row.Color = product.Color;
        //        row.Price = product.Price;
        //        row.SellStartDate = product.SellStartDate;
        //        row.ProductCategoryID = product.ProductCategoryID;
        //        row.IMG = product.IMG;

        //        productEva.SaveChanges();
        //    }

        //}
        public List<string> vs() {

            try
            {
                using (ProductEva_Entities1 product =new ProductEva_Entities1())
                {
                    var cID = product.Category.Select(x=> x.CategoryID);
                    int counter;
                    List<string> name = new List<string>();

                    foreach (var items in cID)
                    {
                        counter = 0;
                        var Pcategory = product.ProductCategory.Where(x=> x.CategoryID==items);
                        foreach (var item in Pcategory)
                        {
                            var id = Pcategory.Where(x=> x.ProductCategoryID==item.ProductCategoryID).Select(x=> x.ProductCategoryName).Skip(counter).FirstOrDefault();
                            counter++;
                            name.Add(id);
                        }
                    }
                    return name.ToList();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }



        }

        
  

    }
}
