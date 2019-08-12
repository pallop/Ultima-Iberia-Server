/***************************************************************************
 *   New LokaiSkill System script by Lokai. This program is free software; you 
 *   can redistribute it and/or modify it under the terms of the GNU GPL. 
 ***************************************************************************/
using System;
using Server;
using Server.Items;
using Server.Network;
using Server.Multis;
using Server.Mobiles;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace Server.LokaiSkillHandlers
{
    public class Linguistics
    {
        private static bool m_WordsLoaded = false;
        public static bool WordsLoaded { get { return m_WordsLoaded; } }

        public static void Initialize()
        {
            EventSink.Speech += new SpeechEventHandler(Linguistics_Speech);
            m_WordsLoaded = LoadWords();
        }

        private static void Linguistics_Speech(SpeechEventArgs e)
        {
            if (LokaiSkillUtilities.LinguisticsEnabled && WordsLoaded)
            {
                Mobile speaker = e.Mobile;

                // If the speaker has a greater AccessLevel than the linguistics cut-off, then we want everyone to hear them properly.
                if (speaker.AccessLevel > LokaiSkillUtilities.LinguisticsLevel) return;

                List<Mobile> hearers = new List<Mobile>();

                IPooledEnumerable eable = speaker.Map.GetObjectsInRange(speaker.Location, 12);

                foreach (object o in eable)
                {
                    if (o is Mobile)
                    {
                        Mobile hearer = (Mobile)o;

                        if (hearer.CanSee(speaker) && (Mobile.NoSpeechLOS || hearer.InLOS(speaker)))
                        {
                            if (hearer.HandlesOnSpeech(speaker))
                            {
                                e.Handled = false;
                                hearer.OnSpeech(e);
                                continue;
                            }
                            if (hearer.NetState != null)
                                hearers.Add(hearer);
                        }
                    }
                }
                eable.Free();

                foreach (Mobile hearer in hearers)
                {
                    // We exclude those from the same race or those with AccessLevels greater than the linguistics cut-off value.
                    if (speaker.Race == hearer.Race || hearer.AccessLevel > LokaiSkillUtilities.LinguisticsLevel)
                    {
                        speaker.SayTo(hearer, e.Speech);
                    }
                    else
                    {
                        speaker.SayTo(hearer, Translate(speaker, hearer, e.Speech));
                    }
                }
                e.Blocked = true;
            }
        }

        private static string Translate(Mobile speaker, Mobile hearer, string speech)
        {
            LokaiSkill lokaiSkill = LokaiSkillUtilities.XMLGetSkills(hearer).Linguistics;

            SuccessRating rating = LokaiSkillUtilities.CheckLokaiSkill(hearer, lokaiSkill, 10, 100);
            if (rating >= SuccessRating.Success) return speech;

            int percent = 0;
            switch (rating)
            {
                case SuccessRating.PartialSuccess: percent = 50; break;
                case SuccessRating.Failure: percent = 20; break;
                case SuccessRating.HazzardousFailure: percent = 10; break;
                case SuccessRating.CriticalFailure: percent = 5; break;
            }

            StringBuilder phrase = new StringBuilder("");

            string[] words = speech.Split(' ');
            if (words.Length > 0)
            {
                if (percent < Utility.Random(100))
                {
                    if (m_Words.Contains(words[0]))
                    {
                        phrase.Append(m_ForeignWords[m_Words.IndexOf(words[0])]);
                    }
                    else
                        phrase.Append(m_ForeignWords[Utility.Random(m_ForeignWords.Length)]); // temporary
                }
                else
                    phrase.Append(words[0]);
                if (words.Length > 1)
                {
                    for (int x = 1; x < words.Length; x++)
                    {
                        phrase.Append(" ");
                        if (percent < Utility.Random(100))
                        {
                            if (m_Words.Contains(words[x]))
                            {
                                phrase.Append(m_ForeignWords[m_Words.IndexOf(words[x])]);
                            }
                            else
                                phrase.Append(m_ForeignWords[Utility.Random(m_ForeignWords.Length)]); // temporary
                        }
                        else
                            phrase.Append(words[x]);

                        phrase.Append(" ");
                    }
                }
            }

            return phrase.ToString();
        }

        private static List<string> m_Words = new List<string>();
        private static string[] m_ForeignWords;

        private static bool LoadWords()
        {
            string fname = "Data/words.txt";
            string[] words;
            List<string> foreign = new List<string>();
            bool success = false;
            StreamReader sr;

            if (File.Exists(fname))
            {
                try
                {
                    sr = new StreamReader(fname, Encoding.Default, false);
                    while (!sr.EndOfStream)
                    {
                        words = sr.ReadLine().Split('|');
                        m_Words.Add(words[0]);
                        foreign.Add(words[1]);
                    }
                    sr.Close();
                    m_ForeignWords = foreign.ToArray();
                    Console.WriteLine("Linguistics translation words successfully loaded.");
                    success = true;
                }
                catch (Exception e) { Console.WriteLine(e.ToString()); }
            }
            else
            {
                Console.WriteLine("Linguistics file does not exist: Data/words.txt");
            }

            return success;
        }
    }
}
