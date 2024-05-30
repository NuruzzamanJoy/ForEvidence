using ForEvidence.Models;
using ForEvidence.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using static ForEvidence.Models.ViewModels.VmStore;

namespace ForEvidence.Controllers
{
    public class StoreController : Controller
    {
        private IWebHostEnvironment _hostingEnvironment;
        private readonly TestContext008 _db;
        public StoreController(TestContext008 db, IWebHostEnvironment hostingEnvironment)
        {
            _db = db;
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpGet]
        public IActionResult Single(int? id)
        {
            ViewData["TypeID"] = new SelectList(_db.Types, "TypeID", "TName");
            #region Normal Linq Query
            VmStore oSale = new VmStore();
            var oSM = _db.StoreMasters.Where(x => x.StoreID == id).FirstOrDefault();
            if (oSM != null)
            {
                oSale.StoreID = oSM.StoreID;
                oSale.StoreName = oSM.StoreName;
                oSale.StoreType = oSM.StoreType;
                oSale.SellDate = oSM.SellDate;
                oSale.TotalDue = oSM.TotalDue;
                var listDetail = new List<VmStore.VmD>();
                var oSD = _db.BookDetails.Where(x => x.StoreID == id).ToList();
                foreach (var item in oSD)
                {
                    var oSaleDetial = new VmStore.VmD();
                    oSaleDetial.BookID = item.BookID;
                    oSaleDetial.Title = item.Title;
                    listDetail.Add(oSaleDetial);
                }
                oSale.details = listDetail;
            }
            oSale = oSale == null ? new VmStore() : oSale;
            #endregion
            #region Lazy Loading
            //var listSM = _db.StoreMasters.ToList();
            #endregion
            #region Eager Loading
            var listSM = _db.StoreMasters.Include(m => m.bookDetails).ToList();
            #endregion
            #region Explicit Loading
            //var listSM = _db.StoreMasters.ToList();
            //foreach (var m in listSM)
            //{
            //    _db.Entry(m).Collection(c => c.bookDetails).Load(); // explicit loading syntax
            //}
            #endregion
            ViewData["list"] = listSM;
            #region Aggregate Function
            if (_db.StoreMasters.Count() > 0)
            {
                ViewData["Sum"] = _db.StoreMasters.Sum(x => x.TotalDue);
                ViewData["Average"] = _db.StoreMasters.Average(x => x.TotalDue);
                ViewData["Max"] = _db.StoreMasters.Max(x => x.TotalDue);
                ViewData["Min"] = _db.StoreMasters.Min(x => x.TotalDue);
                ViewData["count"] = _db.StoreMasters.Count();
            }
            #endregion
            return View(oSale);
        }

        [HttpPost]
        public IActionResult Single(VmStore model, IFormFile[] ImageFile)
        {
            var oSalemaster = _db.StoreMasters.Find(model.StoreID);
            if (oSalemaster == null)
            {
                string wwwRootPath = _hostingEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(ImageFile[0].FileName);
                string extension = Path.GetExtension(ImageFile[0].FileName);
                string fileWithExtension = fileName + extension;
                oSalemaster = new StoreMaster();
                oSalemaster.StoreName = model.StoreName;
                oSalemaster.StoreID = model.StoreID;
                oSalemaster.SellDate = model.SellDate;
                oSalemaster.StoreType = model.StoreType;
                oSalemaster.TotalDue = model.TotalDue;  
                oSalemaster.ImageName = fileWithExtension;
                string serverPath = Path.Combine(wwwRootPath + "/Images/" + fileName + extension);
                using (var fileStream = new FileStream(serverPath, FileMode.Create))
                {
                    ImageFile[0].CopyToAsync(fileStream);
                }
                _db.StoreMasters.Add(oSalemaster);
            }
            else
            {
                string wwwRootPath = _hostingEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(ImageFile[0].FileName);
                string extension = Path.GetExtension(ImageFile[0].FileName);
                string fileWithExtension = fileName + extension;
                oSalemaster.StoreName = model.StoreName;
                oSalemaster.StoreID = model.StoreID;
                oSalemaster.SellDate = model.SellDate;
                oSalemaster.StoreType = model.StoreType;
                oSalemaster.TotalDue = model.TotalDue;
                oSalemaster.TypeID = model.TypeID;
                oSalemaster.ImageName = fileWithExtension;
                string serverPath = Path.Combine(wwwRootPath + "/Images/" + fileName + extension);
                using (var fileStream = new FileStream(serverPath, FileMode.Create))
                {
                    ImageFile[0].CopyToAsync(fileStream);
                }
                var listRem = _db.BookDetails.Where(x => x.StoreID == oSalemaster.StoreID).ToList();
                _db.BookDetails.RemoveRange(listRem);
            }
            _db.SaveChanges();
            var listDetail = new List<BookDetail>();
            for (var i = 0; i < model.Title.Length; i++)
            {
                if (!string.IsNullOrEmpty(model.Title[i]))
                {
                    var oSaleDetial = new BookDetail();
                    oSaleDetial.StoreID = oSalemaster.StoreID;
                    oSaleDetial.Title = model.Title[i];
                    listDetail.Add(oSaleDetial);
                }
            }
            ViewData["TypeID"] = new SelectList(_db.StoreMasters, "TypeID", "TName", oSalemaster.TypeID);
            _db.BookDetails.AddRange(listDetail);
            _db.SaveChanges();

            return RedirectToAction("Single");
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var oSaleMaster = (from o in _db.StoreMasters where o.StoreID == id select o).FirstOrDefault();
            var oDetail = (from o in _db.BookDetails where o.StoreID == id select o).ToList();
            if (oSaleMaster != null)
            {
                _db.StoreMasters.Remove(oSaleMaster);
                _db.BookDetails.RemoveRange(oDetail);
                _db.SaveChanges();
            }
            return RedirectToAction("Single");
        }
        public async Task<IActionResult> Create(BType bType)
        {
            if (ModelState.IsValid)
            {
                _db.Add(bType);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Single));
            }
            return View(bType);
        }
    }
}
