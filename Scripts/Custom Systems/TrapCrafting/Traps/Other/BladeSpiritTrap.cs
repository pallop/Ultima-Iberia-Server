//
// ** Basic Trap Framework (BTF)
// ** Author: Lichbane
//
using System;
using Server.Mobiles;

namespace Server.Items
{
    public class BladeSpiritTrap : BaseTinkerTrap
    {
        private Boolean m_TrapArmed = false;
        private DateTime m_TimeTrapArmed;

        private static string m_ArmedName = "an armed blade spirit trap";
        private static string m_UnarmedName = "an unarmed blade spirit trap";
        private static double m_ExpiresIn = 120.0;
        private static int m_ArmingSkill = 25;
        private static int m_DisarmingSkill = 50;
        private static int m_KarmaLoss = 120;
        private static bool m_AllowedInTown = false;

        [Constructable]
        public BladeSpiritTrap()
            : base(m_ArmedName, m_UnarmedName, m_ExpiresIn, m_ArmingSkill, m_DisarmingSkill, m_KarmaLoss, m_AllowedInTown)
        {
        }

        public override void TrapEffect(Mobile from)
        {
            from.PlaySound(0x4A);  // click sound

            Point3D loc = this.Location;
            BaseCreature.Summon(new BladeSpirits(), false, from, new Point3D(loc), 0x212, (TimeSpan.FromSeconds(60)));

            bool m_TrapsLimit = Trapcrafting.Config.TrapsLimit;
            if ((m_TrapsLimit) && (((PlayerMobile)this.Owner).TrapsActive > 0))
                ((PlayerMobile)this.Owner).TrapsActive -= 1;

            this.Delete();
        }

        public BladeSpiritTrap(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}