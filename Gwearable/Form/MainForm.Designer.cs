namespace Gwearable
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.mainSplit = new System.Windows.Forms.SplitContainer();
            this.leftmainsplitContainer = new System.Windows.Forms.SplitContainer();
            this.leftTopSplitContainer = new System.Windows.Forms.SplitContainer();
            this.openGLsplitContainer = new System.Windows.Forms.SplitContainer();
            this.deviceoptsplitContainer = new System.Windows.Forms.SplitContainer();
            this.leftbottomsplitContainer = new System.Windows.Forms.SplitContainer();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.savingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recentFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recentDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.windowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.layoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fullFunctionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.replayAndDemoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.captureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dEffectsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.shadowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.backgroundToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startMarkerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bindCameraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.centerOfMassToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.transportBarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sensorBarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.workDirectoryBarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editBarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sensorControlsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openGLControlsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusBarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deviceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disconnectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.powerOffToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.calibrationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.calibrationWizardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zeroOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetAllParametersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutUsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.mainSplit)).BeginInit();
            this.mainSplit.Panel2.SuspendLayout();
            this.mainSplit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.leftmainsplitContainer)).BeginInit();
            this.leftmainsplitContainer.Panel1.SuspendLayout();
            this.leftmainsplitContainer.Panel2.SuspendLayout();
            this.leftmainsplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.leftTopSplitContainer)).BeginInit();
            this.leftTopSplitContainer.Panel1.SuspendLayout();
            this.leftTopSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.openGLsplitContainer)).BeginInit();
            this.openGLsplitContainer.Panel2.SuspendLayout();
            this.openGLsplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.deviceoptsplitContainer)).BeginInit();
            this.deviceoptsplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.leftbottomsplitContainer)).BeginInit();
            this.leftbottomsplitContainer.SuspendLayout();
            this.mainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainSplit
            // 
            this.mainSplit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(35)))), ((int)(((byte)(39)))));
            this.mainSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainSplit.Location = new System.Drawing.Point(0, 25);
            this.mainSplit.Margin = new System.Windows.Forms.Padding(0);
            this.mainSplit.Name = "mainSplit";
            // 
            // mainSplit.Panel2
            // 
            this.mainSplit.Panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(35)))), ((int)(((byte)(39)))));
            this.mainSplit.Panel2.Controls.Add(this.leftmainsplitContainer);
            this.mainSplit.Size = new System.Drawing.Size(996, 568);
            this.mainSplit.SplitterDistance = 220;
            this.mainSplit.SplitterWidth = 2;
            this.mainSplit.TabIndex = 1;
            // 
            // leftmainsplitContainer
            // 
            this.leftmainsplitContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(35)))), ((int)(((byte)(39)))));
            this.leftmainsplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.leftmainsplitContainer.Location = new System.Drawing.Point(0, 0);
            this.leftmainsplitContainer.Name = "leftmainsplitContainer";
            this.leftmainsplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // leftmainsplitContainer.Panel1
            // 
            this.leftmainsplitContainer.Panel1.Controls.Add(this.leftTopSplitContainer);
            // 
            // leftmainsplitContainer.Panel2
            // 
            this.leftmainsplitContainer.Panel2.Controls.Add(this.leftbottomsplitContainer);
            this.leftmainsplitContainer.Size = new System.Drawing.Size(774, 568);
            this.leftmainsplitContainer.SplitterDistance = 398;
            this.leftmainsplitContainer.SplitterWidth = 2;
            this.leftmainsplitContainer.TabIndex = 0;
            // 
            // leftTopSplitContainer
            // 
            this.leftTopSplitContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(35)))), ((int)(((byte)(39)))));
            this.leftTopSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.leftTopSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.leftTopSplitContainer.Name = "leftTopSplitContainer";
            this.leftTopSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // leftTopSplitContainer.Panel1
            // 
            this.leftTopSplitContainer.Panel1.Controls.Add(this.openGLsplitContainer);
            // 
            // leftTopSplitContainer.Panel2
            // 
            this.leftTopSplitContainer.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.leftTopSplitContainer.Size = new System.Drawing.Size(774, 398);
            this.leftTopSplitContainer.SplitterDistance = 283;
            this.leftTopSplitContainer.SplitterWidth = 2;
            this.leftTopSplitContainer.TabIndex = 0;
            // 
            // openGLsplitContainer
            // 
            this.openGLsplitContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(35)))), ((int)(((byte)(39)))));
            this.openGLsplitContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.openGLsplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.openGLsplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.openGLsplitContainer.IsSplitterFixed = true;
            this.openGLsplitContainer.Location = new System.Drawing.Point(0, 0);
            this.openGLsplitContainer.Name = "openGLsplitContainer";
            this.openGLsplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // openGLsplitContainer.Panel1
            // 
            this.openGLsplitContainer.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.openGLsplitContainer.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            // 
            // openGLsplitContainer.Panel2
            // 
            this.openGLsplitContainer.Panel2.Controls.Add(this.deviceoptsplitContainer);
            this.openGLsplitContainer.Size = new System.Drawing.Size(774, 283);
            this.openGLsplitContainer.SplitterDistance = 34;
            this.openGLsplitContainer.SplitterWidth = 2;
            this.openGLsplitContainer.TabIndex = 0;
            // 
            // deviceoptsplitContainer
            // 
            this.deviceoptsplitContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(35)))), ((int)(((byte)(39)))));
            this.deviceoptsplitContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.deviceoptsplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.deviceoptsplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.deviceoptsplitContainer.IsSplitterFixed = true;
            this.deviceoptsplitContainer.Location = new System.Drawing.Point(0, 0);
            this.deviceoptsplitContainer.Margin = new System.Windows.Forms.Padding(0);
            this.deviceoptsplitContainer.Name = "deviceoptsplitContainer";
            // 
            // deviceoptsplitContainer.Panel1
            // 
            this.deviceoptsplitContainer.Panel1.BackColor = System.Drawing.SystemColors.Control;
            // 
            // deviceoptsplitContainer.Panel2
            // 
            this.deviceoptsplitContainer.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.deviceoptsplitContainer.Panel2MinSize = 34;
            this.deviceoptsplitContainer.Size = new System.Drawing.Size(774, 247);
            this.deviceoptsplitContainer.SplitterDistance = 39;
            this.deviceoptsplitContainer.SplitterWidth = 2;
            this.deviceoptsplitContainer.TabIndex = 0;
            // 
            // leftbottomsplitContainer
            // 
            this.leftbottomsplitContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(35)))), ((int)(((byte)(39)))));
            this.leftbottomsplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.leftbottomsplitContainer.Location = new System.Drawing.Point(0, 0);
            this.leftbottomsplitContainer.Name = "leftbottomsplitContainer";
            // 
            // leftbottomsplitContainer.Panel1
            // 
            this.leftbottomsplitContainer.Panel1.BackColor = System.Drawing.SystemColors.Control;
            // 
            // leftbottomsplitContainer.Panel2
            // 
            this.leftbottomsplitContainer.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.leftbottomsplitContainer.Size = new System.Drawing.Size(774, 168);
            this.leftbottomsplitContainer.SplitterDistance = 351;
            this.leftbottomsplitContainer.SplitterWidth = 2;
            this.leftbottomsplitContainer.TabIndex = 0;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // mainMenu
            // 
            this.mainMenu.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.mainMenu.BackgroundImage = global::Gwearable.Properties.Resources.menu_back;
            this.mainMenu.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.windowToolStripMenuItem,
            this.deviceToolStripMenuItem,
            this.calibrationToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(996, 25);
            this.mainMenu.TabIndex = 0;
            this.mainMenu.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.fileToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.openDirectoryToolStripMenuItem,
            this.savingToolStripMenuItem,
            this.recentFileToolStripMenuItem,
            this.recentDirectoryToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(39, 21);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
            this.openToolStripMenuItem.Text = "Open file...";
            // 
            // openDirectoryToolStripMenuItem
            // 
            this.openDirectoryToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.openDirectoryToolStripMenuItem.Name = "openDirectoryToolStripMenuItem";
            this.openDirectoryToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.openDirectoryToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
            this.openDirectoryToolStripMenuItem.Text = "Open directory...";
            // 
            // savingToolStripMenuItem
            // 
            this.savingToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.savingToolStripMenuItem.Enabled = false;
            this.savingToolStripMenuItem.Name = "savingToolStripMenuItem";
            this.savingToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
            this.savingToolStripMenuItem.Text = "Saving...";
            // 
            // recentFileToolStripMenuItem
            // 
            this.recentFileToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.recentFileToolStripMenuItem.Name = "recentFileToolStripMenuItem";
            this.recentFileToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
            this.recentFileToolStripMenuItem.Text = "Recent file...";
            // 
            // recentDirectoryToolStripMenuItem
            // 
            this.recentDirectoryToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.recentDirectoryToolStripMenuItem.Name = "recentDirectoryToolStripMenuItem";
            this.recentDirectoryToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
            this.recentDirectoryToolStripMenuItem.Text = "Recent directory...";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
            this.settingsToolStripMenuItem.Text = "Settings...";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // windowToolStripMenuItem
            // 
            this.windowToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.layoutToolStripMenuItem,
            this.dEffectsToolStripMenuItem,
            this.transportBarToolStripMenuItem,
            this.sensorBarToolStripMenuItem,
            this.workDirectoryBarToolStripMenuItem,
            this.editBarToolStripMenuItem,
            this.sensorControlsToolStripMenuItem,
            this.openGLControlsToolStripMenuItem,
            this.statusBarToolStripMenuItem});
            this.windowToolStripMenuItem.Name = "windowToolStripMenuItem";
            this.windowToolStripMenuItem.Size = new System.Drawing.Size(67, 21);
            this.windowToolStripMenuItem.Text = "Window";
            // 
            // layoutToolStripMenuItem
            // 
            this.layoutToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.layoutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fullFunctionToolStripMenuItem,
            this.replayAndDemoToolStripMenuItem,
            this.captureToolStripMenuItem,
            this.editingToolStripMenuItem});
            this.layoutToolStripMenuItem.Name = "layoutToolStripMenuItem";
            this.layoutToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.layoutToolStripMenuItem.Text = "Layout";
            // 
            // fullFunctionToolStripMenuItem
            // 
            this.fullFunctionToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.fullFunctionToolStripMenuItem.Checked = true;
            this.fullFunctionToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.fullFunctionToolStripMenuItem.Name = "fullFunctionToolStripMenuItem";
            this.fullFunctionToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.fullFunctionToolStripMenuItem.Text = "Full function";
            // 
            // replayAndDemoToolStripMenuItem
            // 
            this.replayAndDemoToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.replayAndDemoToolStripMenuItem.Name = "replayAndDemoToolStripMenuItem";
            this.replayAndDemoToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.replayAndDemoToolStripMenuItem.Text = "Replay and demo";
            // 
            // captureToolStripMenuItem
            // 
            this.captureToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.captureToolStripMenuItem.Name = "captureToolStripMenuItem";
            this.captureToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.captureToolStripMenuItem.Text = "Capture";
            // 
            // editingToolStripMenuItem
            // 
            this.editingToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.editingToolStripMenuItem.Name = "editingToolStripMenuItem";
            this.editingToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.editingToolStripMenuItem.Text = "Editing";
            // 
            // dEffectsToolStripMenuItem
            // 
            this.dEffectsToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.dEffectsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.shadowToolStripMenuItem,
            this.backgroundToolStripMenuItem,
            this.startMarkerToolStripMenuItem,
            this.bindCameraToolStripMenuItem,
            this.centerOfMassToolStripMenuItem});
            this.dEffectsToolStripMenuItem.Name = "dEffectsToolStripMenuItem";
            this.dEffectsToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.dEffectsToolStripMenuItem.Text = "3D effects";
            // 
            // shadowToolStripMenuItem
            // 
            this.shadowToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.shadowToolStripMenuItem.Checked = true;
            this.shadowToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.shadowToolStripMenuItem.Name = "shadowToolStripMenuItem";
            this.shadowToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.shadowToolStripMenuItem.Text = "Shadow";
            // 
            // backgroundToolStripMenuItem
            // 
            this.backgroundToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.backgroundToolStripMenuItem.Checked = true;
            this.backgroundToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.backgroundToolStripMenuItem.Name = "backgroundToolStripMenuItem";
            this.backgroundToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.backgroundToolStripMenuItem.Text = "Background";
            // 
            // startMarkerToolStripMenuItem
            // 
            this.startMarkerToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.startMarkerToolStripMenuItem.Checked = true;
            this.startMarkerToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.startMarkerToolStripMenuItem.Name = "startMarkerToolStripMenuItem";
            this.startMarkerToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.startMarkerToolStripMenuItem.Text = "Start marker";
            // 
            // bindCameraToolStripMenuItem
            // 
            this.bindCameraToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.bindCameraToolStripMenuItem.Name = "bindCameraToolStripMenuItem";
            this.bindCameraToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.bindCameraToolStripMenuItem.Text = "Bind camera";
            // 
            // centerOfMassToolStripMenuItem
            // 
            this.centerOfMassToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.centerOfMassToolStripMenuItem.Checked = true;
            this.centerOfMassToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.centerOfMassToolStripMenuItem.Name = "centerOfMassToolStripMenuItem";
            this.centerOfMassToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.centerOfMassToolStripMenuItem.Text = "Center of mass";
            // 
            // transportBarToolStripMenuItem
            // 
            this.transportBarToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.transportBarToolStripMenuItem.Checked = true;
            this.transportBarToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.transportBarToolStripMenuItem.Name = "transportBarToolStripMenuItem";
            this.transportBarToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.transportBarToolStripMenuItem.Text = "Transport bar";
            this.transportBarToolStripMenuItem.CheckStateChanged += new System.EventHandler(this.transportBarToolStripMenuItem_CheckStateChanged);
            this.transportBarToolStripMenuItem.Click += new System.EventHandler(this.transportBarToolStripMenuItem_Click);
            // 
            // sensorBarToolStripMenuItem
            // 
            this.sensorBarToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.sensorBarToolStripMenuItem.Checked = true;
            this.sensorBarToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.sensorBarToolStripMenuItem.Name = "sensorBarToolStripMenuItem";
            this.sensorBarToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.sensorBarToolStripMenuItem.Text = "Sensor bar";
            this.sensorBarToolStripMenuItem.CheckStateChanged += new System.EventHandler(this.sensorBarToolStripMenuItem_CheckStateChanged);
            this.sensorBarToolStripMenuItem.Click += new System.EventHandler(this.sensorBarToolStripMenuItem_Click);
            // 
            // workDirectoryBarToolStripMenuItem
            // 
            this.workDirectoryBarToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.workDirectoryBarToolStripMenuItem.Checked = true;
            this.workDirectoryBarToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.workDirectoryBarToolStripMenuItem.Name = "workDirectoryBarToolStripMenuItem";
            this.workDirectoryBarToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.workDirectoryBarToolStripMenuItem.Text = "Work directory bar";
            this.workDirectoryBarToolStripMenuItem.CheckStateChanged += new System.EventHandler(this.workDirectoryBarToolStripMenuItem_CheckStateChanged);
            this.workDirectoryBarToolStripMenuItem.Click += new System.EventHandler(this.workDirectoryBarToolStripMenuItem_Click);
            // 
            // editBarToolStripMenuItem
            // 
            this.editBarToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.editBarToolStripMenuItem.Checked = true;
            this.editBarToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.editBarToolStripMenuItem.Name = "editBarToolStripMenuItem";
            this.editBarToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.editBarToolStripMenuItem.Text = "Edit bar";
            this.editBarToolStripMenuItem.CheckStateChanged += new System.EventHandler(this.editBarToolStripMenuItem_CheckStateChanged);
            this.editBarToolStripMenuItem.Click += new System.EventHandler(this.editBarToolStripMenuItem_Click);
            // 
            // sensorControlsToolStripMenuItem
            // 
            this.sensorControlsToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.sensorControlsToolStripMenuItem.Checked = true;
            this.sensorControlsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.sensorControlsToolStripMenuItem.Name = "sensorControlsToolStripMenuItem";
            this.sensorControlsToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.sensorControlsToolStripMenuItem.Text = "Sensor controls";
            this.sensorControlsToolStripMenuItem.CheckStateChanged += new System.EventHandler(this.sensorControlsToolStripMenuItem_CheckStateChanged);
            this.sensorControlsToolStripMenuItem.Click += new System.EventHandler(this.sensorControlsToolStripMenuItem_Click);
            // 
            // openGLControlsToolStripMenuItem
            // 
            this.openGLControlsToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.openGLControlsToolStripMenuItem.Checked = true;
            this.openGLControlsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.openGLControlsToolStripMenuItem.Name = "openGLControlsToolStripMenuItem";
            this.openGLControlsToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.openGLControlsToolStripMenuItem.Text = "OpenGL controls";
            this.openGLControlsToolStripMenuItem.CheckStateChanged += new System.EventHandler(this.openGLControlsToolStripMenuItem_CheckStateChanged);
            this.openGLControlsToolStripMenuItem.Click += new System.EventHandler(this.openGLControlsToolStripMenuItem_Click);
            // 
            // statusBarToolStripMenuItem
            // 
            this.statusBarToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.statusBarToolStripMenuItem.Checked = true;
            this.statusBarToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.statusBarToolStripMenuItem.Name = "statusBarToolStripMenuItem";
            this.statusBarToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.statusBarToolStripMenuItem.Text = "Status bar";
            // 
            // deviceToolStripMenuItem
            // 
            this.deviceToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectToolStripMenuItem,
            this.disconnectToolStripMenuItem,
            this.powerOffToolStripMenuItem});
            this.deviceToolStripMenuItem.Name = "deviceToolStripMenuItem";
            this.deviceToolStripMenuItem.Size = new System.Drawing.Size(58, 21);
            this.deviceToolStripMenuItem.Text = "Device";
            // 
            // connectToolStripMenuItem
            // 
            this.connectToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.connectToolStripMenuItem.Name = "connectToolStripMenuItem";
            this.connectToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.connectToolStripMenuItem.Text = "Connect";
            // 
            // disconnectToolStripMenuItem
            // 
            this.disconnectToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.disconnectToolStripMenuItem.Name = "disconnectToolStripMenuItem";
            this.disconnectToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.disconnectToolStripMenuItem.Text = "Disconnect";
            // 
            // powerOffToolStripMenuItem
            // 
            this.powerOffToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.powerOffToolStripMenuItem.Name = "powerOffToolStripMenuItem";
            this.powerOffToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.powerOffToolStripMenuItem.Text = "Power off";
            // 
            // calibrationToolStripMenuItem
            // 
            this.calibrationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.calibrationWizardToolStripMenuItem,
            this.zeroOutToolStripMenuItem,
            this.recordToolStripMenuItem});
            this.calibrationToolStripMenuItem.Name = "calibrationToolStripMenuItem";
            this.calibrationToolStripMenuItem.Size = new System.Drawing.Size(83, 21);
            this.calibrationToolStripMenuItem.Text = "Calibration";
            // 
            // calibrationWizardToolStripMenuItem
            // 
            this.calibrationWizardToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.calibrationWizardToolStripMenuItem.Name = "calibrationWizardToolStripMenuItem";
            this.calibrationWizardToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.calibrationWizardToolStripMenuItem.Text = "Calibration wizard";
            // 
            // zeroOutToolStripMenuItem
            // 
            this.zeroOutToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.zeroOutToolStripMenuItem.Name = "zeroOutToolStripMenuItem";
            this.zeroOutToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.zeroOutToolStripMenuItem.Text = "Zero out";
            // 
            // recordToolStripMenuItem
            // 
            this.recordToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.recordToolStripMenuItem.Name = "recordToolStripMenuItem";
            this.recordToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.recordToolStripMenuItem.Text = "Record...";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.resetAllParametersToolStripMenuItem,
            this.aboutUsToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(47, 21);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // resetAllParametersToolStripMenuItem
            // 
            this.resetAllParametersToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.resetAllParametersToolStripMenuItem.Name = "resetAllParametersToolStripMenuItem";
            this.resetAllParametersToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.resetAllParametersToolStripMenuItem.Text = "Reset all parameters";
            // 
            // aboutUsToolStripMenuItem
            // 
            this.aboutUsToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.aboutUsToolStripMenuItem.Name = "aboutUsToolStripMenuItem";
            this.aboutUsToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.aboutUsToolStripMenuItem.Text = "About us";
            // 
            // Mainform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(35)))), ((int)(((byte)(39)))));
            this.ClientSize = new System.Drawing.Size(996, 593);
            this.Controls.Add(this.mainSplit);
            this.Controls.Add(this.mainMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.mainMenu;
            this.Name = "Mainform";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Mainform_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Mainform_KeyPress);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Mainform_MouseMove);
            this.Resize += new System.EventHandler(this.Mainform_Resize);
            this.mainSplit.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mainSplit)).EndInit();
            this.mainSplit.ResumeLayout(false);
            this.leftmainsplitContainer.Panel1.ResumeLayout(false);
            this.leftmainsplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.leftmainsplitContainer)).EndInit();
            this.leftmainsplitContainer.ResumeLayout(false);
            this.leftTopSplitContainer.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.leftTopSplitContainer)).EndInit();
            this.leftTopSplitContainer.ResumeLayout(false);
            this.openGLsplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.openGLsplitContainer)).EndInit();
            this.openGLsplitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.deviceoptsplitContainer)).EndInit();
            this.deviceoptsplitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.leftbottomsplitContainer)).EndInit();
            this.leftbottomsplitContainer.ResumeLayout(false);
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.SplitContainer mainSplit;
        private System.Windows.Forms.SplitContainer leftmainsplitContainer;
        private System.Windows.Forms.SplitContainer leftbottomsplitContainer;
        private System.Windows.Forms.SplitContainer leftTopSplitContainer;
        private System.Windows.Forms.ToolStripMenuItem openDirectoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem savingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem recentFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem recentDirectoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem windowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem layoutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dEffectsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem transportBarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sensorBarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem workDirectoryBarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editBarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sensorControlsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openGLControlsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem statusBarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deviceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem disconnectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem powerOffToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem calibrationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem calibrationWizardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zeroOutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem recordToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resetAllParametersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutUsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fullFunctionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem replayAndDemoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem captureToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem shadowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem backgroundToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startMarkerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bindCameraToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem centerOfMassToolStripMenuItem;
        private System.Windows.Forms.SplitContainer openGLsplitContainer;
        private System.Windows.Forms.SplitContainer deviceoptsplitContainer;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}

