using System;

namespace HolySplit
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (HolySplitGame game = new HolySplitGame())
            {
                game.Run();
            }
        }
    }
#endif
}

