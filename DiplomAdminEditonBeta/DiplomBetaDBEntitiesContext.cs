using DiplomAdminEditonBeta.Views.PagesTask;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomAdminEditonBeta
{
    public partial class DiplomBetaDBEntities : DbContext
    {
        private static DiplomBetaDBEntities _context;

        public static DiplomBetaDBEntities GetContext()
        {
            if (_context == null) _context = new DiplomBetaDBEntities();
            return _context;
        }
    }

    public partial class Client
    {
        public virtual List<string> TypeClients
        {
            get
            {
                return DiplomBetaDBEntities.GetContext().TypeClient.ToList().Select(p => p.Name).ToList();
            }
        }
    }

    public partial class Point
    {
        public string[] clientsList
        {
            get
            {
                string[] vs = (string[])VendorsAndConsumptionsPage.ChosedClient.Where(p => p.TypeId == Client.TypeId).Select(d => d.CompanyName).ToArray().Clone(); 
                return vs;
            }
        }
    }

    public partial class Task
    {
        public string Clients
        {
            get
            {
                List<string> vs = Point.Select(p => p.Client).Select(p => p.CompanyName).Distinct().ToList();
                string s = "";
                foreach(string r in vs)
                {
                    s += r + " ";
                }
                return s;
            }
        }
    }

    public partial class Constraint
    {
        public List<TypeConstraint> typeConstraints
        {
            get
            {
                return DiplomBetaDBEntities.GetContext().TypeConstraint.ToList();
            }
        }

        public List<Point> PointsVendors
        {
            get
            {
                return Task.Point.Where(p => p.Client.TypeId == 2).ToList();
            }
        }

        public List<Point> PointsConsumers
        {
            get
            {
                return Task.Point.Where(p => p.Client.TypeId == 1).ToList();
            }
        }

        public Point PointVendor
        {
            get
            {
                if (IdPoints == null)
                    return null;
                int id = int.Parse(IdPoints.Split('&')[0]);
                return Task.Point.First(p => p.Position == id && p.Client.TypeId == 2);
            }
        }

        public Point PointConsumer
        {
            get
            {
                if (IdPoints == null)
                    return null;
                int id = int.Parse(IdPoints.Split('&')[1]);
                return Task.Point.First(p => p.Position == id && p.Client.TypeId == 1);
            }
        }
    }

    public partial class Carrier
    {
        public string ListService
        {
            get
            {
                string output = "";
                foreach(ServiceCarrier serviceCarrier in ServiceCarrier)
                {
                    output += serviceCarrier.Service.Name + ": " + serviceCarrier.Cost + ";\n";
                }
                return output;
            }
        }
    }
}
