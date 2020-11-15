using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LabReactjsDemo.Query
{
    public class ItemQuery : IRequest<IEnumerable<Item>>
    {

    }

    public class GetItemHandlerQuery : IRequestHandler<ItemQuery, IEnumerable<Item>>
    {
        public GetItemHandlerQuery()
        {
        }

        public Task<IEnumerable<Item>> Handle(ItemQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(InMemoryDatabase.LabDbContext.Items.AsEnumerable());
        }
    }
}
