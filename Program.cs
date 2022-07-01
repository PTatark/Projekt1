using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt1
{
    class Program
    {
        static double SumaSzeregu (float X, float Eps, out short n, char trace)
        {            
            double a, S;
            n = 0;
            S = 0.0F;
            //iteracyjne oblicznie szeregu potęgowego
            do
            {
                n++;
                //obliczenie wartości kolejnych elementów ciągu
                a = Math.Pow(5 * X / n, n);
                //obliczenie sumy kolejnych elementów ciągu
                S = S + a;
                //śledzenie obliczeń
                if (trace == 'T' || trace =='t')
                    Console.WriteLine("\n\tTRACE: Obliczony {0} element ciągu = {1, 3:F3}", n, a);

            } while (Math.Abs(a) > Eps);

            return S;
        }

        static void podanieliczby(string tekts, out float liczba)
        {
            Console.Write(tekts);
            while (!float.TryParse(Console.ReadLine(), out liczba))
            {
                Console.WriteLine("\n\tERROR: w zapisie wartości liczby rzeczywistej wystąpił niedozwolony znak.");
                Console.Write(tekts);
            }
        }

        static double Heron (double S, float Eps, out short i)
        {
            if ((S <= 0.0F) || (Eps <= 0.0F) || (Eps >= 1.0F))
            {
                i = -1;
                return 0.0F;
            }

            double Xi, Xi_1;
            i = 0;
            Xi = S / 2.0F;

            do
            {
                Xi_1 = Xi;
                i++;
                Xi = (Xi_1 + S / Xi_1) / 2.0F;

            } while (Math.Abs(Xi - Xi_1) > Eps);

            return Xi;
        }

        static double Newton (double S, ushort n, float Eps, out short i)
        {
            double Xi, Xi_1;

            if ((S <= 0.0F) || (n < 0) || (Eps <= 0.0F) || (Eps >= 1.0F))
            {
                i = -1;

                return 0.0F;
            }

            i = 0;
            Xi = S;

            do
            {
                Xi_1 = Xi;
                i++;
                Xi = ((n - 1) * Xi_1 + S / Math.Pow(Xi_1, n - 1)) / (double)n;

            } while (Math.Abs(Xi - Xi_1) > Eps);

            return Xi;
        }

        static void zakonczenie()
        {
            Console.WriteLine("\n\tAutor programu: Piotr Tatarkiewicz");
            Console.WriteLine("\n\tDzisiaj mamy: {0}", DateTime.Now.ToString("g"));
            Console.Write("\n\tNaciśnij dowolny klawisz aby kontynuować");
            //chwilowe zatrzymanie programu
            Console.ReadKey(true);
            Console.Clear();
        }

        static void Main(string[] args)
        {
            
            while (true)
            {

                Console.WriteLine("\n\n\tWybierz jedną z poniższych opcji: ");

                Console.WriteLine("\n\tA. Obliczenie wartości (sumy) zadanego szeregu potęgowego.");
                Console.WriteLine("\n\tB. Tablicowanie wartości zadanego szeregu potęgowego.");
                Console.WriteLine("\n\tC. Tablicowanie wartości pierwiastka kwadratowego, obliczanego metodą Herona," +
                    "\n\t z wartości zadanego szeregu potęgowego.");
                Console.WriteLine("\n\tD. Tablicowanie wartości n-tego pierwiastka, obliczanego metodą Newtona," +
                    "\n\t z wartości zadanego szeregu potęgowego.");
                Console.WriteLine("\n\tE. Sprawdzenie czy liczba jest doskonała.");
                Console.WriteLine("\n\tX. Zakończenie działania programu.");

                Console.Write("\n\n\n\tNaciśnij klawisz aby wybrać jedną z opcji: ");
                var wybranaopcja = Console.ReadKey();

                switch (wybranaopcja.Key)
                {
                    case ConsoleKey.A:
                        {
                            Console.WriteLine("\n\n\n\t\tWybrana funkcjonalność: A. Obliczenie wartości (sumy) zadanego szeregu potęgowego.");

                            //śledzenie                            
                            Console.Write("\n\tJeśli chcesz włączyć śledzenie w tej funkcjonalnośći wpisz 'T' lub 't', a jeśli nie," +
                            "\n\tto naciśnij dowolny inny klawisz aby kontynuować: ");
                            char trace = Console.ReadKey().KeyChar;

                            //deklaracje
                            float X, Eps;
                            double S;

                            podanieliczby("\n\tPodaj wartość zmiennej niezależnej X: ", out X);

                            do
                            {
                                podanieliczby("\n\tPodaj dokładność obliczeń Eps: ", out Eps);

                                if ((Eps <= 0.0F) || (Eps >= 1.0F))
                                    Console.WriteLine("\n\tERROR: podana dokładność obliczeń 'Eps' wykracza poza dozwolony przedział: 0.0 < Eps < 1.0 ");

                            } while ((Eps <= 0.0F) || (Eps >= 1.0F));

                            short LicznikWyrazów;
                            S = SumaSzeregu(X, Eps, out LicznikWyrazów, trace);
                            //śledzenie
                            if (trace == 'T' || trace == 't')
                                Console.WriteLine("\n\tTRACE: Obliczona suma szeregu: {0, 3:F3}", S);
                            
                            Console.WriteLine("\n\tObliczona suma {0} wyrazów szeregu potęgowego jest równa = {1, 3:F3}", LicznikWyrazów, S);

                            zakonczenie();
                            break;
                        }
                    case ConsoleKey.B:
                        {
                            Console.WriteLine("\n\n\n\t\tWybrana funkcjonalność: B. Tablicowanie wartości zadanego szeregu potęgowego.");

                            //deklaracje
                            float Xd, Xg, h, Eps;
                            double S;
                            short LicznikWyrazów;

                            do
                            {
                                podanieliczby("\n\tPodaj dolną granicę 'Xd' przedziału tablicowania wartości szeregu: ", out Xd);

                                podanieliczby("\n\tPodaj górną granicę 'Xg' przedziału tablicowania wartości szeregu: ", out Xg);

                                if (Xd >= Xg)
                                    Console.WriteLine("\n\tERROR: dolna granica przedziału tablicowania musi być mniejsza od górnej granicy tablicowania.");

                            } while (Xd >= Xg);

                            do
                            {
                                podanieliczby("\n\tPodaj wartość przyrostu 'h' zmian wartości zmiennej niezależnej X: ", out h);

                                if ((h <= 0.0F) || (h > ((Xg - Xd) / 2.0F)))
                                    Console.WriteLine("\n\tERROR: przyrost 'h' zmian wartości zmiennej niezależnej 'X' wykracza poza dozwolony" +
                                        "\n\tzakres wartości: 0.0 < h < ((Xg - Xd) / 2.0)");

                            } while ((h <= 0.0F) || (h > ((Xg - Xd) / 2.0F)));

                            do
                            {
                                podanieliczby("\n\tPodaj dokładność obliczeń Eps: ", out Eps);

                                if ((Eps <= 0.0F) || (Eps >= 1.0F))
                                    Console.WriteLine("\n\tERROR: podana wartość dokładności obliczeń wykracza poza dozwolony przedział wartości: 0.0 < Eps < 1.0");

                            } while ((Eps <= 0.0F) || (Eps >= 1.0F));
                
                            Console.WriteLine("\n\n\t\t\tTablicowe zestawienie wartośći szeregu w przedziale: [{0, 3:F2} ; {1, 3:F2}]", Xd, Xg);
                            Console.WriteLine("\n\t\t\tX\t\t      S(X)\t\t\t  Licznik wyrazów szeregu");
                            Console.WriteLine("\t\t   --------------------------------------------------------------------------------");
                            //deklaracje uzupełniające
                            char trace = 'a';
                            float X = Xd;
                            for (int i = 0; X <= Xg; i++, X = Xd + i * h)
                            {
                                S = SumaSzeregu(X, Eps, out LicznikWyrazów, trace);

                                Console.WriteLine("\n\t\t     {0, 6:F2}\t\t     {1, 6:F3}\t\t\t\t    {2}", X, S, LicznikWyrazów);
                            }

                            zakonczenie();
                            break;
                        }
                    case ConsoleKey.C:
                        {
                            Console.WriteLine("\n\n\n\t\tWybrana funkcjonalność: C. Tablicowanie wartości pierwiastka kwadratowego, obliczanego metodą Herona," +
                            "\n\t z wartości zadanego szeregu potęgowego.");

                            //deklaracje
                            float Xd, Xg, h, Eps;
                            double S;
                            short LicznikWyrazów;

                            do
                            {
                                podanieliczby("\n\tPodaj dolną granicę 'Xd' przedziału tablicowania wartości szeregu: ", out Xd);

                                podanieliczby("\n\tPodaj górną granicę 'Xg' przedziału tablicowania wartości szeregu: ", out Xg);

                                if (Xd >= Xg)
                                    Console.WriteLine("\n\tERROR: dolna granica przedziału tablicowania musi być mniejsza od górenej granicy tablicowania.");

                            } while (Xd >= Xg);

                            do
                            {
                                podanieliczby("\n\tPodaj wartość przyrostu 'h' zmian wartości zmiennej niezależnej X: ", out h);

                                if ((h <= 0.0F) || (h > ((Xg - Xd) / 2.0F)))
                                    Console.WriteLine("\n\tERROR: przyrost 'h' zmian wartości zmiennej niezależnej 'X' wykracza poza dozwolony zakres wartości: 0.0 < h < ((Xg - Xd) / 2.0)");

                            } while ((h <= 0.0F) || (h > ((Xg - Xd) / 2.0F)));

                            do
                            {
                                podanieliczby("\n\tPodaj dokładność obliczeń Eps: ", out Eps);

                                if ((Eps <= 0.0F) || (Eps >= 1.0F))
                                    Console.WriteLine("\n\tERROR: podana wartość dokładności obliczeń wykracza poza dozwolony przedział wartości: 0.0 < Eps < 1.0");

                            } while ((Eps <= 0.0F) || (Eps >= 1.0F));

                            Console.WriteLine("\n\n\t\t\tZestawienie tabelaryczne pierwiastka kwadratowego dla wartości szeregu potęgowego.");
                            Console.WriteLine("\n\t\t   X\t      S(X)\t Licznik wyrazów       SQRT\t Licznik iteracji");
                            Console.WriteLine("\n\t\t-------\t  -----------\t-----------------    --------\t------------------");
                            //deklaracje uzupełniające
                            char trace = 'a';
                            float X;
                            short i;
                            short LicznikIteracji;
                            double PierwiastekKwadratowy;

                            for (i = 0, X = Xd; X <= Xg; i++, X = Xd + i * h)
                            {
                                S = SumaSzeregu(X, Eps, out LicznikWyrazów, trace);                                

                                PierwiastekKwadratowy = Heron(S, Eps, out LicznikIteracji);

                                if (LicznikIteracji < 0)
                                {
                                    Console.WriteLine("ERROR: wykryto błąd w parametrach wywołania metody Heron");
                                }
                                else
                                    Console.WriteLine("\n\t\t{0, 6:F2}\t    {1, 6:F2}\t        {2}\t       {3, 3:F2}\t\t {4}", X, S, LicznikWyrazów, PierwiastekKwadratowy, LicznikIteracji);
                            }

                            zakonczenie();
                            break;
                        }
                    case ConsoleKey.D:
                        {
                            Console.WriteLine("\n\n\n\t\tWybrana funkcjonalność: D. Tablicowanie wartości n-tego pierwiastka, obliczanego metodą Newtona," +
                            "\n\t z wartości zadanego szeregu potęgowego.");

                            //deklaracje
                            float Xd, Xg, h, Eps;
                            ushort n;

                            do
                            {
                                podanieliczby("\n\tPodaj dolną granicę 'Xd' przedziału tablicowania wartości szeregu: ", out Xd);

                                podanieliczby("\n\tPodaj górną granicę 'Xg' przedziału tablicowania wartości szeregu: ", out Xg);

                                if (Xd >= Xg)
                                    Console.WriteLine("\n\tERROR: dolna granica przedziału tablicowania musi być mniejsza od górnej granicy tablicowania.");

                            } while (Xd >= Xg);

                            do
                            {
                                podanieliczby("\n\tPodaj wartość przyrostu 'h' zmian wartości zmiennej niezależnej X: ", out h);

                                if ((h <= 0.0F) || (h > ((Xg - Xd) / 2.0F)))
                                    Console.WriteLine("\n\tERROR: przyrost 'h' zmian wartości zmiennej niezależnej X wykracza poza dozwolony" +
                                        "\n\tzakres wartości: 0.0 < h < ((Xg - Xd) / 2.0)");

                            } while ((h <= 0.0F) || (h > ((Xg - Xd) / 2.0F)));

                            do
                            {
                                podanieliczby("\n\tPodaj dokładność obliczeń Eps: ", out Eps);

                                if ((Eps <= 0.0F) || (Eps >= 1.0F))
                                    Console.WriteLine("\n\tERROR: podana wartość dokładności obliczeń wykracza poza dozwolony" +
                                        "\n\tprzedział wartości: 0.0 < Eps < 1.0");

                            } while ((Eps <= 0.0F) || (Eps >= 1.0F));

                            do
                            {
                                Console.Write("\n\tPodaj stopień pierwiastka n: ");
                                while (!ushort.TryParse(Console.ReadLine(), out n))
                                {
                                    Console.Write("\n\tERROR: wystąpił niedozwolony znak w zapisie podanego stopnia pierwiastka");
                                    Console.Write("\n\tPodaj ponownie stopień pierwiastka n: ");
                                }

                                if (n <= 0)
                                    Console.WriteLine("\n\tEROOR: stopień pierwiastka 'n' musi być > od 0");

                            } while (n <= 0);
                            //wypisanie tytułu zestawienia tabelarycznego
                            Console.WriteLine("\n\n\t\t\tZestawienie tabelaryczne pierwiastka obliczonego metodą Newtona");
                            Console.WriteLine("\n\t\tX\t   S(X)\t        {0} pierwiastek Newtona\t  Licznik iteracji\t  Math.Pow(...)", n);
                            Console.WriteLine("\t      -----\t----------\t----------------------\t--------------------\t-----------------");
                            //deklaracje uzupełniające
                            char trace = 'a';
                            float X;
                            short i; //indeks podprzedziału
                            double S;
                            short LicznikWyrazów, LicznikIteracji;
                            double NtyPierwiastek;
                            //wypisanie wierszy obliczeń
                            for (i = 0, X = Xd; X <= Xg; i++, X = Xd + i * h)
                            {//obliczenie wartości szeregu dla aktualnej wartości zmiennej X
                                S = SumaSzeregu(X, Eps, out LicznikWyrazów, trace);
                                //pierwiastek możemy obliczyc tylko dla wartości dodatniej
                                if (S > 0)
                                {//wyznaczenie n-tego pierwiastka
                                    NtyPierwiastek = Newton(S, n, Eps, out LicznikIteracji);
                                    //sprawdzenie czy jest błąd w metodzie
                                    if (LicznikIteracji < 0)
                                        Console.WriteLine("\n\tERROR: wykryty został błąd w parametrach wywołania metody.");
                                    else
                                        Console.WriteLine("\n\t     {0, 6:F2}\t  {1, 6:F2}\t       {2, 6:F3}\t\t\t   {3}\t\t     {4, 6:F3}", X, S,
                                            NtyPierwiastek, LicznikIteracji, (float)Math.Pow(S, 1.0F / n));


                                }
                            }

                            zakonczenie();
                            break;
                        }
                    case ConsoleKey.E:
                        {
                            Console.WriteLine("\n\n\n\t\tE. Sprawdzenie czy liczba jest doskonała.");

                            Console.Write("\n\tWprowadź liczbę którą chcesz sprawdzić: ");
                            string podanaliczba;
                            podanaliczba = Console.ReadLine();
                            ushort liczba, n;
                            int S = 0;
                            while (!ushort.TryParse(podanaliczba, out liczba))
                            {
                                Console.Write("\n\tERROR: wystąpił niedozwolony znak w zapisie podanej liczby");

                                Console.Write("\n\tWprowadź ponownie liczbę którą chcesz sprawdzić: ");
                                podanaliczba = Console.ReadLine();
                            }
                            string dzielniki = "";
                            n = liczba;
                            for (int i = 1; i < liczba; i++)
                            {
                                if (liczba % i == 0)
                                {
                                    dzielniki = dzielniki + Convert.ToString(i) + " ";
                                    S = S + i;
                                }
                            }
                            if (S == n)
                            {
                                Console.WriteLine("\n\tWprowadzona liczba jest doskonała, a jej dzielniki to {0}.", dzielniki);                                
                            }
                            else
                            {
                                Console.WriteLine("\n\tWprowadzona liczba nie jest doskonała.");                                
                            }

                            zakonczenie();
                            break;
                        }
                    case ConsoleKey.X: //zakończenie działnia programu
                        Environment.Exit(0);
                        break;

                }

            } 

        }
    }
}
