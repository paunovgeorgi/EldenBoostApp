using EldenBoost.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EldenBoost.Infrastructure.Data.Configurations
{
    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.HasData(GenerateAuthors());
        }

        private Author[] GenerateAuthors()
        {
            ICollection<Author> authors = new HashSet<Author>();

            Author author;

            author = new Author
            {
                Id = 1,
                Country = "Tatooine",
                UserId = "362456a6-e52a-4065-a619-7af22c96e1e1" 
            };
            authors.Add(author);

            author = new Author
            {
                Id = 2,
                Country = "Coruscant",
                UserId = "851792db-67bb-4b08-8c03-ac2643a0600a" 
            };
            authors.Add(author);

            return authors.ToArray();
        }
    }
}
