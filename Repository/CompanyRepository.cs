using Contracts.Repositories;
using Entities;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
	public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
    {
		public CompanyRepository(AppDbContext context) : base(context)
		{

		}

		public IEnumerable<Company> GetAllCompanies(bool trackChanges) => 
			FindAll(trackChanges)
			.OrderBy(c => c.Name)
			.ToList();

		public Company GetCompany(Guid id, bool trackChanges) =>
			FindByCondition(x => x.Id.Equals(id), trackChanges)
			.SingleOrDefault();

	}
}
