using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProAgil.Domain;
using ProAgil.Repository;

namespace ProAgil.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventoController : ControllerBase
    {
        public IProAgilRepository _Repository { get; }
        public EventoController(IProAgilRepository repository)
        {
            _Repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var results = await _Repository.GetAllEventoAsync(true);
                return Ok(results);
            }
            catch (System.Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
            }

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var results = await _Repository.GetEventoAsyncById(id, true);
                return Ok(results);
            }
            catch (System.Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
            }
        }

        [HttpGet("GetByTema/{Tema}")]
        public async Task<IActionResult> GetByTema(string tema)
        {
            try
            {
                var results = await _Repository.GetAllEventoAsyncByTema(tema, true);
                return Ok(results);
            }
            catch (System.Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Evento model)
        {
            try
            {
                _Repository.add(model);
                if (await _Repository.SaveChangesAsync())
                    return Created($"/api/evento/{model.Id}", model);
            }
            catch (System.Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
            }

            return BadRequest();
        }

        [HttpPut("{EventoId}")]
        public async Task<IActionResult> Put(int EventoId,Evento model)
        {
            try
            {
                var evento = await _Repository.GetEventoAsyncById(EventoId,false);

                if(evento == null)
                {
                    return NotFound();
                } 
               _Repository.Update(model);
               if(await _Repository.SaveChangesAsync())
                return Created($"/api/evento/{model.Id}",model);
            }
            catch (System.Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var evento = await _Repository.GetEventoAsyncById(id,false);

                if(evento == null)
                {
                    return NotFound();
                } 
               _Repository.Delete(evento);
               if(await _Repository.SaveChangesAsync())
                return Ok();
            }
            catch (System.Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
            }

            return BadRequest();
        }
        
    }
}