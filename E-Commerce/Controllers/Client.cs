using System.Security.Claims;
using E_Commerce.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Controllers
{
    public class Client : Controller
    {

        public CUsersPcDesktopProjetsECommerceEcoleMdfContext db;
        public Client(CUsersPcDesktopProjetsECommerceEcoleMdfContext dbContext)
        {
            db = dbContext;
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {

            await HttpContext.SignOutAsync("ClientAuth");


            return RedirectToAction("ClientAuth", "Client");
        }
        [Authorize(AuthenticationSchemes = "ClientAuth")]
        [HttpPost]
        public IActionResult Transaction()
        {
            var userClaims = HttpContext.User.Claims;
            var idcl = Convert.ToInt32(userClaims.FirstOrDefault(c => c.Type == "IdClient").Value);

            List<Panier> panier = db.Paniers.Where(t => t.IdClient == idcl).ToList();
            Transaction t;
            DateTime dateTime = DateTime.Now;

            foreach (var p in panier)
            {

                t = new Transaction()
                {
                    IdClient = idcl,
                    Delivre = "En_Cours",
                    IdProduit = p.IdProduit,
                    NumTransaction = db.Transactions.Count(),
                    QteProduit = p.QteProduit,

                    //PrixTransaction= 5000,
                    DateTransaction = new DateOnly(dateTime.Year, dateTime.Month, dateTime.Day)

                };


                //db.Transactions.Add(t);


            }

            //db.SaveChanges();
            return Json(new { success = true });
        }




        [Authorize(AuthenticationSchemes = "ClientAuth")]
        [HttpPost]
        public IActionResult AjouterPanier(int id)
        {
            /*if (HttpContext.User.Claims.IsNullOrEmpty()) {
                return RedirectToAction("ClientAuth","Client");
            }*/

            var userClaims = HttpContext.User.Claims;
            var idcl = Convert.ToInt32(userClaims.FirstOrDefault(c => c.Type == "IdClient").Value);
            Panier panier = new Panier()
            {
                IdProduit = id,
                IdClient = idcl
            ,
                IdPanier = db.Paniers.Count() + 1,
                QteProduit = 1
            };
            db.Paniers.Add(panier);
            db.SaveChanges();



            return Json(new { success = true });
        }

        public IActionResult Accueil()
        {
            AccueilClient accueilClient = new AccueilClient()
            {
                categories = db.Categories.ToList(),
                //  count= db.Produits.Where(
                //    t => t.IdScategorieNavigation.IdCategorieNavigation.IdCategorie == 1).Count()
                produits = db.Produits.OrderByDescending(t => t.PourcentageSolde).Take(8).ToList(),

            };



            return View(accueilClient);
        }

        [HttpPost]
        public IActionResult SignUp(LoginClient credentials)
        {
            credentials.neww.IdClient = db.Clients.Count() + 1;
            credentials.neww.StatusClient = "normal";
            credentials.neww.PrenomClient = credentials.neww.NomClient;
            credentials.neww.IdAdresse = 1;

            db.Clients.Add(credentials.neww);
            db.SaveChanges();

            return RedirectToAction("ClientAuth", "Client");
        }

        public IActionResult ClientAuth()
        {
            return View();
        }


        [Authorize(AuthenticationSchemes = "ClientAuth")]
        public IActionResult Panier()
        {
            var userClaims = HttpContext.User.Claims;
            var idcl = Convert.ToInt32(userClaims.FirstOrDefault(c => c.Type == "IdClient").Value);
            var model = db.Paniers.Where(
                t => t.IdClient == idcl && t.QteProduit != 0).Include(t => t.IdProduitNavigation).ToList();

            return View(model);
        }

        [Authorize(AuthenticationSchemes = "ClientAuth")]
        public IActionResult Profile()
        {
            var userClaims = HttpContext.User.Claims;
            int id = Convert.ToInt32(userClaims.FirstOrDefault(c => c.Type == "IdClient").Value);

            Models.Client c = db.Clients.Include(t => t.IdAdresseNavigation).FirstOrDefault(t => t.IdClient == id);


            return View(c);
        }


        public IActionResult Categories(int id)
        {
            CategoriesModel model = new CategoriesModel()
            {
                scategories = db.Scategories.Where(t => t.IdCategorieNavigation.IdCategorie == id).ToList(),
                produits = db.Produits.Where(t => t.IdScategorieNavigation.IdCategorie == id).ToList(),
            };

            return View(model);
        }



        public IActionResult DetailsProduit(int id)
        {
            Produit produit = db.Produits.Find(id);

            return View(produit);
        }

        //////////////////////////////////////////////////////////////////////



        [HttpPost]
        public async Task<IActionResult> ClientAuth(LoginClient credentials)
        {
            bool exists = db.Clients.Any(x => x.EmailClient == credentials.exists.Username &&
            x.MotPassClient == credentials.exists.Password && x.StatusClient == "normal");

            Models.Client p = db.Clients.FirstOrDefault(x => x.EmailClient == credentials.exists.Username &&
              x.MotPassClient == credentials.exists.Password && x.StatusClient == "normal");

            if (exists)
            {
                var claims = new List<Claim>
                {
                     new Claim("IdClient", p.IdClient.ToString()),
                    new Claim(ClaimTypes.Name, p.NomClient),
                new Claim(ClaimTypes.Email, p.EmailClient),

                };

                var claimsIdentity = new ClaimsIdentity(claims, "login");

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true
                };

                await HttpContext.SignInAsync("ClientAuth", new ClaimsPrincipal(claimsIdentity), authProperties);



                return RedirectToAction("Accueil", "Client");


            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt");

            return View();
        }


        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpPost]
        public IActionResult UpdateValue(int id, int newValue)
        {
            // Find the entity by ID
            Panier panier = db.Paniers.Find(id);


            // Update the value
            panier.QteProduit = newValue;

            db.Paniers.Update(panier);

            db.SaveChanges();


            // You can return a success message or handle errors as needed
            return Json(new { success = true });
        }

    }
}
