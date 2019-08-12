using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Misc;
using Server.Mobiles;
using Server.Items;
using Server.Network;
using Server.Engines.XmlSpawner2;

namespace Server
{
    public enum SuccessRating
    {
        LokaiSkillNotEnabled,
        TooDifficult,
        CriticalFailure,
        HazzardousFailure,
        Failure,
        PartialSuccess,
        Success,
        CompleteSuccess,
        ExceptionalSuccess,
        TooEasy
    }

    public enum LokaiSkillLock : byte
    {
        Up = 0,
        Down = 1,
        Locked = 2
    }

    public class LokaiSkillUtilities
    {
        public static void Configure()
        {
            EventSink.WorldLoad += new WorldLoadEventHandler(Load);
            EventSink.WorldSave += new WorldSaveEventHandler(Save);
        }

        private static bool m_CommerceEnabled;
        private static bool m_RidingChecksEnabled;
        private static bool m_SailingChecksEnabled;
        private static bool m_LinguisticsEnabled;
        private static AccessLevel m_LinguisticsLevel;
        private static bool[] m_ShowLokaiSkillInGump;
        public static bool CommerceEnabled { get { return m_CommerceEnabled; } set { m_CommerceEnabled = value; } }
        public static bool RidingChecksEnabled { get { return m_RidingChecksEnabled; } set { m_RidingChecksEnabled = value; } }
        public static bool SailingChecksEnabled { get { return m_SailingChecksEnabled; } set { m_SailingChecksEnabled = value; } }
        public static bool LinguisticsEnabled
        {
            get { return m_LinguisticsEnabled; }
            set
            {
                if (value == false || LokaiSkillHandlers.Linguistics.WordsLoaded) m_LinguisticsEnabled = value;
                else Console.WriteLine("Linguistics database was not loaded, so unable to set value to true.");
            }
        }

