using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SynchronizationScheduler.Application.DTO;
using SynchronizationScheduler.Application.Interfaces;
using SynchronizationScheduler.Application.Resources;
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

        private readonly ILogger<PostSynchronizationService> _logger;

        /// <summary>
        /// Constructor for resolving CloudManager, PersonManager from DI container.
        /// </summary>
        /// <param name="cloudManager">CloudManager.</param>
        /// <param name="personManager">PersonManager.</param>
        /// <param name="logger">Serilog.</param>
        public PersonSynchronizationService(ICloudManager cloudManager, 
                                            IPersonManager personManager,
                                            ILogger<PostSynchronizationService> logger)
        {
            _cloudManager = cloudManager ?? throw new ArgumentNullException(nameof(cloudManager));
            _personManager = personManager ?? throw new ArgumentNullException(nameof(personManager));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        ///<inheritdoc/>
        public async Task SynchronizeForAddingPeopleAsync()
        {
            _logger.LogInformation(PeopleSynchronizationService.StartSynchronizationForAdditionPeople);

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

            _logger.LogInformation(PeopleSynchronizationService.EndSynchronizationForAdditionPeople);
        }

        ///<inheritdoc/>
        public async Task SynchronizeForDeletingPeopleAsync()
        {
            _logger.LogInformation(PeopleSynchronizationService.StartSynchronizationForDeletionPeople);

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

            _logger.LogInformation(PeopleSynchronizationService.EndSynchronizationForDeletionPeople);
        }

        ///<inheritdoc/>
        public async Task SynchronizeForUpdatingPeopleAsync()
        {
            _logger.LogInformation(PeopleSynchronizationService.StartSynchronizationForUpdationPeople);

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

            _logger.LogInformation(PeopleSynchronizationService.EndSynchronizationForUpdationPeople);
        }
    }
}
