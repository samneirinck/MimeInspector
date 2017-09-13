using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Fiddler;
using MimeInspector.Utilities;

namespace MimeInspector
{
    public class MimeRequestViewer : Inspector2, IRequestInspector2
    {
        private MimeView _mimeView;

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
                    var mimeMessage = AsyncMimeParser.ParseMessage(value, headers, CancellationToken.None);

                    if (mimeMessage != null)
                    {
                        _mimeView.LoadMimeMessage(mimeMessage);                        
                    }
                    else
                    {
                        Clear();
                    }

                    ////try
                    ////{
                    ////    _mimeView.LoadBody(value, _headers);
                    ////}
                    ////catch (Exception)
                    ////{
                    ////    Clear();
                    ////}
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

        public HTTPRequestHeaders headers { get; set; }

        public MimeRequestViewer()
        {
            _mimeView = new MimeView();
        }

        public void Clear()
        {
            _mimeView.Clear();            
        }

        public override void AddToTab(TabPage o)
        {
            _mimeView = new MimeView();
            o.Text = "MIME";
            o.Controls.Add(_mimeView);
            _mimeView.Dock = DockStyle.Fill;
        }

        public override int GetOrder()
        {
            return 0;
        }
    }
}
