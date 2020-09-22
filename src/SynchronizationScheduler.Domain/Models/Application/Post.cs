using System;
using System.Collections.Generic;
using System.Text;

namespace SynchronizationScheduler.Domain.Models.Application
{
    /// <summary>
    /// Post application model.
    /// </summary>
    public class Post
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }



        public int PersonId { get; set; }

        public Person Person { get; set; }

        public IEnumerable<Comment> Comments { get; set; }
    }
}
