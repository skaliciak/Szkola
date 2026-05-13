using System;
using System.Threading;

class EscapeRoom
{
    static int timeLeft = 300;
    static bool gameOver = false;
    static bool escaped = false;

    static bool hasKey = false;
    static bool hasPaper = false;
    static bool lightOn = false;
    static bool solvedMath = false;

    static string currentRoom = "pokoj1";

    static void Main()
    {
        Console.Title = "ESCAPE ROOM";
        Console.ForegroundColor = ConsoleColor.Green;

        Thread timer = new Thread(TimerTick);
        timer.Start();

        Console.Clear();
        Console.WriteLine("=== ESCAPE ROOM ===\n");
        Console.WriteLine("Masz 10 minut na ucieczkę\n");

        while (!gameOver && !escaped)
        {
            Console.Write($"\n[{currentRoom}] > ");
            string input = Console.ReadLine()?.ToLower().Trim() ?? "";

            if (timeLeft <= 0) break;

            ProcessCommand(input);
        }

        Console.Clear();
        if (escaped)
            Console.WriteLine("WYGRAŁEŚ");
        else
            Console.WriteLine("Przegrałeś.");

        Console.ReadKey();
    }

    static void TimerTick()
    {
        while (timeLeft > 0 && !gameOver && !escaped)
        {
            Thread.Sleep(1000);
            timeLeft--;

            if (timeLeft == 300) Console.WriteLine("\n>> Zostało 5 minut!");
            if (timeLeft == 60) Console.WriteLine("\n>> Została 1 minuta!");
        }
        gameOver = true;
    }

    static void ProcessCommand(string cmd)
    {
        switch (cmd)
        {
            case "podpowiedz":
            case "hint":
                GiveHint();
                break;

            case "czas":
            case "time":
                Console.WriteLine($"Pozostało: {timeLeft / 60} minut {timeLeft % 60} sekund");
                break;

            case "rozejrzyj":
            case "look":
                RozejrzyjSie();
                break;

            case "wez klucz":
                if (currentRoom == "pokoj1" && !hasKey)
                {
                    hasKey = true;
                    Console.WriteLine("Wziąłeś klucz.");
                }
                break;

            case "wez kartke":
            case "wez kartkę":
                if (currentRoom == "pokoj1" && !hasPaper)
                {
                    hasPaper = true;
                    Console.WriteLine("Wziąłeś kartkę. Na kartce jest napisane: 8***");
                }
                break;

            case "wlacz swiatlo":
            case "swiatlo":
                if (currentRoom == "pokoj1" && !lightOn)
                {
                    lightOn = true;
                    Console.WriteLine("Zapaliło się światło.");
                }
                break;

            case "uzyj klucza":
                if (hasKey && currentRoom == "pokoj2")
                {
                    Console.WriteLine("Otworzyłeś szafkę.");
                    Console.WriteLine("W środku jest kartka z zadaniem: 15 × 8 + 27 = ?");
                }
                break;

            case "drzwi":
            case "idz drzwi":
                if (currentRoom == "pokoj1")
                {
                    if (hasKey)
                    {
                        currentRoom = "pokoj2";
                        Console.WriteLine("Przeszedłeś do następnego pokoju.");
                    }
                    else
                    {
                        Console.WriteLine("Drzwi są zamknięte na klucz.");
                    }
                }
                else if (currentRoom == "pokoj2")
                {
                    TryFinalEscape();
                }
                break;

            case "inwentarz":
            case "i":
                ShowInventory();
                break;

            case "quit":
                gameOver = true;
                break;

            default:
                Console.WriteLine("Nieznana komenda. Spróbuj: rozejrzyj, podpowiedz, drzwi...");
                break;
        }
    }

    static void RozejrzyjSie()
    {
        if (currentRoom == "pokoj1")
        {
            if (!lightOn)
                Console.WriteLine("Jest ciemno. Widzisz zarys stołu i drzwi.");
            else
            {
                Console.WriteLine("Na stole leży klucz.");
                Console.WriteLine("Na podłodze leży kartka.");
            }
        }
        else if (currentRoom == "pokoj2")
        {
            Console.WriteLine("Widzisz szafkę i drzwi wyjściowe z panelem numerycznym.");
        }
    }

    static void ShowInventory()
    {
        Console.WriteLine("Masz:");
        if (hasKey) Console.WriteLine("- Klucz");
        if (hasPaper) Console.WriteLine("- Kartkę (8***)");
        if (!hasKey && !hasPaper) Console.WriteLine("nic");
    }

    static void GiveHint()
    {
        Console.WriteLine("=== PODPOWIEDŹ ===");

        if (currentRoom == "pokoj1")
        {
            if (!lightOn)
            {
                Console.WriteLine("• Jest bardzo ciemno → włącz światło");
                Console.WriteLine("   Komenda: wlacz swiatlo");
            }
            else if (!hasKey)
            {
                Console.WriteLine("• Na stole coś leży → weź to");
                Console.WriteLine("   Komenda: wez klucz");
            }
            else if (!hasPaper)
            {
                Console.WriteLine("• Na podłodze leży kartka → podnieś ją");
                Console.WriteLine("   Komenda: wez kartke");
            }
            else
            {
                Console.WriteLine("• Użyj klucza w następnym pokoju");
            }
        }
        else 
        {
            if (!solvedMath)
            {
                Console.WriteLine("• Otwórz szafkę");
                Console.WriteLine("   Komenda: uzyj klucza");
            }
            else
            {
                Console.WriteLine("• Kod zaczyna się od 8 (z kartki)");
                Console.WriteLine("• Dopełnij go wynikiem zadania matematycznego");
            }
        }
    }

    static void TryFinalEscape()
    {
        Console.WriteLine("Przy drzwiach jest panel numeryczny.");

        if (!solvedMath)
        {
            Console.WriteLine("\nNajpierw rozwiąż zadanie z szafki:");
            Console.Write("Ile to 15 × 8 + 27? ");
            string answer = Console.ReadLine()?.Trim();

            if (answer == "147")
            {
                solvedMath = true;
                Console.WriteLine("Poprawna odpowiedź!");
            }
            else
            {
                Console.WriteLine("Źle! Spróbuj ponownie później.");
                return;
            }
        }

        Console.Write("\nPodaj 4-cyfrowy kod: ");
        string kod = Console.ReadLine()?.Trim();

        if (kod == "8147" && hasPaper) 
        {
            escaped = true;
            Console.WriteLine("Poprawny kod! Uciekłeś!");
        }
        else
        {
            Console.WriteLine("Zły kod!");
        }
    }
}