/***************************************************************************
 *   Based off the RunUO Craft system. This program is free software; you 
 *   can redistribute it and/or modify it under the terms of the GNU GPL. 
 ***************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Items;
using Server.Factions;
using Server.Mobiles;
using Server.Commands;

namespace Server.Engines.Build
{
	public enum ConsumeType
	{
		All, Half, None
	}

	public interface IBuildable
	{
		int OnBuild( int quality, bool makersMark, Mobile from, BuildSystem buildSystem, Type typeRes, BaseBuildingTool tool, BuildItem buildItem, int resHue );
	}

	public class BuildItem
	{
		private BuildResCol m_arBuildRes;
		private BuildLokaiSkillCol m_arBuildLokaiSkill;
		private Type m_Type;

        private object m_Arg;

		private string m_GroupNameString;
		private int m_GroupNameNumber;

		private string m_NameString;
		private int m_NameNumber;
		
		private int m_Mana;
		private int m_Hits;
		private int m_Stam;

		private bool m_UseAllRes;

		private bool m_NeedHeat;
		private bool m_NeedOven;
        private bool m_NeedMill;

        private bool m_NeedWoodworker;
        private bool m_NeedCooper;
        private bool m_NeedForeman;

		private bool m_UseSubRes2;

		private bool m_ForceNonExceptional;

		public bool ForceNonExceptional
		{
			get { return m_ForceNonExceptional; }
			set { m_ForceNonExceptional = value; }
		}
	

		private Expansion m_RequiredExpansion;

		public Expansion RequiredExpansion
		{
			get { return m_RequiredExpansion; }
			set { m_RequiredExpansion = value; }
		}

		private Recipe m_Recipe;

		public Recipe Recipe
		{
			get { return m_Recipe; }
		}

		public void AddRecipe( int id, BuildSystem system )
		{
			if( m_Recipe != null )
			{
				Console.WriteLine( "Warning: Attempted add of recipe #{0} to the building of {1} in BuildSystem {2}.", id, this.m_Type.Name, system );
				return;
			}

			m_Recipe = new Recipe( id, system, this );
		}


		private static Dictionary<Type, int> _itemIds = new Dictionary<Type, int>();
		
		public static int ItemIDOf( Type type ) {
			int itemId;

			if ( !_itemIds.TryGetValue( type, out itemId ) ) {
				if ( type == typeof( FactionExplosionTrap ) ) {
					itemId = 14034;
				} else if ( type == typeof( FactionGasTrap ) ) {
					itemId = 4523;
				} else if ( type == typeof( FactionSawTrap ) ) {
					itemId = 4359;
				} else if ( type == typeof( FactionSpikeTrap ) ) {
					itemId = 4517;
				}

				if ( itemId == 0 ) {
					object[] attrs = type.GetCustomAttributes( typeof( BuildItemIDAttribute ), false );

					if ( attrs.Length > 0 ) {
						BuildItemIDAttribute buildItemID = ( BuildItemIDAttribute ) attrs[0];
						itemId = buildItemID.ItemID;
					}
				}

				if ( itemId == 0 ) {
					Item item = null;

					try { item = Activator.CreateInstance( type ) as Item; } catch { }

					if ( item != null ) {
						itemId = item.ItemID;
						item.Delete();
					}
				}

				_itemIds[type] = itemId;
			}

			return itemId;
		}

        public BuildItem(Type type, TextDefinition groupName, TextDefinition name)
            : this(type, groupName, name, null)
        {
        }

		public BuildItem( Type type, TextDefinition groupName, TextDefinition name, object arg )
		{
			m_arBuildRes = new BuildResCol();
			m_arBuildLokaiSkill = new BuildLokaiSkillCol();

			m_Type = type;

			m_GroupNameString = groupName;
			m_NameString = name;

			m_GroupNameNumber = groupName;
			m_NameNumber = name;

            m_Arg = arg;
		}

		public void AddRes( Type type, TextDefinition name, int amount )
		{
			AddRes( type, name, amount, "" );
		}

		public void AddRes( Type type, TextDefinition name, int amount, TextDefinition message )
		{
			BuildRes buildRes = new BuildRes( type, name, amount, message );
			m_arBuildRes.Add( buildRes );
		}


		public void AddLokaiSkill( LokaiSkillName lokaiSkillToMake, double minLokaiSkill, double maxLokaiSkill )
		{
			BuildLokaiSkill buildLokaiSkill = new BuildLokaiSkill( lokaiSkillToMake, minLokaiSkill, maxLokaiSkill );
			m_arBuildLokaiSkill.Add( buildLokaiSkill );
		}

		public int Mana
		{
			get { return m_Mana; }
			set { m_Mana = value; }
		}

		public int Hits
		{
			get { return m_Hits; }
			set { m_Hits = value; }
		}

		public int Stam
		{
			get { return m_Stam; }
			set { m_Stam = value; }
		}

		public bool UseSubRes2
		{
			get { return m_UseSubRes2; }
			set { m_UseSubRes2 = value; }
		}

		public bool UseAllRes
		{
			get { return m_UseAllRes; }
			set { m_UseAllRes = value; }
		}

		public bool NeedHeat
		{
			get { return m_NeedHeat; }
			set { m_NeedHeat = value; }
		}

		public bool NeedOven
		{
			get { return m_NeedOven; }
			set { m_NeedOven = value; }
		}

		public bool NeedMill
		{
			get { return m_NeedMill; }
			set { m_NeedMill = value; }
        }

        public bool NeedWoodworker
        {
            get { return m_NeedWoodworker; }
            set { m_NeedWoodworker = value; }
        }

        public bool NeedCooper
        {
            get { return m_NeedCooper; }
            set { m_NeedCooper = value; }
        }

        public bool NeedForeman
        {
            get { return m_NeedForeman; }
            set { m_NeedForeman = value; }
        }
		
		public Type ItemType
		{
			get { return m_Type; }
		}

		public string GroupNameString
		{
			get { return m_GroupNameString; }
		}

		public int GroupNameNumber
		{
			get { return m_GroupNameNumber; }
		}

		public string NameString
		{
			get { return m_NameString; }
		}

		public int NameNumber
		{
			get { return m_NameNumber; }
		}

		public BuildResCol Ressources
		{
			get { return m_arBuildRes; }
		}

		public BuildLokaiSkillCol LokaiSkills
		{
			get { return m_arBuildLokaiSkill; }
		}

		public bool ConsumeAttributes( Mobile from, ref object message, bool consume )
		{
			bool consumMana = false;
			bool consumHits = false;
			bool consumStam = false;

			if ( Hits > 0 && from.Hits < Hits )
			{
				message = "You lack the required hit points to make that.";
				return false;
			}
			else
			{
				consumHits = consume;
			}

			if ( Mana > 0 && from.Mana < Mana )
			{
				message = "You lack the required mana to make that.";
				return false;
			}
			else
			{
				consumMana = consume;
			}

			if ( Stam > 0 && from.Stam < Stam )
			{
				message = "You lack the required stamina to make that.";
				return false;
			}
			else
			{
				consumStam = consume;
			}

			if ( consumMana )
				from.Mana -= Mana;

			if ( consumHits )
				from.Hits -= Hits;

			if ( consumStam )
				from.Stam -= Stam;

			return true;
		}

		#region Tables
		private static int[] m_HeatSources = new int[]
			{
				0x461, 0x48E, // Sandstone oven/fireplace
				0x92B, 0x96C, // Stone oven/fireplace
				0xDE3, 0xDE9, // Campfire
				0xFAC, 0xFAC, // Firepit
				0x184A, 0x184C, // Heating stand (left)
				0x184E, 0x1850, // Heating stand (right)
				0x398C, 0x399F,  // Fire field
				0x2DDB, 0x2DDC	//Elven stove
			};

		private static int[] m_Ovens = new int[]
			{
				0x461, 0x46F, // Sandstone oven
				0x92B, 0x93F,  // Stone oven
				0x2DDB, 0x2DDC	//Elven stove
			};

		private static int[] m_Mills = new int[]
			{
				0x1920, 0x1921, 0x1922, 0x1923, 0x1924, 0x1295, 0x1926, 0x1928,
				0x192C, 0x192D, 0x192E, 0x129F, 0x1930, 0x1931, 0x1932, 0x1934
			};

        private static int[] m_WoodworkersBench = new int[]
			{
				0x19F1, 0x19F2, 0x19F3, 0x19F4, // Woodworker's Bench
				0x19F5, 0x19F6, 0x19F7, 0x19F8
			};

        private static int[] m_CoopersBench = new int[]
			{
				0x19F9, 0x19FA, 0x19FB, 0x19FC // Cooper's Bench
			};

		private static Type[][] m_TypesTable = new Type[][] { };

		private static Type[] m_ColoredItemTable = new Type[] { };

		private static Type[] m_ColoredResourceTable = new Type[] { };

		private static Type[] m_MarkableTable = new Type[] { };
		#endregion

		public bool IsMarkable( Type type ) { return false; }

		public bool RetainsColorFrom( BuildSystem system, Type type ) { return false; }

		public bool Find( Mobile from, int[] itemIDs )
		{
			Map map = from.Map;

			if ( map == null )
				return false;

			IPooledEnumerable eable = map.GetItemsInRange( from.Location, 2 );

			foreach ( Item item in eable )
			{
				if ( (item.Z + 16) > from.Z && (from.Z + 16) > item.Z && Find( item.ItemID, itemIDs ) )
				{
					eable.Free();
					return true;
				}
			}

			eable.Free();

			for ( int x = -2; x <= 2; ++x )
			{
				for ( int y = -2; y <= 2; ++y )
				{
					int vx = from.X + x;
					int vy = from.Y + y;

					StaticTile[] tiles = map.Tiles.GetStaticTiles( vx, vy, true );

					for ( int i = 0; i < tiles.Length; ++i )
					{
						int z = tiles[i].Z;
						int id = tiles[i].ID & 0x3FFF;

						if ( (z + 16) > from.Z && (from.Z + 16) > z && Find( id, itemIDs ) )
							return true;
					}
				}
			}

			return false;
		}

        public bool Find(Mobile from, Type toFind, int range)
        {
            Map map = from.Map;

            if (map == null)
                return false;

            IPooledEnumerable eable = map.GetItemsInRange(from.Location, range);

            foreach (Item item in eable)
            {
                if (item.GetType() == toFind)
                {
                    eable.Free();
                    return true;
                }
            }

            eable = map.GetMobilesInRange(from.Location, range);

            foreach (Mobile mobile in eable)
            {
                if (mobile.GetType() == toFind)
                {
                    eable.Free();
                    return true;
                }
            }

            eable.Free();
            return false;
        }

		public bool Find( int itemID, int[] itemIDs )
		{
			bool contains = false;

			for ( int i = 0; !contains && i < itemIDs.Length; i += 2 )
				contains = ( itemID >= itemIDs[i] && itemID <= itemIDs[i + 1] );

			return contains;
		}

		public bool IsQuantityType( Type[][] types )
		{
			for ( int i = 0; i < types.Length; ++i )
			{
                Type[] check = types[i];

				for ( int j = 0; j < check.Length; ++j )
				{
					if ( typeof( IHasQuantity ).IsAssignableFrom( check[j] ) )
						return true;
				}
			}

			return false;
		}

		public int ConsumeQuantity( Container cont, Type[][] types, int[] amounts )
		{
			if ( types.Length != amounts.Length )
				throw new ArgumentException();

			Item[][] items = new Item[types.Length][];
			int[] totals = new int[types.Length];

			for ( int i = 0; i < types.Length; ++i )
			{
				items[i] = cont.FindItemsByType( types[i], true );

				for ( int j = 0; j < items[i].Length; ++j )
				{
					IHasQuantity hq = items[i][j] as IHasQuantity;

					if ( hq == null )
					{
						totals[i] += items[i][j].Amount;
					}
					else
					{
						if ( hq is BaseBeverage && ((BaseBeverage)hq).Content != BeverageType.Water )
							continue;

						totals[i] += hq.Quantity;
					}
				}

				if ( totals[i] < amounts[i] )
					return i;
			}

			for ( int i = 0; i < types.Length; ++i )
			{
				int need = amounts[i];

				for ( int j = 0; j < items[i].Length; ++j )
				{
					Item item = items[i][j];
					IHasQuantity hq = item as IHasQuantity;

					if ( hq == null )
					{
						int theirAmount = item.Amount;

						if ( theirAmount < need )
						{
							item.Delete();
							need -= theirAmount;
						}
						else
						{
							item.Consume( need );
							break;
						}
					}
					else
					{
						if ( hq is BaseBeverage && ((BaseBeverage)hq).Content != BeverageType.Water )
							continue;

						int theirAmount = hq.Quantity;

						if ( theirAmount < need )
						{
							hq.Quantity -= theirAmount;
							need -= theirAmount;
						}
						else
						{
							hq.Quantity -= need;
							break;
						}
					}
				}
			}

			return -1;
		}

		public int GetQuantity( Container cont, Type[] types )
		{
			Item[] items = cont.FindItemsByType( types, true );

			int amount = 0;

			for ( int i = 0; i < items.Length; ++i )
			{
				IHasQuantity hq = items[i] as IHasQuantity;

				if ( hq == null )
				{
					amount += items[i].Amount;
				}
				else
				{
					if ( hq is BaseBeverage && ((BaseBeverage)hq).Content != BeverageType.Water )
						continue;

					amount += hq.Quantity;
				}
			}

			return amount;
		}

		public bool ConsumeRes( Mobile from, Type typeRes, BuildSystem buildSystem, ref int resHue, ref int maxAmount, ConsumeType consumeType, ref object message )
		{
			return ConsumeRes( from, typeRes, buildSystem, ref resHue, ref maxAmount, consumeType, ref message, false );
		}

		public bool ConsumeRes( Mobile from, Type typeRes, BuildSystem buildSystem, ref int resHue, ref int maxAmount, ConsumeType consumeType, ref object message, bool isFailure )
		{
			Container ourPack = from.Backpack;

			if ( ourPack == null )
				return false;

			if ( m_NeedHeat && !Find( from, m_HeatSources ) )
			{
				message = 1044487; // You must be near a fire source to cook.
				return false;
			}

			if ( m_NeedOven && !Find( from, m_Ovens ) )
			{
				message = 1044493; // You must be near an oven to bake that.
				return false;
			}

			if ( m_NeedMill && !Find( from, m_Mills ) )
			{
				message = 1044491; // You must be near a flour mill to do that.
				return false;
            }

            if (m_NeedWoodworker && !Find(from, m_WoodworkersBench))
            {
                message = "You must be near a woodworker's bench to do that.";
                return false;
            }

            if (m_NeedCooper && !Find(from, m_CoopersBench))
            {
                message = "You must be near a cooper's bench to do that.";
                return false;
            }

            if (m_NeedForeman && !Find(from, typeof(Foreman), 3))
            {
                message = "You must be near a foreman to do that.";
                return false;
            }

            if (m_arBuildRes.Count == 0)
            {
                message = "Resource count is 0?";
                return false;
            }

			Type[][] types = new Type[m_arBuildRes.Count][];
			int[] amounts = new int[m_arBuildRes.Count];

			maxAmount = int.MaxValue;

			BuildSubResCol resCol = ( m_UseSubRes2 ? buildSystem.BuildSubRes2 : buildSystem.BuildSubRes );

			for ( int i = 0; i < types.Length; ++i )
			{
				BuildRes buildRes = m_arBuildRes.GetAt( i );
				Type baseType = buildRes.ItemType;

				// Resource Mutation
				if ( (baseType == resCol.ResType) && ( typeRes != null ) )
				{
					baseType = typeRes;

					BuildSubRes subResource = resCol.SearchFor( baseType );

                    if (subResource != null && LokaiSkillUtilities.XMLGetSkills(from)[buildSystem.MainLokaiSkill].Base < subResource.RequiredLokaiSkill)
					{
						message = subResource.Message;
						return false;
					}
				}
                // ******************

                for (int j = 0; types[i] == null && j < m_TypesTable.Length; ++j)
                {
                    if (m_TypesTable[j][0] == baseType)
                        types[i] = m_TypesTable[j];
                }

                if (types[i] == null)
                    types[i] = new Type[] { baseType };

				amounts[i] = buildRes.Amount;

				// For stackable items that can be built more than one at a time
				if ( UseAllRes )
				{
					int tempAmount = ourPack.GetAmount( types[i] );
					tempAmount /= amounts[i];
					if ( tempAmount < maxAmount )
					{
						maxAmount = tempAmount;

						if ( maxAmount == 0 )
						{
							BuildRes res = m_arBuildRes.GetAt( i );

							if ( res.MessageNumber > 0 )
								message = res.MessageNumber;
							else if ( !String.IsNullOrEmpty( res.MessageString ) )
								message = res.MessageString;
							else
								message = 502925; // You don't have the resources required to make that item.

							return false;
						}
					}
				}
				// ****************************

				if ( isFailure && !buildSystem.ConsumeOnFailure( from, types[i][0], this ) )
					amounts[i] = 0;
			}

			// We adjust the amount of each resource to consume the max possible
			if ( UseAllRes )
			{
				for ( int i = 0; i < amounts.Length; ++i )
					amounts[i] *= maxAmount;
			}
			else
				maxAmount = -1;
			
			int index = 0;

			// Consume ALL
			if ( consumeType == ConsumeType.All )
			{
				m_ResHue = 0; m_ResAmount = 0; m_System = buildSystem;

				if ( IsQuantityType( types ) )
					index = ConsumeQuantity( ourPack, types, amounts );
				else
					index = ourPack.ConsumeTotalGrouped( types, amounts, true, new OnItemConsumed( OnResourceConsumed ), new CheckItemGroup( CheckHueGrouping ) );

				resHue = m_ResHue;
			}

			// Consume Half ( for use all resource build type )
			else if ( consumeType == ConsumeType.Half )
			{
				for ( int i = 0; i < amounts.Length; i++ )
				{
					amounts[i] /= 2;

					if ( amounts[i] < 1 )
						amounts[i] = 1;
				}

				m_ResHue = 0; m_ResAmount = 0; m_System = buildSystem;

				if ( IsQuantityType( types ) )
					index = ConsumeQuantity( ourPack, types, amounts );
				else
					index = ourPack.ConsumeTotalGrouped( types, amounts, true, new OnItemConsumed( OnResourceConsumed ), new CheckItemGroup( CheckHueGrouping ) );

				resHue = m_ResHue;
			}

			else // ConstumeType.None ( it's basicaly used to know if the builder has enough resource before starting the process )
			{
				index = -1;

				if ( IsQuantityType( types ) )
				{
					for ( int i = 0; i < types.Length; i++ )
					{
						if ( GetQuantity( ourPack, types[i] ) < amounts[i] )
						{
							index = i;
							break;
						}
					}
				}
				else
				{
					for ( int i = 0; i < types.Length; i++ )
					{
						if ( ourPack.GetBestGroupAmount( types[i], true, new CheckItemGroup( CheckHueGrouping ) ) < amounts[i] )
						{
							index = i;
							break;
						}
					}
				}
			}

			if ( index == -1 )
			{
				return true;
			}
			else
			{
				BuildRes res = m_arBuildRes.GetAt( index );

				if ( res.MessageNumber > 0 )
					message = res.MessageNumber;
				else if ( res.MessageString != null && res.MessageString != String.Empty )
					message = res.MessageString;
				else
					message = 502925; // You don't have the resources required to make that item.

				return false;
			}
		}

		private int m_ResHue;
		private int m_ResAmount;
		private BuildSystem m_System;

		private void OnResourceConsumed( Item item, int amount )
		{
			if ( !RetainsColorFrom( m_System, item.GetType() ) )
				return;

			if ( amount >= m_ResAmount )
			{
				m_ResHue = item.Hue;
				m_ResAmount = amount;
			}
		}

		private int CheckHueGrouping( Item a, Item b )
		{
			return b.Hue.CompareTo( a.Hue );
		}

		public double GetExceptionalChance( BuildSystem system, double chance, Mobile from )
		{
			if( m_ForceNonExceptional )
				return 0.0;

			switch ( system.ECA )
			{
				default:
				case BuildECA.ChanceMinusSixty: return chance - 0.6;
				case BuildECA.FiftyPercentChanceMinusTenPercent: return (chance * 0.5) - 0.1;
				case BuildECA.ChanceMinusSixtyToFourtyFive:
				{
                    double offset = 0.60 - ((LokaiSkillUtilities.XMLGetSkills(from)[system.MainLokaiSkill].Value - 95.0) * 0.03);

					if ( offset < 0.45 )
						offset = 0.45;
					else if ( offset > 0.60 )
						offset = 0.60;

					return chance - offset;
				}
			}
		}

		public bool CheckLokaiSkills( Mobile from, Type typeRes, BuildSystem buildSystem, ref int quality, ref bool allRequiredLokaiSkills )
		{
			return CheckLokaiSkills( from, typeRes, buildSystem, ref quality, ref allRequiredLokaiSkills, true );
		}

		public bool CheckLokaiSkills( Mobile from, Type typeRes, BuildSystem buildSystem, ref int quality, ref bool allRequiredLokaiSkills, bool gainLokaiSkills )
		{
			double chance = GetSuccessChance( from, typeRes, buildSystem, gainLokaiSkills, ref allRequiredLokaiSkills );

			if ( GetExceptionalChance( buildSystem, chance, from ) > Utility.RandomDouble() )
				quality = 2;

			return ( chance > Utility.RandomDouble() );
		}

		public double GetSuccessChance( Mobile from, Type typeRes, BuildSystem buildSystem, bool gainLokaiSkills, ref bool allRequiredLokaiSkills )
		{
			double minMainLokaiSkill = 0.0;
			double maxMainLokaiSkill = 0.0;
			double valMainLokaiSkill = 0.0;

			allRequiredLokaiSkills = true;

            LokaiSkills skills = LokaiSkillUtilities.XMLGetSkills(from);

			for ( int i = 0; i < m_arBuildLokaiSkill.Count; i++)
			{
				BuildLokaiSkill buildLokaiSkill = m_arBuildLokaiSkill.GetAt(i);

				double minLokaiSkill = buildLokaiSkill.MinLokaiSkill;
				double maxLokaiSkill = buildLokaiSkill.MaxLokaiSkill;
                double valLokaiSkill = skills[buildLokaiSkill.LokaiSkillToMake].Value;

				if ( valLokaiSkill < minLokaiSkill )
					allRequiredLokaiSkills = false;

				if ( buildLokaiSkill.LokaiSkillToMake == buildSystem.MainLokaiSkill )
				{
					minMainLokaiSkill = minLokaiSkill;
					maxMainLokaiSkill = maxLokaiSkill;
					valMainLokaiSkill = valLokaiSkill;
				}

				if ( gainLokaiSkills ) // This is a passive check. Success chance is entirely dependant on the main lokaiSkill
                    LokaiSkillUtilities.CheckLokaiSkill(from, skills[buildLokaiSkill.LokaiSkillToMake], minLokaiSkill, maxLokaiSkill);
			}

			double chance;

			if ( allRequiredLokaiSkills )
				chance = buildSystem.GetChanceAtMin( this ) + ((valMainLokaiSkill - minMainLokaiSkill) / (maxMainLokaiSkill - minMainLokaiSkill) * (1.0 - buildSystem.GetChanceAtMin( this )));
			else
				chance = 0.0;

			if ( allRequiredLokaiSkills && valMainLokaiSkill == maxMainLokaiSkill )
				chance = 1.0;

			return chance;
		}

		public void Build( Mobile from, BuildSystem buildSystem, Type typeRes, BaseBuildingTool tool )
		{
			if ( from.BeginAction( typeof( BuildSystem ) ) )
			{
				if( RequiredExpansion == Expansion.None || ( from.NetState != null && from.NetState.SupportsExpansion( RequiredExpansion ) ) )
				{
					bool allRequiredLokaiSkills = true;
					double chance = GetSuccessChance( from, typeRes, buildSystem, false, ref allRequiredLokaiSkills );

					if ( allRequiredLokaiSkills && chance >= 0.0 )
					{
						if( this.Recipe == null)
						{
							int badBuild = buildSystem.CanBuild( from, tool, m_Type );

							if( badBuild <= 0 )
							{
								int resHue = 0;
								int maxAmount = 0;
								object message = null;

								if( ConsumeRes( from, typeRes, buildSystem, ref resHue, ref maxAmount, ConsumeType.None, ref message ) )
								{
									message = null;

									if( ConsumeAttributes( from, ref message, false ) )
									{
										BuildContext context = buildSystem.GetContext( from );

										if( context != null )
											context.OnMade( this );

										int iMin = buildSystem.MinBuildEffect;
										int iMax = (buildSystem.MaxBuildEffect - iMin) + 1;
										int iRandom = Utility.Random( iMax );
										iRandom += iMin + 1;
										new InternalTimer( from, buildSystem, this, typeRes, tool, iRandom ).Start();
									}
									else
									{
										from.EndAction( typeof( BuildSystem ) );
										from.SendGump( new BuildGump( from, buildSystem, tool, message ) );
									}
								}
								else
								{
									from.EndAction( typeof( BuildSystem ) );
									from.SendGump( new BuildGump( from, buildSystem, tool, message ) );
								}
							}
							else
							{
								from.EndAction( typeof( BuildSystem ) );
								from.SendGump( new BuildGump( from, buildSystem, tool, badBuild ) );
							}
						}
						else
						{
							from.EndAction( typeof( BuildSystem ) );
							from.SendGump( new BuildGump( from, buildSystem, tool, 1072847 ) ); // You must learn that recipe from a scroll.
						}
					}
					else
					{
						from.EndAction( typeof( BuildSystem ) );
						from.SendGump( new BuildGump( from, buildSystem, tool, 1044153 ) ); // You don't have the required lokaiSkills to attempt this item.
					}
				}
				else
				{
					from.EndAction( typeof( BuildSystem ) );
					from.SendGump( new BuildGump( from, buildSystem, tool, RequiredExpansionMessage( RequiredExpansion ) ) ); //The {0} expansion is required to attempt this item.
				}
			}
			else
			{
				from.SendLocalizedMessage( 500119 ); // You must wait to perform another action
			}
		}

		private object RequiredExpansionMessage( Expansion expansion )	//Eventually convert to TextDefinition, but that requires that we convert all the gumps to ues it too.  Not that it wouldn't be a bad idea.
		{
			switch( expansion )
			{
				case Expansion.SE:
					return 1063307; // The "Samurai Empire" expansion is required to attempt this item.
				case Expansion.ML:
					return 1072650; // The "Mondain's Legacy" expansion is required to attempt this item.
				default:
					return String.Format( "The \"{0}\" expansion is required to attempt this item.", ExpansionInfo.GetInfo( expansion ).Name );
			}
		}

		public void CompleteBuild( int quality, bool makersMark, Mobile from, BuildSystem buildSystem, Type typeRes, BaseBuildingTool tool, CustomBuild customBuild )
		{
			int badBuild = buildSystem.CanBuild( from, tool, m_Type );

			if ( badBuild > 0 )
			{
				if ( tool != null && !tool.Deleted && tool.UsesRemaining > 0 )
					from.SendGump( new BuildGump( from, buildSystem, tool, badBuild ) );
				else
					from.SendLocalizedMessage( badBuild );

				return;
			}

			int checkResHue = 0, checkMaxAmount = 0;
			object checkMessage = null;

			// Not enough resource to build it
			if ( !ConsumeRes( from, typeRes, buildSystem, ref checkResHue, ref checkMaxAmount, ConsumeType.None, ref checkMessage ) )
			{
				if ( tool != null && !tool.Deleted && tool.UsesRemaining > 0 )
					from.SendGump( new BuildGump( from, buildSystem, tool, checkMessage ) );
				else if ( checkMessage is int && (int)checkMessage > 0 )
					from.SendLocalizedMessage( (int)checkMessage );
				else if ( checkMessage is string )
					from.SendMessage( (string)checkMessage );

				return;
			}
			else if ( !ConsumeAttributes( from, ref checkMessage, false ) )
			{
				if ( tool != null && !tool.Deleted && tool.UsesRemaining > 0 )
					from.SendGump( new BuildGump( from, buildSystem, tool, checkMessage ) );
				else if ( checkMessage is int && (int)checkMessage > 0 )
					from.SendLocalizedMessage( (int)checkMessage );
				else if ( checkMessage is string )
					from.SendMessage( (string)checkMessage );

				return;
			}

			bool toolBroken = false;

			int ignored = 1;
			int endquality = 1;

			bool allRequiredLokaiSkills = true;

			if ( CheckLokaiSkills( from, typeRes, buildSystem, ref ignored, ref allRequiredLokaiSkills ) )
			{
				// Resource
				int resHue = 0;
				int maxAmount = 0;

				object message = null;

				// Not enough resource to build it
				if ( !ConsumeRes( from, typeRes, buildSystem, ref resHue, ref maxAmount, ConsumeType.All, ref message ) )
				{
					if ( tool != null && !tool.Deleted && tool.UsesRemaining > 0 )
						from.SendGump( new BuildGump( from, buildSystem, tool, message ) );
					else if ( message is int && (int)message > 0 )
						from.SendLocalizedMessage( (int)message );
					else if ( message is string )
						from.SendMessage( (string)message );

					return;
				}
				else if ( !ConsumeAttributes( from, ref message, true ) )
				{
					if ( tool != null && !tool.Deleted && tool.UsesRemaining > 0 )
						from.SendGump( new BuildGump( from, buildSystem, tool, message ) );
					else if ( message is int && (int)message > 0 )
						from.SendLocalizedMessage( (int)message );
					else if ( message is string )
						from.SendMessage( (string)message );

					return;
				}

				tool.UsesRemaining--;

				if ( tool.UsesRemaining < 1 )
					toolBroken = true;

				if ( toolBroken )
					tool.Delete();

				int num = 0;

				Item item;
				if ( customBuild != null )
				{
					item = customBuild.CompleteBuild( out num );
				}
				else if ( typeof( MapItem ).IsAssignableFrom( ItemType ) && from.Map != Map.Trammel && from.Map != Map.Felucca )
				{
					item = new IndecipherableMap();
					from.SendLocalizedMessage( 1070800 ); // The map you create becomes mysteriously indecipherable.
				}
				else
				{
                    if (m_Arg != null)
                        item = Activator.CreateInstance(ItemType, new object[] { m_Arg }) as Item;
                    else
                        item = Activator.CreateInstance( ItemType ) as Item;
				}

				if ( item != null )
				{
					if( item is IBuildable )
						endquality = ((IBuildable)item).OnBuild( quality, makersMark, from, buildSystem, typeRes, tool, this, resHue );
					else if ( item.Hue == 0 )
						item.Hue = resHue;

					if ( maxAmount > 0 )
					{
                        item.Amount = maxAmount;
					}

					from.AddToBackpack( item );

					if( from.AccessLevel > AccessLevel.Player )
						CommandLogging.WriteLine( from, "Building {0} with build system {1}", CommandLogging.Format( item ), buildSystem.GetType().Name );

					//from.PlaySound( 0x57 );
				}

				if ( num == 0 )
					num = buildSystem.PlayEndingEffect( from, false, true, toolBroken, endquality, makersMark, this );

				if ( tool != null && !tool.Deleted && tool.UsesRemaining > 0 )
					from.SendGump( new BuildGump( from, buildSystem, tool, num ) );
				else if ( num > 0 )
					from.SendLocalizedMessage( num );
			}
			else if ( !allRequiredLokaiSkills )
			{
				if ( tool != null && !tool.Deleted && tool.UsesRemaining > 0 )
					from.SendGump( new BuildGump( from, buildSystem, tool, 1044153 ) );
				else
					from.SendLocalizedMessage( 1044153 ); // You don't have the required lokaiSkills to attempt this item.
			}
			else
			{
				ConsumeType consumeType = ( UseAllRes ? ConsumeType.Half : ConsumeType.All );
				int resHue = 0;
				int maxAmount = 0;

				object message = null;

				// Not enough resource to build it
				if ( !ConsumeRes( from, typeRes, buildSystem, ref resHue, ref maxAmount, consumeType, ref message, true ) )
				{
					if ( tool != null && !tool.Deleted && tool.UsesRemaining > 0 )
						from.SendGump( new BuildGump( from, buildSystem, tool, message ) );
					else if ( message is int && (int)message > 0 )
						from.SendLocalizedMessage( (int)message );
					else if ( message is string )
						from.SendMessage( (string)message );

					return;
				}

				tool.UsesRemaining--;

				if ( tool.UsesRemaining < 1 )
					toolBroken = true;

				if ( toolBroken )
					tool.Delete();

				// LokaiSkillCheck failed.
				int num = buildSystem.PlayEndingEffect( from, true, true, toolBroken, endquality, false, this );

				if ( tool != null && !tool.Deleted && tool.UsesRemaining > 0 )
					from.SendGump( new BuildGump( from, buildSystem, tool, num ) );
				else if ( num > 0 )
					from.SendLocalizedMessage( num );
			}
		}

		private class InternalTimer : Timer
		{
			private Mobile m_From;
			private int m_iCount;
			private int m_iCountMax;
			private BuildItem m_BuildItem;
			private BuildSystem m_BuildSystem;
			private Type m_TypeRes;
			private BaseBuildingTool m_Tool;

			public InternalTimer( Mobile from, BuildSystem buildSystem, BuildItem buildItem, Type typeRes, BaseBuildingTool tool, int iCountMax ) : base( TimeSpan.Zero, TimeSpan.FromSeconds( buildSystem.Delay ), iCountMax )
			{
				m_From = from;
				m_BuildItem = buildItem;
				m_iCount = 0;
				m_iCountMax = iCountMax;
				m_BuildSystem = buildSystem;
				m_TypeRes = typeRes;
				m_Tool = tool;
			}

			protected override void OnTick()
			{
				m_iCount++;

				m_From.DisruptiveAction();

				if ( m_iCount < m_iCountMax )
				{
					m_BuildSystem.PlayBuildEffect( m_From );
				}
				else
				{
					m_From.EndAction( typeof( BuildSystem ) );

					int badBuild = m_BuildSystem.CanBuild( m_From, m_Tool, m_BuildItem.m_Type );

					if ( badBuild > 0 )
					{
						if ( m_Tool != null && !m_Tool.Deleted && m_Tool.UsesRemaining > 0 )
							m_From.SendGump( new BuildGump( m_From, m_BuildSystem, m_Tool, badBuild ) );
						else
							m_From.SendLocalizedMessage( badBuild );

						return;
					}

					int quality = 1;
					bool allRequiredLokaiSkills = true;

					m_BuildItem.CheckLokaiSkills( m_From, m_TypeRes, m_BuildSystem, ref quality, ref allRequiredLokaiSkills, false );

					BuildContext context = m_BuildSystem.GetContext( m_From );

					if ( context == null )
						return;

					if ( typeof( CustomBuild ).IsAssignableFrom( m_BuildItem.ItemType ) )
					{
						CustomBuild cc = null;

						try{ cc = Activator.CreateInstance( m_BuildItem.ItemType, new object[] { m_From, m_BuildItem, m_BuildSystem, m_TypeRes, m_Tool, quality } ) as CustomBuild; }
						catch{}

						if ( cc != null )
							cc.EndBuildAction();

						return;
					}

					bool makersMark = false;

					if ( quality == 2 && LokaiSkillUtilities.XMLGetSkills(m_From)[m_BuildSystem.MainLokaiSkill].Base >= 100.0 )
						makersMark = m_BuildItem.IsMarkable( m_BuildItem.ItemType );

					if ( makersMark && context.MarkOption == BuildMarkOption.PromptForMark )
					{
						m_From.SendGump( new QueryMakersMarkGump( quality, m_From, m_BuildItem, m_BuildSystem, m_TypeRes, m_Tool ) );
					}
					else
					{
						if ( context.MarkOption == BuildMarkOption.DoNotMark )
							makersMark = false;

						m_BuildItem.CompleteBuild( quality, makersMark, m_From, m_BuildSystem, m_TypeRes, m_Tool, null );
					}
				}
			}
		}
	}
}
