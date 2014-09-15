using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ToshlNet.Endpoints;
using ToshlNet.Models;
using ToshlNet.OAuth;

namespace ToshlNet.SampleMVC.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            string code = Request.QueryString["code"];

            if (!string.IsNullOrEmpty(code))
            {
                ToshlOAuth toshlOAuth = (ToshlOAuth)Session["toshlOAuth"];

                OAuthToken oAuthToken = await toshlOAuth.GetAccessToken(Request.Url.AbsoluteUri);

                Me me = new Me(oAuthToken.AccessToken);

                User user = await me.Get();

                user.LastName = "tset";

                //User user1 = await me.Put(user);

                ExpenseApi expenseApi = new ExpenseApi(oAuthToken.AccessToken);

                Expense expense = new Expense()
                {
                    Date = "2014-09-06",
                    Amount = 12.34M,
                    Tags = new string[] { "test", "test432" }
                };

                //await expenseApi.Create(expense);
            }

            return View();
        }

        public ActionResult Toshl()
        {
            ToshlClient toshlClient = new ToshlClient()
            {
                Id = "947da348-5b4a-4e42-848d-6dd71f3768a65d16f23533a889067ed833ba0eceb1a1",
                Secret = "3c888771-20c6-424c-ad37-e219dc855c11fd4cf511bac1990bc38f5cdbad7b2a9b"
            };

            ToshlOAuth toshlOAuth = new ToshlOAuth(toshlClient);

            string authorizeUri = toshlOAuth.GetAuthorizeUri();

            Session.Add("toshlOAuth", toshlOAuth);

            return Redirect(authorizeUri);
        }
    }
}