using BlackjackPOS.DeckOps;

namespace BlackjackPOS.GameState;

public class GameLoop
{
    private readonly Random rng = new();
    private int cash = 100;

    private int currentBet;
    private GameStatus CurrentGameState = GameStatus.RefreshDecks;

    private ICardDeck dealerCards;

    private ICardDeck deck;
    private int draws;
    private int losses;
    private ICardDeck playerCards;
    private int wins;


    public void Run()
    {
        var running = true;

        while (running)
            switch (CurrentGameState)
            {
                case GameStatus.RefreshDecks:
                    RefreshDecks();
                    break;
                case GameStatus.PlayerBet:
                    PlayerBet();
                    break;
                case GameStatus.FirstDeal:
                    FirstDeal();
                    break;
                case GameStatus.PlayerTurn:
                    PlayerTurn();
                    break;
                case GameStatus.DealerTurn:
                    DealerTurn();
                    break;
                case GameStatus.Resolve:
                    Resolve();
                    break;
                case GameStatus.Quit:
                    Quit();
                    break; // Dumb thing to get the compiler to not whine
            }

        // Just Quit here for now
        Console.WriteLine("Goodbye");
        Environment.Exit(0);
    }

    public void RefreshDecks()
    {
        DisplayGameTable(true);
        if (deck == null || deck.DeckSize() < 20)
        {
            Console.Write("New Deck. Shuffling deck.");
            deck = new CardDeckImpl(rng);
            deck.Shuffle();

            Thread.Sleep(500);
            Console.Write(".");
            Thread.Sleep(500);
            Console.Write(". Done");
            Thread.Sleep(500);
        }

        playerCards = new CardDeckImpl(rng, new List<ICard>());
        dealerCards = new CardDeckImpl(rng, new List<ICard>());

        CurrentGameState = GameStatus.PlayerBet;
    }


    public void DisplayGameTable(bool hideDealerCard)
    {
        Console.Clear();
        Console.WriteLine($"Money: ${cash}\t Wins: {wins}\t Losses: {losses}\t Draws: {draws}");
        Console.WriteLine($"Current Bet: ${currentBet}");
        Console.WriteLine("Dealers Cards:");
        Console.WriteLine(HandHelper.DisplayCards(dealerCards, hideDealerCard));
        Console.WriteLine("Players Cards:");
        Console.WriteLine(HandHelper.DisplayCards(playerCards, false));
        Console.WriteLine("");
    }


    public void PlayerBet()
    {
        DisplayGameTable(true);
        Console.Write("How much would you like to bet?\n");

        string betString;
        var bet = 0;
        var validFormat = false;

        while (validFormat == false)
        {
            betString = Console.ReadLine();
            try
            {
                bet = int.Parse(betString);
                validFormat = true;
            }
            catch (FormatException fe)
            {
                Console.WriteLine("Please Enter a valid number");
            }
        }

        currentBet = bet;
        cash -= bet;
        CurrentGameState = GameStatus.FirstDeal;
    }

    public void FirstDeal()
    {
        DisplayGameTable(true);
        var card = deck.Draw();
        dealerCards.AddCard(card);
        Thread.Sleep(500);
        DisplayGameTable(true);
        card = deck.Draw();
        dealerCards.AddCard(card);
        Thread.Sleep(500);
        DisplayGameTable(true);
        card = deck.Draw();
        playerCards.AddCard(card);
        Thread.Sleep(500);
        DisplayGameTable(true);
        card = deck.Draw();
        playerCards.AddCard(card);
        Thread.Sleep(500);
        CurrentGameState = GameStatus.PlayerTurn;
    }


    public void PlayerTurn()
    {
        while (true)
        {
            DisplayGameTable(true);
            Console.Write("(H)it or (S)tand\n");

            var validKey = false;
            char key;
            do
            {
                key = Console.ReadKey(true).KeyChar;
                if (key == 'h' || key == 'H' || key == 's' || key == 'S') validKey = true;
            } while (!validKey);

            if (key == 'h' || key == 'H')
            {
                Console.WriteLine("Hit!");
                Thread.Sleep(500);
                var card = deck.Draw();
                playerCards.AddCard(card);

                var total = HandHelper.CalculateHand(playerCards);

                if (total > 21)
                {
                    DisplayGameTable(false);
                    Console.WriteLine("Busted!");
                    Thread.Sleep(1000);
                    CurrentGameState = GameStatus.Resolve;
                    break;
                }
            }
            else
            {
                // Go to Dealer turn
                Console.WriteLine("Stand");
                Thread.Sleep(500);
                CurrentGameState = GameStatus.DealerTurn;
                break;
            }
        }
    }

    public void DealerTurn()
    {
        DisplayGameTable(false);
        var dealerVal = HandHelper.CalculateHand(dealerCards);

        while (dealerVal <= 21)
        {
            dealerVal = HandHelper.CalculateHand(dealerCards);
            if (dealerVal >= 17 && dealerVal <= 21)
            {
                Console.WriteLine("Dealer Stand");
                Thread.Sleep(500);
                CurrentGameState = GameStatus.Resolve;
                break;
            }

            if (dealerVal < 17)
            {
                Console.WriteLine("Dealer Hit");
                Thread.Sleep(500);
                var card = deck.Draw();
                dealerCards.AddCard(card);
                dealerVal = HandHelper.CalculateHand(dealerCards);

                if (dealerVal > 21)
                {
                    Console.WriteLine("Dealer Busts!");
                    Thread.Sleep(500);
                    CurrentGameState = GameStatus.Resolve;
                    break;
                }
            }
        }
    }

    public void Resolve()
    {
        DisplayGameTable(false);

        var dealer = HandHelper.CalculateHand(dealerCards);
        var player = HandHelper.CalculateHand(playerCards);

        if (player > 21
            || (dealer <= 21 && player < dealer))
        {
            losses++;
            DisplayGameTable(false);
            Console.WriteLine("Player Loses!");
        }

        if (dealer > 21
            || (player <= 21 && player > dealer))
        {
            wins++;
            cash += currentBet * 2;
            DisplayGameTable(false);
            Console.WriteLine("Player Wins!");
        }

        if (player == dealer)
        {
            draws++;
            cash += currentBet;
            DisplayGameTable(false);
            Console.WriteLine("Push");
        }

        Console.WriteLine("press any key to play again.  Q to quit");
        var key = Console.ReadKey(true).KeyChar;

        if (key == 'q' || key == 'Q')
            CurrentGameState = GameStatus.Quit;
        else
            CurrentGameState = GameStatus.RefreshDecks;
    }


    public void Quit()
    {
        Console.WriteLine("Thank you for playing!");
        Environment.Exit(0);
    }

    private enum GameStatus
    {
        RefreshDecks,
        FirstDeal,
        PlayerBet,
        PlayerTurn,
        DealerTurn,
        Resolve,
        Quit
    }
}