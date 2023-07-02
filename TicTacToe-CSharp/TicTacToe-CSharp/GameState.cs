using System; //An event is a notification sent by an object to signal the occurence of an action. Evenets in .Net follow oberver design patter. In C# an event is an encapsulated delegate. It is dependent on the delegate. 

namespace TicTacToe_CSharp //5
{
    class GameState //Here is the 3x3 game grid as 2 dimensional player array
    {
        public Player[,] GameGrid { get; private set; }
        public Player CurrentPlayer { get; private set; } //Need to know which players turn it is 
        public int TurnsPassed { get; private set; } //How many turns have passed
        public bool GameOver { get; private set; } // Need to know whether or not the game is over 
        


        //Need to define a few events. We need to impull the system namespace. MoveMade is the first event

        public event Action<int, int> MoveMade; /* We will erase this event once the player has successfully made a move / the reciever of the event must define an event handler with Type Action int int.
                                                * This is a method which takes two interger arguments and returns void, the two integers will be the row and column of the square that was marked by the move.
                                                * The next event we need is game ended */
        public event Action<GameResult> GameEnded; //This event will supply recievers with a game result object when the game has ended , then we will add a GameRestarted event which is raised when the game is started over.
        public event Action GameRestarted; 

        public GameState() //Created a consturctor which initializes the game grid and sets the current player.  
        {
            GameGrid = new Player[3, 3];
            CurrentPlayer = Player.X;
            //the gridgame contains player.none in every entry by default because that is the first enum member we defined, traditionally player x starts the game.
            //TurnsPassed to 0 GameOver to false for more clarity 
            TurnsPassed = 0;
            GameOver = false;
        }

                 /*Few helper methods : The first one returns whether or not the current player can mark a given square. They can if the game is not over and if the squares empty
                 */

        private bool CanMakeMove(int r, int c)
        {
            return !GameOver && GameGrid[r,c] == Player.None;
        }

        //Next method is IsGridFull()-The grid must be full if and only 9 turns have passed 

        private bool IsGridFull()
        {
            return TurnsPassed == 9; 
        }


        //Next when a move have been made we need to switch to the current player.

        private void SwitchPlayer()
        {
            CurrentPlayer = CurrentPlayer == Player.X ? Player.O : Player.X;
            //if (CurrentPlayer == Player.X)    //if else statement instead of conditional statement
            //{
            //    CurrentPlayer = Player.O;
            //}
            //else
            //{
            //    CurrentPlayer = Player.X;
            //}
        }

                                                                        //We must also know when a player has won the game, first a method which takes an array of square positions and a player 
                                                                        //It should be true if all the given squares are marked by the given player and false otherwise 
        private bool AreSquaresMarked((int, int)[] squares, Player player)
        {
            foreach((int r, int c) in squares)
            {
                if (GameGrid[r, c] != player)
                {
                    return false;
                }
            }
            return true;
        }

        //Now we can write a method to check if a given move won the game

        private bool DidMoveWin(int r, int c, out WinInfo winInfo)        /*It takes the row and column of the square marked by the current player & returns whether or not that move won the game.
                                                                          *If the move was in fact a winning move the method will provide additional info in the WinInfo out parameter*/
        {
            (int, int)[] row = new[] { (r, 0), (r, 1), (r, 2) };          //Thew newly marked square canbe part of a diagonal so we also create a array for the diagonal positions
            (int, int)[] col = new[] { (0, c), (1, c), (2, c) };
            (int, int)[] mainDiag = new[] { (0, 0), (1, 1), (2, 2) };     // If every square is marjed by the current player then that player has won the game 
            (int, int)[] antiDiag = new[] { (0, 2), (1, 1), (2, 0) };     // We'll use our 'AreSquaresMarked' method to check if that is the case, starting with the row 

            if(AreSquaresMarked(row, CurrentPlayer))                      //If AreSquaresMarked returns true , we know that row r is full. So in WinInfo object we set wintype.row and number to r 
            {
                winInfo = new WinInfo { Type = WinType.Row, Number = r};
                return true;
            }                                                             //We do the same thing for column squares but in the win info object we set type to wintype.column and number to c.

            if(AreSquaresMarked(col, CurrentPlayer))
            {
                winInfo = new WinInfo { Type = WinType.Colum, Number = c };
                return true;
            }
                                                                          //Next we check the two diagonals : main, anti - if any of them are marked we dont set the number property in the winInfo object 

            if(AreSquaresMarked(mainDiag, CurrentPlayer))
            {
                winInfo = new WinInfo { Type = WinType.MainDiagonal };
                return true; 
            }

            if(AreSquaresMarked(antiDiag, CurrentPlayer))
            {
                winInfo = new WinInfo { Type = WinType.AntiDiagonal };
                return true;
            }
                                                                          /*If we did not make it past these 4 if statements without returning true then 
                                                                           * the given move did not win the game, we then set wininfo = no; and false
                                                                           * Next we will create a method which checks if a move ended the game */

            winInfo = null;
            return false;
        }

        private bool DidMoveEndGame(int r, int c, out GameResult gameResult ) /*It takes the row and column of the square marked by the move & returns whether or not that move entered the game
                                                                               * If it did then we supply additional info in the GameResult out parameter. First we check if the given move won the game. */
        {
            if(DidMoveWin(r, c, out WinInfo winInfo))                         /*If it the did the winner is the current player, so in the game result object we set winner to the current player 
                                                                              * & pass the winInfo object from DidMoveWin.*/   
            {
                gameResult = new GameResult { Winner = CurrentPlayer, WinInfo = winInfo };
                return true;                                                //If the given move didnt win the game but the entire grid is full then the game ends in a tie 
            }

            if (IsGridFull())                                                //In the gameresult object we set winner to player.none and dont provide any winInfo
            {
                gameResult = new GameResult { Winner = Player.None };       //But the game is over so we return true 
                return true;
            } // If we make it pass these two if statements the game is not over so we set GameResult to null and return false.

            gameResult = null;
            return false;
        }

        //End of few helper methods, time to chain them all together in a public makemovemethod 

        public void MakeMove(int r, int c) /*It takes a row and col as parameters & if possible the square at that position will be marked by the current player 
                                            * First of all if the move cannot be made then we return without doing anything */
        {
            if(!CanMakeMove(r, c))
            {
                return;
            }                               //If the move is possible we perform it and increment TurnsPassed 

            GameGrid[r, c] = CurrentPlayer;
            TurnsPassed++;                  //Then we check if the move ended the game 

            if(DidMoveEndGame(r, c, out GameResult gameResult)) //If so we set gameover to true 
            {
                GameOver = true; //Then we invoke the movemade event with the given row and column, this will crash if there is no event handler registerd for the movemade event // We can make sure that there is an event hadnler by comparing it to null

                MoveMade?.Invoke(r, c);
                GameEnded?.Invoke(gameResult);  //If the move did not end the game we change the current player and invoke the move made event only 
            }
            else
            {
                SwitchPlayer();
                MoveMade?.Invoke(r, c);
            }
        }
        //One final method which resets the game state 

        //Here we replace the gamegrid with a new array which contains only player.none by default & the current player will be set to player.x again 
        public void Reset()
        {
            GameGrid = new Player[3, 3];
            CurrentPlayer = Player.X;
            TurnsPassed = 0;
            GameOver = false;
            GameRestarted?.Invoke();
        }
    }
}
