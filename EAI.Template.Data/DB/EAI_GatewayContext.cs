using Microsoft.EntityFrameworkCore;

namespace $safeprojectname$
{
    public partial class EAI_GatewayContext : DbContext
    {
        public EAI_GatewayContext()
        {
        }

        public EAI_GatewayContext(DbContextOptions<EAI_GatewayContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ApplicationKeys> ApplicationKeys { get; set; }
        public virtual DbSet<Applications> Applications { get; set; }
        public virtual DbSet<ApplicationSettings> ApplicationSettings { get; set; }
        public virtual DbSet<ApplicationTokens> ApplicationTokens { get; set; }
        public virtual DbSet<Clients> Clients { get; set; }
        public virtual DbSet<ClientScopes> ClientScopes { get; set; }
        public virtual DbSet<ClientTypes> ClientTypes { get; set; }
        public virtual DbSet<Logs> Logs { get; set; }
        public virtual DbSet<Ostypes> Ostypes { get; set; }
        public virtual DbSet<Scopes> Scopes { get; set; }

        //        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //        {
        //            if (!optionsBuilder.IsConfigured)
        //            {
        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
        //                optionsBuilder.UseSqlServer("Server=172.20.46.165;Database=EAI_Gateway;Trusted_Connection=True;");
        //            }
        //        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationKeys>(entity =>
            {
                entity.Property(e => e.AppKey)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.OstypeId).HasColumnName("OSTypeId");
            });

            modelBuilder.Entity<Applications>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<ApplicationSettings>(entity =>
            {
                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.Key)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Value).IsRequired();
            });

            modelBuilder.Entity<ApplicationTokens>(entity =>
            {
                entity.Property(e => e.ClientId)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.DeviceName).HasMaxLength(100);

                entity.Property(e => e.ExpiryDate).HasColumnType("datetime");

                entity.Property(e => e.OstypeId).HasColumnName("OSTypeId");
            });

            modelBuilder.Entity<Clients>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ClientId)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.ClientSecret).HasMaxLength(255);

                entity.Property(e => e.Description).HasMaxLength(200);

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.ClientType)
                    .WithMany(p => p.Clients)
                    .HasForeignKey(d => d.ClientTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Clients_ClientTypes");
            });

            modelBuilder.Entity<ClientScopes>(entity =>
            {
                entity.HasOne(d => d.Client)
                    .WithMany(p => p.ClientScopes)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientMethods_Clients");

                entity.HasOne(d => d.Scope)
                    .WithMany(p => p.ClientScopes)
                    .HasForeignKey(d => d.ScopeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientMethods_Methods");
            });

            modelBuilder.Entity<ClientTypes>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Logs>(entity =>
            {
                entity.Property(e => e.ApplicationVersion).HasMaxLength(50);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.DeviceInfo).HasMaxLength(1000);

                entity.Property(e => e.DeviceIp)
                    .HasColumnName("DeviceIP")
                    .HasMaxLength(50);

                entity.Property(e => e.HostIp)
                    .HasColumnName("HostIP")
                    .HasMaxLength(50);

                entity.Property(e => e.MethodName).HasMaxLength(256);

                entity.Property(e => e.Ostype)
                    .HasColumnName("OSType")
                    .HasMaxLength(50);

                entity.Property(e => e.ReferenceNumber).HasMaxLength(50);

                entity.Property(e => e.RequestMethod).HasMaxLength(50);

                entity.Property(e => e.RequestUrl)
                    .HasColumnName("RequestURL")
                    .HasMaxLength(4000);

                entity.Property(e => e.Token).HasMaxLength(2000);
            });

            modelBuilder.Entity<Ostypes>(entity =>
            {
                entity.ToTable("OSTypes");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Scopes>(entity =>
            {
                entity.Property(e => e.Active).HasDefaultValueSql("((1))");

                entity.Property(e => e.ScopeName)
                    .IsRequired()
                    .HasMaxLength(400)
                    .IsUnicode(false);

                entity.HasOne(d => d.Application)
                    .WithMany(p => p.Scopes)
                    .HasForeignKey(d => d.ApplicationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Methods_Applications");
            });
        }
    }
}