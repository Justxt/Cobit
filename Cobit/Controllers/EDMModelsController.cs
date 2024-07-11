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
    public class EDMModelsController : Controller
    {
        private readonly CobitContext _context;

        public EDMModelsController(CobitContext context)
        {
            _context = context;
        }

        // OBTENER TODOS LOS DATOS
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EDMModel>>> Get()
        {
            return await _context.EDMModel.ToListAsync();
        }

        // OBTENER UN SOLO DATO POR ID
        [HttpGet("{id}")]
        public async Task<ActionResult<EDMModel>> GetEDMModel(int id)
        {
            var model = await _context.EDMModel.FindAsync(id);

            if (model == null)
            {
                return BadRequest("EDM No Encontrado");
            }

            return model;
        }

        // AGREGAR DATOS
        [HttpPost]
        public async Task<IActionResult> Create(EDMModel model)
        {
            if (ModelState.IsValid)
            {
                // Verificar si ya existe el CodeText en la base de datos
                if (_context.EDMModel.Any(m => m.CodeText == model.CodeText))
                {
                    return BadRequest("Ya existe un registro con este CodeText.");
                }

                _context.Add(model);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetEDMModel), new { id = model.Id }, model);
            }
            return BadRequest(ModelState);
        }

        // ACTUALIZAR DATOS
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, EDMModel model)
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
                if (!EDMModelExists(id))
                {
                    return BadRequest("EDM No Encontrado");
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
            var model = await _context.EDMModel.FindAsync(id);

            if (model == null)
            {
                return BadRequest("EDM No Encontrado");
            }

            _context.EDMModel.Remove(model);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EDMModelExists(int id)
        {
            return _context.EDMModel.Any(e => e.Id == id);
        }
    }
}