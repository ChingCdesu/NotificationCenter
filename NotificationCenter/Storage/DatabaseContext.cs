using Microsoft.EntityFrameworkCore;
using NotificationCenter.Entities;

namespace NotificationCenter.Storage;

public class DatabaseContext(IConfiguration configuration) : DbContext
{
    public DbSet<Role> Roles { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<SystemSetting> SystemSettings { get; set; } = null!;
    
    public DbSet<Device> Devices { get; set; } = null!;
    public DbSet<Service> Services { get; set; } = null!;
    public DbSet<ServiceAccount> ServiceAccounts { get; set; } = null!;
    public DbSet<ServiceSecret> ServiceSecrets { get; set; } = null!;
    public DbSet<Template> Templates { get; set; } = null!;
    public DbSet<Parameter> Parameters { get; set; } = null!;
    public DbSet<NotificationEndpoint> Endpoints { get; set; } = null!;
    public DbSet<Notification> Notifications { get; set; } = null!;
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var pgConnectionString = configuration.GetValue<string>("Database:ConnectionString");
        optionsBuilder.UseNpgsql(pgConnectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasMany(u => u.Roles)
            .WithMany(r => r.Users)
            .UsingEntity<RoleUserBind>();

        modelBuilder.Entity<Device>()
            .HasMany(d => d.ServiceAccounts)
            .WithMany(a => a.Devices)
            .UsingEntity<ServiceAccountDeviceBind>();

        modelBuilder.Entity<Service>()
            .HasMany(s => s.NotificationEndpoints)
            .WithMany(e => e.Services)
            .UsingEntity<ServiceEndpointBind>();

        modelBuilder.Entity<Service>()
            .HasMany(s => s.Templates)
            .WithMany(t => t.Services)
            .UsingEntity<TemplateServiceBind>();
        
        modelBuilder.Entity<Template>()
            .HasMany(t => t.Parameters)
            .WithMany(p => p.Templates)
            .UsingEntity<TemplateParameterBind>();

        modelBuilder.Entity<Service>()
            .HasOne(s => s.Secret)
            .WithOne(s => s.Service)
            .HasForeignKey<ServiceSecret>(s => s.ServiceId)
            .IsRequired();

        modelBuilder.Entity<Service>()
            .HasMany(s => s.ServiceAccounts)
            .WithOne(s => s.Service)
            .HasForeignKey(s => s.ServiceId)
            .IsRequired();

        modelBuilder.Entity<Notification>()
            .HasOne(n => n.NotificationEndpoint)
            .WithMany()
            .HasForeignKey(n => n.NotificationEndpointId);

        modelBuilder.Entity<Notification>()
            .HasOne(n => n.Service)
            .WithMany()
            .HasForeignKey(n => n.ServiceId);

        modelBuilder.Entity<Notification>()
            .HasOne(n => n.Template)
            .WithMany()
            .HasForeignKey(n => n.TemplateId);

        modelBuilder.Entity<Notification>()
            .HasOne(n => n.Device)
            .WithMany()
            .HasForeignKey(n => n.DeviceId);

        modelBuilder.Entity<Notification>()
            .HasOne(n => n.ServiceAccount)
            .WithMany()
            .HasForeignKey(n => n.ServiceAccountId);

        modelBuilder.Entity<SystemSetting>()
            .HasIndex(s => s.Key)
            .IsUnique();
    }
}