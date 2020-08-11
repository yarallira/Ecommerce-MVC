using ECommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ECommerce.Classes
{
    public class CombosHelper : IDisposable
    {
        public static ECommerceContext db = new ECommerceContext();

        public static List<Departaments> GetDepartaments()
        {
            var dep = db.Departaments.ToList();
            dep.Add(new Departaments
            {
                DepartamentsID = 0,
                Name = "[Selecione um departamento]"
            });
            return dep = dep.OrderBy(d => d.Name).ToList();
        }

        public static List<City> GetCities()
        {
            var dep = db.Cities.ToList();
            dep.Add(new City
            {
                DepartamentsID = 0,
                Name = "[Selecione uma cidade]"
            });
            return dep = dep.OrderBy(d => d.Name).ToList();
        }

        public static List<Company> GetCompanys()
        {
            var comp = db.Companies.ToList();
            comp.Add(new Company
            {
                CompanyID = 0,
                Name = "[Selecione uma companhia]"
            });
            return comp = comp.OrderBy(d => d.Name).ToList();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}