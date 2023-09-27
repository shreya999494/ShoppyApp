using Newtonsoft.Json;
using ShoppyApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace ShoppyApp.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Login(CreateUser cuser)
        {
            string Baseurl = "https://localhost:44319/";

            List<CreateUser> UserInfo = new List<CreateUser>();
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/Values/GetUsers");

                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    UserInfo = JsonConvert.DeserializeObject<List<CreateUser>>(EmpResponse);
                    string Isuser = "False";
                    foreach (var user in UserInfo)
                    {
                        while (user.Email == cuser.Email && user.Password == cuser.Password)
                        {
                            Isuser = "True";
                            HttpResponseMessage tokenres = await client.GetAsync($"api/Authentication/CreateToken?username={user.Email}&password={user.Password}");
                            if (Res.IsSuccessStatusCode)
                            {
                                var tokenstring = tokenres.Content.ReadAsStringAsync().Result;
                                var tokeninfo = JsonConvert.DeserializeObject<dynamic>(tokenstring);
                                
                                //Session[""] = tokeninfo;
                                return RedirectToAction("About", "Home");
                            }
                        }
                    }
                    if (Isuser == "False")
                    {
                        ViewBag.Message = "Invalid Username or Password";
                        ModelState.Clear();
                        return View("Login");
                    }
                }

                return View();
            }
        }
        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Registration(CreateUser cuser)
        {
            string Baseurl = "https://localhost:44319/";

            //List<CreateUser> UserInfo = new List<CreateUser>();
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //var jsonContent = JsonConvert.SerializeObject(cuser);
                //var buffer = System.Text.Encoding.UTF8.GetBytes(jsonContent);
                //var byteContent = new ByteArrayContent(buffer);
                HttpResponseMessage Res = await client.PostAsJsonAsync("api/Values/CreateUser", cuser);
                //HttpResponseMessage Res = await client.PostAsync(Baseurl, byteContent);
                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    var UserInfo = JsonConvert.DeserializeObject<dynamic>(EmpResponse);
                    return RedirectToAction("About", "Home");
                }
                else
                {
                    return View();
                }

                return View();

            }
        }
            
        }

    }