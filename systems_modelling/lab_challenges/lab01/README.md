# Lab 1

## 1.6.1.1 Abstract vs Concrete

**1. Discuss in your group the terms “abstract” and “concrete”. Give for each term 10 examples.**
Abstract:  Love, Kindness, Moral, Hate, Fear, Worry, Freedom, Success, Good, Evil
Concrete: Yellow, Table, Keys, House, Mask, Slippers, Water, Car, Mug, Wool
&nbsp;

**2. Create a table with the columns abstract and concrete. Find at least 10 sample pairs and add them to the table.**
| Abstract       	| Concrete       	|
|----------------	|----------------	|
| Flowers        	| Roses          	|
| Plants         	| Flowers        	|
| Intelligence   	| Dog            	|
| Algorithm      	| Bubble Sort    	|
| Wealth         	| Money          	|
| Money          	| Cryptocurrency 	|
| Cryptocurrency 	| NFT            	|
| Relationship   	| Marriage       	|
| Float          	| 1.5            	|
| Emoji          	| 😀              	|
&nbsp;

**3. Have a contest on the most concrete example e.g. for a car.**
| Name    	| Concrete Car                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            	|
|---------	|---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------	|
| Kenny   	| Latest 2021 Lexus LC which ranks one of the best in sport car rankings                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  	|
| Ihar    	| Saab 9.3 2009 produced in Denmark in Copenhagen at Fictional Straße in the morning by two nice guys, Ada and Bob, who BTW are engaged and have a dog                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    	|
| Jessica 	| Lexus RX 450h 2019, powered by an environmentally friendly hybrid engine with a Velodyne VLP-32C lidar, which allows obstacles to be detected from a distance of up to 130 meters, a NovAtel PwrPak7D navigation device with inertia sensor and RTK correction, which allows the car to be positioned with an accuracy of up to 5 cm, an Allied Vision Mako G-319C cameras that detect obstacles as well as traffic lights, a Delphi ESR 2.5 radar, which detects oncoming cars and is able to determine their speed, and an AutonomouStuff Spectra computer with Nvidia GTX 2080 video card, designed to withstand vibration and run on battery power. 	|
| Monika  	| Tesla Model X - powered by solar and clean energy                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       	|
| Maggie  	| Electric car Mustang Mach-E electric SUV, Moves up to 88kWh                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        	|
&nbsp;

**4.Based on the discussions of the first two exercises, create definitions for “abstract”, “concrete”, and “example”. Examples from the book/lecture are not allowed. Write one paragraph about what these terms could have to do with modelling.**
- **@kehindee aka Kenny**
Abstract: Something we cannot see or touch eg smell
Concrete: May often be tangible eg flower
Modelling has to do with expressing a design, which involves bringing an idea into reality so the idea would be abstract in this case and design would be concrete. For instance ideas or a words (abstract) can be given to develop a system and a UML diagram (concrete) is used to express that idea.

- **@suvorau aka Ihar**
Abstract vs Concrete is a concept which describes a relationship between two items.
“Abstract” tries to capture the whole class of phenomena, while “concrete” picks one thing from the class and specifies details making a unique instance of an abstract thing.
“Example” is something that is more real, than general, it has more details and is closer to “life”, so, it can be useful in bringing the theory to practice and the other way around.
Modelling works with abstract and concrete terms, it generalizes over many details helping to devise a simple and coherent design. Modelling is like a framework, a structure for the implementation of the model. Taking software as an example, modelling generalizes over particular classes, functions and variables to present the system as a comprehensible structure.

- **@tahira**
Abstract leads you to the land of questions whilst answering these questions, can define the concrete term. For example, the term "flower" as an abstract term can lead to a question of what flower.  Answering this would state the specific group from the flower. The term “Rose” is the concrete term that answer our abstract term question.
**Disclaimer**: The above definition works better when we are talking about pairs e.g. one can have a concrete term without abstract term.
As software systems have different stakeholders from different backgrounds, so modelling can provide us a good abstraction level and makes it easier to understand. On the other hand, the structure, design, and notations to define or represent the abstraction level are concrete to support being specific.

- **@shrestha aka Monika**
Abstract is something which is limited within ideas or describes the things in a more generalized form that has the common properties.
Concrete doesn’t really stay within ideas, they are defined in the real world and is the small portion of the generalized form (ex. Flower is the whole universal set that includes the flowers of many types whereas rose is the subset of that universal set and is specific)
In Modelling we convey our imagination through diagrams and examples on a piece of paper (abstract), but when we start modelling, the actual version of it is changed to a physical form (concrete) for eg. creating a robot that can speak and interact with us using the blueprint of it

