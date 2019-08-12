using System;

namespace Server.Factions
{
    public class TrueBritannians : Faction
    {
        private static Faction m_Instance;
        public TrueBritannians()
        {
            m_Instance = this;

            this.Definition =
                new FactionDefinition(
                    2,
                    1254, // dark purple
                    2125, // gold
                    2214, // join stone : gold
                    2125, // broadcast : gold
                    0x76, 0x3EB2, // war horse
                    "Cristianismo", "Cristi", "Crt",
                    new TextDefinition( "Cristianismo"),
                    new TextDefinition( "Faccion Cristiana"),
                    new TextDefinition( "<center>Cristianismo</center>"),
                    new TextDefinition(
                                               "- ``El secreto de andar con Jesús es tener la certeza de que yo no sé, pero él sí``" +
                                               "El pensamiento por sobre el origen de todas las cosas en el humano ," +
                                               "existe de por si, a similar o probablemente misma cantidad de tiempo de su subsistencia." +
                                               "El principio de los conocimientos  católicos remite en los italicos,  " +
                                                "quienes afirmaban que el hombre es una entidad exiliada de la verdad   " +
                                                 "y que debe vivir con la intención de volver a ella.  " +
                                                  " El cristianismo considera la búsqueda y perseguimiento de esta verdad mediante la fe " +
                                                   "  y reconoce como modelo más cercano a las enseñanzas de Cristo y la Biblia, " +
                                                    " , como consecuencia, cualquier ofensa hacia sus creencias, es en principio" +
                                                     "insultar y agredir a la única y absoluta verdad existente, objeto que un cristiano no suele ignorar.  " +
                                                     "insultar y agredir a la única y absoluta verdad existente, objeto que un cristiano no suele ignorar.  " +
                                               " Según el cristianismo un hombre debe vivir para servir a Dios, o no servirá para vivir."),
                    new TextDefinition( "Este area esta controlada por la religion Cristiana"),
                    new TextDefinition( "Esto esta corrupto para los Cristianos"),
                    new TextDefinition( "Piedra de Bautizo Cristiano"),
                    new TextDefinition( "Piedra del Cristianos"),
                    new TextDefinition( ": Cristiano"),
                    new TextDefinition( "Los cristianos ahora son ignorados."),
                    new TextDefinition( "Los cristianos ahora serán advertidos de su inminente fatalidad."),
                    new TextDefinition( "Los cristianos ahora seran atacados nada mas verlos."),
                    new StrongholdDefinition(
                        new Rectangle2D[]
                        {
                            new Rectangle2D(1292, 1556, 25, 25),
                            new Rectangle2D(1292, 1676, 120, 25),
                            new Rectangle2D(1388, 1556, 25, 25),
                            new Rectangle2D(1317, 1563, 71, 18),
                            new Rectangle2D(1300, 1581, 105, 95),
                            new Rectangle2D(1405, 1612, 12, 21),
                            new Rectangle2D(1405, 1633, 11, 5)
                        },
                        new Point3D(1909, 1568, 5),
                        new Point3D(1910, 1568, 5),
                        new Point3D[]
                        {
                            new Point3D(1911, 1568, 5),
                            new Point3D(1912, 1568, 5),
                            new Point3D(1913, 1568, 5),
                            new Point3D(1914, 1568, 5),
                            new Point3D(1915, 1568, 5),
                            new Point3D(1916, 1568, 5),
                            new Point3D(1917, 1567, 5),
                            new Point3D(1917, 1566, 5)
                        }),
                    new RankDefinition[]
                    {
                        new RankDefinition(10, 991, 8, new TextDefinition( "Arcispestre")),
                        new RankDefinition(9, 950, 7, new TextDefinition( "Arzobispo")),
                        new RankDefinition(8, 900, 6, new TextDefinition( "Obispo")),
                        new RankDefinition(7, 800, 6, new TextDefinition( "Abad")),
                        new RankDefinition(6, 700, 5, new TextDefinition( "Deán")),
                        new RankDefinition(5, 600, 5, new TextDefinition( "Diácono")),
                        new RankDefinition(4, 500, 5, new TextDefinition( "Capellan")),
                        new RankDefinition(3, 400, 4, new TextDefinition( "Clérigo")),
                        new RankDefinition(2, 200, 4, new TextDefinition( "Cardenal")),
                        new RankDefinition(1, 0, 4, new TextDefinition( "Prior"))
                    },
                    new GuardDefinition[]
                    {
                        new GuardDefinition(typeof(FactionHenchman), 0x1403, 5000, 1000, 10, new TextDefinition(1011526, "HENCHMAN"), new TextDefinition(1011510, "Hire Henchman")),
                        new GuardDefinition(typeof(FactionMercenary),	0x0F62, 6000, 2000, 10, new TextDefinition(1011527, "MERCENARY"), new TextDefinition(1011511, "Hire Mercenary")),
                        new GuardDefinition(typeof(FactionKnight), 0x0F4D, 7000, 3000, 10, new TextDefinition(1011528, "KNIGHT"), new TextDefinition(1011497, "Hire Knight")),
                        new GuardDefinition(typeof(FactionPaladin), 0x143F, 8000, 4000, 10, new TextDefinition(1011529, "PALADIN"), new TextDefinition(1011498, "Hire Paladin")),
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