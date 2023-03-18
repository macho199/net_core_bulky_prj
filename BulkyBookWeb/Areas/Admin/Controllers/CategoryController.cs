using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using BulkyBook.Models;
using Npgsql;
using Dapper;
using BulkyBook.DataAccess.Repository.IRepository;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly string? _dcs;
        //private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View(_unitOfWork.Category.GetAll());
        }

        // Get
        public IActionResult Create()
        {
            return View();
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("index");
            }

            TempData["success"] = "Success Create";
            _unitOfWork.Category.Add(category);

            return RedirectToAction("index");
        }

        // Get
        public IActionResult Edit(int? id)
        {
            if (id == null || id <= 0)
            {
                return NotFound();
            }

            return View(_unitOfWork.Category.GetFirstOrDefault(x => x.Id.Equals(id)));
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("index");
            }

            TempData["success"] = "Success Edit";
            _unitOfWork.Category.Update(category);

            return RedirectToAction("index");
        }

        public IActionResult Delete(Category category)
        {
            _unitOfWork.Category.Remove(category);
            TempData["success"] = "Success Delete";
            return RedirectToAction("index");
        }
    }
}

