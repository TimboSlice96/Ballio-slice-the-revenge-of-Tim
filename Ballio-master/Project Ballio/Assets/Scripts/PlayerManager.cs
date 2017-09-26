/*
 * This will be a singleton class, meaning only 1 should exist in the game
 * at a time. The purpose is to store any data we need across levels
 * or by different scripts e.g. Health, Attack Power, pick ups, etc.
 * All pick ups are created in the stats script and controlled by the player manager script, other scripts such as doors will call on the player manager when using pick ups.
 */


public class PlayerManager
{
    //this is the singleton instance
    private static PlayerManager self;

    //Gets the instance, if it doesnt exist it creates one
    public static PlayerManager Get()
    {
        if (self == null)
            self = new PlayerManager();//it's null so create a new one

        //give us the instance
        return self;
    }

    //player data goes here
    public Stats stats;

    //reset the values
    public void Reset()
    {
        //In a singleton stats are set here instead of on the Unity inspector menu
        stats = new Stats();
		stats.Red =       0;
		stats.Yellow =    0;
		stats.Green =     0;
		stats.End =       0;

		stats.Timer = 		0f;

		stats.TimeFreeze = false;
    }

    //constructor, just resets the variables to defaults
    public PlayerManager()
    {
        Reset();
    }
}
