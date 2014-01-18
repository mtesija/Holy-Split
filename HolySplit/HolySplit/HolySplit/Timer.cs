using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HolySplit
{
    //The way this works is you give it a time span in the constructor
    //In the update function of your code you call CheckTimer() to see if the amount of time has passed
    //If it has then the timer will reset and use the same time span for another round
    //You can change the time span by calling resetTimer()
    class Timer
    {
        DateTime startTime;
        float delta;

        //Create a new timer and give it an amount of time to check
        public Timer(float delta)
        {
            startTime = DateTime.Now;
            this.delta = delta;
        }

        //Checks if the timer has hit or passed the specified time
        //If it has then it will reset the timer to DateTime.Now
        public bool CheckTimer()
        {
            TimeSpan timePassed = DateTime.Now - startTime;
            if (timePassed.TotalSeconds >= delta)
            {
                startTime = DateTime.Now;
                return true;
            }
            return false;
        }

        //Only use this when you want to change the delta
        public void resetTimer(float delta)
        {
            startTime = DateTime.Now;
            this.delta = delta;
        }
    }
}
