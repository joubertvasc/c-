using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using JVUtils;

namespace JVUtils.Forms
{
    public partial class NewOpenDialog : Form
    {
        #region Internal Variables
        string _initialDir = @"\";
        string _fileName = "";
        string _initialFileName = "";
        string _fileMask = "*.*";
        TreeNode selectedTreeNode = null;
        MenuItem miPlaySound = null;
        MenuItem miOk2 = null;
        MenuItem miCancel2 = null;
        ContextMenu cmOpenDialog = null;
        #endregion

        public NewOpenDialog()
        {
            InitializeComponent();

            if (Utils.IsTouchScreen())
            {
                cmOpenDialog = new ContextMenu();

                miPlaySound = new MenuItem();
                miPlaySound.Text = "Play Sound";
                miPlaySound.Click += new EventHandler(miPlaySoundClick);
                cmOpenDialog.MenuItems.Add(miPlaySound);

                MenuItem menuItem2 = new MenuItem();
                menuItem2.Text = "-";
                cmOpenDialog.MenuItems.Add(menuItem2);

                miOk2 = new MenuItem();
                miOk2.Text = miOk.Text;
                miOk2.Click += new EventHandler(miOk_Click);
                cmOpenDialog.MenuItems.Add(miOk2);

                miCancel2 = new MenuItem();
                miCancel2.Text = miCancel.Text;
                miCancel2.Click += new EventHandler(miCancel_Click);
                cmOpenDialog.MenuItems.Add(miCancel2);

                tvFolders.ContextMenu = cmOpenDialog;
            }
        }

        void miPlaySoundClick(object sender, EventArgs e)
        {
            if (_fileName != "")
            {
                Kernel.PlayFile(_fileName, false);
            }
        }

        // Set the caption of the window
        public string SetText
        {
            get { return Text; }
            set { Text = value; }
        }

        // Set the inicial directory
        public string InitialDir
        {
            get { return _initialDir; }
            set { _initialDir = value; }
        }

        // Get the selected file
        public string FileName
        {
            get { return _fileName; }
        }

        public string InitialFileName
        {
            get { return _initialFileName; }
            set { _initialFileName = value; }
        }

        // Get or Set the file mask
        public string FileMask
        {
            get { return _fileMask; }
            set { _fileMask = value; }
        }

        // Get or Set caption for Ok option
        public string ItemOKCaption
        {
            get { return miOk.Text; }
            set { 
                miOk.Text = value;

                if (miOk2 != null)
                    miOk2.Text = value; 
            }
        }

        // Get or Set caption for Ok option
        public string ItemCancelCaption
        {
            get { return miCancel.Text; }
            set
            {
                miCancel.Text = value;

                if (miCancel2 != null)
                    miCancel2.Text = value;
            }
        }
        
        // Get or Set caption for Play Sound option
        public string ItemPlaySoundCaption
        {
            get { return (miPlaySound != null ? miPlaySound.Text : ""); }
            set 
            { 
                if (miPlaySound != null)
                    miPlaySound.Text = value; 
            }
        }

        private void miCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void miOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void getFiles(DirectoryInfo dri, TreeNode node)
        {
            if (dri != null && node != null)
            {
                try
                {
                    DirectoryInfo[] dInfo = dri.GetDirectories();
                    if (dInfo != null)
                    {
                        if (dInfo.Length > 0)
                        {
                            TreeNode treeNode = new TreeNode();
                            foreach (DirectoryInfo driSub in dInfo)
                            {
                                treeNode = node.Nodes.Add(driSub.Name);
                                getFiles(driSub, treeNode);
                            }
                        }

                        FileInfo[] subfileInfo = dri.GetFiles(_fileMask);
                        if (subfileInfo.Length > 0)
                        {
                            for (int j = 0; j < subfileInfo.Length; j++)
                            {
                                node.Nodes.Add(subfileInfo[j].Name);
                                node.Nodes[node.Nodes.Count - 1].ImageIndex = 2;

                                if (subfileInfo[j].FullName.ToLower().Trim().Equals(_initialFileName.ToLower().Trim()))
                                    selectedTreeNode = node.Nodes[node.Nodes.Count - 1];
                            }
                        }
                    }
                }
                catch { }
            }
        }

        private void NewOpenDialog_Activated(object sender, EventArgs e)
        {
            Utils.ShowWaitCursor();
            try
            {
                tvFolders.Nodes.Clear();
                TreeNode node = new TreeNode();
                if (Directory.Exists(@"\"))
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(@"\");

                    if (dirInfo != null)
                    {
                        DirectoryInfo[] subdirInfo = dirInfo.GetDirectories();
                        //                Array.Sort(subdirInfo);

                        if (subdirInfo != null)
                        {
                            if (subdirInfo.Length > 0)
                            {
                                foreach (DirectoryInfo dri in subdirInfo)
                                {
                                    node = tvFolders.Nodes.Add(dri.Name);
                                    getFiles(dri, node);
                                }
                            }

                            FileInfo[] fileInfo = dirInfo.GetFiles(_fileMask);
                            if (fileInfo.Length > 0)
                            {
                                for (int k = 0; k < fileInfo.Length; k++)
                                {
                                    tvFolders.Nodes.Add(fileInfo[k].Name);
                                    tvFolders.Nodes[tvFolders.Nodes.Count - 1].ImageIndex = 2;

                                    if (fileInfo[k].FullName.ToLower().Trim().Equals(_initialFileName.ToLower().Trim()))
                                        selectedTreeNode = tvFolders.Nodes[tvFolders.Nodes.Count - 1];
                                }
                            }
                        }
                    }
                }

                if (selectedTreeNode != null)
                    tvFolders.SelectedNode = selectedTreeNode;
            }
            finally
            {
                Utils.HideWaitCursor();
            }
        }

        public string FixPath(TreeNode treeNode)
        {
            string setReturn = "";
            try
            {
                setReturn = treeNode.FullPath;
                int index = setReturn.IndexOf("\\\\");
                if (index > 1)
                {
                    setReturn = treeNode.FullPath.Remove(index, 1);
                }
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
            }
            return setReturn;
        }

        private void tvFolders_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.ImageIndex == 2)
            {
                tvFolders.SelectedImageIndex = 2;
                _fileName = @"\" + FixPath(e.Node).ToString();
            }
            else
            {
                tvFolders.SelectedImageIndex = 1;
                _fileName = "";
            }

            miOk.Enabled = _fileName != "";

            if (miOk2 != null)
            {
                miOk2.Enabled = miOk.Enabled;
                miPlaySound.Enabled = miOk.Enabled;
            }
        }

        private void NewOpenDialog_Closing(object sender, CancelEventArgs e)
        {
            if (_fileName == "")
            {
                DialogResult = DialogResult.Cancel;
            }
        }
    }
}