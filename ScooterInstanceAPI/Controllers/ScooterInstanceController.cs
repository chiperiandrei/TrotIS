﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ScooterInstanceAPI.Data;
using ScooterInstanceAPI.DTOs;

namespace ScooterInstanceAPI.Controllers
{
    [ApiController]
    [Route("api/scooterinstance")]
    public class ScooterInstanceController : ControllerBase
    {

        //private readonly ILogger<ScooterInstanceController> _logger;

        //public ScooterInstanceController(ILogger<ScooterInstanceController> logger)
        //{
        //    _logger = logger;
        //}

        private readonly IMediator mediator;

        public ScooterInstanceController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [Route("get_all_scooters")]
        [HttpPost] 
        public async Task<ActionResult<List<ScooterInstance>>> GetAllScooters([FromBody] List<Guid> ids)
        {
            var scooterInstances = await mediator.Send(new GetScooterInstances(ids));
            if (scooterInstances == null)
            {
                return NotFound();
            }

            return Ok(scooterInstances);
        }
        [HttpGet]
        public async Task<ActionResult<List<ScooterInstance>>> GetAllScootersInstances()
        {
            var scooterInstances = await mediator.Send(new GetAllScooterInstances());
            if (scooterInstances == null)
            {
                return NotFound();
            }

            return Ok(scooterInstances);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ScooterInstance>> GetById(Guid id)
        {
            var scooterInstance = await mediator.Send(new GetScooterInstanceById(id));
            if (scooterInstance==null)
            {
                return NotFound();
            }

            return Ok(scooterInstance);
        }

        [HttpPost]
        public async Task<ActionResult<ScooterInstance>> Create([FromBody] CreateScooterInstance request)
        {
            var newScooterInstance = await mediator.Send(request);
            return newScooterInstance;
        }

        [Route("/scooter")]
        [HttpPost]
        public async Task<ActionResult<Scooter>> Create([FromBody] CreateScooter request)
        {
            var newScooter = await mediator.Send(request);
            return newScooter;
        }

        [HttpDelete]
        public async Task<ActionResult<ScooterInstance>> Delete([FromBody] DeleteScooterInstance request)
        {
            await mediator.Send(request);
            return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult<ScooterInstance>> Update([FromBody] UpdateScooterInstance request)
        {
            var scooterInstance = await mediator.Send(request);
            return scooterInstance;
        }
    }
}
