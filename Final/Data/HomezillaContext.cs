using System;
using System.Collections.Generic;
using Final.Entities;
using Final.Model;
using Microsoft.EntityFrameworkCore;

namespace Final.Data;

public partial class HomezillaContext : DbContext
{
    public HomezillaContext()
    {
    }

    public HomezillaContext(DbContextOptions<HomezillaContext> options)
        : base(options)
    {
    }

    //Creating DbSet

    public DbSet<Customer> customer { get; set; }
    public DbSet<orderDetails> orderDetails { get; set; }
  

    public DbSet<providerDetails> providerDetails { get; set; }

    public DbSet<providerServices> providerServices { get; set; }  


    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
