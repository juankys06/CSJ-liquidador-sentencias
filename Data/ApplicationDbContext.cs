using liquidador_web.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace liquidador_web.Data
{
    public class ApplicationDbContext : IdentityDbContext<LiquidadorUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TiposTasas> TiposTasas { get; set; }
        public virtual DbSet<Datasainte> DATASAINTE { get; set; }
        public virtual DbSet<TipoProceso> TipoProceso { get; set; }
        public virtual DbSet<Clase> Clase { get; set; }
        public virtual DbSet<SubClase> SubClase { get; set; }
        public virtual DbSet<T103dainfoproc> T103dainfoproc { get; set; }
        public virtual DbSet<T112drsujeproc> T112drsujeproc { get; set; }
        public virtual DbQuery<Procesos> Procesos { get; set; }
        public virtual DbSet<T926liquidaciones1> T926liquidaciones1 { get; set; }
        public virtual DbSet<TiposPeriodo> TiposPeriodo { get; set; }
        public virtual DbSet<TipoLiquidacion> TipoLiquidacion { get; set; }
        public virtual DbSet<Liquidaciones> Liquidaciones { get; set; }
        public virtual DbSet<Auditoria> Auditoria { get; set; }
        public virtual DbSet<DataLiquidacion> DataLiquidacion { get; set; }

        public virtual DbSet<Ciudad> Ciudad { get; set; }
        public virtual DbSet<Entidad> Entidad { get; set; }
        public virtual DbSet<Especialidad> Especialidad { get; set; }

        public virtual DbSet<Area> Area { get; set; }
        public virtual DbSet<TipoArea> TipoArea { get; set; }
        public virtual DbSet<TipoAreaClase> TipoAreaClase { get; set; }
        public virtual DbSet<TipoAreaSubClase> TipoAreaSubClase { get; set; }
        public virtual DbSet<EntidadEspecialidad> EntidadEspecialidad { get; set; }
        public virtual DbSet<Dainfosuje> Sujeto { get; set; }

        public virtual DbSet<Ayuda> Ayuda { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<TiposTasas>(entity =>
            {
                entity.ToTable("T922TIPOSTASA");

                entity.HasKey(e => e.ID);
                entity.Property(e => e.ID).HasColumnName("A922IDTasa").ValueGeneratedNever();
                entity.Property(e => e.Nombre).HasColumnName("A922NomTasa");
            });

            builder.Entity<TipoLiquidacion>(entity =>
            {
                entity.ToTable("T927TIPOLIQUID");

                entity.HasKey(e => e.codigo);
                entity.Property(e => e.codigo).HasColumnName("A927CODTIPO");
                entity.Property(e => e.nombre).HasColumnName("A927NOMBRET");
            });

            builder.Entity<TiposPeriodo>(entity =>
            {
                entity.ToTable("T923TIPOSPERIODO");

                entity.HasKey(e => e.ID);
                entity.Property(e => e.ID).HasColumnName("A923IDTipoPeriodo").ValueGeneratedNever();
                entity.Property(e => e.Nombre).HasColumnName("A923NomTipoPeriodo");
            });

            builder.Entity<Datasainte>(entity =>
            {
                entity.ToTable("T921DATASAINTE");

                entity.HasKey(e => e.IDTasa);
                entity.HasIndex(e => e.importedID).IsUnique();
                entity.Property(e => e.IDTasa).HasColumnName("A921IDTasa").ValueGeneratedOnAdd();

                entity.Property(e => e.TipoTasa).HasColumnName("A921TipoTasa").HasMaxLength(50).IsUnicode(false);
                entity.Property(e => e.VigenteDesde).HasColumnName("A921VigenteDesde").HasColumnType("datetime");
                entity.Property(e => e.VigenteHasta).HasColumnName("A921VigenteHasta").HasColumnType("datetime");
                entity.Property(e => e.ValorTasa).HasColumnType("float").HasColumnName("A921ValorTasa").IsRequired(false);
                entity.Property(e => e.Periodo).HasColumnName("A921Periodo").HasMaxLength(50).IsUnicode(false);
                entity.Property(e => e.ResVigencia).HasColumnName("A921ResVigencia").HasMaxLength(50).IsUnicode(false);
                //entity.Property(e => e.CreatedBy).HasColumnName("created_by").IsUnicode(true);
                entity.Property(e => e.importedID).HasColumnName("A921IDWebService").IsRequired(false);
            });

            builder.Entity<TipoProceso>(entity =>
            {
                entity.ToTable("T052BAPROCGENE");

                entity.HasKey(e => e.codiproc);

                entity.Property(e => e.codiproc)
                    .HasColumnName("A052CODIPROC")
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.descproc)
                    .IsRequired()
                    .HasColumnName("A052DESCPROC")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            builder.Entity<Clase>(entity =>
            {
                entity.ToTable("T053BACLASGENE");

                entity.HasKey(e => e.codiclas);

                entity.Property(e => e.codiclas)
                    .HasColumnName("A053CODICLAS")
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.descclas)
                    .IsRequired()
                    .HasColumnName("A053DESCCLAS")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            builder.Entity<SubClase>(entity =>
            {
                entity.ToTable("T071BASUBCGENE");

                entity.HasKey(e => e.codisubc);

                entity.Property(e => e.codisubc)
                    .HasColumnName("A071CODISUBC")
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.descsubc)
                    .IsRequired()
                    .HasColumnName("A071DESCSUBC")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            builder.Entity<T103dainfoproc>(entity =>
            {
                entity.ToTable("T103DAINFOPROC");

                entity.HasKey(e => e.A103llavproc);

                entity.HasIndex(e => e.A103numeproc)
                    .HasName("T103DAINFOPROC9");

                entity.HasIndex(e => new { e.A103numeradi, e.A103nuenradi })
                    .HasName("T103DAINFOPROC7");

                entity.HasIndex(e => new { e.A103flagrepa, e.A103ciudradi, e.A103entiradi, e.A103esperadi, e.A103nuenradi })
                    .HasName("T103DAINFOPROC10");

                entity.HasIndex(e => new { e.A103codiclas, e.A103llavproc, e.A103ciudradi, e.A103entiradi, e.A103esperadi, e.A103nuenradi, e.A103anoradi, e.A103numeradi, e.A103flagrepa })
                    .HasName("T103DAINFOPROC19");

                entity.HasIndex(e => new { e.A103llavproc, e.A103ciudradi, e.A103entiradi, e.A103esperadi, e.A103nuenradi, e.A103codiciuo, e.A103codiento, e.A103codiespo, e.A103flagrepa, e.A103consproc, e.A103anoradi, e.A103numeradi, e.A103codinumo, e.A103nombpone })
                    .HasName("T103DAINFOPROC15");

                entity.Property(e => e.A103llavproc)
                    .HasColumnName("A103LLAVPROC")
                    .HasMaxLength(23)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.A103anoradi)
                    .IsRequired()
                    .HasColumnName("A103ANORADI")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.A103anotactd)
                    .HasColumnName("A103ANOTACTD")
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.A103anotacts)
                    .HasColumnName("A103ANOTACTS")
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.A103anotorig)
                    .HasColumnName("A103ANOTORIG")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.A103ciudradi)
                    .IsRequired()
                    .HasColumnName("A103CIUDRADI")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.A103codiactd)
                    .HasColumnName("A103CODIACTD")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.A103codiacts)
                    .HasColumnName("A103CODIACTS")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.A103codiarea)
                    .IsRequired()
                    .HasColumnName("A103CODIAREA")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.A103codiciuo)
                    .IsRequired()
                    .HasColumnName("A103CODICIUO")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.A103codiclas)
                    .IsRequired()
                    .HasColumnName("A103CODICLAS")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.A103codiento)
                    .IsRequired()
                    .HasColumnName("A103CODIENTO")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.A103codiespo)
                    .IsRequired()
                    .HasColumnName("A103CODIESPO")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.A103codiinst)
                    .HasColumnName("A103CODIINST")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.A103codinatu)
                    .HasColumnName("A103CODINATU")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.A103codinumo)
                    .IsRequired()
                    .HasColumnName("A103CODINUMO")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.A103codipadd)
                    .HasColumnName("A103CODIPADD")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.A103codipads)
                    .HasColumnName("A103CODIPADS")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.A103codipone)
                    .HasColumnName("A103CODIPONE")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.A103codiproc)
                    .IsRequired()
                    .HasColumnName("A103CODIPROC")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.A103codiproo)
                    .HasColumnName("A103CODIPROO")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.A103codirama)
                    .HasColumnName("A103CODIRAMA")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.A103codirecu)
                    .IsRequired()
                    .HasColumnName("A103CODIRECU")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.A103codisubc)
                    .IsRequired()
                    .HasColumnName("A103CODISUBC")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.A103codiubic)
                    .HasColumnName("A103CODIUBIC")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.A103codiusua)
                    .HasColumnName("A103CODIUSUA")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.A103consnorm).HasColumnName("A103CONSNORM");

                entity.Property(e => e.A103consproc)
                    .IsRequired()
                    .HasColumnName("A103CONSPROC")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.A103cuadproc)
                    .HasColumnName("A103CUADPROC")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.A103cuadprod)
                    .HasColumnName("A103CUADPROD")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.A103cuadpros)
                    .HasColumnName("A103CUADPROS")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.A103descactd)
                    .HasColumnName("A103DESCACTD")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.A103descacts)
                    .HasColumnName("A103DESCACTS")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.A103entiradi)
                    .IsRequired()
                    .HasColumnName("A103ENTIRADI")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.A103esperadi)
                    .IsRequired()
                    .HasColumnName("A103ESPERADI")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.A103fechdesd)
                    .HasColumnName("A103FECHDESD")
                    .HasColumnType("datetime");

                entity.Property(e => e.A103fechdess)
                    .HasColumnName("A103FECHDESS")
                    .HasColumnType("datetime");

                entity.Property(e => e.A103fechfind)
                    .HasColumnName("A103FECHFIND")
                    .HasColumnType("datetime");

                entity.Property(e => e.A103fechfins)
                    .HasColumnName("A103FECHFINS")
                    .HasColumnType("datetime");

                entity.Property(e => e.A103fechinid)
                    .HasColumnName("A103FECHINID")
                    .HasColumnType("datetime");

                entity.Property(e => e.A103fechinis)
                    .HasColumnName("A103FECHINIS")
                    .HasColumnType("datetime");

                entity.Property(e => e.A103fechorig)
                    .HasColumnName("A103FECHORIG")
                    .HasColumnType("datetime");

                entity.Property(e => e.A103fechpres)
                    .HasColumnName("A103FECHPRES")
                    .HasColumnType("datetime");

                entity.Property(e => e.A103fechproc)
                    .HasColumnName("A103FECHPROC")
                    .HasColumnType("datetime");

                entity.Property(e => e.A103fechregi)
                    .HasColumnName("A103FECHREGI")
                    .HasColumnType("datetime");

                entity.Property(e => e.A103fechrepa)
                    .HasColumnName("A103FECHREPA")
                    .HasColumnType("datetime");

                entity.Property(e => e.A103flagcicd)
                    .HasColumnName("A103FLAGCICD")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.A103flagcics)
                    .HasColumnName("A103FLAGCICS")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.A103flagdete)
                    .HasColumnName("A103FLAGDETE")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.A103flagproc).HasColumnName("A103FLAGPROC");

                entity.Property(e => e.A103flagrepa)
                    .HasColumnName("A103FLAGREPA")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.A103flagvige)
                    .HasColumnName("A103FLAGVIGE")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.A103foliproc)
                    .HasColumnName("A103FOLIPROC")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.A103foliprod)
                    .HasColumnName("A103FOLIPROD")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.A103folipros)
                    .HasColumnName("A103FOLIPROS")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.A103horaproc)
                    .HasColumnName("A103HORAPROC")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.A103horaregi)
                    .HasColumnName("A103HORAREGI")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.A103horarepa)
                    .HasColumnName("A103HORAREPA")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.A103magiapro)
                    .HasColumnName("A103MAGIAPRO")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.A103nombpone)
                    .HasColumnName("A103NOMBPONE")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.A103nuenradi)
                    .IsRequired()
                    .HasColumnName("A103NUENRADI")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.A103numeofic)
                    .HasColumnName("A103NUMEOFIC")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.A103numeproc)
                    .IsRequired()
                    .HasColumnName("A103NUMEPROC")
                    .HasMaxLength(21)
                    .IsUnicode(false);

                entity.Property(e => e.A103numeradi)
                    .IsRequired()
                    .HasColumnName("A103NUMERADI")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.HasOne(d => d.A103codiclasNavigation)
                    .WithMany(p => p.T103dainfoproc)
                    .HasForeignKey(d => d.A103codiclas)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FRK07_T103DAINFOPROC");

                entity.HasOne(d => d.A103codiprocNavigation)
                    .WithMany(p => p.T103dainfoproc)
                    .HasForeignKey(d => d.A103codiproc)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FRK06_T103DAINFOPROC");

                entity.HasOne(d => d.A103codisubcNavigation)
                    .WithMany(p => p.T103dainfoproc)
                    .HasForeignKey(d => d.A103codisubc)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FRK08_T103DAINFOPROC");
            });

            builder.Entity<T112drsujeproc>(entity =>
            {
                entity.ToTable("T112DRSUJEPROC");

                entity.HasKey(e => new { e.A112llavproc, e.A112numesuje, e.A112codisuje });

                entity.HasIndex(e => e.A112codisuje)
                    .HasName("T112DRSUJEPROC20");

                entity.HasIndex(e => new { e.A112codisuje, e.A112llavproc, e.A112numesuje, e.A112nombsuje })
                    .HasName("T112DRSUJEPROC12");

                entity.Property(e => e.A112llavproc)
                    .HasColumnName("A112LLAVPROC")
                    .HasMaxLength(23)
                    .IsUnicode(false);

                entity.Property(e => e.A112numesuje)
                    .HasColumnName("A112NUMESUJE")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.A112codisuje)
                    .HasColumnName("A112CODISUJE")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.A112cargo)
                    .HasColumnName("A112CARGO")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.A112ciudsuje)
                    .HasColumnName("A112CIUDSUJE")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.A112codicarc)
                    .HasColumnName("A112CODICARC")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.A112codiciu1)
                    .HasColumnName("A112CODICIU1")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.A112codiciud)
                    .HasColumnName("A112CODICIUD")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.A112codidocu)
                    .HasColumnName("A112CODIDOCU")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.A112codienti)
                    .HasColumnName("A112CODIENTI")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.A112codiespe)
                    .HasColumnName("A112CODIESPE")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.A112codinume)
                    .HasColumnName("A112CODINUME")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.A112codisanc)
                    .HasColumnName("A112CODISANC")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.A112codisanp)
                    .HasColumnName("A112CODISANP")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.A112consproc)
                    .IsRequired()
                    .HasColumnName("A112CONSPROC")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.A112direccio)
                    .HasColumnName("A112DIRECCIO")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.A112fechfina)
                    .HasColumnName("A112FECHFINA")
                    .HasColumnType("datetime");

                entity.Property(e => e.A112fechinic)
                    .HasColumnName("A112FECHINIC")
                    .HasColumnType("datetime");

                entity.Property(e => e.A112fechterm)
                    .HasColumnName("A112FECHTERM")
                    .HasColumnType("datetime");

                entity.Property(e => e.A112flagdete)
                    .HasColumnName("A112FLAGDETE")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.A112flagexcl)
                    .HasColumnName("A112FLAGEXCL")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.A112flagreha)
                    .HasColumnName("A112FLAGREHA")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.A112flagrevo)
                    .HasColumnName("A112FLAGREVO")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.A112flagterm)
                    .HasColumnName("A112FLAGTERM")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.A112funcabog)
                    .HasColumnName("A112FUNCABOG")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.A112idenrepr)
                    .HasColumnName("A112IDENREPR")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.A112nombrepr)
                    .HasColumnName("A112NOMBREPR")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.A112nombsuje)
                    .IsRequired()
                    .HasColumnName("A112NOMBSUJE")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.A112numeanop).HasColumnName("A112NUMEANOP");

                entity.Property(e => e.A112numeanos).HasColumnName("A112NUMEANOS");

                entity.Property(e => e.A112numediap).HasColumnName("A112NUMEDIAP");

                entity.Property(e => e.A112numedias).HasColumnName("A112NUMEDIAS");

                entity.Property(e => e.A112numemese).HasColumnName("A112NUMEMESE");

                entity.Property(e => e.A112numemesp).HasColumnName("A112NUMEMESP");

                entity.Property(e => e.A112numeproc)
                    .IsRequired()
                    .HasColumnName("A112NUMEPROC")
                    .HasMaxLength(21)
                    .IsUnicode(false);

                entity.Property(e => e.A112obseterm)
                    .HasColumnName("A112OBSETERM")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.A112telefono)
                    .HasColumnName("A112TELEFONO")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            builder.Entity<T926liquidaciones1>(entity =>
            {
                entity.ToTable("T926LIQUIDACIONES1");

                entity.HasKey(e => e.A926FECELABORA);
                entity.Property(e => e.A926FECELABORA).HasColumnName("A926FECELABORA");

                entity.Property(e => e.A926LLAVPROC).HasColumnName("A926LLAVPROC").HasMaxLength(23);
                entity.Property(e => e.A926TIPOLIQ).HasColumnName("A926TIPOLIQ").HasMaxLength(3);
                entity.Property(e => e.A926CONSLIQ).HasColumnName("A926CONSLIQ").HasColumnType("int");
                entity.Property(e => e.A926LIQUIDACION).HasColumnName("A926LIQUIDACION").HasColumnType("ntext");
                entity.Property(e => e.A926CODUSUARIO).HasColumnName("A926CODUSUARIO").HasMaxLength(4);
            });

            builder.Entity<Area>(entity => {
                entity.ToTable("T069BAAREAGENE");

                entity.Property(e => e.codiarea).HasColumnName("A069CODIAREA").HasMaxLength(4);
                entity.Property(e => e.descarea).HasColumnName("A069DESCAREA").HasMaxLength(4);

                entity.HasKey(e => e.codiarea);
            });

            builder.Entity<TipoArea>(entity => {
                entity.ToTable("T090BRPROCAREA");

                entity.Property(e => e.CODIAREA).HasColumnName("A090CODIAREA").HasMaxLength(4);
                entity.Property(e => e.CODIPROC).HasColumnName("A090CODIPROC").HasMaxLength(4);

                entity.HasKey(e => new { e.CODIAREA, e.CODIPROC });
            });

            builder.Entity<TipoAreaClase>(entity => {
                entity.ToTable("T091BRCLASPROC");

                entity.Property(e => e.codiarea).HasColumnName("A091CODIAREA").HasMaxLength(4);
                entity.Property(e => e.codiproc).HasColumnName("A091CODIPROC").HasMaxLength(4);
                entity.Property(e => e.codiclas).HasColumnName("A091CODICLAS").HasMaxLength(4);

                entity.HasKey(e => new { e.codiarea, e.codiclas, e.codiproc });
            });

            builder.Entity<TipoAreaSubClase>(entity => {
                entity.ToTable("T092BRSUBCCLAS");

                entity.Property(e => e.codiarea).HasColumnName("A092CODIAREA").HasMaxLength(4);
                entity.Property(e => e.codiproc).HasColumnName("A092CODIPROC").HasMaxLength(4);
                entity.Property(e => e.codiclas).HasColumnName("A092CODICLAS").HasMaxLength(4);
                entity.Property(e => e.codisubc).HasColumnName("A092CODISUBC").HasMaxLength(4);

                entity.HasKey(e => new { e.codiarea, e.codiclas, e.codiproc, e.codisubc });
            });

            builder.Entity<Entidad>(entity => {
                entity.ToTable("T051BAENTIGENE");

                entity.Property(e => e.codienti).HasColumnName("A051CODIENTI").HasMaxLength(2);
                entity.Property(e => e.descenti).HasColumnName("A051DESCENTI").HasMaxLength(50);

                entity.HasKey(e => e.codienti);
            });

            builder.Entity<Especialidad>(entity => {
                entity.ToTable("T062BAESPEGENE");

                entity.Property(e => e.codiespe).HasColumnName("A062CODIESPE").HasMaxLength(2);
                entity.Property(e => e.descespe).HasColumnName("A062DESCESPE").HasMaxLength(50);

                entity.HasKey(e => e.codiespe);
            });

            builder.Entity<Ciudad>(entity => {
                entity.ToTable("T065BACIUDGENE");

                entity.Property(e => e.codiciud).HasColumnName("A065CODICIUD").HasMaxLength(5);
                entity.Property(e => e.descciud).HasColumnName("A065DESCCIUD").HasMaxLength(50);

                entity.HasKey(e => e.codiciud);
            });

            builder.Entity<EntidadEspecialidad>(entity => {
                entity.ToTable("T081BRESPEENTI");

                entity.Property(e => e.ciudad).HasColumnName("A081CODICIUD").HasMaxLength(5);
                entity.Property(e => e.entidad).HasColumnName("A081CODIENTI").HasMaxLength(2);
                entity.Property(e => e.especialidad).HasColumnName("A081CODIESPE").HasMaxLength(2);
                entity.Property(e => e.despacho).HasColumnName("A081CODINUME").HasMaxLength(3);
                entity.Property(e => e.consradi).HasColumnName("A081CONSRADI");
                entity.Property(e => e.flagdesp).HasColumnName("A081FLAGDESP").HasMaxLength(2);

                entity.HasKey(e => new { e.ciudad, e.entidad, e.especialidad, e.despacho });
            });

            builder.Entity<Dainfosuje>(entity => {
                entity.ToTable("T102DAINFOSUJE");
                entity.Property(e => e.numesuje).HasColumnName("A102NUMESUJE").HasMaxLength(15);
                entity.Property(e => e.nombsuje).HasColumnName("A102NOMBSUJE").HasMaxLength(100);
                entity.Property(e => e.docusuje).HasColumnName("A102DOCUSUJE").HasMaxLength(2);
                entity.Property(e => e.ciudsuje).HasColumnName("A102CIUDSUJE").HasMaxLength(5);
                entity.Property(e => e.dir1suje).HasColumnName("A102DIR1SUJE").HasMaxLength(200);
                entity.Property(e => e.dir2suje).HasColumnName("A102DIR2SUJE").HasMaxLength(200);
                entity.Property(e => e.tel1suje).HasColumnName("A102TEL1SUJE").HasMaxLength(30);
                entity.Property(e => e.fax1suje).HasColumnName("A102FAX1SUJE").HasMaxLength(30);
                entity.Property(e => e.ciu1suje).HasColumnName("A102CIU1SUJE").HasMaxLength(5);
                entity.Property(e => e.ciu2suje).HasColumnName("A102CIU2SUJE").HasMaxLength(5);
                entity.Property(e => e.tarjprof).HasColumnName("A102TARJPROF").HasMaxLength(20);
                entity.Property(e => e.provdefi).HasColumnName("A102PROVDEFI").HasMaxLength(1);
                entity.Property(e => e.flagsanc).HasColumnName("A102FLAGSANC").HasMaxLength(2);
            });

            builder.Entity<Ayuda>(entity => {
                entity.ToTable("AYUDA");
                
                entity.Property(e => e.fecha).HasColumnName("ID").HasColumnType("int");
                entity.Property(e => e.titulo).HasColumnName("TITULO").HasMaxLength(500);
                entity.Property(e => e.urlDocumento).HasColumnName("URL").HasMaxLength(500);
                entity.Property(e => e.fecha).HasColumnName("FECHA").HasColumnType("datetime");
                entity.Property(e => e.roles).HasColumnName("ROLES").HasMaxLength(500);
                
                entity.HasKey(e => e.id);
            });

            //DbSeeds.RoleSeeder(builder);
        }
    }
}
