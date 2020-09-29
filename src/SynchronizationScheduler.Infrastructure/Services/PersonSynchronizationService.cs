using Microsoft.EntityFrameworkCore;
using SynchronizationScheduler.Application.DTO;
using SynchronizationScheduler.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task SynchronizeForAddingPeopleAsync()
        {
            var cloudPeople = await _cloudManager.GetUsers().ToListAsync();
            var applicationPeople = (await _personManager.GetPeopleWithoutTrackingAsync()).ToList();

            var cloudPeopleIds = cloudPeople.Select(p => p.Id).ToList();
            var applicationPeopleIds = applicationPeople.Select(p => p.CloudId).ToList();

            var idsForSync = cloudPeopleIds.Except(applicationPeopleIds);

            var peopleForSync = cloudPeople.Join(idsForSync,    //Join method selects persons which exists in the cloud but not exists in the application.
                cloudPerson => cloudPerson.Id,
                newId => newId,
                (cloudPerson, newId) => cloudPerson); // return a new selection consisting of persons(=> cloudPerson) in which cloudPerson.Id matches newId(idsForSync).

            if (peopleForSync.Any())
            {
                foreach (var user in peopleForSync)
                {
                    var personDto = new PersonDto
                    {
                        CloudId = user.Id,
                        Name = user.Name,
                        Email = user.Email
                    };

                    await _personManager.CreatePersonAsync(personDto);
                }
            }
        }

        ///<inheritdoc/>
        public async Task SynchronizeForDeletingPeopleAsync()
        {
            var cloudPeople = await _cloudManager.GetUsers().ToListAsync();
            var applicationPeople = (await _personManager.GetPeopleWithoutTrackingAsync()).ToList();

            var cloudPeopleIds = cloudPeople.Select(p => p.Id).ToList();
            var applicationPeopleIds = applicationPeople.Select(p => p.CloudId).ToList();

            var idsForSync = applicationPeopleIds.Except(cloudPeopleIds);

            if(idsForSync.Any())
            {
                foreach (var id in idsForSync)
                {
                    await _personManager.DeletePersonByCloudIdAsync(id);
                }
            }
        }

        ///<inheritdoc/>
        public async Task SynchronizeForUpdatingPeopleAsync()
        {
            var cloudPeople = await _cloudManager.GetUsers().ToListAsync();
            var applicationPeople = (await _personManager.GetPeopleWithoutTrackingAsync()).ToList();
            
            foreach(var person in applicationPeople)
            {
                var cloudUser = cloudPeople.FirstOrDefault(user => user.Id == person.CloudId);
                var isUpdated = false;

                if(person.Name != cloudUser.Name)
                {
                    person.Name = cloudUser.Name;
                    isUpdated = true;
                }

                if (person.Email != cloudUser.Email)
                {
                    person.Email = cloudUser.Email;
                    isUpdated = true;
                }

                if(isUpdated)
                {
                    await _personManager.UpdatePersonAsync(person);
                }
            }
        }
    }
}
