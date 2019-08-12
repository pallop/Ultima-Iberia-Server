using System;

namespace Server.Factions
{
    public class Yew : Town
    {
        public Yew()
        {
           this.Definition =
                new TownDefinition(
                    4,
                    0x186D,
                    "Fuerte Cuzcurrita",
                    "Fuerte Cuzcurrita",
                    new TextDefinition( "Fuerte Cuzcurrita"),
                    new TextDefinition("Piedra de ciudad de Fuerte Cuzcurrita"),
                    new TextDefinition( "Monumento de la ciudad de Fuerte Cuzcurrita"),
                    new TextDefinition( "Monumento de la faccion de  Fuerte Cuzcurrita"),
                    new TextDefinition("Piedra de la faccion en Fuerte Cuzcurrita"),
                    new TextDefinition("Faction Town Sigil of Fuerte Cuzcurrita"),
                    new TextDefinition( "Corrupted Faction Town Sigil of Fuerte Cuzcurrita"),
                    new Point3D(1617, 2511, 6),
                    new Point3D(1618, 2511, 6));
        }
    }
}