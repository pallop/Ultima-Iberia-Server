using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Server.Accounting;
using Server.Gumps;
using Server.Network;

namespace Server.Engines.Help
{
    public class PagePromptGump : Gump
    {
        private readonly Mobile _mFrom;
        private readonly PageType _mType;
        public PagePromptGump(Mobile from, PageType type)
            : base(0, 0)
        {
            _mFrom = from;
            _mType = type;

            from.CloseGump(typeof(PagePromptGump));

            AddBackground(50, 50, 540, 350, 2600);

            AddPage(0);

            AddHtmlLocalized(264, 80, 200, 24, 1062524, false, false); // Enter Description
            AddHtmlLocalized(120, 108, 420, 48, 1062638, false, false); // Please enter a brief description (up to 200 characters) of your problem:

            AddBackground(100, 148, 440, 200, 3500);
            AddTextEntry(120, 168, 400, 200, 1153, 0);

            AddButton(175, 355, 2074, 2075, 1, GumpButtonType.Reply, 0); // Okay
            AddButton(405, 355, 2073, 2072, 0, GumpButtonType.Reply, 0); // Cancel
        }

        public override void OnResponse(NetState sender, RelayInfo info)
        {
            if (info.ButtonID == 0)
            {
                _mFrom.SendLocalizedMessage(501235, "", 0x35); // Help request aborted.
            }
            else
            {
                var entry = info.GetTextEntry(0);
                var text = (entry == null ? "" : entry.Text.Trim());

                if (text.Length == 0)
                {
                    _mFrom.SendMessage(0x35, "You must enter a description.");
                    _mFrom.SendGump(new PagePromptGump(_mFrom, _mType));
                }
                else
                {
                    _mFrom.SendLocalizedMessage(501234, "", 0x35); /* The next available Counselor/Game Master will respond as soon as possible.
                    * Please check your Journal for messages every few minutes.
                    */

                    PageQueue.Enqueue(new PageEntry(_mFrom, text, _mType));
                    var mailServerArgs = new[]
                    {
                        "jotunheimruo@gmail.com",
                        "jotunheimruo@gmail.com",
                        text, // do not change
                        _mFrom.ToString(), // do not change
                        "jotunheimruo@gmail.com",
                        "ServUO2014"
                    };
                   Task.Factory.StartNew(() => Sendmail(mailServerArgs));
                }

            }
        }

        public static void Sendmail(string[] args)
        {
            foreach (var account in Accounts.GetAccounts())
            {
                var acct = (Account) account;
                if (acct.AccessLevel < AccessLevel.Counselor) continue;
                var address = acct.GetTag("email");
                if (address == null) continue;
                var client = new SmtpClient(args[0]);
                var mailFrom = new MailAddress(args[1],
                    "Shard " + (char) 0xD8 + " Pages",
                    Encoding.UTF8);
                var to = new MailAddress(address);
                var message = new MailMessage(mailFrom, to)
                {
                    Body = args[2],
                    BodyEncoding = Encoding.UTF8,
                    Subject = string.Format("New support request from: {0}", args[3]),
                    SubjectEncoding = Encoding.UTF8
                };
                client.Credentials = new NetworkCredential(args[4], args[5]);
                client.Send(message);
                message.Dispose();
            }

        }
    }

}