using System;

namespace Server.Factions
{
    public class SkaraBrae : Town
    {
        public SkaraBrae()
        {
            this.Definition =
                new TownDefinition(
                    6,
                    0x186F,
                    "Segovia",
                    "Segovia",
                    new TextDefinition( "Segovia"),
                    new TextDefinition("Piedra de ciudad de Segovia"),
                    new TextDefinition( "Monumento de la ciudad de Segovia"),
                    new TextDefinition( "Monumento de la faccion de  Segovia"),
                    new TextDefinition("Piedra de la faccion en Segovia"),
                    new TextDefinition("Faction Town Sigil of Segovia"),
                    new TextDefinition( "Corrupted Faction Town Sigil of Segovia"),
                    new Point3D(1955, 1531, 5),
                    new Point3D(1956, 1531, 5));
        }
    }
}