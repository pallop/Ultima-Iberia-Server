using System;

namespace Server.Factions
{
    public class Shadowlords : Faction
    {
        private static Faction m_Instance;
        public Shadowlords()
        {
            m_Instance = this;

            this.Definition =
                new FactionDefinition(
                    3,
                    1109, // shadow
                    2211, // green
                    1109, // join stone : shadow
                    2211, // broadcast : green
                    0x79, 0x3EB0, // war horse
                    "Islamismo", "Islam", "IS",
                    new TextDefinition( "Islamismo"),
                    new TextDefinition( "Faccion Islamica"),
                    new TextDefinition( "<center>Islamismo</center>"),
                    new TextDefinition(
                                               "``La sumisión a Dios el Altísimo a través del monoteísmo, la obediencia y el abandono de la idolatría``" +
                                               "Creyentes del Islam, una religión monoteísta abrahámica cuyo dogma se basa en el Corán." +
                                               "Entienden a la deidad creadora y original por sobre todas las cosas como Alá  " +
                                               "y su fe se basa en el mandato de la misma y las enseñanzas del último profeta,Mahoma.  " +
                                               "Aceptan como profetas, aunque sin limitarse a  Adán, Noé, Abraham, Moisés, Salomón e Isa (Jesús).  " +
                                               "Su doctrina se basa en el Chiita, Sunni y Dios"),
                    new TextDefinition( "Este area esta controlada por la religion Islamica"),
                    new TextDefinition( "Esto esta corrupto para los Musulmanes"),
                    new TextDefinition( "Piedra de Bautizo Musulman"),
                    new TextDefinition( "Piedra del Islamista"),
                    new TextDefinition( ": Musulman"),
                    new TextDefinition( "Los Islamistas ahora son ignorados."),
                    new TextDefinition( "Los Islamistas ahora serán advertidos de su inminente fatalidad."),
                    new TextDefinition( "Los Islamistas ahora seran atacados nada mas verlos."),
                    new StrongholdDefinition(
                        new Rectangle2D[]
                        {
                            new Rectangle2D(960, 688, 8, 9),
                            new Rectangle2D(944, 697, 24, 23)
                        },
                       new Point3D(2615, 2906, 12),
                        new Point3D(2616, 2905, 12),
                        new Point3D[]
                        {
                            new Point3D(2615, 2906, 18),
                            new Point3D(2614, 2598, 18),
                            new Point3D(2613, 2595, 18),
                            new Point3D(2612, 2592, 18),
                            new Point3D(2611, 2601, 18),
                            new Point3D(2610, 2598, 18),
                            new Point3D(2609, 2595, 18),
                            new Point3D(2608, 2592, 18)
                        }),
                    new RankDefinition[]
                    {
                        new RankDefinition(10, 991, 8, new TextDefinition( "Califa")),
                        new RankDefinition(9, 950, 7, new TextDefinition( "Visir")),
                        new RankDefinition(8, 900, 6, new TextDefinition( "Valí")),
                        new RankDefinition(7, 800, 6, new TextDefinition( "Emir")),
                        new RankDefinition(6, 700, 5, new TextDefinition( "Cadies")),
                        new RankDefinition(5, 600, 5, new TextDefinition( "Ulema")),
                        new RankDefinition(4, 500, 5, new TextDefinition( "Bereber")),
                        new RankDefinition(3, 400, 4, new TextDefinition( "Muladies")),
                        new RankDefinition(2, 200, 4, new TextDefinition( "Muladies")),
                        new RankDefinition(1, 0, 4, new TextDefinition( "Sirviente"))
                    },
                    new GuardDefinition[]
                    {
                        new GuardDefinition(typeof(FactionHenchman), 0x1403, 5000, 1000, 10, new TextDefinition(1011526, "HENCHMAN"), new TextDefinition(1011510, "Hire Henchman")),
                        new GuardDefinition(typeof(FactionMercenary),	0x0F62, 6000, 2000, 10, new TextDefinition(1011527, "MERCENARY"), new TextDefinition(1011511, "Hire Mercenary")),
                        new GuardDefinition(typeof(FactionDeathKnight),	0x0F45, 7000, 3000, 10, new TextDefinition(1011512, "DEATH KNIGHT"),	new TextDefinition(1011503, "Hire Death Knight")),
                        new GuardDefinition(typeof(FactionNecromancer),	0x13F8, 8000, 4000, 10, new TextDefinition(1011513, "SHADOW MAGE"),	new TextDefinition(1011504, "Hire Shadow Mage")),
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