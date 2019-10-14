using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProAgil.API.Dto;
using ProAgil.Domain;
using ProAgil.Repository;

namespace ProAgil.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PalestranteController : ControllerBase
    {
        public IProAgilRepository _repository { get; }
        public IMapper _mapper { get; }

        public PalestranteController(IProAgilRepository repository, IMapper mapper)
        {
            this._repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var results = await _repository.GetAllPalestrantesAsync(true);
                var palestrates = _mapper.Map<IEnumerable<PalestranteDTO>>(results);
                return Ok(palestrates); 
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
                var palestrate = _mapper.Map<PalestranteDTO>(results);
                return Ok(palestrate);
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
                var palestrates = _mapper.Map<IEnumerable<PalestranteDTO>>(results);
                return Ok(palestrates);
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
                var palestrates = _mapper.Map<IEnumerable<PalestranteDTO>>(results);
                return Ok(palestrates);
            }
            catch (System.Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(PalestranteDTO model)
        {
            try
            {
                var evento = _mapper.Map<Palestrante>(model);
                _repository.add(evento);

                if (await _repository.SaveChangesAsync())
                    return Created($"/api/palestrante/{evento.Id}", _mapper.Map<Palestrante>(model));
            }
            catch (System.Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
            }

            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Put(int Id, PalestranteDTO model)
        {
            try
            {
                var palestrante = await _repository.GetPalestranteAsyncById(Id, false);

                if (palestrante == null)
                {
                    return NotFound();
                }

                _mapper.Map(model, palestrante);
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