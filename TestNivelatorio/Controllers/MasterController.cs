using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestNivelatorio.Interfaces;
using TestNivelatorio.Models;
using TestNivelatorio.Requests;

namespace TestNivelatorio.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MasterController : ControllerBase
    {
        private readonly IGenericService<Vehiculo> _genericService;
        private readonly IMapper _mapper;

        public MasterController(IGenericService<Vehiculo> genericService, IMapper mapper)
        {
            _genericService = genericService;
            _mapper = mapper;
        }

        [HttpGet("vehicles")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _genericService.GetAll();
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpGet("vehicle/{id}")]
        public async Task<IActionResult> Show(int id)
        {
            try
            {
                var result = await _genericService.GetById(id);
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpDelete("vehicle/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _genericService.Delete(id);
                
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpPut("vehicle/{id}")]
        public async Task<IActionResult> Update(int id, Vehiculo vehiculoRequest)
        {
            try
            {
                var exist = await _genericService.GetAsync(v => v.Patente == vehiculoRequest.Patente);
                if (exist.FirstOrDefault() == null)
                {
                    var result = await _genericService.Update(vehiculoRequest, id, null);

                    return Ok("Vehiculo actualizado ID: " + id);
                }
                else
                {
                    return StatusCode(500, "Patente existente! Error 1000");
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpPost("vehicle")]
        public async Task<IActionResult> Add(VehiculoRequest vehiculoRequest)
        {
            try
            {
                var exist = await _genericService.GetAsync(v => v.Patente == vehiculoRequest.Patente);
                if (exist.FirstOrDefault() == null)
                {
                    var vehiculo = _mapper.Map<Vehiculo>(vehiculoRequest);
                    var result = await _genericService.Insert(vehiculo);

                    return Ok(result);
                }
                else
                {
                    return StatusCode(500, "Patente existente! Error 1000");
                }
                
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }
    }
}
