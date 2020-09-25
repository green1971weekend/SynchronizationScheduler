using SynchronizationScheduler.Application.Mapping;
using SynchronizationScheduler.Domain.Models.Application;

namespace SynchronizationScheduler.Application.DTO
{
    /// <summary>
    /// Data transfer object which serves as the shell for interactting with data between database models and program.
    /// </summary>
    public class CommentDto : IMapFrom<Comment>
    {
        public int Id { get; set; }

        public string CloudId { get; set; }

        public int PostId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Body { get; set; }
    }
}
