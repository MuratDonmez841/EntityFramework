using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using ProductEvalation.DataBase;
namespace ProductEvalation.Services
{
    class User
    {
        public void NewUser(Users users)
        {

            try
            {
                using (ProductEva_Entities1 product = new ProductEva_Entities1())
                {
                    product.Users.Add(users);
                    product.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public int ConUser(string mail, string password)
        {

            try
            {
                using (ProductEva_Entities1 product = new ProductEva_Entities1())
                {
                    var wMaild = product.Users.Where(x => x.UserMail == mail).Select(x => x.UserMail).FirstOrDefault();
                    var wPass = product.Users.Where(x => x.UserPassword == password).Select(x => x.UserPassword).FirstOrDefault();
                    var ID = product.Users.Where(x => x.UserMail == mail).Where(x => x.UserPassword == password).Select(x=> x.UserID).FirstOrDefault();
                    if (wPass == null || wMaild == null)
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

        public List<string> CompanyNamesforUI() {

            try
            {
                using (ProductEva_Entities1 product = new ProductEva_Entities1())
                {
                    var companyName = product.Company.Select(x=> x.CompanyName);
                    return companyName.ToList();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public void UpdateUser(Users users,bool clikControl) {
            try
            {
                using (ProductEva_Entities1 product= new ProductEva_Entities1())
                {
                    var userRow = product.Users.Where(X=> X.UserID==users.UserID).FirstOrDefault();
                    if (clikControl==true)
                    {
                        userRow.UserAdress = users.UserAdress;
                        userRow.UserDisct = users.UserDisct;
                        userRow.UserMail = users.UserMail;
                        userRow.UserName = users.UserName;
                        userRow.UserPostaKod = users.UserPostaKod;
                        userRow.UserProvi = users.UserProvi;
                        userRow.UserTel = users.UserTel;
                        userRow.profilPhoto = users.profilPhoto;
                    }
                    else 
                    {
                        userRow.UserAdress = users.UserAdress;
                        userRow.UserDisct = users.UserDisct;
                        userRow.UserMail = users.UserMail;
                        userRow.UserName = users.UserName;
                        userRow.UserPostaKod = users.UserPostaKod;
                        userRow.UserProvi = users.UserProvi;
                        userRow.UserTel = users.UserTel;
                        userRow.profilPhoto = users.profilPhoto;
                        userRow.UserPassword = users.UserPassword;
                    }
    
                    product.SaveChanges();

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public Users getUserInfo(int ID) {
            try
            {
                using (ProductEva_Entities1 product = new ProductEva_Entities1())
                {
                    return product.Users.Where(x => x.UserID == ID).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }



        }

        public List<Sells> getUserProducts(int ID) {
            try
            {
                using (ProductEva_Entities1 product = new ProductEva_Entities1())
                {
                    var userProducts = product.Sells.Where(x=> x.UserID==ID);
                    return userProducts.ToList();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }



        }

        public bool NewFeedBackStatus(int? userID,int? productID) {
            try
            {
                using (ProductEva_Entities1 product = new ProductEva_Entities1())
                {    

                    var control = product.FeedBack.Where(x=> x.UserID==userID);
                    bool control2 = false;
                    foreach (var item in control)
                    {
                        if (item.ProductID==productID)
                        {
                           
                            control2 = true;
                        }
                    }
                    
                    if (control2== false)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        public void AddFeedBAck(FeedBack feedBack, FeedBackDetail detail) {

            using (ProductEva_Entities1 product = new ProductEva_Entities1())
            {
                product.FeedBackDetail.Add(detail);
                product.FeedBack.Add(feedBack);

                var feedBackID = feedBack.ReturnID;
                var feedBDID = detail.FeedBackDetail_ID;

                FeedBack_FeedBackDetail feed = new FeedBack_FeedBackDetail();
                feed.FeedBack_ID = feedBackID;
                feed.FeedBakcDetailID = feedBDID;

                product.FeedBack_FeedBackDetail.Add(feed);
                product.SaveChanges();
            }

        }
        public List<V_FeedBakDetail> UserTicket(int UserID) {

            try
            {
                using (ProductEva_Entities1 product = new ProductEva_Entities1())
                {

                    var userTicket = product.V_FeedBakDetail.Where(x=> x.UserID==UserID);

                    return userTicket.ToList();

                    //var userFeedBack = product.FeedBack.Where(x=> x.UserID==UserID).Select(x=> x.ReturnID);

                    //if (userFeedBack!=null)
                    //{
                    //    var userTicket = product.FeedBack_FeedBackDetail.Where(x=> x.FeedBack_ID==int.Parse(userFeedBack.ToString())).Select(x=> x.FeedBakcDetailID);
                    //    if (userTicket!=null)
                    //    {
                    //        product.Fee
                    //    }
                    //}
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        public string UserTicketProduct(int ProductID,bool re) {
            try
            {
                using (ProductEva_Entities1 product = new ProductEva_Entities1())
                {
                    var productName = product.Products.Where(x=> x.ProductID==ProductID).FirstOrDefault();
                    int? companyID =productName.CompanyID;
                    var companyName = product.Company.Where(x=> x.CompanyID==companyID).FirstOrDefault();

                    if (re==true)
                    {
                        return companyName.CompanyName; 
                    }

                    else
                    {
                        return productName.PName; 
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public bool FlagStatus(int ID) {

            try
            {
                using (ProductEva_Entities1 product = new ProductEva_Entities1())
                {
                    var statusFeed = product.FeedBackDetail.Where(x=> x.FeedBackDetail_ID==ID).FirstOrDefault();

                    if (statusFeed.Flag==0)
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

        public double? AVgPointForProduct(int ID) {

            try
            {
                using (ProductEva_Entities1 product = new ProductEva_Entities1())
                {
                    var feedback = product.FeedBack.Where(x=> x.ProductID==ID).Select(x=> x.ReturnID);
                    IQueryable<int?> feedBack_Detail;
                    double? feedBackDetail=0;
                    foreach (var item in feedback)
                    {
                      feedBack_Detail = product.FeedBack_FeedBackDetail.Where(x => x.FeedBack_ID == item).Select(x=> x.FeedBakcDetailID);
                        foreach (var item2 in feedBack_Detail)
                        {
                            feedBackDetail = product.FeedBackDetail.Where(x=> x.FeedBackDetail_ID==item2).Average(x=> x.Dayanıklılık+x.DısGörünüs+x.FiyatPerf+x.Kalite+x.KullanımKolaylıgı);
                        }
                    }

                    if (feedBackDetail!=0)
                    {
                        return feedBackDetail/5;
                    }
                    else
                    {
                        return 0;
                    }
                 
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        public void UserBuy(Sells sells) {

            using (ProductEva_Entities1 product = new ProductEva_Entities1())
            {
                product.Sells.Add(sells);
                product.SaveChanges();
            }

        }


        public List<string> UserPoint(int? ProductID , int? UserID) {

            try
            {
                using (ProductEva_Entities1 product = new ProductEva_Entities1())
                {
                    var userFeedBack = product.FeedBack.Where(x=> x.ProductID==ProductID).Where(x=> x.UserID==UserID).FirstOrDefault();
                    List<string> userFeedBackPoints = new List<string>();
                    if (userFeedBack != null)
                    {
                        var FeedBack_feedBackdetail = product.FeedBack_FeedBackDetail.Where(x => x.FeedBack_ID == userFeedBack.ReturnID).FirstOrDefault();

                        var feedBackDetail = product.FeedBackDetail.Where(x => x.FeedBackDetail_ID == FeedBack_feedBackdetail.FeedBakcDetailID).FirstOrDefault();
                        
                        if (feedBackDetail.DısGörünüs != null && feedBackDetail.FiyatPerf != null && feedBackDetail.KullanımKolaylıgı != null && feedBackDetail.Kalite != null && feedBackDetail.Dayanıklılık != null)
                        {
                            userFeedBackPoints.Add(feedBackDetail.DısGörünüs.ToString());

                            userFeedBackPoints.Add(feedBackDetail.FiyatPerf.ToString());

                            userFeedBackPoints.Add(feedBackDetail.KullanımKolaylıgı.ToString());

                            userFeedBackPoints.Add(feedBackDetail.Kalite.ToString());

                            userFeedBackPoints.Add(feedBackDetail.Dayanıklılık.ToString());

                            userFeedBackPoints.Add(feedBackDetail.UserReview);

                            return userFeedBackPoints.ToList();
                        }
                        else
                        {
                            userFeedBackPoints = null;
                            return userFeedBackPoints;
                        }
                    }
                    else
                    {
                        userFeedBackPoints = null;
                        return userFeedBackPoints;
                    }

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public List<ProductCategory> aa(int? ID) {
            try
            {
                using (ProductEva_Entities1 product = new ProductEva_Entities1())
                {
                    var ID2 = product.Products.Where(x=> x.ProductID==ID);
                    var qu = ID2.Select(x=> x.ProductCategoryID);
                    List<ProductCategory> productCategories = new List<ProductCategory>();
                    foreach (var item in qu)
                    {
                        int counter = 0;
                        var category = product.ProductCategory.Where(x => x.ProductCategoryID == item);
                            foreach (var item2 in category)
                            {
                                var lastItem = category.Where(x => x.ProductCategoryName == x.ProductCategoryName).OrderBy(x => x.ProductCategoryName).Skip(counter).FirstOrDefault();
                                productCategories.Add(lastItem);
                            } 
                    }
                    return productCategories.ToList();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }



        }


        public bool control(string Mail) {
            try
            {
                using (ProductEva_Entities1 product = new ProductEva_Entities1())
                {
                    var mail = product.Users.Where(x => x.UserMail == Mail).FirstOrDefault();
                    if (mail!=null)
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

  
    }
}
