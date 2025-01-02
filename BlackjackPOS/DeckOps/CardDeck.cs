using System.Text;

namespace BlackjackPOS.DeckOps;

public class CardDeck
{
    private readonly Card[] _cards;
    private readonly Random _rng;
    private int _currentCard;
    public CardDeck(Random rng)
    {
        _cards = new Card[52];
        _rng = rng;
        // initialize the array as a deck of cards
        for (var i = 0; i < 52; i++) _cards[i] = new Card(i, i % 13, i / 13);
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
            Card card = _cards[k];
            _cards[k] = _cards[n];
            _cards[n] = card;
            // Or even shorter, but less readable
            // (_cards[k], _cards[n]) = (_cards[n], _cards[k]);
             
        }
    }


    public new string ToString()
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
        if (_currentCard >= _cards.Length)
        {
            return null;            
        }
        return _cards[_currentCard++];
    }

    public string SuitRankConvert(Card card)
    { 
        string[] suit = ["S", "H", "D", "C"];
        string[] rank = ["A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K"];
        string cardSuit = suit[card.Suit];


        if (card.Rank < 0 || card.Rank >= rank.Length)
        {
             Console.WriteLine($"{card.Suit}\t{card.Rank}\t{card.Suit}");           
        }
        
        string cardRank = rank[card.Rank];
        return cardRank + cardSuit;
    }
}