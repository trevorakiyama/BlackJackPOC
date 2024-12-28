using BlackjackPOS.DeckOps;

namespace BlackjackPOS;

public static class BlackjackPos
{
    //static private CardDeck.Card card = null; 
    
    static void Main()
    {
        if (false)
        {
            CardDeckV2.main2(null);
        }

        Random rnd = new Random();
        
        CardDeck deck = new CardDeck(rnd);
        deck.Shuffle();
        var card = deck.Draw();

        

        GameMethods gameMethods =  new GameMethods(deck);
        
        gameMethods.Start();



    }
    
}