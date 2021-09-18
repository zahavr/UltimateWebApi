using Contracts;
using Contracts.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace UltimateWebApi.Controllers
{
	[ApiController]
    [Route("api/{controller}")]
    public class TestController : ControllerBase
    {
		private readonly IRepositoryManager _repositoryManager;

		public TestController(IRepositoryManager repositoryManager)
		{
			_repositoryManager = repositoryManager;
		}

		[HttpGet]
		public IActionResult GetLogs()
		{
			var ss = _repositoryManager.Company.FindAll(false).ToList();

			return Ok(ss);
		}
    }
}
