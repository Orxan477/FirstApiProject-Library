using FirstRestApi_Library_.DAL;
using FirstRestApi_Library_.DTOs.BookDto;
using FirstRestApi_Library_.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FirstRestApi_Library_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private AppDbContext _context;

        public BookController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var book = _context.Books.Where(b => b.IsDeleted == false && b.Id == id).FirstOrDefault();
            if (book == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { Erorcode = 1045, message = "Book Is Not Found" });
            }
            return Ok(book);
        }
        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            var books =await _context.Books.Where(b=>b.IsDeleted==false).ToListAsync();
            if (books == null)
            {
                return StatusCode(StatusCodes.Status404NotFound,new { Erorcode=1046, message="Not Found"});
            }
            return Ok(books);
        }
        [HttpPost()]
        public async Task<IActionResult> Create(CreateDto bookDto)
        {
            if (!ModelState.IsValid) return BadRequest();
            bool isExistContext = await _context.Books.AnyAsync(p => p.IsDeleted == false &&
                                                             p.Name.Trim().ToLower() == bookDto.Name.Trim().ToLower());

            if (isExistContext) return StatusCode(StatusCodes.Status400BadRequest, 
                                                            new { errorCode = 1044, message = "Multiple Name" });
            if (bookDto.Price == 0)
            {
                return StatusCode(StatusCodes.Status404NotFound,
                                                new { errorCode = 4440, message = "Price Is Not Found" });
            }
            Book newBook = new Book
            {
                Name=bookDto.Name,
                Price=bookDto.Price,
                DateTime=DateTime.Now
            };
            await _context.Books.AddAsync(newBook);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(UpdateDto bookDto,int id)
        {
            Book dbBook = await _context.Books.Where(b => b.IsDeleted == false && b.Id==id).FirstOrDefaultAsync();
            if (dbBook == null) return NotFound();
            if (bookDto.Name == null)
            {
                bookDto.Name = dbBook.Name;
            }
            bool isExistContext = await _context.Books.AnyAsync(p => p.IsDeleted == false &&
                                                             p.Name.Trim().ToLower() == bookDto.Name.Trim().ToLower());

            bool isExist = dbBook.Name.Trim().ToLower() == bookDto.Name.Trim().ToLower();
            if (isExistContext == true && isExist == false) 
            {
                return StatusCode(StatusCodes.Status409Conflict,new { errorCode=4045, Message="Multiple Name" });
            }
            if (!isExist)     dbBook.Name = bookDto.Name;

            

            if (bookDto.Price!=0)    dbBook.Price = bookDto.Price;

            _context.Books.Update(dbBook);
            await _context.SaveChangesAsync();
            return Ok(dbBook);
        }
        [HttpPatch("{id}")]
        //[HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Book dbBook = await _context.Books.Where(b => b.IsDeleted == false && b.Id == id).FirstOrDefaultAsync();
            if (dbBook == null) return StatusCode(StatusCodes.Status404NotFound,
                                                           new { errorCode = 4044, message = "Book Is Not Found" });
            dbBook.IsDeleted = true;
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
