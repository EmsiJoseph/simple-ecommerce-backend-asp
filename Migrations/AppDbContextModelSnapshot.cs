﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using api.Data;

#nullable disable

namespace api.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("api.Data.Models.Account", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("account_status")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)");

                    b.Property<DateTime>("created_at")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("deleted_at")
                        .HasColumnType("datetime2");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("is_deleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("last_login")
                        .HasColumnType("datetime2");

                    b.Property<string>("password_hash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("role")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)");

                    b.Property<DateTime>("updated_at")
                        .HasColumnType("datetime2");

                    b.Property<int>("user_id")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("user_id")
                        .IsUnique();

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("api.Data.Models.Card", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("card_brand")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("card_token")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("created_at")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<int>("customer_id")
                        .HasColumnType("int");

                    b.Property<int?>("expiration_month")
                        .HasColumnType("int");

                    b.Property<int?>("expiration_year")
                        .HasColumnType("int");

                    b.Property<bool>("is_default")
                        .HasColumnType("bit");

                    b.Property<string>("last4")
                        .HasMaxLength(4)
                        .IsUnicode(false)
                        .HasColumnType("varchar(4)");

                    b.Property<DateTime>("updated_at")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.HasKey("id")
                        .HasName("PK__cards__3213E83F6207BED5");

                    b.HasIndex("customer_id");

                    b.ToTable("Cards");
                });

            modelBuilder.Entity("api.Data.Models.Cart", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<DateTime>("created_at")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<int>("customer_id")
                        .HasColumnType("int");

                    b.Property<string>("status")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)");

                    b.Property<DateTime>("updated_at")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.HasKey("id")
                        .HasName("PK__cart__3213E83F3DF31592");

                    b.HasIndex("customer_id");

                    b.ToTable("cart", (string)null);
                });

            modelBuilder.Entity("api.Data.Models.CartItem", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int>("cart_id")
                        .HasColumnType("int");

                    b.Property<DateTime>("created_at")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<int>("product_id")
                        .HasColumnType("int");

                    b.Property<int>("qty")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.Property<DateTime>("updated_at")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.HasKey("id")
                        .HasName("PK__cart_ite__3213E83F64EB7828");

                    b.HasIndex("cart_id");

                    b.HasIndex("product_id");

                    b.ToTable("CartItems");
                });

            modelBuilder.Entity("api.Data.Models.Category", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<DateTime>("created_at")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<DateTime?>("deleted_at")
                        .HasColumnType("datetime");

                    b.Property<string>("description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("is_deleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<int?>("parent_id")
                        .HasColumnType("int");

                    b.Property<DateTime>("updated_at")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.HasKey("id")
                        .HasName("PK__categori__3213E83F0E518CA1");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("api.Data.Models.Order", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int?>("card_id")
                        .HasColumnType("int");

                    b.Property<DateTime>("created_at")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<int>("customer_id")
                        .HasColumnType("int");

                    b.Property<DateTime?>("deleted_at")
                        .HasColumnType("datetime");

                    b.Property<bool>("is_deleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("shipping_address_line1")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("shipping_address_line2")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("shipping_city")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("shipping_country")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("shipping_state")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("shipping_zip_code")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("status")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)");

                    b.Property<DateTime>("updated_at")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.HasKey("id")
                        .HasName("PK__orders__3213E83F15B048FF");

                    b.HasIndex("card_id");

                    b.HasIndex("customer_id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("api.Data.Models.OrderItem", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<DateTime>("created_at")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<DateTime?>("deleted_at")
                        .HasColumnType("datetime");

                    b.Property<bool>("is_deleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<int>("order_id")
                        .HasColumnType("int");

                    b.Property<int>("product_id")
                        .HasColumnType("int");

                    b.Property<int>("qty")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.Property<DateTime>("updated_at")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.HasKey("id")
                        .HasName("PK__order_it__3213E83F3D1FF0A8");

                    b.HasIndex("order_id");

                    b.HasIndex("product_id");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("api.Data.Models.Product", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int?>("category_id")
                        .HasColumnType("int");

                    b.Property<DateTime>("created_at")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<DateTime?>("deleted_at")
                        .HasColumnType("datetime2");

                    b.Property<string>("description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("is_deleted")
                        .HasColumnType("bit");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("price")
                        .HasColumnType("decimal(18, 0)");

                    b.Property<int>("stock")
                        .HasColumnType("int");

                    b.Property<DateTime>("updated_at")
                        .HasColumnType("datetime2");

                    b.HasKey("id")
                        .HasName("PK__products__3213E83FEE7E3AB1");

                    b.HasIndex("category_id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("api.Data.Models.ProductImage", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<DateTime>("created_at")
                        .HasColumnType("datetime2");

                    b.Property<string>("image_url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("is_main")
                        .HasColumnType("bit");

                    b.Property<int>("product_id")
                        .HasColumnType("int");

                    b.Property<int>("productid")
                        .HasColumnType("int");

                    b.Property<DateTime>("updated_at")
                        .HasColumnType("datetime2");

                    b.HasKey("id");

                    b.HasIndex("productid");

                    b.ToTable("ProductImages");
                });

            modelBuilder.Entity("api.Data.Models.User", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("address_line1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("address_line2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("city")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("created_at")
                        .HasColumnType("datetime2");

                    b.Property<DateOnly?>("date_of_birth")
                        .HasColumnType("date");

                    b.Property<DateTime?>("deleted_at")
                        .HasColumnType("datetime2");

                    b.Property<string>("first_name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("is_deleted")
                        .HasColumnType("bit");

                    b.Property<string>("last_name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("state")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("updated_at")
                        .HasColumnType("datetime2");

                    b.Property<string>("zip_code")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("api.Data.Models.Account", b =>
                {
                    b.HasOne("api.Data.Models.User", "user")
                        .WithOne("account")
                        .HasForeignKey("api.Data.Models.Account", "user_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("user");
                });

            modelBuilder.Entity("api.Data.Models.Card", b =>
                {
                    b.HasOne("api.Data.Models.User", "customer")
                        .WithMany("cards")
                        .HasForeignKey("customer_id")
                        .IsRequired()
                        .HasConstraintName("FK_cards_customer");

                    b.Navigation("customer");
                });

            modelBuilder.Entity("api.Data.Models.Cart", b =>
                {
                    b.HasOne("api.Data.Models.User", "customer")
                        .WithMany("carts")
                        .HasForeignKey("customer_id")
                        .IsRequired()
                        .HasConstraintName("FK_cart_customer");

                    b.Navigation("customer");
                });

            modelBuilder.Entity("api.Data.Models.CartItem", b =>
                {
                    b.HasOne("api.Data.Models.Cart", "cart")
                        .WithMany("cart_items")
                        .HasForeignKey("cart_id")
                        .IsRequired()
                        .HasConstraintName("FK_cart_items_cart");

                    b.HasOne("api.Data.Models.Product", "product")
                        .WithMany("cart_items")
                        .HasForeignKey("product_id")
                        .IsRequired()
                        .HasConstraintName("FK_cart_items_product");

                    b.Navigation("cart");

                    b.Navigation("product");
                });

            modelBuilder.Entity("api.Data.Models.Order", b =>
                {
                    b.HasOne("api.Data.Models.Card", "card")
                        .WithMany("orders")
                        .HasForeignKey("card_id")
                        .HasConstraintName("FK_orders_card");

                    b.HasOne("api.Data.Models.User", "customer")
                        .WithMany("orders")
                        .HasForeignKey("customer_id")
                        .IsRequired()
                        .HasConstraintName("FK_orders_customer");

                    b.Navigation("card");

                    b.Navigation("customer");
                });

            modelBuilder.Entity("api.Data.Models.OrderItem", b =>
                {
                    b.HasOne("api.Data.Models.Order", "order")
                        .WithMany("order_items")
                        .HasForeignKey("order_id")
                        .IsRequired()
                        .HasConstraintName("FK_order_items_order");

                    b.HasOne("api.Data.Models.Product", "product")
                        .WithMany("order_items")
                        .HasForeignKey("product_id")
                        .IsRequired()
                        .HasConstraintName("FK_order_items_product");

                    b.Navigation("order");

                    b.Navigation("product");
                });

            modelBuilder.Entity("api.Data.Models.Product", b =>
                {
                    b.HasOne("api.Data.Models.Category", "category")
                        .WithMany("products")
                        .HasForeignKey("category_id");

                    b.Navigation("category");
                });

            modelBuilder.Entity("api.Data.Models.ProductImage", b =>
                {
                    b.HasOne("api.Data.Models.Product", "product")
                        .WithMany("product_images")
                        .HasForeignKey("productid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("product");
                });

            modelBuilder.Entity("api.Data.Models.Card", b =>
                {
                    b.Navigation("orders");
                });

            modelBuilder.Entity("api.Data.Models.Cart", b =>
                {
                    b.Navigation("cart_items");
                });

            modelBuilder.Entity("api.Data.Models.Category", b =>
                {
                    b.Navigation("products");
                });

            modelBuilder.Entity("api.Data.Models.Order", b =>
                {
                    b.Navigation("order_items");
                });

            modelBuilder.Entity("api.Data.Models.Product", b =>
                {
                    b.Navigation("cart_items");

                    b.Navigation("order_items");

                    b.Navigation("product_images");
                });

            modelBuilder.Entity("api.Data.Models.User", b =>
                {
                    b.Navigation("account");

                    b.Navigation("cards");

                    b.Navigation("carts");

                    b.Navigation("orders");
                });
#pragma warning restore 612, 618
        }
    }
}
