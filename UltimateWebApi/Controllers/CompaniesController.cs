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
	[Route("api/[controller]")]
	[ApiController]
	public class CompaniesController : ControllerBase
	{
		private readonly IRepositoryManager _repositoryManager;
		private readonly ILoggerManager _loggerManager;
		private readonly IMapper _mapper;

		public CompaniesController(
			IRepositoryManager repositoryManager, 
			ILoggerManager loggerManager, 
			IMapper mapper)
		{
			_repositoryManager = repositoryManager;
			_loggerManager = loggerManager;
			_mapper = mapper;
		}

		[HttpGet]
		public IActionResult GetCompanies()
		{
				IEnumerable<Company> companies = _repositoryManager.Company.GetAllCompanies(false);

				IEnumerable<CompanyDto> companyDtos = _mapper.Map<IEnumerable<CompanyDto>>(companies);

				return Ok(companyDtos);
		}

		[HttpGet("{id:guid}")]
		public IActionResult GetCompany(Guid id)
		{
			Company company = _repositoryManager.Company.GetCompany(id, false);

			if (company == null)
			{
				_loggerManager.LogError($"Company with id: {id} doesn`t exist in database.");
				return NotFound();
			} 
			
			return Ok(_mapper.Map<CompanyDto>(company));		
		}
	}
}
