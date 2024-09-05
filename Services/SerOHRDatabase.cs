using Microsoft.EntityFrameworkCore;
using TelerikUI.Data;
using TelerikUI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TelerikUI.Services
{
    public class SerOHRDatabase
    {
        private readonly AppDBContext _dbContext;

        // Constructor for dependency injection
        public SerOHRDatabase(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Get Record by ID
        public async Task<ModtblGenerator> GetGeneratorByIdAsync(int id)
        {
            return await _dbContext.Generators.FindAsync(id);
        }

        // Get All Records that start with specific characters
        public async Task<List<ModtblGenerator>> GetMultipleGeneratorAsync(string firstCharacters)
        {
            return await _dbContext.Generators
                                   .Where(g => g.GenName.StartsWith(firstCharacters))
                                   .ToListAsync();
        }

        public async Task GeneratorDeleteByIDAsync(int id)
        {
            var generator = await _dbContext.Generators.FindAsync(id);
            if (generator != null)
            {
                _dbContext.Generators.Remove(generator);
                await _dbContext.SaveChangesAsync();
            }

        }

        // Get All Waste Classes Records
        public async Task<List<ModvGeneratorWasteClass>> GetGeneratorWasteClassesAsync(int WasteNum)
        {
            return await _dbContext.GeneratorWasteClasses
                .Where(g => g.WasteNum == WasteNum)
                .ToListAsync();

        }

        // Get All Generators
        public async Task<List<ModtblGenerator>> GetAllGeneratorsAsync()
        {
            return await _dbContext.Generators
                                     .Take(1000)
                                     .ToListAsync();
        }
    }
}
