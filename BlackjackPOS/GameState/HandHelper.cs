using System.Text;
using BlackjackPOS.DeckOps;

namespace BlackjackPOS.GameState;

public static class HandHelper
{
    public static string DisplayCards(ICardDeck deck, bool hideFirstCard)
    {
        if (deck == null || deck.DeckSize() < 1)
            return "No Cards";

        var hand = "";

        var builder = new StringBuilder();
        var cards = deck.getCards();
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
                builder.Append(card.getRank());
                builder.Append(card.getSuit());
            }

            builder.Append(" ");
        }

        if (cards.Count > 0) hand = builder.ToString();
        return hand;
    }

    public static int CalculateHand(ICardDeck deck)
    {
        if (deck == null || deck.DeckSize() < 1) return 0;
        var cards = deck.getCards();

        var hand = 0;
        var aceCount = 0;

        foreach (var card in cards)
        {
            var rank = card.getRank();
            if (rank == "A")
            {
                aceCount++;
                hand += 11; // Initially consider Ace as 11
            }
            else if (rank == "K" || rank == "Q" || rank == "J" || rank == "10")
            {
                hand += 10; // Face cards and 10 are worth 10 points
            }
            else if (int.TryParse(rank, out var cardValue))
            {
                hand += cardValue; // Number cards are worth their face value
            }
        }

        while (hand > 21 && aceCount > 0)
        {
            hand -= 10; // Treat one Ace as 1 instead of 11
            aceCount--;
        }

        return hand;
    }
}