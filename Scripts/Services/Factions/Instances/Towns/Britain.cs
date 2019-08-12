using System;

namespace Server.Factions
{
    public class Britain : Town
    {
        public Britain()
        {
            this.Definition =
                new TownDefinition(
                    0,
                    0x1869,
                    "Navarra",
                    "Navarra",
                    new TextDefinition( "Navarra"),
                    new TextDefinition("Piedra de ciudad de Navarra"),
                    new TextDefinition( "Monumento de la ciudad de Navarra"),
                    new TextDefinition( "Monumento de la faccion de  Navarra"),
                    new TextDefinition("Piedra de la faccion en Navarra"),
                    new TextDefinition("Faction Town Sigil of Navarra"),
                    new TextDefinition( "Corrupted Faction Town Sigil of Navarra"),
                    new Point3D(2408, 963, 12),
                    new Point3D(2409, 963, 12));
        }
    }
}