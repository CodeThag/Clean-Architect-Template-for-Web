using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Organisations.Queries.GetOrganisations;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Setup.ApplicationTypes.Queries.GetApplicationType
{
    public class GetApplicationTypeListDataTableQuery : DataTableListRequestModel, IRequest<DataTableVm<ApplicationTypeDto>>
    {
    }

    public class GetApplicationTypeListDataTableQueryHandler : IRequestHandler<GetApplicationTypeListDataTableQuery, DataTableVm<ApplicationTypeDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger<GetApplicationTypeListDataTableQueryHandler> _logger;
        private readonly IMapper _mapper;

        public GetApplicationTypeListDataTableQueryHandler(IApplicationDbContext context, ILogger<GetApplicationTypeListDataTableQueryHandler> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<DataTableVm<ApplicationTypeDto>> Handle(GetApplicationTypeListDataTableQuery request, CancellationToken cancellationToken)
        {
            var data = _context.ApplicationTypes.Include(x => x.Department).AsQueryable();
            var totalRecords = data.Count();

            if (request.length == -1) request.length = totalRecords;

            data = string.IsNullOrEmpty(request.search)
                ? data : data.Where(x => x.Name.Contains(request.search)
                                         || x.Description.Contains(request.search)
                                         || x.Department.Name.Contains(request.search)
                                         || x.WorkflowCode.Contains(request.search));

            IQueryable<ApplicationType> OrderingFunction(IQueryable<ApplicationType> m)
            {
                return request.sortDirection == "asc" ? request.sortColumn == 1 ? m.OrderBy(x => x.Name) : m.OrderBy(x => x.Department.Name) : request.sortColumn == 1 ? m.OrderByDescending(x => x.Name) : m.OrderByDescending(x => x.Department.Name);
            }

            var filteredData = OrderingFunction(data).Skip(request.start).Take(request.length);

            var dataTableData = new DataTableVm<ApplicationTypeDto>
            {
                draw = request.draw,
                recordsTotal = totalRecords,
                recordsFiltered = data.Count(),
                data = await filteredData.ProjectTo<ApplicationTypeDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken)
            };

            return dataTableData;
        }
    }
}
