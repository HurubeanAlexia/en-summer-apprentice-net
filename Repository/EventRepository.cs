using endavaPractica.Net.Models;
using Microsoft.EntityFrameworkCore;

namespace endavaPractica.Net.Repository
{
    public class EventRepository : IEventRepository
    {
        private readonly Event2Context _dbContext;

        public EventRepository()
        {
            _dbContext = new Event2Context();
        }

        public int Add(Event @event)
        {
            throw new NotImplementedException();
        }

        public void Delete(Event @event)
        {
            _dbContext.Remove(@event);
            _dbContext.SaveChanges();
        }

        public IEnumerable<Event> GetAll()
        {
            var events = _dbContext.Events;
            return events;
        }

        public async Task<Event> GetById(long id)
        {
            var @event = await _dbContext.Events.Where(e => e.EventId == id).FirstOrDefaultAsync();

            return @event;
        }

        public void Update(Event @event)
        {
            _dbContext.Entry(@event).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

       
    }

}
