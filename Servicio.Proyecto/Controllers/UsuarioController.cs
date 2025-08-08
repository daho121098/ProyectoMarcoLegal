using Microsoft.AspNetCore.Mvc;
using Servicio.Proyecto.Clases;
using Servicio.Proyecto.Modelos;

namespace Servicio.Proyecto.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly ConexionBD conexion;

        public UsuarioController()
        {
            conexion = new ConexionBD();
        }

        [HttpPost("registrar")]
        public IActionResult RegistrarUsuario([FromBody] Usuario usuario)
        {
            var resultado = conexion.RegistrarUsuario(usuario);
            if (!resultado)
                    {
                return StatusCode(500, "Error al registrar el usuario. Por favor, intente más tarde.");
            }

            return Ok("Usuario registrado correctamente.");
        }

        [HttpDelete("eliminar/{id}")]
        public IActionResult EliminarUsuario(int id)
        {
            var resultado = conexion.EliminarUsuario(id);
            if (!resultado)
            {
                return StatusCode(500, $"Error al eliminar el usuario con ID {id}.");
            }

            return Ok("Usuario eliminado correctamente.");
        }

        [HttpPut("actualizar")]
        public IActionResult ActualizarUsuario([FromBody] Usuario usuario)
        {
            var resultado = conexion.ActualizarUsuario(usuario);
            if (!resultado)
            {
                return StatusCode(500, $"Error al actualizar el usuario con ID {usuario.IdUsuario}.");
            }

            return Ok("Usuario actualizado correctamente.");
        }

        [HttpPost("consultar")]
        public IActionResult ConsultarUsuario([FromBody] Usuario usuario)
        {
            var result = conexion.ConsultarUsuario(usuario.IdUsuario, usuario.Contrasennia);
            if (result == null)
            {
                return NotFound("Usuario no encontrado o contraseña incorrecta.");
            }
            return Ok(result);
        }
    }
}
