using AutoMapper;
using Contracts;
using Contracts.Repositories;
using Entities;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace UltimateWebApi.Controllers
{
	[ApiController]
    [Route("api/companies/{companyId}/employess")]
    public class EmployessController : ControllerBase
    {
		private readonly IRepositoryManager _repositoryManager;
		private readonly ILoggerManager _loggerManager;
		private readonly IMapper _mapper;

		public EmployessController(
			IRepositoryManager repositoryManager,
			ILoggerManager loggerManager,
			IMapper mapper)
		{
			_repositoryManager = repositoryManager;
			_loggerManager = loggerManager;
			_mapper = mapper;
		}

		[HttpGet]
		public IActionResult GetEmployeesForCompany(Guid companyId)
		{
			Company company = _repositoryManager.Company.GetCompany(companyId, false);

			if (company == null)
			{
				_loggerManager.LogError($"Company with id {companyId} doesn`t exist in the database.");
				return NotFound();
			}

			IEnumerable<Employee> employees = _repositoryManager.Employee.GetEmployees(companyId, false);
			IEnumerable<EmployeeDto> employeeDtos = _mapper.Map<IEnumerable<EmployeeDto>>(employees);
			return Ok(employeeDtos);
		}

		[HttpGet("{userId}")]
		public IActionResult GetEmployeeForCompany(Guid companyId, Guid userId)
		{
			Company company = _repositoryManager.Company.GetCompany(companyId, false);

			if (company == null)
			{
				_loggerManager.LogError($"Company with id {companyId} doesn`t exist in the database.");
				return NotFound();
			}

			Employee employee = _repositoryManager.Employee.GetEmployee(companyId, userId, false);

			if (employee == null)
			{
				_loggerManager.LogError($"User with id {userId} doesn`t exist in the database.");
				return NotFound();
			}

			return Ok(_mapper.Map<EmployeeDto>(employee));
		}
    }
}
