# Lab 01

**Reflections:**       
I had a great time in the lab with the team. Everyone was full of fantastic ideas and eager to participate in the exercises. We conducted the first activity (Abstract vs Concrete) collectively due to time limits, and then divided the remaining tasks amongst ourselves. I was paired with Tahira to work on Mancala examples and user stories.  

**Contributions:** 
- I set up & organized the gitlab group repository according to the template. 
- Set up a Google doc for easy collaboration and moderated the meeting for the lab session.
- On the Abstract vs. Concrete activity, I curated everyone's responses. Added it to the repository. 
- Did a call with Kenny to explain about examples & user stories       
&nbsp;

## Lab exercises
### 1.6.1.1 Abstract vs Concrete  
- **@jessica**  
The term "abstract" refers to a hazy portrayal of the "object" in question. What they imply now may vary over time or in different situations.      
By defining details that can be held on to, concrete "grounds" abstractions. They improve the stability of abstractions.      
Examples are concrete descriptions that give a vivid image of certain circumstances.          
Making design decisions on software projects entails identifying specific system properties that are related to the quality goals we want to attain. By doing this, we create abstractions. These abstractions are models that represent the system's concrete components. Examples are an effective means of describing and communicating these models to stakeholders from different domains.              
&nbsp;

### 1.6.2.3 Examples for Mancala

1. Merlin and Maggie have just begun a game of Mancala. She gathers all of the stones from the fourth pit on her side of the game board and drops one into each pit she encounters as she moves to the right. Her final pebble has landed in her Kalah. This means she will get another turn and will be able to play another round. Maggie, her opponent, will have to wait till she completes her turn.      
2. Merlin's game opponent, Maggie has just two pits filled with three pebbles each. Merlin sees that Maggie is at a disadvantage. She purposefully picks from a pit that does not have much pebbles and is farther away from her Kalah so that she only drops her pebbles in the pits on her own side of the board. This way, she can stall until Maggie empties out all of her pits and thus ends the game at a loss.      
&nbsp;

### 1.6.3.3 User Stories for Mancala      
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