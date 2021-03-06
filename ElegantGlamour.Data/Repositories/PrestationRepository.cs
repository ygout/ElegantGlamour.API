using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ElegantGlamour.Core.Models;
using ElegantGlamour.Core.Repositories;
using System;
using ElegantGlamour.Core.Specifications;

namespace ElegantGlamour.Data.Repositories
{
    public class PrestationRepository : Repository<Prestation>, IPrestationRepository
    {
        private ElegantGlamourDbContext ElegantGlamourDbContext
        {
            get { return _context as ElegantGlamourDbContext; }
        }
        public PrestationRepository(ElegantGlamourDbContext context) : base(context) { }

        public async Task<Prestation> GetPrestationByIdAsync(int id)
        {
            return await ElegantGlamourDbContext.Prestations
                   .Include(p => p.PrestationCategory)
                   .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IReadOnlyList<Prestation>> GetPrestationsAsync()
        {
            return await ElegantGlamourDbContext.Prestations
                            .Include(p => p.PrestationCategory)
                            .ToListAsync();

        }

        public async Task<IReadOnlyList<PrestationCategory>> GetPrestationCategoriesAsync(ISpecification<PrestationCategory> spec)
        {
            return await ApplySpecificationPrestationCategory(spec).ToListAsync();
        }

        public async Task<PrestationCategory> GetPrestationCategoryAsync(ISpecification<PrestationCategory> spec)
        {
            return await ApplySpecificationPrestationCategory(spec).FirstOrDefaultAsync();
        }
        public async Task<bool> IsPrestationCategoryExistAsync(ISpecification<PrestationCategory> spec)
        {
            return await ApplySpecificationPrestationCategory(spec).AnyAsync();
        }
        private IQueryable<PrestationCategory> ApplySpecificationPrestationCategory(ISpecification<PrestationCategory> spec)
        {
            return SpecificationEvaluator<PrestationCategory>.GetQuery(ElegantGlamourDbContext.PrestationCategories.AsQueryable(), spec);
        }

        public async Task AddPrestationCategoryAsync(PrestationCategory prestationCategory)
        {
            await ElegantGlamourDbContext.PrestationCategories.AddAsync(prestationCategory);
        }

        public void DeletePrestationCategory(PrestationCategory prestationCategory)
        {
            ElegantGlamourDbContext.PrestationCategories.Remove(prestationCategory);
        }
    }
}