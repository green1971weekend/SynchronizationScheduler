using System.Collections.Generic;

namespace SynchronizationScheduler.Domain.Models.Application
{
    /// <summary>
    /// Post application model.
    /// </summary>
    public class Post
    {
        public int Id { get; set; }

        public int CloudId { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }



        public int PersonId { get; set; }

        public Person Person { get; set; }

        public IEnumerable<Comment> Comments { get; set; }
    }
}
