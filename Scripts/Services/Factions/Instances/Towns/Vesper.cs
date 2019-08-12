using System;

namespace Server.Factions
{
    public class Vesper : Town
    {
        public Vesper()
        {
            this.Definition =
                new TownDefinition(
                    5,
                    0x186E,
                     "Fuerte Rascafria",
                    "Fuerte Rascafria",
                    new TextDefinition( "Navarra"),
                    new TextDefinition("Piedra de ciudad de Fuerte Rascafria"),
                    new TextDefinition( "Monumento de la ciudad de Fuerte Rascafria"),
                    new TextDefinition( "Monumento de la faccion de  Fuerte Rascafria"),
                    new TextDefinition("Piedra de la faccion en Fuerte Rascafria"),
                    new TextDefinition("Faction Town Sigil of Fuerte Rascafria"),
                    new TextDefinition( "Corrupted Faction Town Sigil of Fuerte Rascafria"),
                    new Point3D(2546, 2568, 10),
                    new Point3D(2546, 2569, 10));
        }
    }
}