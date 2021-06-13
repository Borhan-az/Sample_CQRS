using CQRSWebAPI.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CQRSWebAPI.Command
{
    public class AddUser
    {
        // Command
        public record Command(string Name, bool IsActive) : IRequest<User>;
        // Handler

        public class Handler : IRequestHandler<Command, User>
        {
            private readonly SeedData data;

            public Handler(SeedData data)
            {
                this.data = data;
            }
            public async Task<User> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = new User { Id = 10, Name = request.Name, IsActive = request.IsActive };
                data.Users.Add(user);
                return user; 
            }   
        }
    }
}