using System;

namespace Server.Factions
{
    public class Minax : Faction
    {
        private static Faction m_Instance;
        public Minax()
        {
            m_Instance = this;

            this.Definition =
                new FactionDefinition(
                    0,
                    1645, // dark red
                    1109, // shadow
                    1645, // join stone : dark red
                    1645, // broadcast : dark red
                    0x78, 0x3EAF, // war horse
                    "Judaismo", "Judais", "Jd",
                    new TextDefinition( "Judaismo"),
                    new TextDefinition( "Faccion Judia"),
                    new TextDefinition( "<center>Judaismo</center>"),
                    new TextDefinition(
                                               "De las tres religiones monoteistas, el Judaísmo es la más antigua, " +
                                               " el rango principal de la fe judía es la creencia de un Dios omnisciente, omnipotente y providente, creador del universo " +
                                               "y elector del pueblo judío como portador de su verdad, la ley de los Diez mandamientos; " +
                                               "La practica judía se basa en la enseñanzas del Torá, compuesto por cinco libros. " +
                                               
                                               "No aceptan a Jesús como profeta, por lo que no siguen las enseñanzas del nuevo testamento"),
                    new TextDefinition( "Este area esta controlada por la religion Judia"),
                    new TextDefinition( "Esto esta corrupto para los Judios"),
                    new TextDefinition( "Piedra de Bautizo Judio"),
                    new TextDefinition( "Piedra del Judaismo"),
                    new TextDefinition( ": Judio"),
                    new TextDefinition( "Los Judios ahora son ignorados."),
                    new TextDefinition( "Los Judios ahora serán advertidos de su inminente fatalidad."),
                    new TextDefinition( "Los Judios ahora seran atacados nada mas verlos."),
                    new StrongholdDefinition(
                        new Rectangle2D[]
                        {
                            new Rectangle2D(1097, 2570, 70, 50)
                        },
                        new Point3D(3641, 2108, 2),
                        new Point3D(3642, 2108, 2),
                        new Point3D[]
                        {
                            new Point3D(3643, 2108, 2),
                            new Point3D(3644, 2108, 2),
                            new Point3D(3645, 2108, 2),
                            new Point3D(3646, 2108, 2),
                            new Point3D(3647, 2108, 2),
                            new Point3D(3648, 2108, 2),
                            new Point3D(3649, 2108, 2),
                            new Point3D(3650, 2108, 2)
                        }),
                    new RankDefinition[]
                    {
                        new RankDefinition(10, 991, 8, new TextDefinition( "Demiurgo")),
                        new RankDefinition(9, 950, 7, new TextDefinition( "Sanedrín")),
                        new RankDefinition(8, 900, 6, new TextDefinition( "Azkenazíez")),
                        new RankDefinition(7, 800, 6, new TextDefinition( "Sefardita")),
                        new RankDefinition(6, 700, 5, new TextDefinition( "Mestizo")),
                        new RankDefinition(5, 600, 5, new TextDefinition( "Converso")),
                        new RankDefinition(4, 500, 5, new TextDefinition( "Mason")),
                        new RankDefinition(3, 400, 4, new TextDefinition( "Servidor")),
                        new RankDefinition(2, 200, 4, new TextDefinition( "Ario")),
                        new RankDefinition(1, 0, 4, new TextDefinition( "Ario"))
                    },
                    new GuardDefinition[]
                    {
                        new GuardDefinition(typeof(FactionHenchman), 0x1403, 5000, 1000, 10, new TextDefinition(1011526, "HENCHMAN"), new TextDefinition(1011510, "Hire Henchman")),
                        new GuardDefinition(typeof(FactionMercenary),	0x0F62, 6000, 2000, 10, new TextDefinition(1011527, "MERCENARY"), new TextDefinition(1011511, "Hire Mercenary")),
                        new GuardDefinition(typeof(FactionBerserker),	0x0F4B, 7000, 3000, 10, new TextDefinition(1011505, "BERSERKER"), new TextDefinition(1011499, "Hire Berserker")),
                        new GuardDefinition(typeof(FactionDragoon), 0x1439, 8000, 4000, 10, new TextDefinition(1011506, "DRAGOON"), new TextDefinition(1011500, "Hire Dragoon")),
                    });
        }

        public static Faction Instance
        {
            get
            {
                return m_Instance;
            }
        }
    }
}