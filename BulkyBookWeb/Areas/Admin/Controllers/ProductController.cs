using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View(_unitOfWork.Product.GetAll());
        }

        public IActionResult Upsert(int? id)
        {
            ProductVM productVM = new()
            {
                Product = new(),
                CategoryList = _unitOfWork.Category.GetAll().Select(
                    u => new SelectListItem()
                    {
                        Text = u.Name,
                        Value = u.Id.ToString()
                    }),
                CoverTypeList = _unitOfWork.CoverType.GetAll().Select(
                    u => new SelectListItem()
                    {
                        Text = u.Name,
                        Value = u.Id.ToString()
                    })
            };

            if (id > 0)
            {
                productVM.Product = _unitOfWork.Product.GetFirstOrDefault(new Product() { Id = (int)id });
            }

            return View(productVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductVM obj, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"images" + Path.DirectorySeparatorChar + "products");
                    var extension = Path.GetExtension(file.FileName);

                    if (!string.IsNullOrEmpty(obj.Product.ImageUrl))
                    {
                        string oldFilePath = Path.Combine(wwwRootPath, obj.Product.ImageUrl.TrimStart(Path.DirectorySeparatorChar));
                        if (System.IO.File.Exists(oldFilePath))
                        {
                            System.IO.File.Delete(oldFilePath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    obj.Product.ImageUrl = Path.DirectorySeparatorChar + "images" + Path.DirectorySeparatorChar + "products" + Path.DirectorySeparatorChar + fileName + extension;
                }

                if (obj.Product.Id != 0)
                {
                    _unitOfWork.Product.Update(obj.Product);
                }
                else
                {
                    _unitOfWork.Product.Add(obj.Product);
                }

                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult Delete(Product product)
        {
            if (product.Id == 0)
            {
                return NotFound();
            }

            product = _unitOfWork.Product.GetFirstOrDefault(product);
            if (!string.IsNullOrEmpty(product.ImageUrl))
            {
                string oldFilePath = Path.Combine(_webHostEnvironment.WebRootPath, product.ImageUrl.TrimStart(Path.DirectorySeparatorChar));
                if (System.IO.File.Exists(oldFilePath))
                {
                    System.IO.File.Delete(oldFilePath);
                }
            }
            
            _unitOfWork.Product.Remove(product);

            TempData["success"] = "삭제 완료!";
            return RedirectToAction("Index");
        }

        #region API Call
        [HttpGet]
        public IActionResult GetAll()
        {
            var products = _unitOfWork.Product.GetAll();
            return Json(new { data = products });
        }
        #endregion
    }
}