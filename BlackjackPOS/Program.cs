using System.Diagnostics;
using System.Net.Mail;
using System.Net.Mime;
using BlackjackPOS.DeckOps;
using BlackjackPOS.GameState;

namespace BlackjackPOS;

public static class BlackjackPos
{
    //static private CardDeck.Card card = null; 
    
    static void Main()
    {
         Random rnd = new Random();
        if (false)
        {
                GameLoop game = new GameLoop();
                game.Run();
        }
       
        CardDeck deck = new CardDeck(rnd);
        deck.Shuffle();
        var card = deck.Draw();

        GameMethods gameMethods =  new GameMethods(deck);
        
        gameMethods.Start();



    }
    
}