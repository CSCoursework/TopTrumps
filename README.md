# Top Trumps
Old project uploaded late. Completed on Fri 13 Nov 2020 at 01:35

## Planning:

There was a small deal of planning that went into this project. Firstly I came up with and planned a theme for my project. I choose us presidents as it was highly topical at the time and fit the clients brief (of a top trumps game).Then I opted to use a class to generate the cards. The first step in my planning was drawing out the class diagram. This has been recreated on ms-paint and has been shown below:

![](C:\Users\vivek\Documents\Computer Science\Writeups\Top Trumps\Card Class diagram.png)

As you can see the use of classes is fairly basic but the plan helped me to easily code a working class for my project which was a major concern due to this only being the second occasion I have ever attempted object orientated programming. Henceforth from here on in the project I took a more extreme programming approach and the plans became loose and far between. They where also mostly in shorthand therefore I will leave the class diagram as the sole example of my planning. My code was also planned to easily add custom card creation should anytime in the future I decide to do.

However before this planning I had to decide on a list of rules. I have listed them below.

### Rules:

- All cards can be seen when choosing
- Higher value always wins
- In the event of a draw the current player (the one who chose the category) wins

Other things that where decided was a set of goals:

### Success Checklist:

- [x] Create a working game
- [x] Random Shuffling
- [x] Keep it efficient
- [x] Easy to append features such as custom card creation and a bigger deck from a text file.

## Design:

I opted to go for a simple command line interface for this program. For this project I was on a tight time constraint so the interface is not fully polished yet. However the program is still in what most would consider it's prototype stages. In the future if I revisit this project there are definitely aspects I would improve however an example of the design language of my project has been placed below. That is a top trump card shown as the program would display it.

![](C:\Users\vivek\Documents\Computer Science\Writeups\Top Trumps\p1Deck.png)

As you can see the program has an ascii art feel which I planned to feel quite childish and _toylike_. However without the colour and the styling incomplete it is hard too tell whether I met this objective so I will leave it up to you , yes the reader, to decide.

## The Code (methods and all):

###  The Class Card:

This is the first bit of code in the document and it defines what a card is through a method.

```c#
   class card
    {
        public string name { get; set; }
        public int rasicm { get; set; }
        public int assinationattmpts { get; set; }
    }
```

I decided to use a class for cards as it was a new journey for me with object orientated program and it was something I was fairly certain it was ideal for. Looking back in hindsight I agree with myself as it was a piece of code that was used over and over to create cards,decks and so much more. This is backed up by the 40 references to this class over the whole project. This for a first use,granted I was only using public attributes and had no child classes, went surprisingly well and went exactly as planned. Another benefit of this is that it allows easy custom card creation as specified in the plan if I'm so inclined.

### Card Creation Method:

This is the code I used to generate the cards. It was a method which set all the attributes for the class card.

```c#
static card newcard(string name, int rascim, int attmpts)
        {
            card toprump = new card();
            {
                toprump.name = name;
                toprump.rasicm = rascim;
                toprump.assinationattmpts = attmpts;
            }
            return toprump;
        }     
```

This method takes in three properties for the attribute , creates a new instance of card and sets the attributes. This is done because it is efficient and would require almost no modification to convert to custom card creation as mentioned in the plan. The  attributes are the top trump categories. This code could also quite easily be modified to add the other two planned categories on the class diagram. This is why this project is still a prototype as I previously stated.

### The WriteCard method:

This is the method I use for writing out a card along with all of its aspects. The output of this is available to be seen in the Design section.

```C#
static void writecard(card toptrump)
        {
            Console.WriteLine("-----------------------");
            Console.WriteLine("¦Name:                 ¦" + toptrump.name);
            Console.WriteLine("¦----------------------¦");
            Console.WriteLine("¦Racism:               ¦" + toptrump.rasicm);
            Console.WriteLine("¦----------------------¦");
            Console.WriteLine("¦Assasination Attempts:¦" + toptrump.assinationattmpts);
            Console.WriteLine("-----------------------");
        }
```

This takes a card and writes out the attributes in a neat table. This method is crucial in a user playing the game as it is approximately 90% of all output by character . It is also a fairly simple method so not much write up is required. 

### The Pointless Method

