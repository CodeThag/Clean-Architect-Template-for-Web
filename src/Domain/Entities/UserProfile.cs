using Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class UserProfile : AuditableEntity
    {
        public Guid Id { get; set; }
        public string Surname { get; set; }
        public string Firstname { get; set; }
        public string Othernames { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int GenderId { get; set; }
        public virtual Gender Gender { get; set; }
        public bool IsActive { get; set; }
        // if null, user does not have an account
        public Guid? UserId { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public Guid  OrganisationId { get; set; }
        public virtual Organisation Organisation { get; set; }

    }
}
