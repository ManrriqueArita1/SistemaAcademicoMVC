using SistemaAcademicoMVC.Models;
using System.Data.Entity;

namespace SistemaAcademicoMVC.Data
{
    public class SistemaAcademicoContext : DbContext
    {
        static SistemaAcademicoContext()
        {
            Database.SetInitializer<SistemaAcademicoContext>(null);
        }

        public SistemaAcademicoContext()
            : base("SistemaAcademicoDB")
        {
        }

        public DbSet<Estudiante> Estudiantes { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}