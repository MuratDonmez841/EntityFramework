using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media.Imaging;
using deneme.Models;
using System.Threading.Tasks;
namespace deneme.Services
{
    public class Services
    {

        IGenericRepsitory<User_PI> UserPI = new IRepository<User_PI>();
        IGenericRepsitory<User_Com> UserCom = new IRepository<User_Com>();

        IGenericRepsitory<FeedBack_Info> _info = new IRepository<FeedBack_Info>();
        IGenericRepsitory<FeedBack_Point> _point = new IRepository<FeedBack_Point>();
        IGenericRepsitory<FeedBack_Description> _description = new IRepository<FeedBack_Description>();

        IGenericRepsitory<Company> _company = new IRepository<Company>();
        IGenericRepsitory<Company_Info> _companinfo = new IRepository<Company_Info>();

        IGenericRepsitory<Product> _product = new IRepository<Product>();

        public async Task NewFeedBack(FeedBack_Info _Info, FeedBack_Point _Point, FeedBack_Description _Description) {
            try
            {
               await _info.Add(_Info);
               _Point.FeedBackID = _Info.FeedBackID;
               _Description.FeedBackID = _Point.FeedBackID;
               await _point.Add(_Point);
               await  _description.Add(_Description);
           
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public List<string> getCompanyName() {
            try
            {
                var namelist = _company.GetSourceList().Select(x => x.CompanyName);

                if (namelist != null)
                {
                    return namelist.ToList();
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public List<string> GetCompanyProduct(string CompanyName) {
            try
            {
                if (CompanyName != null)
                {
                    var CompanyID = _company.GetSourceList(x => x.CompanyName == CompanyName).Select(x => x.CompanyID).FirstOrDefault();
                    return _product.GetSourceList(x => x.CompanyID == CompanyID).Select(x => x.Name).ToList();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public decimal? GetProductPrice(string ProductName) {
            try
            {
                if (ProductName != null)
                {
                    return _product.GetSingle(x => x.Name == ProductName).SellPrice;
                }
                else
                {
                    return 0;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public int GetProductID(string Name) {
            try
            {
                if (Name != null)
                {
                    return _product.GetSingle(x => x.Name == Name).ID;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public int Connetion(string mail, string pass, out bool userConnetion) {
            try
            {
                int? id = 0;
                id = UserCom.GetSingle(x => x.Mail == mail).ID;
                if (id != 0)
                {
                    int? passID = 0;
                    passID = UserCom.GetSingle(x => x.Password == pass).ID;
                    if (passID != 0 && id == passID)
                    {
                        int? companyUser = 0;
                        companyUser = _company.GetSourceList(x => x.UserID == id).Select(x => x.CompanyID).FirstOrDefault();
                        if (companyUser != 0)
                        {
                            userConnetion = false;
                            return int.Parse(companyUser.ToString());
                        }
                        else
                        {
                            userConnetion = true;
                            return int.Parse(id.ToString());
                        }
                    }
                    else
                    {
                        userConnetion = false;
                        return 0;
                    }
                }
                else
                {
                    userConnetion = false;
                    return 0;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public int? CompanyUser(string mail, string pass)
        {



            using (FBEntities entities = new FBEntities())
            {
                int? id = 0;
                id = entities.User_Com.Where(x => x.Mail == mail).Select(x => x.ID).FirstOrDefault();

                if (id != 0)
                {
                    int? id2 = 0;
                    id2 = entities.User_Com.Where(x => x.Password == pass).Select(x => x.ID).FirstOrDefault();
                    if (id == id2)
                    {
                        var companyUser = entities.Company.Where(x => x.UserID == id).Select(x => x.CompanyID).FirstOrDefault();
                        if (companyUser != 0)
                        {
                            return companyUser;
                        }
                        else
                        {
                            return 0;
                        }
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    return 0;
                }
            }

        }
        public User_PI getUSerInfo(int? ID) {
            try
            {
                if (ID != null)
                {
                    return UserPI.GetSourceList(x => x.ID == ID).FirstOrDefault();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public User_Com Get_Com(int? ID) {
            try
            {
                return UserCom.GetSourceList(x => x.ID == ID).FirstOrDefault();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void UpdateUserInfo(User_PI user_PI, User_Com user_Com, int? ID) {
            try
            {
                var user = UserPI.GetSourceList(x => x.ID == ID).FirstOrDefault();

                if (user != null)
                {
                    user.UName = user_PI.UName;
                    user.Cinsiyet = user_PI.Cinsiyet;
                    user.BirdDay = user_PI.BirdDay;
                    user.IMG = user_PI.IMG;

                    UserPI.Update(user);
         
                }
                else
                {

                }
                var userCom = UserCom.GetSourceList(x => x.ID == ID).FirstOrDefault();
                if (userCom != null)
                {
                    userCom.Mail = user_Com.Mail;
                    userCom.Phone = user_Com.Phone;
                    userCom.Adress = user_Com.Adress;

                     UserCom.Update(userCom);
                
                }
                else
                {

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public bool UpdatePassword(User_Com user_Pass, int? ID, string oldPass) {

            try
            {
                using (FBEntities entities = new FBEntities())
                {
                    var user = entities.User_Com.Where(x => x.ID == ID).FirstOrDefault();

                    if (user.Password != oldPass)
                    {
                        return false;
                    }
                    else
                    {
                        user.Password = user_Pass.Password;
                        entities.SaveChanges();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public void NewProduct(Product product) {
            try
            {
               _product.Add(product);
       
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public Company_Info getCompanyInfo(int? ID) {
            try
            {
                return _companinfo.GetSourceList(X => X.CompanyID == ID).FirstOrDefault();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public Company getNameCompany(int? ID) {
            try
            {
                return _company.GetSourceList(x => x.CompanyID == ID).FirstOrDefault();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public string getUserName(int? ID) {
            try
            {
                return UserPI.GetSingle(x => x.ID == ID).UName;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<List<vProducts>> getView(int? id) {
            try
            {
                IGenericRepsitory<Company> genericRepsitoryCompany = new IRepository<Company>();
                IGenericRepsitory<vProducts> genericRepsitory = new IRepository<vProducts>();
                if (id != null)
                {
                    var companyName = genericRepsitoryCompany.GetSingle(x => x.CompanyID == id).CompanyName;
                    return genericRepsitory.GetSourceList(x => x.CompanyName == companyName).ToList();
                }

                else
                {
                  var list=  await Task.Run(()=> genericRepsitory.GetList());

                    return list.ToList();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<string> GetSub() {

            try
            {
                IGenericRepsitory<SubCategory> genericSub = new IRepository<SubCategory>();

                return genericSub.GetSourceList(x => x.SubName == x.SubName).Select(x => x.SubName).ToList();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public int? SubID(string Name) {
            try
            {
                IGenericRepsitory<SubCategory> _Sub = new IRepository<SubCategory>();

                return _Sub.GetSingle(x => x.SubName == Name).SubID;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void updateProduct(Product product, int id) {
            try
            {
                var row = _product.GetSourceList(x => x.ID == id).FirstOrDefault();
                row.Name = product.Name;
                row.SellPrice = product.SellPrice;
                row.SubCategoryID = product.SubCategoryID;

                _product.Update(row);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task DelProduct(int id) {
            try
            {
                var remove = _product.GetSourceList(x => x.ID == id).FirstOrDefault();
                if (remove != null)
                {
                    await Task.Run(()=> { _product.Delete(remove); });
                }
                else
                {

                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<List<vCategory>> categroyList() {
            try
            {
                IGenericRepsitory<vCategory> _vcategoryGeneric = new IRepository<vCategory>();

                var list =await Task.Run(()=> _vcategoryGeneric.GetList());
                return list.ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void NewCategory(Category category, SubCategory subCategory) {
            try
            {
                IGenericRepsitory<Category> _category = new IRepository<Category>();
                IGenericRepsitory<SubCategory> _subCategory = new IRepository<SubCategory>();

                _category.Add(category);
                subCategory.CategoryID = category.CategoryID;
                _subCategory.Add(subCategory);


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void UpdateCategory(Category category, SubCategory subCategory, int id, string name) {
            try
            {
                IGenericRepsitory<Category> _category = new IRepository<Category>();
                IGenericRepsitory<SubCategory> _subCategory = new IRepository<SubCategory>();

                var categoryRow = _category.GetSourceList(x => x.CategoryID == id).FirstOrDefault();
                var subRow = _subCategory.GetSourceList(x => x.SubName == name).FirstOrDefault();

                categoryRow.CategoryName = category.CategoryName;
                subRow.SubName = subCategory.SubName;

                _category.Update(categoryRow);
                _subCategory.Update(subRow);
            }
            catch (Exception ex)
            {

                throw ex;

            }
        }
        public FeedBack_Info ınfo(int feedbackid) {
            try
            {
                return _info.GetSourceList(x => x.FeedBackID == feedbackid).FirstOrDefault();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public FeedBack_Description description(int feedbackid) {
            try
            {
                return _description.GetSourceList(x => x.FeedBackID == feedbackid).FirstOrDefault();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public FeedBack_Point _Point(int feedbackid) {
            try
            {
                return _point.GetSourceList(x => x.FeedBackID == feedbackid).FirstOrDefault();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public string getSoloCompanyName(int? productID) {
            try
            {
                var companyID = _product.GetSingle(x => x.ID == productID).CompanyID;
                return _company.GetSingle(x => x.CompanyID == companyID).CompanyName;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public string getSoloProductName(int? ProductID) {
            try
            {
                if (ProductID != null)
                {
                    return _product.GetSingle(x => x.ID == ProductID).Name;
                }
                else
                {
                    return null;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<vFeedBack> getFeedBackList(int? id)
        {
            try
            {

                using (FBEntities entities = new FBEntities())
                {
                    var userFeedBAckID = entities.FeedBack_Info.Where(x => x.UserID == id);
                    int counter = 0;
                    List<vFeedBack> feedBackIDlist = new List<vFeedBack>();
                    foreach (var item in userFeedBAckID)
                    {
                        var feedbackID = userFeedBAckID.OrderBy(x => x.FeedBackID).Skip(counter).FirstOrDefault();
                        var view = entities.vFeedBack.Where(x => x.Bildirim_Numarası == feedbackID.FeedBackID).FirstOrDefault();
                        feedBackIDlist.Add(view);
                        counter++;
                    }
                    return feedBackIDlist.ToList();

                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public BitmapImage CompanIcon(int? CompanyID) {

            try
            {
                using (FBEntities entities = new FBEntities())
                {
                    var companyIconFilePath = entities.Company_Info.Where(x => x.CompanyID == CompanyID).Select(x => x.Icon).FirstOrDefault();

                    if (companyIconFilePath != null)
                    {
                        BitmapImage bitmapImage = new BitmapImage();
                        bitmapImage.BeginInit();
                        bitmapImage.UriSource = new Uri(companyIconFilePath);
                        bitmapImage.EndInit();
                        return bitmapImage;
                    }
                    else
                    {
                        BitmapImage bitmapImage = new BitmapImage();
                        bitmapImage.BeginInit();
                        bitmapImage.UriSource = new Uri("C:\\b\\anon.jpg");
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
        public void UpdateCompanIcon(Company_Info companyIcon, int? CompanID)
        {
            try
            {
                var CompanyRow = _companinfo.GetSourceList(x => x.CompanyID == CompanID).FirstOrDefault();
                CompanyRow.Icon = companyIcon.Icon;

                _companinfo.Update(CompanyRow);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<Product> getCompanyProduct(int? id, string search)
        {
            try
            {
                IGenericRepsitory<Product> genericRepsitory = new IRepository<Product>();
                if (search != null && id != null)
                {
                    return genericRepsitory.GetSourceList(x => x.CompanyID == id).Where(x => x.Name.Contains(search) || x.SellPrice.ToString().Contains(search)).ToList();
                }
                else
                {
                    return genericRepsitory.GetSourceList(x => x.CompanyID == id).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<vFeedBack> GetIdList(int? id) {

            try
            {
                if (id != null)
                {
                    IGenericRepsitory<vFeedBack> repository = new IRepository<vFeedBack>();

                    return repository.GetSourceList(x => x.Company == id).ToList();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public int GetCompanyID(string CompanyName) {

            try
            {
                if (CompanyName != null)
                {
                    IGenericRepsitory<Company> genericRepsitory = new IRepository<Company>();
                    return genericRepsitory.GetSingle(x => x.CompanyName == CompanyName).CompanyID;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public void NewUser1(User_Com _Com, User_PI _PI) {
           try
            {
               var control= UserPI.Add(_PI);
                _Com.ID = _PI.ID;
                UserCom.Add(_Com);


            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public string GetSoloName(int? id)
        {
            try
            {
                if (id != null)
                {
                    IGenericRepsitory<Company> generic = new IRepository<Company>();

                    return generic.GetSingle(x => x.CompanyID == id).CompanyName;
                }
                else
                {
                    return null;
                }
            }

            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<IEnumerable<User_Com>> UserComRepository() {
            var list = await Task.Run(()=>UserCom.GetList());
            return list.ToList();

        }
        public bool AddCompanyDescription(int? feedBackID, string companyDescription) {

            try
            {
                if (feedBackID != null)
                {
                    var getUserFeedBack = _description.GetSourceList(x => x.FeedBackID == feedBackID).FirstOrDefault();
                    getUserFeedBack.CompanyDescription = companyDescription;

                    _description.Update(getUserFeedBack);
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }


        }
        public string getCompayDescriptions(int? feedBackID){
            try
            {
                string companyDescription;
                if (feedBackID!=null)
                {
                      companyDescription= _description.GetSingle(x => x.FeedBackID == feedBackID).CompanyDescription;
                      if (companyDescription!=null)
                      {
                         return companyDescription;
                      }
                      else
                      {
                          companyDescription = "Henüz geri dönüş yapılmamış!!";
                         return companyDescription;
                      }
                }
                else
                {
                    companyDescription = "Henüz geri dönüş yapılmamış!!";
                    return companyDescription;
                }
                
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }
        public void getViews() {
            try
            {
               
                using (FBEntities _contex = new FBEntities())
                {
                    // var categories = _contex.FeedBack_Info.Join(_contex.FeedBack_Point, _contex.FeedBack_Point, id, _contex.FeedBack_Point.Where(x => x.FeedBackID == id).FirstOrDefault();
                    var a = from info in _contex.FeedBack_Info
                            from point in _contex.FeedBack_Point
                            where info.FeedBackID == point.FeedBackID
                            select new {info.FeedBackID, point};
                    var b = a; 
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }





        }
    }
}
