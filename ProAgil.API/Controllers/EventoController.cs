using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
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
            catch (System.Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou "+ex.Message);
            }

        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload()
        {
            try
            {
                var File = Request.Form.Files[0];
                var FolderName = Path.Combine("Resources", "Images");
                var PathToSave = Path.Combine(Directory.GetCurrentDirectory(), FolderName);

                if (File.Length > 0)
                {
                    var FileName = ContentDispositionHeaderValue.Parse(File.ContentDisposition).FileName;
                    var FullPath = Path.Combine(PathToSave, FileName.Replace("\"", " ").Trim());

                    using (var stream = new FileStream(FullPath,FileMode.Create))
                    {
                        File.CopyTo(stream);
                    }
                }
                return Ok();
            }
            catch (System.Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou " + ex.Message);
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
        public async Task<IActionResult> Post(EventoDTO model)
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
        public async Task<IActionResult> Put(int EventoId,EventoDTO model)
        {
            try
            {

                var idLotes = new List<int>();
                var idRedesSociais = new List<int>();

                foreach (var item in model.Lotes)
                {
                    idLotes.Add(item.Id);
                }

                foreach (var item in model.redesociais)
                {
                    idRedesSociais.Add(item.Id);
                }

                var evento = await _Repository.GetEventoAsyncById(EventoId,false);


                if(evento == null)
                {
                    return NotFound();
                }

                var lotes = evento.Lotes.Where(Lote => !idLotes.Contains(Lote.Id)).ToList<Lote>();
                var redes = evento.redesociais.Where(rede => !idRedesSociais.Contains(rede.Id)).ToList<RedeSocial>();

                if (lotes.Count>0)
                {
                    lotes.ForEach(lote => _Repository.Delete(lote));
                }
                if (redes.Count > 0)
                {
                    redes.ForEach(rede => _Repository.Delete(rede));
                }

                var result = _mapper.Map(model,evento);

               _Repository.Update(result);
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