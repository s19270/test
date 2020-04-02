using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cw3_s19270.Models;
using Microsoft.AspNetCore.Mvc;

namespace cw3_s19270.Controllers
{
    [Route("api/enrollments")]
    [ApiController]
    public class EnrollmentsController : Controller
    {
        private IStudentDbService _service;

        public EnrollmentsController(IStudentDbService service)
        {
            _service = service;
        }


        [HttpPost]
        public IActionResult EnrollStudent(EnrollmentsRequest request)
        {
            _service.EnrollStudent(request);
            var response = new EnrollmentsResponse();

            return Ok(response);
        }
    }
}