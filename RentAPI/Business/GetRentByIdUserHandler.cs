﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using RentAPI.Data;
using RentAPI.DTOs;
using System.Threading;
using System.Threading.Tasks;

namespace RentAPI.Business
{
    public class GetRentByIdUserHandler: IRequestHandler<GetRentByIdUser, Rent>
    {
        private readonly Context context;

        public GetRentByIdUserHandler(Context context)
        {
            this.context = context;
        }

        public async Task<Rent> Handle(GetRentByIdUser request, CancellationToken cancellationToken)
        {
            var rent = await context.Rents.FirstOrDefaultAsync(r => r.UserId == request.UserId && r.IsCancelled==false);
            if (rent != default(Rent))
                return rent;
            else
                return null;
        }
    }
}
