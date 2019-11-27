//
// Copyright (c) Microsoft Corporation.  All rights reserved.
//
//
// Use of this sample source code is subject to the terms of the Microsoft
// license agreement under which you licensed this sample source code. If
// you did not accept the terms of the license agreement, you are not
// authorized to use this sample source code. For the terms of the license,
// please see the license agreement between you and Microsoft or, if applicable,
// see the LICENSE.RTF on your install media or the root of your tools installation.
// THE SAMPLE SOURCE CODE IS PROVIDED "AS IS", WITH NO WARRANTIES OR INDEMNITIES.
//

// ***************************************************************************
// CECamera.cs
//
// Take pictures and videos using Camera managed API.
//

using System;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.Data;
using System.Globalization;
using Microsoft.WindowsMobile.Forms;
using Microsoft.WindowsMobile.Status;
using System.IO;

namespace Camera
{
    /// <summary>
    /// Main form for the CECamera application.
    /// </summary>
    public class CECamera : System.Windows.Forms.Form
    {
        // Default values for options
        private const string cecameraDefaultInitialDirectory = null;
        private const string cecameraDefaultPictureFileName  = "picture1";
        private const string cecameraDefaultPictureExtension = ".jpg";
        private const string cecameraDefaultVideoFileName    = "movie1";        
        private const string cecameraDefaultTitle            = "Title";
        private const string cecameraDefaultResolutionWidth  = "100";
        private const string cecameraDefaultResolutionHeight = "100";
        private const string cecameraDefaultVideoTimeLimit   = "25";

        private System.Windows.Forms.MainMenu mainMenu;
        private System.Windows.Forms.MenuItem menuStart;
        private System.Windows.Forms.MenuItem menuMenu;
        private System.Windows.Forms.MenuItem menuMode;
        private System.Windows.Forms.MenuItem menuModeStill;
        private System.Windows.Forms.MenuItem menuModeVideoOnly;
        private System.Windows.Forms.MenuItem menuModeVideoWithAudio;
        private System.Windows.Forms.MenuItem menuStillQuality;
        private System.Windows.Forms.MenuItem menuStillQualityDefault;
        private System.Windows.Forms.MenuItem menuStillQualityLow;
        private System.Windows.Forms.MenuItem menuStillQualityNormal;
        private System.Windows.Forms.MenuItem menuStillQualityHigh;
        private System.Windows.Forms.MenuItem menuVideoTypes;
        private System.Windows.Forms.MenuItem menuVideoTypesAll;
        private System.Windows.Forms.MenuItem menuVideoTypesStandard;
        private System.Windows.Forms.MenuItem menuVideoTypesMessaging;
        private System.Windows.Forms.MenuItem menuSeparator1;
        private System.Windows.Forms.MenuItem menuReset;
        private System.Windows.Forms.MenuItem menuAbout;
        private System.Windows.Forms.MenuItem menuSeparator2;
        private System.Windows.Forms.MenuItem menuExit;
        private System.Windows.Forms.CheckBox checkInitialDirectory;
        private System.Windows.Forms.TextBox textInitialDirectory;
        private System.Windows.Forms.CheckBox checkDefaultFileName;
        private System.Windows.Forms.TextBox textDefaultFileName;
        private System.Windows.Forms.CheckBox checkTitle;
        private System.Windows.Forms.TextBox textTitle;
        private System.Windows.Forms.CheckBox checkResolution;
        private System.Windows.Forms.TextBox textResolutionWidth;
        private System.Windows.Forms.Label labelX;
        private System.Windows.Forms.TextBox textResolutionHeight;
        private System.Windows.Forms.CheckBox checkVideoTimeLimit;
        private System.Windows.Forms.TextBox textVideoTimeLimit;

