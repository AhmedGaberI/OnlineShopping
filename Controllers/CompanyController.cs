using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShopping.Data;
using OnlineShopping.Models;

namespace OnlineShopping.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly AllDbContext _context;
        public CompanyController(AllDbContext context) => _context = context;

         
        [HttpGet]
        public async Task<IEnumerable<Company>> Get_Companies()
        {
            return await _context.Companies.ToListAsync();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Company), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCompanyById(int id)
        {
            var company = await _context.Companies.FindAsync(id);
            return company == null ? NotFound() : Ok(company);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> AddCompany(Company company)
        {
            await _context.Companies.AddAsync(company);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCompanyById), new { id = company.CompanyId }, company);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> Update_Comapny(int id, Company cmp)
        {
            if (id != cmp.CompanyId) return BadRequest();
            _context.Entry(cmp).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> Delete(int id)
        {
            var companyTodelete = await _context.Companies.FindAsync(id);
            if (companyTodelete == null) return NotFound();
            _context.Companies.Remove(companyTodelete);

            await _context.SaveChangesAsync();
            return NoContent();
        }

        
    }
}
