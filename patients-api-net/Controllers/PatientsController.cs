using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using patients_api_net.Helpers;
using patients_api_net.Models;
using patients_api_net.Services.Patients.Interface;

namespace patients_api_net.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientsService _service;

        public PatientsController(IPatientsService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Add(PatientsModel patient)
        {
            try
            {
                if (patient == null)
                {
                    return StatusCode(400, new ServicesResponse(400, "Invalid payload", patient));
                }

                var res = await _service.AddPatient(patient);
                return StatusCode(res.Code, res); ;
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ServicesResponse(500, "Internal error", new { error = ex.Message }));
            }
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var res = await _service.GetPatients();
                return StatusCode(res.Code, res); ;
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ServicesResponse(500, "Internal error", new { error = ex.Message }));
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var res = await _service.GetPatient(id);
                return StatusCode(200, res); ;
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ServicesResponse(500, "Internal error", new { error = ex.Message }));
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {               
                var res = await _service.DeletePatient(id);
                return StatusCode(res.Code, res); ;
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ServicesResponse(500, "Internal error", new { error = ex.Message }));
            }
        }

        [HttpPatch]
        public async Task<IActionResult> Update(PatientsModel patient)
        {
            try
            {
                var res = await _service.UpdatePatient(patient);
                return StatusCode(res.Code, res); ;
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ServicesResponse(500, "Internal error", new { error = ex.Message }));
            }
        }

    }
}