        //This is the AccessLevel beyond which Linguistics will be used. (Ex: if AccessLevel.Player, then only Players will be challenged.)
        [CommandProperty(AccessLevel.Administrator)]
        public static AccessLevel LinguisticsLevel { get { return m_LinguisticsLevel; } set { m_LinguisticsLevel = value; } }
        public static bool ShowButchering { get { return m_ShowLokaiSkillInGump[0]; } set { m_ShowLokaiSkillInGump[0] = value; } }
        public static bool ShowSkinning { get { return m_ShowLokaiSkillInGump[1]; } set { m_ShowLokaiSkillInGump[1] = value; } }
        public static bool ShowAnimalRiding { get { return m_ShowLokaiSkillInGump[2]; } set { m_ShowLokaiSkillInGump[2] = value; } }
        public static bool ShowSailing { get { return m_ShowLokaiSkillInGump[3]; } set { m_ShowLokaiSkillInGump[3] = value; } }
        public static bool ShowDetectEvil { get { return m_ShowLokaiSkillInGump[4]; } set { m_ShowLokaiSkillInGump[4] = value; } }
        public static bool ShowCureDisease { get { return m_ShowLokaiSkillInGump[5]; } set { m_ShowLokaiSkillInGump[5] = value; } }
        public static bool ShowPickPocket { get { return m_ShowLokaiSkillInGump[6]; } set { m_ShowLokaiSkillInGump[6] = value; } }
        public static bool ShowPilfering { get { return m_ShowLokaiSkillInGump[7]; } set { m_ShowLokaiSkillInGump[7] = value; } }
        public static bool ShowFraming { get { return m_ShowLokaiSkillInGump[8]; } set { m_ShowLokaiSkillInGump[8] = value; } }
        public static bool ShowBrickLaying { get { return m_ShowLokaiSkillInGump[9]; } set { m_ShowLokaiSkillInGump[9] = value; } }
        public static bool ShowRoofing { get { return m_ShowLokaiSkillInGump[10]; } set { m_ShowLokaiSkillInGump[10] = value; } }
        public static bool ShowStoneMasonry { get { return m_ShowLokaiSkillInGump[11]; } set { m_ShowLokaiSkillInGump[11] = value; } }
        public static bool ShowVentriloquism { get { return m_ShowLokaiSkillInGump[12]; } set { m_ShowLokaiSkillInGump[12] = value; } }
        public static bool ShowHypnotism { get { return m_ShowLokaiSkillInGump[13]; } set { m_ShowLokaiSkillInGump[13] = value; } }
        public static bool ShowPreyTracking { get { return m_ShowLokaiSkillInGump[14]; } set { m_ShowLokaiSkillInGump[14] = value; } }
        public static bool ShowSpeakToAnimals { get { return m_ShowLokaiSkillInGump[15]; } set { m_ShowLokaiSkillInGump[15] = value; } }
        public static bool ShowWoodworking { get { return m_ShowLokaiSkillInGump[16]; } set { m_ShowLokaiSkillInGump[16] = value; } }
        public static bool ShowCooperage { get { return m_ShowLokaiSkillInGump[17]; } set { m_ShowLokaiSkillInGump[17] = value; } }
        public static bool ShowSpinning { get { return m_ShowLokaiSkillInGump[18]; } set { m_ShowLokaiSkillInGump[18] = value; } }
        public static bool ShowWeaving { get { return m_ShowLokaiSkillInGump[19]; } set { m_ShowLokaiSkillInGump[19] = value; } }
        public static bool ShowConstruction { get { return m_ShowLokaiSkillInGump[20]; } set { m_ShowLokaiSkillInGump[20] = value; } }
        public static bool ShowCommerce { get { return m_ShowLokaiSkillInGump[21]; } set { m_ShowLokaiSkillInGump[21] = value; } }
        public static bool ShowBrewing { get { return m_ShowLokaiSkillInGump[22]; } set { m_ShowLokaiSkillInGump[22] = value; } }
        public static bool ShowHerblore { get { return m_ShowLokaiSkillInGump[23]; } set { m_ShowLokaiSkillInGump[23] = value; } }
        public static bool ShowTreePicking { get { return m_ShowLokaiSkillInGump[24]; } set { m_ShowLokaiSkillInGump[24] = value; } }
        public static bool ShowTreeSapping { get { return m_ShowLokaiSkillInGump[25]; } set { m_ShowLokaiSkillInGump[25] = value; } }
        public static bool ShowTreeCarving { get { return m_ShowLokaiSkillInGump[26]; } set { m_ShowLokaiSkillInGump[26] = value; } }
        public static bool ShowTreeDigging { get { return m_ShowLokaiSkillInGump[27]; } set { m_ShowLokaiSkillInGump[27] = value; } }
        public static bool ShowTeaching { get { return m_ShowLokaiSkillInGump[28]; } set { m_ShowLokaiSkillInGump[28] = value; } }
        public static bool ShowLinguistics { get { return m_ShowLokaiSkillInGump[29]; } set { m_ShowLokaiSkillInGump[29] = value; } }

