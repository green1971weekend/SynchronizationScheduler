using System;
using System.Collections.Generic;
using System.Text;

namespace SynchronizationScheduler.Domain.Models.Cloud
{
    /// <summary>
    /// Post cloud model.
    /// </summary>
    public class Post
    {
        public int Id { get; set; }

        public int PersonId { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }
    }
}
