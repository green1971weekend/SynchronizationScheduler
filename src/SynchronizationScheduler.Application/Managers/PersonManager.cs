using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SynchronizationScheduler.Application.DTO;
using SynchronizationScheduler.Application.Interfaces;
using SynchronizationScheduler.Domain.Models.Application;
using System;
using System.Collections.Generic;
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
        public async Task<int> CreatePersonAsync(PersonDto personDto)
        {
            var person = _mapper.Map<PersonDto, Person>(personDto);
            await _context.Persons.AddAsync(person);

            return await _context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task<PersonDto> GetPersonAsync(int id)
        {
            var person = await _context.Persons.SingleOrDefaultAsync(person => person.Id == id);
            return _mapper.Map<Person, PersonDto>(person);
        }

        /// <inheritdoc/>
        public async Task<PersonDto> GetPersonWithoutTrackingAsync(int id)
        {
            var person = await _context.Persons.AsNoTracking().SingleOrDefaultAsync(person => person.Id == id);
            return _mapper.Map<Person, PersonDto>(person);
        }

        /// <inheritdoc/>
        public async Task<PersonDto> GetPersonWithoutTrackingByCloudIdAsync(int id)
        {
            var person = await _context.Persons.AsNoTracking().SingleOrDefaultAsync(person => person.CloudId == id);
            return _mapper.Map<Person, PersonDto>(person);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<PersonDto>> GetPeopleAsync()
        {
            var people = await _context.Persons.ToListAsync();
            return _mapper.Map<IEnumerable<Person>, IEnumerable<PersonDto>>(people);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<PersonDto>> GetPeopleWithoutTrackingAsync()
        {
            var people = await _context.Persons.AsNoTracking().ToListAsync();
            return _mapper.Map<IEnumerable<Person>, IEnumerable<PersonDto>>(people);
        }

        /// <inheritdoc/>
        public async Task<int> UpdatePersonAsync(PersonDto personDto)
        {
            var person = _mapper.Map<PersonDto, Person>(personDto);
            _context.Persons.Update(person);

            return await _context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task<int> DeletePersonAsync(int id)
        {
            var person = await _context.Persons.SingleOrDefaultAsync(person => person.Id == id);
            _context.Persons.Remove(person);

            return await _context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task<int> DeletePersonByCloudIdAsync(int cloudId)
        {
            var person = await _context.Persons.SingleOrDefaultAsync(person => person.Id == cloudId);
            _context.Persons.Remove(person);

            return await _context.SaveChangesAsync();
        }
    }
}
