﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DataModels
{
    public class Member
    {
        public Member()
        {
            Tasks = new List<Task>();
        }

        public Guid Id { get; set; } = Guid.NewGuid();
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Roles { get; set; }
        public string Avatar { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
    }
}
