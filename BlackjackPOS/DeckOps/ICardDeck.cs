namespace BlackjackPOS.DeckOps;

public interface ICardDeck
{


    public void Shuffle();
    public ICard Draw();
    public void AddCard(ICard card);
    
    public void AddCards(List<ICard> cards);
    
}