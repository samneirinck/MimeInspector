using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Fiddler;
using MimeInspector.Compression;
using MimeInspector.Utilities;
using MimeKit;
using Standard;

namespace MimeInspector
{
    public partial class MimeView : UserControl
    {
        private MimeMessage _mimeMessage;

        private readonly RawRequest _rawRequest;
        private readonly XMLRequestViewer _xmlRequest;
        private readonly RequestHeaderView _headersRequest;

        public MimeView()
        {
            InitializeComponent();

            _rawRequest = new RawRequest();
            _rawRequest.AddToTab(rawTab);
            _xmlRequest = new XMLRequestViewer();
            _xmlRequest.AddToTab(xmlTab);
            _headersRequest = new RequestHeaderView();
            _headersRequest.AddToTab(headersTab);
        }

        public void LoadMimeMessage(MimeMessage mimeMessage)
        {
            _mimeMessage = mimeMessage;
            FillTreeView();
        }

        public void Clear()
        {
            _mimeMessage = null;
            _rawRequest.body = new byte[] { };
            _xmlRequest.body = new byte[] { };
            _headersRequest.body = new byte[] { };

            messageTree.Nodes.Clear();
        }

        private void FillTreeView()
        {
            var iter = new MimeIterator(_mimeMessage);

            messageTree.BeginUpdate();

            try
            {
                messageTree.Nodes.Clear();

                TreeNode rootNode = null;

                while (iter.MoveNext())
                {
                    TreeNode createdNode = null;

                    if (rootNode == null)
                    {
                        rootNode = messageTree.Nodes.Add(iter.Current.ContentId ?? "body");
                        createdNode = rootNode;
                    }
                    else
                    {
                        createdNode = rootNode.Nodes.Add(iter.Current.ContentId ?? "part");
                    }
                    
                    createdNode.Tag = iter.Current;
                    createdNode.ContextMenu = new ContextMenu();

                    var menuItem = createdNode.ContextMenu.MenuItems.Add("Save to file...");
                    menuItem.Tag = iter.Current;

                    menuItem.Click += (s, e) =>
                    {
                        var dialog = new SaveFileDialog();
                        if (dialog.ShowDialog() == DialogResult.OK)
                        {
                            using (FileStream fs = new FileStream(dialog.FileName, FileMode.OpenOrCreate))
                            {
                                if (((MenuItem)s).Tag is MimePart part)
                                {
                                    part.ContentObject.WriteTo(fs);
                                }

                                fs.Flush();
                            }
                            if (MessageBox.Show("Open now?", "Open attachment", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                Process.Start(dialog.FileName);
                            }
                        }
                    };
                }

                if (rootNode != null)
                {
                    rootNode.Expand();

                    messageTree.SelectedNode = rootNode;
                    rootNode.EnsureVisible();
                }

            }
            finally
            {
                messageTree.EndUpdate();
            }
        }

        private void OnNodeSelected(object sender, TreeViewEventArgs e)
        {
            var entity = e.Node.Tag as MimeEntity;
            var multipart = entity as Multipart;
            var part = entity as MimePart;

            byte[] body = null;

            if (multipart != null)
            {
                body = Encoding.UTF8.GetBytes(multipart.Preamble);
            }

            if (part != null)
            {
                Stream stream = part.ContentObject.Open();

                if (string.Equals(part.ContentType?.MimeType, "application/gzip"))
                {
                    stream = GZipCompressor.Decompress(stream);
                }

                body = StreamUtilities.ReadFully(stream);
            }

            var httpHeaders = new HTTPRequestHeaders();

            if (entity != null)
            {
                foreach (var header in entity.Headers)
                {
                    httpHeaders.Add(header.Field, header.Value);
                }
            }
            _headersRequest.headers = httpHeaders;

            _rawRequest.body = body;
            _xmlRequest.body = body;
        }
    }
}
