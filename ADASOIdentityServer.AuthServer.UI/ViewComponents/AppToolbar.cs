using Microsoft.AspNetCore.Mvc;

namespace ADASOIdentityServer.AuthServer.UI.ViewComponents
{
    public class AppToolbarViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var controller = RouteData.Values["controller"]?.ToString();
            var action = RouteData.Values["action"]?.ToString();

            // Dinamik breadcrumb bilgileri
            var breadcrumb = new List<string>();

            if (controller == "Users")
            {
                breadcrumb.Add("Kullanıcı Yönetimi");
                
                if (action == "Index") breadcrumb.Add("Kullanıcı Listesi");
                
                else if (action == "Create") breadcrumb.Add("Yeni Kullanıcı");

            }
            else if (controller == "Roles")
            {
                breadcrumb.Add("Rol Yönetimi");
                if (action == "Index") breadcrumb.Add("Roller Listesi");
                else if (action == "Create") breadcrumb.Add("Yeni Rol");

            }
            else if (controller == "Departments")
            {
                breadcrumb.Add("Departman Yönetimi");
                if (action == "Index") breadcrumb.Add("Departman Listesi");
                else if (action == "Create") breadcrumb.Add("Yeni Departman");
            }
            else if (controller == "Consultant")
            {
                breadcrumb.Add("Danışman Yönetimi");
                if (action == "Index") breadcrumb.Add("Danışman Listesi");
                else if (action == "Create") breadcrumb.Add("Yeni Danışman");
            }
            else if (controller == "PersonelTitles")
            {
                breadcrumb.Add("Ünvan Yönetimi");
                if (action == "Index") breadcrumb.Add("Ünvan Listesi");
                else if (action == "Create") breadcrumb.Add("Yeni Ünvan");
            }
         
            else if (controller == "Projects")
            {
                breadcrumb.Add("Uygulama Yönetimi");
                if (action == "Index") breadcrumb.Add("Uygulama Listesi");
                else if (action == "Create") breadcrumb.Add("Yeni Uygulama");
            }
            //UserProjects
            else if (controller == "UserProjects")
            {
                breadcrumb.Add("Kullanıcı Uygulama Yönetimi");
                if (action == "Index") breadcrumb.Add("Kullanıcı Proje Listesi");
                else if (action == "Create") breadcrumb.Add("Yeni Kullanıcı Proje");
            }
    
            else
            {
                breadcrumb.Add("Ana Sayfa");
            }

            // diğer controllerlar buraya

            return View("AppToolbar", breadcrumb);
        }
    }

}