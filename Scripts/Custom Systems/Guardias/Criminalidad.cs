/*Este script requiere las modificaciones propias en el PlayerMobile:
 * los flags CrimiNavarra, CrimiSegovia, CrimiGranada, CrimiLisboa y CrimiValencia
 * deben aparecer definidos como valores booleanos, sino dara errores al ejecutar el script.
 * 
 * Para que ademas, el script valga para algo, hay que definir en el script de cada guardia
 * que ataque a los jugadores cuando la prop "Crimidesuciudad" este activada.
 * 
 */


using Server.Commands;
using System;
using System.Collections;
using Server;
using Server.Targeting;
using Server.Mobiles;


namespace Server.Commands
{
    public class CrimiNavarra
    {
        public static void Initialize()
        {
            CommandSystem.Register("CrimiNavarra", AccessLevel.GameMaster, new CommandEventHandler(CrimiNavarra_OnCommand));
        }
        public static void CrimiNavarra_OnCommand(CommandEventArgs e)
        {
            Mobile from = e.Mobile;

            e.GetInt32(0);

            if (from != null)
            {
                from.SendMessage("¿A quien deseas hacer enemigo de Navarra?");
               

                from.Target = new InternalTarget();
            }
        }
        private class InternalTarget : Target
        {


            public InternalTarget()
                : base(-1, true, TargetFlags.None)
            {

            }

            protected override void OnTarget(Mobile from, object targeted)
            {


                if (targeted is PlayerMobile)
                {
                    PlayerMobile pm = targeted as PlayerMobile;

                    if (pm.CrimiNavarra == false)
                    {
                        pm.CrimiNavarra = true;
                        pm.SendMessage("Quedas expulsado de Navarra");
                        from.SendMessage("Ahora los guardias de Navarra le atacaran");
                    }
                    else
                    {
                        pm.CrimiNavarra = false;
                        pm.SendMessage("La guardia de Navarra tiene orden de no atacarte mas");
                        from.SendMessage("La guardia de Navarra no le atacara mas");
                    }
                }
                else
                    from.SendMessage("Solo puedes hacer criminales a jugadores");
            }



        }
    }

    public class CrimiSegovia
    {
        public static void Initialize()
        {
            CommandSystem.Register("CrimiSegovia", AccessLevel.GameMaster, new CommandEventHandler(CrimiSegovia_OnCommand));
        }
        public static void CrimiSegovia_OnCommand(CommandEventArgs e)
        {
            Mobile from = e.Mobile;

            e.GetInt32(0);

            if (from != null)
            {
                from.SendMessage("¿A quien deseas hacer enemigo de Segovia?");


                from.Target = new InternalTarget();
            }
        }
        private class InternalTarget : Target
        {


            public InternalTarget()
                : base(-1, true, TargetFlags.None)
            {

            }

            protected override void OnTarget(Mobile from, object targeted)
            {


                if (targeted is PlayerMobile)
                {
                    PlayerMobile pm = targeted as PlayerMobile;

                    if (pm.CrimiSegovia == false)
                    {
                        pm.CrimiSegovia = true;
                        pm.SendMessage("Quedas expulsado de Segovia");
                        from.SendMessage("Ahora los guardias de Segovia le atacaran");
                    }
                    else
                    {
                        pm.CrimiSegovia = false;
                        pm.SendMessage("La guardia de Segovia tiene orden de no atacarte mas");
                        from.SendMessage("La guardia de Segovia no le atacara mas");
                    }
                }
                else
                    from.SendMessage("Solo puedes hacer criminales a jugadores");
            }



        }
    }

    public class CrimiValencia
    {
        public static void Initialize()
        {
            CommandSystem.Register("CrimiValencia", AccessLevel.GameMaster, new CommandEventHandler(CrimiValencia_OnCommand));
        }
        public static void CrimiValencia_OnCommand(CommandEventArgs e)
        {
            Mobile from = e.Mobile;

            e.GetInt32(0);

            if (from != null)
            {
                from.SendMessage("¿A quien deseas hacer enemigo de Valencia?");


                from.Target = new InternalTarget();
            }
        }
        private class InternalTarget : Target
        {


