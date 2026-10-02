// Modelo de PerfilFisico para SUPLEMENTOR
// Guarda el estado físico actual y el objetivo del usuario registrado.
// Con estos datos el recomendador sugiere la suplementación adecuada.

namespace ApiSuplementor.Models
{
    /// <summary>
    /// Clase que representa el perfil físico de un usuario registrado.
    /// </summary>
    public class PerfilFisico
    {
        /// <summary>
        /// Identificador único del perfil (autoincremental en la base de datos).
        /// </summary>
        public int IdPerfil { get; set; }

        /// <summary>
        /// Identificador del usuario dueño del perfil.
        /// </summary>
        public int IdUsuario { get; set; }

        /// <summary>
        /// Peso actual del usuario en kilogramos.
        /// </summary>
        public decimal PesoKg { get; set; }

        /// <summary>
        /// Altura actual del usuario en centímetros.
        /// </summary>
        public decimal AlturaCm { get; set; }

        /// <summary>
        /// Edad del usuario en años.
        /// </summary>
        public int Edad { get; set; }

        /// <summary>
        /// Sexo del usuario: Masculino o Femenino.
        /// </summary>
        public string Sexo { get; set; }

        /// <summary>
        /// Objetivo del entrenamiento: PerderGrasa, GanarMasaMuscular,
        /// Mantenimiento o Rendimiento.
        /// </summary>
        public string Objetivo { get; set; }

        /// <summary>
        /// Nivel de actividad física: Sedentario, Ligero, Moderado o Intenso.
        /// </summary>
        public string NivelActividad { get; set; }

        /// <summary>
        /// Fecha en que se registró o actualizó la evaluación física.
        /// </summary>
        public DateTime FechaEvaluacion { get; set; }
    }
}