using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;

namespace Top_Trumps
{
    class card
    {
        public string name { get; set; }
        public int rasicm { get; set; }
        public int assinationattmpts { get; set; }
    }

    class Program
    {

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
        static int listlength(List<int> list)
        {
            int i = 0;
            foreach (var value in list)
            {
                i++;
            }
            return i;
        }
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
            if (winner == playercard) { return player; } else{ return openent; };
        }
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
                else
                { i++; }
                
            }
            return tempL;
        }
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
            static void Main(string[] args)
        {
            card[] cardlist = new card[4];
            cardlist[0] = newcard("Trump", 76, 2);
            cardlist[1] = newcard("Obama", 8, 1);
            cardlist[2] = newcard("Lincoln", 1, 2);
            cardlist[3] = newcard("Roosevelt", 85, 1);
            List<int> availablecards = new List<int>();
            int cardcount = 1;
            foreach (card toptrump in cardlist)
            {
                availablecards.Add(cardcount);
                cardcount++;
            }
            int listl = listlength(availablecards);
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
            Boolean gameover = false;
            while (gameover == false)
            {
                card player1card = chooseplayercard(1, player1deck);
                card player2card = chooseplayercard(2, player2deck);
                int winner = playerturn(1, 2, player1card, player2card);
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
                if (player1deck.Length == 0) { Console.WriteLine("Player 2 wins"); gameover = true; }
                if (player2deck.Length == 0) { Console.WriteLine("Player 1 wins"); gameover = true; }
            }
        }    
    }
}
