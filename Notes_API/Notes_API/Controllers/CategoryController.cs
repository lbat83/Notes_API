using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Notes_API.Entities;

namespace Notes_API.Controllers
{
    
    [Route("api/Category")]
    public class CategoryController : Controller
    {
        private readonly NotesContext _context;

        public CategoryController(NotesContext context)
        {
            _context = context;
        }

        // GET: api/Category
        [HttpGet]
        public IEnumerable<Category> GetAll()
        {
            return _context.Category.ToList();
        }

        // GET: api/Category/5
        [HttpGet("{id}", Name = "GetCategory")]
        public IActionResult GetById(int id)
        {
            var item = _context.Category.FirstOrDefault(c => c.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        // POST: api/Category
        [HttpPost]
        public IActionResult Post([FromBody]Category item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            _context.Category.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetCategory", new { id = item.Id }, item);
        }

        // PUT: api/Category/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Category item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var cat = _context.Category.FirstOrDefault(c => c.Id == id);
            if (cat == null)
            {
                return NotFound();
            }

            cat.Name = item.Name;

            _context.Category.Update(cat);
            _context.SaveChanges();
            return new NoContentResult();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var cat = _context.Category.FirstOrDefault(t => t.Id == id);
            if (cat == null)
            {
                return NotFound();
            }

            _context.Category.Remove(cat);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}
