namespace iMote
{
  partial class MainForm
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
      this.btnNext = new System.Windows.Forms.Button();
      this.btnPP = new System.Windows.Forms.Button();
      this.btnPrev = new System.Windows.Forms.Button();
      this.label1 = new System.Windows.Forms.Label();
      this.lblTrack = new System.Windows.Forms.Label();
      this.tmrRefresh = new System.Windows.Forms.Timer(this.components);
      this.Notify = new System.Windows.Forms.NotifyIcon(this.components);
      this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.mnuPlayPause = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuSkip = new System.Windows.Forms.ToolStripMenuItem();
      this.artBox = new System.Windows.Forms.PictureBox();
      this.Vol = new System.Windows.Forms.TrackBar();
      this.btnVolUp = new System.Windows.Forms.Button();
      this.btnVolDown = new System.Windows.Forms.Button();
      this.prgTimestamp = new System.Windows.Forms.TrackBar();
      this.lblArtist = new System.Windows.Forms.Label();
      this.lblAlbum = new System.Windows.Forms.Label();
      this.tabControl1 = new System.Windows.Forms.TabControl();
      this.tabPage1 = new System.Windows.Forms.TabPage();
      this.lblLenNextNext = new System.Windows.Forms.Label();
      this.lblLenNext = new System.Windows.Forms.Label();
      this.lblAlbumNextNext = new System.Windows.Forms.Label();
      this.lblArtistNextNext = new System.Windows.Forms.Label();
      this.lblTrackNextNext = new System.Windows.Forms.Label();
      this.lblAlbumNext = new System.Windows.Forms.Label();
      this.lblArtistNext = new System.Windows.Forms.Label();
      this.lblTrackNext = new System.Windows.Forms.Label();
      this.artNextNext = new System.Windows.Forms.PictureBox();
      this.artNext = new System.Windows.Forms.PictureBox();
      this.tabPage2 = new System.Windows.Forms.TabPage();
      this.btnQueue = new System.Windows.Forms.Button();
      this.dataGridView1 = new System.Windows.Forms.DataGridView();
      this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.artistDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.albumDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.AllMusicSource = new System.Windows.Forms.BindingSource(this.components);
      this.lblTimestamp = new System.Windows.Forms.Label();
      this.ctxMnuSongList = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.mnuQueueSong = new System.Windows.Forms.ToolStripMenuItem();
      this.tmrTick = new System.Windows.Forms.Timer(this.components);
      this.cmbPlaylist = new System.Windows.Forms.ComboBox();
      this.contextMenuStrip1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.artBox)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.Vol)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.prgTimestamp)).BeginInit();
      this.tabControl1.SuspendLayout();
      this.tabPage1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.artNextNext)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.artNext)).BeginInit();
      this.tabPage2.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.AllMusicSource)).BeginInit();
      this.ctxMnuSongList.SuspendLayout();
      this.SuspendLayout();
      // 
      // btnNext
      // 
      this.btnNext.Location = new System.Drawing.Point(174, 259);
      this.btnNext.Name = "btnNext";
      this.btnNext.Size = new System.Drawing.Size(75, 23);
      this.btnNext.TabIndex = 0;
      this.btnNext.Text = "Next";
      this.btnNext.UseVisualStyleBackColor = true;
      this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
      // 
      // btnPP
      // 
      this.btnPP.Location = new System.Drawing.Point(93, 259);
      this.btnPP.Name = "btnPP";
      this.btnPP.Size = new System.Drawing.Size(75, 23);
      this.btnPP.TabIndex = 1;
      this.btnPP.Text = "Play/Pause";
      this.btnPP.UseVisualStyleBackColor = true;
      this.btnPP.Click += new System.EventHandler(this.btnPP_Click);
      // 
      // btnPrev
      // 
      this.btnPrev.Location = new System.Drawing.Point(12, 259);
      this.btnPrev.Name = "btnPrev";
      this.btnPrev.Size = new System.Drawing.Size(75, 23);
      this.btnPrev.TabIndex = 3;
      this.btnPrev.Text = "Prev";
      this.btnPrev.UseVisualStyleBackColor = true;
      this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(157, 161);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(38, 13);
      this.label1.TabIndex = 4;
      this.label1.Text = "Track:";
      // 
      // lblTrack
      // 
      this.lblTrack.AutoSize = true;
      this.lblTrack.Font = new System.Drawing.Font("Trebuchet MS", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblTrack.Location = new System.Drawing.Point(12, 9);
      this.lblTrack.Name = "lblTrack";
      this.lblTrack.Size = new System.Drawing.Size(141, 29);
      this.lblTrack.TabIndex = 5;
      this.lblTrack.Text = "Track Name";
      this.lblTrack.UseMnemonic = false;
      // 
      // tmrRefresh
      // 
      this.tmrRefresh.Interval = 2500;
      this.tmrRefresh.Tick += new System.EventHandler(this.tmrRefresh_Tick);
      // 
      // Notify
      // 
      this.Notify.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
      this.Notify.ContextMenuStrip = this.contextMenuStrip1;
      this.Notify.Icon = ((System.Drawing.Icon)(resources.GetObject("Notify.Icon")));
      this.Notify.Text = "Now Playing";
      this.Notify.Visible = true;
      this.Notify.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Notify_MouseDoubleClick);
      // 
      // contextMenuStrip1
      // 
      this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuPlayPause,
            this.mnuSkip});
      this.contextMenuStrip1.Name = "contextMenuStrip1";
      this.contextMenuStrip1.Size = new System.Drawing.Size(133, 48);
      // 
      // mnuPlayPause
      // 
      this.mnuPlayPause.Name = "mnuPlayPause";
      this.mnuPlayPause.Size = new System.Drawing.Size(132, 22);
      this.mnuPlayPause.Text = "Play/Pause";
      this.mnuPlayPause.Click += new System.EventHandler(this.btnPP_Click);
      // 
      // mnuSkip
      // 
      this.mnuSkip.Name = "mnuSkip";
      this.mnuSkip.Size = new System.Drawing.Size(132, 22);
      this.mnuSkip.Text = "Skip Next";
      this.mnuSkip.Click += new System.EventHandler(this.btnNext_Click);
      // 
      // artBox
      // 
      this.artBox.Location = new System.Drawing.Point(12, 75);
      this.artBox.Name = "artBox";
      this.artBox.Size = new System.Drawing.Size(180, 168);
      this.artBox.TabIndex = 6;
      this.artBox.TabStop = false;
      // 
      // Vol
      // 
      this.Vol.Enabled = false;
      this.Vol.Location = new System.Drawing.Point(332, 229);
      this.Vol.Maximum = 100;
      this.Vol.Name = "Vol";
      this.Vol.Orientation = System.Windows.Forms.Orientation.Vertical;
      this.Vol.Size = new System.Drawing.Size(45, 67);
      this.Vol.TabIndex = 8;
      this.Vol.TickFrequency = 100;
      this.Vol.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
      // 
      // btnVolUp
      // 
      this.btnVolUp.Location = new System.Drawing.Point(306, 238);
      this.btnVolUp.Name = "btnVolUp";
      this.btnVolUp.Size = new System.Drawing.Size(23, 23);
      this.btnVolUp.TabIndex = 9;
      this.btnVolUp.Text = "+";
      this.btnVolUp.UseVisualStyleBackColor = true;
      this.btnVolUp.Click += new System.EventHandler(this.btnVolUp_Click);
      // 
      // btnVolDown
      // 
      this.btnVolDown.Location = new System.Drawing.Point(306, 263);
      this.btnVolDown.Name = "btnVolDown";
      this.btnVolDown.Size = new System.Drawing.Size(23, 23);
      this.btnVolDown.TabIndex = 10;
      this.btnVolDown.Text = "-";
      this.btnVolDown.UseVisualStyleBackColor = true;
      this.btnVolDown.Click += new System.EventHandler(this.btnVolDown_Click);
      // 
      // prgTimestamp
      // 
      this.prgTimestamp.Enabled = false;
      this.prgTimestamp.Location = new System.Drawing.Point(12, 36);
      this.prgTimestamp.Maximum = 100;
      this.prgTimestamp.Name = "prgTimestamp";
      this.prgTimestamp.Size = new System.Drawing.Size(347, 45);
      this.prgTimestamp.TabIndex = 11;
      this.prgTimestamp.TickFrequency = 100;
      // 
      // lblArtist
      // 
      this.lblArtist.Font = new System.Drawing.Font("Trebuchet MS", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblArtist.Location = new System.Drawing.Point(198, 75);
      this.lblArtist.Name = "lblArtist";
      this.lblArtist.Size = new System.Drawing.Size(161, 74);
      this.lblArtist.TabIndex = 14;
      this.lblArtist.Text = "Artist";
      this.lblArtist.UseMnemonic = false;
      // 
      // lblAlbum
      // 
      this.lblAlbum.Font = new System.Drawing.Font("Trebuchet MS", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblAlbum.Location = new System.Drawing.Point(198, 149);
      this.lblAlbum.Name = "lblAlbum";
      this.lblAlbum.Size = new System.Drawing.Size(161, 76);
      this.lblAlbum.TabIndex = 15;
      this.lblAlbum.Text = "Album";
      this.lblAlbum.UseMnemonic = false;
      // 
      // tabControl1
      // 
      this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.tabControl1.Controls.Add(this.tabPage1);
      this.tabControl1.Controls.Add(this.tabPage2);
      this.tabControl1.Location = new System.Drawing.Point(365, 41);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new System.Drawing.Size(320, 245);
      this.tabControl1.TabIndex = 16;
      this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.TabChange);
      // 
      // tabPage1
      // 
      this.tabPage1.Controls.Add(this.lblLenNextNext);
      this.tabPage1.Controls.Add(this.lblLenNext);
      this.tabPage1.Controls.Add(this.lblAlbumNextNext);
      this.tabPage1.Controls.Add(this.lblArtistNextNext);
      this.tabPage1.Controls.Add(this.lblTrackNextNext);
      this.tabPage1.Controls.Add(this.lblAlbumNext);
      this.tabPage1.Controls.Add(this.lblArtistNext);
      this.tabPage1.Controls.Add(this.lblTrackNext);
      this.tabPage1.Controls.Add(this.artNextNext);
      this.tabPage1.Controls.Add(this.artNext);
      this.tabPage1.Location = new System.Drawing.Point(4, 22);
      this.tabPage1.Name = "tabPage1";
      this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage1.Size = new System.Drawing.Size(312, 219);
      this.tabPage1.TabIndex = 0;
      this.tabPage1.Text = "Upcoming";
      this.tabPage1.UseVisualStyleBackColor = true;
      // 
      // lblLenNextNext
      // 
      this.lblLenNextNext.AutoSize = true;
      this.lblLenNextNext.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblLenNextNext.Location = new System.Drawing.Point(110, 169);
      this.lblLenNextNext.Name = "lblLenNextNext";
      this.lblLenNextNext.Size = new System.Drawing.Size(52, 22);
      this.lblLenNextNext.TabIndex = 26;
      this.lblLenNextNext.Text = "label4";
      this.lblLenNextNext.UseMnemonic = false;
      // 
      // lblLenNext
      // 
      this.lblLenNext.AutoSize = true;
      this.lblLenNext.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblLenNext.Location = new System.Drawing.Point(110, 72);
      this.lblLenNext.Name = "lblLenNext";
      this.lblLenNext.Size = new System.Drawing.Size(52, 22);
      this.lblLenNext.TabIndex = 25;
      this.lblLenNext.Text = "label4";
      this.lblLenNext.UseMnemonic = false;
      // 
      // lblAlbumNextNext
      // 
      this.lblAlbumNextNext.AutoSize = true;
      this.lblAlbumNextNext.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblAlbumNextNext.Location = new System.Drawing.Point(110, 147);
      this.lblAlbumNextNext.Name = "lblAlbumNextNext";
      this.lblAlbumNextNext.Size = new System.Drawing.Size(52, 22);
      this.lblAlbumNextNext.TabIndex = 24;
      this.lblAlbumNextNext.Text = "label5";
      this.lblAlbumNextNext.UseMnemonic = false;
      // 
      // lblArtistNextNext
      // 
      this.lblArtistNextNext.AutoSize = true;
      this.lblArtistNextNext.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblArtistNextNext.Location = new System.Drawing.Point(110, 125);
      this.lblArtistNextNext.Name = "lblArtistNextNext";
      this.lblArtistNextNext.Size = new System.Drawing.Size(52, 22);
      this.lblArtistNextNext.TabIndex = 23;
      this.lblArtistNextNext.Text = "label6";
      this.lblArtistNextNext.UseMnemonic = false;
      // 
      // lblTrackNextNext
      // 
      this.lblTrackNextNext.AutoSize = true;
      this.lblTrackNextNext.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblTrackNextNext.Location = new System.Drawing.Point(110, 103);
      this.lblTrackNextNext.Name = "lblTrackNextNext";
      this.lblTrackNextNext.Size = new System.Drawing.Size(52, 22);
      this.lblTrackNextNext.TabIndex = 22;
      this.lblTrackNextNext.Text = "label7";
      this.lblTrackNextNext.UseMnemonic = false;
      // 
      // lblAlbumNext
      // 
      this.lblAlbumNext.AutoSize = true;
      this.lblAlbumNext.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblAlbumNext.Location = new System.Drawing.Point(110, 50);
      this.lblAlbumNext.Name = "lblAlbumNext";
      this.lblAlbumNext.Size = new System.Drawing.Size(52, 22);
      this.lblAlbumNext.TabIndex = 21;
      this.lblAlbumNext.Text = "label4";
      this.lblAlbumNext.UseMnemonic = false;
      // 
      // lblArtistNext
      // 
      this.lblArtistNext.AutoSize = true;
      this.lblArtistNext.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblArtistNext.Location = new System.Drawing.Point(110, 28);
      this.lblArtistNext.Name = "lblArtistNext";
      this.lblArtistNext.Size = new System.Drawing.Size(52, 22);
      this.lblArtistNext.TabIndex = 20;
      this.lblArtistNext.Text = "label3";
      this.lblArtistNext.UseMnemonic = false;
      // 
      // lblTrackNext
      // 
      this.lblTrackNext.AutoSize = true;
      this.lblTrackNext.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblTrackNext.Location = new System.Drawing.Point(110, 6);
      this.lblTrackNext.Name = "lblTrackNext";
      this.lblTrackNext.Size = new System.Drawing.Size(52, 22);
      this.lblTrackNext.TabIndex = 19;
      this.lblTrackNext.Text = "label2";
      this.lblTrackNext.UseMnemonic = false;
      // 
      // artNextNext
      // 
      this.artNextNext.Location = new System.Drawing.Point(6, 101);
      this.artNextNext.Name = "artNextNext";
      this.artNextNext.Size = new System.Drawing.Size(98, 89);
      this.artNextNext.TabIndex = 18;
      this.artNextNext.TabStop = false;
      // 
      // artNext
      // 
      this.artNext.Location = new System.Drawing.Point(6, 6);
      this.artNext.Name = "artNext";
      this.artNext.Size = new System.Drawing.Size(98, 89);
      this.artNext.TabIndex = 17;
      this.artNext.TabStop = false;
      // 
      // tabPage2
      // 
      this.tabPage2.Controls.Add(this.btnQueue);
      this.tabPage2.Controls.Add(this.dataGridView1);
      this.tabPage2.Location = new System.Drawing.Point(4, 22);
      this.tabPage2.Name = "tabPage2";
      this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage2.Size = new System.Drawing.Size(312, 219);
      this.tabPage2.TabIndex = 1;
      this.tabPage2.Text = "All Songs";
      this.tabPage2.UseVisualStyleBackColor = true;
      // 
      // btnQueue
      // 
      this.btnQueue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.btnQueue.Location = new System.Drawing.Point(3, 190);
      this.btnQueue.Name = "btnQueue";
      this.btnQueue.Size = new System.Drawing.Size(303, 23);
      this.btnQueue.TabIndex = 27;
      this.btnQueue.Text = "Queue";
      this.btnQueue.UseVisualStyleBackColor = true;
      this.btnQueue.Click += new System.EventHandler(this.btnQueue_Click);
      // 
      // dataGridView1
      // 
      this.dataGridView1.AllowUserToAddRows = false;
      this.dataGridView1.AllowUserToDeleteRows = false;
      this.dataGridView1.AllowUserToOrderColumns = true;
      this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.dataGridView1.AutoGenerateColumns = false;
      this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nameDataGridViewTextBoxColumn,
            this.artistDataGridViewTextBoxColumn,
            this.albumDataGridViewTextBoxColumn});
      this.dataGridView1.DataSource = this.AllMusicSource;
      this.dataGridView1.Location = new System.Drawing.Point(3, 3);
      this.dataGridView1.MultiSelect = false;
      this.dataGridView1.Name = "dataGridView1";
      this.dataGridView1.ReadOnly = true;
      this.dataGridView1.RowHeadersVisible = false;
      this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.dataGridView1.Size = new System.Drawing.Size(303, 185);
      this.dataGridView1.TabIndex = 0;
      this.dataGridView1.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGrid_ColHeaderClick);
      this.dataGridView1.SelectionChanged += new System.EventHandler(this.djQueueSong);
      // 
      // nameDataGridViewTextBoxColumn
      // 
      this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
      this.nameDataGridViewTextBoxColumn.HeaderText = "Name";
      this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
      this.nameDataGridViewTextBoxColumn.ReadOnly = true;
      // 
      // artistDataGridViewTextBoxColumn
      // 
      this.artistDataGridViewTextBoxColumn.DataPropertyName = "Artist";
      this.artistDataGridViewTextBoxColumn.HeaderText = "Artist";
      this.artistDataGridViewTextBoxColumn.Name = "artistDataGridViewTextBoxColumn";
      this.artistDataGridViewTextBoxColumn.ReadOnly = true;
      // 
      // albumDataGridViewTextBoxColumn
      // 
      this.albumDataGridViewTextBoxColumn.DataPropertyName = "Album";
      this.albumDataGridViewTextBoxColumn.HeaderText = "Album";
      this.albumDataGridViewTextBoxColumn.Name = "albumDataGridViewTextBoxColumn";
      this.albumDataGridViewTextBoxColumn.ReadOnly = true;
      // 
      // AllMusicSource
      // 
      this.AllMusicSource.DataSource = typeof(iMote.Song);
      // 
      // lblTimestamp
      // 
      this.lblTimestamp.Font = new System.Drawing.Font("Trebuchet MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblTimestamp.Location = new System.Drawing.Point(73, 54);
      this.lblTimestamp.Name = "lblTimestamp";
      this.lblTimestamp.Size = new System.Drawing.Size(272, 23);
      this.lblTimestamp.TabIndex = 25;
      this.lblTimestamp.Text = "label2";
      this.lblTimestamp.TextAlign = System.Drawing.ContentAlignment.TopRight;
      // 
      // ctxMnuSongList
      // 
      this.ctxMnuSongList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuQueueSong});
      this.ctxMnuSongList.Name = "ctxMnuSongList";
      this.ctxMnuSongList.Size = new System.Drawing.Size(140, 26);
      // 
      // mnuQueueSong
      // 
      this.mnuQueueSong.Name = "mnuQueueSong";
      this.mnuQueueSong.Size = new System.Drawing.Size(139, 22);
      this.mnuQueueSong.Text = "Queue Song";
      this.mnuQueueSong.Click += new System.EventHandler(this.djQueueSong);
      // 
      // tmrTick
      // 
      this.tmrTick.Tick += new System.EventHandler(this.tmrTick_Tick);
      // 
      // cmbPlaylist
      // 
      this.cmbPlaylist.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.cmbPlaylist.FormattingEnabled = true;
      this.cmbPlaylist.Location = new System.Drawing.Point(564, 9);
      this.cmbPlaylist.Name = "cmbPlaylist";
      this.cmbPlaylist.Size = new System.Drawing.Size(121, 21);
      this.cmbPlaylist.TabIndex = 26;
      this.cmbPlaylist.SelectedIndexChanged += new System.EventHandler(this.cmbPlaylist_SelectedIndexChanged);
      // 
      // MainForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(697, 298);
      this.Controls.Add(this.cmbPlaylist);
      this.Controls.Add(this.artBox);
      this.Controls.Add(this.lblArtist);
      this.Controls.Add(this.lblTimestamp);
      this.Controls.Add(this.tabControl1);
      this.Controls.Add(this.lblAlbum);
      this.Controls.Add(this.prgTimestamp);
      this.Controls.Add(this.lblTrack);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.btnPrev);
      this.Controls.Add(this.btnPP);
      this.Controls.Add(this.btnNext);
      this.Controls.Add(this.btnVolDown);
      this.Controls.Add(this.Vol);
      this.Controls.Add(this.btnVolUp);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "MainForm";
      this.Text = "Blah";
      this.Load += new System.EventHandler(this.Form1_Load);
      this.Resize += new System.EventHandler(this.Form1_Resize);
      this.contextMenuStrip1.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.artBox)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.Vol)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.prgTimestamp)).EndInit();
      this.tabControl1.ResumeLayout(false);
      this.tabPage1.ResumeLayout(false);
      this.tabPage1.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.artNextNext)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.artNext)).EndInit();
      this.tabPage2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.AllMusicSource)).EndInit();
      this.ctxMnuSongList.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button btnNext;
    private System.Windows.Forms.Button btnPP;
    private System.Windows.Forms.Button btnPrev;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label lblTrack;
    private System.Windows.Forms.Timer tmrRefresh;
    private System.Windows.Forms.NotifyIcon Notify;
    private System.Windows.Forms.PictureBox artBox;
    private System.Windows.Forms.TrackBar Vol;
    private System.Windows.Forms.Button btnVolUp;
    private System.Windows.Forms.Button btnVolDown;
    private System.Windows.Forms.TrackBar prgTimestamp;
    private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    private System.Windows.Forms.ToolStripMenuItem mnuPlayPause;
    private System.Windows.Forms.ToolStripMenuItem mnuSkip;
    private System.Windows.Forms.Label lblArtist;
    private System.Windows.Forms.Label lblAlbum;
    private System.Windows.Forms.TabControl tabControl1;
    private System.Windows.Forms.TabPage tabPage1;
    private System.Windows.Forms.Label lblAlbumNextNext;
    private System.Windows.Forms.Label lblArtistNextNext;
    private System.Windows.Forms.Label lblTrackNextNext;
    private System.Windows.Forms.Label lblAlbumNext;
    private System.Windows.Forms.Label lblArtistNext;
    private System.Windows.Forms.Label lblTrackNext;
    private System.Windows.Forms.PictureBox artNextNext;
    private System.Windows.Forms.PictureBox artNext;
    private System.Windows.Forms.TabPage tabPage2;
    private System.Windows.Forms.Label lblTimestamp;
    private System.Windows.Forms.Label lblLenNextNext;
    private System.Windows.Forms.Label lblLenNext;
    private System.Windows.Forms.DataGridView dataGridView1;
    private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn artistDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn albumDataGridViewTextBoxColumn;
    private System.Windows.Forms.BindingSource AllMusicSource;
    private System.Windows.Forms.ContextMenuStrip ctxMnuSongList;
    private System.Windows.Forms.ToolStripMenuItem mnuQueueSong;
    private System.Windows.Forms.Button btnQueue;
    private System.Windows.Forms.Timer tmrTick;
    private System.Windows.Forms.ComboBox cmbPlaylist;
  }
}

