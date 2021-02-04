using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Organisations.Queries.GetOrganisations
{
    public class OrganisationDto : IMapFrom<Organisation>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string TIN { get; set; }
        public string RCNumber { get; set; }
        public string BVN { get; set; }
        public bool IsActive { get; set; }
        public bool IsUnderReview { get; set; }
        public DateTime LastModified { get; set; }
        public string OrganisationType { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Organisation, OrganisationDto>()
                .ForMember(x => x.OrganisationType, opt => opt.MapFrom(s => s.OrganisationType.Name));
        }
    }
}
