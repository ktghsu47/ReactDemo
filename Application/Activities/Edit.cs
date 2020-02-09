using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Persistence;

namespace Application.Activities {
	public class Edit {
		public class Command : IRequest {
            public Guid Id { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public string Category { get; set; }
            public DateTime? Date { get; set; }
            public string City { get; set; }
            public string Venue { get; set; }
        }

		public class Handler : IRequestHandler<Command> {
			private readonly DataContext _db;
			public Handler(DataContext db) { _db = db; }

			public async Task<Unit> Handle(Command request, CancellationToken cancellationToken) {
				var activity = await _db.Activities.FindAsync(request.Id);

                if (activity == null) {
                    throw new Exception("Could not find activity");
                } else {
                    activity.Title = request.Title ?? activity.Title;
                    activity.Description = request.Description ?? activity.Description;
                    activity.Category = request.Category ?? activity.Category;
                    activity.Date = request.Date ?? activity.Date;
                    activity.City = request.City ?? activity.City;
                    activity.Venue = request.Title ?? activity.Venue;                    
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