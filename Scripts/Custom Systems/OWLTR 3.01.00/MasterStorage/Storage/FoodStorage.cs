/*
 created by:
		Hammerhand
*/
using System;
using Server;
using Server.Items;
using System.Collections.Generic;

namespace daat99
{
	public class FoodStorage : BaseStorage
	{
		public FoodStorage() : base() { }
        public FoodStorage(GenericReader reader) : base(reader) { }
        public override string Name { get { return "Food Storage"; } }
        private static Type[] defaultStoredTypes = new Type[] 
		{ 
			typeof(Food),		typeof(CookableFood),	typeof(Dough),		typeof(SweetDough),
			typeof(JarHoney),	typeof(BowlFlour),		typeof(WheatSheaf),	typeof(WoodenBowl),
			typeof(Eggshells),	typeof(SackFlour)
		};
        public override Type[] DefaultStoredTypes { get { return FoodStorage.defaultStoredTypes; } }
		protected static new BaseStorage singleton;
        public static new BaseStorage Storage { get { if (singleton == null) singleton = new FoodStorage(); return singleton; } }
        public override BaseStorage GetStorage() { return FoodStorage.Storage; }
		public override Dictionary<Type, int> GetStorableTypesFromItem(Item item)
		{
			Dictionary<Type, int> types = base.GetStorableTypesFromItem(item);
			if (types == null)
				return new Dictionary<Type, int>(0);
			if (types.Count == 0)
				return types;

			SackFlour sack = item as SackFlour;
			if (sack != null && sack.Quantity > 0 && types.ContainsKey(typeof(SackFlour)))
				types[typeof(SackFlour)] = sack.Quantity;
			return types;
		}
	}
	public class FoodStorageDeed : BaseStorageDeed
	{
		[Constructable]
        public FoodStorageDeed() : base(FoodStorage.Storage) { }
        public FoodStorageDeed(Serial serial) : base(serial) { }
		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}
	}
}
