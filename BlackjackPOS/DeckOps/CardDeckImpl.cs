using System.Text;

namespace BlackjackPOS.DeckOps;

public class CardDeckImpl : ICardDeck
{
    private readonly string[] _cardRanks = ["A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K"];

    public enum CardSuit
    {
        S = 0,
        H = 1,
        D = 2,
        C = 3
    }

    private readonly List<ICard> _cards;
    private readonly Random _rng;

    public CardDeckImpl(Random rng, List<ICard> cards)
    {
        _rng = rng;
        _cards = new List<ICard>(cards);
    }

    /// <summary>
    /// Creates a New 52 card Deck
    /// </summary>
    /// <param name="rng"></param>
    public CardDeckImpl(Random rng)
    {
        int cardCount = 52;
        _rng = rng;
        _cards = new List<ICard>(cardCount);
        // initialize the array as a deck of cards
        for (var i = 0; i < cardCount; i++) _cards.Add(new CardV2(i, _cardRanks[i % 13], (CardSuit)(i / 13)));
    }

    /// <summary>
    /// Shuffle the deck of cards
    /// </summary>
    public void Shuffle()
    {
        var n = _cards.Count;
        while (n > 0)
        {
            n--;
            var k = _rng.Next(n); // Generate a random index from 0 to n-1
            // swap cards
            (_cards[k], _cards[n]) = (_cards[n], _cards[k]);
        }
    }

    public ICard Draw()
    {
        if (_cards.Count < 1) throw new IndexOutOfRangeException("Deck is empty");

        var card = _cards[0];
        _cards.RemoveAt(0);
        return card;
    }

    public void AddCard(ICard card)
    {
        _cards.Add(card);
    }

    public void AddCards(List<ICard> cards)
    {
        _cards.AddRange(cards);
    }

    // public string SuitRankConvert(CardV2 card)
    // {
    //     string[] suit = new string[] { "S", "H", "D", "C" };
    //     string[] rank = new string[] { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };
    //     //string cardSuit = suit[card.Suit];
    //
    //
    //     // if (card.Rank < 0 || card.Rank >= rank.Length)
    //     // {
    //     // Console.WriteLine($"{card.Suit}\t{card.Rank}\t{card.Suit}");
    //     var cardRank = card.Rank;
    //     return cardRank + card.Suit;
    // }
    
    public int DeckSize()
    {
        return _cards.Count;
    }

    public List<ICard> GetCards()
    {
        return new List<ICard>(_cards);
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


    public class CardV2(int id, string rank, CardSuit suit) : ICard
    {
        public int Id { get; } = id;
        public string Rank { get; } = rank;
        public CardSuit Suit { get; } = suit;

        public override string ToString()
        {
            return Id + "\tR:" + Rank + "\tS:" + Suit;
        }

        public string GetRank()
        {
            return Rank;
        }

        public string GetSuit()
        {
            return Suit.ToString();
        }
    }
}