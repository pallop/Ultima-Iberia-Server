using System;

namespace Server.Factions
{
    public class Minoc : Town
    {
        public Minoc()
        {
            this.Definition =
                new TownDefinition(
                    2,
                    0x186B,
                    "Valencia",
                    "Valencia",
                    new TextDefinition( "Valencia"),
                    new TextDefinition("Piedra de ciudad de Valencia"),
                    new TextDefinition( "Monumento de la ciudad de Valencia"),
                    new TextDefinition( "Monumento de la faccion de  Valencia"),
                    new TextDefinition("Piedra de la faccion en Valencia"),
                    new TextDefinition("Faction Town Sigil of Valencia"),
                    new TextDefinition( "Corrupted Faction Town Sigil of Valencia"),
                    new Point3D(3634, 2108, 2),
                    new Point3D(3634, 2107, 2));
        }
    }
}