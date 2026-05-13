using System;

class Program
{
    static void Main()
    {
        Random rand = new Random();

        Console.Write("Podaj liczbę kości: ");
        int liczbaKosci = int.Parse(Console.ReadLine());

        Console.Write("Podaj liczbę rzutów: ");
        int liczbaRzutow = int.Parse(Console.ReadLine());

        int[] statystyki = new int[7];

        Console.WriteLine("\nWyniki rzutów:\n");

        Console.Write("Rzut\t");
        for (int k = 1; k <= liczbaKosci; k++)
        {
            Console.Write($"Kość{k}\t");
        }
        Console.WriteLine();

        for (int rzut = 1; rzut <= liczbaRzutow; rzut++)
        {
            Console.Write($"{rzut}\t");

            for (int kosc = 1; kosc <= liczbaKosci; kosc++)
            {
                int wynik = rand.Next(1, 7);

                Console.Write($"{wynik}\t");

                statystyki[wynik]++;
            }

            Console.WriteLine();
        }

        Console.WriteLine("\nPodsumowanie:");

        for (int i = 1; i <= 6; i++)
        {
            Console.WriteLine($"{i} oczko(a) - {statystyki[i]} razy");
        }
    }
}