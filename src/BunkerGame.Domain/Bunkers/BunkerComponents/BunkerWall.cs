using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace BunkerGame.Domain.Bunkers.BunkerComponents
{
    public class BunkerWall : BunkerComponentEntity
    {
        public BunkerWall(double value, string description,BunkerState bunkerState = BunkerState.Unbroken) : base(value, description)
        {
            BunkerState = bunkerState;
        }
        public BunkerState BunkerState { get; private set; }
        public void UpdateBukerState(BunkerState bunkerState)
        {
            BunkerState = bunkerState;
        }
        public override string ToString()
        {
            return Description;
        }
    }
    public enum BunkerState
    {
        Broken,
        Locked,
        Unbroken
    }
}
