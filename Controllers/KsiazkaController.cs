using AdvancedProgramming_Lesson3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvancedProgramming_Lesson3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KsiazkaItemsController : ControllerBase
    {
        private readonly KsiazkaContext _context;

        public KsiazkaItemsController(KsiazkaContext context)
        {
            _context = context;
        }

        // GET: api/KsiazkaItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<KsiazkaItemDTO>>> GetKsiazkaItems()
        {
            return await _context.KsiazkaItems
                .Select(x => ItemToDTO(x))
                .ToListAsync();
        }

        // GET: api/KsiazkaItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<KsiazkaItemDTO>> GetKsiazkaItem(long id)
        {
            var KsiazkaItem = await _context.KsiazkaItems.FindAsync(id);
            if (KsiazkaItem == null)
            {
                return NotFound();
            }

            return ItemToDTO(KsiazkaItem);
        }

        [HttpPost]
        [Route("UpdateKsiazkaItem")]
        public async Task<ActionResult<KsiazkaItemDTO>> UpdateKsiazkaItem(KsiazkaItemDTO KsiazkaItemDTO)
        {
            var KsiazkaItem = await _context.KsiazkaItems.FindAsync(KsiazkaItemDTO.Id);
            if (KsiazkaItem == null)
            {
                return NotFound();
            }
            KsiazkaItem.Bookname = KsiazkaItemDTO.Bookname;
            KsiazkaItem.Author = KsiazkaItemDTO.Author;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!KsiazkaItemExists(KsiazkaItemDTO.Id))
            {
                return NotFound();
            }

            return CreatedAtAction(
                nameof(UpdateKsiazkaItem),
                new { id = KsiazkaItem.Id },
                ItemToDTO(KsiazkaItem));
        }

        [HttpPost]
        [Route("CreateKsiazkaItem")]
        public async Task<ActionResult<KsiazkaItemDTO>> CreateKsiazkaItem(KsiazkaItemDTO KsiazkaItemDTO)
        {
            var KsiazkaItem = new KsiazkaItem
            {
                Bookname = KsiazkaItemDTO.Bookname,
                Author = KsiazkaItemDTO.Author,
            };

            _context.KsiazkaItems.Add(KsiazkaItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetKsiazkaItem),
                new { id = KsiazkaItem.Id },
                ItemToDTO(KsiazkaItem));
        }

        // DELETE: api/KsiazkaItems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<KsiazkaItem>> DeleteKsiazkaItem(long id)
        {
            var KsiazkaItem = await _context.KsiazkaItems.FindAsync(id);
            if (KsiazkaItem == null)
            {
                return NotFound();
            }
            _context.KsiazkaItems.Remove(KsiazkaItem);
            await _context.SaveChangesAsync();
            return NoContent();
        }


        private bool KsiazkaItemExists(long id) =>
            _context.KsiazkaItems.Any(e => e.Id == id);

        private static KsiazkaItemDTO ItemToDTO(KsiazkaItem KsiazkaItem) =>
            new KsiazkaItemDTO
            {
                Id = KsiazkaItem.Id,
                Bookname = KsiazkaItem.Bookname,
                Author = KsiazkaItem.Author,
            };
    }
}