        public static void Load()
        {
            string filePath = Path.Combine("LokaiSaves/LokaiSkills", "LokaiSkills.bin");
            if (!File.Exists(filePath))
            {
                Console.WriteLine("LokaiSkills.bin did not exist so we are initializing the values.");
                m_LinguisticsLevel = AccessLevel.Player;
                m_RidingChecksEnabled = true;
                m_SailingChecksEnabled = true;
                m_LinguisticsEnabled = true;
                m_CommerceEnabled = true;
                m_ShowLokaiSkillInGump = new bool[] { true, true, true, true, true, true, true, true, true, 
                true, true, true, true, true, true, true, true, true, true, true, true, true, true, 
                true, true, true, true, true, true, true };
                return;
            }
            BinaryFileReader reader = null;
            FileStream fs = null;
            try
            {
                fs = new FileStream(filePath, (FileMode)3, (FileAccess)1, (FileShare)1);
                reader = new BinaryFileReader(new BinaryReader(fs));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return;
            }

            if (reader != null)
            {
                int check = 0;
                try
                {
                    int version = reader.ReadEncodedInt();
                    check++;
                    m_LinguisticsLevel = (AccessLevel)reader.ReadEncodedInt();
                    check++;
                    m_CommerceEnabled = reader.ReadBool();
                    check++;
                    m_RidingChecksEnabled = reader.ReadBool();
                    check++;
                    m_SailingChecksEnabled = reader.ReadBool();
                    check++;
                    m_LinguisticsEnabled = reader.ReadBool();
                    check++;

                    m_ShowLokaiSkillInGump = new bool[] { true, true, true, true, true, true, true, true, true, 
                        true, true, true, true, true, true, true, true, true, true, true, true, true, true, 
                        true, true, true, true, true, true, true };
                    ShowButchering = reader.ReadBool();
                    check++;
                    ShowSkinning = reader.ReadBool();
                    check++;
                    ShowAnimalRiding = reader.ReadBool();
                    check++;
                    ShowSailing = reader.ReadBool();
                    check++;
                    ShowDetectEvil = reader.ReadBool();
                    check++;
                    ShowCureDisease = reader.ReadBool();
                    check++;
                    ShowPickPocket = reader.ReadBool();
                    check++;
                    ShowPilfering = reader.ReadBool();
                    check++;
                    ShowFraming = reader.ReadBool();
                    check++;
                    ShowBrickLaying = reader.ReadBool();
                    check++;
                    ShowRoofing = reader.ReadBool();
                    check++;
                    ShowStoneMasonry = reader.ReadBool();
                    check++;
                    ShowVentriloquism = reader.ReadBool();
                    check++;
                    ShowHypnotism = reader.ReadBool();
                    check++;
                    ShowPreyTracking = reader.ReadBool();
                    check++;
                    ShowSpeakToAnimals = reader.ReadBool();
                    check++;
                    ShowWoodworking = reader.ReadBool();
                    check++;
                    ShowCooperage = reader.ReadBool();
                    check++;
                    ShowSpinning = reader.ReadBool();
                    check++;
                    ShowWeaving = reader.ReadBool();
                    check++;
                    ShowConstruction = reader.ReadBool();
                    check++;
                    ShowCommerce = reader.ReadBool();
                    check++;
                    ShowBrewing = reader.ReadBool();
                    check++;
                    ShowHerblore = reader.ReadBool();
                    check++;
                    ShowTreePicking = reader.ReadBool();
                    check++;
                    ShowTreeSapping = reader.ReadBool();
                    check++;
                    ShowTreeCarving = reader.ReadBool();
                    check++;
                    ShowTreeDigging = reader.ReadBool();
                    check++;
                    ShowTeaching = reader.ReadBool();
                    check++;
                    ShowLinguistics = reader.ReadBool();
                    check++;
                    reader.Close();
                }
                catch
                {
                    Console.WriteLine("Error reading .bin file at line {0}, so we are initializing the values again.", check);
                    m_LinguisticsLevel = AccessLevel.Player;
                    m_RidingChecksEnabled = true;
                    m_SailingChecksEnabled = true;
                    m_LinguisticsEnabled = true;
                    m_CommerceEnabled = true;
                    m_ShowLokaiSkillInGump = new bool[] { true, true, true, true, true, true, true, true, true, 
                        true, true, true, true, true, true, true, true, true, true, true, true, true, true, 
                        true, true, true, true, true, true, true };
                    Console.WriteLine(".bin File closed.");
                    reader.Close();
                }
            }
            else
            {
                Console.WriteLine("Reader was NULL, so we are initializing the values again.");
                m_LinguisticsLevel = AccessLevel.Player;
                m_RidingChecksEnabled = true;
                m_SailingChecksEnabled = true;
                m_LinguisticsEnabled = true;
                m_CommerceEnabled = true;
                m_ShowLokaiSkillInGump = new bool[] { true, true, true, true, true, true, true, true, true, 
                true, true, true, true, true, true, true, true, true, true, true, true, true, true, 
                true, true, true, true, true, true, true };
            }
        }

