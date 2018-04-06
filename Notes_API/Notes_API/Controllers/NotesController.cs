using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Notes_API.Entities;

namespace Notes_API.Controllers
{
    [Produces("application/json")]
    [Route("api/Notes")]
    public class NotesController : Controller
    {
        private readonly NotesContext _context;

        public NotesController(NotesContext context)
        {
            _context = context;
        }
        // GET: api/Notes
        [HttpGet]
        public IEnumerable<Notes> GetAll()
        {
            return _context.Notes.ToList();
        }

        // GET: api/Notes/5
        [HttpGet("{id}", Name = "GetNotes")]
        public IActionResult GetById(int id)
        {
            var item = _context.Notes.FirstOrDefault(n => n.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        // POST: api/Notes
        [HttpPost]
        public IActionResult Post([FromBody] Notes item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            _context.Notes.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetNotes", new { id = item.Id }, item);
        }

        // PUT: api/Notes/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Notes item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var note = _context.Notes.FirstOrDefault(n => n.Id == id);
            if (note == null)
            {
                return NotFound();
            }

            note.Title = item.Title;
            note.Note = item.Note;
            note.CreatedOn = item.CreatedOn;
            note.CategoryId = item.CategoryId;
            note.IsDeleted = item.IsDeleted;
            note.UserId = item.UserId;

            _context.Notes.Update(note);
            _context.SaveChanges();
            return new NoContentResult();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var note = _context.Notes.FirstOrDefault(n => n.Id == id);
            if (note == null)
            {
                return NotFound();
            }

            _context.Notes.Remove(note);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}
