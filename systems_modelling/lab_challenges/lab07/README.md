# Lab 07

## Mancala Sequence Diagrams
![](../../portfolios/kenny/lab-notes/Lab07/Lab07_2.PNG)
![](../../portfolios/monika/lab-notes/lab07/sequencediagram1.jpg)

## Mancala Wireframes
The wireframe prototypes below cover a set of scenarios defined in earlier lab exercises. A brief summary includes:
- User plays another turn if they land on their kalah
- Three rounds of game play 
- Waiting for second player in game lobby
- Game over situation with a player win

I have also included comments on balsamiq to guide through the process...      
(Link to balsamiq wireframes)[https://balsamiq.cloud/sqb0mwf/pveekag/r2278]      
&nbsp;

## Mancala GUI
The source code is available at [mancala-gui](../../portfolios/ihar/lab-notes/lab07/mancala-gui).      
The [JavaFX](https://openjfx.io/index.html) framework has been chosen, because *Node-RED* seemed pretty limited and we preferred code-driven approach rather than workflow-based GUI design.

Here's the screenshot of the GUI:
![](../../portfolios/ihar/lab-notes/lab07/5_turn_two.png)
Both players has been added to the game. Player 2 has just made his turn.      
During GUI development, 1 bug has been discovered and fixed in the network controller.
