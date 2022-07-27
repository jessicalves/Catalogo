using System;
using Catalogo.Model;
using Microsoft.EntityFrameworkCore;

namespace Catalogo.DataBase
{
    public class BancoContext : DbContext
    {
        public DbSet<Produto> Produtos { get; set; }

        public BancoContext()
        {
        }
    }
}

