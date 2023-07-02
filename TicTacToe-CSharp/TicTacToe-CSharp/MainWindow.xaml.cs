using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Animation;

namespace TicTacToe_CSharp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary> The game and the Ui will be controlleed from here, start by creating a functional ui and then add some animations
    /// Need two image sources on for player x and o and store them in dictionary 
    /// 

    public partial class MainWindow : Window
    {
        private readonly Dictionary<Player, ImageSource> imageSources = new Dictionary<Player, ImageSource>()
        {
            { Player.X, new BitmapImage(new Uri("pack://application:,,,/Assets/X15.png")) }, //right now both animations are empty so I am going to create a setup animations method 
            { Player.O, new BitmapImage(new Uri("pack://application:,,,/Assets/O15.png")) }  
        };

        private readonly Dictionary<Player, ObjectAnimationUsingKeyFrames> animations = new Dictionary<Player, ObjectAnimationUsingKeyFrames>()
        {
            {Player.X, new ObjectAnimationUsingKeyFrames() },
            {Player.O, new ObjectAnimationUsingKeyFrames() }
        };

        //we add two more animations of type double animation
        private readonly DoubleAnimation fadeOutAnimation = new DoubleAnimation
        {
            Duration = TimeSpan.FromSeconds(.5), //the duration is set to .5 seconds and it should produce values from 1 to 0. Similarily we will create a fade in animation 
            From = 1, 
            To = 0
        };
        //The fade out animation will be applied in a fade out method 

        private readonly DoubleAnimation fadeInAnimation = new DoubleAnimation
        {
            Duration = TimeSpan.FromSeconds(.5),
            From = 0,
            To = 1
        };


        private readonly Image[,] imageControls = new Image[3, 3];
        private readonly GameState  gameState = new GameState();

        public MainWindow()
        {
            InitializeComponent();
            SetupGameGrid();
            SetupAnimations();

            gameState.MoveMade += OnMoveMade;
            gameState.GameEnded += OnGameEnded;
            gameState.GameRestarted += OnGameRestarted;
        }

        private void SetupGameGrid()
        {
            for (int r = 0; r < 3; r++) 
            {
                for(int c = 0; c < 3; c++)
                {
                    Image imageControl = new Image();
                    GameGrid.Children.Add(imageControl);
                    imageControls[r, c] = imageControl;
                }
            }
        }

        private void SetupAnimations() //both animations to last for .25 seconds , then upload the animations from the assets folder , there are 16 images for each player named x0-x15 and o0-o15 so we need a loop with 16 iterations 
        {
            animations[Player.X].Duration = TimeSpan.FromSeconds(.25);
            animations[Player.O].Duration = TimeSpan.FromSeconds(.25);

            for(int i = 0; i < 16; i++) //inside the loop we load the next image for the x animation 
            {
                Uri xUri = new Uri($"pack://application:,,,/Assets/X{i}.png");
                BitmapImage xImg = new BitmapImage(xUri); // once its loaded we create a discrete object keyframe containing the image, then add it to the x animation  
                DiscreteObjectKeyFrame xKeyFrame = new DiscreteObjectKeyFrame(xImg);
                animations[Player.X].KeyFrames.Add(xKeyFrame);

                //exact same thing for the o animation 

                Uri oUri = new Uri($"pack://application:,,,/Assets/O{i}.png");
                BitmapImage oImg = new BitmapImage(oUri);
                DiscreteObjectKeyFrame oKeyFrame = new DiscreteObjectKeyFrame(oImg);
                animations[Player.O].KeyFrames.Add(oKeyFrame);
                //This method should be called in the constructor 
            }
        }


        private async Task FadeOut(UIElement uiElement) //first we begin the fade out animation on the opacity property of the ui element 
        {
            uiElement.BeginAnimation(OpacityProperty, fadeOutAnimation); //then we wait until it is finished 
            await Task.Delay(fadeOutAnimation.Duration.TimeSpan);       //after that we hide the ui element 
            uiElement.Visibility = Visibility.Hidden;
        }
        // we utilize these methods in our transitions 
        private async Task FadeIn(UIElement uiElement)//we make the uielement visible 
        {
            uiElement.Visibility = Visibility.Visible;
            uiElement.BeginAnimation(OpacityProperty, fadeInAnimation); //begin fade in animation then wait until it is finished 
            await Task.Delay(fadeInAnimation.Duration.TimeSpan);
        }

        //Method which hides the gamescreen and shows the end screen instead 
        private async Task TransitionToEndScreen(string text, ImageSource winnerImage) //takes a string which is the text that should appear at the end screen and an image source of which is the winning player, here we hide the turn panel and the game canvas 
        {
            //TurnPanel.Visibility = Visibility.Hidden;  //instead of changing the visibility directly we use fade in and fade out 
            //GameCanvas.Visibility = Visibility.Hidden; 
            await Task.WhenAll(FadeOut(TurnPanel), FadeOut(GameCanvas));
            ResultText.Text = text;
            WinnerImage.Source = winnerImage;
            //PlayerImage.Source = imageSources[gameState.CurrentPlayer];
            //EndScreen.Visibility = Visibility.Visible;
            await FadeIn(EndScreen);
        }

        private async Task TransitionToGameScreen() //method which takes us from the end screen back to the game screen 
        {
            //EndScreen.Visibility = Visibility.Hidden;
            await FadeOut(EndScreen);
            Line.Visibility = Visibility.Hidden;
            //TurnPanel.Visibility = Visibility.Visible;
            //GameCanvas.Visibility = Visibility.Visible; //when the game is restarted the ongamerestarted method will be called 
            await Task.WhenAll(FadeIn(TurnPanel), FadeIn(GameCanvas)); //we have to await them in  ongamended


        }

        //creating a method which takes a wininfo object and returns the start and endpoint of that line 

        private(Point, Point) FindLinePoints(WinInfo winInfo) //We save the squaresize and add a margin variable which is equal to half of the square size 
        {
            double squareSize = GameGrid.Width / 3;
            double margin = squareSize / 2; //from the data stored in wininfo we can determine where the line should appear on the screen , first we handle the case where the game was won by marking a row 

            if(winInfo.Type == WinType.Row) //In this case the line should be horizontal so both the start and end point will have the same y - coordinate 
            {
                double y = winInfo.Number * squareSize + margin; // this is at the vertical center of the row because the line should span the entire width of the grid , we return these two points
                return (new Point(0, y), new Point(GameGrid.Width, y)); //column case is handled similarily 
            }
            if(winInfo.Type == WinType.Colum)
            {
                double x = winInfo.Number * squareSize + margin;
                return (new Point(x, 0), new Point(x, GameGrid.Height));
            }
            if(winInfo.Type == WinType.MainDiagonal) //maindiagonal 
            {
                return (new Point(0, 0), new Point(GameGrid.Width, GameGrid.Height)); 
            }
            return (new Point(GameGrid.Width, 0), new Point(0, GameGrid.Height)); //antidiagonal - line goes from upper right corner to bottom left corner 
        }
        //method to show the line 
        private async Task ShowLine(WinInfo winInfo)
        {
            (Point start, Point end) = FindLinePoints(winInfo); //we grab the start and end point , then we use those points to set the start and in position of the line we added to the canvas earlier 

            Line.X1 = start.X;
            Line.Y1 = start.Y;

            //Line.X2 = end.X;
            //Line.Y2 = end.Y; //then we make the line visible 
            //animating the endpoint

            DoubleAnimation x2Animation = new DoubleAnimation
            {
                Duration = TimeSpan.FromSeconds(.25),
                From = start.X,
                To = end.X
            };

            DoubleAnimation y2Animation = new DoubleAnimation
            {
                Duration = TimeSpan.FromSeconds(.25),
                From = start.Y,
                To = end.Y
            };

            Line.Visibility = Visibility.Visible; //we will call showline from ongameended
            //then we apply the animations to the x2 and y2 property of the line
            Line.BeginAnimation(Line.X2Property, x2Animation);
            Line.BeginAnimation(Line.Y2Property, y2Animation);
            await Task.Delay(x2Animation.Duration.TimeSpan);
        }


        private void OnMoveMade(int r, int c) //This method is called when a move has been made 
        {
            Player player = gameState.GameGrid[r, c];
            //imageControls[r, c].Source = imageSources[player]; //Instead of setting the source here, we apply one of our animations instead.
            imageControls[r, c].BeginAnimation(Image.SourceProperty, animations[player]);
            PlayerImage.Source = imageSources[gameState.CurrentPlayer];
        }

        private async void OnGameEnded(GameResult gameResult) // we call method onmovemade by ongame ended, first we check if the game ended in a tie, if it did then the winner is saved as player.none .
        { //In this case we call the transition to end screen with the text its a tie and null as the winner image source.  Thhen make ongame ended async and wait for one sec before the transition 
            await Task.Delay(1000);

            if (gameResult.Winner == Player.None)
            {
                await TransitionToEndScreen("Its a tie!", null); // if we do have a winner we pass the text winner and an image of the winning player 
            }
            else
            {
                await ShowLine(gameResult.WinInfo); 
                await Task.Delay(1000); //after line is shown we will wait another sec before going to the end screen 
                await TransitionToEndScreen("Winner: ", imageSources[gameResult.Winner]);

            }
        }
        //making the transition beween the game screen and the end screen a bit more smooth 
        private async void OnGameRestarted() //the grid in the gamestate will be empty so we set the source of every image control to null so the grid also appears empty on the screen
        {
            for(int r = 0; r < 3; r++)
            {
                for (int c = 0; c < 3; c++)
                { // our animations are still in charge of the source for the images we have applied them to so we explicitly cancel them 
                    imageControls[r, c].BeginAnimation(Image.SourceProperty, null);
                    imageControls[r, c].Source = null;
                }
                PlayerImage.Source = imageSources[gameState.CurrentPlayer]; //Then we update the player image and call transition game screen 
                await TransitionToGameScreen();
            }
        }

        private void GameGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            double squareSize = GameGrid.Width / 3;
            Point clickPosition = e.GetPosition(GameGrid);
            int row = (int)(clickPosition.Y / squareSize);
            int col = (int)(clickPosition.X / squareSize);
            gameState.MakeMove(row, col);
        }

        private void Button_Click(object sender, RoutedEventArgs e) //is invoked when the play again button is clicked, here we will call gamestate.reset
        {
            if (gameState.GameOver)
            {
                gameState.Reset(); //we also need a method which takes us from the end screen back to the game screen 
            }

        }
    }
}
