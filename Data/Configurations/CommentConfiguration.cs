﻿using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations;

public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.Property(c => c.Name)
            .HasMaxLength(50);

        builder.Property(c => c.Body)
            .HasMaxLength(2000);

        builder.HasCheckConstraint($"CK_{nameof(Comment)}_{nameof(Comment.ParentId)}",
            $"[{nameof(Comment.ParentId)}] != [{nameof(Comment.Id)}]");
    }
}