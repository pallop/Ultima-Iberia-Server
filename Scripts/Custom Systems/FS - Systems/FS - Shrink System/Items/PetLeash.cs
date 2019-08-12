using System;

namespace Server.Items
{
    public class PetLeash : BaseShrinker
    {
        [Constructable]
        public PetLeash() : this(10)
        {
        	Name = "a pet leash";
        }

        [Constructable]
        public PetLeash(int amount) : base(0x1374, amount)
        {
        	Name = "a pet leash";
        }

        public PetLeash(Serial serial) : base(serial)
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