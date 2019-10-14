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
    public class EventoController : ControllerBase
    {
        public IProAgilRepository _Repository { get; }
        public IMapper _mapper { get; }

        public EventoController(IProAgilRepository repository , IMapper mapper)
        {
            _Repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var results = await _Repository.GetAllEventoAsync(true);

                var eventos = _mapper.Map<IEnumerable<EventoDTO>>(results);

                return Ok(eventos);
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
                var evento = _mapper.Map<EventoDTO>(results);
                return Ok(evento);
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
                var eventos = _mapper.Map<IEnumerable<EventoDTO>>(results);
                return Ok(eventos);
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
                var evento = _mapper.Map<Evento>(model);
                _Repository.add(evento);

                if (await _Repository.SaveChangesAsync())
                    return Created($"/api/evento/{evento.Id}", _mapper.Map<Evento>(model));
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

                _mapper.Map(model,evento);

               _Repository.Update(model);
               if(await _Repository.SaveChangesAsync())
                return Created($"/api/evento/{model.Id}", _mapper.Map(model, evento));
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