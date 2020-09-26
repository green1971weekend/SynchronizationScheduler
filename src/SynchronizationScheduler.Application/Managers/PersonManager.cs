using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SynchronizationScheduler.Application.DTO;
using SynchronizationScheduler.Application.Interfaces;
using SynchronizationScheduler.Domain.Models.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SynchronizationScheduler.Application.Managers
{
    /// <inheritdoc cref="IPersonManager"/>
    public class PersonManager : IPersonManager
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        /// <summary>
        /// Manager constructor which resolves services below.
        /// </summary>
        /// <param name="context">Application database context.</param>
        /// <param name="mapper">Automapper.</param>
        public PersonManager(IApplicationDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <inheritdoc/>
        public async Task<int> CreatePerson(PersonDto personDto)
        {
            var person = _mapper.Map<Person>(personDto);
            await _context.Persons.AddAsync(person);

            return await _context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task<PersonDto> GetPerson(int id)
        {
            var person = await _context.Persons.SingleOrDefaultAsync(person => person.Id == id);
            return _mapper.Map<PersonDto>(person);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<PersonDto>> GetPeople()
        {
            var people = await _context.Persons.ToListAsync();
            return _mapper.Map<IEnumerable<PersonDto>>(people);
        }

        /// <inheritdoc/>
        public async Task<int> UpdatePerson(PersonDto personDto)
        {
            var person = _mapper.Map<Person>(personDto);
            _context.Persons.Update(person);

            return await _context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task<int> DeletePerson(int id)
        {
            var person = await _context.Persons.SingleOrDefaultAsync(person => person.Id == id);
            _context.Persons.Remove(person);

            return await _context.SaveChangesAsync();
        }
    }
}