- **@jessica**
The term "abstract" refers to a hazy portrayal of the "object" in question. What they imply now may vary over time or in different situations.
By defining details that can be held on to, concrete "grounds" abstractions. They improve the stability of abstractions.
Examples are concrete descriptions that give a vivid image of certain circumstances.
Making design decisions on software projects entails identifying specific system properties that are related to the quality goals we want to attain. By doing this, we create abstractions. These abstractions are models that represent the system's concrete components. Examples are an effective means of describing and communicating these models to stakeholders from different domains.      
&nbsp;


## 1.6.2.1 Examples for Mau Mau
**4 additional rules** are taken from https://en.wikipedia.org/wiki/Mau-Mau_(card_game):

- When a player has only one card left, they must say “Mau” (even if it is an Ace); if that card is a Jack, they must say “Mau-Mau”. Failure means that the player must take a card.
- If the game is scored, and the winning card is a Jack, then all points against the losers are doubled.
- Ace reverses the order of play.
- King of spades forces to take three cards and skip.

**8 examples** that satisfy the rules and help to explain them:

1. Alice has remained with the last card HA, but says nothing. After one more round, when her turn comes, she puts the last card on the playing deck and claims her win. However, the previous player notices that she hasn’t said “Mau”, when she remained with one card in hand, and refutes her win. Alice takes one more card from the drawing deck and stays in the game. The order of play is reversed now, because the last card on the deck was HA, and Ace reverses the playing order.
2. Bob has multiple cards in hand. The previous player put DQ before. From diamonds, Bob has only 8, so, he plays with D8 on top of DQ. The next player, Alice, skips her turn, because any 8 forces a plyer to skip a turn.
3. Alice plays with SJ. It doesn’t matter what card was before, because Jack can be played on top of any other card. Alice changes the suit to hearts. The next player, Bob, puts H9, because the value of a card played on Jack doesn’t matter, only a suit counts.
4. The previous player before Bob played with D7. Bob now has to draw two cards from the drawing deck, but the deck is empty. So, a free player takes the playing stack except the last card, shuffles it and turns over. Bob now can draw two cards and the game continues.
5. Alice has to reply on S10, but has no any card of spades, neither she has any 10. Alice has to draw one more card from the drawing stack, and see if she can play with the new card. The new card is H10. Alice then plays with H10, and the turn goes to the next player.
6. Bob answers with SK on top of S10. The next player, Alice, now has to take three cards from the drawing stack and skip the turn regardless of cards she has in hand.
7. Alice plays with D9 on H9. The next player can play either with diamonds or 9 of any suit. Bob doesn’t have 9, but has D10 instead. He plays with D10, and the turn goes further.
8. Bob remained with the last card, DJ. So, he says “Mau-Mau” and waits for his last turn. When the turn comes, he puts DJ. As the players are keeping score of the game and the last winning card was Jack, all points of other players except Bob are doubled. The Bob wins the game.
&nbsp;

## 1.6.2.3 Examples for Mancala
1. Merlin and Maggie have just begun a game of Mancala. She gathers all of the stones from the fourth pit on her side of the game board and drops one into each pit she encounters as she moves to the right. Her final pebble has landed in her Kalah. This means she will get another turn and will be able to play another round. Maggie, her opponent, will have to wait till she completes her turn.
2. Merlin's game opponent, Maggie has just two pits filled with three pebbles each. Merlin sees that Maggie is at a disadvantage. She purposefully picks from a pit that does not have much pebbles and is farther away from her Kalah so that she only drops her pebbles in the pits on her own side of the board. This way, she can stall until Maggie empties out all of her pits and thus ends the game at a loss.
3. Merlin and Marry are good friends, and they decided to play Mancala board game on the weekend game night. Merlin is already familiar with the game, and she told all rules to Marry. They both started the game and followed the rules. Mary acquired 13 stones, and Merlin acquired 16 stones. Marry has beginner luck and has six stones in her pits. At the same time, Merlin has only one stone in the pit next to Kahala, which means she plays her turn and puts her last stone in the Kahala. As a rule, there are no stones in the Merlin pits; the game will be over,  and all the eight leftover stones in Marry pits will go to her Kahalas. At the end of the game, Marry got nineteen stones in her Kahala and won the game.
4. Merlin and Marry are good friends, and they decided to play Mancala board game on the weekend game night. Merlin's first pit has two stones, and the last stone of a Merlin lap falls into an empty pit of her sides. Merlin captured all the stones from Marry's opposite pit along with her stone.      
&nbsp;

