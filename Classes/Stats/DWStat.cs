using System;

namespace DWR_Tracker.Classes
{
  public class DWStat
  {
    public string Name;
    public int Value = 0;
    public int Offset;
    private int bits;

    public event EventHandler ValueChanged;

    public DWStat(string name, int offset, int _bits = 8)
    {
      Name = name;
      Value = 0;
      Offset = offset;
      bits = _bits;
    }

    public virtual void Update(MemoryBlock memData, bool force = false)
    {
      int value = ReadValue(memData);
      if (value != Value || force)
      {
        Value = value;
        OnValueChanged();
      }
    }

    protected virtual void OnValueChanged()
    {
      EventHandler handler = ValueChanged;
      handler?.Invoke(this, new EventArgs());
    }

    protected virtual int ReadValue(MemoryBlock memData)
    {
      switch (bits)
      {
        case 8:
          return memData.ReadU8(Offset);
        case 16:
          byte[] data = new byte[] { memData.ReadU8(Offset), memData.ReadU8(Offset + 1) };
          return BitConverter.ToUInt16(data, 0);
        default:
          return -1;
      }
    }
  }
}
