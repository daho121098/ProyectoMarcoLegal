using Microsoft.AspNetCore.Mvc;
using Servicio.Proyecto.Clases;
using Servicio.Proyecto.Modelos;

namespace Servicio.Proyecto.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmpresaController : ControllerBase
    {
        private readonly ConexionBD conexion;

        public EmpresaController()
        {
            conexion = new ConexionBD();
        }

        [HttpPost("registrar")]
        public IActionResult RegistrarEmpresa([FromBody] Empresa empresa)
        {
            var resultado = conexion.RegistrarEmpresa(empresa);
            if (!resultado)
            {
                return StatusCode(500, "Error al registrar la empresa. Por favor, intente más tarde.");
            }
            return Ok("Empresa registrada correctamente.");
        }

        [HttpGet("consultar/{id}")]
        public IActionResult ConsultarEmpresa(int id)
        {
            var empresa = conexion.ConsultarEmpresa(id);
            if (empresa == null)
            {
                return NotFound("Empresa no encontrada.");
            }
            return Ok(empresa);
        }

        [HttpPut("actualizar")]
        public IActionResult ActualizarEmpresa([FromBody] Empresa empresa)
        {
            var resultado = conexion.ActualizarEmpresa(empresa);
            if (!resultado)
            {
                return StatusCode(500, $"Error al actualizar la empresa con ID {empresa.IdEmpresa}.");
            }
            return Ok("Empresa actualizada correctamente.");
        }

        [HttpDelete("eliminar/{id}")]
        public IActionResult EliminarEmpresa(int id)
        {
            var resultado = conexion.EliminarEmpresa(id);
            if (!resultado)
            {
                return StatusCode(500, $"Error al eliminar la empresa con ID {id}.");
            }
            return Ok("Empresa eliminada correctamente.");
        }

        [HttpPost("asignarAuditor")]
        public IActionResult RegistrarEmpresaAuditor([FromBody] AuditorAsignacion asignacion)
        {
            var resultado = conexion.RegistrarEmpresaAuditor(asignacion);
            if (!resultado)
            {
                return StatusCode(500, "Error al asignar auditor. Por favor, intente más tarde.");
            }
            return Ok("Auditor asignado correctamente.");
        }
    }
}
