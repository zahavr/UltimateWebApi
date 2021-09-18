using Contracts.Repositories;
using Entities;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
	public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
		public EmployeeRepository(AppDbContext context) : base(context)
		{

		}

		public Employee GetEmployee(Guid companyId, Guid userId, bool trackChanges) =>
			FindByCondition(e => e.CompanyId.Equals(companyId) && e.Id.Equals(userId), trackChanges)
			.SingleOrDefault();

		public IEnumerable<Employee> GetEmployees(Guid companyId, bool trackChanges) => 
			FindByCondition(e => e.CompanyId.Equals(companyId), trackChanges)
			.OrderBy(x => x.Name);
	}
}
