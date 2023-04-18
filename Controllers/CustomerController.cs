using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShopping.Data;
using OnlineShopping.Models;

namespace OnlineShopping.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly AllDbContext _context;         
        public CustomerController(AllDbContext context) => _context = context;

        //get all Customers 
        [HttpGet] 
        public async Task<IEnumerable<Customer>> Get_Customers()
        {
            return await _context.Customers.ToListAsync();
        }
        // get Customer By id
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Customer), StatusCodes.Status200OK)] 
        [ProducesResponseType(StatusCodes.Status404NotFound)] 
        public async Task<IActionResult> GetCustomerById(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            return customer == null ? NotFound() : Ok(customer);
        }


        // Add Customer To database 
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> AddCustomer(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCustomerById), new { id = customer.CustomerId }, customer);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> Update_Customer(int id, Customer customer)
        {
            if (id != customer.CustomerId) return BadRequest();
            _context.Entry(customer).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent(); 
        }

        // Delete Customer From database 
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> Delete(int id)
        {
            var customerTodelete = await _context.Customers.FindAsync(id);
            if (customerTodelete == null) return NotFound();
            _context.Customers.Remove(customerTodelete);

            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
