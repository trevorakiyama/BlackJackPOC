using System.Text;
using BlackjackPOS.DeckOps;

namespace BlackjackPOS.GameState;

public static class HandHelper
{
    private static Dictionary<string,int> _valueDict = new Dictionary<string, int>();

    static HandHelper()
    {
        string[] cardRanks = ["A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K"];
        int[] values = [11, 2, 3, 4, 5, 6, 7, 8, 9, 10, 10, 10, 10];

        for (int i = 0; i < cardRanks.Length; i++)
        {
            _valueDict[cardRanks[i]] = values[i];
        }
    }
    
    public static string DisplayCards(ICardDeck? deck, bool hideFirstCard)
    {
        if (deck == null || deck.DeckSize() < 1)
            return "No Cards";

        var hand = "";

        var builder = new StringBuilder();
        var cards = deck.GetCards();
        var first = true;
        foreach (var card in cards)
        {
            if (first && hideFirstCard)
            {
                builder.Append("XX");
                first = false;
            }
            else
            {
                builder.Append(card.GetRank())
                    .Append(card.GetSuit());
            }

            builder.Append(" ");
        }

        if (cards.Count > 0) hand = builder.ToString();
        return hand;
    }

    public static int CalculateBlackjackHand(ICardDeck? deck)
    {
        if (deck == null || deck.DeckSize() < 1) return 0;
        var cards = deck.GetCards();

        var hand = 0;
        var aceCount = 0;

        foreach (var card in cards)
        {
            var rank = card.GetRank();
            int val = _valueDict[rank];
            hand += val;
            
            if (rank == "A")
            {
                aceCount++;
            }
        }
        
        // Adjust if A should be 1 rather than 11
        while (hand > 21 && aceCount > 0)
        {
            hand -= 10;
            aceCount--;
        }

        return hand;
    }
}