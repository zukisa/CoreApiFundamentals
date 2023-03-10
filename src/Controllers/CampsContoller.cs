using System;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using CoreCodeCamp.Data;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using CoreCodeCamp.Modelss;

namespace CoreCodeCamp.Controllers{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    public class CampsController: ControllerBase
    {
        private readonly ICampRepository _repository;
        private readonly IMapper _mapper;

        public CampsController( ICampRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<CampModel[]>> Get(bool includeTalks = false)
        {
            try
            {
                var results = await _repository.GetAllCampsAsync(includeTalks);
                return _mapper.Map<CampModel[]>(results);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure!");
            }
        }

        [HttpGet("{moniker}")]
        public async Task<ActionResult<CampModel>> Get(string moniker) 
        {
            try 
            {
                var result = await _repository.GetCampAsync(moniker);
                if (result == null) return NotFound();
                return _mapper.Map<CampModel>(result);
            }
            catch(Exception) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure!");

            }
        }

        [HttpGet("search")]

        public async Task<ActionResult<CampModel[]>> searchByDate(DateTime theDate, bool incudeTalks = false)
        {
            try
            {
                var results = await _repository.GetAllCampsByEventDate(theDate, incudeTalks);
                if (!results.Any()) return NotFound();
                return _mapper.Map<CampModel[]>(results);
            }
            catch(Exception) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure!");
            }
        }
    }
}