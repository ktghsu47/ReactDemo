using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Persistence;

namespace Application.Activities {
    public class Delete {
        public class Command : IRequest {
            public Guid ID { get; set; }
        }

        public class Handler : IRequestHandler<Command>{
            private readonly DataContext _db;
            public Handler(DataContext db) { _db = db; }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken) {
                var activity = await _db.Activities.FindAsync(request.ID);

                if (activity == null) {
                    throw new Exception("Could not find activity");
                } else {
                    _db.Remove(activity);
                }
                
                var isSuccess = await _db.SaveChangesAsync() > 0;

                if (isSuccess) {
                    return Unit.Value;
                } else {
                    throw new Exception("Problem saving changes");
                }
            }
		}
    }
}