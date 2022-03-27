using DestekAybey.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JWTAuthentication.NET6._0.Auth
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(){}
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){}
        public DbSet<LecErrorCode> LecErrorCodes  { get; set; }
        public DbSet<LecFaq> LecFaqs { get; set; }
        public DbSet<LecLangHtmlTitle> LecLangHtmlTitles { get; set; }
        public DbSet<LecLanguage> LecLanguages { get; set; }
        public DbSet<LecProductSerie> LecProductSeries { get; set; }
        public DbSet<LecErrorCodeProductSerie> LecErrorCodeProductSeries { get; set; } 
    
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<LecErrorCodeProductSerie>()
                .HasKey(ep => new {ep.ErrorCodeId, ep.ProductSerieId});
            builder.Entity<LecErrorCodeProductSerie>()
                .HasOne(e => e.LecErrorCode)
                .WithMany(ep => ep.LecErrorCodeProductSeries)
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey(e => e.ErrorCodeId);
            builder.Entity<LecErrorCodeProductSerie>()
                .HasOne(p => p.LecProductSerie)
                .WithMany(ep => ep.LecErrorCodeProductSeries)
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey(P => P.ProductSerieId);
            
            base.OnModelCreating(builder);

            builder.Entity<IdentityUser>(u =>
            {
                u.Property<string>("Id").HasColumnName("id");
                u.Property<string>("UserName").HasColumnName("user_name");
                u.Property<string>("NormalizedUserName").HasColumnName("normalized_user_name");
                u.Property<string>("Email").HasColumnName("email");
                u.Property<string>("NormalizedEmail").HasColumnName("normalized_email");
                u.Property<bool>("EmailConfirmed").HasColumnName("email_confirmed");
                u.Property<string>("PasswordHash").HasColumnName("password_hash");
                u.Property<string>("SecurityStamp").HasColumnName("security_stamp");
                u.Property<string>("ConcurrencyStamp").HasColumnName("concurrency_stamp");
                u.Property<string>("PhoneNumber").HasColumnName("phone_number");
                u.Property<bool>("PhoneNumberConfirmed").HasColumnName("phone_number_confirmed");
                u.Property<bool>("TwoFactorEnabled").HasColumnName("two_factor_enabled");
                u.Property<DateTimeOffset?>("LockoutEnd").HasColumnName("lockout_end");
                u.Property<bool>("LockoutEnabled").HasColumnName("lockout_enabled");
                u.Property<int>("AccessFailedCount").HasColumnName("access_failed_count");
                u.HasIndex("NormalizedEmail").HasDatabaseName("idx_asp_net_users_email");
                u.HasIndex("NormalizedUserName").HasDatabaseName("idx_asp_net_users_user_name");
                u.ToTable("asp_net_users");
            });         

            builder.Entity<IdentityRole>(r =>
            {
                r.Property<string>("Id").HasColumnName("id");
                r.Property<string>("ConcurrencyStamp").HasColumnName("concurrency_stamp");
                r.Property<string>("Name").HasColumnName("name");
                r.Property<string>("NormalizedName").HasColumnName("normalized_name");
                r.HasIndex("NormalizedName").HasDatabaseName("idx_asp_net_roles_role_name");
                r.ToTable("asp_net_roles");
            });

            builder.Entity<IdentityRoleClaim<string>>(rc =>
            {
                rc.Property<int>("Id").HasColumnName("id");
                rc.Property<string>("ClaimType").HasColumnName("claim_type");
                rc.Property<string>("ClaimValue").HasColumnName("claim_value");
                rc.Property<string>("RoleId").HasColumnName("role_id");
                rc.HasIndex("RoleId").HasDatabaseName("idx_asp_net_role_claims_role_id");
                rc.ToTable("asp_net_role_claims");
            });

            builder.Entity<IdentityUserToken<string>>(ut =>
            {
                ut.Property<string>("UserId").HasColumnName("user_id");
                ut.Property<string>("LoginProvider").HasColumnName("login_provider");
                ut.Property<string>("Name").HasColumnName("name");
                ut.Property<string>("Value").HasColumnName("value");
                ut.ToTable("asp_net_user_tokens");
            });

            builder.Entity<IdentityUserRole<string>>(ur =>
            {
                ur.Property<string>("UserId").HasColumnName("user_id");
                ur.Property<string>("RoleId").HasColumnName("role_id");
                ur.HasIndex("RoleId").HasDatabaseName("idx_asp_net_user_roles_role_id");
                ur.ToTable("asp_net_user_roles");
            });

            builder.Entity<IdentityUserLogin<string>>(ul =>
            {
                ul.Property<string>("LoginProvider").HasColumnName("login_provider");
                ul.Property<string>("ProviderKey").HasColumnName("provider_key");
                ul.Property<string>("ProviderDisplayName").HasColumnName("provider_display_name");
                ul.Property<string>("UserId").HasColumnName("user_id");
                ul.HasIndex("UserId").HasDatabaseName("idx_asp_net_user_logins_user_id");
                ul.ToTable("asp_net_user_logins");
            });

            builder.Entity<IdentityUserClaim<string>>(uc =>
            {
                uc.Property<int>("Id").HasColumnName("id");
                uc.Property<string>("ClaimType").HasColumnName("claim_type");
                uc.Property<string>("ClaimValue").HasColumnName("claim_value");
                uc.Property<string>("UserId").HasColumnName("user_id");
                uc.HasIndex("UserId").HasDatabaseName("idx_asp_net_user_claims_user_id");
                uc.ToTable("asp_net_user_claims");
            });

            builder.Entity<LecErrorCode>(ec => {
                ec.Property<int>("Id").HasColumnName("id");
                ec.Property<DateTime?>("CreatedAt").HasColumnName("created_at");
                ec.Property<int>("ErrorCode").HasColumnName("error_code");
                ec.Property<string>("ErrorExplanation").HasColumnName("error_explanation");
                ec.Property<string>("ErrorName").HasColumnName("error_name");
                ec.Property<int>("LecLanguageId").HasColumnName("lec_language_id");
                ec.Property<DateTime?>("UpdatedAt").HasColumnName("updated_at");
                ec.HasIndex("LecLanguageId").HasDatabaseName("idx_lec_error_code_lec_language_id");
                ec.ToTable("lec_error_codes");
            });

            builder.Entity<LecFaq>(f =>{
                f.Property<int>("Id").HasColumnName("id");
                f.Property<string>("Content").HasColumnName("content");
                f.Property<DateTime?>("CreatedAt").HasColumnName("created_at");
                f.Property<int>("LecLanguageId").HasColumnName("lec_language_id");
                f.Property<int>("LecProductSerieId").HasColumnName("lec_product_serie_id");
                f.Property<string>("Title").HasColumnName("title");
                f.Property<DateTime?>("UpdatedAt").HasColumnName("updated_at");
                f.HasIndex("LecLanguageId").HasDatabaseName("idx_lec_faq_lec_language_id");
                f.HasIndex("LecProductSerieId").HasDatabaseName("idx_lec_faq_product_seri_id");
                f.ToTable("lec_faqs");
            });

            builder.Entity<LecLangHtmlTitle>(h =>{
                h.Property<int>("Id").HasColumnName("id");
                h.Property<DateTime?>("CreatedAt").HasColumnName("created_at");
                h.Property<string>("Key").HasColumnName("key");
                h.Property<int>("LecLanguageId").HasColumnName("lec_language_id");
                h.Property<string>("Name").HasColumnName("name");
                h.Property<DateTime?>("UpdatedAt").HasColumnName("updated_at");
                h.Property<string>("Value").HasColumnName("value");
                h.HasIndex("LecLanguageId").HasDatabaseName("idx_lec_lang_html_title_lec_language_id");
                h.ToTable("lec_lang_html_titles");
                
            });
            
            builder.Entity<LecLanguage>(l =>{
                l.Property<int>("Id").HasColumnName("id");
                l.Property<DateTime?>("CreatedAt").HasColumnName("created_at");
                l.Property<string>("Language").HasColumnName("language");
                l.Property<DateTime?>("UpdatedAt").HasColumnName("updated_at");
                l.ToTable("lec_language");
            });

            builder.Entity<LecProductSerie>(p =>{
                p.Property<int>("Id").HasColumnName("id");
                p.Property<DateTime?>("CreatedAt").HasColumnName("created_at");
                p.Property<string>("ProductSerie").HasColumnName("product_serie");
                p.Property<DateTime?>("UpdatedAt").HasColumnName("updated_at");
                p.ToTable("lec_product_series");
            });

            builder.Entity("DestekAybey.Models.LecErrorCodeProductSerie", b =>
                {
                    b.Property<int>("ErrorCodeId").HasColumnName("error_code_id");
                    b.Property<int>("ProductSerieId").HasColumnName("product_serie_id");
                    b.HasIndex("ProductSerieId").HasDatabaseName("idx_lec_error_code_product_series_product_serie_id");
                    b.ToTable("lec_error_code_product_series");
                });
        }
    }
}