        /// <summary>
        /// Constructor
        /// </summary>
        public CECamera()
        {
            //
            // Required for Windows Form Designer support
            //
            this.InitializeComponent();

            //
            // Specify all options as default value
            //
            this.ResetOptions();
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose( bool disposing )
        {
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mainMenu = new System.Windows.Forms.MainMenu();
            this.menuStart = new System.Windows.Forms.MenuItem();
            this.menuMenu = new System.Windows.Forms.MenuItem();
            this.menuMode = new System.Windows.Forms.MenuItem();
            this.menuModeStill = new System.Windows.Forms.MenuItem();
            this.menuModeVideoOnly = new System.Windows.Forms.MenuItem();
            this.menuModeVideoWithAudio = new System.Windows.Forms.MenuItem();
            this.menuStillQuality = new System.Windows.Forms.MenuItem();
            this.menuStillQualityDefault = new System.Windows.Forms.MenuItem();
            this.menuStillQualityLow = new System.Windows.Forms.MenuItem();
            this.menuStillQualityNormal = new System.Windows.Forms.MenuItem();
            this.menuStillQualityHigh = new System.Windows.Forms.MenuItem();
            this.menuVideoTypes = new System.Windows.Forms.MenuItem();
            this.menuVideoTypesAll = new System.Windows.Forms.MenuItem();
            this.menuVideoTypesStandard = new System.Windows.Forms.MenuItem();
            this.menuVideoTypesMessaging = new System.Windows.Forms.MenuItem();
            this.menuSeparator1 = new System.Windows.Forms.MenuItem();
            this.menuReset = new System.Windows.Forms.MenuItem();
            this.menuAbout = new System.Windows.Forms.MenuItem();
            this.menuSeparator2 = new System.Windows.Forms.MenuItem();
            this.menuExit = new System.Windows.Forms.MenuItem();
            this.checkInitialDirectory = new System.Windows.Forms.CheckBox();
            this.textInitialDirectory = new System.Windows.Forms.TextBox();
            this.checkDefaultFileName = new System.Windows.Forms.CheckBox();
            this.textDefaultFileName = new System.Windows.Forms.TextBox();
            this.checkTitle = new System.Windows.Forms.CheckBox();
            this.textTitle = new System.Windows.Forms.TextBox();
            this.checkResolution = new System.Windows.Forms.CheckBox();
            this.textResolutionWidth = new System.Windows.Forms.TextBox();
            this.labelX = new System.Windows.Forms.Label();
            this.textResolutionHeight = new System.Windows.Forms.TextBox();
            this.checkVideoTimeLimit = new System.Windows.Forms.CheckBox();
            this.textVideoTimeLimit = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.MenuItems.Add(this.menuStart);
            this.mainMenu.MenuItems.Add(this.menuMenu);
            // 
            // menuStart
            // 
            this.menuStart.Text = "Start";
            this.menuStart.Click += new System.EventHandler(this.menuStart_Click);
            // 
            // menuMenu
            // 
            this.menuMenu.MenuItems.Add(this.menuMode);
            this.menuMenu.MenuItems.Add(this.menuStillQuality);
            this.menuMenu.MenuItems.Add(this.menuVideoTypes);
            this.menuMenu.MenuItems.Add(this.menuSeparator1);
            this.menuMenu.MenuItems.Add(this.menuReset);
            this.menuMenu.MenuItems.Add(this.menuAbout);
            this.menuMenu.MenuItems.Add(this.menuSeparator2);
            this.menuMenu.MenuItems.Add(this.menuExit);
            this.menuMenu.Text = "Menu";
            // 
            // menuMode
            // 
            this.menuMode.MenuItems.Add(this.menuModeStill);
            this.menuMode.MenuItems.Add(this.menuModeVideoOnly);
            this.menuMode.MenuItems.Add(this.menuModeVideoWithAudio);
            this.menuMode.Text = "Mode";
            // 
            // menuModeStill
            // 
            this.menuModeStill.Text = "Still";
            this.menuModeStill.Click += new System.EventHandler(this.menuModeStill_Click);
            // 
            // menuModeVideoOnly
            // 
            this.menuModeVideoOnly.Text = "Video Only";
            this.menuModeVideoOnly.Click += new System.EventHandler(this.menuModeVideoOnly_Click);
            // 
            // menuModeVideoWithAudio
            // 
            this.menuModeVideoWithAudio.Text = "Video with Audio";
            this.menuModeVideoWithAudio.Click += new System.EventHandler(this.menuModeVideoWithAudio_Click);
            // 
            // menuStillQuality
            // 
            this.menuStillQuality.MenuItems.Add(this.menuStillQualityDefault);
            this.menuStillQuality.MenuItems.Add(this.menuStillQualityLow);
            this.menuStillQuality.MenuItems.Add(this.menuStillQualityNormal);
            this.menuStillQuality.MenuItems.Add(this.menuStillQualityHigh);
            this.menuStillQuality.Text = "Still Quality";
            // 
            // menuStillQualityDefault
            // 
            this.menuStillQualityDefault.Text = "Default";
            this.menuStillQualityDefault.Click += new System.EventHandler(this.menuStillQualityDefault_Click);
            // 
            // menuStillQualityLow
            // 
            this.menuStillQualityLow.Text = "Low";
            this.menuStillQualityLow.Click += new System.EventHandler(this.menuStillQualityLow_Click);
            // 
            // menuStillQualityNormal
            // 
            this.menuStillQualityNormal.Text = "Normal";
            this.menuStillQualityNormal.Click += new System.EventHandler(this.menuStillQualityNormal_Click);
            // 
            // menuStillQualityHigh
            // 
            this.menuStillQualityHigh.Text = "High";
            this.menuStillQualityHigh.Click += new System.EventHandler(this.menuStillQualityHigh_Click);
            // 
            // menuVideoTypes
            // 
            this.menuVideoTypes.MenuItems.Add(this.menuVideoTypesAll);
            this.menuVideoTypes.MenuItems.Add(this.menuVideoTypesStandard);
            this.menuVideoTypes.MenuItems.Add(this.menuVideoTypesMessaging);
            this.menuVideoTypes.Text = "Video Types";
            // 
            // menuVideoTypesAll
            // 
            this.menuVideoTypesAll.Text = "All";
            this.menuVideoTypesAll.Click += new System.EventHandler(this.menuVideoTypesAll_Click);
            // 
            // menuVideoTypesStandard
            // 
            this.menuVideoTypesStandard.Text = "Standard";
            this.menuVideoTypesStandard.Click += new System.EventHandler(this.menuVideoTypesStandard_Click);
            // 
            // menuVideoTypesMessaging
            // 
            this.menuVideoTypesMessaging.Text = "Messaging";
            this.menuVideoTypesMessaging.Click += new System.EventHandler(this.menuVideoTypesMessaging_Click);
            // 
            // menuSeparator1
            // 
            this.menuSeparator1.Text = "-";
            // 
            // menuReset
            // 
            this.menuReset.Text = "Reset";
            this.menuReset.Click += new System.EventHandler(this.menuReset_Click);
            // 
            // menuAbout
            // 
            this.menuAbout.Text = "About";
            this.menuAbout.Click += new System.EventHandler(this.menuAbout_Click);
            // 
            // menuSeparator2
            // 
            this.menuSeparator2.Text = "-";
            // 
            // menuExit
            // 
            this.menuExit.Text = "Exit";
            this.menuExit.Click += new System.EventHandler(this.menuExit_Click);
            // 
            // checkInitialDirectory
            // 
            this.checkInitialDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.checkInitialDirectory.Location = new System.Drawing.Point(5, 5);
            this.checkInitialDirectory.Name = "checkInitialDirectory";
            this.checkInitialDirectory.Size = new System.Drawing.Size(199, 16);
            this.checkInitialDirectory.TabIndex = 0;
            this.checkInitialDirectory.Text = "Initial Directory:";
            this.checkInitialDirectory.CheckStateChanged += new System.EventHandler(this.checkInitialDirectory_CheckStateChanged);
            // 
            // textInitialDirectory
            // 
            this.textInitialDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textInitialDirectory.Location = new System.Drawing.Point(5, 25);
            this.textInitialDirectory.MaxLength = 260;
            this.textInitialDirectory.Name = "textInitialDirectory";
            this.textInitialDirectory.Size = new System.Drawing.Size(186, 21);
            this.textInitialDirectory.TabIndex = 1;
            // 
            // checkDefaultFileName
            // 
            this.checkDefaultFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.checkDefaultFileName.Checked = true;
            this.checkDefaultFileName.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkDefaultFileName.Location = new System.Drawing.Point(5, 53);
            this.checkDefaultFileName.Name = "checkDefaultFileName";
            this.checkDefaultFileName.Size = new System.Drawing.Size(186, 16);
            this.checkDefaultFileName.TabIndex = 2;
            this.checkDefaultFileName.Text = "Default File Name:";
            this.checkDefaultFileName.CheckStateChanged += new System.EventHandler(this.checkDefaultFileName_CheckStateChanged);
            // 
            // textDefaultFileName
            // 
            this.textDefaultFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textDefaultFileName.Location = new System.Drawing.Point(5, 75);
            this.textDefaultFileName.MaxLength = 260;
            this.textDefaultFileName.Name = "textDefaultFileName";
            this.textDefaultFileName.Size = new System.Drawing.Size(186, 21);
            this.textDefaultFileName.TabIndex = 3;
            // 
            // checkTitle
            // 
            this.checkTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.checkTitle.Location = new System.Drawing.Point(5, 102);
            this.checkTitle.Name = "checkTitle";
            this.checkTitle.Size = new System.Drawing.Size(153, 23);
            this.checkTitle.TabIndex = 4;
            this.checkTitle.Text = "Title:";
            this.checkTitle.CheckStateChanged += new System.EventHandler(this.checkTitle_CheckStateChanged);
            // 
            // textTitle
            // 
            this.textTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textTitle.Location = new System.Drawing.Point(5, 131);
            this.textTitle.MaxLength = 64;
            this.textTitle.Name = "textTitle";
            this.textTitle.Size = new System.Drawing.Size(186, 21);
            this.textTitle.TabIndex = 5;
            // 
            // checkResolution
            // 
            this.checkResolution.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.checkResolution.Location = new System.Drawing.Point(5, 160);
            this.checkResolution.Name = "checkResolution";
            this.checkResolution.Size = new System.Drawing.Size(199, 16);
            this.checkResolution.TabIndex = 6;
            this.checkResolution.Text = "Resolution (Width x Height):";
            this.checkResolution.CheckStateChanged += new System.EventHandler(this.checkResolution_CheckStateChanged);
            // 
            // textResolutionWidth
            // 
            this.textResolutionWidth.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textResolutionWidth.Location = new System.Drawing.Point(5, 183);
            this.textResolutionWidth.MaxLength = 10;
            this.textResolutionWidth.Name = "textResolutionWidth";
            this.textResolutionWidth.Size = new System.Drawing.Size(97, 21);
            this.textResolutionWidth.TabIndex = 7;
            // 
            // labelX
            // 
            this.labelX.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelX.Location = new System.Drawing.Point(63, 183);
            this.labelX.Name = "labelX";
            this.labelX.Size = new System.Drawing.Size(71, 16);
            this.labelX.Text = "x";
            this.labelX.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // textResolutionHeight
            // 
            this.textResolutionHeight.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textResolutionHeight.Location = new System.Drawing.Point(93, 183);
            this.textResolutionHeight.MaxLength = 10;
            this.textResolutionHeight.Name = "textResolutionHeight";
            this.textResolutionHeight.Size = new System.Drawing.Size(98, 21);
            this.textResolutionHeight.TabIndex = 9;
            // 
            // checkVideoTimeLimit
            // 
            this.checkVideoTimeLimit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.checkVideoTimeLimit.Location = new System.Drawing.Point(5, 214);
            this.checkVideoTimeLimit.Name = "checkVideoTimeLimit";
            this.checkVideoTimeLimit.Size = new System.Drawing.Size(160, 19);
            this.checkVideoTimeLimit.TabIndex = 10;
            this.checkVideoTimeLimit.Text = "Video Time Limit:";
            this.checkVideoTimeLimit.CheckStateChanged += new System.EventHandler(this.checkVideoTimeLimit_CheckStateChanged);
            // 
            // textVideoTimeLimit
            // 
            this.textVideoTimeLimit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textVideoTimeLimit.Location = new System.Drawing.Point(5, 239);
            this.textVideoTimeLimit.MaxLength = 10;
            this.textVideoTimeLimit.Name = "textVideoTimeLimit";
            this.textVideoTimeLimit.Size = new System.Drawing.Size(199, 21);
            this.textVideoTimeLimit.TabIndex = 11;
            // 
            // CECamera
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.checkInitialDirectory);
            this.Controls.Add(this.textInitialDirectory);
            this.Controls.Add(this.checkDefaultFileName);
            this.Controls.Add(this.textDefaultFileName);
            this.Controls.Add(this.checkTitle);
            this.Controls.Add(this.textTitle);
            this.Controls.Add(this.checkResolution);
            this.Controls.Add(this.textResolutionWidth);
            this.Controls.Add(this.labelX);
            this.Controls.Add(this.textResolutionHeight);
            this.Controls.Add(this.checkVideoTimeLimit);
            this.Controls.Add(this.textVideoTimeLimit);
            this.Menu = this.mainMenu;
            this.Name = "CECamera";
            this.Text = "CECamera";
            this.Closed += new System.EventHandler(this.MainForm_Closed);
            this.ResumeLayout(false);

        }
        #endregion

