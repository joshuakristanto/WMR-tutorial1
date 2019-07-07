using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLogic
{
    public class ItemEventArgs: EventArgs
    {
        public int Id = int.MinValue;
    }
    public class MatchEventArgs : EventArgs
    {
        public int Id = int.MinValue;
        public int Score;

    }

    public class NoMatchEventArgs: EventArgs
    {
        public bool IsStrike;

    }
    public class ShellGameLogic
    {
   
        public Item[] Items;

        private LMRRandom rand;

        private readonly int numberOfItems;

        private int itemLocation;

        private int totalStrikes;

        private int missedCount;

        public int Strikes = 0;

        public event EventHandler GameOver;

        public event EventHandler <MatchEventArgs> MatchMade;

        public event EventHandler <NoMatchEventArgs> MatchNotMade;

        public event EventHandler StartTurn;

        public event EventHandler ItemReset;

        public event EventHandler ResetComplete;

        public event EventHandler<ItemEventArgs> CheckingItem;

        public event EventHandler <ItemEventArgs> SelectedItem;

        public ShellGameLogic (int numberOfItems, int totalStrikes) : this(new NetRandom(), numberOfItems, totalStrikes)
        {

        }
        
        public ShellGameLogic(LMRRandom rand, int numberOfItems, int totalStrikes)
        {
            this.rand = rand;
            this.numberOfItems = numberOfItems;
            this.totalStrikes = totalStrikes;
            Items = new Item [numberOfItems];

            CreateItems();
            GenerateRandomNumber();
            ResetItem();
        }

        private void GenerateRandomNumber()
        {
            itemLocation = rand.Next(0, numberOfItems);
        }

        private void CreateItems()
        {
           for (int i = 0; i <numberOfItems; i++)
            {
                Items[i] = new Item();
                Items[i].Id = i;
                Items[i].AlreadyChecked = false;
            }
        }

        public bool CheckForItem(int itemId)
        {
            // did we already checked this item for this turn?
            CheckingItem?.Invoke(this, new ItemEventArgs() { Id = itemId });
            //bool alreadyChecked = Items[itemId].AlreadyChecked;
            if (Items[itemId].Id == itemLocation)
            {
                SelectedItem?.Invoke(this, new ItemEventArgs() { Id = itemId });
                //1st = 3
                //2nd = 2
                //3rd = 0
                int score = (numberOfItems - missedCount);
                if(score == 1)
                {
                    Strikes++;
                    MatchNotMade?.Invoke(this, new NoMatchEventArgs() { IsStrike = true });
                    //Raise Event No Match

                }
                else
                {
                    // Raise Match Mode Event
                    MatchMade?.Invoke(this, new MatchEventArgs() { Id = Items[itemId].Id, Score = score });

                }
                CloseItems();

                StartTurn?.Invoke(this, EventArgs.Empty);

                return true;
            }
            if (!Items[itemId].AlreadyChecked)
            {
                //Raose Event Match Not Made
                MatchNotMade?.Invoke(this, new NoMatchEventArgs() { IsStrike = false });
                missedCount++;
                Items[itemId].AlreadyChecked = true;
            }
            
             return false;
            
        }
        private void CloseItems()
        {
            for (int i = 0; i < numberOfItems; i++)
            {
               
          
                Items[i].AlreadyChecked = false;
            }
        }
        public void ResetItem()

        {
            // Raise Event ItemReset
            ItemReset?.Invoke(this, EventArgs.Empty);
            if(Strikes >= totalStrikes)
            {
                GameOver?.Invoke(this,EventArgs.Empty);
                Strikes = 0;
            }
            missedCount = 0;

            GenerateRandomNumber();

            //RaiseComplete Revant
            ResetComplete?.Invoke(this, EventArgs.Empty);
        }
    }
}
