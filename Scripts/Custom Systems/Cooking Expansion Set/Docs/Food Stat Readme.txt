
 -Modified the foods to have properties, each food can have of
 1 skill boost, int/dex/str, 1 or 2 or 3 stat boosts
 -each boost will last as long as the foods fillfactor, which is set for 15 minutes, so if the food has
 a fillfactor of 4, the boost lasts 1 hour and so does the food till your at that hunger level again.
 -The time can also be specified manually in the scripts per item but if nothing is specified it is
 same as fillfactor
 -each of the boosts are easily adjusted in the food scripts
 Statboost time & skill boost time can be adjusted globaly in food.cs to be less than fillfactor, note:

 If it is set longer than Fillfactor it does Not stack but just doesn't add the boost if you eat again 
(checks if there's already a boost applied)so the first Boost may run out before you can eat again because your full.

[Constructable]
public DriedPeachSlice( int amount ) : base( amount, 0x172A )
{
Name = "Dried Peach Slice";
Stackable = true;
Hue = 435;
Weight = 0.2;
FillFactor = 1;//1 fillfactor = 1 minutes of time

IntBoost = 2;
DexBoost = 1;
//StrBoost = 2;
//StatBoostTime = 500;//can set it's time in SECONDS--default is fillfactor
Skill = "Lumberjacking";
SkillBoost = 2;
//SkillBoostTime = 10;//can set this time in MINUTES independantly from StatBoostTime --default is fillfactor
//XmlSpawner treats XmlSkill differently than XmlDex/str/int time wise.
		}