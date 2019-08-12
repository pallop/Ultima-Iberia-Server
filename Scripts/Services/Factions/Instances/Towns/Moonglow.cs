using System;

namespace Server.Factions
{
    public class Moonglow : Town
    {
        public Moonglow()
        {
            this.Definition =
                new TownDefinition(
                    3,
                    0x186C,
                      "Granada",
                    "Granada",
                    new TextDefinition( "Granada"),
                    new TextDefinition("Piedra de ciudad de Granada"),
                    new TextDefinition( "Monumento de la ciudad de Granada"),
                    new TextDefinition( "Monumento de la faccion de  Granada"),
                    new TextDefinition("Piedra de la faccion en Granada"),
                    new TextDefinition("Faction Town Sigil of Granada"),
                    new TextDefinition( "Corrupted Faction Town Sigil of Granada"),
                    new Point3D(2602, 2907, 12),
                    new Point3D(2603, 2906, 12));
        }
    }
}