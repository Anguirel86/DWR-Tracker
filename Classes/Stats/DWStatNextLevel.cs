namespace DWR_Tracker.Classes.Stats
{
  public class DWStatNextLevel : DWStat
  {
    public DWStat LevelStat;

    // Initialize the experience values needed for level up.
    // Values are based on the fast level flag.
    private int[] expToLevel = new int[30]
    {
            0, 5, 17, 35, 82, 165,337, 600, 975, 1500, 2175, 3000, 4125, 5625, 7500,
            9750, 12000, 14250, 16500, 19500, 22500, 25500, 28500, 31500, 34500,
            37500, 40500, 43500, 46500, 49151
    };

    public DWStatNextLevel(string name, int offset) : base(name, offset)
    {
    }

    /// <summary>
    /// Set the amount of experience needed for each level.  
    /// </summary>
    /// <param name="expToLevel">Int array containing experience values for each level</param>
    public void SetExpToLevelValues(int[] expToLevel)
    {
      this.expToLevel = expToLevel;
    }

    protected override int ReadValue(MemoryBlock memData)
    {
      // At the end of the game your level gets set to 255
      if (LevelStat.Value == 255) { return -1; }

      return this.expToLevel[LevelStat.Value];
    }
  }
}
