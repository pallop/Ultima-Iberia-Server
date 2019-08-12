using System;

namespace Server.Factions
{
    public class Magincia : Town
    {
        public Magincia()
        {
            this.Definition =
                new TownDefinition(
                    7,
                    0x1870,
                    "Lisboa",
                    "Lisboa",
                    new TextDefinition( "Lisboa"),
                    new TextDefinition("Piedra de ciudad de Lisboa"),
                    new TextDefinition( "Monumento de la ciudad de Lisboa"),
                    new TextDefinition( "Monumento de la faccion de  Lisboa"),
                    new TextDefinition("Piedra de la faccion en Lisboa"),
                    new TextDefinition("Faction Town Sigil of Lisboa"),
                    new TextDefinition( "Corrupted Faction Town Sigil of Lisboa"),
                    new Point3D(676, 2065, 0),
                    new Point3D(677, 2065, 0));
        }
    }
}