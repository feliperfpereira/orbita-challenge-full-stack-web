using StudentsApi.Models;
using StudentsApi.Controllers;
using StudentsApi.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentsTest
{
    public class DummyDataInitializer
    {
        public DummyDataInitializer()
        {

        }

        public void Seed(AppDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.Students.AddRange(
                new Student() { CPF = "41447663810", Email = "teste@teste.com", Nome = "Teste Nome", RA = "0000"  },
                new Student() { CPF = "41447663810", Email = "teste1@teste.com", Nome = "Teste1 Nome", RA = "0001"  },
                new Student() { CPF = "41447663810", Email = "teste2@teste.com", Nome = "Teste2 Nome", RA = "0002"  },
                new Student() { CPF = "41447663810", Email = "teste3@teste.com", Nome = "Teste3 Nome", RA = "0003"  }
            );
            
            context.SaveChanges();
        }
    }
}