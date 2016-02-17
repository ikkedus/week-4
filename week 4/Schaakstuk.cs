using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using week_4.Enums;

namespace week_4
{
    class Schaakstuk
    {
        public SchaakstukType type { get; set; }
        public SchaakstukKleur kleur { get; set; }
        public bool moved { get; set; }

        public Schaakstuk(SchaakstukType Type, SchaakstukKleur Kleur)
        {
            this.kleur = Kleur;
            this.type = Type;
            moved = false;
        }
    }
}
