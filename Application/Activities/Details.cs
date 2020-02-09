using System;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Persistence;

namespace Application.Activities {
    public class Details {
        public class Query : IRequest<Activity> {
            public Guid ID { get; set; }
        }

		public class Handler : IRequestHandler<Query, Activity> {
			private readonly DataContext _db;
			public Handler(DataContext db) { _db = db; }

			public async Task<Activity> Handle(Query request, CancellationToken cancellationToken) {
				return await _db.Activities.FindAsync(request.ID);
			}
		}
	}
}