This is a method used for measuring how long a list is. I later realised as I was writing a report it was pointless as there is an inbuilt method called `list.count`. This misunderstanding is probably a result of it being `array.length` for an array. However while keeping the code inefficient I decided to leave it in to serve as a reminder for future. If the code is updated for card creation I will remove this in the streamlining phase.

```c#
     static int listlength(List<int> list)
        {
            int i = 0;
            foreach (var value in list){i++;}
            return i;
        }
```

This method takes in a list. Declares an integer for counting. Then the method iterates that integer for each value in the list. Overall this method doesn't have a drastic impact on efficiency as it is quite efficient.

### Pick a card...

This method makes asks the player to pick a  card from the current player deck.

```c#
static card chooseplayercard(int player, card[] cplayerdeck)
      {
         Console.WriteLine("Player " + player + " deck:");
         int i = 0;
         int pickcard = cplayerdeck.Length + 1;
         foreach (card value in cplayerdeck) { Console.Write(i); writecard(value); i++; }
         Console.WriteLine("Player " + player + " please choose a card");
         card test = null;
         while (pickcard > cplayerdeck.Length)
           {
             string INPUT = Console.ReadLine();
             try { pickcard = Int32.Parse(INPUT); test = cplayerdeck[pickcard]; }
             catch { Console.WriteLine("Try again looser!"); }
           }
           Console.Clear();
           return test;
       }
```

This method takes in the current and a card array which is the current players deck. Then it asks the player to pick a card from the array that is their deck. It then lets the player know who should be typing. After this it sets an integer for iterating and writes each card in the deck along with its position in the deck. The variable for the picked card is also set to invalid length. The code then enters a while statement that only breaks when the variable picked card is valid.  This while loop takes input and parses it to an integer in a try catch statement until it is a valid number and not a letter or too high. It then returns that user selected card as a variable named test. It was named this in debugging and has stayed this way as the program is a porotype and hasn't been streamlined yet as was aforementioned.

### Take your turn

This method handles the player turn and returns the winning player as an integer.

- ```c#
   static int playerturn(int player,int openent, card playercard, card openentcard)
          {
              Console.WriteLine("Please Choose a category by writing the first letter of that category");
              string selection = Console.ReadLine();
              card winner = null;
              while (winner == null)
              {
                  switch (selection)
                  {
                      case "r":
                          if (playercard.rasicm > openentcard.rasicm) { winner = playercard; } else { winner = openentcard; }
                          break;
                      case "a":
                          if (playercard.assinationattmpts > openentcard.assinationattmpts) { winner = playercard; } else { winner = openentcard; }
                          break;
  
                      default:
                          Console.WriteLine("try again looser");
                          selection = Console.ReadLine();
                          break;
                  }
              }
              if (winner == playercard) { return player; } else{ return openent; }
   }
  ```

This method may look fairly long however it is fairly simple when broken down. The code starts off by taking in all 4 variables (the player and their cards). It then asks the current player to write the first letter of the category they want while in a switch case. It also declares a variable that controls the while loop (winner). The while loop states while the winner is undecided the category must be unchosen so select a category on repeat until a valid category is chosen. The switch case then for each valid category compares the two values from both cards in an in IF statement. The higher card will always win. If there is a win the current player gets the advantage and keeps the card as mentioned in the rules from the planning stage. The final line is an if statement that returns the (integer) player number of the winner.

### Ha ha looser:

This method takes the looser array and returns an identical array just one item shorter. To make the array shorter it removes the loosing card from the array. Hence the name `removeloosercard` . 

```c#
        static card[] removeloosercard(card[] looserdeck, card loosingcard) 
        {
            card[] tempL = new card[looserdeck.Length - 1];
            int i = 0;
            int newI = 0;
            foreach (card value in looserdeck) 
            {
                if (looserdeck[i] != loosingcard)
                {
                    tempL[newI] = looserdeck[i];
                    i++;
                    newI++;
                }
                else{ i++; }
            }
            return tempL;
        }
```

