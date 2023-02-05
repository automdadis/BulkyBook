﻿using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvinroment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment= hostEnvinroment;
        }

        public IActionResult Index()
        {
            return View();
        }



        //Get 
        public IActionResult Upsert(int? id)
        {
            //Dropdowns Πρεπει να τα περάσουμε στο view 

            ProductViewModel productViewModel = new()
            {
                Product = new(),
                CategoryList = _unitOfWork.Category.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                CoverTypeList = _unitOfWork.CoverType.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),

            };

            if (id == null || id == 0)
            {
                //create product
                //ViewBag.CategoryList = CategoryList;
                //iewData["CoverTypeList"] = CoverTypeList;
                return View(productViewModel);
            }
            else
            {
                productViewModel.Product = _unitOfWork.Product.GetFirstotDefault(u=>u.Id==id);
                return View(productViewModel);
            }
            
        }

        //post 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductViewModel obj, IFormFile? file) 
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"images\products");
                    var extension = Path.GetExtension(file.FileName);
                    if (obj.Product.ImageUrl != null)
                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    ;obj.Product.ImageUrl=@"\images\products" + fileName + extension;

                }
                if (obj.Product.Id==0)
                _unitOfWork.Save();
                TempData["success"] = "Product updated succesfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }


        #region API CALLS
        [HttpGet]

        public IActionResult GetAll()
        {
            var productList = _unitOfWork.Product.GetAll(includeProperties: "Category,CoverType");
            return Json(new {data = productList});
        }
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var obj = _unitOfWork.Product.GetFirstotDefault(u => u.Id == id);
            if (obj == null)
            {
                return Json(new {success =false, message ="Error while deleting"});
            }
            var oldImagePath = Path.Combine(_hostEnvironment.WebRootPath, obj.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }
            _unitOfWork.Product.Remove(obj);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Succesful" });
        }
        #endregion
    }
}
