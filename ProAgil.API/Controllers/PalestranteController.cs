using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProAgil.Domain;
using ProAgil.Repository;

namespace ProAgil.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PalestranteController : ControllerBase
    {
        public IProAgilRepository _repository { get; }

        public PalestranteController(IProAgilRepository repository)
        {
            this._repository = repository;

        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var results = await _repository.GetAllPalestrantesAsync(true);
                return Ok(results); 
            }
            catch (System.Exception)
            {
                
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
            }
            
        }

         [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var results = await _repository.GetPalestranteAsyncById(id,true);
                return Ok(results);
            }
            catch (System.Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
            }
        }

        [HttpGet("nome/{Name}")]
        public async Task<IActionResult> GetByName(string Name)
        {
            try
            {
                var results = await _repository.GetAllPalestrantesAsyncByName(Name,true);
                return Ok(results);
            }
            catch (System.Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
            }
        }

        [HttpGet("tema/{Tema}")]
        public async Task<IActionResult> GetByTema(string Name)
        {
            try
            {
                var results = await _repository.GetAllPalestrantesAsyncByName(Name,true);
                return Ok(results);
            }
            catch (System.Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Palestrante model)
        {
            try
            {
                _repository.add(model);
                if (await _repository.SaveChangesAsync())
                    return Created($"/api/palestrante/{model.Id}", model);
            }
            catch (System.Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
            }

            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Put(int Id, Palestrante model)
        {
            try
            {
                var palestrante = await _repository.GetPalestranteAsyncById(Id, false);

                if (palestrante == null)
                {
                    return NotFound();
                }
                _repository.Update(model);
                if (await _repository.SaveChangesAsync())
                    return Created($"/api/palestrante/{model.Id}", model);
            }
            catch (System.Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
            }

            return BadRequest();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int Id)
        {
            try
            {
                var palestrante = await _repository.GetPalestranteAsyncById(Id, false);

                if (palestrante == null)
                {
                    return NotFound();
                }
                _repository.Delete(palestrante);
                if (await _repository.SaveChangesAsync())
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