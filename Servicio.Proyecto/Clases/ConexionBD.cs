using MySql.Data.MySqlClient;
using Servicio.Proyecto.Modelos;
using Servicio.Proyecto.Models;
using System;
using System.Data;

namespace Servicio.Proyecto.Clases
{
    public class ConexionBD
    {
        private readonly string cadenaConexion = "server=marcolegal-060825-antoniodhio1-8804.b.aivencloud.com;port=24361;database=Proyecto_Marco;uid=avnadmin;pwd=AVNS_k_DnTlsqXugfoKYxhG3;SslMode=none;";

        public bool ValidarConexion()
        {
            try
            {
                using var conexion = new MySqlConnection(cadenaConexion);
                conexion.Open();

                if (conexion.State == ConnectionState.Open)
                {
                    Console.WriteLine("Conexión exitosa a la base de datos.");
                    return true;
                }
                else
                {
                    Console.WriteLine("No se pudo abrir la conexión.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al conectar: " + ex.Message);
                return false;
            }
        }

        public bool RegistrarUsuario(Usuario usuario)
        {
            try
            {
                using var conexion = new MySqlConnection(cadenaConexion);
                conexion.Open();

                if (conexion.State != ConnectionState.Open)
                {
                    Console.WriteLine("La conexión no pudo ser abierta.");
                    return false;
                }

                using var cmd = new MySqlCommand("SP_RegistrarUsuario", conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("p_correo", usuario.Correo);
                cmd.Parameters.AddWithValue("p_contrasennia", usuario.Contrasennia);
                cmd.Parameters.AddWithValue("p_nombre", usuario.Nombre);
                cmd.Parameters.AddWithValue("p_id_rol", usuario.IdRol);
                cmd.Parameters.AddWithValue("p_id_estado", usuario.IdEstado);
                cmd.Parameters.AddWithValue("p_verificado", usuario.Verificado);

                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al registrar usuario: " + ex.Message);
                return false;
            }
        }

        public bool EliminarUsuario(int id)
        {
            try
            {
                using var conexion = new MySqlConnection(cadenaConexion);
                conexion.Open();

                if (conexion.State != ConnectionState.Open)
                {
                    Console.WriteLine("La conexión no pudo ser abierta.");
                    return false;
                }

                using var cmd = new MySqlCommand("SP_EliminarUsuario", conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("p_id_usuario", id);
                cmd.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al eliminar usuario: " + ex.Message);
                return false;
            }
        }

        public bool ActualizarUsuario(Usuario usuario)
        {
            try
            {
                using var conexion = new MySqlConnection(cadenaConexion);
                conexion.Open();

                if (conexion.State != ConnectionState.Open)
                {
                    Console.WriteLine("La conexión no pudo ser abierta.");
                    return false;
                }

                using var cmd = new MySqlCommand("SP_ActualizarUsuario", conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("p_id_usuario", usuario.IdUsuario);
                cmd.Parameters.AddWithValue("p_correo", usuario.Correo);
                cmd.Parameters.AddWithValue("p_contrasennia", usuario.Contrasennia);
                cmd.Parameters.AddWithValue("p_nombre", usuario.Nombre);
                cmd.Parameters.AddWithValue("p_id_rol", usuario.IdRol);
                cmd.Parameters.AddWithValue("p_id_estado", usuario.IdEstado);
                cmd.Parameters.AddWithValue("p_verificado", usuario.Verificado);

                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al actualizar usuario: " + ex.Message);
                return false;
            }
        }

        public Usuario ConsultarUsuario(int id, string contrasennia)
        {
            try
            {
                using var conexion = new MySqlConnection(cadenaConexion);
                conexion.Open();

                if (conexion.State != ConnectionState.Open)
                {
                    Console.WriteLine("La conexión no pudo ser abierta.");
                    return null;
                }

                using var cmd = new MySqlCommand("SP_ConsultarUsuario", conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("p_id_usuario", id);
                cmd.Parameters.AddWithValue("p_contrasennia", contrasennia);

                using var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return new Usuario
                    {
                        IdUsuario = id,
                        Correo = reader["correo"].ToString(),
                        Nombre = reader["nombre"].ToString(),
                        IdRol = Convert.ToInt32(reader["id_rol"]),
                        IdEstado = Convert.ToInt32(reader["id_estado"]),
                        Verificado = Convert.ToBoolean(reader["verificado"])
                    };
                }

                return new Usuario();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al consultar usuario: " + ex.Message);
                return new Usuario
                {
                    Nombre = "catch: " + ex
                };
            }
        }

        public bool RegistrarEmpresa(Empresa empresa)
        {
            try
            {
                using var conexion = new MySqlConnection(cadenaConexion);
                conexion.Open();

                using var cmd = new MySqlCommand("SP_RegistrarEmpresa", conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("p_nombre", empresa.Nombre);
                cmd.Parameters.AddWithValue("p_sector", empresa.Sector);
                cmd.Parameters.AddWithValue("p_id_estado", empresa.IdEstado);

                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al registrar empresa: " + ex.Message);
                return false;
            }
        }

        public Empresa ConsultarEmpresa(int idEmpresa)
        {
            try
            {
                using var conexion = new MySqlConnection(cadenaConexion);
                conexion.Open();

                using var cmd = new MySqlCommand("SP_ConsultarEmpresa", conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("p_id_empresa", idEmpresa);

                using var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return new Empresa
                    {
                        IdEmpresa = idEmpresa,
                        Nombre = reader["nombre"].ToString(),
                        Sector = reader["sector"].ToString(),
                        IdEstado = Convert.ToInt32(reader["id_estado"])
                    };
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al consultar empresa: " + ex.Message);
                return null;
            }
        }

        public bool ActualizarEmpresa(Empresa empresa)
        {
            try
            {
                using var conexion = new MySqlConnection(cadenaConexion);
                conexion.Open();

                using var cmd = new MySqlCommand("SP_ActualizarEmpresa", conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("p_id_empresa", empresa.IdEmpresa);
                cmd.Parameters.AddWithValue("p_nombre", empresa.Nombre);
                cmd.Parameters.AddWithValue("p_sector", empresa.Sector);
                cmd.Parameters.AddWithValue("p_id_estado", empresa.IdEstado);

                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al actualizar empresa: " + ex.Message);
                return false;
            }
        }

        public bool EliminarEmpresa(int idEmpresa)
        {
            try
            {
                using var conexion = new MySqlConnection(cadenaConexion);
                conexion.Open();

                using var cmd = new MySqlCommand("SP_EliminarEmpresa", conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("p_id_empresa", idEmpresa);

                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al eliminar empresa: " + ex.Message);
                return false;
            }
        }

        public bool RegistrarEmpresaAuditor(AuditorAsignacion asignacion)
        {
            try
            {
                using var conexion = new MySqlConnection(cadenaConexion);
                conexion.Open();

                using var cmd = new MySqlCommand("SP_RegistrarEmpresaAuditor", conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("p_id_empresa", asignacion.IdEmpresa);
                cmd.Parameters.AddWithValue("p_id_usuario", asignacion.IdUsuario);

                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al asignar auditor: " + ex.Message);
                return false;
            }
        }


    }
}
