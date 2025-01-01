using System.Diagnostics;
using BlackjackPOS.DeckOps;

namespace BlackjackPOS;

public static class BlackjackPos
{
    //static private CardDeck.Card card = null; 
    
    static void Main()
    {
         Random rnd = new Random();
        if (false)
        {
                // This could be 
                    ICardDeck deck2= new CardDeckImpl(rnd, 52);
                    Console.WriteLine(deck2);
                    deck2.Shuffle();
                    
                    
                    Console.WriteLine(deck2);
        }

       
        CardDeck deck = new CardDeck(rnd);
        deck.Shuffle();
        var card = deck.Draw();

        GameMethods gameMethods =  new GameMethods(deck);
        
        gameMethods.Start();



    }
    
}