        /// <summary>
        /// The main entry point for the application.
        /// </summary>

        /// <summary>
        /// Exit the application.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">Event arguments</param>
        private void MainForm_Closed(object sender, System.EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Enable/Disable user input of the textInitialDirectory edit control
        /// for allowing/disallowing the user to specify the initial directory
        /// of the CameraCaptureDialog.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">Event arguments</param>
        private void checkInitialDirectory_CheckStateChanged(object sender, System.EventArgs e)
        {
            this.textInitialDirectory.Enabled = this.checkInitialDirectory.Checked;
        }

        /// <summary>
        /// Enable/Disable user input of the textDefaultFileName edit control
        /// for allowing/disallowing the user to specify the default file name
        /// of the CameraCaptureDialog.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">Event arguments</param>
        private void checkDefaultFileName_CheckStateChanged(object sender, System.EventArgs e)
        {
            this.textDefaultFileName.Enabled = this.checkDefaultFileName.Checked;

            validateFileNameInput();
        }

        /// <summary>
        /// Enable/Disable user input of the textTitle edit control
        /// for allowing/disallowing the user to specify the title
        /// of the CameraCaptureDialog.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">Event arguments</param>
        private void checkTitle_CheckStateChanged(object sender, System.EventArgs e)
        {
            this.textTitle.Enabled = this.checkTitle.Checked;
        }

        /// <summary>
        /// Enable/Disable user input of the textResolutionWidth and textResolutionHeight edit control
        /// for allowing/disallowing the user to specify the resolution width and height
        /// of the CameraCaptureDialog.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">Event arguments</param>
        private void checkResolution_CheckStateChanged(object sender, System.EventArgs e)
        {
            this.textResolutionWidth.Enabled = this.checkResolution.Checked;
            this.textResolutionHeight.Enabled = this.checkResolution.Checked;
        }

        /// <summary>
        /// Enable/Disable user input of the textVideoTimeLimit edit control
        /// for allowing/disallowing the user to specify the video time limit
        /// of the CameraCaptureDialog.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">Event arguments</param>
        private void checkVideoTimeLimit_CheckStateChanged(object sender, EventArgs e)
        {
            this.textVideoTimeLimit.Enabled = this.checkVideoTimeLimit.Checked;
        }

        /// <summary>
        /// Launches Camera using CameraCaptureDialog with specified arguments.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">Event arguments</param>
        private void menuStart_Click(object sender, System.EventArgs e)
        {
            CameraCaptureDialog cameraCapture = new CameraCaptureDialog();
            cameraCapture.Owner = this;

            cameraCapture.DefaultFileName = this.textDefaultFileName.Text + cecameraDefaultPictureExtension;

            // Specify the options as user specified.
            if (this.checkInitialDirectory.Checked)
            {
                cameraCapture.InitialDirectory = this.textInitialDirectory.Text;
            }
            if (this.checkDefaultFileName.Checked)
            {
                if (this.menuModeStill.Checked)
                {
//                    if (cameraCapture.DefaultFileName != null)
//                    {
//                        // It is necessary to end picture files with ".jpg".
//                        // Otherwise the argument is invalid.
//                        cameraCapture.DefaultFileName = cameraCapture.DefaultFileName + cecameraDefaultPictureExtension;
//                    }
                    if (this.textDefaultFileName.Text != null)
                    {

                        // It is necessary to end picture files with ".jpg".

                        // Otherwise the argument is invalid.

                        cameraCapture.DefaultFileName = this.textDefaultFileName.Text + cecameraDefaultPictureExtension;

                    }

                }
                else
                {
                    // If it is a video, pass null. This will return a filename with a 
                    // correct extension. Later on we rename the file and keep the extension.
                    cameraCapture.DefaultFileName =  null;
                }
            }

            if (this.checkTitle.Checked)
            {
                cameraCapture.Title = this.textTitle.Text;
            }
            if (this.checkResolution.Checked)
            {
                int resolutionWidth = 0;
                int resolutionHeight = 0;

                if (!ConvertStringToInt(this.textResolutionWidth.Text, ref resolutionWidth))
                {
                    MessageBox.Show("Please input a valid resolution width.", this.Text,
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    textResolutionWidth.Focus();
                    return;
                }
                if (!ConvertStringToInt(this.textResolutionHeight.Text, ref resolutionHeight))
                {
                    MessageBox.Show("Please input a valid resolution height.", this.Text,
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    textResolutionHeight.Focus();
                    return;
                }

                cameraCapture.Resolution = new Size(resolutionWidth, resolutionHeight);
            }
            if (this.checkVideoTimeLimit.Checked)
            {
                int videoTimeLimit = 0;

                if (!ConvertStringToInt(this.textVideoTimeLimit.Text, ref videoTimeLimit))
                {
                    MessageBox.Show("Please input a valid video time limit.", this.Text,
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    textVideoTimeLimit.Focus();
                    return;
                }

                cameraCapture.VideoTimeLimit = new TimeSpan(0, 0, videoTimeLimit);
            }

            // Specify capture mode
            if (this.menuModeStill.Checked)
            {
                cameraCapture.Mode = CameraCaptureMode.Still;
            }
            else if (this.menuModeVideoOnly.Checked)
            {
                cameraCapture.Mode = CameraCaptureMode.VideoOnly;
            }
            else if (this.menuModeVideoWithAudio.Checked)
            {
                cameraCapture.Mode = CameraCaptureMode.VideoWithAudio;
            }

            // Specify still quality
            if (this.menuStillQualityDefault.Checked)
            {
                cameraCapture.StillQuality = CameraCaptureStillQuality.Default;
            }
            else if (this.menuStillQualityLow.Checked)
            {
                cameraCapture.StillQuality = CameraCaptureStillQuality.Low;
            }
            else if (this.menuStillQualityNormal.Checked)
            {
                cameraCapture.StillQuality = CameraCaptureStillQuality.Normal;
            }
            else if (this.menuStillQualityHigh.Checked)
            {
                cameraCapture.StillQuality = CameraCaptureStillQuality.High;
            }

            // Specify video types
            if (this.menuVideoTypesAll.Checked)
            {
                cameraCapture.VideoTypes = CameraCaptureVideoTypes.All;
            }
            else if (this.menuVideoTypesStandard.Checked)
            {
                cameraCapture.VideoTypes = CameraCaptureVideoTypes.Standard;
            }
            else if (this.menuVideoTypesMessaging.Checked)
            {
                cameraCapture.VideoTypes = CameraCaptureVideoTypes.Messaging;
            }
            
/*            object cameraEnabled = Status.SystemState.GetValue(Status.SystemProperty.CameraEnabled);
            if (null != cameraEnabled && 0 == (int)cameraEnabled)
            {
                MessageBox.Show("The camera is disabled", this.Text,
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            } /**/

            try
            {
                // Displays the "Camera Capture" dialog box
                if (DialogResult.OK == cameraCapture.ShowDialog())
                {
                    string fileName = cameraCapture.FileName;

                    // If it is a video we rename the file so that it has the user entered
                    // default filename and the correct extension.
                    if (cameraCapture.Mode != CameraCaptureMode.Still)
                    {
                        string extension = fileName.Substring(fileName.LastIndexOf("."));
                        string directory = "";

                        if (fileName.LastIndexOf("\\") != -1)
                        {
                            directory = fileName.Substring(0, fileName.LastIndexOf("\\") + 1);
                        }

                        fileName = directory + this.textDefaultFileName.Text + extension;

                        System.IO.File.Move(cameraCapture.FileName, fileName);
                    }

                    // The method completed successfully.
                    MessageBox.Show("The picture or video has been successfully captured and saved to:\n\n" + fileName,
                        this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                }
            }
            catch (ArgumentException ex)
            {
                // An invalid argument was specified.
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
            catch (OutOfMemoryException ex)
            {
                // There is not enough memory to save the image or video.
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK,
                    MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
            }
            catch (InvalidOperationException ex)
            {
                // An unknown error occurred.
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK,
                    MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
            }
        }

        /// <summary>
        /// Checks the selected menu item of "Mode".
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">Event arguments</param>
        private void menuModeStill_Click(object sender, System.EventArgs e)
        {
            for (int i = 0; i < this.menuMode.MenuItems.Count; i++)
            {
                this.menuMode.MenuItems[i].Checked = false;
            }
            this.menuModeStill.Checked = true;

            validateFileNameInput();

            this.checkResolution.Enabled = true;
            this.checkVideoTimeLimit.Checked = false;
            this.checkVideoTimeLimit.Enabled = false;
        }

        /// <summary>
        /// Checks the selected menu item of "Mode".
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">Event arguments</param>
        private void menuModeVideoOnly_Click(object sender, System.EventArgs e)
        {
            for (int i = 0; i < this.menuMode.MenuItems.Count; i++)
            {
                this.menuMode.MenuItems[i].Checked = false;
            }
            this.menuModeVideoOnly.Checked = true;

            validateFileNameInput();

            this.checkResolution.Checked = false;
            this.checkResolution.Enabled = false;
            this.checkVideoTimeLimit.Enabled = true;
        }

        /// <summary>
        /// Checks the selected menu item of "Mode".
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">Event arguments</param>
        private void menuModeVideoWithAudio_Click(object sender, System.EventArgs e)
        {
            for (int i = 0; i < this.menuMode.MenuItems.Count; i++)
            {
                this.menuMode.MenuItems[i].Checked = false;
            }
            this.menuModeVideoWithAudio.Checked = true;

            validateFileNameInput();

            this.checkResolution.Checked = false;
            this.checkResolution.Enabled = false;
            this.checkVideoTimeLimit.Enabled = true;            
        }

        /// <summary>
        /// Verifies the filename to be consistent with the camera mode.
        /// </summary>
        private void validateFileNameInput()
        {
            if (this.menuModeStill.Checked)
            {
                if (textDefaultFileName.Text == null || textDefaultFileName.Text.Length == 0 ||
                    textDefaultFileName.Text.Equals(cecameraDefaultVideoFileName))
                {
                    this.textDefaultFileName.Text = cecameraDefaultPictureFileName;
                }
            }
            else
            {
                if (textDefaultFileName.Text == null || textDefaultFileName.Text.Length == 0 ||
                    textDefaultFileName.Text.Equals(cecameraDefaultPictureFileName))
                {
                    textDefaultFileName.Text = cecameraDefaultVideoFileName;
                }
            }
        }
        
        /// <summary>
        /// Checks the selected menu item of "Still Quality".
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">Event arguments</param>
        private void menuStillQualityDefault_Click(object sender, System.EventArgs e)
        {
            for (int i = 0; i < this.menuStillQuality.MenuItems.Count; i++)
            {
                this.menuStillQuality.MenuItems[i].Checked = false;
            }
            this.menuStillQualityDefault.Checked = true;
        }

        /// <summary>
        /// Checks the selected menu item of "Still Quality".
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">Event arguments</param>
        private void menuStillQualityLow_Click(object sender, System.EventArgs e)
        {
            for (int i = 0; i < this.menuStillQuality.MenuItems.Count; i++)
            {
                this.menuStillQuality.MenuItems[i].Checked = false;
            }
            this.menuStillQualityLow.Checked = true;
        }

        /// <summary>
        /// Checks the selected menu item of "Still Quality".
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">Event arguments</param>
        private void menuStillQualityNormal_Click(object sender, System.EventArgs e)
        {
            for (int i = 0; i < this.menuStillQuality.MenuItems.Count; i++)
            {
                this.menuStillQuality.MenuItems[i].Checked = false;
            }
            this.menuStillQualityNormal.Checked = true;
        }

        /// <summary>
        /// Checks the selected menu item of "Still Quality".
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">Event arguments</param>
        private void menuStillQualityHigh_Click(object sender, System.EventArgs e)
        {
            for (int i = 0; i < this.menuStillQuality.MenuItems.Count; i++)
            {
                this.menuStillQuality.MenuItems[i].Checked = false;
            }
            this.menuStillQualityHigh.Checked = true;
        }

        /// <summary>
        /// Checks the selected menu item of "Video Types".
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">Event arguments</param>
        private void menuVideoTypesAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.menuVideoTypes.MenuItems.Count; i++)
            {
                this.menuVideoTypes.MenuItems[i].Checked = false;
            }
            this.menuVideoTypesAll.Checked = true;
        }

        /// <summary>
        /// Checks the selected menu item of "Video Types".
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">Event arguments</param>
        private void menuVideoTypesStandard_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.menuVideoTypes.MenuItems.Count; i++)
            {
                this.menuVideoTypes.MenuItems[i].Checked = false;
            }
            this.menuVideoTypesStandard.Checked = true;
        }

        /// <summary>
        /// Checks the selected menu item of "Video Types".
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">Event arguments</param>
        private void menuVideoTypesMessaging_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.menuVideoTypes.MenuItems.Count; i++)
            {
                this.menuVideoTypes.MenuItems[i].Checked = false;
            }
            this.menuVideoTypesMessaging.Checked = true;
        }

