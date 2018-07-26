﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Server.Models;

namespace ConferenceBarrelServer.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20180725160758_Conference")]
    partial class Conference
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846");

            modelBuilder.Entity("Server.Models.Conference", b =>
                {
                    b.Property<int>("ConferenceId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<decimal>("TicketCost");

                    b.Property<string>("Title");

                    b.HasKey("ConferenceId");

                    b.ToTable("Conferences");
                });
#pragma warning restore 612, 618
        }
    }
}
