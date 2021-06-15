using CQRSWebAPI.DTOs;
using CQRSWebAPI.Model;
using CQRSWebAPI.Validation;
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
        public record Command(string Name, bool IsActive) : IRequest<Response<User>>;


        // Validator 
        public class Validator : IValidationHandler<Command>
        {
            private readonly SeedData data;

            public Validator(SeedData data) => this.data = data;


            public async Task<ValidationResult> Validation(Command request)
            {
                
                if (data.Users.Any(x => x.Name.Equals(request.Name, StringComparison.OrdinalIgnoreCase)))
                    return ValidationResult.Fail("User Already Exist");
                
                return ValidationResult.Success;
            }
        }

        // Handler
        public class Handler : IRequestHandler<Command, Response<User>>
        {
            private readonly SeedData data;

            public Handler(SeedData data)
            {
                this.data = data;
            }
            public async Task<Response<User>> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = new User { Id = 10, Name = request.Name, IsActive = request.IsActive };
                data.Users.Add(user);
                return new Response<User> { data = user };
            }
        }
        public class Response<T> : ResponseDto
        {
            public T data { get; set; }

        }
    }
}