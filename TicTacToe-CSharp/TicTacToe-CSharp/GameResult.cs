namespace TicTacToe_CSharp //4
{
    class GameResult
    {
        public Player Winner { get; set; } 
        public WinInfo WinInfo { get; set; } /*If the game ends in with a tie then we set the winner to none and we wont supply a wininfo object 
                                              * next is the logic for the game and it will be contained inside a gamestate class. */
    }
}
