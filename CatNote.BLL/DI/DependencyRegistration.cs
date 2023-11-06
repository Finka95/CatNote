using CatNote.DAL.Entities;
using CatNote.DAL.Repositories.Interfaces;
using CatNote.DAL.Repositories;
using CatNote.DAL;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatNote.DAL.DI;

namespace CatNote.BLL.DI;
public static class DependencyRegistration
{
    public static void AddBusinessServices(this IServiceCollection services, string connectionString)
    {
        services.AddDatabaseServices(connectionString);
    }
}
