using GestionAsistente.AccesoDatos.Modelos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GestionAsistente.AccesoDatos.Contexto
{
    public class GestionAsistenteContexto : DbContext
    {
        public GestionAsistenteContexto() : base(new DbContextOptionsBuilder<GestionAsistenteContexto>()
            .UseSqlServer("String de conexion").Options)
        {
        }
        public DbSet<PersonaEF> PersonaEFs { get; set; }
        public DbSet<AsistenteEF> AsistenteEFs { get; set; }
        public DbSet<AdministradorEF> AdministradorEFs { get; set; }
        public DbSet<UsuarioEF> UsuariosEF { get; set; }
        public DbSet<RolEF> RolEFs { get; set; }
        public DbSet<UnidadEF> UnidadEFs { get; set; }
        public DbSet<BadgeEF> BadgesEF { get; set; }
        public DbSet<EstacionTrabajoEF> EstacionTrabajoEFs { get; set; }
        public DbSet<HorarioEF> HorarioEFs { get; set; }
        public DbSet<OficinaEF> OficinaEFs { get; set; }
        public DbSet<HistorialAccionesEF> HistoriaAccionesEFs { get; set; }
        public DbSet<EncargadoEF> EncargadoEFs { get; set; }
        public DbSet<ReporteBadgeEF> ReporteBadgeEFs { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AsistenteEF>()
                .HasOne(a => a.Persona) // Relación con PersonaEF
                .WithMany() // Si PersonaEF no tiene relación inversa, simplemente lo omitimos
                .OnDelete(DeleteBehavior.Restrict); // Configura la eliminación a Restrict

            modelBuilder.Entity<AsistenteEF>()
       .HasOne(a => a.Unidad) // Relación con UnidadEF
       .WithMany() // Si UnidadEF no tiene relación inversa, simplemente lo omitimos
       .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<AsistenteEF>()
                .HasOne(a => a.Encargado) // Relación con EncargadoEF
                .WithMany() // Si EncargadoEF no tiene relación inversa, simplemente lo omitimos
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<AsistenteEF>()
                .HasOne(a => a.Badge) // Relación con UnidadEF
                .WithMany() // Si UnidadEF no tiene relación inversa, simplemente lo omitimos
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<AsistenteEF>()
                .HasOne(a => a.Encargado)
                .WithMany()
                .HasForeignKey(a => a.EncargadoID)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<AsistenteEF>()
                 .HasOne(a => a.Unidad)
                 .WithMany()
                 .HasForeignKey(a => a.UnidadID)
                 .IsRequired(false)
                 .OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<AsistenteEF>()
               .HasOne(a => a.Badge)
               .WithMany()
               .HasForeignKey(a => a.BadgeID)
               .IsRequired(false)
               .OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<EncargadoEF>()
                .HasOne(e => e.Unidad) // Relación con Unidad
                .WithMany() // Si Unidad no tiene relación inversa, simplemente lo omitimos
                .OnDelete(DeleteBehavior.SetNull); // Configura la eliminación a SetNull

            modelBuilder.Entity<BadgeEF>()
                .HasOne(b => b.Unidad) // Relación con Unidad
                .WithMany() // Si Unidad no tiene relación inversa, simplemente lo omitimos
                .OnDelete(DeleteBehavior.SetNull); // Configura la eliminación a SetNull

        }
    }
}
