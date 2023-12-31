﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using liquidador_web.Data;

namespace liquidador_web.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20190502011618_Usuario_Liquidador")]
    partial class Usuario_Liquidador
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("liquidador_web.Models.Datasainte", b =>
                {
                    b.Property<int>("IDTasa")
                        .HasColumnName("A921IDTasa");

                    b.Property<string>("Periodo")
                        .HasColumnName("A921Periodo")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("ResVigencia")
                        .HasColumnName("A921ResVigencia")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("TipoTasa")
                        .HasColumnName("A921TipoTasa")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<double?>("ValorTasa")
                        .HasColumnName("A921ValorTasa")
                        .HasColumnType("float");

                    b.Property<DateTime>("VigenteDesde")
                        .HasColumnName("A921VigenteDesde")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("VigenteHasta")
                        .HasColumnName("A921VigenteHasta")
                        .HasColumnType("datetime");

                    b.HasKey("IDTasa");

                    b.ToTable("T921DATASAINTE");
                });

            modelBuilder.Entity("liquidador_web.Models.LiquidadorUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Creator");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FullName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("liquidador_web.Models.T052baprocgene", b =>
                {
                    b.Property<string>("A052codiproc")
                        .HasColumnName("A052CODIPROC")
                        .HasMaxLength(4)
                        .IsUnicode(false);

                    b.Property<string>("A052descproc")
                        .IsRequired()
                        .HasColumnName("A052DESCPROC")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.HasKey("A052codiproc");

                    b.ToTable("T052BAPROCGENE");
                });

            modelBuilder.Entity("liquidador_web.Models.T053baclasgene", b =>
                {
                    b.Property<string>("A053codiclas")
                        .HasColumnName("A053CODICLAS")
                        .HasMaxLength(4)
                        .IsUnicode(false);

                    b.Property<string>("A053descclas")
                        .IsRequired()
                        .HasColumnName("A053DESCCLAS")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.HasKey("A053codiclas");

                    b.ToTable("T053BACLASGENE");
                });

            modelBuilder.Entity("liquidador_web.Models.T071basubcgene", b =>
                {
                    b.Property<string>("A071codisubc")
                        .HasColumnName("A071CODISUBC")
                        .HasMaxLength(4)
                        .IsUnicode(false);

                    b.Property<string>("A071descsubc")
                        .IsRequired()
                        .HasColumnName("A071DESCSUBC")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.HasKey("A071codisubc");

                    b.ToTable("T071BASUBCGENE");
                });

            modelBuilder.Entity("liquidador_web.Models.T103dainfoproc", b =>
                {
                    b.Property<string>("A103llavproc")
                        .HasColumnName("A103LLAVPROC")
                        .HasMaxLength(23)
                        .IsUnicode(false);

                    b.Property<string>("A103anoradi")
                        .IsRequired()
                        .HasColumnName("A103ANORADI")
                        .HasMaxLength(4)
                        .IsUnicode(false);

                    b.Property<string>("A103anotactd")
                        .HasColumnName("A103ANOTACTD")
                        .HasMaxLength(1000)
                        .IsUnicode(false);

                    b.Property<string>("A103anotacts")
                        .HasColumnName("A103ANOTACTS")
                        .HasMaxLength(1000)
                        .IsUnicode(false);

                    b.Property<string>("A103anotorig")
                        .HasColumnName("A103ANOTORIG")
                        .HasMaxLength(255)
                        .IsUnicode(false);

                    b.Property<string>("A103ciudradi")
                        .IsRequired()
                        .HasColumnName("A103CIUDRADI")
                        .HasMaxLength(5)
                        .IsUnicode(false);

                    b.Property<string>("A103codiactd")
                        .HasColumnName("A103CODIACTD")
                        .HasMaxLength(8)
                        .IsUnicode(false);

                    b.Property<string>("A103codiacts")
                        .HasColumnName("A103CODIACTS")
                        .HasMaxLength(8)
                        .IsUnicode(false);

                    b.Property<string>("A103codiarea")
                        .IsRequired()
                        .HasColumnName("A103CODIAREA")
                        .HasMaxLength(4)
                        .IsUnicode(false);

                    b.Property<string>("A103codiciuo")
                        .IsRequired()
                        .HasColumnName("A103CODICIUO")
                        .HasMaxLength(5)
                        .IsUnicode(false);

                    b.Property<string>("A103codiclas")
                        .IsRequired()
                        .HasColumnName("A103CODICLAS")
                        .HasMaxLength(4)
                        .IsUnicode(false);

                    b.Property<string>("A103codiento")
                        .IsRequired()
                        .HasColumnName("A103CODIENTO")
                        .HasMaxLength(2)
                        .IsUnicode(false);

                    b.Property<string>("A103codiespo")
                        .IsRequired()
                        .HasColumnName("A103CODIESPO")
                        .HasMaxLength(2)
                        .IsUnicode(false);

                    b.Property<string>("A103codiinst")
                        .HasColumnName("A103CODIINST")
                        .HasMaxLength(4)
                        .IsUnicode(false);

                    b.Property<string>("A103codinatu")
                        .HasColumnName("A103CODINATU")
                        .HasMaxLength(4)
                        .IsUnicode(false);

                    b.Property<string>("A103codinumo")
                        .IsRequired()
                        .HasColumnName("A103CODINUMO")
                        .HasMaxLength(3)
                        .IsUnicode(false);

                    b.Property<string>("A103codipadd")
                        .HasColumnName("A103CODIPADD")
                        .HasMaxLength(8)
                        .IsUnicode(false);

                    b.Property<string>("A103codipads")
                        .HasColumnName("A103CODIPADS")
                        .HasMaxLength(8)
                        .IsUnicode(false);

                    b.Property<string>("A103codipone")
                        .HasColumnName("A103CODIPONE")
                        .HasMaxLength(4)
                        .IsUnicode(false);

                    b.Property<string>("A103codiproc")
                        .IsRequired()
                        .HasColumnName("A103CODIPROC")
                        .HasMaxLength(4)
                        .IsUnicode(false);

                    b.Property<string>("A103codiproo")
                        .HasColumnName("A103CODIPROO")
                        .HasMaxLength(4)
                        .IsUnicode(false);

                    b.Property<string>("A103codirama")
                        .HasColumnName("A103CODIRAMA")
                        .HasMaxLength(20)
                        .IsUnicode(false);

                    b.Property<string>("A103codirecu")
                        .IsRequired()
                        .HasColumnName("A103CODIRECU")
                        .HasMaxLength(4)
                        .IsUnicode(false);

                    b.Property<string>("A103codisubc")
                        .IsRequired()
                        .HasColumnName("A103CODISUBC")
                        .HasMaxLength(4)
                        .IsUnicode(false);

                    b.Property<string>("A103codiubic")
                        .HasColumnName("A103CODIUBIC")
                        .HasMaxLength(4)
                        .IsUnicode(false);

                    b.Property<string>("A103codiusua")
                        .HasColumnName("A103CODIUSUA")
                        .HasMaxLength(4)
                        .IsUnicode(false);

                    b.Property<int?>("A103consnorm")
                        .HasColumnName("A103CONSNORM");

                    b.Property<string>("A103consproc")
                        .IsRequired()
                        .HasColumnName("A103CONSPROC")
                        .HasMaxLength(2)
                        .IsUnicode(false);

                    b.Property<string>("A103cuadproc")
                        .HasColumnName("A103CUADPROC")
                        .HasMaxLength(15)
                        .IsUnicode(false);

                    b.Property<string>("A103cuadprod")
                        .HasColumnName("A103CUADPROD")
                        .HasMaxLength(15)
                        .IsUnicode(false);

                    b.Property<string>("A103cuadpros")
                        .HasColumnName("A103CUADPROS")
                        .HasMaxLength(15)
                        .IsUnicode(false);

                    b.Property<string>("A103descactd")
                        .HasColumnName("A103DESCACTD")
                        .HasMaxLength(150)
                        .IsUnicode(false);

                    b.Property<string>("A103descacts")
                        .HasColumnName("A103DESCACTS")
                        .HasMaxLength(150)
                        .IsUnicode(false);

                    b.Property<string>("A103entiradi")
                        .IsRequired()
                        .HasColumnName("A103ENTIRADI")
                        .HasMaxLength(2)
                        .IsUnicode(false);

                    b.Property<string>("A103esperadi")
                        .IsRequired()
                        .HasColumnName("A103ESPERADI")
                        .HasMaxLength(2)
                        .IsUnicode(false);

                    b.Property<DateTime?>("A103fechdesd")
                        .HasColumnName("A103FECHDESD")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("A103fechdess")
                        .HasColumnName("A103FECHDESS")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("A103fechfind")
                        .HasColumnName("A103FECHFIND")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("A103fechfins")
                        .HasColumnName("A103FECHFINS")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("A103fechinid")
                        .HasColumnName("A103FECHINID")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("A103fechinis")
                        .HasColumnName("A103FECHINIS")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("A103fechorig")
                        .HasColumnName("A103FECHORIG")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("A103fechpres")
                        .HasColumnName("A103FECHPRES")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("A103fechproc")
                        .HasColumnName("A103FECHPROC")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("A103fechregi")
                        .HasColumnName("A103FECHREGI")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("A103fechrepa")
                        .HasColumnName("A103FECHREPA")
                        .HasColumnType("datetime");

                    b.Property<string>("A103flagcicd")
                        .HasColumnName("A103FLAGCICD")
                        .HasMaxLength(2)
                        .IsUnicode(false);

                    b.Property<string>("A103flagcics")
                        .HasColumnName("A103FLAGCICS")
                        .HasMaxLength(2)
                        .IsUnicode(false);

                    b.Property<string>("A103flagdete")
                        .HasColumnName("A103FLAGDETE")
                        .HasMaxLength(2)
                        .IsUnicode(false);

                    b.Property<bool?>("A103flagproc")
                        .HasColumnName("A103FLAGPROC");

                    b.Property<string>("A103flagrepa")
                        .HasColumnName("A103FLAGREPA")
                        .HasMaxLength(2)
                        .IsUnicode(false);

                    b.Property<string>("A103flagvige")
                        .HasColumnName("A103FLAGVIGE")
                        .HasMaxLength(2)
                        .IsUnicode(false);

                    b.Property<string>("A103foliproc")
                        .HasColumnName("A103FOLIPROC")
                        .HasMaxLength(250)
                        .IsUnicode(false);

                    b.Property<string>("A103foliprod")
                        .HasColumnName("A103FOLIPROD")
                        .HasMaxLength(250)
                        .IsUnicode(false);

                    b.Property<string>("A103folipros")
                        .HasColumnName("A103FOLIPROS")
                        .HasMaxLength(250)
                        .IsUnicode(false);

                    b.Property<string>("A103horaproc")
                        .HasColumnName("A103HORAPROC")
                        .HasMaxLength(8)
                        .IsUnicode(false);

                    b.Property<string>("A103horaregi")
                        .HasColumnName("A103HORAREGI")
                        .HasMaxLength(8)
                        .IsUnicode(false);

                    b.Property<string>("A103horarepa")
                        .HasColumnName("A103HORAREPA")
                        .HasMaxLength(8)
                        .IsUnicode(false);

                    b.Property<string>("A103magiapro")
                        .HasColumnName("A103MAGIAPRO")
                        .HasMaxLength(4)
                        .IsUnicode(false);

                    b.Property<string>("A103nombpone")
                        .HasColumnName("A103NOMBPONE")
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.Property<string>("A103nuenradi")
                        .IsRequired()
                        .HasColumnName("A103NUENRADI")
                        .HasMaxLength(3)
                        .IsUnicode(false);

                    b.Property<string>("A103numeofic")
                        .HasColumnName("A103NUMEOFIC")
                        .HasMaxLength(15)
                        .IsUnicode(false);

                    b.Property<string>("A103numeproc")
                        .IsRequired()
                        .HasColumnName("A103NUMEPROC")
                        .HasMaxLength(21)
                        .IsUnicode(false);

                    b.Property<string>("A103numeradi")
                        .IsRequired()
                        .HasColumnName("A103NUMERADI")
                        .HasMaxLength(5)
                        .IsUnicode(false);

                    b.HasKey("A103llavproc");

                    b.HasIndex("A103codiproc");

                    b.HasIndex("A103codisubc");

                    b.HasIndex("A103numeproc")
                        .HasName("T103DAINFOPROC9");

                    b.HasIndex("A103numeradi", "A103nuenradi")
                        .HasName("T103DAINFOPROC7");

                    b.HasIndex("A103flagrepa", "A103ciudradi", "A103entiradi", "A103esperadi", "A103nuenradi")
                        .HasName("T103DAINFOPROC10");

                    b.HasIndex("A103codiclas", "A103llavproc", "A103ciudradi", "A103entiradi", "A103esperadi", "A103nuenradi", "A103anoradi", "A103numeradi", "A103flagrepa")
                        .HasName("T103DAINFOPROC19");

                    b.HasIndex("A103llavproc", "A103ciudradi", "A103entiradi", "A103esperadi", "A103nuenradi", "A103codiciuo", "A103codiento", "A103codiespo", "A103flagrepa", "A103consproc", "A103anoradi", "A103numeradi", "A103codinumo", "A103nombpone")
                        .HasName("T103DAINFOPROC15");

                    b.ToTable("T103DAINFOPROC");
                });

            modelBuilder.Entity("liquidador_web.Models.T112drsujeproc", b =>
                {
                    b.Property<string>("A112llavproc")
                        .HasColumnName("A112LLAVPROC")
                        .HasMaxLength(23)
                        .IsUnicode(false);

                    b.Property<string>("A112numesuje")
                        .HasColumnName("A112NUMESUJE")
                        .HasMaxLength(15)
                        .IsUnicode(false);

                    b.Property<string>("A112codisuje")
                        .HasColumnName("A112CODISUJE")
                        .HasMaxLength(4)
                        .IsUnicode(false);

                    b.Property<string>("A112cargo")
                        .HasColumnName("A112CARGO")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("A112ciudsuje")
                        .HasColumnName("A112CIUDSUJE")
                        .HasMaxLength(5)
                        .IsUnicode(false);

                    b.Property<string>("A112codicarc")
                        .HasColumnName("A112CODICARC")
                        .HasMaxLength(4)
                        .IsUnicode(false);

                    b.Property<string>("A112codiciu1")
                        .HasColumnName("A112CODICIU1")
                        .HasMaxLength(5)
                        .IsUnicode(false);

                    b.Property<string>("A112codiciud")
                        .HasColumnName("A112CODICIUD")
                        .HasMaxLength(5)
                        .IsUnicode(false);

                    b.Property<string>("A112codidocu")
                        .HasColumnName("A112CODIDOCU")
                        .HasMaxLength(2)
                        .IsUnicode(false);

                    b.Property<string>("A112codienti")
                        .HasColumnName("A112CODIENTI")
                        .HasMaxLength(2)
                        .IsUnicode(false);

                    b.Property<string>("A112codiespe")
                        .HasColumnName("A112CODIESPE")
                        .HasMaxLength(2)
                        .IsUnicode(false);

                    b.Property<string>("A112codinume")
                        .HasColumnName("A112CODINUME")
                        .HasMaxLength(3)
                        .IsUnicode(false);

                    b.Property<string>("A112codisanc")
                        .HasColumnName("A112CODISANC")
                        .HasMaxLength(4)
                        .IsUnicode(false);

                    b.Property<string>("A112codisanp")
                        .HasColumnName("A112CODISANP")
                        .HasMaxLength(4)
                        .IsUnicode(false);

                    b.Property<string>("A112consproc")
                        .IsRequired()
                        .HasColumnName("A112CONSPROC")
                        .HasMaxLength(2)
                        .IsUnicode(false);

                    b.Property<string>("A112direccio")
                        .HasColumnName("A112DIRECCIO")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<DateTime?>("A112fechfina")
                        .HasColumnName("A112FECHFINA")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("A112fechinic")
                        .HasColumnName("A112FECHINIC")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("A112fechterm")
                        .HasColumnName("A112FECHTERM")
                        .HasColumnType("datetime");

                    b.Property<string>("A112flagdete")
                        .HasColumnName("A112FLAGDETE")
                        .HasMaxLength(2)
                        .IsUnicode(false);

                    b.Property<string>("A112flagexcl")
                        .HasColumnName("A112FLAGEXCL")
                        .HasMaxLength(2)
                        .IsUnicode(false);

                    b.Property<string>("A112flagreha")
                        .HasColumnName("A112FLAGREHA")
                        .HasMaxLength(2)
                        .IsUnicode(false);

                    b.Property<string>("A112flagrevo")
                        .HasColumnName("A112FLAGREVO")
                        .HasMaxLength(2)
                        .IsUnicode(false);

                    b.Property<string>("A112flagterm")
                        .HasColumnName("A112FLAGTERM")
                        .HasMaxLength(2)
                        .IsUnicode(false);

                    b.Property<string>("A112funcabog")
                        .HasColumnName("A112FUNCABOG")
                        .HasMaxLength(1)
                        .IsUnicode(false);

                    b.Property<string>("A112idenrepr")
                        .HasColumnName("A112IDENREPR")
                        .HasMaxLength(15)
                        .IsUnicode(false);

                    b.Property<string>("A112nombrepr")
                        .HasColumnName("A112NOMBREPR")
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.Property<string>("A112nombsuje")
                        .IsRequired()
                        .HasColumnName("A112NOMBSUJE")
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.Property<int?>("A112numeanop")
                        .HasColumnName("A112NUMEANOP");

                    b.Property<int?>("A112numeanos")
                        .HasColumnName("A112NUMEANOS");

                    b.Property<int?>("A112numediap")
                        .HasColumnName("A112NUMEDIAP");

                    b.Property<int?>("A112numedias")
                        .HasColumnName("A112NUMEDIAS");

                    b.Property<int?>("A112numemese")
                        .HasColumnName("A112NUMEMESE");

                    b.Property<int?>("A112numemesp")
                        .HasColumnName("A112NUMEMESP");

                    b.Property<string>("A112numeproc")
                        .IsRequired()
                        .HasColumnName("A112NUMEPROC")
                        .HasMaxLength(21)
                        .IsUnicode(false);

                    b.Property<string>("A112obseterm")
                        .HasColumnName("A112OBSETERM")
                        .HasMaxLength(250)
                        .IsUnicode(false);

                    b.Property<string>("A112telefono")
                        .HasColumnName("A112TELEFONO")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.HasKey("A112llavproc", "A112numesuje", "A112codisuje");

                    b.HasIndex("A112codisuje")
                        .HasName("T112DRSUJEPROC20");

                    b.HasIndex("A112codisuje", "A112llavproc", "A112numesuje", "A112nombsuje")
                        .HasName("T112DRSUJEPROC12");

                    b.ToTable("T112DRSUJEPROC");
                });

            modelBuilder.Entity("liquidador_web.Models.T926liquidaciones1", b =>
                {
                    b.Property<string>("A926FECELABORA")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("A926FECELABORA");

                    b.Property<string>("A926CODUSUARIO")
                        .HasColumnName("A926CODUSUARIO")
                        .HasMaxLength(4);

                    b.Property<int>("A926CONSLIQ")
                        .HasColumnName("A926CONSLIQ")
                        .HasColumnType("int");

                    b.Property<string>("A926LIQUIDACION")
                        .HasColumnName("A926LIQUIDACION")
                        .HasColumnType("ntext");

                    b.Property<string>("A926LLAVPROC")
                        .HasColumnName("A926LLAVPROC")
                        .HasMaxLength(23);

                    b.Property<string>("A926TIPOLIQ")
                        .HasColumnName("A926TIPOLIQ")
                        .HasMaxLength(3);

                    b.HasKey("A926FECELABORA");

                    b.ToTable("T926LIQUIDACIONES1");
                });

            modelBuilder.Entity("liquidador_web.Models.TiposTasas", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnName("A922IDTasa");

                    b.Property<string>("Nombre")
                        .HasColumnName("A922NomTasa");

                    b.HasKey("ID");

                    b.ToTable("T922TIPOSTASA");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("Name")
                        .HasMaxLength(128);

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("liquidador_web.Models.T103dainfoproc", b =>
                {
                    b.HasOne("liquidador_web.Models.T053baclasgene", "A103codiclasNavigation")
                        .WithMany("T103dainfoproc")
                        .HasForeignKey("A103codiclas")
                        .HasConstraintName("FRK07_T103DAINFOPROC");

                    b.HasOne("liquidador_web.Models.T052baprocgene", "A103codiprocNavigation")
                        .WithMany("T103dainfoproc")
                        .HasForeignKey("A103codiproc")
                        .HasConstraintName("FRK06_T103DAINFOPROC");

                    b.HasOne("liquidador_web.Models.T071basubcgene", "A103codisubcNavigation")
                        .WithMany("T103dainfoproc")
                        .HasForeignKey("A103codisubc")
                        .HasConstraintName("FRK08_T103DAINFOPROC");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("liquidador_web.Models.LiquidadorUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("liquidador_web.Models.LiquidadorUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("liquidador_web.Models.LiquidadorUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("liquidador_web.Models.LiquidadorUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