        public static void Save(WorldSaveEventArgs e)
        {

            if (!Directory.Exists("LokaiSaves/LokaiSkills"))
                Directory.CreateDirectory("LokaiSaves/LokaiSkills");
            string filePath = Path.Combine("LokaiSaves/LokaiSkills", "LokaiSkills.bin");
            BinaryFileWriter writer = null;

            try
            {
                if (File.Exists(filePath)) File.Delete(filePath);
                Console.WriteLine(".bin file successfully deleted.");
            }
            catch (Exception err)
            {
                Console.WriteLine("Unable to delete the BinaryFileWriter so exiting SAVE process.");
                Console.WriteLine(err.ToString());
                return;
            }
            try
            {
                writer = new BinaryFileWriter(filePath, true);
            }
            catch
            {
                Console.WriteLine("Unable to create new BinaryFileWriter so exiting SAVE process.");
                return;
            }
            writer.WriteEncodedInt((int)0); //version

            writer.WriteEncodedInt((int)m_LinguisticsLevel);
            writer.Write(m_CommerceEnabled);
            writer.Write(m_RidingChecksEnabled);
            writer.Write(m_SailingChecksEnabled);
            writer.Write(m_LinguisticsEnabled);

            writer.Write(ShowButchering);
            writer.Write(ShowSkinning);
            writer.Write(ShowAnimalRiding);
            writer.Write(ShowSailing);
            writer.Write(ShowDetectEvil);
            writer.Write(ShowCureDisease);
            writer.Write(ShowPickPocket);
            writer.Write(ShowPilfering);
            writer.Write(ShowFraming);
            writer.Write(ShowBrickLaying);
            writer.Write(ShowRoofing);
            writer.Write(ShowStoneMasonry);
            writer.Write(ShowVentriloquism);
            writer.Write(ShowHypnotism);
            writer.Write(ShowPreyTracking);
            writer.Write(ShowSpeakToAnimals);
            writer.Write(ShowWoodworking);
            writer.Write(ShowCooperage);
            writer.Write(ShowSpinning);
            writer.Write(ShowWeaving);
            writer.Write(ShowConstruction);
            writer.Write(ShowCommerce);
            writer.Write(ShowBrewing);
            writer.Write(ShowHerblore);
            writer.Write(ShowTreePicking);
            writer.Write(ShowTreeSapping);
            writer.Write(ShowTreeCarving);
            writer.Write(ShowTreeDigging);
            writer.Write(ShowTeaching);
            writer.Write(ShowLinguistics);
            writer.Close();
            Console.WriteLine("All LokaiSkill values successfully written to .bin file. File closed.");
        }

        public static bool ShowLokaiSkill(int num)
        {
            return m_ShowLokaiSkillInGump[num];
        }

        public static void ChangeShowLokaiSkill(int num)
        {
            m_ShowLokaiSkillInGump[num] = !m_ShowLokaiSkillInGump[num];
        }

        public static LokaiSkills XMLGetSkills(Mobile from)
        {
            LSA lsa;
            lsa = (LSA)XmlAttach.FindAttachment(from, typeof(LSA));
            if (lsa == null)
            {
                lsa = new LSA(from);
                XmlAttach.AttachTo(from, lsa);
            }
            return lsa.Skills;
        }

