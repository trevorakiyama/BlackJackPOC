namespace BlackjackPOS.DeckOps;

public class GameMethods
{
    public int DealerScore;
    public int PlayerScore;
    public int Kitty;
    CardDeck _deck;
    public List<string> cards = new List<string>();

    public GameMethods (CardDeck cardDeck)
    {
        this._deck = cardDeck;
    }

    public void Start()
    {
        Console.WriteLine("Bet:");
        Kitty = Convert.ToInt32(Console.ReadLine()) + Kitty;
        Deal();
    }
    public void Deal()
    {       
        var dealer = _deck.Draw();
        var dealer2 = _deck.Draw();
        DealerScore = dealer.Rank + dealer2.Rank; 
        var card1 = _deck.Draw();
        var card2 = _deck.Draw();
        cards.Add(_deck.SuitRankConvert(card1));
        cards.Add(_deck.SuitRankConvert(card2));
        PlayerScore = card1.Rank + card2.Rank;

        Console.WriteLine("Dealer's Cards: ");
        Console.WriteLine(_deck.SuitRankConvert(dealer) + " | ?");
        Console.WriteLine("Your cards are:");
        for (int i = 0; i < cards.Count; i++)
        {
            Console.Write(cards[i] + " ");
        }
        Console.WriteLine();
        Console.WriteLine("HIT or STAND?");
        
        if (Console.ReadLine() == "HIT" || Console.ReadLine() == "hit")
        {
            Hit();
        } else if (Console.ReadLine() == "STAND" || Console.ReadLine() == "stand")
        {
            
        }
        
    }

    public void Hit()
    {
        var card1 = _deck.Draw();
        cards.Add(_deck.SuitRankConvert(card1));

        PlayerScore = card1.Rank + PlayerScore + 1;
        if (PlayerScore <= 21)
        {
                  Console.WriteLine("Your cards are:");
                  for (int i = 0; i < cards.Count; i++)
                  {
                      Console.Write(cards[i] + " ");
                  }
                  Console.WriteLine();
                  Console.WriteLine("HIT or BET?");
                  
                  if (Console.ReadLine() == "HIT" || Console.ReadLine() == "hit")
                  {
                      Hit();
                  } else if (Console.ReadLine() == "STAND" || Console.ReadLine() == "stand")
                  {
                      
                  }
  
            
        }
        else
        {
            Console.WriteLine("Bust!");
            Console.WriteLine("Try again? y/n");
            if (Console.ReadLine() == "y" || Console.ReadLine() == "yes")
            {
                cards.Clear();
                Start();
            }
            else 
            {
                Console.WriteLine("Bye!");
                Console.WriteLine("Type 'Start' to Start!");
                if (Console.ReadLine() == "Start")
                {
                    Start();
                }
            } 

        }

    }


    
}