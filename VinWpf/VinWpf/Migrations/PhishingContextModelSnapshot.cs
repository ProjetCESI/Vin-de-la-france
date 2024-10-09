﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using VinWpf.DataSet;

#nullable disable

namespace VinWpf.Migrations
{
    [DbContext(typeof(PhishingContext))]
    partial class PhishingContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("VinWpf.DataSet.ArticlesClass", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("FamillesClassId")
                        .HasColumnType("integer");

                    b.Property<int>("FournisseursClassId")
                        .HasColumnType("integer");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("MinimumThreshold")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("QuantityStock")
                        .HasColumnType("integer");

                    b.Property<Guid>("Reference")
                        .HasColumnType("uuid");

                    b.Property<int>("UnitPrice")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("FamillesClassId");

                    b.HasIndex("FournisseursClassId");

                    b.ToTable("ArticlesClass");
                });

            modelBuilder.Entity("VinWpf.DataSet.ClientsClass", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("ClientsClass");
                });

            modelBuilder.Entity("VinWpf.DataSet.CommandeClientsClass", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ClientsClassId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Statut")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ClientsClassId");

                    b.ToTable("CommandeClientsClass");
                });

            modelBuilder.Entity("VinWpf.DataSet.CommandeFournisseursClass", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("FournisseursClassId")
                        .HasColumnType("integer");

                    b.Property<string>("Statut")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("FournisseursClassId");

                    b.ToTable("CommandeFournisseursClass");
                });

            modelBuilder.Entity("VinWpf.DataSet.FamillesClass", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("FamillesClass");
                });

            modelBuilder.Entity("VinWpf.DataSet.FournisseursClass", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("FournisseursClass");
                });

            modelBuilder.Entity("VinWpf.DataSet.LigneCommandeClientsClass", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ArticlesClassId")
                        .HasColumnType("integer");

                    b.Property<int>("CommandeClientsClassId")
                        .HasColumnType("integer");

                    b.Property<int>("PrixUnitaire")
                        .HasColumnType("integer");

                    b.Property<int>("Quantite")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ArticlesClassId");

                    b.HasIndex("CommandeClientsClassId");

                    b.ToTable("LigneCommandeClientsClass");
                });

            modelBuilder.Entity("VinWpf.DataSet.LigneCommandeFournisseursClass", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ArticlesClassId")
                        .HasColumnType("integer");

                    b.Property<int>("CommandeFournisseursClassId")
                        .HasColumnType("integer");

                    b.Property<int>("PrixUnitaire")
                        .HasColumnType("integer");

                    b.Property<int>("Quantite")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ArticlesClassId");

                    b.HasIndex("CommandeFournisseursClassId");

                    b.ToTable("LigneCommandeFournisseursClass");
                });

            modelBuilder.Entity("VinWpf.DataSet.ArticlesClass", b =>
                {
                    b.HasOne("VinWpf.DataSet.FamillesClass", "FamillesClass")
                        .WithMany()
                        .HasForeignKey("FamillesClassId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VinWpf.DataSet.FournisseursClass", "FournisseursClass")
                        .WithMany()
                        .HasForeignKey("FournisseursClassId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FamillesClass");

                    b.Navigation("FournisseursClass");
                });

            modelBuilder.Entity("VinWpf.DataSet.CommandeClientsClass", b =>
                {
                    b.HasOne("VinWpf.DataSet.ClientsClass", "ClientsClass")
                        .WithMany()
                        .HasForeignKey("ClientsClassId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ClientsClass");
                });

            modelBuilder.Entity("VinWpf.DataSet.CommandeFournisseursClass", b =>
                {
                    b.HasOne("VinWpf.DataSet.FournisseursClass", "FournisseursClass")
                        .WithMany()
                        .HasForeignKey("FournisseursClassId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FournisseursClass");
                });

            modelBuilder.Entity("VinWpf.DataSet.LigneCommandeClientsClass", b =>
                {
                    b.HasOne("VinWpf.DataSet.ArticlesClass", "ArticlesClass")
                        .WithMany()
                        .HasForeignKey("ArticlesClassId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VinWpf.DataSet.CommandeClientsClass", "CommandeClientsClass")
                        .WithMany()
                        .HasForeignKey("CommandeClientsClassId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ArticlesClass");

                    b.Navigation("CommandeClientsClass");
                });

            modelBuilder.Entity("VinWpf.DataSet.LigneCommandeFournisseursClass", b =>
                {
                    b.HasOne("VinWpf.DataSet.ArticlesClass", "ArticlesClass")
                        .WithMany()
                        .HasForeignKey("ArticlesClassId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VinWpf.DataSet.CommandeFournisseursClass", "CommandeFournisseursClass")
                        .WithMany()
                        .HasForeignKey("CommandeFournisseursClassId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ArticlesClass");

                    b.Navigation("CommandeFournisseursClass");
                });
#pragma warning restore 612, 618
        }
    }
}