        public static SuccessRating CheckLokaiSkill(Mobile from, LokaiSkill lokaiSkill, double minLokaiSkill, double maxLokaiSkill)
        {
            double value = lokaiSkill.Value;

            if (value < minLokaiSkill)
                return SuccessRating.TooDifficult; // Too difficult
            else if (value >= maxLokaiSkill)
                return SuccessRating.TooEasy; // No challenge

            double chance = (value - minLokaiSkill) / (maxLokaiSkill - minLokaiSkill);

            if (LokaiSkillUtilities.XMLGetSkills(from).Cap == 0)
                return SuccessRating.LokaiSkillNotEnabled;

            SuccessRating rating = SuccessRating.PartialSuccess;

            double random = Utility.RandomDouble();
            bool success = (chance >= random);

            double gc = (double)(LokaiSkillUtilities.XMLGetSkills(from).Cap -
                LokaiSkillUtilities.XMLGetSkills(from).Total) / LokaiSkillUtilities.XMLGetSkills(from).Cap;
            gc += (lokaiSkill.Cap - lokaiSkill.Base) / lokaiSkill.Cap;
            gc /= 2;

            gc += (1.0 - chance) * (success ? 0.5 : (Core.AOS ? 0.0 : 0.2));
            gc /= 2;

            if (gc < 0.01)
                gc = 0.01;

            if (from is BaseCreature && ((BaseCreature)from).Controlled)
                gc *= 2;

            if (from.Alive && (gc >= Utility.RandomDouble() || lokaiSkill.Base < 10.0))
                Gain(from, lokaiSkill);

            if (chance - random <= -0.9)
                rating = SuccessRating.CriticalFailure;
            else if (chance - random <= -0.6)
                rating = SuccessRating.HazzardousFailure;
            else if (chance - random <= 0.0)
                rating = SuccessRating.Failure;
            else if (chance - random <= 0.15)
                rating = SuccessRating.PartialSuccess;
            else if (chance - random <= 0.45)
                rating = SuccessRating.Success;
            else if (chance - random <= 0.75)
                rating = SuccessRating.CompleteSuccess;
            else if (chance - random <= 0.9)
                rating = SuccessRating.ExceptionalSuccess;

            return rating;
        }

        public static void Gain(Mobile from, LokaiSkill lokaiSkill)
        {
            if (from.Region.IsPartOf(typeof(Regions.Jail)))
                return;

            if (lokaiSkill.Base < lokaiSkill.Cap && lokaiSkill.Lock == LokaiSkillLock.Up)
            {
                int oldLokaiSkill = lokaiSkill.BaseFixedPoint;
                int toGain = 1;

                if (lokaiSkill.Base <= 30.0)
                    toGain = Utility.Random(3) + 1;

                LokaiSkills lokaiSkills = LokaiSkillUtilities.XMLGetSkills(from);

                if (lokaiSkills.Total >= lokaiSkills.Cap)
                {
                    for (int i = 0; i < lokaiSkills.Length; ++i)
                    {
                        LokaiSkill toLower = lokaiSkills[i];

                        if (toLower != lokaiSkill && toLower.Lock == LokaiSkillLock.Down && toLower.BaseFixedPoint >= toGain)
                        {
                            toLower.BaseFixedPoint -= toGain;
                            break;
                        }
                    }
                }

                if ((lokaiSkills.Total + toGain) <= lokaiSkills.Cap)
                {
                    lokaiSkill.BaseFixedPoint += toGain;
                }
                if ((oldLokaiSkill <= 199 && lokaiSkill.BaseFixedPoint >= 200) ||
                    (oldLokaiSkill <= 299 && lokaiSkill.BaseFixedPoint >= 300) ||
                    (oldLokaiSkill <= 399 && lokaiSkill.BaseFixedPoint >= 400) ||
                    (oldLokaiSkill <= 499 && lokaiSkill.BaseFixedPoint >= 500) ||
                    (oldLokaiSkill <= 599 && lokaiSkill.BaseFixedPoint >= 600) ||
                    (oldLokaiSkill <= 699 && lokaiSkill.BaseFixedPoint >= 700) ||
                    (oldLokaiSkill <= 799 && lokaiSkill.BaseFixedPoint >= 800) ||
                    (oldLokaiSkill <= 899 && lokaiSkill.BaseFixedPoint >= 900))
                    lokaiSkills.Teaching.BaseFixedPoint += (7 - toGain);
            }

            if (lokaiSkill.Lock == LokaiSkillLock.Up)
            {
                LokaiSkillInfo info = lokaiSkill.Info;

                if (from.StrLock == StatLockType.Up && (info.StrScale / 33.3) > Utility.RandomDouble())
                    SkillCheck.GainStat(from, SkillCheck.Stat.Str);
                else if (from.DexLock == StatLockType.Up && (info.IntScale / 33.3) > Utility.RandomDouble())
                    SkillCheck.GainStat(from, SkillCheck.Stat.Dex);
                else if (from.IntLock == StatLockType.Up && (info.DexScale / 33.3) > Utility.RandomDouble())
                    SkillCheck.GainStat(from, SkillCheck.Stat.Int);
            }
        }
    }
}
