using BlackjackPOS.DeckOps;

namespace BlackjackPOS;

public static class BlackjackPos
{

    static void Main()
    {  

        Console.WriteLine("Initial Deck");

        Random rnd = new Random();
        
        CardDeck deck = new CardDeck(rnd);
        Console.WriteLine(deck.ToString());
        
        Console.WriteLine("Shuffled");
        deck.Shuffle();
        Console.WriteLine(deck.ToString());
        
        



    }
    
}