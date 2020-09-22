﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SynchronizationScheduler.Domain.Models.Application
{
    /// <summary>
    /// Comment application model.
    /// </summary>
    public class Comment
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Body { get; set; }



        public Post Post { get; set; }

        public int PostId { get; set; }
    }
}
