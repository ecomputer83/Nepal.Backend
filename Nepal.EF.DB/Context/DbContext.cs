using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Nepal.EF.DB.DataObject;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nepal.EF.DB.Context
{
    public class CoreContext : IdentityDbContext<User>
    {
        public CoreContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<MailMessage>(entity =>
            {
                entity.Property(e => e.MailMessageId)
                    .HasColumnName("MailMessageID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.EntityId)
                    .HasColumnName("EntityID")
                    .HasMaxLength(50);

                entity.Property(e => e.EntityType).HasMaxLength(50);

                entity.Property(e => e.ErrorMessage).HasMaxLength(500);

                entity.Property(e => e.MailBody).IsRequired();

                entity.Property(e => e.MailSubject)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.ModifiedBy)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Priority)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Template).HasMaxLength(50);
            });

            builder.Entity<MailMessageAddress>(entity =>
            {
                entity.Property(e => e.MailMessageAddressId)
                    .HasColumnName("MailMessageAddressID")
                    .ValueGeneratedNever();

                entity.Property(e => e.AddressType)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.DisplayName).HasMaxLength(200);

                entity.Property(e => e.EmailAddress).HasMaxLength(200);

                entity.Property(e => e.MailMessageId).HasColumnName("MailMessageID");

                entity.HasOne(d => d.MailMessage)
                    .WithMany(p => p.MailMessageAddress)
                    .HasForeignKey(d => d.MailMessageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MailMessageAddress_MailMessage");
            });

            builder.Entity<MailMessageAttachment>(entity =>
            {
                entity.Property(e => e.MailMessageAttachmentId)
                    .HasColumnName("MailMessageAttachmentID")
                    .ValueGeneratedNever();

                entity.Property(e => e.DocumentId).HasColumnName("DocumentID");

                entity.Property(e => e.MailMessageId).HasColumnName("MailMessageID");

                entity.HasOne(d => d.MailMessage)
                    .WithMany(p => p.MailMessageAttachment)
                    .HasForeignKey(d => d.MailMessageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MailMessageAttachment_MailMessage");
            });

            builder.Entity<MailMessageQueue>(entity =>
            {
                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.EntityId)
                    .HasColumnName("EntityID")
                    .HasMaxLength(100);

                entity.Property(e => e.EntityType).HasMaxLength(100);

                entity.Property(e => e.Payload)
                    .IsRequired()
                    .HasColumnType("xml");

                entity.Property(e => e.Subject).HasMaxLength(100);

                entity.Property(e => e.Template).HasMaxLength(100);
            });
        }
        public virtual DbSet<Article> Articles { get; set; }
        public virtual DbSet<Credit> Credits { get; set; }
        public virtual DbSet<Depot> Depots { get; set; }
        public virtual DbSet<Marketer> Marketers { get; set; }
        public virtual DbSet<MarketerCustomer> MarketerCustomers { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderCredit> OrderCredits { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Program> Programs { get; set; }
        public virtual DbSet<SalesPrice> SalesPrices { get; set; }
        public virtual DbSet<MailMessage> MailMessage { get; set; }
        public virtual DbSet<MailMessageAddress> MailMessageAddress { get; set; }
        public virtual DbSet<MailMessageAttachment> MailMessageAttachment { get; set; }
        public virtual DbSet<MailMessageQueue> MailMessageQueue { get; set; }
        public virtual DbSet<MailTemplate> MailTemplate { get; set; }
    }
}