## 1.6.2.6 Examples for ATM
_By @shrestha and @kehindee_

**Deliverable: 8 Examples**

1. *Insert an ATM card that you want to use to withdraw money* <br>
Angela wants to withdraw a total of 500 euros from her account. To take out that amount from the ATM, she needs an ATM card. Therefore, she went to her bank’s nearest branch to get the card. After a few minutes, she was issued the card by one of the bank employees. Angela now has the card that she wanted and finally, she could insert her card to the ATM. At last, she is able to withdraw her 500 euros from her account.

2. *Select the preferred language of your choice* <br>
Angela knows 3 different languages: English, Russian and Estonian.  English was the default language on the machine’s screen. Since she has to select her preferred language, she selected Estonian because it’s her native language. Then, she followed the instructions shown on the screen and withdrew the cash.

3. *Enter PIN number which consists of 4 digits* <br>
Jenny wants to reset her PIN but for her to reset her PIN code, she has to know the previous PIN. Thus, she entered her old PIN code and entered the new one to change it.

4. *Selection of transaction type (cash withdrawal - in our case)* <br>
Karli is willing to take out some amount from his account. So, he has to select the cash withdrawal option from the ATM in order to continue his transaction.

5. *Selection of account type (savings, current, etc)* <br>
Jenny wants to withdraw 600 euros in cash from her account but her account type which is savings only permits 500 euros withdrawal per day. So, she has to enter the amount limited to her account type inorder to withdraw the amount she desires.

6. *Input or select the withdrawal amount* <br>
Katrin wants to withdraw 700 euros from her account but when she inputted the amount she was notified that her account balance is less than the amount that she wants to withdraw. So, Katrin has to withdraw 680 euros because she cannot withdraw all her money from her account.

7. *Dispensing of cash by the ATM* <br>
Moana wants to withdraw a total of 500 euros but she wants to withdraw 50 euros in 10 places since she needs a smaller denomination of euros. But, the ATM can only dispense 100 euros in 5 places. So, she is compelled to withdraw 100 euros in 5 places.

8. *Take a printed receipt, if necessary* <br>
Katrin was asked by her manager to withdraw a certain amount of money from the ATM.
After withdrawal, Katrin was left with a choice to take her receipt from the ATM or go without taking it. Since she needed her receipt to show it to the manager she had to choose the option of taking the transaction receipt.      
&nbsp;

## 1.6.3.3 User Stories for Mancala
**Title: Merlin can get another turn after playing one round**
1. Initial Situation - The Mancala board is placed on the table between Merlin & Maggie. The Mancala board consists of fourteen pits - six playing pits plus one score pit (the kalah) per player. Both Merlin and Maggie have 3 pebbles in each of their playing pits to start the game.
2. Merlin takes the first turn, she picks up all three pebbles from the fourth pit on her side of the board. The fourth pit is now empty. She moves to the right.
3. Merlin drops one pebble into the fifth pit on her side of the board. The fifth pit now has four pebbles, and she has two pebbles left in her hand. She moves to the right.
4. Merlin drops one pebble into the sixth pit on her side of the board. The sixth pit now has four pebbles, and she has one pebble left in her hand. She moves to the right.
5. Merlin drops the only pebble left in her hand in her Kalah. She has no pebbles left in her hand. Her Kalah has one pebble in it now. She is eligible to play another round. To play another round, Merlin is allowed to pick from any of the pits that have pebbles in them.
&nbsp;

