using HelpLife.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HelpLife.DataBase
{
    public class LifeContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Historico> Historicos { get; set; }
        public DbSet<Medico> Medicos { get; set; }
        public DbSet<Alerta> Alertas { get; set; }
        public DbSet<MedicoUsuario> MedicosUsuarios { get; set; }

        public LifeContext(DbContextOptions op) : base(op)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MedicoUsuario>()
                .HasKey(x => new { x.UsuarioId, x.MedicoId});

            modelBuilder.Entity<MedicoUsuario>()
                .HasOne(x => x.Usuario)
                .WithMany(x => x.MedicosUsuarios)
                .HasForeignKey(x => x.UsuarioId);

            modelBuilder.Entity<MedicoUsuario>()
               .HasOne(x => x.Medico)
               .WithMany(x => x.MedicosUsuarios)
               .HasForeignKey(x => x.MedicoId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
