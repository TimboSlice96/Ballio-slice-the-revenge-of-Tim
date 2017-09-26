[System.Serializable]
public class Stats
{
    //these are retrievable data types that can be used across scripts
    //All pick ups are created in the stats script and controlled by the player manager script, other scripts such as doors will call on the player manager when using pick ups.

    //these are the pickups
    public int Red;
	public int Yellow;
	public int Green;
	public int End;

	//this keeps track of the level timer
	public float Timer;
	public bool TimeFreeze;
}