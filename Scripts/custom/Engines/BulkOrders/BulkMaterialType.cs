using System;
using Server;
using Server.Items;

namespace Server.Engines.BulkOrders
{
    public enum BulkMaterialType
    {
		//daat99 OWLTR start - custom resources
        None,
        DullCopper,
        ShadowIron,
        Copper,
        Bronze,
        Gold,
        Agapite,
        Verite,
        Valorite,
		Blaze,
		Ice,
		Toxic,
		Electrum,
		Platinum,
        Spined,
        Horned,
		Barbed,
		Polar,
		Synthetic,
		BlazeL,
		Daemonic,
		Shadow,
		Frost,
		Ethereal,
		Heartwood,
		Bloodwood,
		Frostwood,
		OakWood,
		AshWood,
		YewWood,
		Ebony,
		Bamboo,
		PurpleHeart,
		Redwood,
		Petrified
		//daat99 OWLTR end - custom resources
    }

    public enum BulkGenericType
    {
        Iron,
        Cloth,
        Leather,
		//daat99 OWLTR start - wood bods
		RegularWood
		//daat99 OWLTR end - wood bods
    }

    public class BGTClassifier
    {
        public static BulkGenericType Classify(BODType deedType, Type itemType)
        {
            if (deedType == BODType.Tailor)
            {
                if (itemType == null || itemType.IsSubclassOf(typeof(BaseArmor)) || itemType.IsSubclassOf(typeof(BaseShoes)))
                    return BulkGenericType.Leather;

                return BulkGenericType.Cloth;
            }
			//daat99 OWLTR start - wood bods
			else if ( deedType == BODType.Fletcher )
				return BulkGenericType.RegularWood;
			//daat99 OWLTR end - wood bods

            return BulkGenericType.Iron;
        }
    }
}