            public InternalTarget()
                : base(-1, true, TargetFlags.None)
            {

            }

            protected override void OnTarget(Mobile from, object targeted)
            {


                if (targeted is PlayerMobile)
                {
                    PlayerMobile pm = targeted as PlayerMobile;

                    if (pm.CrimiValencia == false)
                    {
                        pm.CrimiValencia = true;
                        pm.SendMessage("Quedas expulsado de Valencia");
                        from.SendMessage("Ahora los guardias de Valencia le atacaran");
                    }
                    else
                    {
                        pm.CrimiValencia = false;
                        pm.SendMessage("La guardia de Valencia tiene orden de no atacarte mas");
                        from.SendMessage("La guardia de Valencia no le atacara mas");
                    }
                }
                else
                    from.SendMessage("Solo puedes hacer criminales a jugadores");
            }



        }
    }

    public class CrimiLisboa
        {
            public static void Initialize()
            {
                CommandSystem.Register("CrimiLisboa", AccessLevel.GameMaster, new CommandEventHandler(CrimiLisboa_OnCommand));
            }
            public static void CrimiLisboa_OnCommand(CommandEventArgs e)
            {
                Mobile from = e.Mobile;

                e.GetInt32(0);

                if (from != null)
                {
                    from.SendMessage("¿A quien deseas hacer enemigo de Lisboa?");


                    from.Target = new InternalTarget();
                }
            }
            private class InternalTarget : Target
            {


                public InternalTarget()
                    : base(-1, true, TargetFlags.None)
                {

                }

                protected override void OnTarget(Mobile from, object targeted)
                {


                    if (targeted is PlayerMobile)
                    {
                        PlayerMobile pm = targeted as PlayerMobile;

                        if (pm.CrimiLisboa == false)
                        {
                            pm.CrimiLisboa = true;
                            pm.SendMessage("Quedas expulsado de Lisboa");
                            from.SendMessage("Ahora los guardias de Lisboa le atacaran");
                        }
                        else
                        {
                            pm.CrimiLisboa = false;
                            pm.SendMessage("La guardia de Lisboa tiene orden de no atacarte mas");
                            from.SendMessage("La guardia de Lisboa no le atacara mas");
                        }
                    }
                    else
                        from.SendMessage("Solo puedes hacer criminales a jugadores");
                }



            }
        }

    public class CrimiGranada
        {
            public static void Initialize()
            {
                CommandSystem.Register("CrimiGranada", AccessLevel.GameMaster, new CommandEventHandler(CrimiGranada_OnCommand));
            }
            public static void CrimiGranada_OnCommand(CommandEventArgs e)
            {
                Mobile from = e.Mobile;

                e.GetInt32(0);

                if (from != null)
                {
                    from.SendMessage("¿A quien deseas hacer enemigo de Granada?");


                    from.Target = new InternalTarget();
                }
            }
            private class InternalTarget : Target
            {


                public InternalTarget()
                    : base(-1, true, TargetFlags.None)
                {

                }

                protected override void OnTarget(Mobile from, object targeted)
                {


                    if (targeted is PlayerMobile)
                    {
                        PlayerMobile pm = targeted as PlayerMobile;

                        if (pm.CrimiGranada == false)
                        {
                            pm.CrimiGranada = true;
                            pm.SendMessage("Quedas expulsado de Granada");
                            from.SendMessage("Ahora los guardias de Granada le atacaran");
                        }
                        else
                        {
                            pm.CrimiGranada = false;
                            pm.SendMessage("La guardia de Granada tiene orden de no atacarte mas");
                            from.SendMessage("La guardia de Granada no le atacara mas");
                        }
                    }
                    else
                        from.SendMessage("Solo puedes hacer criminales a jugadores");
                }



            }
        }
    }




