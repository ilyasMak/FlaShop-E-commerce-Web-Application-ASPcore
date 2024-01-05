using System.Security.Claims;
using E_Commerce.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace E_Commerce.Controllers
{
    [Authorize(AuthenticationSchemes = "AdminAuth")]
    public class Administrateur : Controller

    {
        public CUsersPcDesktopProjetsECommerceEcoleMdfContext db;
        public Administrateur(CUsersPcDesktopProjetsECommerceEcoleMdfContext dbContext)
        {
            db = dbContext;
        }

        public IActionResult Accueil()
        {
            AdminDashBoard dash = new AdminDashBoard
            {
                TotalTransactionPrice = db.Transactions.Sum(t => t.PrixTransaction),
                OwnerCount = db.Proprietaires.Count(),
                UserCount = db.Clients.Count(),
                RecentTransactions = db.Transactions.Include(c => c.IdClientNavigation)
                .Include(c => c.IdProduitNavigation).OrderByDescending(t => t.DateTransaction)
                .Take(10).ToList(),
                UsersBlocked = db.Proprietaires.Where(t => t.StatusProprietaire == "blocked").ToList()

            };


            return View(dash);
        }

        public IActionResult Propriétaires()
        {
            var ListeProprietaires = db.Proprietaires.ToList();

            return View(ListeProprietaires);
        }

        public IActionResult Clients()
        {
            var ListeClients = db.Clients.ToList();

            return View(ListeClients);
        }

        public IActionResult Reports()
        {
            return View();
        }

        public IActionResult Produits()
        {
            var ListeProduits = db.Produits
                .Include(c => c.IdProprietaireNavigation)
                .Include(c => c.IdScategorieNavigation)
                .ToList();

            return View(ListeProduits);
        }

        public IActionResult MessageErreur()
        {
            return View();
        }

        public IActionResult Produit()
        {
            return View();
        }

        public IActionResult Utilisateur(int id, string type)
        {
            SharedClientProp data = new SharedClientProp();
            data.Id = id;
            data.type = type;
            if (type == "owner")
            {
                data.Nom = db.Proprietaires.FirstOrDefault(t => t.IdProprietaire == id).NomProprietaire;
                data.Email = db.Proprietaires.FirstOrDefault(t => t.IdProprietaire == id).EmailProprietaire;
                data.Prenom = db.Proprietaires.FirstOrDefault(t => t.IdProprietaire == id).PrenomProprietaire;
                data.Status = db.Proprietaires.FirstOrDefault(t => t.IdProprietaire == id).StatusProprietaire;
                data.Adress_Activ = db.Proprietaires.FirstOrDefault(t => t.IdProprietaire == id).ActiviteProprietaire;
                data.transactions = db.Transactions.Include(t => t.IdProduitNavigation).Where(t => t.IdProduitNavigation.IdProprietaire == id).ToList();
                data.produits = db.Produits.Where(t => t.IdProprietaire == id).ToList();

            }
            if (type == "client")
            {
                data.Nom = db.Clients.FirstOrDefault(t => t.IdClient == id).NomClient;
                data.Prenom = db.Clients.FirstOrDefault(t => t.IdClient == id).PrenomClient;
                data.Email = db.Clients.FirstOrDefault(t => t.IdClient == id).EmailClient;
                data.Status = db.Clients.FirstOrDefault(t => t.IdClient == id).StatusClient;
                data.Adress_Activ = db.Clients.FirstOrDefault(t => t.IdClient == id).IdAdresseNavigation?.NumMaison + " " +
                db.Clients.FirstOrDefault(t => t.IdClient == id).IdAdresseNavigation?.Rue + " " +
                    db.Clients.FirstOrDefault(t => t.IdClient == id).IdAdresseNavigation?.Ville + " " +
                    db.Clients.FirstOrDefault(t => t.IdClient == id).IdAdresseNavigation?.Pays;




                data.transactions = db.Transactions.Include(t => t.IdProduitNavigation).Where(t => t.IdClient == id).ToList();

            }


            return View(data);
        }


        /// //////////////////////////////////////////////////

        [AllowAnonymous]
        public IActionResult AdminAuth()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> AdminAuth(LoginModel credentials)
        {
            bool exists = db.Proprietaires.Any(x => x.EmailProprietaire == credentials.Username &&
            x.MotPassProprietaire == credentials.Password && x.StatusProprietaire == "Admin");

            Models.Proprietaire p = db.Proprietaires.FirstOrDefault(x => x.EmailProprietaire == credentials.Username &&
              x.MotPassProprietaire == credentials.Password && x.StatusProprietaire == "Admin");

            if (exists)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, p.NomProprietaire),
                new Claim(ClaimTypes.Email, p.EmailProprietaire),

                };

                var claimsIdentity = new ClaimsIdentity(claims, "login");

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = credentials.RememberMe
                };

                await HttpContext.SignInAsync("AdminAuth", new ClaimsPrincipal(claimsIdentity), authProperties);



                return RedirectToAction("Accueil", "Administrateur");


            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {

            await HttpContext.SignOutAsync("AdminAuth");


            return RedirectToAction("AdminAuth", "Administrateur");
        }

        [HttpPost]
        public IActionResult Delete(int id, string type)
        {
            if (type == "owner")
            {
                db.Proprietaires.Remove(db.Proprietaires.Find(id));

            }
            if (type == "client")
            {
                db.Clients.Remove(db.Clients.Find(id));

            }

            db.SaveChanges();
            return RedirectToAction("Accueil", "Administrateur");
        }



        [HttpGet]
        public IActionResult GetDataByDate(string date)
        {
            // Convert the date string to a DateTime object (you might need to adjust the format)
            if (DateOnly.TryParse(date, out DateOnly selectedDate))
            {
                // Assuming you have a model named User and a property named Data
                var data = db.Transactions.Where(u => u.DateTransaction == selectedDate).Sum(t => t.PrixTransaction);

                // Return the data as a string (you might need to convert it based on your data type)
                return Ok(data);
            }

            // Return an error if the date is not valid
            return BadRequest("Invalid date format");
        }

        [HttpPost]
        public IActionResult Block(bool block, int id, string type)
        {
            if (block == true)
            {
                if (type == "owner")
                {
                    Models.Proprietaire p = db.Proprietaires.Find(id);
                    p.StatusProprietaire = "blocked";

                    db.Proprietaires.Update(p);
                    db.SaveChanges();
                    return RedirectToAction("Propriétaires", "Administrateur");
                }

                if (type == "client")
                {

                    Models.Client c = db.Clients.Find(id);
                    c.StatusClient = "blocked";

                    db.Clients.Update(c);
                    db.SaveChanges();
                    return RedirectToAction("Clients", "Administrateur");
                }

            }
            else
            {
                return RedirectToAction("Produits", "Administrateur");

            }
            return RedirectToAction("Accueil", "Administrateur");
        }

    }


}
