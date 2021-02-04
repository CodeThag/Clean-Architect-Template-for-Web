using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Organisations.Queries.GetOrganisations
{
    public class GetOrganisationListQuery : DataTableListRequestModel, IRequest<DataTableVm<OrganisationDto>>
    {
    }

    public class GetOrganisationListQueryHandler : IRequestHandler<GetOrganisationListQuery, DataTableVm<OrganisationDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger<GetOrganisationListQueryHandler> _logger;
        private readonly IMapper _mapper;

        public GetOrganisationListQueryHandler(IApplicationDbContext context, ILogger<GetOrganisationListQueryHandler> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<DataTableVm<OrganisationDto>> Handle(GetOrganisationListQuery request, CancellationToken cancellationToken)
        {
            var data = _context.Organisations.Include(x => x.OrganisationType).AsQueryable();
            var totalRecords = data.Count();

            if (request.length == -1) request.length = totalRecords;

            data = string.IsNullOrEmpty(request.search)
                ? data : data.Where(x => x.Name.Contains(request.search) 
                || x.TIN.Contains(request.search) 
                || x.RCNumber.Contains(request.search) 
                || x.OrganisationType.Name.Contains(request.search));

            IQueryable<Organisation> OrderingFunction(IQueryable<Organisation> m)
            {
                return request.sortDirection == "asc" ? request.sortColumn == 1 ? m.OrderBy(x => x.Name) : m.OrderBy(x => x.TIN) : request.sortColumn == 1 ? m.OrderByDescending(x => x.Name) : m.OrderByDescending(x => x.TIN);
            }

            var filteredData = OrderingFunction(data).Skip(request.start).Take(request.length);

            var dataTableData = new DataTableVm<OrganisationDto>
            {
                draw = request.draw,
                recordsTotal = totalRecords,
                recordsFiltered = data.Count(),
                data = await filteredData.ProjectTo<OrganisationDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken)
            };

            return dataTableData;
        }
    }
}
