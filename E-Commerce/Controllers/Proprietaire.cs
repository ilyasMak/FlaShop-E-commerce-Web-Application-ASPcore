using System.Diagnostics;
using System.Security.Claims;
using E_Commerce.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Controllers


{
    [Authorize(AuthenticationSchemes = "ProprietaireAuth")]
    public class Proprietaire : Controller

    {
        private readonly IWebHostEnvironment _webHostEnvironment;




        private readonly CUsersPcDesktopProjetsECommerceEcoleMdfContext db;
        private readonly ILogger<Proprietaire> _logger;

        public Proprietaire(CUsersPcDesktopProjetsECommerceEcoleMdfContext dbContext,
            ILogger<Proprietaire> logger,
            IWebHostEnvironment webHostEnvironment
            )
        {
            db = dbContext;
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
        }

        /// <summary>
        /// ///////////////////////////////////////////////////////////////////
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public IActionResult ProprietaireAuth()
        {
            return View();
        }



        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ProprietaireAuth(LoginPropModel credentials)
        {
            bool exists = db.Proprietaires.Any(x => x.EmailProprietaire == credentials.exists.Username &&
            x.MotPassProprietaire == credentials.exists.Password && x.StatusProprietaire == "normal");

            Models.Proprietaire p = db.Proprietaires.FirstOrDefault(x => x.EmailProprietaire == credentials.exists.Username &&
              x.MotPassProprietaire == credentials.exists.Password && x.StatusProprietaire == "normal");

            if (exists)
            {
                var claims = new List<Claim>
                {
                     new Claim("Id", p.IdProprietaire.ToString()),
                    new Claim(ClaimTypes.Name, p.NomProprietaire),
                new Claim(ClaimTypes.Email, p.EmailProprietaire),

                };

                var claimsIdentity = new ClaimsIdentity(claims, "login");

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true
                };

                await HttpContext.SignInAsync("ProprietaireAuth", new ClaimsPrincipal(claimsIdentity), authProperties);



                return RedirectToAction("Accueil", "Proprietaire");


            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt");

            return View();
        }



        /// <summary>
        /// /////////////////////////////////////////////////////////
        /// </summary>
        /// <returns></returns>
        /// 
        public IActionResult CréerProduit()
        {

            return View();
        }



        [HttpPost]
        public IActionResult SignUp(LoginPropModel credentials)
        {
            credentials.neww.IdProprietaire = db.Proprietaires.Count() + 1;
            credentials.neww.StatusProprietaire = "normal";
            credentials.neww.PrenomProprietaire = credentials.neww.NomProprietaire;

            db.Proprietaires.Add(credentials.neww);
            db.SaveChanges();

            return RedirectToAction("ProprietaireAuth", "Proprietaire");
        }

        [HttpPost]


        /// <summary>
        /// /////////////////////////////////////////////
        /// </summary>
        /// <returns></returns>




        public IActionResult SaveProduit(ModelProp produit)
        {
            List<IFormFile> Images = produit.Images;
            Produit produit1 = produit.Ajouter;
            produit1.IdScategorie = 2;
            var idpr = db.Produits.Count() + 1;
            produit1.IdProduit = idpr;
            var userClaims = HttpContext.User.Claims;
            int id = Convert.ToInt32(userClaims.FirstOrDefault(c => c.Type == "Id").Value);
            produit1.IdProprietaire = id;
            var fileName1 = Path.GetFileNameWithoutExtension(Images[0].FileName);
            //   var fileName2 = Path.GetFileNameWithoutExtension(Images[1].FileName);
            // var fileName3 = Path.GetFileNameWithoutExtension(Images[2].FileName);
            var extension1 = Path.GetExtension(Images[0].FileName);
            //var extension2 = Path.GetExtension(Images[1].FileName);
            //var extension3 = Path.GetExtension(Images[2].FileName);
            string fileName2 = "produit" + idpr + ".png";


            var path1 = Path.Combine(_webHostEnvironment.WebRootPath, "ImagesProduits", fileName2);
            //var path2= Path.Combine(_webHostEnvironment.WebRootPath, "ImagesProduits", fileName2);
            //var path3= Path.Combine(_webHostEnvironment.WebRootPath, "ImagesProduits", fileName3);

            using (var stream = new FileStream(path1, FileMode.Create))
            {
                Images[0].CopyTo(stream);

            }
            /*    using (var stream = new FileStream(path2, FileMode.Create))
                {
                    Images[1].CopyTo(stream);

                }
                using (var stream = new FileStream(path3, FileMode.Create))
                {
                    Images[2].CopyTo(stream);

                }




         /*       foreach (var Image in produit.Images)
                {
                    using (var stream = Image.OpenReadStream())
                    {
                        byte[] imageData;

                        using (var memoryStream = new MemoryStream())
                        {
                            stream.CopyToAsync(memoryStream);
                            imageData = memoryStream.ToArray();
                        }

                        Image image1 = new Image
                        {
                            IdImage = db.Images.Count() + 1
                        ,
                            IdProduit = idpr,

                            Image1 = imageData
                        };

                        db.Images.Add(image1);
                    }

                }

           */
            /* Image image25 =new Image()
             {
                 IdImage=db.Images.Count()+1,
                 Image1=path1,
                 IdProduit=idpr
             };
             db.Images.Add(image25);
          */
            db.Produits.Add(produit1);
            db.SaveChanges();

            return RedirectToAction("Accueil", "Proprietaire");
        }

        ///////////////////////////////////////////////////////////////////////////
        public IActionResult Accueil()
        {

            var userClaims = HttpContext.User.Claims;
            int id = Convert.ToInt32(userClaims.FirstOrDefault(c => c.Type == "Id").Value);


            ModelProp model = new ModelProp()
            {
                CountSales = db.Transactions.Where(t => t.IdProduitNavigation.IdProprietaire == id).Sum(t => t.PrixTransaction),
                CountProduits = db.Produits.Where(t => t.IdProprietaire == id).Count(),
                proprietaire = db.Proprietaires.Where(t => t.IdProprietaire == id).FirstOrDefault(),
                ListeTransactions = db.Transactions.Include(t => t.IdClientNavigation).Include(t => t.IdProduitNavigation)
                .Where(t => t.IdProduitNavigation.IdProprietaire == id).OrderByDescending(t => t.DateTransaction).ToList(),
                ListeTransactionsRecent = db.Transactions.Include(t => t.IdClientNavigation).Include(t => t.IdProduitNavigation)
                .Where(t => t.IdProduitNavigation.IdProprietaire == id).OrderByDescending(t => t.DateTransaction).Take(10).ToList(),
                ListeProduits = db.Produits.Where(t => t.IdProprietaire == id).ToList()
                ,
                ListeProduitsRecents = db.Produits.Where(t => t.IdProprietaire == id).ToList()
                ,
                Categories = db.Categories.ToList(),
                Scategories = db.Scategories.ToList(),
            };




            return View(model);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {

            await HttpContext.SignOutAsync("ProprietaireAuth");


            return RedirectToAction("ProprietaireAuth", "Proprietaire");
        }

        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpPost]
        public IActionResult UpdateValue(int id, int newValue)
        {
            // Find the entity by ID
            Produit produit = db.Produits.Find(id);


            // Update the value
            produit.Qte = newValue;

            db.Produits.Update(produit);

            db.SaveChanges();


            // You can return a success message or handle errors as needed
            return Json(new { success = true });
        }

    }
}
