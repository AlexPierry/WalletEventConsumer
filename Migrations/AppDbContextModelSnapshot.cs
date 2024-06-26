﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WalletEventConsumer.Data;

#nullable disable

namespace WalletEventConsumer.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("WalletEventConsumer.Model.BalanceModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("PayloadId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PayloadId");

                    b.ToTable("BalanceEvents");
                });

            modelBuilder.Entity("WalletEventConsumer.Model.Payload", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("AccountIdFrom")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasAnnotation("Relational:JsonPropertyName", "account_id_from");

                    b.Property<string>("AccountIdTo")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasAnnotation("Relational:JsonPropertyName", "account_id_to");

                    b.Property<double>("BalanceAccountIdFrom")
                        .HasColumnType("double")
                        .HasAnnotation("Relational:JsonPropertyName", "balance_account_id_from");

                    b.Property<double>("BalanceAccountIdTo")
                        .HasColumnType("double")
                        .HasAnnotation("Relational:JsonPropertyName", "balance_account_id_to");

                    b.HasKey("Id");

                    b.ToTable("Payload");
                });

            modelBuilder.Entity("WalletEventConsumer.Model.BalanceModel", b =>
                {
                    b.HasOne("WalletEventConsumer.Model.Payload", "Payload")
                        .WithMany()
                        .HasForeignKey("PayloadId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Payload");
                });
#pragma warning restore 612, 618
        }
    }
}
