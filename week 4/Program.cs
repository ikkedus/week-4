using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using week_4.Enums;

namespace week_4
{
    class Program
    {
        static void Main(string[] args)
        {
            Schaakstuk[,] bord = new Schaakstuk[8,8];
            SchaakstukKleur beurt = SchaakstukKleur.Wit;
            InitSchaakbord(bord);
            ToonSchaakbord(bord);
            Console.WriteLine("Wit maakt eerste zet.");
            while (true)
            {
                Console.Write("geef van-positie:");
                string vanPostite = Console.ReadLine();
                Positie van;
                if (!(vanPostite.Length > 2) && StringToPosition(vanPostite,out van))
                {
                    Console.Write("geef naar-positie:");
                    string naarPostite = Console.ReadLine();
                    Positie naar;
                    if (!(naarPostite.Length > 2) && StringToPosition(naarPostite, out naar))
                    {
                        MaakSprong(van,naar,ref bord,ref beurt);
                        ToonSchaakbord(bord);
                    }
                }
            }
        }

        private static void MaakSprong(Positie van,Positie naar ,ref Schaakstuk[,] bord,ref SchaakstukKleur beurt)
        {
            if (bord[van.rij,van.kolom] != null)
            {
                bool goedGekeurd = false;
                switch (bord[van.rij, van.kolom].type)
                {
                    case SchaakstukType.Pion:
                        int value = Math.Abs(van.rij - naar.rij);
                        if (value == 1 || (!bord[van.rij, van.kolom].moved && value ==2) )
                        {
                            bord[van.rij, van.kolom].moved = true;
                            goedGekeurd = true;
                        }
                        break;
                    case SchaakstukType.Toren:
                        int value1 = Math.Abs(van.rij - naar.rij);
                        int value2 = Math.Abs(van.kolom - naar.kolom);
                        if ((value2 == 0 && value1 > 0) || (value2 > 0 && value1 == 0))
                        {
                            goedGekeurd = true;
                        }
                        break;
                    case SchaakstukType.Paard:
                        value1 = Math.Abs(van.rij - naar.rij);
                        value2 = Math.Abs(van.kolom - naar.kolom);
                        if ((value2 == 1 && value1 == 2)|| (value2 == 2 && value1 == 1))
                        {
                            goedGekeurd = true;
                        }
                        break;
                    case SchaakstukType.Loper:
                        value1 = Math.Abs(van.rij - naar.rij);
                        value2 = Math.Abs(van.kolom - naar.kolom);
                        if (value2 == value1 && value1 != 0)
                        {
                            goedGekeurd = true;
                        }
                        break;
                    case SchaakstukType.Koning:
                        value1 = Math.Abs(van.rij - naar.rij);
                        value2 = Math.Abs(van.kolom - naar.kolom);
                        if ((value2 == 1 || value1 == 1) && !(value2 > 1 || value1 > 1))
                        {
                            goedGekeurd = true;
                        }
                        break;
                    case SchaakstukType.Koningin:
                        value1 = Math.Abs(van.rij - naar.rij);
                        value2 = Math.Abs(van.kolom - naar.kolom);
                        if (value2 == value1 && value1 != 0)
                        {
                            goedGekeurd = true;
                        }
                        else if ((value2 == 0 && value1 > 0) || (value2 > 0 && value1 == 0))
                        {
                            goedGekeurd = true;
                        }
                        break;
                }
                if (bord[van.rij, van.kolom].kleur == beurt && goedGekeurd && (bord[naar.rij,naar.kolom] == null || bord[van.rij, van.kolom].kleur != bord[naar.rij, naar.kolom].kleur))
                {
                    bord[naar.rij, naar.kolom] = bord[van.rij, van.kolom];
                    
                    bord[van.rij, van.kolom] = null;

                    if (beurt == SchaakstukKleur.Wit)
                    {
                        beurt = SchaakstukKleur.Zwart;
                    }
                    else
                    {
                        beurt = SchaakstukKleur.Wit;

                    }
                }
            }
        }

        private static bool StringToPosition(string txt, out Positie pos)
        {
            // "A2": "A" => kolom 0, "2" => rij 1
            txt = txt.ToUpper();
            pos = new Positie();
            pos.rij = 0;
            pos.kolom = txt[0] - 'A';
            int rij;
            if (int.TryParse(txt[1].ToString(), out rij) && pos.kolom < 8 && rij < 8)
            {
                pos.rij = rij - 1;
            }
            else
            {
                return false;
            }
            return true;
        }

