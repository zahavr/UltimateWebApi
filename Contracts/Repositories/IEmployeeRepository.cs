using Entities;
using System;
using System.Collections.Generic;

namespace Contracts.Repositories
{
	public interface IEmployeeRepository : IRepositoryBase<Employee>
    {
        IEnumerable<Employee> GetEmployees(Guid companyId, bool trackChanges);
        Employee GetEmployee(Guid companyId, Guid userId, bool trackChanges);
    }
}
