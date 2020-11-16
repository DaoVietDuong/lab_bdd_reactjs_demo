using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LabReactjsDemo.Command
{
    public class CreateItemCommand : IRequest<Guid>
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
    }

    public class CreateItemHandlerCommand : IRequestHandler<CreateItemCommand, Guid>
    {
        public Task<Guid> Handle(CreateItemCommand request, CancellationToken cancellationToken)
        {
            var newItem = new Item()
            {
                Id = request.Id.HasValue ? request.Id.Value : Guid.NewGuid(),
                Description = request.Description,
                Name = request.Name,
                Quantity = request.Quantity
            };
            InMemoryDatabase.LabDbContext.Items.Add(newItem);
            return Task.FromResult(newItem.Id);
        }
    }
}