        private static void InitSchaakbord(Schaakstuk[,] schaakbord)
        {
            // maak schaakbord leeg
            for (int r = 0; r < schaakbord.GetLength(0); r++)
            {
                for (int k = 0; k < schaakbord.GetLength(1); k++)
                {
                    schaakbord[r, k] = null;
                }
            }

            // plaats witte schaakstukken ('bovenaan')
            schaakbord[0, 0] = new Schaakstuk(SchaakstukType.Toren, SchaakstukKleur.Wit);
            schaakbord[0, 1] = new Schaakstuk(SchaakstukType.Paard, SchaakstukKleur.Wit);
            schaakbord[0, 2] = new Schaakstuk(SchaakstukType.Loper, SchaakstukKleur.Wit);
            schaakbord[0, 3] = new Schaakstuk(SchaakstukType.Koning, SchaakstukKleur.Wit);
            schaakbord[0, 4] = new Schaakstuk(SchaakstukType.Koningin, SchaakstukKleur.Wit);
            schaakbord[0, 5] = new Schaakstuk(SchaakstukType.Loper, SchaakstukKleur.Wit);
            schaakbord[0, 6] = new Schaakstuk(SchaakstukType.Paard, SchaakstukKleur.Wit);
            schaakbord[0, 7] = new Schaakstuk(SchaakstukType.Toren, SchaakstukKleur.Wit);
            //pionen
            for (int i = 0; i < schaakbord.GetLength(1); i++)
            {
                schaakbord[1, i] = new Schaakstuk(SchaakstukType.Pion, SchaakstukKleur.Wit);
                schaakbord[6, i] = new Schaakstuk(SchaakstukType.Pion, SchaakstukKleur.Zwart);
            }
            // plaats zwarte schaakstukken ('onderaan')
            schaakbord[7, 0] = new Schaakstuk(SchaakstukType.Toren, SchaakstukKleur.Zwart);
            schaakbord[7, 1] = new Schaakstuk(SchaakstukType.Paard, SchaakstukKleur.Zwart);
            schaakbord[7, 2] = new Schaakstuk(SchaakstukType.Loper, SchaakstukKleur.Zwart);
            schaakbord[7, 3] = new Schaakstuk(SchaakstukType.Koning, SchaakstukKleur.Zwart);
            schaakbord[7, 4] = new Schaakstuk(SchaakstukType.Koningin, SchaakstukKleur.Zwart);
            schaakbord[7, 5] = new Schaakstuk(SchaakstukType.Loper, SchaakstukKleur.Zwart);
            schaakbord[7, 6] = new Schaakstuk(SchaakstukType.Paard, SchaakstukKleur.Zwart);
            schaakbord[7, 7] = new Schaakstuk(SchaakstukType.Toren, SchaakstukKleur.Zwart);
        }

        private static void ToonSchaakbord(Schaakstuk[,] schaakbord)
        {
            Console.Clear();
            // save colors
            ConsoleColor backgroundBackup = Console.BackgroundColor;
            ConsoleColor foregroundBackup = Console.ForegroundColor;

            // print letters bovenaan schaakbord
            Console.WriteLine("   A  B  C  D  E  F  G  H ");

            for (int r = 0; r < schaakbord.GetLength(0); r++)
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("{0}", r + 1);

                for (int k = 0; k < schaakbord.GetLength(1); k++)
                {
                    // toggle background color
                    if ((r + k)%2 == 0)
                        Console.BackgroundColor = ConsoleColor.DarkYellow;
                    else
                        Console.BackgroundColor = ConsoleColor.Gray;

                    Schaakstuk schaakstuk = schaakbord[r, k];
                    if (schaakstuk == null)
                    {
                        Console.Write("   ");
                        continue;
                    }
                    // stel de fontkleur in, afhankelijk van de kleur van het schaakstuk
                    if (schaakstuk.kleur == SchaakstukKleur.Wit)
                        Console.ForegroundColor = ConsoleColor.White;
                    else if (schaakstuk.kleur == SchaakstukKleur.Zwart)
                        Console.ForegroundColor = ConsoleColor.Black;
                    else
                        Console.ForegroundColor = ConsoleColor.Cyan;

                    // print het schaakstuk
                    switch (schaakstuk.type)
                    {
                        case SchaakstukType.Koning:
                            Console.Write(" K ");
                            break;
                        case SchaakstukType.Koningin:
                            Console.Write(" Q ");
                            break;
                        case SchaakstukType.Loper:
                            Console.Write(" L ");
                            break;
                        case SchaakstukType.Paard:
                            Console.Write(" P ");
                            break;
                        case SchaakstukType.Pion:
                            Console.Write(" p ");
                            break;
                        case SchaakstukType.Toren:
                            Console.Write(" T ");
                            break;
                    }
                }
                Console.WriteLine();
            }

            // restore colors
            Console.BackgroundColor = backgroundBackup;
            Console.ForegroundColor = foregroundBackup;
        }
    }
}
