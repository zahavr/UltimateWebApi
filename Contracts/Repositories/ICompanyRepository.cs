using Entities;
using System;
using System.Collections.Generic;

namespace Contracts.Repositories
{
	public interface ICompanyRepository : IRepositoryBase<Company>
	{
		IEnumerable<Company> GetAllCompanies(bool trackChanges);
		Company GetCompany(Guid id, bool trackChanges);
	}
}
