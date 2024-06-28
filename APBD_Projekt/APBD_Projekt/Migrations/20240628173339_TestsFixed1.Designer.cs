﻿// <auto-generated />
using System;
using APBD_Projekt.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace APBD_Projekt.Migrations
{
    [DbContext(typeof(CustomerDbContext))]
    [Migration("20240628173339_TestsFixed1")]
    partial class TestsFixed1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("APBD_Projekt.Models.AppUser", b =>
                {
                    b.Property<int>("IdUser")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdUser"));

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RefreshToken")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("RefreshTokenExp")
                        .HasColumnType("datetime2");

                    b.Property<string>("Rola")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdUser");

                    b.ToTable("Uzytkownicy");
                });

            modelBuilder.Entity("APBD_Projekt.Models.DTO_s.Kontrakt", b =>
                {
                    b.Property<int>("KontraktID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("KontraktID"));

                    b.Property<double>("Cena")
                        .HasPrecision(10, 2)
                        .HasColumnType("float(10)");

                    b.Property<int>("ClientID")
                        .HasColumnType("int");

                    b.Property<string>("ClientType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("CzyAktywna")
                        .HasColumnType("bit");

                    b.Property<bool>("CzyPodpisana")
                        .HasColumnType("bit");

                    b.Property<DateTime>("DataWaznosciDo")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataWaznosciOd")
                        .HasColumnType("datetime2");

                    b.Property<double>("IleZaplacono")
                        .HasColumnType("float");

                    b.Property<string>("InformacjaOAktualizacjach")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LataWsparcia")
                        .HasColumnType("int");

                    b.Property<int?>("OprogramowanieID")
                        .HasColumnType("int");

                    b.Property<string>("OprogramowanieWersja")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ZnizkaProcent")
                        .HasColumnType("int");

                    b.HasKey("KontraktID");

                    b.HasIndex("OprogramowanieID");

                    b.ToTable("Kontrakty");
                });

            modelBuilder.Entity("APBD_Projekt.Models.Firma", b =>
                {
                    b.Property<int>("FirmaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FirmaID"));

                    b.Property<string>("Adres")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("KRS")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("NazwaFirmy")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("NrTelefonu")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("FirmaID");

                    b.ToTable("Firmy");
                });

            modelBuilder.Entity("APBD_Projekt.Models.KlientFizyczny", b =>
                {
                    b.Property<int>("KlientID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("KlientID"));

                    b.Property<string>("Adres")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Imie")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Nazwisko")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("NrTelefonu")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("PESEL")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<bool>("czyUsuniety")
                        .HasColumnType("bit");

                    b.HasKey("KlientID");

                    b.ToTable("KlienciFizyczni");
                });

            modelBuilder.Entity("APBD_Projekt.Models.Oprogramowanie", b =>
                {
                    b.Property<int>("OprogramowanieID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OprogramowanieID"));

                    b.Property<double>("Cena")
                        .HasPrecision(10, 2)
                        .HasColumnType("float(10)");

                    b.Property<string>("Kategoria")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Nazwa")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Opis")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Wersja")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("OprogramowanieID");

                    b.ToTable("Oprogramowania");
                });

            modelBuilder.Entity("APBD_Projekt.Models.Platnosc", b =>
                {
                    b.Property<int>("PlatnoscID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PlatnoscID"));

                    b.Property<double>("IleZaplacono")
                        .HasColumnType("float");

                    b.Property<int>("KlientID")
                        .HasColumnType("int");

                    b.Property<int?>("KontraktID")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<double>("PozostaloDoZaplaty")
                        .HasColumnType("float");

                    b.HasKey("PlatnoscID");

                    b.HasIndex("KontraktID");

                    b.ToTable("Platnosci");
                });

            modelBuilder.Entity("APBD_Projekt.Models.Subskrybcja", b =>
                {
                    b.Property<int>("SubskrybcjaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SubskrybcjaID"));

                    b.Property<decimal>("Cena")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)");

                    b.Property<int>("ClientID")
                        .HasColumnType("int");

                    b.Property<string>("ClientType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CzasOdnowienia")
                        .HasColumnType("datetime2");

                    b.Property<bool>("CzyOplacona")
                        .HasColumnType("bit");

                    b.Property<string>("Nazwa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OprogramowanieID")
                        .HasColumnType("int");

                    b.HasKey("SubskrybcjaID");

                    b.HasIndex("OprogramowanieID");

                    b.ToTable("Subskrybcje");
                });

            modelBuilder.Entity("APBD_Projekt.Models.Znizka", b =>
                {
                    b.Property<int>("ZnikaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ZnikaID"));

                    b.Property<DateTime>("ObowiazujeDo")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ObowiazujeOd")
                        .HasColumnType("datetime2");

                    b.Property<string>("Oferta")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Wartosc")
                        .HasColumnType("int");

                    b.HasKey("ZnikaID");

                    b.ToTable("Znizki");
                });

            modelBuilder.Entity("OprogramowanieZnizka", b =>
                {
                    b.Property<int>("OprogramowaniaOprogramowanieID")
                        .HasColumnType("int");

                    b.Property<int>("ZnizkiZnikaID")
                        .HasColumnType("int");

                    b.HasKey("OprogramowaniaOprogramowanieID", "ZnizkiZnikaID");

                    b.HasIndex("ZnizkiZnikaID");

                    b.ToTable("OprogramowanieZnizka");
                });

            modelBuilder.Entity("APBD_Projekt.Models.DTO_s.Kontrakt", b =>
                {
                    b.HasOne("APBD_Projekt.Models.Oprogramowanie", "Oprogramowanie")
                        .WithMany("Kontrakty")
                        .HasForeignKey("OprogramowanieID");

                    b.Navigation("Oprogramowanie");
                });

            modelBuilder.Entity("APBD_Projekt.Models.Platnosc", b =>
                {
                    b.HasOne("APBD_Projekt.Models.DTO_s.Kontrakt", "Kontrakt")
                        .WithMany("Platnosci")
                        .HasForeignKey("KontraktID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Kontrakt");
                });

            modelBuilder.Entity("APBD_Projekt.Models.Subskrybcja", b =>
                {
                    b.HasOne("APBD_Projekt.Models.Oprogramowanie", "Oprogramowanie")
                        .WithMany("Subskrybcje")
                        .HasForeignKey("OprogramowanieID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Oprogramowanie");
                });

            modelBuilder.Entity("OprogramowanieZnizka", b =>
                {
                    b.HasOne("APBD_Projekt.Models.Oprogramowanie", null)
                        .WithMany()
                        .HasForeignKey("OprogramowaniaOprogramowanieID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("APBD_Projekt.Models.Znizka", null)
                        .WithMany()
                        .HasForeignKey("ZnizkiZnikaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("APBD_Projekt.Models.DTO_s.Kontrakt", b =>
                {
                    b.Navigation("Platnosci");
                });

            modelBuilder.Entity("APBD_Projekt.Models.Oprogramowanie", b =>
                {
                    b.Navigation("Kontrakty");

                    b.Navigation("Subskrybcje");
                });
#pragma warning restore 612, 618
        }
    }
}
