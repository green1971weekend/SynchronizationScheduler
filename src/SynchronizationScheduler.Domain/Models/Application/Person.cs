using System;
using System.Collections.Generic;
using System.Text;

namespace SynchronizationScheduler.Domain.Models.Application
{
    /// <summary>
    /// Person application model.
    /// </summary>
    public class Person
    {
        public int Id { get; set; }

        public string CloudId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }


        public ICollection<Post> Posts { get; set; }
    }
}
