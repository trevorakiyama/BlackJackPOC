using BlackjackPOS.DeckOps;
using BlackjackPOS.GameState;

namespace BlackjackPOS;

public static class BlackjackPos
{
    //static private CardDeck.Card card = null; 
    
    static void Main()
    {
         Random rng = new Random();
        if (true)
        {
                GameLoop game = new GameLoop(rng);
                game.Run();
        }
       
        CardDeck deck = new CardDeck(rng);
        deck.Shuffle();

        GameMethods gameMethods =  new GameMethods(deck);
        
        gameMethods.Start();



    }
    
}