using CQRSWebAPI.Caching;
using CQRSWebAPI.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CQRSWebAPI.Query
{
    public class GetUserById
    {
        //Query 
        public record Query(int Id) : IRequest<Response<User>>, ICacheable
        {
            public string CacheKey => $"GetUserById-{Id}";
        }

        public class Handler : IRequestHandler<Query, Response<User>>
        {
            private readonly SeedData _data;
            public Handler(SeedData data)
            {
                _data = data;
            }


            public async Task<Response<User>> Handle(Query request, CancellationToken cancellationToken)
            {
                var user = _data.Users.FirstOrDefault(x => x.Id == request.Id);
                return user == null ? null : new Response<User>(1, true, user);
                // Here is The Bussines Logic

            }
        }

        public record Response<T>(int Id, bool IsSuccess, T Data);
    }
}