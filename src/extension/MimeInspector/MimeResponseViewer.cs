using System.Threading;
using System.Windows.Forms;
using Fiddler;
using MimeInspector.Utilities;

namespace MimeInspector
{
    public class MimeResponseViewer : Inspector2, IResponseInspector2
    {
        private readonly MimeView _mimeView;

        public bool bDirty => false;

        public byte[] body
        {
            get
            {
                return null;
            }

            set
            {
                if (value != null && value.Length > 0)
                {
                    var mimeMessage = MimeParser.ParseMessage(value, headers, CancellationToken.None);

                    if (mimeMessage != null)
                    {
                        _mimeView.LoadMimeMessage(mimeMessage);
                    }
                    else
                    {
                        Clear();
                    }
                }
                else
                {
                    Clear();
                }
            }
        }

        public bool bReadOnly
        {
            get
            {
                return true;
            }

            set
            {
            }
        }

        public HTTPResponseHeaders headers { get; set; }

        public MimeResponseViewer()
        {
            _mimeView = new MimeView();
        }

        public override void AddToTab(TabPage o)
        {
            o.Text = "MIME";
            o.Controls.Add(_mimeView);
            _mimeView.Dock = DockStyle.Fill;
        }

        public void Clear()
        {
            _mimeView.Clear();
        }

        public override int GetOrder()
        {
            return 0;
        }
    }
}
