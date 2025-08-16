using LoanManagementSystemAssignment.Enums;
using LoanManagementSystemAssignment.Services;
using LoanManagementSystemAssignment.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LoanManagementSystemAssignment.Controllers
{
	public class LoanController : Controller
	{
		private readonly ILoanService _service;

		public LoanController(ILoanService service)
		{
			_service = service;
		}

		public async Task<IActionResult> Index()
		{
			var loans = await _service.GetAllAsync();
			return View(loans);
		}

		[HttpGet]
		public IActionResult Add()
		{
			ModelState.Clear();
			var model = new LoanApplicationViewModel
			{
				CustomerName = string.Empty,
				NicPassport = string.Empty,
				LoanType = LoanType.Personal,
				Status = LoanStatus.New,
				InterestRate = 5.0m
			};
			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Add(LoanApplicationViewModel model)
		{
			if (ModelState.IsValid)
			{
				try
				{
					await _service.AddAsync(model);
					return RedirectToAction("Index");
				}
				catch (ArgumentException ex)
				{
					ModelState.AddModelError("", ex.Message);
				}
			}
			return View(model);
		}

		public async Task<IActionResult> Details(int id)
		{
			try
			{
				var loan = await _service.GetByIdAsync(id);
				return View(loan);
			}
			catch (ArgumentException ex)
			{
				return NotFound(ex.Message);
			}
		}

		[HttpPost]
		public async Task<IActionResult> EditStatus(int id, string status)
		{
			try
			{
				await _service.UpdateStatusAsync(id, status);
				return RedirectToAction("Index");
			}
			catch (ArgumentException ex)
			{
				ModelState.AddModelError("", ex.Message);
				var loans = await _service.GetAllAsync();
				return View("Index", loans);
			}
		}
	}
}
