using System;

namespace DWR_Tracker.Classes
{
    public abstract class DWItem
    {
        public string Name;
        public int Value;
        public int Count;
        public string ImagePath;
        
        public bool IsBattleGear;
        public bool IsRequiredItem;
        public bool IsFirstHalfQuestItem = false;
        public bool allowsMultiple;
        public bool forceOwnRead = false;
        public bool ShowCount = false;
        
        public (string ImagePath, string Name, int ExtraValue)[] ItemInfo;

        public event EventHandler ValueChanged;

        public abstract int ReadValue(MemoryBlock memData);

        public void Update(MemoryBlock memData, bool force = false) {
            Update(ReadValue(memData), 1, force);
        }

        public virtual void Update(int value, bool force = false)
        {
            Update(value, 1, force);
        }

        public void Update(int value, int count, bool force = false)
        {
            if (value != Value || count != Count || force)
            {
                Value = value;
                Count = count;
                Name = ItemInfo[value].Name;
                ImagePath = ItemInfo[value].ImagePath;
                OnValueChanged();
            }
        }

        protected virtual void OnValueChanged()
        {
            EventHandler handler = ValueChanged;
            handler?.Invoke(this, new EventArgs());
        }
    }
}