This method was a key part of the stupid idea to store the player decks in arrays. That was probably due to a lack of adequate planning as this section was planned with incredible speed. This method starts off by declaring an array called `tempL` for temp looser. This method creates an array exactly one shorter then the loser's deck. The code then declares two variables `i` and `newI` which are the places in there respective array. `i` is the place of a card in the old array and `newI` is the place in the new array. The logic of transferring all but the card that lost goes as so (if there is any ,using an array was a bad decision,). If the loosing card is not the loosing card the position (`newI`) is equal to that  `i` value. Next `newI` and `i` are both iterated. if the value is the loosing card only `i` iterates. This means that card is skipped in transferring then it continues as normal for each remaining card in `looserdeck` till all the cards are transferred.

### I am the Champion no time for losers:

This code does a similar job to the aforementioned method except the inverse. It takes the winners deck array and creates an array which is precisely 1 item longer. Then it adds the looser card to it.

```c#
static card[] addloosercard(card[] winnerdeck, card loosercard)
        {
            int i = 0;
            card[] tempw = new card[winnerdeck.Length + 1];
            foreach (card value in winnerdeck)
            {
                tempw[i] = winnerdeck[i];
                i++;
            }
            tempw[i] = loosercard;
            return tempw;
        }
```

As this method is fairly similar to the last one I will keep this writeup portion brief. This method declares an integer and an array called `tempw` which is short for temp winner. It is exactly one longer and just iterates adding each value to `tempw` and then adds the loosing card. When streaming this method I would use a `for` statement instead of the `foreach` as the entire `foreach` statement could be replaced by 1 line.

### The Rude Goldberg Method:

This method is the bane of my existence. The single biggest error I made in this program. It has slowed down the creation of program significantly. If I where to stream line this I would most certainty use lists for player decks. 

```c#
   static void arrayupdating(int winner,card[] player1deck,card[] player2deck,card player1card,card player2card) 
    {
        if (winner == 1)
        {
            card[] temp = new card[player2deck.Length - 1];
            temp = removeloosercard(player2deck, player2card);
            Array.Resize(ref player2deck, player2deck.Length - 1);
            player2deck = temp;
            Array.Resize(ref temp, player1deck.Length + 1);
            temp = addloosercard(player1deck, player2card);
            Array.Resize(ref player1deck, player1deck.Length + 1);
            player1deck = temp;
        }
        else
        {
            card[] temp = new card[player1deck.Length - 1];
            temp = removeloosercard(player1deck, player1card);
            Array.Resize(ref player1deck, player1deck.Length - 1);
            player1deck = temp;
            Array.Resize(ref temp, player1deck.Length + 1);
            temp = addloosercard(player2deck, player1card);
            Array.Resize(ref player2deck, player2deck.Length + 1);
            player2deck = temp;
        }
    }
```
This method takes in both players decks,the winner and both card the players played. The method then splits into to nearly identical methods that do the same but switches the winner around. I will talk through as if the `if` statement was true and player 1 won the round. I will go through line by line and explain exactly what each line works. The first line declares a temporary array 1 smaller then loosing players hand. It then sets that array as equal to the result of the method `removloosercard` (mentioned under the subheading Ha Ha looser). Which if you remember correctly is just the player hand without the loosing card. The next line uses the built in c# method `array.resize()` to change the size of the array. To the best of my understanding this method works by removing all previous references to that array and declaring a new one that is exactly the bigger/smaller by the new size specified after the comma. Then player the loosing players deck (in the aforementioned scenario player 2) is set equal to temp (the same deck but without the loosing card).The next 3 lines do the inverse for the winning player. To specify: The first out of the three lines resizes the size of temp (I didn't want to declare a new temp because I am incredibly lazy). Then it uses the method `addloosercard` (referenced as I am the champion... in the report) to set temp as equal to the winner deck and the loosing card . The last of the 3 lines sets the winner deck as equal to temp. 

### Finally Main...

The method Main does quite a lot so I have split it up into sections:

The first of which use the object `card` (referenced as the Class Card in this report) and method `newcard` (referenced as Card Creation Method in this report).

#### Card list:

```c#
    static void Main(string[] args)
        {
            card[] cardlist = new card[4];
            cardlist[0] = newcard("Trump", 76, 2);
            cardlist[1] = newcard("Obama", 8, 1);
            cardlist[2] = newcard("Lincoln", 1, 2);
            cardlist[3] = newcard("Roosevelt", 85, 1);
```

This section declares and array called list of cards. Then it uses the `newcard` method  (referenced as Card Creation Method in this report). to declare 4 cards. However 4 could quite easily be swapped for the length of a file and then a loop could be used to add cards from there as that is the only time a number is used.

