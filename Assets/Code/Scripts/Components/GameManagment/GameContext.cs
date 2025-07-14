
using System.Collections.Generic;
using Code.Scripts.Components.Entity;

namespace Code.Scripts.Components.GameManagment
{
	public class GameContext
	{
		public Player currentPlayer;
		public Enemy targetEnemy;
		public ACard sourceCard;
		public ACard targetCard;
		public GameBoard.GameBoard gameBoard;
		public List<ACard> allCardsInHand;
		public List<ACard> allCardsInTable;

	}
}