/***************************************************************************
 *   New LokaiSkill System script by Lokai. This program is free software; you 
 *   can redistribute it and/or modify it under the terms of the GNU GPL. 
 ***************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Network;
using Server.Engines.XmlSpawner2;

namespace Server
{
    public class LokaiSkillMod : XmlAttachment
    {
        private Mobile m_From;
        private LokaiSkillName m_Skill;
        private bool m_Relative;
        private double m_Value;
        private bool m_ObeyCap;

        [Attachable]
        public LokaiSkillMod(LokaiSkillName skill, Mobile mob)
            : this(skill, true, false, 10.0, mob)
        {
        }

        [Attachable]
        public LokaiSkillMod(LokaiSkillName skill)
            : this(skill, true, false, 10.0, null)
        {
        }

        [Attachable]
        public LokaiSkillMod(LokaiSkillName skill, bool relative, bool obey, double value, Mobile mob)
        {
            m_Skill = skill;
            m_Relative = relative;
            m_ObeyCap = obey;
            m_Value = value;
            m_From = mob;
        }

        [Attachable]
        public LokaiSkillMod(ASerial serial) : base(serial) { }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)1); //version

            writer.Write((bool)m_ObeyCap);
            writer.Write((bool)m_Relative);
            writer.Write((int)m_Skill);
            writer.Write((double)m_Value);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
            switch (version) {
                case 1:
                    {
                        m_ObeyCap = reader.ReadBool();
                        m_Relative = reader.ReadBool();
                        m_Skill = (LokaiSkillName)reader.ReadInt();
                        m_Value = reader.ReadDouble();
                        goto case 0;
                    }
                case 0:break;
            }
        }

        public bool ObeyCap
        {
            get { return m_ObeyCap; }
            set
            {
                m_ObeyCap = value;

                if (m_From != null)
                {
                    LokaiSkill sk = LokaiSkillUtilities.XMLGetSkills(m_From)[m_Skill];

                    if (sk != null)
                        sk.Update();
                }
            }
        }

        public Mobile From
        {
            get
            {
                return m_From;
            }
            set
            {
                if (m_From != value)
                {
                    m_From = value;
                    XmlAttach.AttachTo(m_From, this);
                }
            }
        }

        public void Remove()
        {
            From = null;
        }

        public override void AddProperties(ObjectPropertyList list)
        {
            base.AddProperties(list);
            //list.Add(String.Format("{0}: +{1}", Skill.ToString(), Value.ToString("F1")));
            if (Owner is Mobile) return;
            list.Add(String.Concat(String.Format("{0}: ", Skill.ToString()), String.Format("<BASEFONT COLOR={0}>+{1}", "#0FFF00", Value.ToString("F1"))));
        }

        public override void OnRemoved(object parent)
        {
            try
            {
                if (parent is Mobile)
                {
                    Mobile from = parent as Mobile;
                    XmlAttach.FindAttachmentOnMobile(from, typeof(LokaiSkillMod), string.Format("{0}{1}{2}", Skill.ToString(), from.Name, Serial.Value)).Delete();
                    LokaiSkills skills = LokaiSkillUtilities.XMLGetSkills(from);
                    skills.OnLokaiSkillChange(skills[Skill]);
                }
            }
            catch { base.OnRemoved(parent); }
        }

        public override void OnEquip(Mobile from)
        {
            try
            { 
                LokaiSkillMod lsm = new LokaiSkillMod(Skill, Relative, ObeyCap, Value, from);
                lsm.Name = string.Format("{0}{1}{2}", Skill.ToString(), from.Name, Serial.Value);
                XmlAttach.AttachTo(from, lsm);
                LokaiSkills skills = LokaiSkillUtilities.XMLGetSkills(from);
                skills.OnLokaiSkillChange(skills[Skill]);
            }
            catch { base.OnEquip(from); }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public LokaiSkillName Skill
        {
            get
            {
                return m_Skill;
            }
            set
            {
                if (m_Skill != value)
                {
                    LokaiSkill oldUpdate = (m_From == null ? LokaiSkillUtilities.XMLGetSkills(m_From)[m_Skill] : null);

                    m_Skill = value;

                    if (m_From != null)
                    {
                        LokaiSkill sk = LokaiSkillUtilities.XMLGetSkills(m_From)[m_Skill];

                        if (sk != null)
                            sk.Update();
                    }

                    if (oldUpdate != null)
                        oldUpdate.Update();
                }
            }
        }

        public bool Relative
        {
            get
            {
                return m_Relative;
            }
            set
            {
                if (m_Relative != value)
                {
                    m_Relative = value;

                    if (m_From != null)
                    {
                        LokaiSkill sk = LokaiSkillUtilities.XMLGetSkills(m_From)[m_Skill];

                        if (sk != null)
                            sk.Update();
                    }
                }
            }
        }

        public bool Absolute
        {
            get
            {
                return !m_Relative;
            }
            set
            {
                if (m_Relative == value)
                {
                    m_Relative = !value;

                    if (m_From != null)
                    {
                        LokaiSkill sk = LokaiSkillUtilities.XMLGetSkills(m_From)[m_Skill];

                        if (sk != null)
                            sk.Update();
                    }
                }
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public double Value
        {
            get
            {
                return m_Value;
            }
            set
            {
                if (m_Value != value)
                {
                    m_Value = value;

                    if (m_From != null)
                    {
                        LokaiSkill sk = LokaiSkillUtilities.XMLGetSkills(m_From)[m_Skill];

                        if (sk != null)
                            sk.Update();
                    }
                }
            }
        }

        public virtual bool CheckCondition() { return true; }


        public virtual void UpdateSkillMods(Mobile from)
        {
            ValidateSkillMods(from);

            ArrayList a = XmlAttach.FindAttachments(from);

            foreach (XmlAttachment x in a)
            {
                if (x is LokaiSkillMod)
                {
                    LokaiSkillMod mod = x as LokaiSkillMod;

                    LokaiSkill sk = LokaiSkillUtilities.XMLGetSkills(from)[this.Skill];

                    if (sk != null)
                        sk.Update();
                }
            }
        }

        public virtual void ValidateSkillMods(Mobile from)
        {

            ArrayList a = XmlAttach.FindAttachments(from);

            foreach (XmlAttachment x in a)
            {
                if (x is LokaiSkillMod)
                {
                    if (((LokaiSkillMod)x).CheckCondition())
                        continue;
                    else
                        InternalRemoveSkillMod(from, x as LokaiSkillMod);
                }
            }
        }

        public virtual void AddSkillMod(Mobile from)
        {
            if (from == null)
                return;

            ValidateSkillMods(from);

            if (XmlAttach.FindAttachment(from, typeof(LokaiSkillMod)) != this /*m_SkillMods.Contains(mod)*/)
            {
                XmlAttach.AttachTo(from, this);

                LokaiSkill sk = LokaiSkillUtilities.XMLGetSkills(from)[this.Skill];

                if (sk != null)
                    sk.Update();
            }
        }

        public virtual void RemoveSkillMod(Mobile from)
        {
            if (from == null)
                return;

            ValidateSkillMods(from);

            InternalRemoveSkillMod(from);
        }

        private void InternalRemoveSkillMod(Mobile from)
        {
            if (XmlAttach.FindAttachment(from, typeof(LokaiSkillMod)) == this)
            {
                this.Delete();

                LokaiSkill sk = LokaiSkillUtilities.XMLGetSkills(from)[this.Skill];

                if (sk != null)
                    sk.Update();
            }
        }

        private void InternalRemoveSkillMod(Mobile from, LokaiSkillMod mod)
        {
            if (XmlAttach.FindAttachment(from, typeof(LokaiSkillMod)) == mod)
            {
                mod.Delete();

                LokaiSkill sk = LokaiSkillUtilities.XMLGetSkills(from)[mod.Skill];

                if (sk != null)
                    sk.Update();
            }
        }
    }
}
