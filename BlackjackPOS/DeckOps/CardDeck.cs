using System.Text;

namespace BlackjackPOS.DeckOps;

public class CardDeck
{
    private readonly Card[] _cards = new Card[52];
    private readonly Random _rng;
    private int currentCard = 0;
    public CardDeck(Random rng)
    {
        this._rng = rng;
        for (var i = 0; i < 52; i++) _cards[i] = new Card(i, i % 13, i / 13);
    }


    public static void myStaticMethod()
    {
        return;
    }
    
    
    public void Shuffle()
    {
        int  n = _cards.Length;
        while (n > 0)
        {
            n--;
            var k = _rng.Next(n); // Generate a random index from 0 to n-1
            // Swap the elements
            Card card = _cards[k];
            _cards[k] = _cards[n];
            _cards[n] = card;
            // Or even shorter
            // (_cards[k], _cards[n]) = (_cards[n], _cards[k]);
             
        }
    }


    public override string ToString()
    {
        var builder = new StringBuilder();

        foreach (var card in _cards)
        {
            builder.Append(card);
            builder.Append("\n");
        }

        return builder.ToString();
    }

    public class Card(int id, int rank, int suit)
    {
        public int Id { get; } = id;
        public int Rank { get; } = rank;
        public int Suit { get; } = suit;
        


        public override string ToString()
        {
            return Id + "\tR:" + Rank + "\tS:" + Suit;
        }
    }

    public Card Draw()
    {
        if (currentCard >= _cards.Length)
        {
            return null;            
        }
        return _cards[currentCard++];
    }

    public string SuitRankConvert(Card card)
    { 
        string[] suit = new string[] { "S", "H", "D", "C" };
        string[] rank = new string[] { "A", "2", "3", "4", "5", "6", "7", "8", "9", "J", "Q", "K" };
        string cardSuit = suit[card.Suit];
        string cardRank = rank[card.Rank];
        return cardRank + cardSuit;

        
    }
    
    
    
}