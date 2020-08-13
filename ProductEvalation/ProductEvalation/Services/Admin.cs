using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using ProductEvalation.DataBase;
namespace ProductEvalation.Services
{
    class Admin
    {
        public void NewADmin(Company admins)
        {
            using (ProductEva_Entities1 product = new ProductEva_Entities1())
            {

                    product.Company.Add(admins);
                    product.SaveChanges();
      
            }
        }
        public int ConADmin(string mail, string password)
        {

            try
            {
                using (ProductEva_Entities1 product = new ProductEva_Entities1())
                {
                    var adminMail = product.Company.Where(x => x.Mail == mail).Select(x => x.Mail).FirstOrDefault();
                    var adminPas = product.Company.Where(x => x.Apassword == password).Select(x => x.Apassword).FirstOrDefault();
                    var ID = product.Company.Where(x => x.Mail == mail).Where(x => x.Apassword == password).Select(x=> x.CompanyID).FirstOrDefault();
                    if (adminMail == null || adminPas == null)
                    {
                        return 0;
                    }
                    else
                    {
                        return ID;
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public int GetCompanyID(string companyName)
        {

            try
            {
                using (ProductEva_Entities1 product = new ProductEva_Entities1())
                {
                    var name = product.Company.Where(x => x.CompanyName == companyName).FirstOrDefault();
                    int id = name.CompanyID;
                    return id;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public bool flagControl(int ID ) {

            using (ProductEva_Entities1 product = new ProductEva_Entities1())
            {
                var flagCont = product.FeedBackDetail.Where(x=> x.FeedBackDetail_ID==ID).FirstOrDefault();
                if (flagCont.Flag==1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }

        public Company Company(int ID ) {
            try
            {
                using (ProductEva_Entities1 product = new ProductEva_Entities1())
                {
                    var row = product.Company.Where(x=> x.CompanyID==ID).FirstOrDefault();

                    return row;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }



        }

        public int ToplamSatis(int ID) {

            try
            {
                using (ProductEva_Entities1 product = new ProductEva_Entities1())
                {
                    var products = product.Products.Where(x=> x.CompanyID==ID);
                    var productID = products.Select(x=> x.ProductID);
                    int ID2=0;
                    foreach (var item in productID)
                    {
                         ID2 += product.Sells.Where(x=> x.ProductID==item).Count();
                    }
                    return ID2;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }
        public bool MailControlCompany(string CompanyMail) {

            try
            {
                using (ProductEva_Entities1 product = new ProductEva_Entities1())
                {
                    var companymail = product.Company.Where(x=> x.Mail==CompanyMail).FirstOrDefault();
                    if (companymail!=null)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        public BitmapImage feedBakcImage(int feedBackDetailID)
        {

            try
            {
                using (ProductEva_Entities1 product = new ProductEva_Entities1())
                {
                    var row = product.FeedBackDetail.Where(x => x.FeedBackDetail_ID == feedBackDetailID).FirstOrDefault();
                    BitmapImage bitmapImage = new BitmapImage();
                    string imgFilePath = row.IMG;
                    if (row.IMG!= null)
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

    }

}
