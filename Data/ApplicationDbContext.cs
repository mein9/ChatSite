﻿using AspNetCoreTodo.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreTodo.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<ChatMessage> ChatMessages { get; set; } // Add this line

    override protected void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}