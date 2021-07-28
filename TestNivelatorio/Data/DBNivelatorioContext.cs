using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore;
using TestNivelatorio.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TestNivelatorio.Data
{
    public class DBNivelatorioContext : DbContext
    {
        

        public DBNivelatorioContext(DbContextOptions<DBNivelatorioContext> options): base(options)
        {

        }
        public DbSet<Vehiculo> Vehiculos { get; set; }


    }



}
