using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProAgil.Domain;

namespace ProAgil.Repository
{
    public class ProAgilRepository : IProAgilRepository
    {
        public ProAgilContext _context { get; }

        public ProAgilRepository(ProAgilContext context)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        // CRUD
        public void add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }
        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }
        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }
       
        public async Task<bool> SaveChangesAsync()
        {
           return (await _context.SaveChangesAsync()) > 0 ;
        }

        //GET EVENTOS
        public async Task<Evento[]> GetAllEventoAsync(bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos
               .Include(c => c.Lote)
               .Include(c => c.RedeSocial);

            if (includePalestrantes == true)
            {
                query = query.Include(pe => pe.PalestrantesEvento)
                .ThenInclude(p => p.Palestrante);
            }

            query = query.OrderByDescending(c => c.DataEvento);

            return await query.ToArrayAsync();
        }

        public async Task<Evento[]> GetAllEventoAsyncByTema(string tema, bool includePalestrantes)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(c => c.Lote)
                .Include(c => c.RedeSocial);

            if (includePalestrantes == true)
            {
                query = query.Include(pe => pe.PalestrantesEvento)
                .ThenInclude(p => p.Palestrante);
            }

            query = query.OrderByDescending(c => c.DataEvento).Where(x => x.Tema.ToLower().Contains(tema.ToLower()));

            return await query.ToArrayAsync();
        }


        public async Task<Evento> GetEventoAsyncById(int EventoId, bool includePalestrantes)
        {
           IQueryable<Evento> query = _context.Eventos
                .Include(c => c.Lote)
                .Include(c => c.RedeSocial);

            if (includePalestrantes == true)
            {
                query = query.Include(pe => pe.PalestrantesEvento)
                .ThenInclude(p => p.Palestrante);
            }

            query = query.OrderByDescending(c => c.DataEvento).Where(x => x.Id == EventoId);

            return await query.FirstOrDefaultAsync();
        }


        //Palestrantes
        public async Task<Palestrante> GetPalestranteAsyncById(int PalestranteId, bool includeEventos = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes.Include(x => x.RedeSocial);

            if (includeEventos == true)
            {
                query = query.Include(pe => pe.PalestrantesEventos)
                .ThenInclude(p => p.Evento);
            }

            query = query.OrderBy(c => c.Nome).Where(x => x.Id == PalestranteId);


            return await query.FirstOrDefaultAsync();

            
        }

        public async Task<Palestrante[]> GetAllPalestrantesAsync(bool includeEventos = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
                .Include(x => x.RedeSocial);

            if (includeEventos == true)
            {
                query = query.Include(pe => pe.PalestrantesEventos)
                .ThenInclude(p => p.Evento);
            }

            query = query.OrderBy(c => c.Nome);

            return await query.ToArrayAsync();

        }

        public async Task<Palestrante[]> GetAllPalestrantesAsyncByName(string Palestrantename, bool includeEventos)
        {
            IQueryable<Palestrante> query = _context.Palestrantes.Include(x => x.RedeSocial);

            if (includeEventos == true)
            {
                query = query.Include(pe => pe.PalestrantesEventos)
                .ThenInclude(p => p.Evento);
            }

            query = query.OrderBy(c => c.Nome).Where(x => x.Nome.ToLower().Contains(Palestrantename.ToLower()));

            return await query.ToArrayAsync();
        }
    }
}