##### Reason For Racism Values:

1. Roosevelt is the highest at 85 for supporting and sighing the executive that created Japanese internment
2. Trump is next because he says and tweets racist things
3. Obama is higher then Lincoln because he did less to stop racism then Abraham

#### Available Cards:

This Creates the list of available cards used to shuffle and deal cards from cards list

```c#
List<int> availablecards = new List<int>();
            int cardcount = 1;
            foreach (card toptrump in cardlist)
            {
                availablecards.Add(cardcount);
                cardcount++;
            }
            int listl = listlength(availablecards);
```

This section declares a list and sets the values of the list to the position of cards is the array card list + 1.

#### Scrambled Eggs

This method _scrambles_ all the cards and deals them out. In other words it shuffles and deals the top trump cards.

```c#
		   card[] player1deck = new card[listl / 2];
            card[] player2deck = new card[listl / 2];
            int cardsperplayer = 0;
            Boolean shuffled = false;
            while (shuffled == false)
            {
                Random newcard = new Random();
                int cardfor1 = availablecards[newcard.Next(0, listlength(availablecards))] - 1;
                player1deck[cardsperplayer] = cardlist[cardfor1];
                availablecards.Remove(cardfor1 + 1);
                int cardfor2 = availablecards[newcard.Next(0, listlength(availablecards))] - 1;
                player2deck[cardsperplayer] = cardlist[cardfor2];
                availablecards.Remove(cardfor2 + 1);
                cardsperplayer++;
                listl = listlength(availablecards);
                if (listl == 0) { shuffled = true; }
            }
```

This section starts with declaring variables. The first two are the player deck arrays (shudder at my poor choice) and the next two are the amount of cards each player currently has (used for the location in the array to put the dealt card) and a Boolean used to check if shuffling is done.  Then line by line in the while statement goes as: the first one is a random number which is actually a position in available cards. This is done by using `listlenghth - 1` (referenced as pointless method in this report) as the max random value. It then sets the current value of the place in the deck to the randomly selected card. Then it removes that position from available cards as it is no longer available. This is repeated for player 2. Then the loop checks if there is no available cards left. if there isn't it sets `shuffled` as true and breaks the while loop. Other wise it repeats. I mean happy with this section as there is no numbers so it would react to a changing card list size.

#### "_One ring to rule them all and in the darkness bind them_"

This is the game loop (ring) that pulls and ties (binds) all together.

```c#
 Boolean gameover = false;
            while (gameover == false)
            {
                card player1card = chooseplayercard(1, player1deck);
                card player2card = chooseplayercard(2, player2deck);
                int winner = playerturn(1, 2, player1card, player2card);
                arrayupdating(winner,player1deck,player2deck,player1card,player2card);
                winner = playerturn(2, 1, player2card, player1card);
                arrayupdating(winner, player1deck, player2deck, player1card, player2card);
                if (player1deck.Length == 0) { Console.WriteLine("Player 2 wins"); gameover = true; }
                if (player2deck.Length == 0) { Console.WriteLine("Player 1 wins"); gameover = true; }
            }
```

This loop is the last bit of code in the game. Line by line the first two lines call `chooseplayercard` (Pick a Card... in the report) to get player one's and two's card chosen. Then it sets the integer winner as equal to the winner of the round using the method `playerturn` (Take your turn in this report). Then it uses the method `arrayupdating` (Rude Goldberg method in this report)to update the arrays. It then repeats the last two steps but allowing player 2 to have there turn. Then it checks if either player has no cards left. If they don't it breaks the game loop and ends the game. If they do it loops round.

## Evaluation:

### Initial:

Overall this program came out to be a success fulfilling all the goals I had in mind. The use of arrays for player hands is probably the biggest and time consuming mistake I made.

### Further Comments:

Making some mistakes such as creating the pointless method has taught me new syntax like `list.count` where as as the rude Goldberg method had a lot of errors that where tedious to solve it gave me a chance to use problem decomposition to fix it as isolated the random number section to find the issue. All other goals such as easiness to append , random shuffling and conciseness have been met. Easiness to append has been talked about throughout the entire report. As for conciseness the entire thing is under 200 lines. I am happy with the result as i have a working program.
