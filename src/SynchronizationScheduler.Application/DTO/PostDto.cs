using SynchronizationScheduler.Application.Mapping;
using SynchronizationScheduler.Domain.Models.Application;

namespace SynchronizationScheduler.Application.DTO
{
    /// <summary>
    /// Data transfer object which serves as the shell for interactting with data between database models and program.
    /// </summary>
    public class PostDto : IMapFrom<Post>
    {
        public int Id { get; set; }

        public string CloudId { get; set; }

        public int PersonId { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }
    }
}
