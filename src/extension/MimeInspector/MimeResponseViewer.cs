using System;
using System.Windows.Forms;
using Fiddler;

namespace MimeInspector
{
    public class MimeResponseViewer : Inspector2, IResponseInspector2
    {
        private MimeView _mimeView;
        private HTTPResponseHeaders _headers;

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
                    try
                    {
                        _mimeView.LoadBody(value, _headers);
                    }
                    catch (Exception)
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

        public HTTPResponseHeaders headers
        {
            get
            {
                return null;
            }

            set
            {
                _headers = value;
            }
        }

        public MimeResponseViewer()
        {
            _mimeView = new MimeView();
        }

        public override void AddToTab(TabPage o)
        {
            _mimeView = new MimeView();
            o.Text = "MIME";
            o.Controls.Add(_mimeView);
            _mimeView.Dock = DockStyle.Fill;
        }

        public void Clear()
        {
        }

        public override int GetOrder()
        {
            return 0;
        }
    }
}
