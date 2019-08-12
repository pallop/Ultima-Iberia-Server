using System;

namespace Server.Factions
{
    public class CouncilOfMages : Faction
    {
        private static Faction m_Instance;
        public CouncilOfMages()
        {
            m_Instance = this;

            this.Definition =
                new FactionDefinition(
                    1,
                    1325, // blue
                    1310, // bluish white
                    1325, // join stone : blue
                    1325, // broadcast : blue
                    0x77, 0x3EB1, // war horse
                    "Paganismo", "Pagan", "Pgn",
                    new TextDefinition( "Paganismo"),
                    new TextDefinition( "Faccion Pagana"),
                    new TextDefinition( "<center>Paganismo</center>"),
                    new TextDefinition(
                                               "``Los dioses eternamente bellos y siempre felices son quienes decidirán el final de los mortales`` " +
                                               "Los paganos (así llamados despectivamente por las religiones monoteistas), " +
                                               "son aquellos politeistas, que como bien el termino dice, creen en la existencia de varios dioses, " +
                                               " y que ellos son quienes controlan o deciden ante la simple realidad mortal. " +
                                               " Consideran los distintos talentos como dones divinos y el propósito que estos conllevan. " +
                                               " Algo que diferencia totalmente la religión politeista de la monoteista, " +
                                               " es que en mayor caso, o frecuentemente, se entiende que los dioses no son capaces de controlar la voluntad del destino, " +
                                               "por lo que este enigma, es el poder superior a todas las cosas."),
                   new TextDefinition( "Este area esta controlada por los Paganos"),
                    new TextDefinition( "Esto esta corrupto para los Paganos"),
                    new TextDefinition( "Piedra de ``Bautizo`` Pagano"),
                    new TextDefinition( "Piedra del Paganismo"),
                    new TextDefinition( ": Pagano"),
                    new TextDefinition( "Los Paganos ahora son ignorados."),
                    new TextDefinition( "Los Paganos ahora serán advertidos de su inminente fatalidad."),
                    new TextDefinition( "Los Paganos ahora seran atacados nada mas verlos."),
                    new StrongholdDefinition(
                        new Rectangle2D[]
                        {
                            new Rectangle2D(3756, 2232, 4, 23),
                            new Rectangle2D(3760, 2227, 60, 28),
                            new Rectangle2D(3782, 2219, 18, 8),
                            new Rectangle2D(3778, 2255, 35, 17)
                        },
                        new Point3D(4247, 2297, 0),
                        new Point3D(4248, 2297, 0),
                        new Point3D[]
                        {
                            new Point3D(4249, 2297, 0),
                            new Point3D(4250, 2297, 0),
                            new Point3D(4251, 2297, 0),
                            new Point3D(4252, 2297, 0),
                            new Point3D(4253, 2297, 0),
                            new Point3D(4254, 2297, 0),
                            new Point3D(4255, 2297, 0),
                            new Point3D(4256, 2297, 0)
                        }),
                    new RankDefinition[]
                    {
                        new RankDefinition(10, 991, 8, new TextDefinition( "Chaman Padre")),
                        new RankDefinition(9, 950, 7, new TextDefinition( "Avatar Runico")),
                        new RankDefinition(8, 900, 6, new TextDefinition( "Avatar de Fuego")),
                        new RankDefinition(7, 800, 6, new TextDefinition( "Avatar de Hielo")),
                        new RankDefinition(6, 700, 5, new TextDefinition( "Avatar de Agua")),
                        new RankDefinition(5, 600, 5, new TextDefinition( "Avatar de Arena")),
                        new RankDefinition(4, 500, 5, new TextDefinition( "Avatar de Madera")),
                        new RankDefinition(3, 400, 4, new TextDefinition( "Avatar de Viento")),
                        new RankDefinition(2, 200, 4, new TextDefinition( "Invocador maestro")),
                        new RankDefinition(1, 0, 4, new TextDefinition( "Invocador aprendiz"))
                    },
                    new GuardDefinition[]
                    {
                        new GuardDefinition(typeof(FactionHenchman), 0x1403, 5000, 1000, 10, new TextDefinition(1011526, "HENCHMAN"), new TextDefinition(1011510, "Hire Henchman")),
                        new GuardDefinition(typeof(FactionMercenary),	0x0F62, 6000, 2000, 10, new TextDefinition(1011527, "MERCENARY"), new TextDefinition(1011511, "Hire Mercenary")),
                        new GuardDefinition(typeof(FactionSorceress),	0x0E89, 7000, 3000, 10, new TextDefinition(1011507, "SORCERESS"), new TextDefinition(1011501, "Hire Sorceress")),
                        new GuardDefinition(typeof(FactionWizard), 0x13F8, 8000, 4000, 10, new TextDefinition(1011508, "ELDER WIZARD"),	new TextDefinition(1011502, "Hire Elder Wizard")),
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