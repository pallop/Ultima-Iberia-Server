using System;
using Server.Engines.Craft;

namespace Server.Items
{
	[FlipableAttribute( 0x1028, 0x1029 )]
    public class RunicDovetailSaw : BaseRunicTool
    {
		public override CraftSystem CraftSystem{ get{ return DefCarpentry.CraftSystem; } }
		[Constructable]
        public RunicDovetailSaw(CraftResource resource)
            : base(resource, 0x1028)
        {
            this.Weight = 2.0;
            this.Hue = CraftResources.GetHue(resource);
        }

        [Constructable]
        public RunicDovetailSaw(CraftResource resource, int uses)
            : base(resource, uses, 0x1028)
        {
            this.Weight = 2.0;
            this.Hue = CraftResources.GetHue(resource);
        }

        public RunicDovetailSaw(Serial serial)
            : base(serial)
        {
        }

       
        public override int LabelNumber
        {
            get
            {
                int index = CraftResources.GetIndex(this.Resource);

                if (index >= 1 && index <= 6)
                    return 1072633 + index;
					
                return 1024137; // dovetail saw
            }
        }
		public override void AddNameProperties( ObjectPropertyList list )
		{
			base.AddNameProperties( list );
			int index = CraftResources.GetIndex( Resource );
			if ( index >= 1 && index <= 6 )
				return;
			if ( !CraftResources.IsStandard( Resource ) )
			{
				int num = CraftResources.GetLocalizationNumber( Resource );
				if ( num > 0 )
					list.Add( num );
				else
					list.Add( CraftResources.GetName( Resource ) );
			}
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
		//daat99 OWTLR start - runic storage
		public override System.Type GetCraftableType()
		{
			switch (Resource)
			{
				case CraftResource.OakWood:
					return typeof(OakRunicFletcherTools);
				case CraftResource.AshWood:
					return typeof(AshRunicFletcherTools);
				case CraftResource.YewWood:
					return typeof(YewRunicFletcherTools);
				case CraftResource.Heartwood:
					return typeof(HeartwoodRunicFletcherTools);
				case CraftResource.Bloodwood:
					return typeof(BloodwoodRunicFletcherTools);
				case CraftResource.Frostwood:
					return typeof(FrostwoodRunicFletcherTools);
				case CraftResource.Ebony:
					return typeof(EbonyRunicFletcherTools);
				case CraftResource.Bamboo:
					return typeof(BambooRunicFletcherTools);
				case CraftResource.PurpleHeart:
					return typeof(PurpleHeartRunicFletcherTools);
				case CraftResource.Redwood:
					return typeof(RedwoodRunicFletcherTools);
				case CraftResource.Petrified:
					return typeof(PetrifiedRunicFletcherTools);
				default:
					return null;
    }
		}
		//daat99 OWLTR end - runic storage
	}
}