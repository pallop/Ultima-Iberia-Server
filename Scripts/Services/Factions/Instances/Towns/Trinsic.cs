using System;

namespace Server.Factions
{
    public class Trinsic : Town
    {
        public Trinsic()
        {
            this.Definition =
                new TownDefinition(
                    1,
                    0x186A,
                     "Palma",
                    "Palma",
                   new TextDefinition( "Palma"),
                    new TextDefinition("Piedra de ciudad de Palma"),
                    new TextDefinition( "Monumento de la ciudad de Palma"),
                    new TextDefinition( "Monumento de la faccion de  Palma"),
                    new TextDefinition("Piedra de la faccion en Palma"),
                    new TextDefinition("Faction Town Sigil of Palma"),
                    new TextDefinition( "Corrupted Faction Town Sigil of Palma"),
                    new Point3D(4245, 2297, 0),
                    new Point3D(4246, 2297, 0));
        }
    }
}