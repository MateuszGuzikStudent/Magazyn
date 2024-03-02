using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Produkt
{
    using ConsoleApp1.Entity;
    class Produkt : Entity
    {
        public string Nazwa { get; set; }
        public DateTime DataRozpoczęcia { get; set; }
        public DateTime DataZakończenia { get; set; }
        public int Ilość { get; set; }
        public string Wielkość { get; set; }
        public int UzytkownikId { get; set; }
    }
}
