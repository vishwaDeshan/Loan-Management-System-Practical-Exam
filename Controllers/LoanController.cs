using LoanManagementSystemAssignment.Services;
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
	}
}
