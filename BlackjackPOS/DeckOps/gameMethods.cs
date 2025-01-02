namespace BlackjackPOS.DeckOps;

public class GameMethods
{
    public int DealerScore;
    public int PlayerScore;
    public int Kitty;
    CardDeck _deck;
    public List<string> Cards = new List<string>();
    public List<string> DealerCards = new List<string>();
    

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
        DealerCards.Add(_deck.SuitRankConvert(dealer));
        DealerCards.Add(_deck.SuitRankConvert(dealer2));
        
        
        var card1 = _deck.Draw();
        var card2 = _deck.Draw();
        Cards.Add(_deck.SuitRankConvert(card1));
        Cards.Add(_deck.SuitRankConvert(card2));
        PlayerScore = card1.Rank + card2.Rank;

        Console.WriteLine("Dealer's Cards: ");
        Console.WriteLine(_deck.SuitRankConvert(dealer) + " | ?");
        Console.WriteLine("Your cards are:");
        for (int i = 0; i < Cards.Count; i++)
        {
            Console.Write(Cards[i] + " ");
        }
        Console.WriteLine();
        Console.WriteLine("HIT or STAND?");
        var line = Console.ReadLine();
        if (line == "HIT" || line == "hit")
        {
            Hit();
        } else if (line == "STAND" || line == "stand")
        {
            Stand();
        }
        
    }

    public void Hit()
    {
        Console.WriteLine("Dealer's Cards: ");
        Console.WriteLine(DealerCards[0] + " | ?");
        
        
        var card1 = _deck.Draw();
        Cards.Add(_deck.SuitRankConvert(card1));

        PlayerScore = card1.Rank + PlayerScore;
        
        Console.WriteLine("Your cards are:");
        for (int i = 0; i < Cards.Count; i++)
        {
            Console.Write(Cards[i] + " ");
        }
        if (PlayerScore <= 21)
        {

            Console.WriteLine(); 
            Console.WriteLine("HIT or STAND?");
            var line = Console.ReadLine();
                  
            if (line == "HIT" || line == "hit")
            { 
                Hit();
            } else if (line == "STAND" || line == "stand")
            { 
                Stand();
            }
  
            
        }
        else
        {
            Console.WriteLine();
            Console.WriteLine("Bust!");
            Console.WriteLine("Try again? y/n");
            if (Console.ReadLine() == "y" || Console.ReadLine() == "yes")
            {
                Cards.Clear();
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

    public void Stand()
    {
        var dealerCard = _deck.Draw();
        DealerCards.Add(_deck.SuitRankConvert(dealerCard));
        DealerScore = DealerScore + dealerCard.Rank;
        
        Console.WriteLine("Dealer's Cards: ");
        for (int i = 0; i < DealerCards.Count; i++)
        {
            Console.Write(DealerCards[i] + " ");
        }
        
        if (PlayerScore <= DealerScore)
        {
            Console.WriteLine();
            Console.WriteLine("You Lose! Your Score is:" + PlayerScore + " The Dealers Score is:" + DealerScore);
        }
        else if (PlayerScore >= DealerScore)
        {
            Console.WriteLine();
            Console.WriteLine("You Win! Your Score is:" + PlayerScore + " The Dealers Score is:" + DealerScore);
        }


}

    
}