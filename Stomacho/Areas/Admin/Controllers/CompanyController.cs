using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Stomacho.DataAccess.Repository.IRepository;
using Stomacho.Models;
using Stomacho.Models.ViewModels;
using Stomacho.Utility;

namespace Stomacho.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = SD.Role_Admin)]
public class CompanyController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public CompanyController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
        List<Company> objCompanyList = _unitOfWork.Company.GetAll().ToList();
        
        return View(objCompanyList);
    }

    public IActionResult Upsert(int? id)
    {
        if(id == null || id == 0)
        {
            //create
            return View(new Company());
        }
        else
        {
            //update
            Company companyObj = _unitOfWork.Company.Get(u => u.Id == id);
            return View(companyObj);
        }
    }

    [HttpPost]
    public IActionResult Upsert(Company CompanyObj)
    {
        if (ModelState.IsValid)
        {
            if (CompanyObj.Id == 0)
            {
                _unitOfWork.Company.Add(CompanyObj);
            }
            else
            {
                _unitOfWork.Company.Update(CompanyObj);
            }

            _unitOfWork.Save();
            TempData["success"] = "Company created successfully";
            return RedirectToAction("Index");
        }
        else
        {
            return View(CompanyObj);
        }
    }

    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        Company? companyFromDb = _unitOfWork.Company.Get(u => u.Id == id);
        if (companyFromDb == null)
        {
            return NotFound(); 
        }
        return View(companyFromDb);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeletePOST(int? id)
    {
        Company? obj = _unitOfWork.Company.Get(u => u.Id == id);

        if (obj == null)
        {
            return NotFound();
        }
        
        _unitOfWork.Company.Remove(obj);
        _unitOfWork.Save();
        TempData["success"] = "Company deleted successfully";
        return RedirectToAction("Index");
    }
}