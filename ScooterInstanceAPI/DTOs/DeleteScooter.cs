﻿using System;
using MediatR;

namespace ScooterInstanceAPI.DTOs
{
    public class DeleteScooter: IRequest
    {
        public Guid ScooterId { get; set; }

        public DeleteScooter(Guid scooterId)
        {
            ScooterId = scooterId;
        }
    }
}
