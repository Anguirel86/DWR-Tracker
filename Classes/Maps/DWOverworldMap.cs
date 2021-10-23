using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DWR_Tracker.Classes.Maps
{
  public struct GridTile
  {
    public GridTile(int x, int y, int tileIndex, bool isKnown)
    {
      X = x;
      Y = y;
      TileIndex = tileIndex;
      IsKnown = isKnown;
    }

    public int X;
    public int Y;
    public int TileIndex;
    public bool IsKnown;
  }

  public class DWOverworldMap
  {
    private const int tilesOffset = 0x1D5D; 
    private const int rowPointersOffset = 0x2663; // Value upped by 0x10 to account for ROM header
    private const int gridSize = 120;
    private GridTile[,] grid = new GridTile[gridSize, gridSize];
    private int tileSize = 16;
    private bool IsDecoded = false;
    int[] keyTiles = { (int)TileName.Town, (int)TileName.Cave, (int)TileName.Castle };

    public DWOverworldMap()
    {
      for (int y = 0; y < gridSize; y++)
      {
        for (int x = 0; x < gridSize; x++)
        {
          grid[x, y] = new GridTile(x, y, (int)TileName.Water, false);
        }
      }
    }

    public void DecodeMap(byte[] romData)
    {
      int count, tileIndex;

      // pointers to each row of tiles are after tile tile table
      int[] rowPointers = new int[gridSize];
      for (int i = 0; i < gridSize; i++)
      {
          byte[] bytes = new byte[] { romData[rowPointersOffset + (i * 2)], romData[rowPointersOffset + (i * 2) + 1] };
          rowPointers[i] = BitConverter.ToUInt16(bytes, 0) + 0x10 - 0x8000; // +0x10 is for the ROM header 
      }


      // process each tile by row
      for (int y = 0; y < gridSize; y++)
      {
          int mapByte = romData[rowPointers[y]];
          if (mapByte == 0xFF) { break; }
          tileIndex = mapByte >> 4;
          count = mapByte & 0xF;

          for (int x = 0, byteCtr = 0; x < gridSize; x++)
          {
              grid[x, y] = new GridTile(x, y, tileIndex, false);
              if (count == 0)
              {
                  mapByte = romData[rowPointers[y] + ++byteCtr];
                  if (mapByte == 0xFF) { break; }
                  tileIndex = mapByte >> 4;
                  count = mapByte & 0xF;
              }
              else
              {
                  count--;
              }
          }
      }

      IsDecoded = true;
    }

    public Image GetImage(int herox, int heroy)
    {
      if (!IsDecoded) { return null; }

      List<GridTile> layer2Tiles = new List<GridTile>();
      Bitmap image = new Bitmap(gridSize * tileSize, gridSize * tileSize);
      Graphics g = Graphics.FromImage(image);

      // draw each individual tile
      for (int y = 0; y < gridSize; y++)
      {
          for (int x = 0; x < gridSize; x++)
          {
              GridTile tile = grid[x, y];
              int tileIndex = tile.IsKnown ? tile.TileIndex : (int)TileName.Unknown;
              Image tileImage = DWRTiles[tileIndex].Image;

              if (keyTiles.Contains(tileIndex))
              {
                  layer2Tiles.Add(tile);
              }
              else
              {
                  g.DrawImage(tileImage, new Point(x * tileSize, y * tileSize));
              }
          }
      }

      // draw towns, caves, and castles larger
      foreach(GridTile tile in layer2Tiles)
      {
          g.DrawImage(DWRTiles[tile.TileIndex].Image, 
              new Point((tile.X - 1) * tileSize, (tile.Y - 1) * tileSize));
      }

      // draw the hero
      g.DrawImage(DWRTiles[(int)TileName.Hero].Image,
              new Point((herox - 1) * tileSize, (heroy - 1) * tileSize));

      return image;
    }

    public void Discover(int x, int y)
    {
      for (int iy = -7; iy < 7; iy++)
      {
        for (int ix = -8; ix < 8; ix++)
        {
          int x2 = x + ix;
          int y2 = y + iy;
          if (x2 < 0 || x2 > 119 || y2 < 0 || y2 > 119) { continue; }
          grid[x2, y2].IsKnown = true;
        }
      }
    }

    public static DWTile[] DWRTiles = new DWTile[15]
    {
            new DWTile("Grass", "grass.png"),
            new DWTile("Desert", "desert.png"),
            new DWTile("Hill", "hill.png"),
            new DWTile("Mountain", "mountain.png"),
            new DWTile("Water", "water.png"),
            new DWTile("Block", "block.png"),
            new DWTile("Forest", "forest.png"),
            new DWTile("Swamp", "swamp.png"),
            new DWTile("Town", "town.png"),
            new DWTile("Cave", "cave.png"),
            new DWTile("Castle", "castle.png"),
            new DWTile("Bridge", "bridge.png"),
            new DWTile("Stairs Down", "empty.png"),
            new DWTile("Unknown", "unknown.png"),
            new DWTile("Hero", "hero.png")
    };
  }

  public enum TileName
  {
    Grass,
    Desert,
    Hill,
    Mountain,
    Water,
    Block,
    Forest,
    Swamp,
    Town,
    Cave,
    Castle,
    Bridge,
    StairsDown,
    Unknown,
    Hero
  }


}