        /// <summary>
        /// Resets all settings as the default value and updates UI.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">Event arguments</param>
        private void menuReset_Click(object sender, System.EventArgs e)
        {
            this.ResetOptions();
        }

        /// <summary>
        /// Display a message box for program information.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">Event arguments</param>
        private void menuAbout_Click(object sender, System.EventArgs e)
        {
            MessageBox.Show("CECamera Version 1.0\n\nSimple sample for CameraCaptureDialog.", this.Text);
        }

        /// <summary>
        /// Close self to exit the application.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">Event arguments</param>
        private void menuExit_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Resets all settings as the default value and updates UI.
        /// </summary>
        private void ResetOptions()
        {
            // Uncheck all checkboxs to specify the options using the default value
            this.checkInitialDirectory.Checked = false;
            this.checkDefaultFileName.Checked = false;
            this.checkTitle.Checked = false;
            this.checkResolution.Checked = false;            
            this.checkVideoTimeLimit.Checked = false;
            this.checkVideoTimeLimit.Enabled = false;

            // Set the default value for the options
            this.textInitialDirectory.Text = cecameraDefaultInitialDirectory;
            this.textDefaultFileName.Text = cecameraDefaultPictureFileName;
            this.textTitle.Text = cecameraDefaultTitle;
            this.textResolutionWidth.Text = cecameraDefaultResolutionWidth;
            this.textResolutionHeight.Text = cecameraDefaultResolutionHeight;
            this.textVideoTimeLimit.Text = cecameraDefaultVideoTimeLimit;

            // Disable the edit controls since specify using default settings
            this.textInitialDirectory.Enabled = false;
            this.textDefaultFileName.Enabled = false;
            this.textTitle.Enabled = false;
            this.textResolutionWidth.Enabled = false;
            this.textResolutionHeight.Enabled = false;
            this.textVideoTimeLimit.Enabled = false;

            // Checks the menu item of "Mode" as default item
            for (int i = 0; i < this.menuMode.MenuItems.Count; i++)
            {
                this.menuMode.MenuItems[i].Checked = false;
            }
            this.menuModeStill.Checked = true;

            // Checks the menu item of "Still Quality" as default item
            for (int i = 0; i < this.menuStillQuality.MenuItems.Count; i++)
            {
                this.menuStillQuality.MenuItems[i].Checked = false;
            }
            this.menuStillQualityDefault.Checked = true;

            // Checks the menu item of "Video Types" as default item
            for (int i = 0; i < this.menuVideoTypes.MenuItems.Count; i++)
            {
                this.menuVideoTypes.MenuItems[i].Checked = false;
            }
            this.menuVideoTypesAll.Checked = true;
        }

        /// <summary>
        /// Converts the string to an integer.
        /// </summary>
        /// <param name="value">A string to convert</param>
        /// <param name="result">A integer of the converted result</param>
        /// <returns>true if converts successfully; otherwise, false.</returns>
        private static bool ConvertStringToInt(string value, ref int result)
        {
            try
            {
                result = Convert.ToInt32(value, NumberFormatInfo.CurrentInfo);
            }
            catch (FormatException)
            {
                return false;
            }
            catch (OverflowException)
            {
                return false;
            }

            return true;
        }
    }
}
