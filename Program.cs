namespace TheConsoleLottery
{
    internal class Program
    {
        
        static int[] BuyTickets(ref int wallet, int price, ref int numberOfTickets)
        {
            while (true)
            {
            Console.WriteLine("Hur många lotter vill du köpa?");
            Console.WriteLine($"Du har {wallet}kr just nu");
            Console.WriteLine($"Lotterna kostar :{price} kr");
            int.TryParse(Console.ReadLine(), out numberOfTickets);
                if (wallet - (numberOfTickets * price) < 0)
                {
                    Console.WriteLine("du har inte råd!");
                    continue;
                }
                
                int[] lottNumbers = new int[numberOfTickets];
                for (int i = 0; i < numberOfTickets;) 
                {
                    Console.WriteLine("Ange ditt lott nummer 1-50");
                    int.TryParse(Console.ReadLine(), out lottNumbers[i]);
                    if (lottNumbers[i] > 0 && 51 > lottNumbers[i] )
                        i++;

                   else
                        Console.WriteLine("Ange ett nummer mellan 1-50");
                }

               
                if (numberOfTickets != 0)
                {
                    wallet = wallet - (numberOfTickets * price);
                    return lottNumbers;                    
                }
            }
        }

        static int StartGame(int numberOfTickets, int numberOfPulls, Random random, int winprize, int[] lottNumbers)
        {
            int totalprize = 0;
            Console.Clear();
            Console.WriteLine($"Du har {numberOfTickets} antal lotter");
            Console.Write("Dina lott nummer är: ");
            foreach(int element in lottNumbers)
                Console.Write(element + " ");
           
            int[] winningNumbers = new int[numberOfPulls];
            for (int i = 0; i < numberOfPulls; i++)
                winningNumbers[i] = random.Next(1, 51);
            bool winning = false;
            for (int j = 0; j < numberOfPulls; j++ )
                for (int i = 0; i < numberOfTickets; i++)
                {
                    if (lottNumbers[i] == winningNumbers[j])
                    {
                        Console.WriteLine($"Grattis du vann på nummer {lottNumbers[i]}");
                        totalprize += winprize;
                        winning = true;
                        
                    }    
                }
            if (winning == false)
            {
                Console.WriteLine("\nDu vann inte, tyvärr :(");
                Console.Write($"Vinnande lott nummer är:");
                foreach (int element in winningNumbers)
                    Console.Write(element+ " ");
                Console.WriteLine("Tryck enter för att återgå till huvudmenyn");
                Console.ReadLine();

            }
            if (winning == true)
                Console.WriteLine("Tryck Enter för att återgå till huvudmenyn");
            return totalprize;
        }
        static void Settings(ref int numberOfPulls,ref int price,ref int winprize)
        {
            bool settingsMenu = true;
            while(settingsMenu)
            {
                Console.Clear();
                Console.WriteLine("Vill du ändra: ");
                Console.WriteLine($"[1]Antalet dragningar, just nu {numberOfPulls}");
                Console.WriteLine($"[2]Priset på Lotterna, just nu {price}");
                Console.WriteLine($"[3]Hur mycket du vinner, just nu {winprize}");
                Console.WriteLine($"[4]Återgå till huvudmenyn");
                int.TryParse(Console.ReadLine(), out int menuChoice);
                switch (menuChoice)
                {
                    case 1:
                        Console.WriteLine("Hur många gånger ska vi dra vinnande lotter nummer?");
                        int.TryParse(Console.ReadLine(), out numberOfPulls);
                        break;
                    case 2:
                        Console.WriteLine("Vad ska priset på lotterna vara?");
                        int.TryParse(Console.ReadLine(), out price);
                        break;
                    case 3:
                        Console.WriteLine("Hur stor ska vinsten vara?");
                        int.TryParse(Console.ReadLine(), out winprize);
                        break;
                    case 4:
                        settingsMenu = false;
                        break;
                    default:
                        Console.WriteLine("Ange en siffra mellan 1 och 4");
                        break;


                }
               
            }
        }

        static void Main(string[] args)
        {
            int[] ticketArray = new int[5];
            Random random = new Random();
            int numberOfPulls = 3;
            int price = 10;
            int wallet = 50;        
            int numberOfTickets = 0;
            bool menyLoop = true;
            int winprize = 30;
            while (menyLoop)
            {
                Console.Clear();
                Console.WriteLine("välkommen till Lotterispelet");
                Console.WriteLine($"Du har {numberOfTickets} antal lotter, {wallet}kr och lotterna just kostar {price} kr");
                Console.WriteLine("[1]Köp Lotter");
                Console.WriteLine("[2]Starta lottdragningen");
                Console.WriteLine("[3]Alternativ");
                Console.WriteLine("[4]Avsluta");
                int.TryParse(Console.ReadLine(), out int menu);

                switch (menu)
                { 
                case 1:
                    ticketArray = BuyTickets(ref wallet, price, ref numberOfTickets);
                    break;
                case 2:
                        if (numberOfTickets == 0)
                        {
                            Console.WriteLine("Du kan inte starta spelet utan lotter!");
                            Console.WriteLine("Tryck enter för att återgå till huvudmenyn");
                            Console.ReadLine();
                            break;
                        }
                        wallet += StartGame(numberOfTickets, numberOfPulls, random, winprize, ticketArray);
                        numberOfTickets = 0;
                        break;
                case 3:
                        Settings(ref numberOfPulls,ref price,ref winprize);
                    break;
                case 4:
                        menyLoop = false;
                    break;
                    default:
                        Console.WriteLine("Ange en siffra 1 till 4");
                        break;
                }
            }
        }
    }
}