**Title: Merlin stalls to win the game**  
1. Initial Situation - All of Merlin's six playing pits have at least one pebble in them. She also has a number of pebbles in her Kalah from the game play. Maggie has 6 pebbles in her fifth pit & 4 pebbles in her sixth pit. She also has a number of pebbles in her kalah from game play. It is Maggie's turn to play.
2. Maggie picks all 6 pebbles in her fifth pit. Moving right, she drops pebbles into each pit she comes across. She gets to her Kalah. She drops one in, but still has 4 pebbles in hand. She continues moving and drops off all the pebble's in Merlin's pits, stopping in the fourth pit. Her turn ends.
3. Merlin purposefully picks up the 4 pebbles from the 1st pit on her side of the board. Moving right, she drops pebbles into each pit she comes across. She drops the last pebble in the fifth pit on her side of the board.
4. Maggie picks up all the 4 pebbles from her sixth pit. This is the only pit left with pebbles. She drops one into her Kalah, and continues till she drops all the pebbles in Merlin's pits stopping in the third pit. Her side of the board is now empty. The game ends.
5. Merlin counts all the pebbles in her Kalah plus the pebbles in the 5 pits on her side of the board. She has more than half of the pebbles on the board. She wins the game.
&nbsp;

**Title: Merlin makes an invalid move which the system rejects**      
1. Initial Situation - Merlin has pebbles in 5 playing pits, and 9 pebbles in her kalah. Maggie has pebbles in 3 playing pits only, and 10 pebbles in her kalah.
2. Merlin has just finished a turn. The system recognizes that it is Maggie's turn to play.
3. Merlin clicks her fourth pit to go on another turn after finishing her turn.
4. The system marks the move as invalid and does not distribute the pebbles to the respective pits.
5. The system shows an error message to Merlin saying that it is not her turn to play.
6. Maggie can play her turn successfully.
&nbsp;

**Title: The system counts all pebbles to declare the winner**      
1. Initial Situation - Merlin has 7 pebbles shared across 5 of her playing pits, and 15 pebbles in her Kalah. Maggie has two pebbles in her fifth & sixth pits, and 14 pebbles in her kalah. It is her turn to play.
2. Maggie picks up the pebble from her fifth pit, and moving right, places it in her sixth pit. Her turn ends.
3. Merlin picks up the pebbles in her 1st pit. They are two in number. Moving right, she drops it in her second and third pit respectively. Her turn ends
4. Maggie picks up the 2 pebbles from her sixth pit. She places one in her kalah, and moving right, places one in Merlin's first pit. Her side of the board is now empty, the game ends.
5. Merlin still has pebbles on her side of the board. The systems adds all of them to her kalah.
6. The system redristibutes each person's pebbles from their kalah to each of the pit to mirror new game state. Each pit is filled with three pebbles.
7. Merlin's side of the board has been filled with three pebbles each, with 5 left over in her kalah. This places Merlin as the winner as she has more than half of the pebbles on the board.
&nbsp;

**Title: Game Ends - Opponent’s pit are empty**      
**Initial Situation:** Merlin and Marry decided to play Mancala game on the weekend game night. Mancala game is a two-player game and has six pits and one special pit for points collection named Kahala. Merlin and Marry have three stones in six pits and empty kahal on their sides at the beginning of the game. Merlin and Merlin started playing the game and exchanged games turn based on the rules.
1. Marry has collected 13 stones in her Kahala and moved her stone from the third pit, and her lap last stone landed in one of her pits, which ended her turn.
2. Merlin collected 16 stones in her kahala and left only one stone in her last pit next to her Kahala. She is left with no other choices. She moved her last stone that goes to her Kahala. In total, her Kahala has 17 stones now but no stone in any of the six pits.
3. The game is over as Merlin has no more stones to move around in her pits.
4. All the leftover six stones in Marry's pit will go to her Kahala, and now she has 19 stones. Mary's won the game because she had more stones than Merlin in her Kahala.
&nbsp;

**Title: Taking opponents stones when a player has an empty pit**      
**Initial Situation:** Merlin and Marry decided to play Mancala game on the weekend game night. The mancala game is a two-player game with six pits and one special pit for points collection named Kahala. Merlin and Marry have three stones in six pits and empty kahal on their sides at the beginning of the game. They both started the game and collected the stones in their Kahalas. 
 1. Marry has collected 13 stones in her Kahala and moved her stone from the first pit. Her first pit has 3 stones, and she put one stone in the second, third, and fourth pit. 
 2. Marry dropped the last stone is in her own pit; now her turn is over, and Merlin got her turn.
 3. Merlin has only two stones in her third pit. She has one stone in her fourth pit, and her fifth pit is empty. She moves her stone and put the stone in her fifth empty pit. 
 4. Merlin's last and only stone landed in the empty pit. She captures all stones from the Marry's opposite pit i.e. second pit. All three stones from Marry's second pit and Merlin's stone from her fifth pit goes to Merlins Kahala.
