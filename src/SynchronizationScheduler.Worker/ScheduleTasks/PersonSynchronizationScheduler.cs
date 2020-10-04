using Coravel.Invocable;
using SynchronizationScheduler.Application.Interfaces;
using System;
using System.Threading.Tasks;

namespace SynchronizationScheduler.Worker.ScheduleTasks
{
    /// <summary>
    /// Synchronization scheduler for person models, invokes by the coravel service.
    /// </summary>
    public class PersonSynchronizationScheduler : IInvocable
    {
        IPersonSynchronizationService _personSynchronizationService;

        public PersonSynchronizationScheduler(IPersonSynchronizationService personSynchronizationService)
        {
            _personSynchronizationService = personSynchronizationService ?? throw new ArgumentNullException(nameof(personSynchronizationService));
        }

        /// <summary>
        /// Serves as a trigger for coravel.
        /// </summary>
        public async Task Invoke()
        {
            await _personSynchronizationService.SynchronizeForAddingPeopleAsync();
            await _personSynchronizationService.SynchronizeForDeletingPeopleAsync();
            await _personSynchronizationService.SynchronizeForUpdatingPeopleAsync();
        }
    }
}
