using Microsoft.EntityFrameworkCore;
using SynchronizationScheduler.Application.DTO;
using SynchronizationScheduler.Application.Interfaces;
using SynchronizationScheduler.Domain.Models.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynchronizationScheduler.Infrastructure.Services
{
    ///<inheritdoc cref="IPersonSynchronizationService"/>
    public class PersonSynchronizationService : IPersonSynchronizationService
    {
        private readonly ICloudManager _cloudManager;

        private readonly IPersonManager _personManager;

        /// <summary>
        /// Constructor for resolving CloudManager, PersonManager from DI container.
        /// </summary>
        /// <param name="cloudManager">CloudManager.</param>
        /// <param name="personManager">PersonManager.</param>
        public PersonSynchronizationService(ICloudManager cloudManager, IPersonManager personManager)
        {
            _cloudManager = cloudManager ?? throw new ArgumentNullException(nameof(cloudManager));
            _personManager = personManager ?? throw new ArgumentNullException(nameof(personManager));
        }

        ///<inheritdoc/>
        public async Task SynchronizeForAddingPeople()
        {
            var cloudPeople = await _cloudManager.GetUsers().ToListAsync();
            var applicationPeople = (await _personManager.GetPeople()).ToList();

            var cloudPeopleIds = cloudPeople.Select(p => p.Id).ToList();
            var applicationPeopleIds = applicationPeople.Select(p => p.Id).ToList();

            var idsForSync = cloudPeopleIds.Except(applicationPeopleIds);

            var peopleForSync = cloudPeople.Join(idsForSync,    //Join method selects persons which exists in the cloud but not exists in the application.
                cloudPerson => cloudPerson.Id,
                newId => newId,
                (cloudPerson, newId) => cloudPerson); // return a new selection consisting of persons(=> cloudPerson) in which cloudPerson.Id matches newId(idsForSync).

            foreach(var user in peopleForSync)
            {
                var personDto = new PersonDto
                {
                    CloudId = user.Id,
                    Name = user.Name,
                    Email = user.Email
                };

                await _personManager.CreatePerson(personDto);
            }
        }

    }
}
