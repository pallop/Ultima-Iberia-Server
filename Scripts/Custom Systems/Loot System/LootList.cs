using System;
using System.Reflection;
using System.IO;
using Server;
using Server.Items;
using Server.ACC.CSS.Systems.Ancient;
using Server.ACC.CSS.Systems.Avatar;
using Server.ACC.CSS.Systems.Cleric;
using Server.ACC.CSS.Systems.Bard;
using Server.ACC.CSS.Systems.Druid;
using Server.ACC.CSS.Systems.Ranger;


namespace Server
{
	public class ArtifactList
	{
		static int index;
		static Type type;

		public static Type[] ArtifactTypes = new Type[]
		{
		
		//Robes	
			typeof(HoodableRobe),//typeof(NHMFreeLife),
			
		//Scrolls 
			/*//AncientScrolls
			typeof(AncientFireworksScroll ),typeof(AncientGlimmerScroll ),typeof( AncientAwakenScroll),typeof( AncientThunderScroll),typeof( AncientWeatherScroll),typeof( AncientIgniteScroll),typeof( AncientDouseScroll),typeof(AncientLocateScroll ),typeof( AncientAwakenAllScroll),typeof( AncientDetectTrapScroll),typeof(AncientGreatDouseScroll ),typeof(AncientGreatIgniteScroll ),typeof(AncientEnchantScroll),typeof( AncientFalseCoinScroll),
			typeof( AncientGreatLightScroll),typeof( AncientDestroyTrapScroll),typeof( AncientSleepScroll),typeof(AncientSwarmScroll ),typeof( AncientPeerScroll),typeof(AncientSeanceScroll ),typeof(AncientCharmScroll ),typeof(AncientDanceScroll ),typeof(AncientMassSleepScroll ),typeof(AncientCloneScroll ),typeof(AncientCauseFearScroll ),typeof( AncientFireRingScroll),typeof(AncientTremorScroll ),typeof(AncientSleepFieldScroll ),
			typeof(AncientMassMightScroll ),typeof( AncientMassCharmScroll),typeof(AncientInvisibilityAllScroll ),typeof(AncientDeathVortexScroll ),typeof(AncientMassDeathScroll ),

			//AvatarScrolls
			typeof( AvatarHeavenlyLightScroll),typeof(AvatarHeavensGateScroll ),typeof( AvatarMarkOfGodsScroll),*/

			//BardScrolls
			typeof( BardArmysPaeonScroll),typeof( BardEnchantingEtudeScroll),typeof( BardEnergyCarolScroll),typeof(BardEnergyThrenodyScroll ),typeof( BardFireCarolScroll),typeof( BardFireThrenodyScroll),
			typeof(BardFoeRequiemScroll ),typeof(BardIceCarolScroll ),typeof(BardIceThrenodyScroll ),typeof(BardKnightsMinneScroll),typeof(BardMagesBalladScroll ),typeof(BardMagicFinaleScroll ),typeof(BardPoisonCarolScroll ),typeof(BardPoisonThrenodyScroll ),typeof(BardSheepfoeMamboScroll ),typeof(BardSinewyEtudeScroll ),

			//ClericScrolls
			typeof( ClericAngelicFaithScroll),typeof(ClericBanishEvilScroll ),typeof( ClericDampenSpiritScroll),typeof( ClericDivineFocusScroll),
			typeof(ClericHammerOfFaithScroll ),typeof(ClericPurgeScroll ),typeof(ClericRestorationScroll ),typeof(ClericSacredBoonScroll ),typeof(ClericSacrificeScroll ),typeof(ClericSmiteScroll ),typeof(ClericTouchOfLifeScroll ),typeof(ClericTrialByFireScroll ),

			//DruidScrolls
			typeof( DruidBlendWithForestScroll),typeof(DruidEnchantedGroveScroll ),typeof(DruidFamiliarScroll),typeof(DruidGraspingRootsScroll ),typeof(DruidHollowReedScroll ),typeof(DruidLureStoneScroll ),
			typeof(DruidMushroomGatewayScroll ),typeof(DruidNaturesPassageScroll ),typeof( DruidPackOfBeastScroll),typeof(DruidRestorativeSoilScroll),typeof(DruidShieldOfEarthScroll ),typeof( DruidSpringOfLifeScroll),typeof(DruidStoneCircleScroll ),typeof(DruidSwarmOfInsectsScroll ),typeof(DruidLeafWhirlwindScroll ),typeof(DruidVolcanicEruptionScroll),

			//RangerScrolls
			typeof( RangerFireBowScroll),typeof(RangerPhoenixFlightScroll ),typeof(RangerHuntersAimScroll ),typeof( RangerIceBowScroll),
			typeof(RangerLightningBowScroll ),typeof(RangerFamiliarScroll ),typeof(RangerNoxBowScroll ),typeof(RangerSummonMountScroll )

			//Others

		};
		public static Type[] Artifacts{ get{ return ArtifactTypes; } }

		public static Item RandomArtifact()
		{
			index = Utility.Random( ArtifactTypes.Length );
			type = ArtifactTypes[index];
			return Activator.CreateInstance( type )as Item;
		}
	}
}

//typeof(  ),
//typeof(  ),
//typeof(  ),
//typeof(  ),
//typeof(  ),
//typeof(  ),
