using System.Text;

namespace BlackjackPOS.DeckOps;

public class CardDeck
{
    private readonly Card[] _cards = new Card[52];
    private readonly Random _rng;
    
    public CardDeck(Random rng)
    {
        this._rng = rng;
         for (var i = 0; i < 52; i++) _cards[i] = new Card(i, i % 13, i / 13);
    }

    public void Shuffle()
    {
        int  n = _cards.Length;
        while (n > 0)
        {
            n--;
            var k = _rng.Next(n); // Generate a random index from 0 to n-1
            // Swap the elements
            Card value = _cards[k];
            _cards[k] = _cards[n];
            _cards[n] = value;
            // Or even shorter
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
        public override string ToString()
        {
            return id + "\tR:" + rank + "\tS:" + suit;
        }
    }
}