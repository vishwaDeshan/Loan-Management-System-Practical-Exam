using LoanManagementSystemAssignment.Services;
using LoanManagementSystemAssignment.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LoanManagementSystemAssignment.Controllers
{
	[Authorize]
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
				NicPassport = string.Empty
			};
			return View(model);
		}

		[HttpGet]
		public async Task<IActionResult> AddOrEdit(int? id)
		{
			ModelState.Clear();
			LoanApplicationViewModel model;

			if (id == null)
			{
				model = new LoanApplicationViewModel
				{
					CustomerName = string.Empty,
					NicPassport = string.Empty
				};
			}
			else
			{
				var loan = await _service.GetByIdAsync(id.Value);
				model = new LoanApplicationViewModel
				{
					Id = loan.Id,
					CustomerName = loan.CustomerName,
					NicPassport = loan.NicPassport,
					LoanType = loan.LoanType,
					InterestRate = loan.InterestRate,
					LoanAmount = loan.LoanAmount,
					DurationMonths = loan.DurationMonths,
					Status = loan.Status
				};
			}

			ViewBag.IsEdit = id != null;
			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> AddOrEdit(LoanApplicationViewModel model)
		{
			if (ModelState.IsValid)
			{
				try
				{
					if (model.Id == 0)
					{
						await _service.AddAsync(model);
					}
					else
					{
						await _service.UpdateAsync(model);
					}
					return RedirectToAction("Index");
				}
				catch (ArgumentException ex)
				{
					ModelState.AddModelError("", ex.Message);
				}
			}

			ViewBag.IsEdit = model.Id != 0;
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
