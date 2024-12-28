using System.Text;

namespace BlackjackPOS.DeckOps;

public class CardDeckV2
{
    public static void main2(String[] args)
    {
        Random rnd = new Random();
        
        CardDeckV2 deck = new CardDeckV2(rnd);
        
        Console.WriteLine(deck);
        
        deck.Shuffle();

        Console.WriteLine(deck);

    }
    
    private readonly CardV2[] _cards = new CardV2[52];
    private readonly Random _rng;
    private int currentCard = 0;
    public CardDeckV2(Random rng)
    {
        _cards = new CardV2[52];
        _rng = rng;
        // initialize the array as a deck of cards
        for (var i = 0; i < 52; i++) _cards[i] = new CardV2(i, i % 13, i / 13);
    }
    
    
    /// <summary>
    /// Shuffle the deck of cards
    /// </summary>
    public void Shuffle()
    {
        int  n = _cards.Length;
        while (n > 0)
        {
            n--;
            var k = _rng.Next(n); // Generate a random index from 0 to n-1
            // Swap the elements
            CardV2 card = _cards[k];
            _cards[k] = _cards[n];
            _cards[n] = card;
            // Or even shorter, but less readable
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

    public class CardV2(int id, int rank, int suit)
    {
        public int Id { get; } = id;
        public int Rank { get; } = rank;
        public int Suit { get; } = suit;
        
        public override string ToString()
        {
            return Id + "\tR:" + Rank + "\tS:" + Suit;
        }
    }

    public CardV2 Draw()
    {
        if (currentCard >= _cards.Length)
        {
            return null;            
        }
        return _cards[currentCard++];
    }

    public string SuitRankConvert(CardV2 card)
    { 
        string[] suit = new string[] { "S", "H", "D", "C" };
        string[] rank = new string[] { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };
        string cardSuit = suit[card.Suit];


        if (card.Rank < 0 || card.Rank >= rank.Length)
        {
             Console.WriteLine($"{card.Suit}\t{card.Rank}\t{card.Suit}");           
        }
        
        string cardRank = rank[card.Rank];
        return cardRank + cardSuit;
    }
}