&nbsp;

**Title: Give a scenario for describing the last three turns of a Mancala game making the game ending in a tie.**      
**Initial Situation:**  Merlin and Marry have 15 pits in their Kahalas and 3 stones in their pits, as shown in fig.
1.  Merlin moves 2 stones from her third pit and drops one stone in the fourth and fifth stone. 
2. Merlin last stone landed in the empty pit on her side, therfore, she captures the opposite pit stone from Marry stone. 
3. Merlin kahal gets 2 stones in total, and her turn is over.
4.Marry move her 2 stones from the fourth pit and drop one stone is the fifth and sixth pit. 
5. Marry's last stone landed in the empty pit; she also captures Merlin's fifth pit stone. 
6. Marry got two stones in her Kahala, and her turn is over.
7. Marry and Merlin both have 1 stone in pits and 17 stone in Kahala. The count of stone is the same; neither Merlin nor Marry won; the game was a tie.      
&nbsp;      

## 1.6.3.6 User Stories for ATM Money Withdrawal
_By @kehindee and @shrestha_ <br>

**Extend the eight examples from Section 1.6.2.6 to scenarios.**

**Title:** *Angela needs a card.* <br>
1. Initial situation: Angela wanted to withdraw money from the ATM but she doesn’t have a card.
2. Angela wants to withdraw a total of 500 euros from her account.
3. To take out that amount from the ATM, she needs an ATM card.
4. Therefore, she went to her bank’s nearest branch to get the card.
5. After a few minutes, she was issued the card by one of the bank employees.
6. Angela now has the card that she wanted and finally, she could insert her card to the ATM. At last, she is able to withdraw her 500 euros from her account.
&nbsp;

**Title:** *Angela selects her preferred language.* <br>
1. Initial situation: Angela wanted to withdraw Cash but needs to select the language she best understands.
2. Angela knows 3 different languages: English, Russian and Estonian.
3. English was the default language on the machine’s screen.
4. Since she has to select her preferred language, she selected Estonian because it’s her native language.
5. Then, she followed the instructions shown on the screen and withdrew the cash.
&nbsp;

**Title:** *Jenny tries to enter the PIN code.* <br>
1. Initial situation: Jenny went to the ATM to change her PIN code.
2. Jenny wants to reset her PIN but for her to reset her PIN code, she has to know the previous PIN.
3. Thus, she entered her old PIN code and entered the new one to change it.
&nbsp;

**Title:** *Karli selects the transaction type.* <br>
1. Initial situation: Karli wanted to withdraw money from the ATM according to her transaction.
2. Karli is willing to take out some amount from his account.
3. So, he has to select the cash withdrawal option from the ATM in order to continue his transaction.
&nbsp;

**Title:** *Jenny selects the account type.* <br>
1. Initial situation: Jenny wanted to withdraw money from the ATM according to her account type.
2. Jenny wants to withdraw 600 euros in cash from her account but her account type which is savings only permits 500 euros withdrawal per day.
3. So, she has to enter the amount limited to her account type inorder to withdraw the amount she desires.
&nbsp;

**Title:** *Katrin selects the withdrawal amount.* <br>
1. Initial situation: Katrin wanted to withdraw money from the ATM and she needs to specify the amount.
2. Katrin wants to withdraw 700 euros from her account but when she inputted the amount she was notified that her account balance is less than the amount that she wants to withdraw.
3. So, Katrin has to withdraw 680 euros because she cannot withdraw all her money from her account.
&nbsp;

**Title:** *Moana dispenses cash by the ATM.* <br>
1. Initial situation: Moana wanted to withdraw money of smaller denomination.
2. Moana wants to withdraw a total of 500 euros but she wants to withdraw 50 euros in 10 places since she needs a smaller denomination of euros.
3. But, the ATM can only dispense 100 euros in 5 places. So, she is compelled to withdraw 100 euros in 5 places.
&nbsp;

**Title:** *Katrin prints the transaction receipt.* <br>
1. Initial situation: Katrin wants a receipt for her transaction.
2. Katrin was asked by her manager to withdraw a certain amount of money from the ATM.
3. After withdrawal, Katrin was left with a choice to take her receipt from the ATM or go without taking it.
4. Since she needed her receipt to show it to the manager she had to choose the option of taking the transaction receipt.
&nbsp;
