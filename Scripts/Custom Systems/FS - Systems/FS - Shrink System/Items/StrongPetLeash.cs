using System;

namespace Server.Items
{
    public class StrongPetLeash : BaseShrinker
    {
        [Constructable]
        public StrongPetLeash() : this(50)
        {
        	Name = "a strong pet leash";
        }

        [Constructable]
        public StrongPetLeash(int amount) : base(0x1374, amount)
        {
        	Name = "a strong pet leash";
        }

        public StrongPetLeash(Serial serial) : base(serial)
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