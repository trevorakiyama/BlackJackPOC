using BlackjackPOS.DeckOps;

namespace BlackjackPOS.GameState;

public class GameLoop(Random rng)
{
    private int _playerMoney = 100;

    private int _currentBet;
    private GameStatus _currentGameState = GameStatus.RefreshDecks;

    private ICardDeck _dealerCards = new CardDeckImpl(rng, new List<ICard>());
    private ICardDeck _deck = new CardDeckImpl(rng, new List<ICard>());
    private ICardDeck _playerCards = new CardDeckImpl(rng, new List<ICard>());
    
    private int _draws;
    private int _losses;

    private int _wins;


    public void Run()
    {
        var running = true;

        while (running)
            switch (_currentGameState)
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
                    running = false;
                    break; // Dumb thing to get the compiler to not whine
            }

        // Just Quit here for now
        Console.WriteLine("Goodbye");
        Environment.Exit(0);
    }

    public void RefreshDecks()
    {
        DisplayGameTable(true);
        if (_deck == null || _deck.DeckSize() < 20)
        {
            Console.Write("New Deck. Shuffling deck.");
            _deck = new CardDeckImpl(rng);
            _deck.Shuffle();

            Thread.Sleep(500);
            Console.Write(".");
            Thread.Sleep(500);
            Console.Write(". Done");
            Thread.Sleep(500);
        }

        _playerCards = new CardDeckImpl(rng, new List<ICard>());
        _dealerCards = new CardDeckImpl(rng, new List<ICard>());

        _currentGameState = GameStatus.PlayerBet;
    }


    public void DisplayGameTable(bool hideDealerCard)
    {
        Console.Clear();
        Console.WriteLine($"Money: ${_playerMoney}\t Wins: {_wins}\t Losses: {_losses}\t Draws: {_draws}");
        Console.WriteLine($"Current Bet: ${_currentBet}");
        Console.WriteLine("Dealers Cards:");
        Console.WriteLine(HandHelper.DisplayCards(_dealerCards, hideDealerCard));
        Console.WriteLine("Players Cards:");
        Console.WriteLine(HandHelper.DisplayCards(_playerCards, false));
        Console.WriteLine("");
    }


    public void PlayerBet()
    {
        DisplayGameTable(true);


        string? betString;
        var bet = 0;
        var validBet = false;

        while (validBet == false)
        {
            Console.Write("How much would you like to bet?\n");
            betString = Console.ReadLine();
            try
            {
                if (betString != null) bet = int.Parse(betString);
                validBet = true;
            }
            catch (FormatException)
            {
                Console.WriteLine("Please Enter a valid number");
            }

            if (validBet)
            {
                if (bet > _playerMoney)
                {
                    Console.WriteLine("You do not have enough money.");
                    validBet = false;
                }
            }
        }

        _currentBet = bet;
        _playerMoney -= bet;
        _currentGameState = GameStatus.FirstDeal;
    }

    public void FirstDeal()
    {
        DisplayGameTable(true);
        var card = _deck.Draw();
        _dealerCards.AddCard(card);
        Thread.Sleep(500);
        DisplayGameTable(true);
        card = _deck.Draw();
        _dealerCards.AddCard(card);
        Thread.Sleep(500);
        DisplayGameTable(true);
        card = _deck.Draw();
        _playerCards.AddCard(card);
        Thread.Sleep(500);
        DisplayGameTable(true);
        card = _deck.Draw();
        _playerCards.AddCard(card);
        Thread.Sleep(500);
        _currentGameState = GameStatus.PlayerTurn;
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
                var card = _deck.Draw();
                _playerCards.AddCard(card);

                var total = HandHelper.CalculateBlackjackHand(_playerCards);

                if (total > 21)
                {
                    DisplayGameTable(false);
                    Console.WriteLine("Busted!");
                    Thread.Sleep(1000);
                    _currentGameState = GameStatus.Resolve;
                    break;
                }
            }
            else
            {
                // Go to Dealer turn
                Console.WriteLine("Stand");
                Thread.Sleep(500);
                _currentGameState = GameStatus.DealerTurn;
                break;
            }
        }
    }

    public void DealerTurn()
    {
        
        var dealerVal = HandHelper.CalculateBlackjackHand(_dealerCards);

        while (dealerVal <= 21)
        {
            DisplayGameTable(false);
            Thread.Sleep(500);
            dealerVal = HandHelper.CalculateBlackjackHand(_dealerCards);
            if (dealerVal >= 17 && dealerVal <= 21)
            {
                Console.WriteLine("Dealer Stand");
                Thread.Sleep(500);
                _currentGameState = GameStatus.Resolve;
                break;
            }

            if (dealerVal < 17)
            { 
                var card = _deck.Draw();
                _dealerCards.AddCard(card);
                Console.WriteLine("Dealer Hit");
                Thread.Sleep(500);
                dealerVal = HandHelper.CalculateBlackjackHand(_dealerCards);

                if (dealerVal > 21)
                {
                    Console.WriteLine("Dealer Busts!");
                    Thread.Sleep(500);
                    _currentGameState = GameStatus.Resolve;
                    break;
                }
            }
        }
    }

    public void Resolve()
    {
        DisplayGameTable(false);

        var dealer = HandHelper.CalculateBlackjackHand(_dealerCards);
        var player = HandHelper.CalculateBlackjackHand(_playerCards);

        if (player > 21
            || (dealer <= 21 && player < dealer))
        {
            _losses++;
            DisplayGameTable(false);
            Console.WriteLine("Player Loses!");
        }

        if (dealer > 21
            || (player <= 21 && player > dealer))
        {
            _wins++;
            _playerMoney += _currentBet * 2;
            DisplayGameTable(false);
            Console.WriteLine("Player Wins!");
        }

        if (player == dealer)
        {
            _draws++;
            _playerMoney += _currentBet;
            DisplayGameTable(false);
            Console.WriteLine("Push");
        }

        if (_playerMoney <= 0)
        {
            Console.WriteLine("You are Bankrupt!\nGame Over!");
            _currentGameState = GameStatus.Quit;
        }
        else
        {
            Console.WriteLine("press any key to play again.  Q to quit");
            var key = Console.ReadKey(true).KeyChar;

            if (key == 'q' || key == 'Q')
                _currentGameState = GameStatus.Quit;
            else
                _currentGameState = GameStatus.RefreshDecks;
        }
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