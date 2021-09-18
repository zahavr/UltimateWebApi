using Contracts.Repositories;
using Persistence;
using System;

namespace Repository
{
	public class RepositoryManager : IRepositoryManager
	{
		private readonly AppDbContext _context;
		private ICompanyRepository _companyRepository;
		private IEmployeeRepository _employeeRepository;

		public RepositoryManager(AppDbContext context)
		{
			_context = context;
		}

		public ICompanyRepository Company { get => _companyRepository ??= new CompanyRepository(_context); }

		public IEmployeeRepository Employee { get => _employeeRepository ??= new EmployeeRepository(_context); }

		public void Save() => _context.SaveChanges();
	}
}
