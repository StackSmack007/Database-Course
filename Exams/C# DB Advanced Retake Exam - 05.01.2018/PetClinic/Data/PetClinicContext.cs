namespace PetClinic.Data
{
    using Microsoft.EntityFrameworkCore;
    using PetClinic.Models;

    public class PetClinicContext : DbContext
    {
        public PetClinicContext() { }

        public PetClinicContext(DbContextOptions options)
            : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }
        public DbSet<Animal> Animals {get;set;}
        public DbSet<Vet> Vets  {get;set;}
        public DbSet<Passport> Passports  {get;set;}
        public DbSet<AnimalAid> AnimalAids  {get;set;}
        public DbSet<Procedure> Procedures  {get;set;}
        public DbSet<ProcedureAnimalAid> ProceduresAnimalAids  {get;set;}



protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<ProcedureAnimalAid>(cfg =>
            {
                cfg.HasKey(x => new { x.ProcedureId, x.AnimalAidId });
                cfg.HasOne(pa => pa.Procedure).WithMany(p => p.ProcedureAnimalAids).HasForeignKey(pa => pa.ProcedureId);
                cfg.HasOne(pa => pa.AnimalAid).WithMany(p => p.AnimalAidProcedures).HasForeignKey(pa => pa.AnimalAidId);
            });
            builder.Entity<Vet>().HasIndex(v=>v.PhoneNumber).IsUnique();
            builder.Entity<AnimalAid>().HasIndex(aa=>aa.Name).IsUnique();
        }
    }
}
