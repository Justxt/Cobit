using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cobit.Data;
using Cobit.Models;

namespace Cobit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AGModelsController : Controller
    {
        private readonly CobitContext _context;

        public AGModelsController(CobitContext context)
        {
            _context = context;
        }

        // OBTENER TODOS LOS DATOS
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AGModel>>> Get()
        {
            return await _context.AGModels.ToListAsync();
        }

        // OBTENER UN SOLO DATO POR ID
        [HttpGet("{id}")]
        public async Task<ActionResult<AGModel>> GetAGModel(int id)
        {
            var model = await _context.AGModels.FindAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            return model;
        }

        // AGREGAR DATOS
        [HttpPost]
        public async Task<IActionResult> Create(AGModel model)
        {
            if (ModelState.IsValid)
            {
                // Verificar si ya existe el CodeText en la base de datos
                if (_context.AGModels.Any(m => m.CodeText == model.CodeText))
                {
                    return BadRequest("Ya existe un registro con este CodeText.");
                }

                _context.Add(model);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetAGModel), new { id = model.Id }, model);
            }
            return BadRequest(ModelState);
        }

        // ACTUALIZAR DATOS
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, AGModel model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            _context.Entry(model).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AGModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // ELIMINAR DATOS
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _context.AGModels.FindAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            _context.AGModels.Remove(model);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AGModelExists(int id)
        {
            return _context.AGModels.Any(e => e.Id == id);
        }
    }
}