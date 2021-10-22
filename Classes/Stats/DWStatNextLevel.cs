namespace DWR_Tracker.Classes.Stats
{
    public class DWStatNextLevel : DWStat
    {
        public DWStat LevelStat;

        public DWStatNextLevel(string name, int offset) : base(name, offset)
        {
        }

        protected override int ReadValue(MemoryBlock memData)
        {
            // At the end of the game your level gets set to 255
            if (LevelStat.Value == 255) { return -1; }

            // TODO: make this less brittle
            return DWGlobals.LevelNexts[LevelStat.Value];
        }
    }
}
