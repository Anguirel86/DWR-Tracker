using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace DWR_Tracker.Classes
{
  public class DWEnemy
  {
    public string Name;
    public int[] HP;
    public int Strength;
    public int Agility;
    public int XP;
    public int Gold;
    public float HurtResist;
    public float SleepResist;
    public float StopspellResist;
    public float Evasion;
    public int RunGroup;
    public float StopspellCap;
    public string Skill1;
    public int Skill1Chance;
    public string Skill2;
    public int Skill2Chance;

    private float runGroupFactor;
    private const string baseImagePath = "DWR_Tracker.Images.Enemies.";
    private int[] hurtDamage = new int[2] { 9, 16 };
    private int[] hurtmoreDamage = new int[2] { 58, 65 };
    private string[] skill2 = new string[4] { "HURT", "HURTMORE", "FIRE", "DL2 FIRE" };
    private string[] skill1 = new string[4] { "SLEEP", "STOPSPELL", "HEAL", "HEALMORE" };

    public DWEnemy(string name, int[] hp, int strength, int agility, int xp,
        int gold, float hurtResist, float sleepResist, float evasion, int runGroup,
        float stopspellCap)
    {
      Name = name;
      HP = hp;
      Strength = strength;
      Agility = agility;
      XP = xp;
      Gold = gold;
      HurtResist = hurtResist;
      SleepResist = sleepResist;
      Evasion = evasion;
      RunGroup = runGroup;
      StopspellCap = stopspellCap;
      runGroupFactor = (new float[4] { 0.25f, 0.375f, 0.5f, 1.0f })[runGroup - 1];
    }

    /// <summary>
    /// Update the stat values for this monster.
    /// </summary>
    /// <param name="hp">Intger array containing min and max monster HP values</param>
    /// <param name="strength">Monster strength</param>
    /// <param name="agility">Monster agility</param>
    public void updateStats(int[] hp, int strength, int agility)
    {
      this.HP = hp;
      this.Strength = strength;
      this.Agility = agility;
    }

    /// <summary>
    /// Update the reward values for this monster.
    /// </summary>
    /// <param name="xp">Experience awarded for killing this monster</param>
    /// <param name="gold">Gold awarded for killing this monster</param>
    public void updateRewards(int xp, int gold)
    {
      this.XP = xp;
      this.Gold = gold;
    }

    /// <summary>
    /// Update this monster's defences and resistances. 
    /// </summary>
    /// <param name="hurtResist"></param>
    /// <param name="sleepResist"></param>
    /// <param name="evasion"></param>
    /// <param name="runGroup"></param>
    /// <param name="stopspellCap"></param>
    public void updateDefences(float hurtResist, float sleepResist, float evasion, int runGroup, float stopspellCap)
    {
      this.HurtResist = hurtResist;
      this.SleepResist = sleepResist;
      this.Evasion = evasion;
      this.RunGroup = runGroup;
      this.StopspellCap = stopspellCap;
    }

    public string GetSkill1(int index)
    {
      return skill1[index];
    }

    public string GetSkill2(int index)
    {
      return skill2[index];
    }

    public Image GetImage()
    {
      int picSize = 192;
      string ImagePath = baseImagePath + Name.ToLower().Replace(" ", "_") + ".png";
      Assembly myAssembly = Assembly.GetExecutingAssembly();
      Stream myStream = myAssembly.GetManifestResourceStream(ImagePath);
      Bitmap src = new Bitmap(Image.FromStream(myStream), new Size(64, 64));
      Bitmap dst = new Bitmap(picSize, picSize);
      Graphics g = Graphics.FromImage(dst);
      g.InterpolationMode = InterpolationMode.NearestNeighbor;
      g.DrawImage(
          src,
          new Rectangle(0, 0, picSize, picSize)
      );
      return dst;
    }

    private int getSkillChance(int chanceValue)
    {
      int chance = 75;
      if (chanceValue == 0)
      {
        chance = 0;
      }
      else if (chanceValue == 1)
      {
        chance = 25;
      }
      else if (chanceValue == 2)
      {
        chance = 50;
      }
      return chance;
    }

    public string[,] GetBattleInfo(DWHero hero)
    {
      int[] damageDealt = DamageDealtRange(hero);
      int[] damageTaken = DamageTakenRange(hero);

      return new string[20, 2]
      {
        // info
        { Name, "" },
        { "HP", HP[0] == HP[1] ? HP[0].ToString() : HP[0] + "-" + HP[1] },
        { "ST", Strength.ToString() },
        { "AG", Agility.ToString() },
        { "E", XP.ToString() },
        { "G", Gold.ToString() },

        // attack
        { "MELEE:", "" },
        { "enemy dmg", damageDealt[0] + "-" + damageDealt[1] },
        { "hero dmg", damageTaken[0] + "-" + damageTaken[1] },
        { "", "" },

        // skills
        { "SKILLS:", "" },
        { Skill1, getSkillChance(Skill1Chance).ToString() + "%" },
        { Skill2, getSkillChance(Skill2Chance).ToString() + "%" },
        { "", "" },

        // defense
        { "CHANCE:", "" },
        { "HURT", Math.Floor((1f - HurtResist) * 100).ToString() + "%" },
        { "SLEEP", Math.Floor((1f - SleepResist) * 100).ToString() + "%" },
        { "STOPSPELL", Math.Floor((1f - StopspellResist) * 100).ToString() + "%" },
        { "dodge", Math.Floor(Evasion * 100).ToString() + "%" },
        { "run", Math.Floor((1f - ChanceToBlockHeroRun(hero)) * 100).ToString() + "%" },

      };
    }

    // TODO: I'm dumb when it comes to probability calculations, so I'm doing
    // this the brute force way. Gotta be a cleaner method, I just don't know it.
    public float ChanceToBlockHeroRun(DWHero hero, bool isBackAttackCheck = false)
    {
      float runFactor = isBackAttackCheck ? 0.25f : runGroupFactor;
      int[] heroArray = Enumerable
          .Range(0, 255)
          .Select(x => x * hero.Agility.Value)
          .ToArray();
      int[] enemyArray = Enumerable
          .Range(0, 255)
          .Select(x => (int)Math.Floor(x * Agility * runFactor))
          .ToArray();

      int enemyWin = 0;
      for (int i = 0; i < heroArray.Length; i++)
      {
        for (int j = 0; j < enemyArray.Length; j++)
        {
          if (heroArray[i] < enemyArray[j])
          {
            enemyWin++;
          }
        }
      }
      return enemyWin / (float)(heroArray.Length * enemyArray.Length);
    }

    public bool Run25DueToHeroStrength(DWHero hero)
    {
      return Strength * 2 <= hero.Strength.Value;
    }

    public float ChanceToBackAttack(DWHero hero)
    {
      return ChanceToBlockHeroRun(hero, true);
    }

    public float HurtShot()
    {
      int dead = 0;
      IEnumerable<int> hurtRange = Enumerable.Range(hurtDamage[0], hurtDamage[1]);
      for (int i = 0; i < HP.Length; i++)
      {
        dead += hurtRange.Where(x => x >= HP[i]).ToArray().Length;
      }
      return dead / (float)(hurtRange.ToArray().Length * HP.Length);
    }

    public float HurtmoreShot()
    {
      int dead = 0;
      IEnumerable<int> hurtmoreRange = Enumerable.Range(hurtmoreDamage[0], hurtmoreDamage[1]);
      for (int i = 0; i < HP.Length; i++)
      {
        dead += hurtmoreRange.Where(x => x >= HP[i]).ToArray().Length;
      }
      return dead / (float)(hurtmoreRange.ToArray().Length * HP.Length);
    }

    public int[] DamageDealtRange(DWHero hero)
    {
      if (IsDefenseBroken(hero))
      {
        return new int[2]
        {
          0,
          (Strength + 4) / 6
        };
      }
      else
      {
        return new int[2]
        {
          (Strength - (hero.DefensePower.Value / 2)) / 4,
          (Strength - (hero.DefensePower.Value / 2)) / 2
        };
      }
    }

    public int[] DamageTakenRange(DWHero hero)
    {
      return new int[2]
      {
        Math.Max((hero.AttackPower.Value - (Agility / 2)) / 4, 0),
        Math.Max((hero.AttackPower.Value - (Agility / 2)) / 2, 1)
      };
    }

    public bool IsDefenseBroken(DWHero hero)
    {
      return hero.DefensePower.Value >= Strength;
    }

    public float MaxHPToXPRatio()
    {
      return HP[1] / (float)XP;
    }
  }
}
