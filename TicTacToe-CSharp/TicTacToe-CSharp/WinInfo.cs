namespace TicTacToe_CSharp //3
{
    public class WinInfo 
    {
        public WinType Type { get; set; } //This class will supply information about how a player won the game 
        public int Number { get; set; } //If a player won by marking a row or column the number int will show which one
    }
}
