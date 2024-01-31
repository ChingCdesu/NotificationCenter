﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NotificationCenter.Storage;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace NotificationCenter.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("NotificationCenter.Entities.Device", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("DeviceFamily")
                        .HasColumnType("integer");

                    b.Property<string>("DeviceModel")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("DeviceToken")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("DistroVersion")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("SystemVersion")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("createdAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("updatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Devices");
                });

            modelBuilder.Entity("NotificationCenter.Entities.Notification", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("DeviceId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("NotificationEndpointId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ServiceAccountId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ServiceId")
                        .HasColumnType("uuid");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<Guid>("TemplateId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("createdAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("updatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("DeviceId");

                    b.HasIndex("NotificationEndpointId");

                    b.HasIndex("ServiceAccountId");

                    b.HasIndex("ServiceId");

                    b.HasIndex("TemplateId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("NotificationCenter.Entities.NotificationEndpoint", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("AppId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("AppSecret")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("DeviceFamily")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("createdAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("updatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Endpoints");
                });

            modelBuilder.Entity("NotificationCenter.Entities.Parameter", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Format")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Remark")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("System")
                        .HasColumnType("boolean");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("createdAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("updatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Parameters");
                });

            modelBuilder.Entity("NotificationCenter.Entities.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Permissions")
                        .HasColumnType("numeric(20,0)");

                    b.Property<bool>("SystemRole")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("createdAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("updatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("NotificationCenter.Entities.RoleUserBind", b =>
                {
                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("RoleId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("RoleUserBind");
                });

            modelBuilder.Entity("NotificationCenter.Entities.Service", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PackageIdentifier")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("createdAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("updatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Services");
                });

            modelBuilder.Entity("NotificationCenter.Entities.ServiceAccount", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ServiceId")
                        .HasColumnType("uuid");

                    b.Property<string>("ServiceIdentifier")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("createdAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("updatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("ServiceId");

                    b.ToTable("ServiceAccounts");
                });

            modelBuilder.Entity("NotificationCenter.Entities.ServiceAccountDeviceBind", b =>
                {
                    b.Property<Guid>("DeviceId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ServiceAccountId")
                        .HasColumnType("uuid");

                    b.HasKey("DeviceId", "ServiceAccountId");

                    b.HasIndex("ServiceAccountId");

                    b.ToTable("ServiceAccountDeviceBind");
                });

            modelBuilder.Entity("NotificationCenter.Entities.ServiceEndpointBind", b =>
                {
                    b.Property<Guid>("NotificationEndpointId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ServiceId")
                        .HasColumnType("uuid");

                    b.HasKey("NotificationEndpointId", "ServiceId");

                    b.HasIndex("ServiceId");

                    b.ToTable("ServiceEndpointBind");
                });

            modelBuilder.Entity("NotificationCenter.Entities.ServiceSecret", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("HashedServiceSecret")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("ServiceId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("createdAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("updatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("ServiceId")
                        .IsUnique();

                    b.ToTable("ServiceSecrets");
                });

            modelBuilder.Entity("NotificationCenter.Entities.SystemSetting", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("createdAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("updatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("SystemSettings");
                });

            modelBuilder.Entity("NotificationCenter.Entities.Template", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("createdAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("updatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Templates");
                });

            modelBuilder.Entity("NotificationCenter.Entities.TemplateParameterBind", b =>
                {
                    b.Property<Guid>("ParameterId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TemplateId")
                        .HasColumnType("uuid");

                    b.HasKey("ParameterId", "TemplateId");

                    b.HasIndex("TemplateId");

                    b.ToTable("TemplateParameterBind");
                });

            modelBuilder.Entity("NotificationCenter.Entities.TemplateServiceBind", b =>
                {
                    b.Property<Guid>("ServiceId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TemplateId")
                        .HasColumnType("uuid");

                    b.HasKey("ServiceId", "TemplateId");

                    b.HasIndex("TemplateId");

                    b.ToTable("TemplateServiceBind");
                });

            modelBuilder.Entity("NotificationCenter.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("AuthenticationSchema")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("createdAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("updatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("NotificationCenter.Entities.Notification", b =>
                {
                    b.HasOne("NotificationCenter.Entities.Device", "Device")
                        .WithMany()
                        .HasForeignKey("DeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NotificationCenter.Entities.NotificationEndpoint", "NotificationEndpoint")
                        .WithMany()
                        .HasForeignKey("NotificationEndpointId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NotificationCenter.Entities.ServiceAccount", "ServiceAccount")
                        .WithMany()
                        .HasForeignKey("ServiceAccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NotificationCenter.Entities.Service", "Service")
                        .WithMany()
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NotificationCenter.Entities.Template", "Template")
                        .WithMany()
                        .HasForeignKey("TemplateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Device");

                    b.Navigation("NotificationEndpoint");

                    b.Navigation("Service");

                    b.Navigation("ServiceAccount");

                    b.Navigation("Template");
                });

            modelBuilder.Entity("NotificationCenter.Entities.RoleUserBind", b =>
                {
                    b.HasOne("NotificationCenter.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NotificationCenter.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NotificationCenter.Entities.ServiceAccount", b =>
                {
                    b.HasOne("NotificationCenter.Entities.Service", "Service")
                        .WithMany("ServiceAccounts")
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Service");
                });

            modelBuilder.Entity("NotificationCenter.Entities.ServiceAccountDeviceBind", b =>
                {
                    b.HasOne("NotificationCenter.Entities.Device", null)
                        .WithMany()
                        .HasForeignKey("DeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NotificationCenter.Entities.ServiceAccount", null)
                        .WithMany()
                        .HasForeignKey("ServiceAccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NotificationCenter.Entities.ServiceEndpointBind", b =>
                {
                    b.HasOne("NotificationCenter.Entities.NotificationEndpoint", null)
                        .WithMany()
                        .HasForeignKey("NotificationEndpointId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NotificationCenter.Entities.Service", null)
                        .WithMany()
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NotificationCenter.Entities.ServiceSecret", b =>
                {
                    b.HasOne("NotificationCenter.Entities.Service", "Service")
                        .WithOne("Secret")
                        .HasForeignKey("NotificationCenter.Entities.ServiceSecret", "ServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Service");
                });

            modelBuilder.Entity("NotificationCenter.Entities.TemplateParameterBind", b =>
                {
                    b.HasOne("NotificationCenter.Entities.Parameter", null)
                        .WithMany()
                        .HasForeignKey("ParameterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NotificationCenter.Entities.Template", null)
                        .WithMany()
                        .HasForeignKey("TemplateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NotificationCenter.Entities.TemplateServiceBind", b =>
                {
                    b.HasOne("NotificationCenter.Entities.Service", null)
                        .WithMany()
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NotificationCenter.Entities.Template", null)
                        .WithMany()
                        .HasForeignKey("TemplateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NotificationCenter.Entities.Service", b =>
                {
                    b.Navigation("Secret")
                        .IsRequired();

                    b.Navigation("ServiceAccounts");
                });
#pragma warning restore 612, 618
        }
    }
}
