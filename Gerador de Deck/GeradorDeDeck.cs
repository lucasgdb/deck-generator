namespace Gerador_de_Deck
{
    abstract class InicializaPrograma
    {
        [System.Runtime.InteropServices.DllImport("User32.dll")]
        public static extern void SwitchToThisWindow(System.IntPtr hWnd);

        static System.Threading.Mutex _mutex = new System.Threading.Mutex(true, name: "7d89086c-8e9f-43c8-8acd-d8cf877f48ca");

        [System.STAThread]
        static void Main()
        {
            if (_mutex.WaitOne(System.TimeSpan.Zero, true))
                try
                {
                    System.Windows.Forms.Application.EnableVisualStyles();
                    System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
                    System.Windows.Forms.Application.Run(new Programa());
                }
                finally { _mutex.ReleaseMutex(); }
            else
            {
                System.Diagnostics.Process p = System.Diagnostics.Process.GetCurrentProcess();
                System.Diagnostics.Process[] ps = System.Diagnostics.Process.GetProcessesByName(p.ProcessName);
                foreach (System.Diagnostics.Process pp in ps)
                    if (pp.Id != p.Id) SwitchToThisWindow(pp.MainWindowHandle);
            }
        }
    }

    partial class Programa : System.Windows.Forms.Form
    {
        public Programa()
        {
            // Variáveis
            this.components = new System.ComponentModel.Container();
            _Deck = new Classes.CartasInformacoes();
            valores = new byte[] { 85, 85, 82, 78, 71, 64, 56, 47, 39, 31, 25, 19, 13 };
            arenas = new string[] { "Campo de Treino", "Estádio Goblin", "Fosso dos Ossos", "Torneio Bárbaro",
                "Parquinho da P.E.K.K.A", "Vale dos Feitiços", "Oficina do Construtor", "Arena Real", "Pico Congelado",
                "Arena da Selva", "Montanha do Porco", "Eletrovale", "Arena Lendária" };
            for (byte i = 1; i < _Deck.CartasInformacao.Length; i++)
            { dados.Add(_Deck.CartasInformacao[i].Split('\n')[0]); dados.Add(_Deck.CodigoCartas[i].ToString()); }

            #region Cria Formulário
            this.tip = new System.Windows.Forms.ToolTip(this.components)
            {
                AutoPopDelay = 5000,
                InitialDelay = 400,
                ReshowDelay = 400
            };

            #region Barra superior
            this.pBarra = new System.Windows.Forms.Panel
            {
                Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right,
                BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle,
                Location = new System.Drawing.Point(167, 0),
                Size = new System.Drawing.Size(766, 30),
            };

            this.picIco = new System.Windows.Forms.PictureBox
            {
                Cursor = System.Windows.Forms.Cursors.Default,
                Location = new System.Drawing.Point(2, 2),
                Size = new System.Drawing.Size(23, 23),
                SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage,
                TabStop = false,
                Image = Properties.Resources.icon.ToBitmap()
            };
            ((System.ComponentModel.ISupportInitialize)(this.picIco)).BeginInit();
            this.tip.SetToolTip(this.picIco, "Gerador de Deck - Clash Royale");
            this.picIco.DoubleClick += (s, e) => Control_MaximizaRestaura();
            this.picIco.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.picIco.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            ((System.ComponentModel.ISupportInitialize)(this.picIco)).EndInit();

            this.lblNome = new System.Windows.Forms.Label
            {
                Anchor = System.Windows.Forms.AnchorStyles.Top,
                AutoSize = true,
                Font = new System.Drawing.Font("Microsoft Sans Serif", 13.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0),
            };
            this.lblNome.DoubleClick += (s, e) => Control_MaximizaRestaura();
            this.lblNome.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.lblNome.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);

            this.btnMinimizar = new System.Windows.Forms.Button
            {
                Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right,
                Cursor = System.Windows.Forms.Cursors.Hand,
                Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0),
                Location = new System.Drawing.Point(676, 0),
                Size = new System.Drawing.Size(30, 28),
                Text = "─",
                UseVisualStyleBackColor = true,
                TabStop = false
            };
            this.tip.SetToolTip(this.btnMinimizar, "Minimizar");
            this.btnMinimizar.FlatAppearance.BorderSize = 0;
            this.btnMinimizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinimizar.Click += (s, e) => { pOpcoes.Select(); WindowState = System.Windows.Forms.FormWindowState.Minimized; };

            this.btnRedimensionar = new System.Windows.Forms.Button()
            {
                Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right,
                Cursor = System.Windows.Forms.Cursors.Hand,
                Font = new System.Drawing.Font("Marlett", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0x31),
                Location = new System.Drawing.Point(706, 0),
                Size = new System.Drawing.Size(30, 28),
                Text = "1",
                UseVisualStyleBackColor = true,
                TextAlign = System.Drawing.ContentAlignment.MiddleRight,
                TabStop = false
            };
            this.tip.SetToolTip(this.btnRedimensionar, "Redimensionar");
            this.btnRedimensionar.FlatAppearance.BorderSize = 0;
            this.btnRedimensionar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRedimensionar.Click += (s, e) => Control_MaximizaRestaura();
            this.btnRedimensionar.Click += (s, e) => pBarra.Select();

            this.btnFechar = new System.Windows.Forms.Button
            {
                Cursor = System.Windows.Forms.Cursors.Hand,
                Dock = System.Windows.Forms.DockStyle.Right,
                FlatStyle = System.Windows.Forms.FlatStyle.Flat,
                Font = new System.Drawing.Font("Marlett", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0x72),
                Text = "r",
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                Size = new System.Drawing.Size(30, 28),
                UseVisualStyleBackColor = true,
                TabStop = false
            };
            this.tip.SetToolTip(this.btnFechar, "Fechar");
            this.btnFechar.FlatAppearance.BorderSize = 0;
            this.btnFechar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkRed;
            this.btnFechar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red;
            this.btnFechar.Click += (s, e) =>
            {
                nIcon.Dispose();
                System.Environment.Exit(0);
            };

            this.pBarra.SuspendLayout();
            this.pBarra.Controls.Add(this.btnFechar);
            this.pBarra.Controls.Add(this.btnRedimensionar);
            this.pBarra.Controls.Add(this.btnMinimizar);
            this.pBarra.Controls.Add(this.lblNome);
            this.pBarra.Controls.Add(this.picIco);
            this.pBarra.DoubleClick += (s, e) => Control_MaximizaRestaura();
            this.pBarra.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.pBarra.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.pBarra.ResumeLayout(false);
            this.pBarra.PerformLayout();
            #endregion

            #region Barra lateral esquerda      
            this.pOpcoes = new System.Windows.Forms.Panel
            {
                Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left,
                BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle,
                Location = new System.Drawing.Point(0, 0),
                Size = new System.Drawing.Size(168, 470),
            };

            System.Windows.Forms.PictureBox picLinha1 = new System.Windows.Forms.PictureBox
            {
                BackColor = System.Drawing.Color.White,
                BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle,
                Location = new System.Drawing.Point(12, 64),
                Size = new System.Drawing.Size(130, 1),
                TabStop = false
            };
            ((System.ComponentModel.ISupportInitialize)(picLinha1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(picLinha1)).EndInit();

            System.Windows.Forms.PictureBox picLinha2 = new System.Windows.Forms.PictureBox
            {
                BackColor = System.Drawing.Color.White,
                BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle,
                Location = new System.Drawing.Point(12, 117),
                Size = new System.Drawing.Size(130, 1),
                TabStop = false
            };
            ((System.ComponentModel.ISupportInitialize)(picLinha2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(picLinha2)).EndInit();

            System.Windows.Forms.PictureBox picLinha3 = new System.Windows.Forms.PictureBox
            {
                Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left,
                BackColor = System.Drawing.Color.White,
                BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle,
                Location = new System.Drawing.Point(12, 460),
                Size = new System.Drawing.Size(130, 1),
                TabStop = false
            };
            ((System.ComponentModel.ISupportInitialize)(picLinha3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(picLinha3)).EndInit();

            System.Windows.Forms.PictureBox picLinha4 = new System.Windows.Forms.PictureBox
            {
                BackColor = System.Drawing.Color.White,
                BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle,
                Location = new System.Drawing.Point(12, 7),
                Size = new System.Drawing.Size(130, 1),
                TabStop = false
            };
            ((System.ComponentModel.ISupportInitialize)(picLinha4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(picLinha4)).EndInit();

            System.Windows.Forms.PictureBox picLinha5 = new System.Windows.Forms.PictureBox
            {
                BackColor = System.Drawing.Color.White,
                BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle,
                Location = new System.Drawing.Point(12, 195),
                Size = new System.Drawing.Size(130, 1),
                TabStop = false
            };
            ((System.ComponentModel.ISupportInitialize)(picLinha5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(picLinha5)).EndInit();

            System.Windows.Forms.PictureBox picLinha6 = new System.Windows.Forms.PictureBox
            {
                BackColor = System.Drawing.Color.White,
                BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle,
                Location = new System.Drawing.Point(12, 234),
                Size = new System.Drawing.Size(130, 1),
                TabStop = false
            };
            ((System.ComponentModel.ISupportInitialize)(picLinha6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(picLinha6)).EndInit();

            System.Windows.Forms.PictureBox picLinha7 = new System.Windows.Forms.PictureBox
            {
                BackColor = System.Drawing.Color.White,
                BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle,
                Location = new System.Drawing.Point(12, 156),
                Size = new System.Drawing.Size(130, 1),
                TabStop = false
            };
            ((System.ComponentModel.ISupportInitialize)(picLinha7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(picLinha7)).EndInit();

            System.Windows.Forms.PictureBox picLinha8 = new System.Windows.Forms.PictureBox
            {
                Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left,
                BackColor = System.Drawing.Color.White,
                BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle,
                Location = new System.Drawing.Point(12, 411),
                Size = new System.Drawing.Size(130, 1),
                TabStop = false
            };
            ((System.ComponentModel.ISupportInitialize)(picLinha8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(picLinha8)).EndInit();

            System.Windows.Forms.PictureBox picLinha9 = new System.Windows.Forms.PictureBox()
            {
                BackColor = System.Drawing.Color.White,
                BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle,
                Location = new System.Drawing.Point(12, 273),
                Size = new System.Drawing.Size(130, 1),
                TabStop = false
            };
            ((System.ComponentModel.ISupportInitialize)(picLinha9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(picLinha9)).EndInit();

            System.Windows.Forms.PictureBox picLinha10 = new System.Windows.Forms.PictureBox()
            {
                BackColor = System.Drawing.Color.White,
                BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle,
                Location = new System.Drawing.Point(12, 312),
                Size = new System.Drawing.Size(130, 1),
                TabStop = false
            };
            ((System.ComponentModel.ISupportInitialize)(picLinha9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(picLinha9)).EndInit();

            this.btnGeradorDeck = new System.Windows.Forms.Button
            {
                Cursor = System.Windows.Forms.Cursors.Hand,
                Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0),
                Location = new System.Drawing.Point(12, 7),
                Size = new System.Drawing.Size(130, 58),
                Text = "Gerador de Deck",
                UseVisualStyleBackColor = true,
                TabStop = false
            };
            this.tip.SetToolTip(this.btnGeradorDeck, "Gerador de Deck");
            this.btnGeradorDeck.FlatAppearance.BorderSize = 0;
            this.btnGeradorDeck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGeradorDeck.Click += (s, e) => ClickBotao(true, pGerador, btnGeradorDeck);

            this.btnSelecaoDeCartas = new System.Windows.Forms.Button
            {
                Cursor = System.Windows.Forms.Cursors.Hand,
                Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0),
                Location = new System.Drawing.Point(12, 64),
                Size = new System.Drawing.Size(130, 54),
                Text = "Seleção de Cartas",
                UseVisualStyleBackColor = true,
                TabStop = false
            };
            this.tip.SetToolTip(this.btnSelecaoDeCartas, "Seleção de Cartas");
            this.btnSelecaoDeCartas.FlatAppearance.BorderSize = 0;
            this.btnSelecaoDeCartas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelecaoDeCartas.Click += (s, e) => ClickBotao(true, pSelecaoDeCartas, btnSelecaoDeCartas);

            this.btnDecksSalvos = new System.Windows.Forms.Button
            {
                Cursor = System.Windows.Forms.Cursors.Hand,
                Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0),
                Location = new System.Drawing.Point(12, 117),
                Size = new System.Drawing.Size(130, 40),
                Text = "Decks salvos",
                UseVisualStyleBackColor = true,
                TabStop = false
            };
            this.tip.SetToolTip(this.btnDecksSalvos, "Decks salvos");
            this.btnDecksSalvos.FlatAppearance.BorderSize = 0;
            this.btnDecksSalvos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDecksSalvos.Click += (s, e) => ClickBotao(true, pDecksSalvos, btnDecksSalvos);

            this.btnMelhoresDecks = new System.Windows.Forms.Button()
            {
                Cursor = System.Windows.Forms.Cursors.Hand,
                Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0),
                Location = new System.Drawing.Point(12, 156),
                Size = new System.Drawing.Size(130, 40),
                Text = "Melhores Decks",
                UseVisualStyleBackColor = true,
                FlatStyle = System.Windows.Forms.FlatStyle.Flat,
                TabStop = false
            };
            this.tip.SetToolTip(this.btnMelhoresDecks, "Melhores Decks");
            this.btnMelhoresDecks.FlatAppearance.BorderSize = 0;
            this.btnMelhoresDecks.Click += (s, e) => ClickBotao(true, pMelhoresDecks, btnMelhoresDecks);

            this.btnBalanceamento = new System.Windows.Forms.Button()
            {
                Cursor = System.Windows.Forms.Cursors.Hand,
                Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0),
                ForeColor = System.Drawing.Color.White,
                Location = new System.Drawing.Point(12, 195),
                Size = new System.Drawing.Size(130, 40),
                Text = "Balanceamento",
                UseVisualStyleBackColor = true,
                FlatStyle = System.Windows.Forms.FlatStyle.Flat,
                TabStop = false
            };
            this.tip.SetToolTip(this.btnBalanceamento, "Balanceamento");
            this.btnBalanceamento.FlatAppearance.BorderSize = 0;
            this.btnBalanceamento.Click += (s, e) => ClickBotao(true, pBalanceamento, btnBalanceamento);

            this.btnConfig = new System.Windows.Forms.Button
            {
                Cursor = System.Windows.Forms.Cursors.Hand,
                Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0),
                Location = new System.Drawing.Point(12, 234),
                Size = new System.Drawing.Size(130, 40),
                Text = "Configurações",
                UseVisualStyleBackColor = true,
                TabStop = false
            };
            this.tip.SetToolTip(this.btnConfig, "Configurações");
            this.btnConfig.FlatAppearance.BorderSize = 0;
            this.btnConfig.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfig.Click += (s, e) => ClickBotao(true, pConfig, btnConfig);

            this.btnAtualizador = new System.Windows.Forms.Button
            {
                Cursor = System.Windows.Forms.Cursors.Hand,
                Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0),
                Location = new System.Drawing.Point(12, 273),
                Size = new System.Drawing.Size(130, 40),
                Text = "Atualizador",
                UseVisualStyleBackColor = true,
                TabStop = false
            };
            this.tip.SetToolTip(this.btnAtualizador, "Atualizador");
            this.btnAtualizador.FlatAppearance.BorderSize = 0;
            this.btnAtualizador.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAtualizador.Click += (s, e) => ClickBotao(true, pAtualizador, btnAtualizador);

            this.btnSobre = new System.Windows.Forms.Button
            {
                Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left,
                Cursor = System.Windows.Forms.Cursors.Hand,
                Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0),
                Image = Properties.Resources.info.ToBitmap(),
                ImageAlign = System.Drawing.ContentAlignment.MiddleLeft,
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                Location = new System.Drawing.Point(12, 411),
                Size = new System.Drawing.Size(130, 50),
                Text = "        Sobre",
                UseVisualStyleBackColor = true,
                TabStop = false
            };
            this.tip.SetToolTip(this.btnSobre, "Sobre");
            this.btnSobre.FlatAppearance.BorderSize = 0;
            this.btnSobre.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSobre.Click += (s, e) => ClickBotao(true, pSobre, btnSobre);

            this.pSelected = new System.Windows.Forms.Panel
            {
                Location = new System.Drawing.Point(145, 7),
                Size = new System.Drawing.Size(15, 58),
                Height = btnGeradorDeck.Height,
                Top = btnGeradorDeck.Top,
                Cursor = System.Windows.Forms.Cursors.Hand
            };
            int y = 0; System.Drawing.Point point = new System.Drawing.Point(); bool f = false;
            this.pSelected.MouseDown += (s, e) =>
            {
                pSelected.Location = new System.Drawing.Point(pSelected.Location.X, pSelected.Location.Y + 1);
                pSelected.Size = new System.Drawing.Size(pSelected.Size.Width - 1, pSelected.Size.Height - 2);
                f = true; y = MousePosition.Y - pSelected.Location.Y;
            };
            this.pSelected.MouseMove += (s, e) =>
            {
                if (f)
                {
                    point = MousePosition;
                    point.X = pSelected.Location.X;
                    point.Y -= y;
                    if (point.Y >= 7 && point.Y <= btnSobre.Location.Y) { pSelected.Location = point; Seleciona(false); }
                    else if (point.Y < 7) ClickBotao(true, pGerador, btnGeradorDeck);
                    else if (point.Y > btnSobre.Location.Y) ClickBotao(true, pSobre, btnSobre);
                    System.Windows.Forms.Application.DoEvents();
                }
            };
            this.pSelected.MouseUp += (s, e) =>
            {
                f = false;
                Seleciona(true);
                pSelected.Location = new System.Drawing.Point(pSelected.Location.X, pSelected.Location.Y - 1);
                pSelected.Size = new System.Drawing.Size(pSelected.Size.Width + 1, pSelected.Size.Height + 2);
            };
            this.pSelected.MouseEnter += (s, e) =>
            {
                pSelected.Location = new System.Drawing.Point(pSelected.Location.X, pSelected.Location.Y - 1);
                pSelected.Size = new System.Drawing.Size(pSelected.Size.Width + 1, pSelected.Size.Height + 2);
            };
            this.pSelected.MouseLeave += (s, e) =>
            {
                pSelected.Location = new System.Drawing.Point(pSelected.Location.X, pSelected.Location.Y + 1);
                pSelected.Size = new System.Drawing.Size(pSelected.Size.Width - 1, pSelected.Size.Height - 2);
            };

            this.pOpcoes.SuspendLayout();
            this.pOpcoes.Controls.Add(picLinha1);
            this.pOpcoes.Controls.Add(picLinha2);
            this.pOpcoes.Controls.Add(picLinha3);
            this.pOpcoes.Controls.Add(picLinha4);
            this.pOpcoes.Controls.Add(picLinha5);
            this.pOpcoes.Controls.Add(picLinha6);
            this.pOpcoes.Controls.Add(picLinha7);
            this.pOpcoes.Controls.Add(picLinha8);
            this.pOpcoes.Controls.Add(picLinha9);
            this.pOpcoes.Controls.Add(picLinha10);
            this.pOpcoes.Controls.Add(this.btnConfig);
            this.pOpcoes.Controls.Add(this.btnDecksSalvos);
            this.pOpcoes.Controls.Add(this.btnMelhoresDecks);
            this.pOpcoes.Controls.Add(this.btnBalanceamento);
            this.pOpcoes.Controls.Add(this.btnAtualizador);
            this.pOpcoes.Controls.Add(this.btnSelecaoDeCartas);
            this.pOpcoes.Controls.Add(this.btnSobre);
            this.pOpcoes.Controls.Add(this.pSelected);
            this.pOpcoes.Controls.Add(this.btnGeradorDeck);
            this.pOpcoes.ResumeLayout(false);
            #endregion

            #region Gerador de Deck
            this.pGerador = new System.Windows.Forms.Panel
            {
                Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right,
                BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle,
                Location = new System.Drawing.Point(167, 29),
                Size = new System.Drawing.Size(766, 441),
            };
            
            this.CMSGerador = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.CMSGerador.SuspendLayout();
            System.Windows.Forms.ToolStripMenuItem ligarGerador = new System.Windows.Forms.ToolStripMenuItem() { Text = "Ligar Gerador de Deck" };
            ligarGerador.Click += (s, e) =>
            {
                if (!rodarDeck.Enabled)
                {
                    Classes.ArquivoRegras.Criar();
                    chkBuscarDeck.Enabled = false;
                    chkBuscarDeck.Checked = false;
                    buscarDeck.Stop();
                    ligarGerador.Checked = true;
                    CMSGerador.Items[0].Text = "Desligar Gerador de Deck";
                    rodarDeck.Start();
                }
                else
                {
                    chkBuscarDeck.Enabled = true;
                    ligarGerador.Checked = false;
                    CMSGerador.Items[0].Text = "Ligar Gerador de Deck";
                    rodarDeck.Stop();
                }
            };
            this.CMSGerador.Items.Add(ligarGerador);
            this.CMSGerador.Items.Add("-");
            this.CMSGerador.Items.Add("Copiar Deck");
            this.CMSGerador.Items[2].Image = Properties.Resources.copiar.ToBitmap();
            this.CMSGerador.Items[2].Click += (s, e) => CopiarDeck();
            this.CMSGerador.Items.Add("Colar Deck");
            this.CMSGerador.Items[3].Image = Properties.Resources.colar.ToBitmap();
            this.CMSGerador.Items[3].Click += (s, e) => ColarDeck();
            this.CMSGerador.Items.Add("Salvar Deck");
            this.CMSGerador.Items[4].Image = Properties.Resources.salvar.ToBitmap();
            this.CMSGerador.Items[4].Click += (s, e) => SalvarDeck();
            this.CMSGerador.Items.Add("Limpar Deck");
            this.CMSGerador.Items[5].Image = Properties.Resources.apagar.ToBitmap();
            this.CMSGerador.Items[5].Click += (s, e) => LimparDeck();
            this.CMSGerador.Items.Add("Embaralhar Deck");
            this.CMSGerador.Items[6].Image = Properties.Resources.atualizar.ToBitmap();
            this.CMSGerador.Items[6].Click += (s, e) => EmbaralharDeck();
            this.CMSGerador.ResumeLayout(false);

            this.TSIGerador = new System.Windows.Forms.ToolStripMenuItem { Text = "Menu" };
            this.TSIGerador.DropDownItems.Add("Copiar Deck");
            this.TSIGerador.DropDownItems[0].Image = Properties.Resources.copiar.ToBitmap();
            this.TSIGerador.DropDownItems[0].Click += (s, e) => { pGerador.Select(); CopiarDeck(); };
            this.TSIGerador.DropDownItems.Add("Colar Deck");
            this.TSIGerador.DropDownItems[1].Image = Properties.Resources.colar.ToBitmap();
            this.TSIGerador.DropDownItems[1].Click += (s, e) => { pGerador.Select(); ColarDeck(); };
            this.TSIGerador.DropDownItems.Add("Salvar Deck");
            this.TSIGerador.DropDownItems[2].Image = Properties.Resources.salvar.ToBitmap();
            this.TSIGerador.DropDownItems[2].Click += (s, e) => { pGerador.Select(); SalvarDeck(); };
            this.TSIGerador.DropDownItems.Add("Limpar Deck");
            this.TSIGerador.DropDownItems[3].Image = Properties.Resources.apagar.ToBitmap();
            this.TSIGerador.DropDownItems[3].Click += (s, e) => { pGerador.Select(); LimparDeck(); };
            this.TSIGerador.DropDownItems.Add("Embaralhar Deck");
            this.TSIGerador.DropDownItems[4].Image = Properties.Resources.atualizar.ToBitmap();
            this.TSIGerador.DropDownItems[4].Click += (s, e) => { pGerador.Select(); EmbaralharDeck(); };

            this.menuStripGerador = new System.Windows.Forms.MenuStrip
            {
                ForeColor = System.Drawing.Color.Black,
                Dock = System.Windows.Forms.DockStyle.None,
                Location = new System.Drawing.Point(0, 0),
                Text = "Menu"
            };
            this.menuStripGerador.SuspendLayout();
            this.menuStripGerador.Items.Add(TSIGerador);
            this.menuStripGerador.ResumeLayout(false);
            this.menuStripGerador.PerformLayout();

            this.picBau = new System.Windows.Forms.PictureBox
            {
                Anchor = System.Windows.Forms.AnchorStyles.Top,
                BackColor = System.Drawing.Color.Transparent,
                Cursor = System.Windows.Forms.Cursors.Hand,
                Image = Properties.Resources.bau.ToBitmap(),
                Location = new System.Drawing.Point(97, 6),
                Size = new System.Drawing.Size(27, 26),
                SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage,
                TabStop = false
            };
            ((System.ComponentModel.ISupportInitialize)(this.picBau)).BeginInit();
            this.tip.SetToolTip(this.picBau, "Ver mais variações de Decks do Meta");
            this.picBau.Click += (s, e) =>
            {
                byte arena = System.Convert.ToByte(System.Math.Abs(cbArena.SelectedIndex - 12));
                System.Diagnostics.Process.Start(string.Format("https://statsroyale.com/br/decks/popular?arena={0}", arena == 0 ? 1 : arena));
            };
            ((System.ComponentModel.ISupportInitialize)(this.picBau)).EndInit();

            this.picDica = new System.Windows.Forms.PictureBox
            {
                Anchor = System.Windows.Forms.AnchorStyles.Top,
                BackColor = System.Drawing.Color.Transparent,
                Cursor = System.Windows.Forms.Cursors.Hand,
                Image = Properties.Resources.dica.ToBitmap(),
                Location = new System.Drawing.Point(130, 6),
                Size = new System.Drawing.Size(27, 26),
                SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage,
                TabStop = false
            };
            ((System.ComponentModel.ISupportInitialize)(this.picDica)).BeginInit();
            this.tip.SetToolTip(this.picDica, "Dicas");
            this.picDica.Click += (s, e) =>
            {
                pGerador.Select();
                byte i = System.Convert.ToByte(_Random.Next(0, Classes.CartasDescInfo.Dica.Length));
                System.Windows.Forms.MessageBox.Show(Classes.CartasDescInfo.Dica[i] + '.', string.Format("Dica {0} de {1}", (i + 1).ToString().Length == 1 ? "0" + (i + 1) : (i + 1).ToString(), Classes.CartasDescInfo.Dica.Length), System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            };
            ((System.ComponentModel.ISupportInitialize)(this.picDica)).EndInit();

            this.picYouTube = new System.Windows.Forms.PictureBox
            {
                Anchor = System.Windows.Forms.AnchorStyles.Top,
                BackColor = System.Drawing.Color.Transparent,
                Cursor = System.Windows.Forms.Cursors.Hand,
                Image = Properties.Resources.youtube,
                Location = new System.Drawing.Point(163, 6),
                Size = new System.Drawing.Size(27, 26),
                SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage,
                TabStop = false
            };
            ((System.ComponentModel.ISupportInitialize)(this.picYouTube)).BeginInit();
            this.tip.SetToolTip(this.picYouTube, "YouTube");
            this.picYouTube.Click += (s, e) => System.Diagnostics.Process.Start("https://www.youtube.com/c/lucasnaja");
            ((System.ComponentModel.ISupportInitialize)(this.picYouTube)).EndInit();

            this.btnDiminuirArena = new System.Windows.Forms.Button
            {
                Anchor = System.Windows.Forms.AnchorStyles.Top,
                Cursor = System.Windows.Forms.Cursors.Hand,
                ForeColor = System.Drawing.Color.White,
                ImageAlign = System.Drawing.ContentAlignment.TopRight,
                Location = new System.Drawing.Point(9, 37),
                Size = new System.Drawing.Size(23, 24),
                Text = "-",
                Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0),
                UseVisualStyleBackColor = false,
                TabIndex = 0
            };
            this.tip.SetToolTip(this.btnDiminuirArena, "Diminuir Arena");
            this.btnDiminuirArena.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDiminuirArena.Click += (s, e) => cbArena.SelectedIndex++;

            this.cbArena = new System.Windows.Forms.ComboBox
            {
                Anchor = System.Windows.Forms.AnchorStyles.Top,
                Cursor = System.Windows.Forms.Cursors.Hand,
                DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList,
                FlatStyle = System.Windows.Forms.FlatStyle.Flat,
                Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0),
                ForeColor = System.Drawing.Color.White,
                FormattingEnabled = true,
                Location = new System.Drawing.Point(38, 37),
                Size = new System.Drawing.Size(155, 24),
                TabIndex = 1
            };
            this.cbArena.Items.AddRange(new string[] {
            "Arena 12",
            "Arena 11",
            "Arena 10",
            "Arena 9",
            "Arena 8",
            "Arena 7",
            "Arena 6",
            "Arena 5",
            "Arena 4",
            "Arena 3",
            "Arena 2",
            "Arena 1",
            "Campo de Treino"});
            this.cbArena.SelectedIndex = Properties.Settings.Default.arena;
            this.cbArena.SelectedIndexChanged += (s, e) =>
            {
                pGerador.Select();
                AtualizaArena();
                AtualizaListaCartas();
                CalcularMedia();
            };

            this.btnAumentarArena = new System.Windows.Forms.Button
            {
                Anchor = System.Windows.Forms.AnchorStyles.Top,
                Cursor = System.Windows.Forms.Cursors.Hand,
                Enabled = false,
                Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0),
                ForeColor = System.Drawing.Color.White,
                ImageAlign = System.Drawing.ContentAlignment.TopRight,
                Location = new System.Drawing.Point(199, 37),
                Size = new System.Drawing.Size(23, 24),
                Text = "+",
                UseVisualStyleBackColor = false,
                TabIndex = 2
            };
            this.tip.SetToolTip(this.btnAumentarArena, "Aumentar Arena");
            this.btnAumentarArena.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAumentarArena.Click += (s, e) => cbArena.SelectedIndex--;

            this.btnDiminuirRaridade = new System.Windows.Forms.Button
            {
                Anchor = System.Windows.Forms.AnchorStyles.Top,
                Cursor = System.Windows.Forms.Cursors.Hand,
                Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0),
                ForeColor = System.Drawing.Color.White,
                ImageAlign = System.Drawing.ContentAlignment.TopRight,
                Location = new System.Drawing.Point(9, 67),
                Size = new System.Drawing.Size(23, 24),
                Text = "-",
                UseVisualStyleBackColor = false,
                TabIndex = 3
            };
            this.btnDiminuirRaridade.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDiminuirRaridade.Click += (s, e) => cbRaridade.SelectedIndex++;
            this.tip.SetToolTip(this.btnDiminuirRaridade, "Diminuir Raridade");

            this.cbRaridade = new System.Windows.Forms.ComboBox
            {
                Anchor = System.Windows.Forms.AnchorStyles.Top,
                Cursor = System.Windows.Forms.Cursors.Hand,
                DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList,
                FlatStyle = System.Windows.Forms.FlatStyle.Flat,
                ForeColor = System.Drawing.Color.White,
                FormattingEnabled = true,
                Location = new System.Drawing.Point(38, 67),
                Size = new System.Drawing.Size(155, 24),
                TabIndex = 4
            };
            this.cbRaridade.Items.AddRange(new string[] { "Todas as Raridades", "Comuns", "Raras", "Épicas", "Lendárias" });
            this.cbRaridade.SelectedIndex = Properties.Settings.Default.raridade;
            this.cbRaridade.SelectedIndexChanged += (s, e) =>
            {
                pGerador.Select();
                AtualizaRaridade();
                AtualizaListaCartas();
                CalcularMedia();
            };

            this.btnAumentarRaridade = new System.Windows.Forms.Button
            {
                Anchor = System.Windows.Forms.AnchorStyles.Top,
                Cursor = System.Windows.Forms.Cursors.Hand,
                Enabled = false,
                Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0),
                ForeColor = System.Drawing.Color.White,
                ImageAlign = System.Drawing.ContentAlignment.TopRight,
                Location = new System.Drawing.Point(199, 67),
                Size = new System.Drawing.Size(23, 24),
                Text = "+",
                UseVisualStyleBackColor = false,
                TabIndex = 5
            };
            this.tip.SetToolTip(this.btnAumentarRaridade, "Aumentar Raridade");
            this.btnAumentarRaridade.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAumentarRaridade.Click += (s, e) => cbRaridade.SelectedIndex--;

            this.btnDiminuirTipo = new System.Windows.Forms.Button
            {
                Anchor = System.Windows.Forms.AnchorStyles.Top,
                Cursor = System.Windows.Forms.Cursors.Hand,
                Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0),
                ForeColor = System.Drawing.Color.White,
                ImageAlign = System.Drawing.ContentAlignment.TopRight,
                Location = new System.Drawing.Point(9, 97),
                Size = new System.Drawing.Size(23, 24),
                Text = "-",
                UseVisualStyleBackColor = false,
                TabIndex = 6
            };
            this.btnDiminuirTipo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDiminuirTipo.Click += (s, e) => cbTipo.SelectedIndex++;
            this.tip.SetToolTip(this.btnDiminuirTipo, "Diminuir Tipo");

            this.cbTipo = new System.Windows.Forms.ComboBox
            {
                Anchor = System.Windows.Forms.AnchorStyles.Top,
                Cursor = System.Windows.Forms.Cursors.Hand,
                DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList,
                FlatStyle = System.Windows.Forms.FlatStyle.Flat,
                ForeColor = System.Drawing.Color.White,
                FormattingEnabled = true,
                Location = new System.Drawing.Point(38, 97),
                Size = new System.Drawing.Size(155, 24),
                TabIndex = 7
            };
            this.cbTipo.Items.AddRange(new string[] { "Todos os Tipos", "Tropas", "Construções", "Feitiços" });
            this.cbTipo.SelectedIndex = Properties.Settings.Default.tipo;
            this.cbTipo.SelectedIndexChanged += (s, e) =>
            {
                pGerador.Select();
                AtualizaTipo();
                AtualizaListaCartas();
                CalcularMedia();
            };

            this.btnAumentarTipo = new System.Windows.Forms.Button
            {
                Anchor = System.Windows.Forms.AnchorStyles.Top,
                Cursor = System.Windows.Forms.Cursors.Hand,
                Enabled = false,
                Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0),
                ForeColor = System.Drawing.Color.White,
                ImageAlign = System.Drawing.ContentAlignment.TopRight,
                Location = new System.Drawing.Point(199, 97),
                Size = new System.Drawing.Size(23, 24),
                Text = "+",
                UseVisualStyleBackColor = false,
                TabIndex = 8
            };
            this.tip.SetToolTip(this.btnAumentarTipo, "Aumentar Tipo");
            this.btnAumentarTipo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAumentarTipo.Click += (s, e) => cbTipo.SelectedIndex--;

            this.lblMedia = new System.Windows.Forms.Label
            {
                Anchor = System.Windows.Forms.AnchorStyles.Top,
                AutoSize = true,
                BackColor = System.Drawing.Color.Transparent,
                ContextMenuStrip = this.CMSGerador,
                Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0),
                ForeColor = System.Drawing.Color.White,
                Location = new System.Drawing.Point(39, 124),
                Size = new System.Drawing.Size(129, 18),
                Text = "Elixir Médio: 0.0"
            };
            this.lblMedia.Click += (s, e) => pOpcoes.Select();

            this.lbl1 = new System.Windows.Forms.Label
            {
                Anchor = System.Windows.Forms.AnchorStyles.Top,
                AutoSize = true,
                BackColor = System.Drawing.Color.Transparent,
                ContextMenuStrip = this.CMSGerador,
                Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0),
                ForeColor = System.Drawing.Color.White,
                Location = new System.Drawing.Point(7, 147),
                Size = new System.Drawing.Size(117, 18),
                Text = "< Informações >"
            };
            this.lbl1.Click += (s, e) => pOpcoes.Select();

            this.lblInformacoes = new System.Windows.Forms.Label
            {
                Anchor = System.Windows.Forms.AnchorStyles.Top,
                AutoSize = true,
                BackColor = System.Drawing.Color.Transparent,
                ContextMenuStrip = this.CMSGerador,
                Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0),
                ForeColor = System.Drawing.Color.White,
                Location = new System.Drawing.Point(5, 169),
                Size = new System.Drawing.Size(179, 16),
                Text = "Nenhuma Carta selecionada"
            };
            this.lblInformacoes.Click += (s, e) => pOpcoes.Select();

            this.Carta1 = new System.Windows.Forms.PictureBox
            {
                Anchor = System.Windows.Forms.AnchorStyles.Top,
                BackColor = System.Drawing.Color.Transparent,
                Cursor = System.Windows.Forms.Cursors.Hand,
                Image = Properties.Resources.nova_carta_clash_royale,
                Location = new System.Drawing.Point(230, 8),
                Size = new System.Drawing.Size(124, 154),
                SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage,
                TabStop = false
            };
            ((System.ComponentModel.ISupportInitialize)(this.Carta1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Carta1)).EndInit();

            this.Carta2 = new System.Windows.Forms.PictureBox
            {
                Anchor = System.Windows.Forms.AnchorStyles.Top,
                BackColor = System.Drawing.Color.Transparent,
                Cursor = System.Windows.Forms.Cursors.Hand,
                Image = Properties.Resources.nova_carta_clash_royale,
                Location = new System.Drawing.Point(362, 8),
                Size = new System.Drawing.Size(127, 154),
                SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage,
                TabStop = false
            };
            ((System.ComponentModel.ISupportInitialize)(this.Carta2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Carta2)).EndInit();

            this.Carta3 = new System.Windows.Forms.PictureBox
            {
                Anchor = System.Windows.Forms.AnchorStyles.Top,
                BackColor = System.Drawing.Color.Transparent,
                Cursor = System.Windows.Forms.Cursors.Hand,
                Image = Properties.Resources.nova_carta_clash_royale,
                Location = new System.Drawing.Point(496, 8),
                Size = new System.Drawing.Size(124, 154),
                SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage,
                TabStop = false
            };
            ((System.ComponentModel.ISupportInitialize)(this.Carta3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Carta3)).EndInit();

            this.Carta4 = new System.Windows.Forms.PictureBox
            {
                Anchor = System.Windows.Forms.AnchorStyles.Top,
                BackColor = System.Drawing.Color.Transparent,
                Cursor = System.Windows.Forms.Cursors.Hand,
                Image = Properties.Resources.nova_carta_clash_royale,
                Location = new System.Drawing.Point(629, 8),
                Size = new System.Drawing.Size(124, 154),
                SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage,
                TabStop = false
            };
            ((System.ComponentModel.ISupportInitialize)(this.Carta4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Carta4)).EndInit();

            this.Carta5 = new System.Windows.Forms.PictureBox
            {
                Anchor = System.Windows.Forms.AnchorStyles.Top,
                BackColor = System.Drawing.Color.Transparent,
                Cursor = System.Windows.Forms.Cursors.Hand,
                Image = Properties.Resources.nova_carta_clash_royale,
                Location = new System.Drawing.Point(230, 171),
                Size = new System.Drawing.Size(124, 154),
                SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage,
                TabStop = false
            };
            ((System.ComponentModel.ISupportInitialize)(this.Carta5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Carta5)).EndInit();

            this.Carta6 = new System.Windows.Forms.PictureBox
            {
                Anchor = System.Windows.Forms.AnchorStyles.Top,
                BackColor = System.Drawing.Color.Transparent,
                Cursor = System.Windows.Forms.Cursors.Hand,
                Image = Properties.Resources.nova_carta_clash_royale,
                Location = new System.Drawing.Point(363, 171),
                Size = new System.Drawing.Size(124, 154),
                SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage,
                TabStop = false
            };
            ((System.ComponentModel.ISupportInitialize)(this.Carta6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Carta6)).EndInit();

            this.Carta7 = new System.Windows.Forms.PictureBox
            {
                Anchor = System.Windows.Forms.AnchorStyles.Top,
                BackColor = System.Drawing.Color.Transparent,
                Cursor = System.Windows.Forms.Cursors.Hand,
                Image = Properties.Resources.nova_carta_clash_royale,
                Location = new System.Drawing.Point(496, 171),
                Size = new System.Drawing.Size(124, 154),
                SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage,
                TabStop = false
            };
            ((System.ComponentModel.ISupportInitialize)(this.Carta7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Carta7)).EndInit();

            this.Carta8 = new System.Windows.Forms.PictureBox
            {
                Anchor = System.Windows.Forms.AnchorStyles.Top,
                BackColor = System.Drawing.Color.Transparent,
                Cursor = System.Windows.Forms.Cursors.Hand,
                Image = Properties.Resources.nova_carta_clash_royale,
                Location = new System.Drawing.Point(629, 171),
                Size = new System.Drawing.Size(124, 154),
                SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage,
                TabStop = false
            };
            ((System.ComponentModel.ISupportInitialize)(this.Carta8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Carta8)).EndInit();

            this.btnCopiarDeck = new System.Windows.Forms.Button
            {
                Cursor = System.Windows.Forms.Cursors.Hand,
                Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0),
                ForeColor = System.Drawing.Color.White,
                Location = new System.Drawing.Point(12, 18),
                Size = new System.Drawing.Size(110, 39),
                Text = "Copiar Deck",
                UseVisualStyleBackColor = false,
                TabIndex = 12
            };
            this.tip.SetToolTip(this.btnCopiarDeck, "Copiar Deck atual");
            this.btnCopiarDeck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCopiarDeck.Click += (s, e) => { pGerador.Select(); CopiarDeck(); };

            this.btnSalvarDeck = new System.Windows.Forms.Button
            {
                Cursor = System.Windows.Forms.Cursors.Hand,
                Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0),
                ForeColor = System.Drawing.Color.White,
                Location = new System.Drawing.Point(12, 57),
                Size = new System.Drawing.Size(110, 39),
                Text = "Salvar Deck",
                UseVisualStyleBackColor = false,
                TabIndex = 13
            };
            this.tip.SetToolTip(this.btnSalvarDeck, "Salvar Deck atual");
            this.btnSalvarDeck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalvarDeck.Click += (s, e) => { pGerador.Select(); SalvarDeck(); };

            this.chkBuscarDeck = new System.Windows.Forms.CheckBox
            {
                AutoSize = true,
                BackColor = System.Drawing.Color.Transparent,
                Cursor = System.Windows.Forms.Cursors.Hand,
                ForeColor = System.Drawing.Color.White,
                Location = new System.Drawing.Point(165, 37),
                RightToLeft = System.Windows.Forms.RightToLeft.Yes,
                Size = new System.Drawing.Size(99, 17),
                Text = "Buscar Deck",
                UseVisualStyleBackColor = false,
                TabIndex = 14
            };
            this.chkBuscarDeck.Click += (s, e) =>
            {
                if (string.Format("{0:f1}", media) == string.Format("{0:f1}", valorMedia.Value)) GerarDeck();
                if (chkBuscarDeck.Checked) { CMSGerador.Items[0].Enabled = false; rodarDeck.Stop(); buscarDeck.Start(); }
                else { CMSGerador.Items[0].Enabled = true; buscarDeck.Stop(); }
            };

            this.valorMedia = new System.Windows.Forms.NumericUpDown
            {
                Cursor = System.Windows.Forms.Cursors.Hand,
                DecimalPlaces = 1,
                ForeColor = System.Drawing.Color.White,
                Increment = 0.1M,
                Location = new System.Drawing.Point(270, 35),
                Maximum = 7.1M,
                Minimum = 1.7M,
                Size = new System.Drawing.Size(46, 20),
                TextAlign = System.Windows.Forms.HorizontalAlignment.Center,
                Value = 4.4M,
                TabIndex = 15
            };
            ((System.ComponentModel.ISupportInitialize)(this.valorMedia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.valorMedia)).EndInit();

            this.ckGInteligente = new System.Windows.Forms.CheckBox
            {
                AutoSize = true,
                BackColor = System.Drawing.Color.Transparent,
                Cursor = System.Windows.Forms.Cursors.Hand,
                Location = new System.Drawing.Point(170, 62),
                Size = new System.Drawing.Size(187, 17),
                Text = "Gerador de Deck Inteligente",
                UseVisualStyleBackColor = false,
                TabIndex = 16
            };
            this.ckGInteligente.CheckedChanged += (s, e) =>
            {
                if (ckGInteligente.Checked)
                {
                    cbArena.Enabled = false;
                    cbRaridade.Enabled = false;
                    cbTipo.Enabled = false;
                    tip.SetToolTip(btnGerar, "Gerar Deck Inteligente");
                }
                else
                {
                    cbArena.Enabled = true;
                    cbRaridade.Enabled = true;
                    cbTipo.Enabled = true;
                    tip.SetToolTip(btnGerar, "Gerar Deck");
                }
            };

            this.btnVoltarDeck = new System.Windows.Forms.Button
            {
                Cursor = System.Windows.Forms.Cursors.Hand,
                Enabled = false,
                Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0),
                ForeColor = System.Drawing.Color.White,
                Location = new System.Drawing.Point(374, 32),
                Size = new System.Drawing.Size(23, 47),
                Text = "<",
                UseVisualStyleBackColor = false,
                TabIndex = 11
            };
            this.tip.SetToolTip(this.btnVoltarDeck, "Voltar ao Deck anterior");
            this.btnVoltarDeck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVoltarDeck.Click += (s, e) =>
            {
                pGerador.Select();
                btnVoltarDeck.Enabled = false;
                media = 0.0f;
                for (byte i = 0; i < Cartas.Length; i++)
                {
                    Cartas[i].Image = _Deck.CartasImagem[_Deck.deckAnterior[i]];
                    string txt = _Deck.CartasInformacao[_Deck.deckAnterior[i]].Split('\n')[0];
                    if (ckNome.Checked)
                        tip.SetToolTip(Cartas[i], txt == "Nenhuma Carta selecionada" ? "Carta Inexistente" : txt);
                    media += _Deck.CustoElixir[_Deck.deckAnterior[i]] / 8;
                    _Deck.deckAtual[i] = _Deck.deckAnterior[i];
                }
                lblMedia.Text = string.Format("Elixir Médio: {0:f1}", media).Replace(',', '.');
            };

            this.btnGerar = new System.Windows.Forms.Button
            {
                Cursor = System.Windows.Forms.Cursors.Hand,
                Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0),
                ForeColor = System.Drawing.Color.White,
                Location = new System.Drawing.Point(397, 32),
                Size = new System.Drawing.Size(127, 47),
                Text = "&Gerar Deck",
                UseVisualStyleBackColor = false,
                TabIndex = 10
            };
            this.tip.SetToolTip(this.btnGerar, "Gerar Deck");
            this.btnGerar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGerar.Click += (s, e) =>
            {
                pGerador.Select();
                Classes.ArquivoRegras.Criar();
                GerarDeck();
            };

            this.gbFuncoes = new System.Windows.Forms.GroupBox
            {
                Anchor = System.Windows.Forms.AnchorStyles.Top,
                BackColor = System.Drawing.Color.Transparent,
                Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0),
                ForeColor = System.Drawing.Color.White,
                Location = new System.Drawing.Point(221, 329),
                Size = new System.Drawing.Size(536, 104),
                TabStop = false,
                Text = "Funções",
                TabIndex = 9
            };
            this.gbFuncoes.SuspendLayout();
            this.gbFuncoes.Controls.Add(this.ckGInteligente);
            this.gbFuncoes.Controls.Add(this.btnCopiarDeck);
            this.gbFuncoes.Controls.Add(this.valorMedia);
            this.gbFuncoes.Controls.Add(this.btnSalvarDeck);
            this.gbFuncoes.Controls.Add(this.chkBuscarDeck);
            this.gbFuncoes.Controls.Add(this.btnVoltarDeck);
            this.gbFuncoes.Controls.Add(this.btnGerar);
            gbFuncoes.Click += (s, e) => pOpcoes.Select();
            this.gbFuncoes.ResumeLayout(false);
            this.gbFuncoes.PerformLayout();

            this.pGerador.ContextMenuStrip = CMSGerador;
            this.pGerador.SuspendLayout();
            this.pGerador.Controls.Add(this.Carta1);
            this.pGerador.Controls.Add(this.Carta2);
            this.pGerador.Controls.Add(this.Carta3);
            this.pGerador.Controls.Add(this.Carta4);
            this.pGerador.Controls.Add(this.Carta5);
            this.pGerador.Controls.Add(this.Carta6);
            this.pGerador.Controls.Add(this.Carta7);
            this.pGerador.Controls.Add(this.Carta8);
            this.pGerador.Controls.Add(this.gbFuncoes);
            this.pGerador.Controls.Add(this.btnDiminuirRaridade);
            this.pGerador.Controls.Add(this.btnAumentarRaridade);
            this.pGerador.Controls.Add(this.lblMedia);
            this.pGerador.Controls.Add(this.btnDiminuirArena);
            this.pGerador.Controls.Add(this.btnAumentarArena);
            this.pGerador.Controls.Add(this.btnDiminuirTipo);
            this.pGerador.Controls.Add(this.cbTipo);
            this.pGerador.Controls.Add(this.btnAumentarTipo);
            this.pGerador.Controls.Add(this.picBau);
            this.pGerador.Controls.Add(this.picDica);
            this.pGerador.Controls.Add(this.picYouTube);
            this.pGerador.Controls.Add(this.cbRaridade);
            this.pGerador.Controls.Add(this.lblInformacoes);
            this.pGerador.Controls.Add(this.lbl1);
            this.pGerador.Controls.Add(this.cbArena);
            this.pGerador.Controls.Add(this.menuStripGerador);
            this.pGerador.Click += (s, e) => pGerador.Select();
            this.pGerador.ResumeLayout(false);
            this.pGerador.PerformLayout();
            #endregion

            #region Atualizador
            this.pAtualizador = new System.Windows.Forms.Panel
            {
                Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right,
                BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle,
                Location = new System.Drawing.Point(167, 29),
                Size = new System.Drawing.Size(766, 441),
            };

            this.lbl13 = new System.Windows.Forms.Label
            {
                Anchor = System.Windows.Forms.AnchorStyles.Top,
                AutoSize = true,
                BackColor = System.Drawing.Color.Transparent,
                Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0),
                Size = new System.Drawing.Size(228, 25),
                Text = "Versão atual: 3.0 Final"
            };
            lbl13.Location = new System.Drawing.Point((pAtualizador.Size.Width - lbl13.Size.Width) / 2, 6);
            this.lbl13.Click += (s, e) => pAtualizador.Select();

            this.lblBuild = new System.Windows.Forms.Label
            {
                Anchor = System.Windows.Forms.AnchorStyles.Top,
                AutoSize = true,
                BackColor = System.Drawing.Color.Transparent,
                Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0),
                Size = new System.Drawing.Size(80, 25),
                Text = "Build " + bAtual
            };
            lblBuild.Location = new System.Drawing.Point((pAtualizador.Size.Width - lblBuild.Size.Width) / 2, 37);
            this.lblBuild.Click += (s, e) => pAtualizador.Select();

            this.btnAtualizar = new System.Windows.Forms.Button
            {
                Anchor = System.Windows.Forms.AnchorStyles.Top,
                Cursor = System.Windows.Forms.Cursors.Hand,
                Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0),
                ForeColor = System.Drawing.Color.White,
                Size = new System.Drawing.Size(244, 39),
                Text = "&Verificar atualização",
                UseVisualStyleBackColor = false
            };
            btnAtualizar.Location = new System.Drawing.Point((pAtualizador.Size.Width - btnAtualizar.Size.Width) / 2, 73);
            this.tip.SetToolTip(this.btnAtualizar, "Verificar atualização");
            this.btnAtualizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAtualizar.Click += (s, e) =>
            {
                pAtualizador.Select();
                btnAtualizar.Enabled = false;
                emAtualizacao = true;
                System.Net.WebClient wc = new System.Net.WebClient();
                if (btnAtualizar.Text == "&Verificar atualização")
                {
                    lblStatus.Text = "Status: Verificando atualização";
                    picGIF.Show();
                    wc.DownloadStringAsync(new System.Uri("https://drive.google.com/uc?authuser=0&id=19G-ctY2o81C-OtBev-YxUxonMX_Esf8g&export=download"));
                    wc.DownloadStringCompleted += (ss, ee) =>
                    {
                        try
                        {
                            if (ee.Result.Split('\n').Length > 0)
                            {
                                infos = ee.Result.Split('\n');
                                byte bAtualizada = System.Convert.ToByte(infos[0].Trim());
                                lblUBuild.Text = string.Format("Última Build: {0}", bAtualizada);
                                if (bAtual < bAtualizada)
                                {
                                    SwitchToThisWindow(this.Handle);
                                    nIcon.BalloonTipClicked += (sss, eee) => { SwitchToThisWindow(this.Handle); ClickBotao(true, pAtualizador, btnAtualizador); };
                                    nIcon.BalloonTipText = "Existe uma nova Build disponível para download.";
                                    nIcon.ShowBalloonTip(200);
                                    lblStatus.Text = "Status: Existe uma nova Build disponível para download.";
                                    btnAtualizar.Text = "&Baixar atualização";
                                    tip.SetToolTip(btnAtualizar, "Baixar nova atualização");
                                }
                                else if (bAtual > bAtualizada) lblStatus.Text = "Status: Seja bem-vindo, Desenvolvedor. <3 Nova Build saindo fresquinha!";
                                else lblStatus.Text = "Status: Não há nova Build disponível para download";
                            }
                        }
                        catch { lblStatus.Text = "Status: Verifique sua conexão com a internet"; wc.Dispose(); }
                        finally
                        {
                            picGIF.Hide();
                            btnAtualizar.Enabled = true;
                            emAtualizacao = false;
                            wc.Dispose();
                        }
                    };
                }
                else if (btnAtualizar.Text == "&Baixar atualização")
                {
                    picGIF.Show();
                    lblStatus.Text = "Status: Conectando-se ao Servidor";
                    wc.DownloadFileAsync(new System.Uri(infos[1].Trim()), string.Format("{0}\\Gerador de Deck - Build {1}.rar", System.Windows.Forms.Application.StartupPath, infos[0].Trim()));
                    emAtualizacao = true;
                    wc.DownloadProgressChanged += (ss, ee) =>
                    {
                        try { lblStatus.Text = string.Format("Status: Baixando Build {2} do Gerador de Deck - {0}KB de {1}KB", ee.BytesReceived / 1024, infos[2].Trim(), infos[0].Trim()); }
                        catch { lblStatus.Text = "Status: Verifique sua conexão com a internet"; wc.Dispose(); picGIF.Hide(); btnAtualizar.Enabled = true; }
                    };
                    wc.DownloadFileCompleted += (ss, ee) =>
                    {
                        try
                        {
                            picGIF.Hide();
                            lblStatus.Cursor = System.Windows.Forms.Cursors.Hand;
                            SwitchToThisWindow(this.Handle);
                            ClickBotao(true, pAtualizador, btnAtualizador);
                            lblStatus.Text = "Status: Download concluído. Extraia a nova versão do Gerador na pasta do\nexecutável. Clique aqui!";
                            lblStatus.Click += (sss, eee) =>
                            {
                                try { System.Diagnostics.Process.Start(string.Format("{0}\\Gerador de Deck - Build {1}.rar", System.Windows.Forms.Application.StartupPath, infos[0].Trim())); }
                                finally { System.Environment.Exit(0); }
                            };
                        }
                        finally { wc.Dispose(); }
                    };
                }
            };

            this.picGIF = new System.Windows.Forms.PictureBox
            {
                Anchor = System.Windows.Forms.AnchorStyles.Top,
                BackColor = System.Drawing.Color.Transparent,
                Image = Properties.Resources.gifEscuro,
                Location = new System.Drawing.Point(btnAtualizar.Location.X + btnAtualizar.Size.Width + 7, 73),
                Size = new System.Drawing.Size(46, 39),
                SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage,
                TabStop = false
            };
            ((System.ComponentModel.ISupportInitialize)(this.picGIF)).BeginInit();
            this.tip.SetToolTip(this.picGIF, "Carregando");
            this.picGIF.Visible = false;
            this.picGIF.Click += (s, e) => pAtualizador.Select();
            ((System.ComponentModel.ISupportInitialize)(this.picGIF)).EndInit();

            this.lblUBuild = new System.Windows.Forms.Label
            {
                Anchor = System.Windows.Forms.AnchorStyles.Top,
                AutoSize = true,
                BackColor = System.Drawing.Color.Transparent,
                Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0),
                Size = new System.Drawing.Size(255, 25),
                Text = "Última Build: Aguardando"
            };
            lblUBuild.Location = new System.Drawing.Point((pAtualizador.Size.Width - lblUBuild.Size.Width) / 2, 119);
            this.lblUBuild.Click += (s, e) => pAtualizador.Select();

            this.lblStatus = new System.Windows.Forms.Label
            {
                AutoSize = true,
                BackColor = System.Drawing.Color.Transparent,
                FlatStyle = System.Windows.Forms.FlatStyle.Flat,
                Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0),
                Location = new System.Drawing.Point(11, 154),
                Size = new System.Drawing.Size(268, 25),
                Text = "Status: Aguardando ordem"
            };

            this.lblV1 = new System.Windows.Forms.Label
            {
                Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left,
                AutoSize = true,
                BackColor = System.Drawing.Color.Transparent,
                Cursor = System.Windows.Forms.Cursors.Hand,
                FlatStyle = System.Windows.Forms.FlatStyle.Flat,
                Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0),
                Location = new System.Drawing.Point(11, 343),
                Size = new System.Drawing.Size(218, 25),
                Text = "- Baixar V1.0 (8.4MB)"
            };
            this.tip.SetToolTip(this.lblV1, "Baixar Gerador de Deck Versão 1.0");
            this.lblV1.Click += (s, e) => BaixarGerador("https://drive.google.com/uc?authuser=0&id=17A-wYD-6cdJO-gTQl0LWTFC94sJuI1DN&export=download", "8440", "1.0");

            this.lblV2 = new System.Windows.Forms.Label
            {
                Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left,
                AutoSize = true,
                BackColor = System.Drawing.Color.Transparent,
                Cursor = System.Windows.Forms.Cursors.Hand,
                FlatStyle = System.Windows.Forms.FlatStyle.Flat,
                Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0),
                Location = new System.Drawing.Point(11, 374),
                Size = new System.Drawing.Size(218, 25),
                Text = "- Baixar V2.0 (3.1MB)"
            };
            this.tip.SetToolTip(this.lblV2, "Baixar Gerador de Deck Versão 2.0");
            this.lblV2.Click += (s, e) => BaixarGerador("https://drive.google.com/uc?authuser=0&id=1G0F1uH-jQ51O1kHdjuBsyZcLMvnTpU6C&export=download", "3181", "2.0");

            this.lblV25 = new System.Windows.Forms.Label
            {
                Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left,
                AutoSize = true,
                BackColor = System.Drawing.Color.Transparent,
                Cursor = System.Windows.Forms.Cursors.Hand,
                FlatStyle = System.Windows.Forms.FlatStyle.Flat,
                Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0),
                Location = new System.Drawing.Point(11, 406),
                Size = new System.Drawing.Size(200, 25),
                Text = "- Baixar V2.5 (4MB)"
            };
            this.tip.SetToolTip(this.lblV25, "Baixar Gerador de Deck Versão 2.5");
            this.lblV25.Click += (s, e) => BaixarGerador("https://drive.google.com/uc?authuser=0&id=1uSono0pTD5BfC9Wy2M6XOLzdltuhHh6x&export=download", "4079", "2.5");

            this.pAtualizador.SuspendLayout();
            this.pAtualizador.Controls.Add(this.lblV25);
            this.pAtualizador.Controls.Add(this.lblV2);
            this.pAtualizador.Controls.Add(this.lblV1);
            this.pAtualizador.Controls.Add(this.lblStatus);
            this.pAtualizador.Controls.Add(this.picGIF);
            this.pAtualizador.Controls.Add(this.lblUBuild);
            this.pAtualizador.Controls.Add(this.btnAtualizar);
            this.pAtualizador.Controls.Add(this.lblBuild);
            this.pAtualizador.Controls.Add(this.lbl13);
            this.pAtualizador.Click += (s, e) => pAtualizador.Select();
            this.pAtualizador.ResumeLayout(false);
            this.pAtualizador.PerformLayout();
            #endregion                      

            #region Sobre
            this.pSobre = new System.Windows.Forms.Panel
            {
                Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right,
                BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle,
                Location = new System.Drawing.Point(167, 29),
                Size = new System.Drawing.Size(766, 441),
            };

            this.lbl2 = new System.Windows.Forms.Label
            {
                Anchor = System.Windows.Forms.AnchorStyles.Top,
                AutoSize = true,
                BackColor = System.Drawing.Color.Transparent,
                Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0),
                Size = new System.Drawing.Size(392, 25),
                Text = "Programa Desenvolvido por Lucas Naja"
            };
            lbl2.Location = new System.Drawing.Point((pSobre.Size.Width - lbl2.Size.Width) / 2, 9);
            this.lbl2.Click += (s, e) => System.Diagnostics.Process.Start("https://www.youtube.com/c/lucasnaja");

            this.lbl3 = new System.Windows.Forms.Label
            {
                Anchor = System.Windows.Forms.AnchorStyles.Top,
                AutoSize = true,
                BackColor = System.Drawing.Color.Transparent,
                Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0),
                Size = new System.Drawing.Size(228, 25),
                Text = "Versão atual: 3.0 Final"
            };
            lbl3.Location = new System.Drawing.Point((pSobre.Size.Width - lbl3.Size.Width) / 2, 34);
            this.lbl3.Click += (s, e) => pSobre.Select();

            this.lblBuild2 = new System.Windows.Forms.Label
            {
                Anchor = System.Windows.Forms.AnchorStyles.Top,
                AutoSize = true,
                BackColor = System.Drawing.Color.Transparent,
                Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0),
                Size = new System.Drawing.Size(80, 25),
                Text = "Build " + bAtual
            };
            lblBuild2.Location = new System.Drawing.Point((pSobre.Size.Width - lblBuild2.Size.Width) / 2, 59);
            this.lblBuild2.Click += (s, e) => pSobre.Select();

            this.picCanal = new System.Windows.Forms.PictureBox
            {
                Anchor = System.Windows.Forms.AnchorStyles.Top,
                BackColor = System.Drawing.Color.Transparent,
                Cursor = System.Windows.Forms.Cursors.Default,
                Image = Properties.Resources.Logo,
                Location = new System.Drawing.Point(159, 94),
                Size = new System.Drawing.Size(213, 224),
                SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage,
                TabStop = false
            };
            ((System.ComponentModel.ISupportInitialize)(this.picCanal)).BeginInit();
            this.picCanal.Click += (s, e) => System.Diagnostics.Process.Start("https://www.youtube.com/c/lucasnaja");
            ((System.ComponentModel.ISupportInitialize)(this.picCanal)).EndInit();

            this.lbl6 = new System.Windows.Forms.Label
            {
                Anchor = System.Windows.Forms.AnchorStyles.Top,
                AutoSize = true,
                BackColor = System.Drawing.Color.Transparent,
                Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0),
                Location = new System.Drawing.Point(377, 113),
                Size = new System.Drawing.Size(191, 25),
                Text = "• Totalmente grátis"
            };
            this.lbl6.Click += (s, e) => pSobre.Select();

            this.lbl7 = new System.Windows.Forms.Label
            {
                Anchor = System.Windows.Forms.AnchorStyles.Top,
                AutoSize = true,
                BackColor = System.Drawing.Color.Transparent,
                Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0),
                Location = new System.Drawing.Point(377, 145),
                Size = new System.Drawing.Size(247, 25),
                Text = "• O melhor e mais rápido"
            };
            this.lbl7.Click += (s, e) => pSobre.Select();

            this.lbl8 = new System.Windows.Forms.Label
            {
                Anchor = System.Windows.Forms.AnchorStyles.Top,
                AutoSize = true,
                BackColor = System.Drawing.Color.Transparent,
                Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0),
                Location = new System.Drawing.Point(377, 177),
                Size = new System.Drawing.Size(200, 25),
                Text = "• Sem propagandas"
            };
            this.lbl8.Click += (s, e) => pSobre.Select();

            this.lbl9 = new System.Windows.Forms.Label
            {
                Anchor = System.Windows.Forms.AnchorStyles.Top,
                AutoSize = true,
                BackColor = System.Drawing.Color.Transparent,
                Cursor = System.Windows.Forms.Cursors.Hand,
                FlatStyle = System.Windows.Forms.FlatStyle.Flat,
                Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0),
                Location = new System.Drawing.Point(377, 209),
                Size = new System.Drawing.Size(160, 25),
                Text = "• &Código aberto"
            };
            this.lbl9.Click += (s, e) => System.Diagnostics.Process.Start("https://github.com/LucasNaja/DeckGenerator");

            this.lbl10 = new System.Windows.Forms.Label
            {
                Anchor = System.Windows.Forms.AnchorStyles.Top,
                AutoSize = true,
                BackColor = System.Drawing.Color.Transparent,
                Cursor = System.Windows.Forms.Cursors.Hand,
                Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0),
                Location = new System.Drawing.Point(377, 241),
                Size = new System.Drawing.Size(248, 25),
                Text = "• &Sistema de atualização"
            };
            this.lbl10.Click += (s, e) => ClickBotao(true, pAtualizador, btnAtualizador);

            this.lbl11 = new System.Windows.Forms.Label
            {
                Anchor = System.Windows.Forms.AnchorStyles.Top,
                AutoSize = true,
                BackColor = System.Drawing.Color.Transparent,
                Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0),
                Location = new System.Drawing.Point(378, 270),
                Size = new System.Drawing.Size(262, 25),
                Text = "• Ferramentas atualizadas"
            };
            this.lbl11.Click += (s, e) => pSobre.Select();

            this.lbl12 = new System.Windows.Forms.Label
            {
                Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left,
                AutoSize = true,
                BackColor = System.Drawing.Color.Transparent,
                Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0),
                Location = new System.Drawing.Point(11, 374),
                Size = new System.Drawing.Size(618, 25),
                Text = "Contato: lucasnaja0@gmail.com (Sugestões, bugs, ideias, etc.)"
            };
            this.lbl12.Click += (s, e) => pSobre.Select();

            this.lbl5 = new System.Windows.Forms.Label
            {
                Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left,
                AutoSize = true,
                BackColor = System.Drawing.Color.Transparent,
                Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0),
                Location = new System.Drawing.Point(11, 405),
                Size = new System.Drawing.Size(357, 25),
                Text = "Versão atual do Clash Royale: 2.2.3"
            };
            this.lbl5.Click += (s, e) => pSobre.Select();

            this.picAtalho = new System.Windows.Forms.PictureBox()
            {
                Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right,
                BackColor = System.Drawing.Color.Transparent,
                Cursor = System.Windows.Forms.Cursors.Hand,
                Image = Properties.Resources.atalho.ToBitmap(),
                Location = new System.Drawing.Point(596, 407),
                Size = new System.Drawing.Size(27, 26),
                SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage,
                TabStop = false
            };
            ((System.ComponentModel.ISupportInitialize)(this.picAtalho)).BeginInit();
            this.tip.SetToolTip(this.picAtalho, "Teclas de Atalho");
            this.picAtalho.Click += (s, e) =>
            {
                pSobre.Select();
                byte i = 0; System.Windows.Forms.DialogResult dialog;
                do dialog = System.Windows.Forms.MessageBox.Show(Classes.CartasDescInfo.Atalhos[i] + '.' + System.Environment.NewLine + "Deseja ir para o próximo atalho?", string.Format("Atalho {0} de 0{1}", (i + 1).ToString().Length == 1 ? "0" + (++i) : (++i).ToString(), Classes.CartasDescInfo.Atalhos.Length), System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Information);
                while (dialog == System.Windows.Forms.DialogResult.Yes && i < Classes.CartasDescInfo.Atalhos.Length - 1);
                if (dialog == System.Windows.Forms.DialogResult.Yes) System.Windows.Forms.MessageBox.Show(Classes.CartasDescInfo.Atalhos[Classes.CartasDescInfo.Atalhos.Length - 1] + '.', "Atalho 0" + Classes.CartasDescInfo.Atalhos.Length + " de 0" + Classes.CartasDescInfo.Atalhos.Length, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            };
            ((System.ComponentModel.ISupportInitialize)(this.picAtalho)).EndInit();

            this.picGitHub = new System.Windows.Forms.PictureBox
            {
                Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right,
                BackColor = System.Drawing.Color.Transparent,
                Cursor = System.Windows.Forms.Cursors.Hand,
                Image = Properties.Resources.github.ToBitmap(),
                Location = new System.Drawing.Point(629, 404),
                Size = new System.Drawing.Size(27, 26),
                SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage,
                TabStop = false
            };
            ((System.ComponentModel.ISupportInitialize)(this.picGitHub)).BeginInit();
            this.tip.SetToolTip(this.picGitHub, "GitHub");
            this.picGitHub.Click += (s, e) => System.Diagnostics.Process.Start("https://github.com/LucasNaja/DeckGenerator");
            ((System.ComponentModel.ISupportInitialize)(this.picGitHub)).EndInit();

            this.picBau2 = new System.Windows.Forms.PictureBox
            {
                Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right,
                BackColor = System.Drawing.Color.Transparent,
                Cursor = System.Windows.Forms.Cursors.Hand,
                Image = Properties.Resources.bau.ToBitmap(),
                Location = new System.Drawing.Point(662, 404),
                Size = new System.Drawing.Size(27, 26),
                SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage,
                TabStop = false
            };
            ((System.ComponentModel.ISupportInitialize)(this.picBau2)).BeginInit();
            this.tip.SetToolTip(this.picBau2, "Ver mais variações de Decks do Meta");
            this.picBau2.Click += (s, e) =>
            {
                byte arena = System.Convert.ToByte(System.Math.Abs(cbArena.SelectedIndex - 12));
                System.Diagnostics.Process.Start(string.Format("https://statsroyale.com/br/decks/popular?arena={0}", arena == 0 ? 1 : arena));
            };
            ((System.ComponentModel.ISupportInitialize)(this.picBau2)).EndInit();

            this.picDica2 = new System.Windows.Forms.PictureBox
            {
                Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right,
                BackColor = System.Drawing.Color.Transparent,
                Cursor = System.Windows.Forms.Cursors.Hand,
                Image = Properties.Resources.dica.ToBitmap(),
                Location = new System.Drawing.Point(695, 404),
                Size = new System.Drawing.Size(27, 26),
                SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage,
                TabStop = false
            };
            ((System.ComponentModel.ISupportInitialize)(this.picDica2)).BeginInit();
            this.tip.SetToolTip(this.picDica2, "Dicas");
            this.picDica2.Click += (s, e) =>
            {
                pSobre.Select();
                byte i = 0; System.Windows.Forms.DialogResult dialog;
                do dialog = System.Windows.Forms.MessageBox.Show(Classes.CartasDescInfo.Dica[i] + '.' + System.Environment.NewLine + "Deseja ir para a próxima dica?", string.Format("Dica {0} de {1}", (i + 1).ToString().Length == 1 ? "0" + (++i) : (++i).ToString(), Classes.CartasDescInfo.Dica.Length), System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Information);
                while (dialog == System.Windows.Forms.DialogResult.Yes && i < Classes.CartasDescInfo.Dica.Length - 1);
                if (dialog == System.Windows.Forms.DialogResult.Yes) System.Windows.Forms.MessageBox.Show(Classes.CartasDescInfo.Dica[Classes.CartasDescInfo.Dica.Length - 1] + '.', "Dica " + Classes.CartasDescInfo.Dica.Length + " de " + Classes.CartasDescInfo.Dica.Length, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            };
            ((System.ComponentModel.ISupportInitialize)(this.picDica2)).EndInit();

            this.picYouTube2 = new System.Windows.Forms.PictureBox
            {
                Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right,
                BackColor = System.Drawing.Color.Transparent,
                Cursor = System.Windows.Forms.Cursors.Hand,
                Image = Properties.Resources.youtube,
                Location = new System.Drawing.Point(728, 404),
                Size = new System.Drawing.Size(27, 26),
                SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage,
                TabStop = false
            };
            ((System.ComponentModel.ISupportInitialize)(this.picYouTube2)).BeginInit();
            this.tip.SetToolTip(this.picYouTube2, "YouTube");
            this.picYouTube2.Click += (s, e) => System.Diagnostics.Process.Start("https://www.youtube.com/c/lucasnaja");
            ((System.ComponentModel.ISupportInitialize)(this.picYouTube2)).EndInit();

            this.pSobre.SuspendLayout();
            this.pSobre.Controls.Add(this.lbl12);
            this.pSobre.Controls.Add(this.lblBuild2);
            this.pSobre.Controls.Add(picAtalho);
            this.pSobre.Controls.Add(this.picGitHub);
            this.pSobre.Controls.Add(this.picBau2);
            this.pSobre.Controls.Add(this.picDica2);
            this.pSobre.Controls.Add(this.picYouTube2);
            this.pSobre.Controls.Add(this.lbl11);
            this.pSobre.Controls.Add(this.lbl10);
            this.pSobre.Controls.Add(this.lbl9);
            this.pSobre.Controls.Add(this.lbl8);
            this.pSobre.Controls.Add(this.lbl7);
            this.pSobre.Controls.Add(this.lbl6);
            this.pSobre.Controls.Add(this.lbl5);
            this.pSobre.Controls.Add(this.picCanal);
            this.pSobre.Controls.Add(this.lbl3);
            this.pSobre.Controls.Add(this.lbl2);
            this.pSobre.Click += (s, e) => pSobre.Select();
            this.pSobre.ResumeLayout(false);
            this.pSobre.PerformLayout();
            #endregion

            #region Configurações
            this.pConfig = new System.Windows.Forms.Panel
            {
                Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right,
                BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle,
                ForeColor = System.Drawing.Color.White,
                Location = new System.Drawing.Point(167, 29),
                Size = new System.Drawing.Size(766, 441),
            };

            this.CMSConfig = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.CMSConfig.SuspendLayout();
            this.CMSConfig.Size = new System.Drawing.Size(214, 70);
            this.CMSConfig.Items.Add("Resetar ao Progresso salvo");
            this.CMSConfig.Items[0].Image = Properties.Resources.atualizar.ToBitmap();
            this.CMSConfig.Items[0].Click += (s, e) => ResetarSalvo();
            this.CMSConfig.Items.Add("Configurações padrão");
            this.CMSConfig.Items[1].Image = Properties.Resources.atualizar.ToBitmap();
            this.CMSConfig.Items[1].Click += (s, e) => ResetarPadrao();
            this.CMSConfig.Items.Add("-");
            this.CMSConfig.Items.Add("Salvar");
            this.CMSConfig.Items[3].Image = Properties.Resources.salvar.ToBitmap();
            this.CMSConfig.Items[3].Click += (s, e) => CSalvar();
            this.CMSConfig.ResumeLayout(false);

            this.TSIConfig = new System.Windows.Forms.ToolStripMenuItem
            {
                Size = new System.Drawing.Size(50, 20),
                Text = "Menu"
            };
            this.TSIConfig.DropDownItems.Add("Resetar ao Progresso salvo");
            this.TSIConfig.DropDownItems[0].Image = Properties.Resources.atualizar.ToBitmap();
            this.TSIConfig.DropDownItems[0].Click += (s, e) => { pConfig.Select(); ResetarSalvo(); };
            this.TSIConfig.DropDownItems.Add("Configurações padrão");
            this.TSIConfig.DropDownItems[1].Image = Properties.Resources.atualizar.ToBitmap();
            this.TSIConfig.DropDownItems[1].Click += (s, e) => { pConfig.Select(); ResetarPadrao(); };
            this.TSIConfig.DropDownItems.Add("-");
            this.TSIConfig.DropDownItems.Add("Salvar");
            this.TSIConfig.DropDownItems[3].Image = Properties.Resources.salvar.ToBitmap();
            this.TSIConfig.DropDownItems[3].Click += (s, e) => { pConfig.Select(); CSalvar(); };

            this.menuStripConfig = new System.Windows.Forms.MenuStrip
            {
                ForeColor = System.Drawing.Color.Black,
                Dock = System.Windows.Forms.DockStyle.None,
                Location = new System.Drawing.Point(0, 0),
                Size = new System.Drawing.Size(58, 24),
                Text = "Menu"
            };
            this.menuStripConfig.SuspendLayout();
            this.menuStripConfig.Items.Add(TSIConfig);
            this.menuStripConfig.ResumeLayout(false);
            this.menuStripConfig.PerformLayout();

            this.ckNome = new System.Windows.Forms.CheckBox
            {
                Anchor = System.Windows.Forms.AnchorStyles.Top,
                AutoSize = true,
                BackColor = System.Drawing.Color.Transparent,
                Cursor = System.Windows.Forms.Cursors.Hand,
                Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0),
                ForeColor = System.Drawing.Color.White,
                Location = new System.Drawing.Point(221, 7),
                Size = new System.Drawing.Size(365, 24),
                Text = "Nome das Cartas no Tip (Passar o mouse)",
                UseVisualStyleBackColor = false,
                TabIndex = 0
            };
            this.ckNome.CheckedChanged += (s, e) =>
            {
                if (ckNome.Checked)
                {
                    for (byte i = 0; i < Cartas.Length; i++)
                    {
                        string txt = _Deck.CartasInformacao[_Deck.deckAtual[i]].Split('\n')[0];
                        tip.SetToolTip(Cartas[i], txt == "Nenhuma Carta selecionada" ? "Carta Inexistente" : txt);
                    }

                    for (byte i = 0; i < picCarta.Length; i++)
                        tip.SetToolTip(picCarta[i], _Deck.CartasInformacao[i + 1].Split('\n')[0]);

                    for (byte i = 0; i < (picImagem == null ? 0 : (picImagem.Length > 50 ? 50 : picImagem.Length)); i++)
                        for (byte j = 0; j < (picImagem[i] == null ? 0 : picImagem[i].Length); j++)
                            for (byte k = 1; k < _Deck.CartasInformacao.Length; k++)
                                if (decksSalvos != null && decksSalvos[i].Split(';')[j] == _Deck.CodigoCartas[k].ToString())
                                { tip.SetToolTip(picImagem[i][j], _Deck.CartasInformacao[k].Split('\n')[0]); break; }

                    for (byte i = 0; i < (picImagemMDecks == null ? 0 : (picImagemMDecks.Length > 11 ? 11 : picImagemMDecks.Length)); i++)
                        for (byte j = 0; j < (picImagemMDecks[i] == null ? 0 : picImagemMDecks[i].Length); j++)
                            for (byte k = 1; k < _Deck.CartasInformacao.Length; k++)
                                if (melhoresDecks != null && melhoresDecks[i].Split('|')[0].Split(';')[j] == _Deck.CodigoCartas[k].ToString())
                                { tip.SetToolTip(picImagemMDecks[i][j], _Deck.CartasInformacao[k].Split('\n')[0]); break; }

                    for (byte i = 0; i < (picImagemBalanceamento == null ? 0 : picImagemBalanceamento.Length); i++)
                        for (byte j = 0; j < _Deck.CodigoCartas.Length; j++)
                            if (cartasBalanceadas[i].Split('|')[0] == _Deck.CodigoCartas[j].ToString())
                                tip.SetToolTip(picImagemBalanceamento[i], _Deck.CartasInformacao[j].Split('\n')[0]);
                    tip.SetToolTip(picProx, "Em breve");
                }
                else
                {
                    for (byte i = 0; i < Cartas.Length; i++)
                        tip.SetToolTip(Cartas[i], null);

                    for (byte i = 0; i < picCarta.Length; i++)
                        tip.SetToolTip(picCarta[i], null);

                    for (byte i = 0; i < (picImagem == null ? 0 : picImagem.Length > 50 ? 50 : picImagem.Length); i++)
                        for (byte j = 0; j < (picImagem[i] == null ? 0 : picImagem[i].Length); j++)
                            tip.SetToolTip(picImagem[i][j], null);

                    for (byte i = 0; i < (picImagemMDecks == null ? 0 : picImagemMDecks.Length > 11 ? 11 : picImagemMDecks.Length); i++)
                        for (byte j = 0; j < (picImagemMDecks[i] == null ? 0 : picImagemMDecks[i].Length); j++)
                            tip.SetToolTip(picImagemMDecks[i][j], null);

                    for (byte i = 0; i < (picImagemBalanceamento == null ? 0 : picImagemBalanceamento.Length); i++)
                        tip.SetToolTip(picImagemBalanceamento[i], null);
                    tip.SetToolTip(picProx, null);
                }
            };

            this.ckEfeitoMouse = new System.Windows.Forms.CheckBox
            {
                Anchor = System.Windows.Forms.AnchorStyles.Top,
                AutoSize = true,
                BackColor = System.Drawing.Color.Transparent,
                Cursor = System.Windows.Forms.Cursors.Hand,
                Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0),
                ForeColor = System.Drawing.Color.White,
                Location = new System.Drawing.Point(221, 32),
                Size = new System.Drawing.Size(295, 24),
                Text = "Efeito de Crescimento das Cartas",
                UseVisualStyleBackColor = false,
                TabIndex = 1
            };
            this.ckEfeitoMouse.CheckedChanged += (s, e) =>
            {
                if (ckEfeitoMouse.Checked)
                {
                    nUpTCarta.Enabled = true;
                    lblTamanho.Enabled = true;
                    nUpTCarta.Minimum = 1;
                    if (Properties.Settings.Default.efeitoValor == 0) nUpTCarta.Value = 2;
                    else nUpTCarta.Value = Properties.Settings.Default.efeitoValor;
                }
                else
                {
                    nUpTCarta.Enabled = false;
                    lblTamanho.Enabled = false;
                    nUpTCarta.Minimum = 0;
                    nUpTCarta.Value = 0;
                }
            };

            this.ckEfeitoClick = new System.Windows.Forms.CheckBox()
            {
                Anchor = System.Windows.Forms.AnchorStyles.Top,
                AutoSize = true,
                BackColor = System.Drawing.Color.Transparent,
                Cursor = System.Windows.Forms.Cursors.Hand,
                Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0),
                ForeColor = System.Drawing.Color.White,
                Location = new System.Drawing.Point(221, 57),
                Size = new System.Drawing.Size(295, 24),
                Text = "Efeito de Clique das Cartas",
                UseVisualStyleBackColor = false,
                TabIndex = 1
            };
            if (Properties.Settings.Default.cliqueCartas) ckEfeitoClick.Checked = true;

            this.nUpTCarta = new System.Windows.Forms.NumericUpDown
            {
                Anchor = System.Windows.Forms.AnchorStyles.Top,
                Cursor = System.Windows.Forms.Cursors.Hand,
                Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0),
                ForeColor = System.Drawing.Color.White,
                Location = new System.Drawing.Point(221, 86),
                Maximum = 2M,
                Minimum = 1M,
                Size = new System.Drawing.Size(33, 21),
                TextAlign = System.Windows.Forms.HorizontalAlignment.Center,
                Value = 2M,
                TabIndex = 2
            };
            ((System.ComponentModel.ISupportInitialize)(this.nUpTCarta)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUpTCarta)).EndInit();

            this.lblTamanho = new System.Windows.Forms.Label
            {
                Anchor = System.Windows.Forms.AnchorStyles.Top,
                AutoSize = true,
                BackColor = System.Drawing.Color.Transparent,
                Cursor = System.Windows.Forms.Cursors.Hand,
                Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0),
                ForeColor = System.Drawing.Color.White,
                Location = new System.Drawing.Point(258, 86),
                Size = new System.Drawing.Size(293, 20),
                Text = "Tamanho do crescimento de Cartas"
            };
            this.lblTamanho.MouseDown += (s, e) =>
            {
                if (nUpTCarta.Value == 1) nUpTCarta.Value = 2;
                else nUpTCarta.Value = 1;
            };

            if (Properties.Settings.Default.efeitoCartas)
            {
                ckEfeitoMouse.Checked = true;
                nUpTCarta.Value = Properties.Settings.Default.efeitoValor;
            }
            else
            {
                nUpTCarta.Enabled = false;
                nUpTCarta.Minimum = 0;
                nUpTCarta.Value = 0;
            }

            this.ckDesfoque = new System.Windows.Forms.CheckBox
            {
                Anchor = System.Windows.Forms.AnchorStyles.Top,
                AutoSize = true,
                BackColor = System.Drawing.Color.Transparent,
                Cursor = System.Windows.Forms.Cursors.Hand,
                Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0),
                ForeColor = System.Drawing.Color.White,
                Location = new System.Drawing.Point(221, 114),
                Size = new System.Drawing.Size(354, 24),
                Text = "Transparência ao desfoque de Programa",
                UseVisualStyleBackColor = false,
                TabIndex = 3
            };
            if (Properties.Settings.Default.desfoque) ckDesfoque.Checked = true;

            this.ckVoltarDeck = new System.Windows.Forms.CheckBox
            {
                Anchor = System.Windows.Forms.AnchorStyles.Top,
                AutoSize = true,
                BackColor = System.Drawing.Color.Transparent,
                Cursor = System.Windows.Forms.Cursors.Hand,
                Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0),
                ForeColor = System.Drawing.Color.White,
                Location = new System.Drawing.Point(221, 140),
                Size = new System.Drawing.Size(267, 24),
                Text = "Botão Voltar ao Deck anterior",
                UseVisualStyleBackColor = false,
                TabIndex = 4
            };
            this.ckVoltarDeck.CheckedChanged += (s, e) =>
            {
                if (ckVoltarDeck.Checked) btnVoltarDeck.Visible = true;
                else btnVoltarDeck.Visible = false;
            };
            if (!Properties.Settings.Default.voltarDeck) btnVoltarDeck.Visible = false;
            else ckVoltarDeck.Checked = true;

            this.rbMin = new System.Windows.Forms.RadioButton
            {
                AutoSize = true,
                BackColor = System.Drawing.Color.Transparent,
                Cursor = System.Windows.Forms.Cursors.Hand,
                Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0),
                ForeColor = System.Drawing.Color.White,
                Location = new System.Drawing.Point(12, 18),
                Size = new System.Drawing.Size(81, 22),
                Text = "Mínimo",
                UseVisualStyleBackColor = false,
                TabIndex = 6
            };
            this.rbMin.CheckedChanged += (s, e) => CalcularMedia();

            this.rbMax = new System.Windows.Forms.RadioButton
            {
                AutoSize = true,
                BackColor = System.Drawing.Color.Transparent,
                Cursor = System.Windows.Forms.Cursors.Hand,
                Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0),
                ForeColor = System.Drawing.Color.White,
                Location = new System.Drawing.Point(177, 18),
                Size = new System.Drawing.Size(85, 22),
                Text = "Máximo",
                UseVisualStyleBackColor = false,
                TabIndex = 8
            };
            this.rbMax.CheckedChanged += (s, e) => CalcularMedia();

            this.rbMed = new System.Windows.Forms.RadioButton
            {
                AutoSize = true,
                BackColor = System.Drawing.Color.Transparent,
                Checked = true,
                Cursor = System.Windows.Forms.Cursors.Hand,
                Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0),
                ForeColor = System.Drawing.Color.White,
                Location = new System.Drawing.Point(99, 18),
                Size = new System.Drawing.Size(72, 22),
                TabStop = true,
                Text = "Médio",
                UseVisualStyleBackColor = false,
                TabIndex = 7
            };
            this.rbMed.CheckedChanged += (s, e) => CalcularMedia();

            this.gbCusto = new System.Windows.Forms.GroupBox
            {
                Anchor = System.Windows.Forms.AnchorStyles.Top,
                BackColor = System.Drawing.Color.Transparent,
                Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0),
                ForeColor = System.Drawing.Color.White,
                Location = new System.Drawing.Point(218, 170),
                Size = new System.Drawing.Size(330, 52),
                TabStop = false,
                Text = "Elixir Médio total de Cartas ao mudar Arena/Raridade",
                TabIndex = 5
            };
            this.gbCusto.SuspendLayout();
            this.gbCusto.Controls.Add(this.rbMin);
            this.gbCusto.Controls.Add(this.rbMax);
            this.gbCusto.Controls.Add(this.rbMed);
            this.gbCusto.ResumeLayout(false);
            this.gbCusto.PerformLayout();

            this.rbClaro = new System.Windows.Forms.RadioButton
            {
                AutoSize = true,
                BackColor = System.Drawing.Color.Transparent,
                Cursor = System.Windows.Forms.Cursors.Hand,
                Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0),
                ForeColor = System.Drawing.Color.White,
                Location = new System.Drawing.Point(12, 19),
                Size = new System.Drawing.Size(67, 22),
                Text = "Claro",
                UseVisualStyleBackColor = false,
                TabIndex = 10
            };
            this.rbClaro.CheckedChanged += (s, e) => { if (rbClaro.Checked) CorClaro(); };

            this.rbPadrao = new System.Windows.Forms.RadioButton
            {
                AutoSize = true,
                BackColor = System.Drawing.Color.Transparent,
                Checked = true,
                Cursor = System.Windows.Forms.Cursors.Hand,
                Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0),
                ForeColor = System.Drawing.Color.White,
                Location = new System.Drawing.Point(85, 19),
                Size = new System.Drawing.Size(80, 22),
                TabStop = true,
                Text = "Padrão",
                UseVisualStyleBackColor = false,
                TabIndex = 11
            };
            this.rbPadrao.CheckedChanged += (s, e) => { if (rbPadrao.Checked) CorPadrao(); };

            this.rbEscuro = new System.Windows.Forms.RadioButton
            {
                AutoSize = true,
                BackColor = System.Drawing.Color.Transparent,
                Cursor = System.Windows.Forms.Cursors.Hand,
                Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0),
                ForeColor = System.Drawing.Color.White,
                Location = new System.Drawing.Point(171, 19),
                Size = new System.Drawing.Size(80, 22),
                Text = "Escuro",
                UseVisualStyleBackColor = false,
                TabIndex = 12
            };
            this.rbEscuro.CheckedChanged += (s, e) => { if (rbEscuro.Checked) CorEscuro(); };

            this.gbTema = new System.Windows.Forms.GroupBox
            {
                Anchor = System.Windows.Forms.AnchorStyles.Top,
                BackColor = System.Drawing.Color.Transparent,
                Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0),
                ForeColor = System.Drawing.Color.White,
                Location = new System.Drawing.Point(218, 228),
                Size = new System.Drawing.Size(330, 52),
                TabStop = false,
                Text = "Temas",
                TabIndex = 9
            };
            this.gbTema.SuspendLayout();
            this.gbTema.Controls.Add(this.rbPadrao);
            this.gbTema.Controls.Add(this.rbEscuro);
            this.gbTema.Controls.Add(this.rbClaro);
            this.gbTema.ResumeLayout(false);
            this.gbTema.PerformLayout();

            this.rbMinimalistico = new System.Windows.Forms.RadioButton
            {
                AutoSize = true,
                BackColor = System.Drawing.Color.Transparent,
                Cursor = System.Windows.Forms.Cursors.Hand,
                Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0),
                ForeColor = System.Drawing.Color.White,
                Location = new System.Drawing.Point(12, 19),
                Size = new System.Drawing.Size(121, 22),
                Text = "Minimalista",
                UseVisualStyleBackColor = false,
                TabIndex = 14
            };

            this.rbGrafico = new System.Windows.Forms.RadioButton
            {
                AutoSize = true,
                BackColor = System.Drawing.Color.Transparent,
                Cursor = System.Windows.Forms.Cursors.Hand,
                Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0),
                ForeColor = System.Drawing.Color.White,
                Location = new System.Drawing.Point(130, 19),
                Size = new System.Drawing.Size(82, 22),
                TabStop = true,
                Text = "Gráfico",
                UseVisualStyleBackColor = false,
                TabIndex = 15
            };

            if (Properties.Settings.Default.modo) rbGrafico.Checked = true;
            else rbMinimalistico.Checked = true;

            this.gbModo = new System.Windows.Forms.GroupBox
            {
                Anchor = System.Windows.Forms.AnchorStyles.Top,
                BackColor = System.Drawing.Color.Transparent,
                Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0),
                ForeColor = System.Drawing.Color.White,
                Location = new System.Drawing.Point(218, 286),
                Size = new System.Drawing.Size(330, 52),
                TabStop = false,
                Text = "Decks salvos - Modo",
                TabIndex = 13
            };
            this.gbModo.SuspendLayout();
            this.gbModo.Controls.Add(this.rbGrafico);
            this.gbModo.Controls.Add(this.rbMinimalistico);
            this.gbModo.ResumeLayout(false);
            this.gbModo.PerformLayout();

            this.btnCSalvar = new System.Windows.Forms.Button
            {
                Anchor = System.Windows.Forms.AnchorStyles.Top,
                Cursor = System.Windows.Forms.Cursors.Hand,
                Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0),
                ForeColor = System.Drawing.Color.White,
                Location = new System.Drawing.Point(218, 344),
                Size = new System.Drawing.Size(130, 40),
                Text = "&Salvar",
                UseVisualStyleBackColor = false,
                TabIndex = 16
            };
            this.tip.SetToolTip(this.btnCSalvar, "Salvar Configurações atuais");
            this.btnCSalvar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCSalvar.Click += (s, e) => { pConfig.Select(); CSalvar(); };

            this.pConfig.ContextMenuStrip = CMSConfig;
            this.pConfig.SuspendLayout();
            this.pConfig.Controls.Add(this.gbModo);
            this.pConfig.Controls.Add(this.gbTema);
            this.pConfig.Controls.Add(this.gbCusto);
            this.pConfig.Controls.Add(this.ckVoltarDeck);
            this.pConfig.Controls.Add(this.btnCSalvar);
            this.pConfig.Controls.Add(this.ckDesfoque);
            this.pConfig.Controls.Add(this.lblTamanho);
            this.pConfig.Controls.Add(this.nUpTCarta);
            this.pConfig.Controls.Add(this.ckNome);
            this.pConfig.Controls.Add(this.ckEfeitoClick);
            this.pConfig.Controls.Add(this.ckEfeitoMouse);
            this.pConfig.Controls.Add(this.menuStripConfig);
            this.pConfig.Click += (s, e) => pConfig.Select();
            this.pConfig.ResumeLayout(false);
            this.pConfig.PerformLayout();
            #endregion

            #region Seleção de Cartas
            this.pSelecaoDeCartas = new System.Windows.Forms.Panel
            {
                Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right,
                AutoScroll = true,
                BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle,
                Location = new System.Drawing.Point(167, 29),
                Size = new System.Drawing.Size(766, 441),
            };
            this.pSelecaoDeCartas.Click += (s, e) => pSelecaoDeCartas.Select();

            Classes.ArquivoRegras.Criar();
            System.Collections.ArrayList listaCartas = new System.Collections.ArrayList();
            localInicialGrpBox = new System.Drawing.Point[_Deck.CartasInformacao.Length - 1];
            grpBoxSCartas = new System.Windows.Forms.GroupBox[_Deck.CartasInformacao.Length - 1];
            picCarta = new System.Windows.Forms.PictureBox[_Deck.CartasInformacao.Length - 1];
            lblInfo = new System.Windows.Forms.Label[_Deck.CartasInformacao.Length - 1];
            btnInfo = new System.Windows.Forms.Button[_Deck.CartasInformacao.Length - 1];
            cbPermitir = new System.Windows.Forms.CheckBox[_Deck.CartasInformacao.Length - 1];

            txtPesquisa.AutoCompleteCustomSource = dados;
            int yy = -110;
            int xx = 109;

            for (byte i = 0; i < grpBoxSCartas.Length; i++)
            {
                byte valorI = i;
                #region Sets
                if (i % 2 == 0) { xx = 109; yy += 175; }
                else if (i % 2 == 1) { xx = 380; }
                // grpBox
                grpBoxSCartas[i] = new System.Windows.Forms.GroupBox
                {
                    Size = new System.Drawing.Size(265, 170),
                    BackColor = System.Drawing.Color.Transparent,
                    Font = new System.Drawing.Font(Font.FontFamily, 8.25f, System.Drawing.FontStyle.Bold),
                    Text = string.Format("{0} ({1} - {2})", _Deck.CartasInformacao[i + 1].Split('\n')[0], _Deck.CartasInformacao[i + 1].Split('\n')[1].Split()[1], _Deck.CartasInformacao[i + 1].Split('\n')[2].Split()[1])
                };

                localInicialGrpBox[i] = new System.Drawing.Point(xx, yy);
                // picCarta
                picCarta[i] = new System.Windows.Forms.PictureBox
                {
                    Size = new System.Drawing.Size(123, 144),
                    Location = new System.Drawing.Point(7, 17),
                    BackColor = System.Drawing.Color.Transparent,
                    SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage,
                    Image = _Deck.CartasImagem[i + 1],
                    Cursor = System.Windows.Forms.Cursors.Hand
                };
                // lblInfo
                byte arena = 0;
                for (byte j = System.Convert.ToByte(valores.Length - 1); j > 0; j--)
                    if (i + 1 < valores[j])
                        break;
                    else arena++;
                lblInfo[i] = new System.Windows.Forms.Label
                {
                    AutoSize = true,
                    Location = new System.Drawing.Point(132, 25),
                    Font = new System.Drawing.Font(Font.FontFamily, 8.25f, System.Drawing.FontStyle.Regular),
                    Text = _Deck.CartasInformacao[i + 1].Split('\n')[0] + System.Environment.NewLine +
                    _Deck.CartasInformacao[i + 1].Split('\n')[1] + System.Environment.NewLine +
                    _Deck.CartasInformacao[i + 1].Split('\n')[2] + System.Environment.NewLine +
                  string.Format("Custo de Elixir: {0}", _Deck.CustoElixir[i + 1]).Replace(',', '.') + System.Environment.NewLine +
                  string.Format("Código: {0}", _Deck.CodigoCartas[i + 1]) + System.Environment.NewLine +
                  string.Format("Arena: {0}", arena == 0 ? "Campo de Treino" : arena.ToString())
                };
                // btnInfo
                btnInfo[i] = new System.Windows.Forms.Button
                {
                    Text = "Descrição",
                    FlatStyle = System.Windows.Forms.FlatStyle.Flat,
                    Cursor = System.Windows.Forms.Cursors.Hand,
                    Size = new System.Drawing.Size(80, 25),
                    Font = new System.Drawing.Font(Font.FontFamily, 8.25f, System.Drawing.FontStyle.Bold),
                    Location = new System.Drawing.Point(177, 136),
                    ForeColor = System.Drawing.Color.White
                };
                // cbPermitir
                cbPermitir[i] = new System.Windows.Forms.CheckBox
                {
                    Text = "Permitir",
                    AutoSize = true,
                    Location = new System.Drawing.Point(192, 116),
                    Cursor = System.Windows.Forms.Cursors.Hand,
                    BackColor = System.Drawing.Color.Transparent
                };
                #endregion

                #region Métodos
                if (ckNome.Checked)
                    tip.SetToolTip(picCarta[i], _Deck.CartasInformacao[i + 1].Split('\n')[0]);
                tip.SetToolTip(btnInfo[i], "Descrição da Carta");
                #endregion

                int localInicialX = picCarta[i].Location.X, localInicialY = picCarta[i].Location.Y;
                int tamInicialHeigth = picCarta[i].Size.Height, tamInicialWidth = picCarta[i].Size.Width;

                #region Eventos
                picCarta[i].MouseEnter += (s, e) =>
                {
                    picCarta[valorI].Location = new System.Drawing.Point(localInicialX - System.Convert.ToByte(nUpTCarta.Value), localInicialY - System.Convert.ToByte(nUpTCarta.Value));
                    picCarta[valorI].Size = new System.Drawing.Size(tamInicialWidth + System.Convert.ToByte(nUpTCarta.Value) * 2, tamInicialHeigth + System.Convert.ToByte(nUpTCarta.Value) * 2);
                };
                picCarta[i].MouseLeave += (s, e) =>
                {
                    picCarta[valorI].Location = new System.Drawing.Point(localInicialX, localInicialY);
                    picCarta[valorI].Size = new System.Drawing.Size(tamInicialWidth, tamInicialHeigth);
                };
                picCarta[i].MouseDown += (s, e) =>
                {
                    if (ckEfeitoClick.Checked)
                    {
                        picCarta[valorI].Location = new System.Drawing.Point(picCarta[valorI].Location.X + 2 + System.Convert.ToByte(nUpTCarta.Value), picCarta[valorI].Location.Y + 2 + System.Convert.ToByte(nUpTCarta.Value));
                        picCarta[valorI].Size = new System.Drawing.Size(picCarta[valorI].Size.Width - 4 - System.Convert.ToByte(nUpTCarta.Value) * 2, picCarta[valorI].Size.Height - 4 - System.Convert.ToByte(nUpTCarta.Value) * 2);
                    }
                };
                picCarta[i].MouseUp += (s, e) =>
                {
                    if (ckEfeitoClick.Checked)
                    {
                        picCarta[valorI].Location = new System.Drawing.Point(picCarta[valorI].Location.X - 2 - System.Convert.ToByte(nUpTCarta.Value), picCarta[valorI].Location.Y - 2 - System.Convert.ToByte(nUpTCarta.Value));
                        picCarta[valorI].Size = new System.Drawing.Size(picCarta[valorI].Size.Width + 4 + System.Convert.ToByte(nUpTCarta.Value) * 2, picCarta[valorI].Size.Height + 4 + System.Convert.ToByte(nUpTCarta.Value) * 2);
                    }
                };

                picCarta[i].Click += (s, e) =>
                {
                    grpBoxSCartas[valorI].Select();
                    string infoAdicional = string.Empty;
                    byte v = 0;
                    for (byte j = System.Convert.ToByte(valores.Length - 1); j > 0; j--)
                        if (valorI + 1 < valores[j])
                        {
                            infoAdicional = string.Format("Arena: {0} {1}", arenas[v], v != 0 ? "(" + v + ")" : string.Empty); break;
                        }
                        else { v++; }

                    lblInformacoes.Text = _Deck.CartasInformacao[valorI + 1] + System.Environment.NewLine +
                    string.Format("Custo de Elixir: {0}", _Deck.CustoElixir[valorI + 1]).Replace(',', '.') + System.Environment.NewLine +
                    infoAdicional;
                };

                btnInfo[i].Click += (s, e) =>
                {
                    grpBoxSCartas[valorI].Select();
                    string descricao = Classes.CartasDescInfo.Descricao[valorI + 1];
                    System.Windows.Forms.MessageBox.Show(descricao, "Descrição - " + _Deck.CartasInformacao[valorI + 1].Split('\n')[0], System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                };

                cbPermitir[i].CheckedChanged += (s, e) => { ChecarCB(); };

                lblInfo[i].Click += (s, e) => { grpBoxSCartas[valorI].Select(); };

                grpBoxSCartas[i].Click += (s, e) => { grpBoxSCartas[valorI].Select(); };
                #endregion

                #region Controls
                pSelecaoDeCartas.Controls.Add(grpBoxSCartas[i]);
                grpBoxSCartas[i].Controls.Add(picCarta[i]);
                grpBoxSCartas[i].Controls.Add(lblInfo[i]);
                grpBoxSCartas[i].Controls.Add(btnInfo[i]);
                grpBoxSCartas[i].Controls.Add(cbPermitir[i]);
                #endregion

                if (i == grpBoxSCartas.Length - 1 && (i + 1) % 2 == 0) { xx = 109; yy += 175; }
                else if (i == grpBoxSCartas.Length - 1 && (i + 1) % 2 == 1) { xx = 380; }
            }

            lblProx = new System.Windows.Forms.Label()
            {
                Text = "Em breve",
                AutoSize = true,
                Font = new System.Drawing.Font(Font.FontFamily, 8.25f, System.Drawing.FontStyle.Regular),
                Location = new System.Drawing.Point(132, 25)
            };
            lblProx.Click += (s, e) => gbProx.Select();

            picProx = new System.Windows.Forms.PictureBox()
            {
                Size = new System.Drawing.Size(123, 144),
                Location = new System.Drawing.Point(7, 17),
                BackColor = System.Drawing.Color.Transparent,
                SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage,
                Image = _Deck.CartasImagem[0],
                Cursor = System.Windows.Forms.Cursors.Hand
            };
            picProx.Click += (s, e) => gbProx.Select();

            byte posInicialX = System.Convert.ToByte(picProx.Location.X), posInicialY = System.Convert.ToByte(picProx.Location.Y);
            byte tamInicialW = System.Convert.ToByte(picProx.Size.Width), tamInicialH = System.Convert.ToByte(picProx.Size.Height);

            picProx.MouseEnter += (s, e) =>
            {
                picProx.Location = new System.Drawing.Point(posInicialX - System.Convert.ToByte(nUpTCarta.Value), posInicialY - System.Convert.ToByte(nUpTCarta.Value));
                picProx.Size = new System.Drawing.Size(tamInicialW + System.Convert.ToByte(nUpTCarta.Value * 2), tamInicialH + System.Convert.ToByte(nUpTCarta.Value * 2));
            };
            picProx.MouseLeave += (s, e) =>
            {
                picProx.Location = new System.Drawing.Point(posInicialX, posInicialY);
                picProx.Size = new System.Drawing.Size(tamInicialW, tamInicialH);
            };
            picProx.MouseDown += (s, e) =>
            {
                if (ckEfeitoClick.Checked)
                {
                    picProx.Location = new System.Drawing.Point(picProx.Location.X + 2 + System.Convert.ToByte(nUpTCarta.Value), picProx.Location.Y + 2 + System.Convert.ToByte(nUpTCarta.Value));
                    picProx.Size = new System.Drawing.Size(picProx.Size.Width - 4 - System.Convert.ToByte(nUpTCarta.Value) * 2, picProx.Size.Height - 4 - System.Convert.ToByte(nUpTCarta.Value) * 2);
                }
            };
            picProx.MouseUp += (s, e) =>
            {
                if (ckEfeitoClick.Checked)
                {
                    picProx.Location = new System.Drawing.Point(picProx.Location.X - 2 - System.Convert.ToByte(nUpTCarta.Value), picProx.Location.Y - 2 - System.Convert.ToByte(nUpTCarta.Value));
                    picProx.Size = new System.Drawing.Size(picProx.Size.Width + 4 + System.Convert.ToByte(nUpTCarta.Value) * 2, picProx.Size.Height + 4 + System.Convert.ToByte(nUpTCarta.Value) * 2);
                }
            };

            picProx.Click += (s, e) => lblInformacoes.Text = _Deck.CartasInformacao[0];

            cbProx = new System.Windows.Forms.CheckBox()
            {
                Enabled = false,
                Text = "Permitir",
                AutoSize = true,
                Location = new System.Drawing.Point(192, 116),
                Cursor = System.Windows.Forms.Cursors.Hand,
                BackColor = System.Drawing.Color.Transparent
            };

            btnProx = new System.Windows.Forms.Button()
            {
                Enabled = false,
                Text = "Descrição",
                FlatStyle = System.Windows.Forms.FlatStyle.Flat,
                Cursor = System.Windows.Forms.Cursors.Hand,
                Size = new System.Drawing.Size(80, 25),
                Font = new System.Drawing.Font(Font.FontFamily, 8.25f, System.Drawing.FontStyle.Bold),
                Location = new System.Drawing.Point(177, 136),
                ForeColor = System.Drawing.Color.White
            };

            gbProx = new System.Windows.Forms.GroupBox()
            {
                Text = "Em breve",
                Size = new System.Drawing.Size(265, 170),
                Location = new System.Drawing.Point(xx, yy),
                BackColor = System.Drawing.Color.Transparent,
                Font = new System.Drawing.Font(Font.FontFamily, 8.25f, System.Drawing.FontStyle.Bold)
            };
            gbProx.Click += (s, e) => gbProx.Select();
            gbProx.Controls.Add(lblProx);
            gbProx.Controls.Add(picProx);
            gbProx.Controls.Add(cbProx);
            gbProx.Controls.Add(btnProx);

            for (byte i = 0; i < tsItems.Length; i++)
            {
                tsItems[i] = new System.Windows.Forms.ToolStripMenuItem();
                if (i == 2) cMenu.Items.Add("-");
                cMenu.Items.Add(tsItems[i]);
            }

            #region Sets
            // cMenu
            tsItems[0].Image = Properties.Resources.atualizar.ToBitmap();
            tsItems[0].Text = "Atualizar";
            tsItems[1].Image = Properties.Resources.salvar.ToBitmap();
            tsItems[1].Text = "Salvar";
            tsItems[2].Text = "Selecionar Todos";
            tsItems[3].Text = "Comuns";
            tsItems[4].Text = "Raras";
            tsItems[5].Text = "Épicas";
            tsItems[6].Text = "Lendárias";
            tsItems[7].Text = "Tropas";
            tsItems[8].Text = "Construções";
            tsItems[9].Text = "Feitiços";
            // menuStripSC / TSMI
            TSMI.DropDownItems.Add("&Atualizar");
            TSMI.DropDownItems[0].Image = Properties.Resources.atualizar.ToBitmap();
            TSMI.DropDownItems.Add("&Salvar");
            TSMI.DropDownItems[1].Image = Properties.Resources.salvar.ToBitmap();
            menuStripSC.Items.Add(TSMI);
            menuStripSC.Dock = System.Windows.Forms.DockStyle.None;
            menuStripSC.Location = new System.Drawing.Point(0, 0);
            // btnSalvar
            btnSalvar.Location = new System.Drawing.Point(565, yy + 175);
            // cbSort
            cbSort.Items.Add("Por Arena");
            cbSort.Items.Add("Por Arena (Decrescente)");
            cbSort.Items.Add("Por Elixir");
            cbSort.Items.Add("Por Elixir (Decrescente)");
            cbSort.Items.Add("Por Raridade");
            cbSort.Items.Add("Por Raridade (Decrescente)");
            cbSort.Items.Add("Por Tipo");
            cbSort.Items.Add("Por Tipo (Decrescente)");
            // pSelecao
            pSelecaoDeCartas.ContextMenuStrip = cMenu;
            #endregion

            // Métodos
            tip.SetToolTip(btnSalvar, "Salvar Seleção atual");
            tip.SetToolTip(btnPesquisar, "Pesquisar Carta");
            Atualizar();
            ChecarCB();

            #region Eventos
            // Eventos
            lblAjuda.Click += (s, e) => pSelecaoDeCartas.Select();
            TSMI.DropDownItems[0].Click += (s, e) => { pSelecaoDeCartas.Select(); Atualizar(); };
            TSMI.DropDownItems[1].Click += (s, e) => { pSelecaoDeCartas.Select(); Salvar(); };
            btnSalvar.Click += (s, e) => { pSelecaoDeCartas.Select(); Salvar(); };
            cbSort.SelectedIndexChanged += (s, e) =>
            {
                pSelecaoDeCartas.VerticalScroll.Value = pSelecaoDeCartas.VerticalScroll.Minimum;
                Properties.Settings.Default.index = System.Convert.ToByte(cbSort.SelectedIndex);
                Properties.Settings.Default.Save();
                if (cbSort.SelectedIndex == 0)
                    for (byte i = 0; i < grpBoxSCartas.Length; i++)
                    {
                        grpBoxSCartas[i].Location = localInicialGrpBox[i];
                        grpBoxSCartas[i].TabIndex = i + 3;
                    }
                else if (cbSort.SelectedIndex == 1)
                    for (byte i = System.Convert.ToByte(grpBoxSCartas.Length); i > 0; i--)
                    {
                        grpBoxSCartas[i - 1].Location = localInicialGrpBox[-(i - grpBoxSCartas.Length)];
                        grpBoxSCartas[i - 1].TabIndex = -(i - grpBoxSCartas.Length) + 3;
                    }
                else if (cbSort.SelectedIndex == 2)
                {
                    System.Collections.ArrayList listaDeCartas = new System.Collections.ArrayList(_Deck.CustoElixir);
                    listaDeCartas.Sort();
                    System.Collections.ArrayList listaDeCartas2 = new System.Collections.ArrayList(_Deck.CustoElixir);
                    for (byte i = 1; i <= localInicialGrpBox.Length; i++)
                    {
                        grpBoxSCartas[listaDeCartas2.IndexOf(listaDeCartas[i]) - 1].Location = localInicialGrpBox[i - 1];
                        grpBoxSCartas[listaDeCartas2.IndexOf(listaDeCartas[i]) - 1].TabIndex = i + 2;
                        listaDeCartas2[listaDeCartas2.IndexOf(listaDeCartas[i])] = 0;
                    }
                }
                else if (cbSort.SelectedIndex == 3)
                {
                    System.Collections.ArrayList listaDeCartas = new System.Collections.ArrayList(_Deck.CustoElixir);
                    listaDeCartas.Sort();
                    System.Collections.ArrayList listaDeCartas2 = new System.Collections.ArrayList(_Deck.CustoElixir);
                    for (byte i = 1; i <= grpBoxSCartas.Length; i++)
                    {
                        grpBoxSCartas[listaDeCartas2.IndexOf(listaDeCartas[i]) - 1].Location = localInicialGrpBox[-(i - grpBoxSCartas.Length)];
                        grpBoxSCartas[listaDeCartas2.IndexOf(listaDeCartas[i]) - 1].TabIndex = -(i - grpBoxSCartas.Length) + 2;
                        listaDeCartas2[listaDeCartas2.IndexOf(listaDeCartas[i])] = 0;
                    }
                }
                else if (cbSort.SelectedIndex == 4)
                {
                    System.Collections.ArrayList listaDeCartas = new System.Collections.ArrayList();
                    for (byte i = 1; i < _Deck.CartasInformacao.Length; i++)
                        if (_Deck.CartasInformacao[i].Split('\n')[1] == "Raridade: Comum") listaDeCartas.Add(i);
                    for (byte i = 1; i < _Deck.CartasInformacao.Length; i++)
                        if (_Deck.CartasInformacao[i].Split('\n')[1] == "Raridade: Rara") listaDeCartas.Add(i);
                    for (byte i = 1; i < _Deck.CartasInformacao.Length; i++)
                        if (_Deck.CartasInformacao[i].Split('\n')[1] == "Raridade: Épica") listaDeCartas.Add(i);
                    for (byte i = 1; i < _Deck.CartasInformacao.Length; i++)
                        if (_Deck.CartasInformacao[i].Split('\n')[1] == "Raridade: Lendária") listaDeCartas.Add(i);
                    for (byte i = 0; i < grpBoxSCartas.Length; i++)
                    {
                        grpBoxSCartas[System.Convert.ToByte(listaDeCartas[i]) - 1].Location = localInicialGrpBox[i];
                        grpBoxSCartas[System.Convert.ToByte(listaDeCartas[i]) - 1].TabIndex = i + 3;
                    }
                }
                else if (cbSort.SelectedIndex == 5)
                {
                    System.Collections.ArrayList listaDeCartas = new System.Collections.ArrayList();
                    for (byte i = System.Convert.ToByte(_Deck.CartasInformacao.Length - 1); i > 0; i--)
                        if (_Deck.CartasInformacao[i].Split('\n')[1] == "Raridade: Lendária") listaDeCartas.Add(i);
                    for (byte i = System.Convert.ToByte(_Deck.CartasInformacao.Length - 1); i > 0; i--)
                        if (_Deck.CartasInformacao[i].Split('\n')[1] == "Raridade: Épica") listaDeCartas.Add(i);
                    for (byte i = System.Convert.ToByte(_Deck.CartasInformacao.Length - 1); i > 0; i--)
                        if (_Deck.CartasInformacao[i].Split('\n')[1] == "Raridade: Rara") listaDeCartas.Add(i);
                    for (byte i = System.Convert.ToByte(_Deck.CartasInformacao.Length - 1); i > 0; i--)
                        if (_Deck.CartasInformacao[i].Split('\n')[1] == "Raridade: Comum") listaDeCartas.Add(i);
                    for (byte i = 0; i < grpBoxSCartas.Length; i++)
                    {
                        grpBoxSCartas[System.Convert.ToByte(listaDeCartas[i]) - 1].Location = localInicialGrpBox[i];
                        grpBoxSCartas[System.Convert.ToByte(listaDeCartas[i]) - 1].TabIndex = i + 3;
                    }
                }
                else if (cbSort.SelectedIndex == 6)
                {
                    byte local = 0;
                    for (byte i = 0; i < grpBoxSCartas.Length; i++)
                        if (_Deck.CartasInformacao[i + 1].Split('\n')[2] == "Tipo: Tropa")
                        { grpBoxSCartas[i].Location = localInicialGrpBox[local]; grpBoxSCartas[i].TabIndex = local + 3; local++; }
                    for (byte i = 0; i < grpBoxSCartas.Length; i++)
                        if (_Deck.CartasInformacao[i + 1].Split('\n')[2] == "Tipo: Construção")
                        { grpBoxSCartas[i].Location = localInicialGrpBox[local]; grpBoxSCartas[i].TabIndex = local + 3; local++; }
                    for (byte i = 0; i < grpBoxSCartas.Length; i++)
                        if (_Deck.CartasInformacao[i + 1].Split('\n')[2] == "Tipo: Feitiço")
                        { grpBoxSCartas[i].Location = localInicialGrpBox[local]; grpBoxSCartas[i].TabIndex = local + 3; local++; }
                }
                else if (cbSort.SelectedIndex == 7)
                {
                    byte local = 0;
                    for (byte i = 0; i < grpBoxSCartas.Length; i++)
                        if (_Deck.CartasInformacao[i + 1].Split('\n')[2] == "Tipo: Feitiço")
                        { grpBoxSCartas[i].Location = localInicialGrpBox[local]; grpBoxSCartas[i].TabIndex = local + 3; local++; }
                    for (byte i = 0; i < grpBoxSCartas.Length; i++)
                        if (_Deck.CartasInformacao[i + 1].Split('\n')[2] == "Tipo: Construção")
                        { grpBoxSCartas[i].Location = localInicialGrpBox[local]; grpBoxSCartas[i].TabIndex = local + 3; local++; }
                    for (byte i = 0; i < grpBoxSCartas.Length; i++)
                        if (_Deck.CartasInformacao[i + 1].Split('\n')[2] == "Tipo: Tropa")
                        { grpBoxSCartas[i].Location = localInicialGrpBox[local]; grpBoxSCartas[i].TabIndex = local + 3; local++; }
                }
                pSelecaoDeCartas.Select();
            };
            tsItems[0].Click += (s, e) => Atualizar();
            tsItems[1].Click += (s, e) => Salvar();
            tsItems[2].Click += (s, e) => Todos();
            tsItems[3].Click += (s, e) => RarTipo("Comum", 3, 1, "Raridade");
            tsItems[4].Click += (s, e) => RarTipo("Rara", 4, 1, "Raridade");
            tsItems[5].Click += (s, e) => RarTipo("Épica", 5, 1, "Raridade");
            tsItems[6].Click += (s, e) => RarTipo("Lendária", 6, 1, "Raridade");
            tsItems[7].Click += (s, e) => RarTipo("Tropa", 7, 2, "Tipo");
            tsItems[8].Click += (s, e) => RarTipo("Construção", 8, 2, "Tipo");
            tsItems[9].Click += (s, e) => RarTipo("Feitiço", 9, 2, "Tipo");
            btnPesquisar.Click += (s, e) => Pesquisar();
            txtPesquisa.KeyUp += (s, e) => { if (e.KeyCode == System.Windows.Forms.Keys.Enter) Pesquisar(); };
            #endregion

            // Pós Evento
            cbSort.SelectedIndex = Properties.Settings.Default.index;

            // Controls
            pSelecaoDeCartas.Controls.Add(menuStripSC);
            pSelecaoDeCartas.Controls.Add(btnSalvar);
            pSelecaoDeCartas.Controls.Add(txtPesquisa);
            pSelecaoDeCartas.Controls.Add(btnPesquisar);
            pSelecaoDeCartas.Controls.Add(cbSort);
            pSelecaoDeCartas.Controls.Add(lblAjuda);
            pSelecaoDeCartas.Controls.Add(gbProx);

            void Salvar()
            {
                AtualizaListaCartas();
                try
                {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    for (byte i = 0; i < cbPermitir.Length; i++)
                    {
                        if (cbPermitir[i].Checked) sb.Append(string.Format("{0}|Permitido{1}", _Deck.CartasInformacao[i + 1].Split('\n')[0], i == cbPermitir.Length - 1 ? string.Empty : System.Environment.NewLine));
                        else sb.Append(string.Format("{0}|Não permitido{1}", _Deck.CartasInformacao[i + 1].Split('\n')[0], i == cbPermitir.Length - 1 ? string.Empty : System.Environment.NewLine));
                    }
                    if (System.IO.File.Exists(Classes.ArquivoRegras.pathSCartas))
                    {
                        System.IO.File.SetAttributes(Classes.ArquivoRegras.pathSCartas, System.IO.FileAttributes.Normal);
                        System.IO.File.Delete(Classes.ArquivoRegras.pathSCartas);
                    }
                    System.IO.StreamWriter sw = new System.IO.StreamWriter(Classes.ArquivoRegras.pathSCartas);
                    sw.Write(sb.ToString());
                    sw.Close();
                    CalcularMedia();
                    System.IO.File.SetAttributes(Classes.ArquivoRegras.pathSCartas, System.IO.FileAttributes.ReadOnly);
                }
                catch { Classes.ArquivoRegras.ReCriar(); CalcularMedia(); }
            }

            void ChecarCB()
            {
                byte qtdAll = 0, all = 0, qtdCo = 0, co = 0, qtdRa = 0, ra = 0, qtdEp = 0, ep = 0, qtdLen = 0, len = 0, qtdTr = 0, tr = 0, qtdCon = 0, con = 0, qtdFei = 0, fei = 0;

                for (byte i = 0; i < cbPermitir.Length; i++)
                {
                    qtdAll++;
                    if (cbPermitir[i].Checked) all++;
                    if (_Deck.CartasInformacao[i + 1].Split('\n')[1] == "Raridade: Comum")
                    {
                        qtdCo++;
                        if (cbPermitir[i].Checked) co++;
                    }
                    else if (_Deck.CartasInformacao[i + 1].Split('\n')[1] == "Raridade: Rara")
                    {
                        qtdRa++;
                        if (cbPermitir[i].Checked) ra++;
                    }
                    else if (_Deck.CartasInformacao[i + 1].Split('\n')[1] == "Raridade: Épica")
                    {
                        qtdEp++;
                        if (cbPermitir[i].Checked) ep++;
                    }
                    else if (_Deck.CartasInformacao[i + 1].Split('\n')[1] == "Raridade: Lendária")
                    {
                        qtdLen++;
                        if (cbPermitir[i].Checked) len++;
                    }
                    if (_Deck.CartasInformacao[i + 1].Split('\n')[2] == "Tipo: Tropa")
                    {
                        qtdTr++;
                        if (cbPermitir[i].Checked) tr++;
                    }
                    else if (_Deck.CartasInformacao[i + 1].Split('\n')[2] == "Tipo: Construção")
                    {
                        qtdCon++;
                        if (cbPermitir[i].Checked) con++;
                    }
                    else if (_Deck.CartasInformacao[i + 1].Split('\n')[2] == "Tipo: Feitiço")
                    {
                        qtdFei++;
                        if (cbPermitir[i].Checked) fei++;
                    }
                }

                tsItems[2].Text = string.Format("Selecionar Todas {0}/{1}", all, qtdAll);
                tsItems[2].Checked = all == qtdAll ? true : false;
                tsItems[3].Text = string.Format("Comuns {0}/{1}", co, qtdCo);
                tsItems[3].Checked = co == qtdCo ? true : false;
                tsItems[4].Text = string.Format("Raras {0}/{1}", ra, qtdRa);
                tsItems[4].Checked = ra == qtdRa ? true : false;
                tsItems[5].Text = string.Format("Épicas {0}/{1}", ep, qtdEp);
                tsItems[5].Checked = ep == qtdEp ? true : false;
                tsItems[6].Text = string.Format("Lendárias {0}/{1}", len, qtdLen);
                tsItems[6].Checked = len == qtdLen ? true : false;
                tsItems[7].Text = string.Format("Tropas {0}/{1}", tr, qtdTr);
                tsItems[7].Checked = tr == qtdTr ? true : false;
                tsItems[8].Text = string.Format("Construções {0}/{1}", con, qtdCon);
                tsItems[8].Checked = con == qtdCon ? true : false;
                tsItems[9].Text = string.Format("Feitiços {0}/{1}", fei, qtdFei);
                tsItems[9].Checked = fei == qtdFei ? true : false;
            }

            void Todos()
            {
                if (tsItems[2].Checked)
                    for (byte i = 0; i < cbPermitir.Length; i++)
                        cbPermitir[i].Checked = false;
                else
                    for (byte i = 0; i < cbPermitir.Length; i++)
                        cbPermitir[i].Checked = true;
            }

            void RarTipo(string tipo, byte r, byte t, string rtipo)
            {
                if (tsItems[r].Checked)
                    for (byte i = 0; i < cbPermitir.Length; i++)
                    {
                        if (_Deck.CartasInformacao[i + 1].Split('\n')[t] == string.Format("{0}: {1}", rtipo, tipo))
                            cbPermitir[i].Checked = false;
                    }
                else
                    for (byte i = 0; i < cbPermitir.Length; i++)
                        if (_Deck.CartasInformacao[i + 1].Split('\n')[t] == string.Format("{0}: {1}", rtipo, tipo))
                            cbPermitir[i].Checked = true;
            }

            void Atualizar()
            {
                string[] check = System.IO.File.ReadAllLines(Classes.ArquivoRegras.pathSCartas);
                for (byte i = 0; i < check.Length; i++)
                {
                    try
                    {
                        if (check[i].Split('|')[1] == "Permitido") cbPermitir[i].Checked = true;
                        else cbPermitir[i].Checked = false;
                    }
                    catch { Classes.ArquivoRegras.ReCriar(); CalcularMedia(); }
                }
            }

            void Pesquisar()
            {
                for (byte i = 0; i < grpBoxSCartas.Length; i++)
                {
                    if (RetirarAcentos(txtPesquisa.Text.Trim().ToLower()) == RetirarAcentos(_Deck.CartasInformacao[i + 1].Split('\n')[0].ToLower()))
                    { grpBoxSCartas[i].Select(); txtPesquisa.Clear(); break; }
                    else if (txtPesquisa.Text.Trim() == _Deck.CodigoCartas[i + 1].ToString())
                    { grpBoxSCartas[i].Select(); txtPesquisa.Clear(); break; }
                    else if (i == grpBoxSCartas.Length - 1)
                        txtPesquisa.Select();
                }
            }
            #endregion

            #region Decks salvos
            this.pDecksSalvos = new System.Windows.Forms.Panel
            {
                Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right,
                AutoScroll = true,
                BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle,
                Location = new System.Drawing.Point(167, 29),
                Size = new System.Drawing.Size(766, 441),
            };
            this.pDecksSalvos.Click += (s, e) => pDecksSalvos.Select();

            // Métodos
            void ApagarTodos()
            {
                if (System.IO.File.Exists(Classes.ArquivoRegras.pathDSalvos) && System.IO.File.ReadAllLines(Classes.ArquivoRegras.pathDSalvos).Length > 0)
                {
                    if (System.Windows.Forms.MessageBox.Show("Deseja apagar todos os Decks salvos?", "Decks salvos", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        if (System.IO.File.Exists(Classes.ArquivoRegras.pathDSalvos))
                        {
                            System.IO.File.SetAttributes(Classes.ArquivoRegras.pathDSalvos, System.IO.FileAttributes.Normal);
                            System.IO.File.Delete(Classes.ArquivoRegras.pathDSalvos);
                        }
                        CriaDecksSalvos();
                    }
                }
                else System.Windows.Forms.MessageBox.Show("Não há Decks salvos para serem apagados.", "Decks salvos", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            }

            // conMenu
            conMenu.Items.Add("Atualizar");
            conMenu.Items[0].Image = Properties.Resources.atualizar.ToBitmap();
            conMenu.Items.Add("Apagar Decks salvos");
            conMenu.Items[1].Image = Properties.Resources.apagar.ToBitmap();
            // menuStripDS / TSMIDS
            TSMIDS.DropDownItems.Add("Atualizar");
            TSMIDS.DropDownItems[0].Image = Properties.Resources.atualizar.ToBitmap();
            TSMIDS.DropDownItems.Add("Apagar Decks salvos");
            TSMIDS.DropDownItems[1].Image = Properties.Resources.apagar.ToBitmap();
            menuStripDS.Items.Add(TSMIDS);
            // Eventos
            conMenu.Items[1].Click += (s, e) => ApagarTodos();
            conMenu.Items[0].Click += (s, e) => CriaDecksSalvos();
            TSMIDS.DropDownItems[0].Click += (s, e) => { pDecksSalvos.Select(); CriaDecksSalvos(); };
            TSMIDS.DropDownItems[1].Click += (s, e) => { pDecksSalvos.Select(); ApagarTodos(); };

            pDecksSalvos.Controls.Add(menuStripDS);
            pDecksSalvos.ContextMenuStrip = conMenu;

            CriaDecksSalvos();

            void CriaDecksSalvos()
            {
                int yG = 10, yB1 = 16, yB2 = 50;
                for (byte i = 0; i < (lblDeck == null ? 0 : lblDeck.Length); i++) if (lblDeck[i] != null) lblDeck[i].Dispose();
                for (byte i = 0; i < (picImagem == null ? 0 : picImagem.Length); i++)
                    for (byte j = 0; j < (picImagem[i] == null ? 0 : picImagem[i].Length); j++) picImagem[i][j].Dispose();
                for (byte i = 0; i < (grpBoxDSalvos == null ? 0 : grpBoxDSalvos.Length); i++)
                {
                    grpBoxDSalvos[i].Dispose();
                    btnCopia[i].Dispose();
                    btnCola[i].Dispose();
                    conOpcoes[i].Dispose();
                }
                for (byte i = 0; i < (btnApagar == null ? 0 : btnApagar.Length); i++) if (btnApagar[i] != null) btnApagar[i].Dispose();

                if (System.IO.File.Exists(Classes.ArquivoRegras.pathDSalvos) && System.IO.File.ReadAllLines(Classes.ArquivoRegras.pathDSalvos).Length > 0)
                {
                    pDecksSalvos.Controls.Remove(lblInformacao);
                    decksSalvos = System.IO.File.ReadAllLines(Classes.ArquivoRegras.pathDSalvos);
                }
                else
                {
                    pDecksSalvos.Controls.Add(lblInformacao);
                    return;
                }

                grpBoxDSalvos = new System.Windows.Forms.GroupBox[decksSalvos.Length > 50 ? 50 : decksSalvos.Length];
                btnCola = new System.Windows.Forms.Button[decksSalvos.Length > 50 ? 50 : decksSalvos.Length];
                btnCopia = new System.Windows.Forms.Button[decksSalvos.Length > 50 ? 50 : decksSalvos.Length];
                btnApagar = new System.Windows.Forms.Button[decksSalvos.Length > 50 ? 50 : decksSalvos.Length];
                lblDeck = new System.Windows.Forms.Label[decksSalvos.Length > 50 ? 50 : decksSalvos.Length];
                conOpcoes = new System.Windows.Forms.ContextMenuStrip[decksSalvos.Length > 50 ? 50 : decksSalvos.Length];
                picImagem = new System.Windows.Forms.PictureBox[decksSalvos.Length > 50 ? 50 : decksSalvos.Length][];

                for (byte i = 0; i < (decksSalvos.Length > 50 ? 50 : decksSalvos.Length); i++)
                {
                    byte copiaI = i;

                    //Sets conOpcoes
                    conOpcoes[i] = new System.Windows.Forms.ContextMenuStrip();
                    conOpcoes[i].Items.Add("Mostrar Deck - Gráfico");
                    conOpcoes[i].Items[0].Image = Properties.Resources.mostrar.ToBitmap();
                    conOpcoes[i].Items.Add("Copiar Deck");
                    conOpcoes[i].Items[1].Image = Properties.Resources.copiar.ToBitmap();
                    conOpcoes[i].Items.Add("Colar Deck");
                    conOpcoes[i].Items[2].Image = Properties.Resources.colar.ToBitmap();
                    conOpcoes[i].Items.Add("Apagar Deck");
                    conOpcoes[i].Items[3].Image = Properties.Resources.apagar.ToBitmap();

                    // gpBox
                    grpBoxDSalvos[i] = new System.Windows.Forms.GroupBox
                    {
                        Size = new System.Drawing.Size(536, 0),
                        ContextMenuStrip = conOpcoes[i],
                        BackColor = System.Drawing.Color.Transparent,
                        ForeColor = corLetra,
                        Font = new System.Drawing.Font(Font.FontFamily, 8.25f, System.Drawing.FontStyle.Bold),
                        TabStop = false
                    };
                    grpBoxDSalvos[i].Location = new System.Drawing.Point((pDecksSalvos.Size.Width - grpBoxDSalvos[i].Size.Width) / 2 - 30, yG);

                    float m = 0.0f;
                    for (byte b = 0; b < decksSalvos[i].Split(';').Length; b++)
                        for (byte j = 0; j < _Deck.CustoElixir.Length; j++) if (decksSalvos[i].Split(';')[b] == _Deck.CodigoCartas[j].ToString()) m += _Deck.CustoElixir[j] / 8;
                    grpBoxDSalvos[i].Text = string.Format("Deck {0} - Elixir Médio: {1:f1}", i + 1, m).Replace(',', '.');

                    // btnCopia
                    btnCopia[i] = new System.Windows.Forms.Button
                    {
                        Text = "Copiar Deck",
                        TabIndex = i,
                        Font = new System.Drawing.Font(Font.FontFamily, 8.25f, System.Drawing.FontStyle.Bold),
                        FlatStyle = System.Windows.Forms.FlatStyle.Flat,
                        BackColor = corFundo2,
                        ForeColor = System.Drawing.Color.White,
                        Cursor = System.Windows.Forms.Cursors.Hand,
                        Size = new System.Drawing.Size(98, 27),
                        Location = new System.Drawing.Point(grpBoxDSalvos[i].Location.X + grpBoxDSalvos[i].Size.Width + 10, yB1)
                    };
                    btnCopia[i].FlatAppearance.MouseDownBackColor = corFundoClick;

                    // btnCola
                    btnCola[i] = new System.Windows.Forms.Button
                    {
                        Text = "Colar Deck",
                        TabIndex = i,
                        Font = new System.Drawing.Font(Font.FontFamily, 9f, System.Drawing.FontStyle.Bold),
                        FlatStyle = System.Windows.Forms.FlatStyle.Flat,
                        BackColor = corFundo2,
                        ForeColor = System.Drawing.Color.White,
                        Cursor = System.Windows.Forms.Cursors.Hand,
                        Size = new System.Drawing.Size(98, 27),
                        Location = new System.Drawing.Point(grpBoxDSalvos[i].Location.X + grpBoxDSalvos[i].Size.Width + 10, yB2)
                    };
                    btnCola[i].FlatAppearance.MouseDownBackColor = corFundoClick;

                    if (rbGrafico.Checked) CriaCartas();
                    else CriaTexto();

                    // Métodos
                    tip.SetToolTip(btnCopia[i], "Copiar Deck");
                    tip.SetToolTip(btnCola[i], "Colar Deck no Gerador");
                    void Copiar()
                    {
                        grpBoxDSalvos[copiaI].Select();
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        sb.AppendFormat("https://link.clashroyale.com/deck/pt?deck={0}", decksSalvos[copiaI]);
                        System.Windows.Forms.Clipboard.SetText(sb.ToString());
                    }
                    void Colar()
                    {
                        grpBoxDSalvos[copiaI].Select();
                        for (byte j = 0; j < _Deck.deckAtual.Length; j++)
                            _Deck.deckAnterior[j] = _Deck.deckAtual[j];
                        string[] cDeck = decksSalvos[copiaI].Split(';');
                        media = 0.0f;
                        for (byte j = 0; j < cDeck.Length; j++)
                            for (byte b = 0; b < _Deck.CodigoCartas.Length; b++)
                                if (cDeck[j] == _Deck.CodigoCartas[b].ToString())
                                {
                                    _Deck.deckAtual[j] = b;
                                    media += _Deck.CustoElixir[b] / 8;
                                    Cartas[j].Image = _Deck.CartasImagem[b];
                                    if (ckNome.Checked)
                                        tip.SetToolTip(Cartas[j], _Deck.CartasInformacao[b].Split('\n')[0]);
                                }
                        lblMedia.Text = string.Format("Elixir Médio: {0:f1}", media).Replace(',', '.');
                        btnVoltarDeck.Enabled = true;
                    }
                    void ApagarAtual()
                    {
                        System.IO.File.SetAttributes(Classes.ArquivoRegras.pathDSalvos, System.IO.FileAttributes.Normal);
                        System.IO.StreamWriter sw = new System.IO.StreamWriter(Classes.ArquivoRegras.pathDSalvos);
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        for (byte j = 0; j < decksSalvos.Length; j++)
                        {
                            if (decksSalvos[copiaI] != decksSalvos[j])
                                sb.AppendFormat("{0}{1}", decksSalvos[j], j == decksSalvos.Length - 1 ? string.Empty : System.Environment.NewLine);
                        }
                        sw.Write(sb.ToString());
                        sw.Close();
                        if (System.IO.File.Exists(Classes.ArquivoRegras.pathDSalvos) && System.IO.File.ReadAllLines(Classes.ArquivoRegras.pathDSalvos).Length == 0)
                            System.IO.File.Delete(Classes.ArquivoRegras.pathDSalvos);
                        else System.IO.File.SetAttributes(Classes.ArquivoRegras.pathDSalvos, System.IO.FileAttributes.Hidden | System.IO.FileAttributes.ReadOnly);
                        CriaDecksSalvos();
                    }
                    void CriaCartas()
                    {
                        conOpcoes[copiaI].Items[0].Text = "Mostrar Deck - Minimalista";
                        grpBoxDSalvos[copiaI].Size = new System.Drawing.Size(536, 340);
                        picImagem[copiaI] = new System.Windows.Forms.PictureBox[8];
                        int x1 = 6, x2 = 6;

                        for (byte j = 0; j < picImagem[copiaI].Length; j++)
                        {
                            byte copiaJ = j;
                            picImagem[copiaI][j] = new System.Windows.Forms.PictureBox
                            {
                                BackColor = System.Drawing.Color.Transparent,
                                Size = new System.Drawing.Size(124, 150),
                                SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage,
                                Cursor = System.Windows.Forms.Cursors.Hand,
                            };

                            if (j < 4) { picImagem[copiaI][j].Location = new System.Drawing.Point(x1, 19); x1 += 133; }
                            else { picImagem[copiaI][j].Location = new System.Drawing.Point(x2, 178); x2 += 133; }

                            int posInicialXX = picImagem[copiaI][j].Location.X;
                            int posInicialYY = picImagem[copiaI][j].Location.Y;
                            int tamInicialHeigth = picImagem[copiaI][j].Size.Height;
                            int tamInicialWidth = picImagem[copiaI][j].Size.Width;

                            picImagem[copiaI][j].MouseEnter += (ss, ee) =>
                            {
                                picImagem[copiaI][copiaJ].Location = new System.Drawing.Point(posInicialXX - System.Convert.ToByte(nUpTCarta.Value), posInicialYY - System.Convert.ToByte(nUpTCarta.Value));
                                picImagem[copiaI][copiaJ].Size = new System.Drawing.Size(tamInicialWidth + System.Convert.ToByte(nUpTCarta.Value) * 2, tamInicialHeigth + System.Convert.ToByte(nUpTCarta.Value) * 2);
                            };
                            picImagem[copiaI][j].MouseLeave += (ss, ee) =>
                            {
                                picImagem[copiaI][copiaJ].Location = new System.Drawing.Point(posInicialXX, posInicialYY);
                                picImagem[copiaI][copiaJ].Size = new System.Drawing.Size(tamInicialWidth, tamInicialHeigth);
                            };
                            picImagem[copiaI][j].MouseDown += (s, e) =>
                            {
                                if (ckEfeitoClick.Checked)
                                {
                                    picImagem[copiaI][copiaJ].Location = new System.Drawing.Point(picImagem[copiaI][copiaJ].Location.X + 2 + System.Convert.ToByte(nUpTCarta.Value), picImagem[copiaI][copiaJ].Location.Y + 2 + System.Convert.ToByte(nUpTCarta.Value));
                                    picImagem[copiaI][copiaJ].Size = new System.Drawing.Size(picImagem[copiaI][copiaJ].Size.Width - 4 - System.Convert.ToByte(nUpTCarta.Value) * 2, picImagem[copiaI][copiaJ].Size.Height - 4 - System.Convert.ToByte(nUpTCarta.Value) * 2);
                                }
                            };
                            picImagem[copiaI][j].MouseUp += (s, e) =>
                            {
                                if (ckEfeitoClick.Checked)
                                {
                                    picImagem[copiaI][copiaJ].Location = new System.Drawing.Point(picImagem[copiaI][copiaJ].Location.X - 2 - System.Convert.ToByte(nUpTCarta.Value), picImagem[copiaI][copiaJ].Location.Y - 2 - System.Convert.ToByte(nUpTCarta.Value));
                                    picImagem[copiaI][copiaJ].Size = new System.Drawing.Size(picImagem[copiaI][copiaJ].Size.Width + 4 + System.Convert.ToByte(nUpTCarta.Value) * 2, picImagem[copiaI][copiaJ].Size.Height + 4 + System.Convert.ToByte(nUpTCarta.Value) * 2);
                                }
                            };

                            for (byte b = 0; b < _Deck.CartasImagem.Length; b++)
                                if (decksSalvos[copiaI].Split(';')[j] == _Deck.CodigoCartas[b].ToString())
                                {
                                    picImagem[copiaI][j].Image = _Deck.CartasImagem[b];
                                    if (ckNome.Checked)
                                        tip.SetToolTip(picImagem[copiaI][j], _Deck.CartasInformacao[b].Split('\n')[0]);
                                }

                            picImagem[copiaI][j].Click += (ss, ee) =>
                            {
                                grpBoxDSalvos[copiaI].Select();

                                for (byte b = 1; b < _Deck.CartasInformacao.Length; b++)
                                    if (decksSalvos[copiaI].Split(';')[copiaJ] == _Deck.CodigoCartas[b].ToString())
                                    {
                                        string infoAdicional = string.Empty;
                                        byte v = 0;

                                        for (byte c = System.Convert.ToByte(valores.Length - 1); c > 0; c--)
                                            if (b < valores[c])
                                            {
                                                infoAdicional = string.Format("Arena: {0} {1}", arenas[v], v != 0 ? "(" + v + ")" : string.Empty); break;
                                            }
                                            else v++;

                                        lblInformacoes.Text = _Deck.CartasInformacao[b] + System.Environment.NewLine +
                                        string.Format("Custo de Elixir: {0}", _Deck.CustoElixir[b]).Replace(',', '.') + System.Environment.NewLine +
                                        infoAdicional;
                                        break;
                                    }
                            };
                            grpBoxDSalvos[copiaI].Controls.Add(picImagem[copiaI][j]);
                        }

                        btnApagar[copiaI] = new System.Windows.Forms.Button()
                        {
                            Text = "Apagar Deck",
                            TabIndex = copiaI + 1,
                            Size = new System.Drawing.Size(98, 27),
                            FlatStyle = System.Windows.Forms.FlatStyle.Flat,
                            BackColor = corFundo2,
                            Cursor = System.Windows.Forms.Cursors.Hand,
                            ForeColor = System.Drawing.Color.White,
                            Font = new System.Drawing.Font(Font.FontFamily, 9f, System.Drawing.FontStyle.Bold),
                            Location = new System.Drawing.Point(grpBoxDSalvos[copiaI].Location.X + grpBoxDSalvos[copiaI].Size.Width + 10, btnCola[copiaI].Location.Y + 34),
                        };
                        btnApagar[copiaI].FlatAppearance.MouseDownBackColor = corFundo2;
                        btnApagar[copiaI].Click += (s, e) => ApagarAtual();
                        tip.SetToolTip(btnApagar[copiaI], "Apagar Deck");
                        pDecksSalvos.Controls.Add(btnApagar[copiaI]);
                    }
                    void CriaTexto()
                    {
                        grpBoxDSalvos[copiaI].Size = new System.Drawing.Size(536, 69);

                        lblDeck[copiaI] = new System.Windows.Forms.Label
                        {
                            AutoSize = true,
                            Location = new System.Drawing.Point(15, 21),
                            Font = new System.Drawing.Font(Font.FontFamily, 9f, System.Drawing.FontStyle.Bold)
                        };
                        for (byte k = 0; k < decksSalvos[copiaI].Split(';').Length; k++)
                            for (byte j = 0; j < _Deck.CodigoCartas.Length; j++)
                                if (decksSalvos[copiaI].Split(';')[k] == _Deck.CodigoCartas[j].ToString())
                                    lblDeck[copiaI].Text += string.Format("{0}{1}{2}", _Deck.CartasInformacao[j].Split('\n')[0], k == decksSalvos[copiaI].Length - 1 ? "." : ", ", k == 3 ? System.Environment.NewLine : string.Empty);

                        grpBoxDSalvos[copiaI].Controls.Add(lblDeck[copiaI]);
                    }

                    // Eventos
                    grpBoxDSalvos[i].Click += (s, e) => grpBoxDSalvos[copiaI].Select();
                    if (lblDeck[i] != null) lblDeck[i].Click += (s, e) => grpBoxDSalvos[copiaI].Select();
                    btnCopia[i].Click += (s, e) => Copiar();
                    btnCola[i].Click += (s, e) => Colar();
                    conOpcoes[i].Items[0].Click += (s, e) =>
                    {
                        if (conOpcoes[copiaI].Items[0].Text == "Mostrar Deck - Gráfico")
                        {
                            conOpcoes[copiaI].Items[0].Text = "Mostrar Deck - Minimalista";
                            grpBoxDSalvos[copiaI].Controls.Remove(lblDeck[copiaI]);
                            CriaCartas();

                            for (byte j = System.Convert.ToByte(copiaI + 1); j < decksSalvos.Length; j++)
                            {
                                grpBoxDSalvos[j].Location = new System.Drawing.Point(grpBoxDSalvos[j].Location.X, grpBoxDSalvos[j].Location.Y + 269);
                                btnCopia[j].Location = new System.Drawing.Point(btnCopia[j].Location.X, btnCopia[j].Location.Y + 269);
                                btnCopia[j].TabIndex = j + 3;
                                btnCola[j].Location = new System.Drawing.Point(btnCola[j].Location.X, btnCola[j].Location.Y + 269);
                                btnCola[j].TabIndex = j + 4;
                                if (btnApagar[j] != null) btnApagar[j].Location = new System.Drawing.Point(btnApagar[j].Location.X, btnApagar[j].Location.Y + 269);
                                if (btnApagar[j] != null) btnCopia[j].TabIndex = j + 5;
                            }
                        }
                        else
                        {
                            conOpcoes[copiaI].Items[0].Text = "Mostrar Deck - Gráfico";
                            for (byte j = 0; j < picImagem[copiaI].Length; j++)
                                picImagem[copiaI][j].Dispose();

                            btnApagar[copiaI].Dispose();

                            for (byte j = System.Convert.ToByte(copiaI + 1); j < decksSalvos.Length; j++)
                            {
                                grpBoxDSalvos[j].Location = new System.Drawing.Point(grpBoxDSalvos[j].Location.X, grpBoxDSalvos[j].Location.Y - 269);
                                btnCopia[j].Location = new System.Drawing.Point(btnCopia[j].Location.X, btnCopia[j].Location.Y - 269);
                                btnCopia[j].TabIndex = j + 3;
                                btnCola[j].Location = new System.Drawing.Point(btnCola[j].Location.X, btnCola[j].Location.Y - 269);
                                btnCola[j].TabIndex = j + 4;
                                if (btnApagar[j] != null) btnApagar[j].Location = new System.Drawing.Point(btnApagar[j].Location.X, btnApagar[j].Location.Y - 269);
                                if (btnApagar[j] != null) btnCopia[j].TabIndex = j + 5;
                            }

                            CriaTexto();
                        }

                        byte num = 0;
                        for (byte j = 0; j < decksSalvos.Length; j++)
                        {
                            if (btnCopia[j] != null) btnCopia[j].TabIndex = num++;
                            if (btnCola[j] != null) btnCola[j].TabIndex = num++;
                            if (btnApagar[j] != null) btnApagar[j].TabIndex = num++;
                        }
                        grpBoxDSalvos[copiaI].Select();
                    };
                    conOpcoes[i].Items[1].Click += (s, e) => Copiar();
                    conOpcoes[i].Items[2].Click += (s, e) => Colar();
                    conOpcoes[i].Items[3].Click += (s, e) => ApagarAtual();

                    // Controls add
                    pDecksSalvos.Controls.Add(grpBoxDSalvos[i]);
                    pDecksSalvos.Controls.Add(btnCopia[i]);
                    pDecksSalvos.Controls.Add(btnCola[i]);

                    yG += 79 + (rbGrafico.Checked ? 269 : 0); yB1 += 79 + (rbGrafico.Checked ? 269 : 0); yB2 += 79 + (rbGrafico.Checked ? 269 : 0);
                }
            }
            #endregion

            #region Melhores Decks
            this.pMelhoresDecks = new System.Windows.Forms.Panel()
            {
                Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right,
                AutoScroll = true,
                BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle,
                Location = new System.Drawing.Point(167, 29),
                Size = new System.Drawing.Size(766, 441),
            };
            this.pMelhoresDecks.Click += (s, e) => pMelhoresDecks.Select();

            tsmiMDecks = new System.Windows.Forms.ToolStripMenuItem() { Text = "Menu" };
            tsmiMDecks.DropDownItems.Add("Atualizar");
            tsmiMDecks.DropDownItems[0].Image = Properties.Resources.atualizar.ToBitmap();
            tsmiMDecks.DropDownItems[0].Click += (s, e) => { pMelhoresDecks.Select(); BaixaMelhoresDecks(); };
            tsmiMDecks.DropDownItems.Add("Apagar Melhores Decks");
            tsmiMDecks.DropDownItems[1].Image = Properties.Resources.apagar.ToBitmap();
            tsmiMDecks.DropDownItems[1].Click += (s, e) => ApagaMelhoresDecks();

            menuStripMDecks.Items.Add(tsmiMDecks);

            cmsMDecks = new System.Windows.Forms.ContextMenuStrip();
            cmsMDecks.Items.Add("Atualizar");
            cmsMDecks.Items[0].Image = Properties.Resources.atualizar.ToBitmap();
            cmsMDecks.Items[0].Click += (s, e) => BaixaMelhoresDecks();
            cmsMDecks.Items.Add("Apagar Melhores Decks");
            cmsMDecks.Items[1].Image = Properties.Resources.apagar.ToBitmap();
            cmsMDecks.Items[1].Click += (s, e) => ApagaMelhoresDecks();

            pMelhoresDecks.ContextMenuStrip = cmsMDecks;
            pMelhoresDecks.Controls.Add(menuStripMDecks);

            if (System.IO.File.Exists(Classes.ArquivoRegras.pathMDecks) && System.IO.File.ReadAllLines(Classes.ArquivoRegras.pathMDecks).Length > 0)
                CriaMelhoresDecks();
            else pMelhoresDecks.Controls.Add(lblAjuda2);

            void CriaMelhoresDecks()
            {
                pMelhoresDecks.Select();
                System.IO.File.SetAttributes(Classes.ArquivoRegras.pathMDecks, System.IO.FileAttributes.Normal);
                pMelhoresDecks.Controls.Remove(lblAjuda2);

                for (byte i = 0; i < (picImagemMDecks == null ? 0 : picImagemMDecks.Length); i++)
                    for (byte j = 0; j < (picImagemMDecks[i] == null ? 0 : picImagemMDecks[i].Length); j++) picImagemMDecks[i][j].Dispose();
                for (byte i = 0; i < (grpBoxMDecks == null ? 0 : grpBoxMDecks.Length); i++) grpBoxMDecks[i].Dispose();
                for (byte i = 0; i < (btnCopiaMDecks == null ? 0 : btnCopiaMDecks.Length); i++) btnCopiaMDecks[i].Dispose();
                for (byte i = 0; i < (btnColaMDecks == null ? 0 : btnColaMDecks.Length); i++) btnColaMDecks[i].Dispose();
                for (byte i = 0; i < (btnSalvaMDecks == null ? 0 : btnSalvaMDecks.Length); i++) btnSalvaMDecks[i].Dispose();

                int yG = 10, yB1 = 17, yB2 = 50, yB3 = 83;
                melhoresDecks = System.IO.File.ReadAllLines(Classes.ArquivoRegras.pathMDecks);

                System.Windows.Forms.ContextMenuStrip[] cmsGBMDecks = new System.Windows.Forms.ContextMenuStrip[melhoresDecks.Length > 11 ? 11 : melhoresDecks.Length];
                grpBoxMDecks = new System.Windows.Forms.GroupBox[melhoresDecks.Length > 11 ? 11 : melhoresDecks.Length];
                btnCopiaMDecks = new System.Windows.Forms.Button[melhoresDecks.Length > 11 ? 11 : melhoresDecks.Length];
                btnColaMDecks = new System.Windows.Forms.Button[melhoresDecks.Length > 11 ? 11 : melhoresDecks.Length];
                btnSalvaMDecks = new System.Windows.Forms.Button[melhoresDecks.Length > 11 ? 11 : melhoresDecks.Length];
                picImagemMDecks = new System.Windows.Forms.PictureBox[melhoresDecks.Length > 11 ? 11 : melhoresDecks.Length][];

                for (byte i = 0; i < (melhoresDecks.Length > 11 ? 11 : melhoresDecks.Length); i++)
                {
                    byte copiaI = i;

                    void Copiar()
                    {
                        grpBoxMDecks[copiaI].Select();
                        string deck = "https://link.clashroyale.com/deck/pt?deck=";
                        for (byte j = 0; j < melhoresDecks[copiaI].Split('|')[0].Split(';').Length; j++)
                            deck += melhoresDecks[copiaI].Split('|')[0].Split(';')[j] + (j == melhoresDecks[copiaI].Split('|')[0].Split(';').Length - 1 ? string.Empty : ";");
                        System.Windows.Forms.Clipboard.SetText(deck);
                    }

                    void Colar()
                    {
                        grpBoxMDecks[copiaI].Select();
                        for (byte j = 0; j < _Deck.deckAnterior.Length; j++)
                            _Deck.deckAnterior[j] = _Deck.deckAtual[j];
                        btnVoltarDeck.Enabled = true;
                        media = 0.0f;

                        for (byte j = 0; j < _Deck.deckAtual.Length; j++)
                        {
                            for (byte k = 1; k < _Deck.CartasInformacao.Length; k++)
                                if (melhoresDecks[copiaI].Split('|')[0].Split(';')[j] == _Deck.CodigoCartas[k].ToString())
                                {
                                    _Deck.deckAtual[j] = k;
                                    Cartas[j].Image = _Deck.CartasImagem[k];
                                    media += _Deck.CustoElixir[k] / 8;
                                    if (ckNome.Checked) tip.SetToolTip(Cartas[j], _Deck.CartasInformacao[k].Split('\n')[0]);
                                }
                        }
                        lblMedia.Text = string.Format("Elixir Médio: {0:f1}", media).Replace(',', '.');
                    }

                    void SalvarMD()
                    {
                        grpBoxMDecks[copiaI].Select();

                        System.Text.StringBuilder sb = new System.Text.StringBuilder();

                        if (System.IO.File.Exists(Classes.ArquivoRegras.pathDSalvos) && System.IO.File.ReadAllLines(Classes.ArquivoRegras.pathDSalvos).Length > 49)
                        {
                            System.Windows.Forms.MessageBox.Show("Não é permitido salvar mais de 50 Decks. Delete algum para salvar novamente.", "Melhores Decks", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            return;
                        }
                        else if (System.IO.File.Exists(Classes.ArquivoRegras.pathDSalvos))
                        {
                            string[] deck = System.IO.File.ReadAllLines(Classes.ArquivoRegras.pathDSalvos);
                            for (byte k = 0; k < deck.Length; k++)
                            {
                                byte soma = 0;
                                for (byte j = 0; j < deck[k].Split(';').Length; j++)
                                    for (byte b = 0; b < melhoresDecks[copiaI].Split('|')[0].Split(';').Length; b++)
                                        if (melhoresDecks[copiaI].Split('|')[0].Split(';')[j] == deck[k].Split(';')[b])
                                        { soma++; break; }
                                if (soma == 8)
                                {
                                    System.Windows.Forms.MessageBox.Show("Já existe um Deck com as mesmas Cartas salvo.", "Melhores Decks", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                    return;
                                }
                                sb.AppendFormat("{0}{1}", deck[k].Trim(), System.Environment.NewLine);
                            }
                        }
                        if (System.IO.File.Exists(Classes.ArquivoRegras.pathDSalvos))
                            System.IO.File.SetAttributes(Classes.ArquivoRegras.pathDSalvos, System.IO.FileAttributes.Normal);
                        System.IO.StreamWriter sw = new System.IO.StreamWriter(Classes.ArquivoRegras.pathDSalvos);
                        for (byte j = 0; j < melhoresDecks[copiaI].Split('|')[0].Split(';').Length; j++)
                            sb.AppendFormat("{0}{1}", melhoresDecks[copiaI].Split('|')[0].Split(';')[j], j == melhoresDecks[copiaI].Split('|')[0].Split(';').Length - 1 ? string.Empty : ";");
                        sw.Write(sb.ToString());
                        sw.Close();
                        System.IO.File.SetAttributes(Classes.ArquivoRegras.pathDSalvos, System.IO.FileAttributes.Hidden | System.IO.FileAttributes.ReadOnly);
                    }

                    cmsGBMDecks[i] = new System.Windows.Forms.ContextMenuStrip();
                    cmsGBMDecks[i].Items.Add("Copiar Deck");
                    cmsGBMDecks[i].Items[0].Image = Properties.Resources.copiar.ToBitmap();
                    cmsGBMDecks[i].Items[0].Click += (s, e) => Copiar();
                    cmsGBMDecks[i].Items.Add("Colar Deck");
                    cmsGBMDecks[i].Items[1].Image = Properties.Resources.colar.ToBitmap();
                    cmsGBMDecks[i].Items[1].Click += (s, e) => Colar();
                    cmsGBMDecks[i].Items.Add("Salvar Deck");
                    cmsGBMDecks[i].Items[2].Image = Properties.Resources.salvar.ToBitmap();
                    cmsGBMDecks[i].Items[2].Click += (s, e) => SalvarMD();

                    float elixirMedio = 0.0f;
                    for (byte j = 0; j < melhoresDecks[i].Split('|')[0].Split(';').Length; j++)
                        for (byte k = 1; k < _Deck.CodigoCartas.Length; k++)
                            if (melhoresDecks[i].Split('|')[0].Split(';')[j] == _Deck.CodigoCartas[k].ToString())
                                elixirMedio += _Deck.CustoElixir[k] / 8;

                    grpBoxMDecks[i] = new System.Windows.Forms.GroupBox()
                    {
                        Size = new System.Drawing.Size(536, 340),
                        BackColor = System.Drawing.Color.Transparent,
                        ForeColor = corLetra,
                        Font = new System.Drawing.Font(Font.FontFamily, 8.25f, System.Drawing.FontStyle.Bold),
                        Text = string.Format("Elixir Médio: {0:f1} - Arena {1} +", elixirMedio, melhoresDecks[i].Split('|')[1]).Replace(',', '.'),
                    };
                    grpBoxMDecks[i].Location = new System.Drawing.Point((pMelhoresDecks.Size.Width - grpBoxMDecks[i].Size.Width) / 2 - 30, yG);
                    grpBoxMDecks[i].Click += (s, e) => grpBoxMDecks[copiaI].Select();
                    grpBoxMDecks[i].ContextMenuStrip = cmsGBMDecks[i];

                    picImagemMDecks[i] = new System.Windows.Forms.PictureBox[8];
                    int x1 = 6, x2 = 6;

                    for (byte j = 0; j < picImagemMDecks[i].Length; j++)
                    {
                        byte copiaJ = j;
                        picImagemMDecks[i][j] = new System.Windows.Forms.PictureBox()
                        {
                            Size = new System.Drawing.Size(124, 150),
                            SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage,
                            BackColor = System.Drawing.Color.Transparent,
                            Cursor = System.Windows.Forms.Cursors.Hand
                        };

                        for (byte k = 0; k < _Deck.CartasInformacao.Length; k++)
                            if (melhoresDecks[copiaI].Split('|')[0].Split(';')[j] == _Deck.CodigoCartas[k].ToString())
                            {
                                picImagemMDecks[i][j].Image = _Deck.CartasImagem[k];
                                if (ckNome.Checked) tip.SetToolTip(picImagemMDecks[i][j], _Deck.CartasInformacao[k].Split('\n')[0]);
                                break;
                            }

                        if (j < 4) { picImagemMDecks[i][j].Location = new System.Drawing.Point(x1, 19); x1 += 133; }
                        else { picImagemMDecks[i][j].Location = new System.Drawing.Point(x2, 178); x2 += 133; }

                        int posInicialXX = picImagemMDecks[i][j].Location.X;
                        int posInicialYY = picImagemMDecks[i][j].Location.Y;
                        int tamInicialHeigth = picImagemMDecks[i][j].Size.Height;
                        int tamInicialWidth = picImagemMDecks[i][j].Size.Width;

                        picImagemMDecks[i][j].MouseEnter += (s, e) =>
                        {
                            picImagemMDecks[copiaI][copiaJ].Location = new System.Drawing.Point(posInicialXX - System.Convert.ToByte(nUpTCarta.Value), posInicialYY - System.Convert.ToByte(nUpTCarta.Value));
                            picImagemMDecks[copiaI][copiaJ].Size = new System.Drawing.Size(tamInicialWidth + System.Convert.ToByte(nUpTCarta.Value) * 2, tamInicialHeigth + System.Convert.ToByte(nUpTCarta.Value) * 2);
                        };
                        picImagemMDecks[i][j].MouseLeave += (s, e) =>
                        {
                            picImagemMDecks[copiaI][copiaJ].Location = new System.Drawing.Point(posInicialXX, posInicialYY);
                            picImagemMDecks[copiaI][copiaJ].Size = new System.Drawing.Size(tamInicialWidth, tamInicialHeigth);
                        };
                        picImagemMDecks[i][j].MouseDown += (s, e) =>
                        {
                            if (ckEfeitoClick.Checked)
                            {
                                picImagemMDecks[copiaI][copiaJ].Location = new System.Drawing.Point(picImagemMDecks[copiaI][copiaJ].Location.X + 2 + System.Convert.ToByte(nUpTCarta.Value), picImagemMDecks[copiaI][copiaJ].Location.Y + 2 + System.Convert.ToByte(nUpTCarta.Value));
                                picImagemMDecks[copiaI][copiaJ].Size = new System.Drawing.Size(picImagemMDecks[copiaI][copiaJ].Size.Width - 4 - System.Convert.ToByte(nUpTCarta.Value) * 2, picImagemMDecks[copiaI][copiaJ].Size.Height - 4 - System.Convert.ToByte(nUpTCarta.Value) * 2);
                            }
                        };
                        picImagemMDecks[i][j].MouseUp += (s, e) =>
                        {
                            if (ckEfeitoClick.Checked)
                            {
                                picImagemMDecks[copiaI][copiaJ].Location = new System.Drawing.Point(picImagemMDecks[copiaI][copiaJ].Location.X - 2 - System.Convert.ToByte(nUpTCarta.Value), picImagemMDecks[copiaI][copiaJ].Location.Y - 2 - System.Convert.ToByte(nUpTCarta.Value));
                                picImagemMDecks[copiaI][copiaJ].Size = new System.Drawing.Size(picImagemMDecks[copiaI][copiaJ].Size.Width + 4 + System.Convert.ToByte(nUpTCarta.Value) * 2, picImagemMDecks[copiaI][copiaJ].Size.Height + 4 + System.Convert.ToByte(nUpTCarta.Value) * 2);
                            }
                        };
                        picImagemMDecks[i][j].Click += (ss, ee) =>
                        {
                            grpBoxMDecks[copiaI].Select();

                            for (byte k = 1; k < _Deck.CartasInformacao.Length; k++)
                                if (melhoresDecks[copiaI].Split(';')[copiaJ] == _Deck.CodigoCartas[k].ToString())
                                {
                                    string infoAdicional = string.Empty;
                                    byte v = 0;

                                    for (byte c = System.Convert.ToByte(valores.Length - 1); c > 0; c--)
                                        if (k < valores[c])
                                        {
                                            infoAdicional = string.Format("Arena: {0} {1}", arenas[v], v != 0 ? "(" + v + ")" : string.Empty); break;
                                        }
                                        else v++;

                                    lblInformacoes.Text = _Deck.CartasInformacao[k] + System.Environment.NewLine +
                                    string.Format("Custo de Elixir: {0}", _Deck.CustoElixir[k]).Replace(',', '.') + System.Environment.NewLine +
                                    infoAdicional;
                                    break;
                                }
                        };

                        grpBoxMDecks[i].Controls.Add(picImagemMDecks[i][j]);
                    }

                    btnCopiaMDecks[i] = new System.Windows.Forms.Button()
                    {
                        Text = "Copiar Deck",
                        Font = new System.Drawing.Font(Font.FontFamily, 8.25f, System.Drawing.FontStyle.Bold),
                        FlatStyle = System.Windows.Forms.FlatStyle.Flat,
                        BackColor = corFundo2,
                        ForeColor = System.Drawing.Color.White,
                        Cursor = System.Windows.Forms.Cursors.Hand,
                        Size = new System.Drawing.Size(98, 27),
                        Location = new System.Drawing.Point(grpBoxMDecks[i].Location.X + grpBoxMDecks[i].Size.Width + 10, yB1)
                    };
                    btnCopiaMDecks[i].Click += (s, e) => Copiar();
                    btnCopiaMDecks[i].FlatAppearance.MouseDownBackColor = corFundoClick;
                    tip.SetToolTip(btnCopiaMDecks[i], "Copiar Deck");

                    btnColaMDecks[i] = new System.Windows.Forms.Button()
                    {
                        Text = "Colar Deck",
                        Font = new System.Drawing.Font(Font.FontFamily, 9f, System.Drawing.FontStyle.Bold),
                        FlatStyle = System.Windows.Forms.FlatStyle.Flat,
                        BackColor = corFundo2,
                        ForeColor = System.Drawing.Color.White,
                        Cursor = System.Windows.Forms.Cursors.Hand,
                        Size = new System.Drawing.Size(98, 27),
                        Location = new System.Drawing.Point(grpBoxMDecks[i].Location.X + grpBoxMDecks[i].Size.Width + 10, yB2)
                    };
                    btnColaMDecks[i].Click += (s, e) => Colar();
                    btnColaMDecks[i].FlatAppearance.MouseDownBackColor = corFundoClick;
                    tip.SetToolTip(btnColaMDecks[i], "Colar Deck no Gerador");

                    btnSalvaMDecks[i] = new System.Windows.Forms.Button()
                    {
                        Text = "Salvar Deck",
                        Font = new System.Drawing.Font(Font.FontFamily, 9f, System.Drawing.FontStyle.Bold),
                        FlatStyle = System.Windows.Forms.FlatStyle.Flat,
                        BackColor = corFundo2,
                        ForeColor = System.Drawing.Color.White,
                        Cursor = System.Windows.Forms.Cursors.Hand,
                        Size = new System.Drawing.Size(98, 27),
                        Location = new System.Drawing.Point(grpBoxMDecks[i].Location.X + grpBoxMDecks[i].Size.Width + 10, yB3)
                    };
                    btnSalvaMDecks[i].Click += (s, e) => SalvarMD();
                    btnSalvaMDecks[i].FlatAppearance.MouseDownBackColor = corFundoClick;
                    tip.SetToolTip(btnSalvaMDecks[i], "Salvar Deck");

                    pMelhoresDecks.Controls.Add(grpBoxMDecks[i]);
                    pMelhoresDecks.Controls.Add(btnCopiaMDecks[i]);
                    pMelhoresDecks.Controls.Add(btnColaMDecks[i]);
                    pMelhoresDecks.Controls.Add(btnSalvaMDecks[i]);

                    yG += 348; yB1 += 348; yB2 += 348; yB3 += 348;
                }

                System.IO.File.SetAttributes(Classes.ArquivoRegras.pathMDecks, System.IO.FileAttributes.Hidden | System.IO.FileAttributes.ReadOnly);
            }

            void BaixaMelhoresDecks()
            {
                cmsMDecks.Items[0].Enabled = false;
                tsmiMDecks.DropDownItems[0].Enabled = false;
                System.Net.WebClient wc = new System.Net.WebClient();
                wc.DownloadStringAsync(new System.Uri("https://drive.google.com/uc?authuser=0&id=1BUKRrwV02PYql5cPwwJbbNcH0sAZLmFq&export=download"));
                wc.DownloadStringCompleted += (s, e) =>
                {
                    try
                    {
                        if (e.Result.Split('\n').Length > 0)
                        {
                            if (System.IO.File.Exists(Classes.ArquivoRegras.pathMDecks))
                            {
                                System.IO.File.SetAttributes(Classes.ArquivoRegras.pathMDecks, System.IO.FileAttributes.Normal);
                                System.IO.File.Delete(Classes.ArquivoRegras.pathMDecks);
                            }

                            System.IO.StreamWriter sr = new System.IO.StreamWriter(Classes.ArquivoRegras.pathMDecks);
                            sr.Write(e.Result);
                            sr.Close();
                            CriaMelhoresDecks();
                        }
                    }
                    catch (System.Exception ex) { System.Windows.Forms.MessageBox.Show(string.IsNullOrEmpty(ex.InnerException.ToString()) ? "Verifique sua conexão com a internet!" : ex.InnerException.ToString(), string.IsNullOrEmpty(ex.InnerException.Message) ? "Melhores Decks" : ex.InnerException.Message, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error); }
                    finally
                    {
                        cmsMDecks.Items[0].Enabled = true;
                        tsmiMDecks.DropDownItems[0].Enabled = true;
                    }
                };
            }

            void ApagaMelhoresDecks()
            {
                if (System.IO.File.Exists(Classes.ArquivoRegras.pathMDecks) && System.IO.File.ReadAllLines(Classes.ArquivoRegras.pathMDecks).Length > 0)
                {
                    if (System.Windows.Forms.MessageBox.Show("Deseja apagar os Melhores Decks?", "Melhores Decks", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        System.IO.File.SetAttributes(Classes.ArquivoRegras.pathMDecks, System.IO.FileAttributes.Normal);
                        System.IO.File.Delete(Classes.ArquivoRegras.pathMDecks);
                        for (byte i = 0; i < (picImagemMDecks == null ? 0 : picImagemMDecks.Length); i++)
                            for (byte j = 0; j < (picImagemMDecks[i] == null ? 0 : picImagemMDecks[i].Length); j++) picImagemMDecks[i][j].Dispose();
                        for (byte i = 0; i < (grpBoxMDecks == null ? 0 : grpBoxMDecks.Length); i++) grpBoxMDecks[i].Dispose();
                        for (byte i = 0; i < (btnCopiaMDecks == null ? 0 : btnCopiaMDecks.Length); i++) btnCopiaMDecks[i].Dispose();
                        for (byte i = 0; i < (btnColaMDecks == null ? 0 : btnColaMDecks.Length); i++) btnColaMDecks[i].Dispose();
                        for (byte i = 0; i < (btnSalvaMDecks == null ? 0 : btnSalvaMDecks.Length); i++) btnSalvaMDecks[i].Dispose();
                        pMelhoresDecks.Controls.Add(lblAjuda2);
                    }
                }
                else System.Windows.Forms.MessageBox.Show("Não há Melhores Decks para serem apagados.", "Melhores Decks", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            }
            #endregion

            #region Balanceamento
            this.pBalanceamento = new System.Windows.Forms.Panel()
            {
                Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right,
                AutoScroll = true,
                BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle,
                Location = new System.Drawing.Point(167, 29),
                Size = new System.Drawing.Size(766, 441),
            };
            this.pBalanceamento.Click += (s, e) => pBalanceamento.Select();

            tsmiBalanceamento = new System.Windows.Forms.ToolStripMenuItem() { Text = "Menu" };
            tsmiBalanceamento.DropDownItems.Add("Atualizar");
            tsmiBalanceamento.DropDownItems[0].Image = Properties.Resources.atualizar.ToBitmap();
            tsmiBalanceamento.DropDownItems[0].Click += (s, e) => { pBalanceamento.Select(); BaixaBalanceamento(); };
            tsmiBalanceamento.DropDownItems.Add("Apagar Cartas Balanceadas");
            tsmiBalanceamento.DropDownItems[1].Image = Properties.Resources.apagar.ToBitmap();
            tsmiBalanceamento.DropDownItems[1].Click += (s, e) => ApagaBalanceamento();

            menuStripBalanceamento.Items.Add(tsmiBalanceamento);
            pBalanceamento.Controls.Add(menuStripBalanceamento);

            cmsBalanceamento = new System.Windows.Forms.ContextMenuStrip();
            cmsBalanceamento.Items.Add("Atualizar");
            cmsBalanceamento.Items[0].Image = Properties.Resources.atualizar.ToBitmap();
            cmsBalanceamento.Items[0].Click += (s, e) => BaixaBalanceamento();
            cmsBalanceamento.Items.Add("Apagar Cartas Balanceadas");
            cmsBalanceamento.Items[1].Image = Properties.Resources.apagar.ToBitmap();
            cmsBalanceamento.Items[1].Click += (s, e) => ApagaBalanceamento();

            pBalanceamento.ContextMenuStrip = cmsBalanceamento;

            if (System.IO.File.Exists(Classes.ArquivoRegras.pathBalanceamento) && System.IO.File.ReadAllLines(Classes.ArquivoRegras.pathBalanceamento).Length > 0)
                CriaBalanceamento();
            else pBalanceamento.Controls.Add(lblAjuda3);

            void CriaBalanceamento()
            {
                System.IO.File.SetAttributes(Classes.ArquivoRegras.pathBalanceamento, System.IO.FileAttributes.Normal);

                for (byte i = 0; i < (picImagemBalanceamento == null ? 0 : picImagemBalanceamento.Length); i++) picImagemBalanceamento[i].Dispose();
                for (byte i = 0; i < (lblBalanceamento == null ? 0 : lblBalanceamento.Length); i++) lblBalanceamento[i].Dispose();
                for (byte i = 0; i < (grpBoxBalanceamento == null ? 0 : grpBoxBalanceamento.Length); i++) grpBoxBalanceamento[i].Dispose();
                for (byte i = 0; i < (cmsPicBalanceamento == null ? 0 : cmsPicBalanceamento.Length); i++) cmsPicBalanceamento[i].Dispose();

                pBalanceamento.Controls.Remove(lblAjuda3);

                cartasBalanceadas = System.IO.File.ReadAllLines(Classes.ArquivoRegras.pathBalanceamento);

                grpBoxBalanceamento = new System.Windows.Forms.GroupBox[cartasBalanceadas.Length];
                picImagemBalanceamento = new System.Windows.Forms.PictureBox[cartasBalanceadas.Length];
                lblBalanceamento = new System.Windows.Forms.Label[cartasBalanceadas.Length];
                cmsPicBalanceamento = new System.Windows.Forms.ContextMenuStrip[cartasBalanceadas.Length];

                int yG = 30;
                for (byte i = 0; i < cartasBalanceadas.Length; i++)
                {
                    byte copiaI = i;

                    grpBoxBalanceamento[i] = new System.Windows.Forms.GroupBox()
                    {
                        Location = new System.Drawing.Point(12, yG),
                        Size = new System.Drawing.Size(pBalanceamento.Size.Width - 43, 173),
                        BackColor = System.Drawing.Color.Transparent,
                        ForeColor = corLetra,
                        Font = new System.Drawing.Font(Font.FontFamily, 8.25f, System.Drawing.FontStyle.Bold)
                    };
                    grpBoxBalanceamento[i].Click += (s, e) => pBalanceamento.Select();

                    picImagemBalanceamento[i] = new System.Windows.Forms.PictureBox()
                    {
                        Size = new System.Drawing.Size(124, 146),
                        Location = new System.Drawing.Point(8, 18),
                        BackColor = System.Drawing.Color.Transparent,
                        SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage,
                        Cursor = System.Windows.Forms.Cursors.Hand
                    };

                    for (byte j = 0; j < _Deck.CodigoCartas.Length; j++)
                    {
                        byte copiaJ = j;
                        if (cartasBalanceadas[i].Split('|')[0] == _Deck.CodigoCartas[j].ToString())
                        {
                            picImagemBalanceamento[i].Image = _Deck.CartasImagem[j];
                            if (ckNome.Checked) tip.SetToolTip(picImagemBalanceamento[i], _Deck.CartasInformacao[j].Split('\n')[0]);
                            grpBoxBalanceamento[i].Text = _Deck.CartasInformacao[j].Split('\n')[0] + " (" + _Deck.CartasInformacao[j].Split('\n')[1] + " - " + _Deck.CartasInformacao[j].Split('\n')[2] + ")";
                            picImagemBalanceamento[i].Click += (s, e) =>
                            {
                                grpBoxBalanceamento[copiaI].Select();
                                string arena = string.Empty;
                                for (byte k = 0; k < valores.Length; k++)
                                    if (copiaJ < valores[-(k - valores.Length + 1)])
                                    {
                                        arena = string.Format("{0}{1}", arenas[k], k == 0 ? string.Empty : " (" + k + ")");
                                        break;
                                    }
                                lblInformacoes.Text = _Deck.CartasInformacao[copiaJ] + System.Environment.NewLine +
                                "Custo de Elixir: " + _Deck.CustoElixir[copiaJ] + System.Environment.NewLine +
                                "Arena: " + arena;
                            };
                        }
                    }

                    int posInicialXX = picImagemBalanceamento[i].Location.X, posInicialYY = picImagemBalanceamento[i].Location.Y;
                    int tamInicialWW = picImagemBalanceamento[i].Size.Width, tamInicialHH = picImagemBalanceamento[i].Size.Height;

                    picImagemBalanceamento[i].MouseEnter += (s, e) =>
                    {
                        picImagemBalanceamento[copiaI].Location = new System.Drawing.Point(posInicialXX - System.Convert.ToByte(nUpTCarta.Value), posInicialYY - System.Convert.ToByte(nUpTCarta.Value));
                        picImagemBalanceamento[copiaI].Size = new System.Drawing.Size(tamInicialWW + System.Convert.ToByte(nUpTCarta.Value * 2), tamInicialHH + System.Convert.ToByte(nUpTCarta.Value * 2));
                    };
                    picImagemBalanceamento[i].MouseLeave += (s, e) =>
                    {
                        picImagemBalanceamento[copiaI].Location = new System.Drawing.Point(posInicialXX, posInicialYY);
                        picImagemBalanceamento[copiaI].Size = new System.Drawing.Size(tamInicialWW, tamInicialHH);
                    };
                    picImagemBalanceamento[i].MouseDown += (s, e) =>
                    {
                        if (ckEfeitoClick.Checked)
                        {
                            picImagemBalanceamento[copiaI].Location = new System.Drawing.Point(picImagemBalanceamento[copiaI].Location.X + 2 + System.Convert.ToByte(nUpTCarta.Value), picImagemBalanceamento[copiaI].Location.Y + 2 + System.Convert.ToByte(nUpTCarta.Value));
                            picImagemBalanceamento[copiaI].Size = new System.Drawing.Size(picImagemBalanceamento[copiaI].Size.Width - 4 - System.Convert.ToByte(nUpTCarta.Value) * 2, picImagemBalanceamento[copiaI].Size.Height - 4 - System.Convert.ToByte(nUpTCarta.Value) * 2);
                        }
                    };
                    picImagemBalanceamento[i].MouseUp += (s, e) =>
                    {
                        if (ckEfeitoClick.Checked)
                        {
                            picImagemBalanceamento[copiaI].Location = new System.Drawing.Point(picImagemBalanceamento[copiaI].Location.X - 2 - System.Convert.ToByte(nUpTCarta.Value), picImagemBalanceamento[copiaI].Location.Y - 2 - System.Convert.ToByte(nUpTCarta.Value));
                            picImagemBalanceamento[copiaI].Size = new System.Drawing.Size(picImagemBalanceamento[copiaI].Size.Width + 4 + System.Convert.ToByte(nUpTCarta.Value) * 2, picImagemBalanceamento[copiaI].Size.Height + 4 + System.Convert.ToByte(nUpTCarta.Value) * 2);
                        }
                    };

                    cmsPicBalanceamento[i] = new System.Windows.Forms.ContextMenuStrip();
                    cmsPicBalanceamento[i].Items.Add("Descrição da Carta");
                    cmsPicBalanceamento[i].Items[0].Image = Properties.Resources.info.ToBitmap();
                    cmsPicBalanceamento[i].Items[0].Click += (s, e) =>
                    {
                        grpBoxBalanceamento[copiaI].Select();
                        for (byte j = 0; j < _Deck.CodigoCartas.Length; j++)
                            if (cartasBalanceadas[copiaI].Split('|')[0] == _Deck.CodigoCartas[j].ToString())
                                System.Windows.Forms.MessageBox.Show(Classes.CartasDescInfo.Descricao[j], "Descrição - " + _Deck.CartasInformacao[j].Split('\n')[0], System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                    };

                    picImagemBalanceamento[i].ContextMenuStrip = cmsPicBalanceamento[i];

                    lblBalanceamento[i] = new System.Windows.Forms.Label()
                    {
                        Location = new System.Drawing.Point(137, 84),
                        Font = new System.Drawing.Font(Font.FontFamily, 9.5f, System.Drawing.FontStyle.Bold),
                        AutoSize = true
                    };
                    lblBalanceamento[i].Click += (s, e) => grpBoxBalanceamento[copiaI].Select();

                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    byte lAtual = 0;
                    for (byte j = 0; j < cartasBalanceadas[i].Split('|')[1].Length; j++)
                        if (lAtual > 69 + (766 - pBalanceamento.Size.Width == 0 ? 0 : (pBalanceamento.Size.Width - 741) / 5) && cartasBalanceadas[i].Split('|')[1][j] == ' ')
                        {
                            sb.Append(System.Environment.NewLine);
                            lAtual = 0;
                            lblBalanceamento[i].Location = new System.Drawing.Point(lblBalanceamento[i].Location.X, lblBalanceamento[i].Location.Y - 5);
                        }
                        else { sb.Append(cartasBalanceadas[i].Split('|')[1][j]); lAtual++; }
                    lblBalanceamento[i].Text = sb.ToString();

                    grpBoxBalanceamento[i].Controls.Add(picImagemBalanceamento[i]);
                    grpBoxBalanceamento[i].Controls.Add(lblBalanceamento[i]);

                    pBalanceamento.Controls.Add(grpBoxBalanceamento[i]);

                    yG += 178;
                }

                System.IO.File.SetAttributes(Classes.ArquivoRegras.pathBalanceamento, System.IO.FileAttributes.Hidden | System.IO.FileAttributes.ReadOnly);
            }

            void BaixaBalanceamento()
            {
                tsmiBalanceamento.DropDownItems[0].Enabled = false;
                cmsBalanceamento.Items[0].Enabled = false;

                System.Net.WebClient wc = new System.Net.WebClient();
                wc.DownloadStringAsync(new System.Uri("https://drive.google.com/uc?authuser=0&id=1cVd_jutvSjL34giTyNESmIHMWKv_9EUc&export=download"));
                wc.DownloadStringCompleted += (s, e) =>
                {
                    try
                    {
                        if (e.Result.Split('\n').Length > 0)
                        {
                            if (System.IO.File.Exists(Classes.ArquivoRegras.pathBalanceamento))
                                System.IO.File.SetAttributes(Classes.ArquivoRegras.pathBalanceamento, System.IO.FileAttributes.Normal);

                            System.IO.StreamWriter sw = new System.IO.StreamWriter(Classes.ArquivoRegras.pathBalanceamento);
                            sw.Write(e.Result);
                            sw.Close();
                            System.IO.File.SetAttributes(Classes.ArquivoRegras.pathBalanceamento, System.IO.FileAttributes.Hidden | System.IO.FileAttributes.ReadOnly);
                            CriaBalanceamento();
                        }
                    }
                    catch (System.Exception ex)
                    {
                        System.Windows.Forms.MessageBox.Show(ex.InnerException.ToString(), ex.InnerException.Message, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    }
                    finally
                    {
                        tsmiBalanceamento.DropDownItems[0].Enabled = true;
                        cmsBalanceamento.Items[0].Enabled = true;
                    }
                };
            }

            void ApagaBalanceamento()
            {
                if (System.IO.File.Exists(Classes.ArquivoRegras.pathBalanceamento) && System.IO.File.ReadAllLines(Classes.ArquivoRegras.pathBalanceamento).Length > 0)
                {
                    if (System.Windows.Forms.MessageBox.Show("Deseja apagar as Cartas Balanceadas?", "Balanceamento", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        System.IO.File.SetAttributes(Classes.ArquivoRegras.pathBalanceamento, System.IO.FileAttributes.Normal);
                        System.IO.File.Delete(Classes.ArquivoRegras.pathBalanceamento);
                        for (byte i = 0; i < (picImagemBalanceamento == null ? 0 : picImagemBalanceamento.Length); i++) picImagemBalanceamento[i].Dispose();
                        for (byte i = 0; i < (lblBalanceamento == null ? 0 : lblBalanceamento.Length); i++) lblBalanceamento[i].Dispose();
                        for (byte i = 0; i < (grpBoxBalanceamento == null ? 0 : grpBoxBalanceamento.Length); i++) grpBoxBalanceamento[i].Dispose();
                        for (byte i = 0; i < (cmsPicBalanceamento == null ? 0 : cmsPicBalanceamento.Length); i++) cmsPicBalanceamento[i].Dispose();
                        pBalanceamento.Controls.Add(lblAjuda3);
                    }
                }
                else System.Windows.Forms.MessageBox.Show("Não há Cartas Balanceadas para serem apagadas.", "Balanceamento", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            }
            #endregion

            // Outras configurações
            this.rodarDeck = new System.Windows.Forms.Timer(this.components) { Interval = 1 };
            this.rodarDeck.Tick += (s, e) => GerarDeck();

            this.buscarDeck = new System.Windows.Forms.Timer(this.components) { Interval = 1 };
            this.buscarDeck.Tick += (s, e) =>
            {
                if (string.Format("{0:f1}", media) == string.Format("{0:f1}", valorMedia.Value))
                { chkBuscarDeck.Checked = false; CMSGerador.Items[0].Enabled = true; buscarDeck.Stop(); }
                else GerarDeck();
            };

            // Formulário
            this.SuspendLayout();
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(42, 44, 51);
            this.ClientSize = new System.Drawing.Size(933, 470);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = Properties.Resources.icon;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(this.ClientSize.Width, 470);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Controls.Add(this.pBarra);
            this.Controls.Add(this.pOpcoes);
            this.Controls.Add(this.pGerador);
            this.Controls.Add(this.pSelecaoDeCartas);
            this.Controls.Add(this.pDecksSalvos);
            this.Controls.Add(this.pBalanceamento);
            this.Controls.Add(this.pMelhoresDecks);
            this.Controls.Add(this.pAtualizador);
            this.Controls.Add(this.pConfig);
            this.Controls.Add(this.pSobre);

            this.Activated += (s, e) =>
            {
                if (btnRedimensionar.Text == "2" && this.Size != System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Size)
                    this.ClientSize = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Size;
                Opacity = 1;
                System.Windows.Forms.Control[] controles = { lblNome, btnMinimizar, btnRedimensionar, btnFechar, btnGeradorDeck, btnSelecaoDeCartas, btnDecksSalvos,
                btnMelhoresDecks, btnBalanceamento, btnConfig, btnAtualizador, btnSobre};
                for (byte i = 0; i < controles.Length; i++) controles[i].ForeColor = System.Drawing.Color.White;
            };
            this.Deactivate += (s, e) =>
            {
                if (ckDesfoque.Checked) Opacity = 0.93;
                System.Windows.Forms.Control[] controles = { lblNome, btnMinimizar, btnRedimensionar, btnFechar, btnGeradorDeck, btnSelecaoDeCartas, btnDecksSalvos,
                btnMelhoresDecks, btnBalanceamento, btnConfig, btnAtualizador, btnSobre};
                for (byte i = 0; i < controles.Length; i++) controles[i].ForeColor = System.Drawing.Color.LightGray;
            };
            this.Load += (s, e) =>
            {
                pGerador.Select();

                for (byte i = 0; i < grpBoxSCartas.Length; i++)
                    grpBoxSCartas[i].Anchor = System.Windows.Forms.AnchorStyles.Top;
                gbProx.Anchor = System.Windows.Forms.AnchorStyles.Top;

                ClickBotao(true, pGerador, btnGeradorDeck);
                AtualizaArena(); AtualizaRaridade(); AtualizaTipo();
                AtualizaListaCartas();

                if (Properties.Settings.Default.minmmax == 0) rbMin.Checked = true;
                else if (Properties.Settings.Default.minmmax == 1) rbMed.Checked = true;
                else rbMax.Checked = true;

                System.Windows.Forms.ContextMenuStrip cmsNIcon = new System.Windows.Forms.ContextMenuStrip();
                cmsNIcon.Items.Add("Gerador de Deck");
                cmsNIcon.Items[0].Image = Properties.Resources.icon.ToBitmap();
                cmsNIcon.Items[0].Click += (ss, ee) => { SwitchToThisWindow(this.Handle); ClickBotao(true, pGerador, btnGeradorDeck); };
                cmsNIcon.Items.Add("Seleção de Cartas");
                cmsNIcon.Items[1].Image = Properties.Resources.select.ToBitmap();
                cmsNIcon.Items[1].Click += (ss, ee) => { SwitchToThisWindow(this.Handle); ClickBotao(true, pSelecaoDeCartas, btnSelecaoDeCartas); };
                cmsNIcon.Items.Add("Decks salvos");
                cmsNIcon.Items[2].Image = Properties.Resources.salvar.ToBitmap();
                cmsNIcon.Items[2].Click += (ss, ee) => { SwitchToThisWindow(this.Handle); ClickBotao(true, pDecksSalvos, btnDecksSalvos); };
                cmsNIcon.Items.Add("Melhores Decks");
                cmsNIcon.Items[3].Image = Properties.Resources.melhoresdecks;
                cmsNIcon.Items[3].Click += (ss, ee) => { SwitchToThisWindow(this.Handle); ClickBotao(true, pMelhoresDecks, btnMelhoresDecks); };
                cmsNIcon.Items.Add("Balanceamento");
                cmsNIcon.Items[4].Image = Properties.Resources.balancear.ToBitmap();
                cmsNIcon.Items[4].Click += (ss, ee) => { SwitchToThisWindow(this.Handle); ClickBotao(true, pBalanceamento, btnBalanceamento); };
                cmsNIcon.Items.Add("Configurações");
                cmsNIcon.Items[5].Image = Properties.Resources.config.ToBitmap();
                cmsNIcon.Items[5].Click += (ss, ee) => { SwitchToThisWindow(this.Handle); ClickBotao(true, pConfig, btnConfig); };
                cmsNIcon.Items.Add("Atualizador");
                cmsNIcon.Items[6].Image = Properties.Resources.update.ToBitmap();
                cmsNIcon.Items[6].Click += (ss, ee) => { SwitchToThisWindow(this.Handle); ClickBotao(true, pAtualizador, btnAtualizador); };
                cmsNIcon.Items.Add("Sobre");
                cmsNIcon.Items[7].Image = Properties.Resources.info.ToBitmap();
                cmsNIcon.Items[7].Click += (ss, ee) => { SwitchToThisWindow(this.Handle); ClickBotao(true, pSobre, btnSobre); };
                cmsNIcon.Items.Add("-");
                cmsNIcon.Items.Add("Sair");
                cmsNIcon.Items[9].Image = Properties.Resources.apagar.ToBitmap();
                cmsNIcon.Items[9].Click += (ss, ee) =>
                {
                    nIcon.Dispose();
                    System.Environment.Exit(0);
                };

                nIcon = new System.Windows.Forms.NotifyIcon()
                {
                    BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info,
                    BalloonTipTitle = "Gerador de Deck",
                    Icon = Icon,
                    Visible = true,
                    Text = "Gerador de Deck",
                    ContextMenuStrip = cmsNIcon
                };
                nIcon.DoubleClick += (ss, ee) => SwitchToThisWindow(this.Handle);

                SwitchToThisWindow(this.Handle);
            };
            this.ResumeLayout(false);
            #endregion

            // Variáveis de controles e Métodos
            System.Windows.Forms.PictureBox[]
            Icons = { picYouTube, picDica, picBau, picBau2, picYouTube2, picDica2, picGitHub, picAtalho };
            Cartas = new System.Windows.Forms.PictureBox[8] { Carta1, Carta2, Carta3, Carta4, Carta5, Carta6, Carta7, Carta8 };
            System.Windows.Forms.ContextMenuStrip[] CMS = new System.Windows.Forms.ContextMenuStrip[8];
            posInicialGD = new System.Drawing.Point[8];
            tamInicialGD = new System.Drawing.Size[8];
            // Iterações
            for (byte i = 0; i < Icons.Length; i++)
            {
                byte valorI = i;

                Icons[i].MouseEnter += (s, e) =>
                {
                    Icons[valorI].Location = new System.Drawing.Point(Icons[valorI].Location.X - 1, Icons[valorI].Location.Y - 1);
                    Icons[valorI].Size = new System.Drawing.Size(Icons[valorI].Size.Width + 2, Icons[valorI].Size.Height + 2);
                };
                Icons[i].MouseLeave += (s, e) =>
                {
                    Icons[valorI].Location = new System.Drawing.Point(Icons[valorI].Location.X + 1, Icons[valorI].Location.Y + 1);
                    Icons[valorI].Size = new System.Drawing.Size(Icons[valorI].Size.Width - 2, Icons[valorI].Size.Height - 2);
                };
            }
            for (byte i = 0; i < Cartas.Length; i++)
            {
                byte valorI = i;
                if (ckNome.Checked)
                    tip.SetToolTip(Cartas[i], "Carta Inexistente");

                AtualizaTamPos(i);

                Cartas[i].MouseEnter += (s, e) =>
                {
                    Cartas[valorI].Location = new System.Drawing.Point(posInicialGD[valorI].X - System.Convert.ToByte(nUpTCarta.Value), posInicialGD[valorI].Y - System.Convert.ToByte(nUpTCarta.Value));
                    Cartas[valorI].Size = new System.Drawing.Size(tamInicialGD[valorI].Width + System.Convert.ToByte(nUpTCarta.Value) * 2, tamInicialGD[valorI].Height + System.Convert.ToByte(nUpTCarta.Value) * 2);
                };
                Cartas[i].MouseLeave += (s, e) =>
                {
                    Cartas[valorI].Location = posInicialGD[valorI];
                    Cartas[valorI].Size = tamInicialGD[valorI];
                };
                Cartas[i].Click += (s, e) =>
                {
                    pGerador.Select();
                    string infoAdicional = string.Empty;
                    byte v = 0;
                    for (byte j = System.Convert.ToByte(valores.Length - 1); j > 0; j--)
                        if (_Deck.deckAtual[valorI] < valores[j]) { infoAdicional = string.Format("Arena: {0} {1}", arenas[v], v != 0 ? "(" + v + ")" : string.Empty); break; }
                        else v++;
                    string infoAdicional2 = _Deck.CartasInformacao[_Deck.deckAtual[valorI]] == "Nenhuma Carta selecionada" ?
                    string.Empty : System.Environment.NewLine + string.Format("Custo de Elixir: {0}", _Deck.CustoElixir[_Deck.deckAtual[valorI]].ToString().Replace(',', '.'))
                    + System.Environment.NewLine + string.Format("{0}", infoAdicional);
                    lblInformacoes.Text = _Deck.CartasInformacao[_Deck.deckAtual[valorI]] + infoAdicional2;
                };
                Cartas[i].MouseDown += (s, e) =>
                {
                    if (ckEfeitoClick.Checked)
                    {
                        Cartas[valorI].Location = new System.Drawing.Point(Cartas[valorI].Location.X + 2 + System.Convert.ToByte(nUpTCarta.Value), Cartas[valorI].Location.Y + 2 + System.Convert.ToByte(nUpTCarta.Value));
                        Cartas[valorI].Size = new System.Drawing.Size(Cartas[valorI].Size.Width - 4 - System.Convert.ToByte(nUpTCarta.Value) * 2, Cartas[valorI].Size.Height - 4 - System.Convert.ToByte(nUpTCarta.Value) * 2);
                    }
                };
                Cartas[i].MouseUp += (s, e) =>
                {
                    if (ckEfeitoClick.Checked)
                    {
                        Cartas[valorI].Location = new System.Drawing.Point(Cartas[valorI].Location.X - 2 - System.Convert.ToByte(nUpTCarta.Value), Cartas[valorI].Location.Y - 2 - System.Convert.ToByte(nUpTCarta.Value));
                        Cartas[valorI].Size = new System.Drawing.Size(Cartas[valorI].Size.Width + 4 + System.Convert.ToByte(nUpTCarta.Value) * 2, Cartas[valorI].Size.Height + 4 + System.Convert.ToByte(nUpTCarta.Value) * 2);
                    }
                };

                CMS[i] = new System.Windows.Forms.ContextMenuStrip();

                CMS[i].Items.Add("Descrição da Carta");
                CMS[i].Items[0].Image = Properties.Resources.info.ToBitmap();
                CMS[i].Items[0].Click += (s, e) =>
                {
                    pOpcoes.Select();
                    string descricao = Classes.CartasDescInfo.Descricao[_Deck.deckAtual[valorI]];
                    string txt = _Deck.CartasInformacao[_Deck.deckAtual[valorI]].Split('\n')[0];
                    System.Windows.Forms.MessageBox.Show(descricao, "Descrição - " + (txt == "Nenhuma Carta selecionada" ? "Carta Inexistente" : txt), System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                };
                CMS[i].Items.Add("Trocar de Carta");
                CMS[i].Items[1].Image = Properties.Resources.atualizar.ToBitmap();
                CMS[i].Items[1].Click += (s, e) =>
                {
                    var codNome = CaixaDialogo(valorI);
                    for (byte j = 1; j < _Deck.CartasInformacao.Length; j++)
                        if (codNome != string.Empty && System.Array.IndexOf(_Deck.deckAtual, j) == -1 && (codNome == _Deck.CartasInformacao[j].Split('\n')[0] || codNome == _Deck.CodigoCartas[j].ToString()))
                        {
                            Cartas[valorI].Image = _Deck.CartasImagem[j];
                            media -= _Deck.CustoElixir[_Deck.deckAtual[valorI]] / 8;
                            _Deck.deckAtual[valorI] = j;
                            media += _Deck.CustoElixir[_Deck.deckAtual[valorI]] / 8;
                            string txt = _Deck.CartasInformacao[j].Split('\n')[0];
                            if (ckNome.Checked)
                                tip.SetToolTip(Cartas[valorI], txt == "Nenhuma Carta selecionada" ? "Carta Inexistente" : txt);
                            break;
                        }
                    lblMedia.Text = string.Format("Elixir Médio: {0:f1}", media).Replace(',', '.');
                };
                CMS[i].Items.Add("Remover a Carta");
                CMS[i].Items[2].Image = Properties.Resources.apagar.ToBitmap();
                CMS[i].Items[2].Click += (s, e) =>
                {
                    media -= _Deck.CustoElixir[_Deck.deckAtual[valorI]] / 8;
                    Cartas[valorI].Image = _Deck.CartasImagem[0];
                    if (ckNome.Checked) tip.SetToolTip(Cartas[valorI], "Carta Inexistente");
                    _Deck.deckAtual[valorI] = 0;
                    lblMedia.Text = string.Format("Elixir Médio: {0:f1}", media).Replace(',', '.');
                };

                Cartas[i].ContextMenuStrip = CMS[i];
            }
            // Marca d'água
            tList.Add(txtPesquisa);
            sList.Add("Digite o Nome da Carta para Pesquisar (Press. Enter)");
            SetCueText(ref tList, sList);
            // Condições
            if (Properties.Settings.Default.tipCartas) ckNome.Checked = true;
            if (Properties.Settings.Default.tema == 0) { CorClaro(); rbClaro.Checked = true; }
            else if (Properties.Settings.Default.tema == 1) { CorPadrao(); rbPadrao.Checked = true; }
            else { CorEscuro(); rbEscuro.Checked = true; }
        }

        System.Drawing.Point[] posInicialGD;
        System.Drawing.Size[] tamInicialGD;

        private byte bAtual = 37;
        private float media = 0.0f;
        private System.Random _Random = new System.Random();
        private System.Windows.Forms.AutoCompleteStringCollection dados = new System.Windows.Forms.AutoCompleteStringCollection();
        private bool emAtualizacao = false;
        public static Classes.CartasInformacoes _Deck;
        private System.Windows.Forms.PictureBox[] Cartas;
        private byte[] valores;
        private string[] infos, arenas;
        private System.Drawing.Color corLetra, corFundo, corFundo2, corFundoClick;
        System.Windows.Forms.NotifyIcon nIcon;
        System.Collections.ArrayList listaCartas;

        private void AtualizaListaCartas()
        {
            byte qtdCartas = 0;

            for (byte i = 0; i < valores.Length; i++)
                if (cbArena.SelectedIndex == i) qtdCartas = valores[i];

            listaCartas = new System.Collections.ArrayList();
            string[] raridade = { "Comum", "Rara", "Épica", "Lendária" };
            string[] tipo = { "Tropa", "Construção", "Feitiço" };

            for (byte j = 1; j < qtdCartas; j++)
                if (cbPermitir[j - 1].Checked && cbRaridade.SelectedIndex == 0 && cbTipo.SelectedIndex == 0)
                    listaCartas.Add(j);
                else if (cbPermitir[j - 1].Checked && cbRaridade.SelectedIndex == 0 && cbTipo.SelectedIndex != 0 && _Deck.CartasInformacao[j].Split('\n')[2] == "Tipo: " + tipo[cbTipo.SelectedIndex - 1])
                    listaCartas.Add(j);
                else if (cbPermitir[j - 1].Checked && cbTipo.SelectedIndex == 0 && cbRaridade.SelectedIndex != 0 && _Deck.CartasInformacao[j].Split('\n')[1] == string.Format("Raridade: {0}", raridade[cbRaridade.SelectedIndex - 1]))
                    listaCartas.Add(j);
                else if (cbPermitir[j - 1].Checked && cbRaridade.SelectedIndex != 0 && cbTipo.SelectedIndex != 0 && _Deck.CartasInformacao[j].Split('\n')[1] == string.Format("Raridade: {0}", raridade[cbRaridade.SelectedIndex - 1]) &&
                    _Deck.CartasInformacao[j].Split('\n')[2] == string.Format("Tipo: {0}", tipo[cbTipo.SelectedIndex - 1]))
                    listaCartas.Add(j);
        }

        string[] Defesa = { "Gigante", "Golem", "Gigante Real" };
        string[] AtkTorre = { "Corredor", "Ariete de Batalha", "Gigante Real" };
        string[] AP = { "Bárbaros de Elite", "Gigante Real", "P.E.K.K.A" };
        string[] C = { "Lápide", "Canhão" };
        string[] CV3 = { "Cabana de Bárbaros", "Cabana de Goblins", "Fornalha" };
        string[] CA = { "Sparky", "Esqueleto Gigante", "Megacavaleiro" };
        string[] Si = { "Morteiro", "X-Besta" };
        string[] F = { "Flechas", "Bola de Fogo", "Veneno" };
        string[] FV2 = { "Relâmpago", "Foguete" };
        string[] FV3 = { "Zap", "Tronco" };
        string[] FV4 = { "Fúria", "Tornado" };
        string[] Surp = { "Mineiro", "Barril de Goblins" };
        string[] Ciclar = { "Esqueletos", "Goblins", "Goblins Lanceiros", "Espírito de Gelo", "Espíritos de Fogo", "Morcegos", "Golem de Gelo" };
        string[] Sup = { "Bruxa", "Príncipe", "Megacavaleiro", "Mosqueteira", "Três Mosqueteiras", "Bruxa Sombria", "Lançador", "Caçador", "Executor", "Carrinho de Canhão", "Patifes" };
        string[] SV2 = { "Bebê Dragão", "Mini P.E.K.K.A", "Valquíria", "Dragão Infernal", "Mago Elétrico", "Príncipe das Trevas", "Lenhador", "Eletrocutadores", "Máquina Voadora" };
        string[] SV3 = { "Fantasma Real", "Arqueiro Mágico", "Bandida", "Princesa", "Mago de Gelo", "Barril de Esqueletos", "Exército de Esqueletos", "Cavaleiro", "Gangue de Goblins", "Guardas", "Arqueiras" };
        string[] M = { "Servos", "Horda de Servos", "Megasservo" };

        string[] Tipos =
        {
                    "Três Mosqueteiras|Patifes|Mineiro|SV3|SV3|Ciclar|FV3|Coletor de Elixir",
                    "Patifes|P.E.K.K.A|Mineiro|Veneno|Ciclar|FV3|SV3|SV3",
                    "Patifes|Cabana de Goblins|Cemitério|Veneno|Ciclar|M|FV3|SV3",
                    "Patifes|Príncipe|Barril de Goblins|SV3|SV3|F|FV3|Ciclar",
                    "Patifes|Lava Hound|Balão|M|F|FV3|M|C",
                    "Patifes|Mineiro|Barril de Goblins|M|SV3|FV3|SV2|SV3",
                    "Três Mosqueteiras|Corredor|Fantasma Real|Ciclar|SV2|M|FV3|C",
                    "Golem|Príncipe|Arqueiro Mágico|M|SV3|Ciclar|F|FV3",
                    "Gigante|Arqueiro Mágico|Príncipe|M|SV3|F|SV3|FV3",
                    "Arqueiro Mágico|Mineiro|Balão|Ciclar|Ciclar|FV4|FV3|SV3",
                    "Megacavaleiro|Barril de Esqueletos|Ciclar|SV3|SV2|Surp|F|FV3",
                    "Megacavaleiro|Balão|Mineiro|Ciclar|Ciclar|M|M|FV3",
                    "Megacavaleiro|Máquina Voadora|Cemitério|Veneno|Ciclar|Ciclar|FV3|SV2",
                    "Megacavaleiro|Gigante|F|M|SV3|SV2|FV3|Coletor de Elixir",
                    "Mineiro|Caçador|Príncipe|Príncipe das Trevas|Ciclar|Ciclar|F|FV3",
                    "Mineiro|Caçador|Ciclar|Ciclar|Ciclar|FV3|SV3|F",
                    "Si|Caçador|Ciclar|SV3|FV3|Ciclar|M|F",
                    "Golem|Caçador|Bruxa Sombria|Ciclar|FV4|F|FV3|SV3",
                    "Golem|Eletrocutadores|Bruxa Sombria|FV3|F|FV3|M|Ciclar",
                    "Mineiro|F|Caçador|Eletrocutadores|SV3|FV3|Surp|SV2",
                    "Gangue de Goblins|Eletrocutadores|SV3|SV3|FV3|FV2|FV4|SV3",
                    "Três Mosqueteiras|Eletrocutadores|Mago de Gelo|Ciclar|FV4|SV2|FV3|Coletor de Elixir",
                    "D|S|SV2|SV3|Ciclar|Ciclar|FV3|F",
                    "Si|Ciclar|Ciclar|SV3|SV2|FV2|FV3|M",
                    "Ciclar|AP|FV3|F|S|SV3|FV3|Surp",
                    "Ariete de Batalha|Golem de Gelo|SV2|SV3|F|FV3|Ciclar|Surp",
                    "Corredor|Megacavaleiro|Golem de Gelo|SV2|Ciclar|Ciclar|FV3|F",
                    "Torre Inferno|SV3|SV3|Ciclar|SV3|FV2|Surp|FV3",
                    "Megacavaleiro|Ciclar|Surp|SV3|SV2|Ciclar|SV3|FV3",
                    "D|Servos|S|SV2|Ciclar|FV3|F|FV3",
                    "P.E.K.K.A|Príncipe|Príncipe das Trevas|Tornado|Ciclar|FV3|F|SV3",
                    "Sparky|D|SV2|SV3|Ciclar|F|FV3|Surp",
                    "Lava Hound|Balão|M|M|SV3|Lápide|F|FV3",
                    "SV3|SV3|Ciclar|Corredor|Ciclar|C|F|FV3",
                    "SV3|SV3|Ciclar|Ciclar|Si|Flechas|FV2|FV3",
                    "SV3|SV2|Ciclar|SV3|Megacavaleiro|SV3|Cabana de Goblins|FV3",
                    "AT|S|SV3|Ciclar|F|FV3|Ciclar|M",
                    "D|Coletor de Elixir|SV2|SV2|SV3|S|FV3|Tornado",
                    "Corredor|Gelo|S|SV2|SV3|Ciclar|F|FV3",
                    "Balão|FV4|Surp|SV2|SV2|FV3|Ciclar|Ciclar",
                    "FV3|D|M|F|SV2|S|Ciclar|SV3",
                    "C|SV2|SV3|Surp|Ciclar|F|FV3|AP",
                    "D|Sparky|Tornado|SV2|SV3|FV3|Ciclar|FV3",
                    "Golem de Gelo|Cemitério|F|FV3|M|Lápide|SV3|FV3",
                     "Megacavaleiro|Mineiro|SV2|Ciclar|SV3|Ciclar|SV3|FV3",
                    "Fantasma Real|Megacavaleiro|SV3|SV2|SV3|Surp|SV3|FV3",
                    "Três Mosqueteiras|Coletor de Elixir|SV3|Ciclar|S|FV3|Surp|SV2",
                    "Três Mosqueteiras|Coletor de Elixir|SV3|Ciclar|S|FV3|Surp|P.E.K.K.A",
                    "CA|Surp|FV3|FV3|FV2|Ciclar|S|SV3",
                    "Corredor|Dragão Infernal|FV2|FV3|FV4|Ciclar|SV2|Ciclar",
                    "Gigante|F|SV2|Ciclar|S|FV3|C|Ciclar",
                    "Ariete de Batalha|F|FV3|Ciclar|SV2|SV3|SV3|SV2",
                    "Balão|Mineiro|FV3|SV2|F|Ciclar|M|Lápide",
                    "Si|Ciclar|Ciclar|FV3|FV3|M|SV3|Corredor",
                    "Mineiro|Horda de Servos|FV3|F|SV3|SV3|Surp|Torre Inferno",
                    "Cabana de Goblins|Surp|SV3|F|SV2|M|FV3|Cemitério",
                    "Si|FV3|M|Surp|Ciclar|Ciclar|SV3|SV3"
                };

        private void GerarDeck()
        {
            for (byte i = 0; i < _Deck.deckAtual.Length; i++)
                _Deck.deckAnterior[i] = _Deck.deckAtual[i];
            btnVoltarDeck.Enabled = true;
            System.Array.Clear(_Deck.deckAtual, 0, _Deck.deckAtual.Length);

            if (ckGInteligente.Checked)
            {
                string[][] grupos = { Defesa, Sup, AP, FV3, Si, F, FV2, C, SV2, SV3, M, AtkTorre, Surp, Ciclar, CV3, FV4, CA, };
                string[] tropas = { "D", "S", "AP", "FV3", "Si", "F", "FV2", "C", "SV2", "SV3", "M", "AT", "Surp", "Ciclar", "CV3", "FV4", "CA" };

                System.Collections.ArrayList novoDeck = new System.Collections.ArrayList();

                string tipo = Tipos[_Random.Next(0, Tipos.Length)];
                media = 0.0f;
                for (byte i = 0; i < tipo.Split('|').Length; i++)
                {
                    for (byte j = 0; j < tropas.Length; j++)
                        if (tipo.Split('|')[i] == tropas[j])
                        {
                            string tropa = grupos[j][_Random.Next(0, grupos[j].Length)];
                            while (novoDeck.IndexOf(tropa) != -1) tropa = grupos[j][_Random.Next(0, grupos[j].Length)];
                            novoDeck.Add(tropa);
                            break;
                        }
                        else if (j == tropas.Length - 1)
                            novoDeck.Add(tipo.Split('|')[i]);

                    for (byte j = 1; j < _Deck.CartasInformacao.Length; j++)
                        if (novoDeck[i].ToString() == _Deck.CartasInformacao[j].Split('\n')[0])
                        {
                            _Deck.deckAtual[i] = j;
                            Cartas[i].Image = _Deck.CartasImagem[j];
                            if (ckNome.Checked)
                                tip.SetToolTip(Cartas[i], _Deck.CartasInformacao[j].Split('\n')[0]);
                            media += _Deck.CustoElixir[j] / 8;
                            break;
                        }
                }
                EmbaralharDeck();
                lblMedia.Text = string.Format("Elixir Médio: {0:f1}", media).Replace(',', '.');
            }
            else
            {
                try
                {
                    byte tamanho = System.Convert.ToByte(listaCartas.Count < 8 ? listaCartas.Count : _Deck.deckAtual.Length);
                    media = 0.0f;

                    for (byte i = 0; i < tamanho; i++)
                    {
                        byte b;
                        do b = System.Convert.ToByte(listaCartas[_Random.Next(0, listaCartas.Count)]);
                        while (System.Array.IndexOf(_Deck.deckAtual, b) != -1);
                        _Deck.deckAtual[i] = b;
                        if (ckNome.Checked)
                            tip.SetToolTip(Cartas[i], _Deck.CartasInformacao[b].Split('\n')[0]);
                        Cartas[i].Image = _Deck.CartasImagem[b];
                        media += _Deck.CustoElixir[b] / 8;
                    }

                    lblMedia.Text = string.Format("Elixir Médio: {0:f1}", media).Replace(',', '.');

                    for (byte i = tamanho; i < 8; i++)
                    {
                        Cartas[i].Image = Properties.Resources.nova_carta_clash_royale;
                        if (ckNome.Checked)
                            tip.SetToolTip(Cartas[i], "Carta Inexistente");
                    }
                }
                catch { Classes.ArquivoRegras.Criar(); }
            }
        }

        private string CaixaDialogo(byte ind)
        {
            string resultado = string.Empty;

            System.Windows.Forms.Form frmDialog = new System.Windows.Forms.Form()
            {
                FormBorderStyle = System.Windows.Forms.FormBorderStyle.None,
                Size = new System.Drawing.Size(370, 185),
                StartPosition = System.Windows.Forms.FormStartPosition.CenterParent,
                BackColor = corFundo,
                ForeColor = corLetra,
                Icon = Icon,
                Text = "Trocador de Carta",
                ShowInTaskbar = false
            };

            System.Drawing.Point point, point2; int x = 0, y = 0, x2 = 0, y2 = 0; bool click = false;
            void Control_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
            {
                click = true;
                x = MousePosition.X - frmDialog.Location.X; x2 = MousePosition.X - Location.X;
                y = MousePosition.Y - frmDialog.Location.Y; y2 = MousePosition.Y - Location.Y;
            }
            void Control_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
            {
                if (click)
                {
                    point = MousePosition;
                    point.X -= x;
                    point.Y -= y;
                    frmDialog.Location = point;
                    point2 = MousePosition;
                    point2.X -= x2;
                    point2.Y -= y2;
                    if (this.btnRedimensionar.Text != "2" && this.Size != new System.Drawing.Size(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width,
                        System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height))
                        Location = point2;
                }
            }
            void Control_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
            {
                click = false;
            }

            System.Windows.Forms.Panel pBarra = new System.Windows.Forms.Panel()
            {
                Size = new System.Drawing.Size(frmDialog.Size.Width, 30),
                Location = new System.Drawing.Point(0, 0),
                BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            };

            pBarra.MouseDown += new System.Windows.Forms.MouseEventHandler(Control_MouseDown);
            pBarra.MouseMove += new System.Windows.Forms.MouseEventHandler(Control_MouseMove);
            pBarra.MouseUp += new System.Windows.Forms.MouseEventHandler(Control_MouseUp);

            System.Windows.Forms.Panel pFundo = new System.Windows.Forms.Panel()
            {
                BackColor = corFundo,
                Location = new System.Drawing.Point(0, 29),
                Size = new System.Drawing.Size(frmDialog.Size.Width, frmDialog.Size.Height - 29),
                BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            };

            System.Windows.Forms.ToolTip tip = new System.Windows.Forms.ToolTip()
            {
                InitialDelay = 400,
                ReshowDelay = 400,
                AutoPopDelay = 5000
            };

            System.Windows.Forms.Button btnFecharDialogo = new System.Windows.Forms.Button()
            {
                Size = new System.Drawing.Size(30, 28),
                Location = new System.Drawing.Point(frmDialog.Size.Width - 61, 0),
                FlatStyle = System.Windows.Forms.FlatStyle.Flat,
                BackColor = corFundo2,
                ForeColor = System.Drawing.Color.White,
                Font = new System.Drawing.Font("Marlett", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0x72),
                Text = "r",
                Cursor = System.Windows.Forms.Cursors.Hand,
                Dock = System.Windows.Forms.DockStyle.Right,
                TabStop = false
            };
            btnFecharDialogo.Click += (s, e) => frmDialog.Close();
            btnFecharDialogo.FlatAppearance.BorderSize = 0;
            btnFecharDialogo.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkRed;
            btnFecharDialogo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red;

            System.Windows.Forms.PictureBox picIcon = new System.Windows.Forms.PictureBox()
            {
                Image = Properties.Resources.icon.ToBitmap(),
                Size = new System.Drawing.Size(23, 23),
                Location = new System.Drawing.Point(2, 2),
                SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
            };
            picIcon.MouseDown += new System.Windows.Forms.MouseEventHandler(Control_MouseDown);
            picIcon.MouseMove += new System.Windows.Forms.MouseEventHandler(Control_MouseMove);
            picIcon.MouseUp += new System.Windows.Forms.MouseEventHandler(Control_MouseUp);

            System.Windows.Forms.Label lblTitulo = new System.Windows.Forms.Label()
            {
                AutoSize = true,
                ForeColor = System.Drawing.Color.White,
                Text = "Trocador de Carta - " + (ind + 1) + "ª Carta",
                Location = new System.Drawing.Point(62, 3),
                Font = new System.Drawing.Font(Font.FontFamily, 12.75f, System.Drawing.FontStyle.Bold)
            };
            lblTitulo.MouseDown += new System.Windows.Forms.MouseEventHandler(Control_MouseDown);
            lblTitulo.MouseMove += new System.Windows.Forms.MouseEventHandler(Control_MouseMove);
            lblTitulo.MouseUp += new System.Windows.Forms.MouseEventHandler(Control_MouseUp);

            System.Windows.Forms.TextBox txtResposta = new System.Windows.Forms.TextBox()
            {
                Size = new System.Drawing.Size(350, 7),
                Location = new System.Drawing.Point(9, 6),
                Font = new System.Drawing.Font(Font.FontFamily, 10.5f, System.Drawing.FontStyle.Regular),
                TextAlign = System.Windows.Forms.HorizontalAlignment.Center,
                AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append,
                AutoCompleteCustomSource = dados,
                AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource,
                MaxLength = 22,
                TabIndex = 0
            };
            tList.Add(txtResposta); sList.Add("Digite o Nome ou Código da Carta para Trocar");
            SetCueText(ref tList, sList);

            System.Windows.Forms.ComboBox cbCartas = new System.Windows.Forms.ComboBox()
            {
                FlatStyle = System.Windows.Forms.FlatStyle.Flat,
                Size = new System.Drawing.Size(234, 7),
                Location = new System.Drawing.Point(10, 36),
                DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList,
                ForeColor = System.Drawing.Color.White,
                Font = new System.Drawing.Font(Font.FontFamily, 10.5f, System.Drawing.FontStyle.Bold),
                Cursor = System.Windows.Forms.Cursors.Hand,
                TabIndex = 3
            };
            cbCartas.SelectedIndexChanged += (s, e) =>
            {
                if (!txtResposta.Focused)
                    if (cbCartas.SelectedIndex == 0) txtResposta.Clear();
                    else txtResposta.Text = cbCartas.Items[cbCartas.SelectedIndex].ToString();
                txtResposta.Select();
            };

            System.Windows.Forms.Button btnTrocar = new System.Windows.Forms.Button()
            {
                Size = new System.Drawing.Size(95, 30),
                Location = new System.Drawing.Point(264, 79),
                Text = "Trocar",
                Font = new System.Drawing.Font(Font.FontFamily, 10.5f, System.Drawing.FontStyle.Bold),
                FlatStyle = System.Windows.Forms.FlatStyle.Flat,
                BackColor = corFundo2,
                ForeColor = System.Drawing.Color.White,
                Cursor = System.Windows.Forms.Cursors.Hand,
                TabIndex = 1
            };
            btnTrocar.FlatAppearance.MouseDownBackColor = corFundoClick;

            System.Windows.Forms.Button btnCancelar = new System.Windows.Forms.Button()
            {
                Text = "Cancelar",
                Size = new System.Drawing.Size(95, 30),
                Location = new System.Drawing.Point(264, 114),
                Font = new System.Drawing.Font(Font.FontFamily, 10.5f, System.Drawing.FontStyle.Bold),
                FlatStyle = System.Windows.Forms.FlatStyle.Flat,
                BackColor = corFundo2,
                ForeColor = System.Drawing.Color.White,
                Cursor = System.Windows.Forms.Cursors.Hand,
                TabIndex = 2
            };
            btnCancelar.Click += (s, e) => frmDialog.Close();
            btnCancelar.FlatAppearance.MouseDownBackColor = corFundoClick;

            System.Windows.Forms.Label lblResultado = new System.Windows.Forms.Label()
            {
                AutoSize = true,
                Location = new System.Drawing.Point(5, 128),
                Font = new System.Drawing.Font(Font.FontFamily, 9.75f, System.Drawing.FontStyle.Bold)
            };
            lblResultado.Click += (s, e) => txtResposta.Select();

            if (rbClaro.Checked) pBarra.BackColor = btnFecharDialogo.BackColor = cbCartas.BackColor = corFundo2;
            else if (rbPadrao.Checked) { pBarra.BackColor = btnFecharDialogo.BackColor = System.Drawing.Color.FromArgb(35, 35, 35); cbCartas.BackColor = corFundo; }
            else pBarra.BackColor = btnFecharDialogo.BackColor = cbCartas.BackColor = corFundo2;

            frmDialog.Load += (s, e) =>
            {
                txtResposta.Select();
                cbCartas.Items.Add(_Deck.CartasInformacao[0]);

                byte qtdCartas = 0;

                for (byte i = 0; i < valores.Length; i++)
                    if (cbArena.SelectedIndex == i) qtdCartas = valores[i];

                string[] raridade = { "Comum", "Rara", "Épica", "Lendária" };
                string[] tipo = { "Tropa", "Construção", "Feitiço" };

                for (byte j = 1; j < qtdCartas; j++)
                    try
                    {
                        if (cbRaridade.SelectedIndex == 0 && cbTipo.SelectedIndex == 0)
                            cbCartas.Items.Add(_Deck.CartasInformacao[j].Split('\n')[0]);
                        else if (cbRaridade.SelectedIndex == 0 && cbTipo.SelectedIndex != 0 && _Deck.CartasInformacao[j].Split('\n')[2] == "Tipo: " + tipo[cbTipo.SelectedIndex - 1])
                            cbCartas.Items.Add(_Deck.CartasInformacao[j].Split('\n')[0]);
                        else if (cbTipo.SelectedIndex == 0 && cbRaridade.SelectedIndex != 0 && _Deck.CartasInformacao[j].Split('\n')[1] == string.Format("Raridade: {0}", raridade[cbRaridade.SelectedIndex - 1]))
                            cbCartas.Items.Add(_Deck.CartasInformacao[j].Split('\n')[0]);
                        else if (cbRaridade.SelectedIndex != 0 && cbTipo.SelectedIndex != 0 && _Deck.CartasInformacao[j].Split('\n')[1] == string.Format("Raridade: {0}", raridade[cbRaridade.SelectedIndex - 1]) &&
                            _Deck.CartasInformacao[j].Split('\n')[2] == string.Format("Tipo: {0}", tipo[cbTipo.SelectedIndex - 1]))
                            cbCartas.Items.Add(_Deck.CartasInformacao[j].Split('\n')[0]);
                    }
                    catch { Classes.ArquivoRegras.ReCriar(); }

                cbCartas.SelectedIndex = 0;
            };
            frmDialog.Deactivate += (s, e) =>
            {
                if (ckDesfoque.Checked) frmDialog.Opacity = 0.93;
                lblTitulo.ForeColor = btnFecharDialogo.ForeColor = System.Drawing.Color.LightGray;
            };
            frmDialog.Activated += (s, e) =>
            {
                frmDialog.Opacity = 1;
                lblTitulo.ForeColor = btnFecharDialogo.ForeColor = System.Drawing.Color.White;
            };
            pBarra.Click += (s, e) => txtResposta.Select();
            pFundo.Click += (s, e) => txtResposta.Select();
            picIcon.Click += (s, e) => txtResposta.Select();
            lblTitulo.Click += (s, e) => txtResposta.Select();
            btnTrocar.Click += (s, e) => Trocar();
            txtResposta.TextChanged += (s, e) =>
            {
                if (txtResposta.Text.Trim() == string.Empty) cbCartas.SelectedIndex = 0;
                else
                {
                    for (byte i = 1; i < _Deck.CartasInformacao.Length; i++)
                        if (RetirarAcentos(txtResposta.Text.Trim().ToLower()) == RetirarAcentos(_Deck.CartasInformacao[i].Split('\n')[0].ToLower()))
                            for (byte j = 0; j < cbCartas.Items.Count; j++)
                                if (cbCartas.Items[j].ToString() == _Deck.CartasInformacao[i].Split('\n')[0])
                                { cbCartas.SelectedIndex = j; break; }

                    btnTrocar.Text = "Trocar"; tip.SetToolTip(btnTrocar, "Trocar Carta");
                }
            };
            txtResposta.KeyUp += (s, e) => { if (e.KeyCode == System.Windows.Forms.Keys.Enter) Trocar(); };

            void Trocar()
            {
                txtResposta.Select();
                for (byte i = 1; i < _Deck.CartasInformacao.Length; i++)
                {
                    if (RetirarAcentos(txtResposta.Text.Trim().ToLower()) == RetirarAcentos(_Deck.CartasInformacao[i].Split('\n')[0].ToLower()))
                    { resultado = _Deck.CartasInformacao[i].Split('\n')[0]; frmDialog.Close(); }
                    else if (txtResposta.Text.Trim().Equals(_Deck.CodigoCartas[i].ToString()))
                    { resultado = _Deck.CodigoCartas[i].ToString(); frmDialog.Close(); }
                    else if (i == _Deck.CartasInformacao.Length - 1)
                        lblResultado.Text = "Nome/Código de Carta não existe.";
                }
            }
            tip.SetToolTip(btnFecharDialogo, "Fechar");
            tip.SetToolTip(picIcon, "Gerador de Deck - Clash Royale");
            tip.SetToolTip(btnTrocar, "Trocar Carta");
            tip.SetToolTip(btnCancelar, "Cancelar");

            frmDialog.Controls.Add(pBarra);
            frmDialog.Controls.Add(pFundo);
            pBarra.Controls.Add(btnFecharDialogo);
            pBarra.Controls.Add(picIcon);
            pBarra.Controls.Add(lblTitulo);
            pFundo.Controls.Add(txtResposta);
            pFundo.Controls.Add(cbCartas);
            pFundo.Controls.Add(btnTrocar);
            pFundo.Controls.Add(btnCancelar);
            pFundo.Controls.Add(lblResultado);
            frmDialog.ShowDialog();
            return resultado;
        }

        private void CalcularMedia()
        {
            System.Collections.ArrayList listaCartas = new System.Collections.ArrayList();
            byte qtdCartas = 0;

            for (byte i = 0; i < valores.Length; i++)
                if (cbArena.SelectedIndex == i) qtdCartas = valores[i];

            Classes.ArquivoRegras.Criar();
            string[] check = System.IO.File.ReadAllLines(Classes.ArquivoRegras.pathSCartas);
            string[] raridade = { "Comum", "Rara", "Épica", "Lendária" };
            string[] tipo = { "Tropa", "Construção", "Feitiço" };

            for (byte j = 1; j < qtdCartas; j++)
                try
                {
                    if (check[j - 1].Split('|')[1] == "Permitido" && cbRaridade.SelectedIndex == 0 && cbTipo.SelectedIndex == 0)
                        listaCartas.Add(_Deck.CustoElixir[j]);
                    else if (check[j - 1].Split('|')[1] == "Permitido" && cbRaridade.SelectedIndex == 0 && cbTipo.SelectedIndex != 0 && _Deck.CartasInformacao[j].Split('\n')[2] == "Tipo: " + tipo[cbTipo.SelectedIndex - 1])
                        listaCartas.Add(_Deck.CustoElixir[j]);
                    else if (check[j - 1].Split('|')[1] == "Permitido" && cbTipo.SelectedIndex == 0 && cbRaridade.SelectedIndex != 0 && _Deck.CartasInformacao[j].Split('\n')[1] == string.Format("Raridade: {0}", raridade[cbRaridade.SelectedIndex - 1]))
                        listaCartas.Add(_Deck.CustoElixir[j]);
                    else if (check[j - 1].Split('|')[1] == "Permitido" && cbRaridade.SelectedIndex != 0 && cbTipo.SelectedIndex != 0 && _Deck.CartasInformacao[j].Split('\n')[1] == string.Format("Raridade: {0}", raridade[cbRaridade.SelectedIndex - 1]) &&
                        _Deck.CartasInformacao[j].Split('\n')[2] == string.Format("Tipo: {0}", tipo[cbTipo.SelectedIndex - 1]))
                        listaCartas.Add(_Deck.CustoElixir[j]);
                }
                catch { Classes.ArquivoRegras.ReCriar(); }

            double somaMinima = 0.0f;
            double somaMaxima = 0.0f;
            listaCartas.Sort();
            byte tamanho = System.Convert.ToByte(listaCartas.Count < 8 ? listaCartas.Count : 8);
            byte t1 = 0;
            byte listaCount = System.Convert.ToByte(listaCartas.Count);

            while (listaCount < 0) { t1++; listaCount++; }
            byte tamanho2 = System.Convert.ToByte((listaCartas.Count - 8) < 0 ? t1 : listaCartas.Count);

            for (byte i = 0; i < tamanho; i++)
                somaMinima += System.Convert.ToDouble(listaCartas[i]);

            double mediaMinima = 0.0f;
            mediaMinima = somaMinima / 8;
            valorMedia.Minimum = System.Convert.ToDecimal(mediaMinima);

            for (byte i = System.Convert.ToByte(listaCartas.Count); i > listaCartas.Count - tamanho; i--)
                somaMaxima += System.Convert.ToDouble(listaCartas[i - 1]);

            double mediaMaxima = 0.0f;
            mediaMaxima = somaMaxima / 8;
            valorMedia.Maximum = System.Convert.ToDecimal(mediaMaxima);
            if (rbMin.Checked)
                valorMedia.Value = System.Convert.ToDecimal(mediaMinima);
            else if (rbMed.Checked)
                valorMedia.Value = System.Convert.ToDecimal(mediaMinima + mediaMaxima) / 2;
            else if (rbMax.Checked)
                valorMedia.Value = System.Convert.ToDecimal(mediaMaxima);
        }

        private void MudaCor(System.Drawing.Color corLetra, System.Drawing.Color corFundo, System.Drawing.Color corFundo2)
        {
            BackColor = corFundo;
            ForeColor = corLetra;

            System.Windows.Forms.Panel[] _panel = { pGerador, pSelecaoDeCartas, pDecksSalvos, pMelhoresDecks, pBalanceamento, pAtualizador, pSobre };
            for (byte i = 0; i < _panel.Length; i++) _panel[i].ForeColor = corFundo;
            pGerador.ForeColor = pConfig.ForeColor = corLetra;
            pOpcoes.BackColor = corFundo2;

            System.Windows.Forms.GroupBox[] gb = { gbFuncoes, gbCusto, gbTema, gbModo };
            for (byte i = 0; i < gb.Length; i++) gb[i].ForeColor = corLetra;
            for (byte i = 0; i < grpBoxSCartas.Length; i++) grpBoxSCartas[i].ForeColor = corLetra;
            gbProx.ForeColor = corLetra;

            System.Windows.Forms.Button[] btn = { btnGeradorDeck, btnGerar, btnSelecaoDeCartas, btnDecksSalvos, btnConfig, btnSobre,
            btnCopiarDeck, btnSalvarDeck, btnCSalvar, btnVoltarDeck, btnAumentarRaridade, btnAumentarArena, btnDiminuirRaridade, btnDiminuirArena,
            btnSalvar, btnAtualizar, btnPesquisar, btnAtualizador, btnMelhoresDecks, btnBalanceamento, btnDiminuirTipo, btnAumentarTipo};
            for (byte i = 0; i < btn.Length; i++)
            {
                btn[i].BackColor = corFundo2;
                btn[i].FlatAppearance.MouseDownBackColor = corFundoClick;
            }
            btnMinimizar.FlatAppearance.MouseDownBackColor = corFundoClick;
            btnRedimensionar.FlatAppearance.MouseDownBackColor = corFundoClick;
            for (byte i = 0; i < btnInfo.Length; i++) { btnInfo[i].BackColor = corFundo2; btnInfo[i].FlatAppearance.MouseDownBackColor = corFundoClick; }
            for (byte i = 0; i < (btnCopia == null ? 0 : btnCopia.Length); i++) { btnCopia[i].BackColor = corFundo2; btnCopia[i].FlatAppearance.MouseDownBackColor = corFundo2; }
            for (byte i = 0; i < (btnCola == null ? 0 : btnCola.Length); i++) { btnCola[i].BackColor = corFundo2; btnCola[i].FlatAppearance.MouseDownBackColor = corFundo2; }
            for (byte i = 0; i < (btnCopiaMDecks == null ? 0 : btnCopiaMDecks.Length); i++) { btnCopiaMDecks[i].BackColor = corFundo2; btnCopiaMDecks[i].FlatAppearance.MouseDownBackColor = corFundo2; }
            for (byte i = 0; i < (btnColaMDecks == null ? 0 : btnColaMDecks.Length); i++) { btnColaMDecks[i].BackColor = corFundo2; btnColaMDecks[i].FlatAppearance.MouseDownBackColor = corFundo2; }
            for (byte i = 0; i < (btnSalvaMDecks == null ? 0 : btnSalvaMDecks.Length); i++) { btnSalvaMDecks[i].BackColor = corFundo2; btnSalvaMDecks[i].FlatAppearance.MouseDownBackColor = corFundo2; }
            for (byte i = 0; i < (btnApagar == null ? 0 : btnApagar.Length); i++)
                if (btnApagar[i] != null) { btnApagar[i].BackColor = corFundo2; btnApagar[i].FlatAppearance.MouseDownBackColor = corFundo2; }

            for (byte i = 0; i < (grpBoxDSalvos == null ? 0 : grpBoxDSalvos.Length); i++) grpBoxDSalvos[i].ForeColor = corLetra;
            for (byte i = 0; i < (grpBoxMDecks == null ? 0 : grpBoxMDecks.Length); i++) grpBoxMDecks[i].ForeColor = corLetra;
            for (byte i = 0; i < (grpBoxBalanceamento == null ? 0 : grpBoxBalanceamento.Length); i++) grpBoxBalanceamento[i].ForeColor = corLetra;

            valorMedia.BackColor = nUpTCarta.BackColor = corFundo;
            valorMedia.ForeColor = nUpTCarta.ForeColor = corLetra;

            System.Windows.Forms.CheckBox[] ck = { chkBuscarDeck, ckGInteligente, ckNome, ckEfeitoMouse, ckDesfoque, ckVoltarDeck };
            for (byte i = 0; i < ck.Length; i++) ck[i].ForeColor = corLetra;

            rbMin.ForeColor = rbMed.ForeColor = rbMax.ForeColor = corLetra = rbGrafico.ForeColor =
            rbEscuro.ForeColor = rbClaro.ForeColor = rbPadrao.ForeColor = rbMinimalistico.ForeColor = corLetra;

            System.Windows.Forms.Label[] lbl = { lblInformacoes, lblMedia, lbl1, lblAjuda, lblInformacao, lbl13, lblBuild,
            lblUBuild, lblStatus, lblV1, lblV2, lblV25, lblBuild2, lbl2, lbl3, lbl6, lbl7, lbl8, lbl9, lbl10, lbl11,
            lbl12, lbl5, lblTamanho, lblProx, lblAjuda2, lblAjuda3};
            for (byte i = 0; i < lbl.Length; i++) lbl[i].ForeColor = corLetra;
        }

        private void CorClaro()
        {
            corLetra = System.Drawing.Color.FromArgb(7, 42, 70);
            corFundo = System.Drawing.Color.White;
            corFundo2 = corLetra;
            corFundoClick = System.Drawing.Color.FromArgb(4, 39, 67);

            pBarra.BackColor = btnMinimizar.BackColor = corFundo2;
            pSelected.BackColor = corFundo;
            picGIF.Image = Properties.Resources.gifEscuro;
            picGitHub.Image = Properties.Resources.github.ToBitmap();

            System.Windows.Forms.ComboBox[] cbo = { cbArena, cbRaridade, cbTipo, cbSort };
            for (byte i = 0; i < cbo.Length; i++) { cbo[i].BackColor = corLetra; cbo[i].ForeColor = corFundo; }
            System.Windows.Forms.MenuStrip[] ms = { menuStripGerador, menuStripSC, menuStripDS, menuStripMDecks, menuStripBalanceamento, menuStripConfig };
            for (byte i = 0; i < ms.Length; i++) ms[i].BackColor = System.Drawing.SystemColors.Control;

            MudaCor(corLetra, corFundo, corFundo2);
        }

        private void CorPadrao()
        {
            corLetra = System.Drawing.Color.White;
            corFundo = corFundo2 = System.Drawing.Color.FromArgb(42, 44, 51);
            corFundoClick = System.Drawing.Color.FromArgb(37, 39, 45);

            pBarra.BackColor = btnMinimizar.BackColor = System.Drawing.Color.FromArgb(35, 35, 35);
            pSelected.BackColor = System.Drawing.Color.Teal;
            picGIF.Image = Properties.Resources.gifBranco;
            picGitHub.Image = Properties.Resources.github_icon;

            System.Windows.Forms.ComboBox[] cbo = { cbArena, cbRaridade, cbTipo, cbSort };
            for (byte i = 0; i < cbo.Length; i++) { cbo[i].BackColor = corFundo2; cbo[i].ForeColor = corLetra; }
            System.Windows.Forms.MenuStrip[] ms = { menuStripGerador, menuStripSC, menuStripDS, menuStripMDecks, menuStripBalanceamento, menuStripConfig };
            for (byte i = 0; i < ms.Length; i++) ms[i].BackColor = System.Drawing.Color.White;

            MudaCor(corLetra, corFundo, corFundo2);
        }

        private void CorEscuro()
        {
            corLetra = System.Drawing.Color.White;
            corFundo = System.Drawing.Color.FromArgb(42, 44, 51);
            corFundo2 = System.Drawing.Color.FromArgb(35, 35, 35);
            corFundoClick = System.Drawing.Color.FromArgb(32, 32, 32);

            pBarra.BackColor = btnMinimizar.BackColor = corFundo2;
            pSelected.BackColor = System.Drawing.Color.White;
            picGIF.Image = Properties.Resources.gifBranco;
            picGitHub.Image = Properties.Resources.github_icon;

            System.Windows.Forms.ComboBox[] cbo = { cbArena, cbRaridade, cbTipo, cbSort };
            for (byte i = 0; i < cbo.Length; i++) { cbo[i].BackColor = corFundo2; cbo[i].ForeColor = corLetra; }
            System.Windows.Forms.MenuStrip[] ms = { menuStripGerador, menuStripSC, menuStripDS, menuStripMDecks, menuStripBalanceamento, menuStripConfig };
            for (byte i = 0; i < ms.Length; i++) ms[i].BackColor = System.Drawing.Color.White;

            MudaCor(corLetra, corFundo, corFundo2);
        }

        private void BaixarGerador(string url, string tamanho, string versao)
        {
            if (!emAtualizacao)
            {
                btnAtualizar.Enabled = false;
                picGIF.Visible = true;
                lblStatus.Text = "Status: Conectando-se ao Servidor";
                System.Net.WebClient wc = new System.Net.WebClient();
                wc.DownloadFileAsync(new System.Uri(url), string.Format("{0}\\Gerador de Deck V{1}.rar", System.Windows.Forms.Application.StartupPath, versao));
                emAtualizacao = true;
                wc.DownloadProgressChanged += (s, ee) =>
                {
                    try { lblStatus.Text = string.Format("Status: Baixando Gerador de Deck V{2} - {0}KB de {1}KB", ee.BytesReceived / 1024, tamanho, versao); }
                    catch { lblStatus.Text = "Status: Verifique sua conexão com a internet"; wc.Dispose(); }
                };
                wc.DownloadFileCompleted += (s, ee) =>
                {
                    SwitchToThisWindow(this.Handle);
                    ClickBotao(true, pAtualizador, btnAtualizador);
                    lblStatus.Text = "Status: Download concluído. Abra o arquivo na pasta do executável!";
                    picGIF.Visible = false;
                    emAtualizacao = false;
                    btnAtualizar.Enabled = true;
                    wc.Dispose();
                };
            }
            else System.Windows.Forms.MessageBox.Show("Já existe um progresso em andamento. Aguarde!", "Atualizador", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
        }

        private void EmbaralharDeck()
        {
            byte[] deck = new byte[8];
            for (byte i = 0; i < _Deck.deckAtual.Length; i++) deck[i] = _Deck.deckAtual[i];
            System.Array.Clear(_Deck.deckAtual, 0, _Deck.deckAtual.Length);

            System.Collections.ArrayList deckEmbaralhado = new System.Collections.ArrayList();
            for (byte i = 0; i < _Deck.deckAtual.Length; i++)
            {
                byte posicao = System.Convert.ToByte(_Random.Next(0, _Deck.deckAtual.Length));
                while (deckEmbaralhado.IndexOf(posicao) != -1) posicao = System.Convert.ToByte(_Random.Next(0, _Deck.deckAtual.Length));
                deckEmbaralhado.Add(posicao);
            }

            for (byte i = 0; i < deck.Length; i++)
            {
                _Deck.deckAtual[i] = deck[System.Convert.ToByte(deckEmbaralhado[i])];
                Cartas[i].Image = _Deck.CartasImagem[deck[System.Convert.ToByte(deckEmbaralhado[i])]];
                if (ckNome.Checked) tip.SetToolTip(Cartas[i], _Deck.CartasInformacao[deck[System.Convert.ToByte(deckEmbaralhado[i])]].Split('\n')[0]);
            }
        }

        private void CopiarDeck()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("https://link.clashroyale.com/deck/pt?deck=");
            for (byte i = 0; i < _Deck.deckAtual.Length; i++)
                sb.Append(_Deck.CodigoCartas[_Deck.deckAtual[i]] + (i == _Deck.deckAtual.Length - 1 ? string.Empty : ";"));
            System.Windows.Forms.Clipboard.SetText(sb.ToString());
        }

        private void ColarDeck()
        {
            string link = System.Windows.Forms.Clipboard.GetText();
            if (link != string.Empty && link.Contains(";"))
            {
                try
                {
                    if (link.Contains("&")) link = link.Remove(link.IndexOf('&'));
                    if (link.Contains("=")) link = link.Split('=')[1];
                    byte qtd = 0;
                    for (byte i = 0; i < link.Length; i++) if (link[i] == ';') qtd++;
                    if (qtd != 7)
                    {
                        System.Windows.Forms.MessageBox.Show("Link de Deck desconhecido.", "Gerador de Deck", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        return;
                    }
                    string[] cod = link.Split(';');
                    media = 0.0f;
                    for (byte j = 0; j < cod.Length; j++)
                        for (byte i = 0; i < _Deck.CodigoCartas.Length; i++)
                            if (System.Convert.ToInt32(cod[j]) == _Deck.CodigoCartas[i])
                            {
                                _Deck.deckAnterior[j] = _Deck.deckAtual[j];
                                _Deck.deckAtual[j] = i;
                                Cartas[j].Image = _Deck.CartasImagem[i];
                                string txt = _Deck.CartasInformacao[i].Split('\n')[0];
                                if (ckNome.Checked)
                                    tip.SetToolTip(Cartas[j], txt == "Nenhuma Carta selecionada" ? "Carta Inexistente" : txt);
                                media += _Deck.CustoElixir[i] / 8;
                            }
                    btnVoltarDeck.Enabled = true;
                    lblMedia.Text = string.Format("Elixir Médio: {0:f1}", media).Replace(',', '.');
                }
                catch { System.Windows.Forms.MessageBox.Show("Link de Deck desconhecido.", "Gerador de Deck", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error); }
            }
            else System.Windows.Forms.MessageBox.Show("Link de Deck desconhecido.", "Gerador de Deck", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
        }

        private void LimparDeck()
        {
            for (byte i = 0; i < _Deck.deckAnterior.Length; i++)
                _Deck.deckAnterior[i] = _Deck.deckAtual[i];
            btnVoltarDeck.Enabled = true;

            _Deck.deckAtual = new byte[8];
            media = 0.0f;
            for (byte i = 0; i < _Deck.deckAtual.Length; i++)
            {
                Cartas[i].Image = _Deck.CartasImagem[_Deck.deckAtual[i]];
                if (ckNome.Checked)
                    tip.SetToolTip(Cartas[i], "Carta Inexistente");
            }
            lblMedia.Text = string.Format("Elixir Médio: {0:f1}", media).Replace(',', '.');
        }

        private void SalvarDeck()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            bool Verificar(byte[] deck, object value)
            {
                byte soma = 0;
                for (byte i = 0; i < deck.Length; i++)
                    if (deck[i] == System.Convert.ToByte(value))
                        soma++;
                return (soma == 0) ? false : true;
            }

            if (Verificar(_Deck.deckAtual, 0) == true)
            {
                System.Windows.Forms.MessageBox.Show("Não é permitido salvar Decks com Cartas faltando.", "Gerador de Deck", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return;
            }
            else if (System.IO.File.Exists(Classes.ArquivoRegras.pathDSalvos) && System.IO.File.ReadAllLines(Classes.ArquivoRegras.pathDSalvos).Length > 49)
            {
                System.Windows.Forms.MessageBox.Show("Não é permitido salvar mais de 50 Decks. Delete algum para salvar novamente.", "Gerador de Deck", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return;
            }
            else if (System.IO.File.Exists(Classes.ArquivoRegras.pathDSalvos))
            {
                System.IO.File.SetAttributes(Classes.ArquivoRegras.pathDSalvos, System.IO.FileAttributes.Normal);
                string[] deck = System.IO.File.ReadAllLines(Classes.ArquivoRegras.pathDSalvos);
                for (byte i = 0; i < deck.Length; i++)
                {
                    byte soma = 0;
                    for (byte j = 0; j < deck[i].Split(';').Length; j++)
                        for (byte b = 0; b < _Deck.deckAtual.Length; b++)
                            if (_Deck.CodigoCartas[_Deck.deckAtual[j]].ToString() == deck[i].Split(';')[b])
                            { soma++; break; }
                    if (soma == 8)
                    {
                        System.Windows.Forms.MessageBox.Show("Já existe um Deck com as mesmas Cartas salvo.", "Gerador de Deck", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        return;
                    }
                    sb.AppendFormat("{0}{1}", deck[i].Trim(), System.Environment.NewLine);
                }
            }

            System.IO.StreamWriter sw = new System.IO.StreamWriter(Classes.ArquivoRegras.pathDSalvos);
            for (byte i = 0; i < _Deck.deckAtual.Length; i++)
                sb.AppendFormat("{0}{1}", _Deck.CodigoCartas[_Deck.deckAtual[i]], i == _Deck.deckAtual.Length - 1 ? string.Empty : ";");
            sw.Write(sb.ToString());
            sw.Close();
            System.IO.File.SetAttributes(Classes.ArquivoRegras.pathDSalvos, System.IO.FileAttributes.Hidden | System.IO.FileAttributes.ReadOnly);
        }

        private void Mostrar(System.Windows.Forms.Panel _panel)
        {
            if (WindowState == System.Windows.Forms.FormWindowState.Minimized)
                WindowState = System.Windows.Forms.FormWindowState.Normal;
            System.Windows.Forms.Panel[] panels = { pGerador, pSelecaoDeCartas, pAtualizador, pDecksSalvos, pMelhoresDecks, pBalanceamento, pConfig, pSobre };
            for (byte i = 0; i < panels.Length; i++) if (panels[i] != _panel) panels[i].Hide();
            if (_panel.Visible == false) _panel.Show();
            _panel.Select();
        }

        private void AttNome(string nome)
        {
            lblNome.Text = nome.Trim();
            Text = nome.Trim();
            lblNome.Location = new System.Drawing.Point((pBarra.Size.Width - lblNome.Size.Width) / 2, 2);
        }

        private void CSalvar()
        {
            if (ckNome.Checked) Properties.Settings.Default.tipCartas = true;
            else Properties.Settings.Default.tipCartas = false;

            if (ckEfeitoMouse.Checked)
            {
                Properties.Settings.Default.efeitoCartas = true;
                Properties.Settings.Default.efeitoValor = System.Convert.ToByte(nUpTCarta.Value);
            }
            else
            {
                Properties.Settings.Default.efeitoCartas = false;
                Properties.Settings.Default.efeitoValor = 0;
            }

            if (ckEfeitoClick.Checked) Properties.Settings.Default.cliqueCartas = true;
            else Properties.Settings.Default.cliqueCartas = false;

            if (ckDesfoque.Checked) Properties.Settings.Default.desfoque = true;
            else Properties.Settings.Default.desfoque = false;

            if (ckVoltarDeck.Checked) Properties.Settings.Default.voltarDeck = true;
            else Properties.Settings.Default.voltarDeck = false;

            if (rbMin.Checked) Properties.Settings.Default.minmmax = 0;
            else if (rbMed.Checked) Properties.Settings.Default.minmmax = 1;
            else Properties.Settings.Default.minmmax = 2;

            if (rbEscuro.Checked) Properties.Settings.Default.tema = 2;
            else if (rbClaro.Checked) Properties.Settings.Default.tema = 0;
            else Properties.Settings.Default.tema = 1;

            if (rbGrafico.Checked) Properties.Settings.Default.modo = true;
            else Properties.Settings.Default.modo = false;

            Properties.Settings.Default.Save();
            Properties.Settings.Default.Reload();
        }

        private void AtualizaArena()
        {
            if (cbArena.SelectedIndex == 0) { btnAumentarArena.Enabled = false; btnDiminuirArena.Enabled = true; }
            else if (cbArena.SelectedIndex == 12) { btnDiminuirArena.Enabled = false; btnAumentarArena.Enabled = true; }
            else { btnAumentarArena.Enabled = true; btnDiminuirArena.Enabled = true; }

            Properties.Settings.Default.arena = System.Convert.ToByte(cbArena.SelectedIndex);
            Properties.Settings.Default.Save();
        }

        private void AtualizaRaridade()
        {
            if (cbRaridade.SelectedIndex == 0)
                cbRaridade.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            else cbRaridade.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);

            btnDiminuirRaridade.Size = new System.Drawing.Size(btnDiminuirRaridade.Size.Width, cbRaridade.Size.Height);
            btnAumentarRaridade.Size = new System.Drawing.Size(btnAumentarRaridade.Size.Width, cbRaridade.Size.Height);

            if (cbRaridade.SelectedIndex == 0) { btnAumentarRaridade.Enabled = false; btnDiminuirRaridade.Enabled = true; }
            else if (cbRaridade.SelectedIndex == 4) { btnDiminuirRaridade.Enabled = false; btnAumentarRaridade.Enabled = true; }
            else { btnAumentarRaridade.Enabled = true; btnDiminuirRaridade.Enabled = true; }

            Properties.Settings.Default.raridade = System.Convert.ToByte(cbRaridade.SelectedIndex);
            Properties.Settings.Default.Save();
        }

        private void AtualizaTipo()
        {
            if (cbTipo.SelectedIndex == 0)
                cbTipo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            else cbTipo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);

            btnDiminuirTipo.Size = new System.Drawing.Size(btnDiminuirTipo.Size.Width, cbTipo.Size.Height);
            btnAumentarTipo.Size = new System.Drawing.Size(btnAumentarTipo.Size.Width, cbTipo.Size.Height);

            if (cbTipo.SelectedIndex == 0) { btnAumentarTipo.Enabled = false; btnDiminuirTipo.Enabled = true; }
            else if (cbTipo.SelectedIndex == 3) { btnDiminuirTipo.Enabled = false; btnAumentarTipo.Enabled = true; }
            else { btnAumentarTipo.Enabled = true; btnDiminuirTipo.Enabled = true; }

            Properties.Settings.Default.tipo = System.Convert.ToByte(cbTipo.SelectedIndex);
            Properties.Settings.Default.Save();
        }

        private string RetirarAcentos(string frase)
        {
            string sAcento = "aaaaeeeiiioooouuu";
            string cAcento = "áãâàéèêíìîóòõôúùû";
            for (byte i = 0; i < sAcento.Length; i++)
            {
                frase = frase.Replace(cAcento[i], sAcento[i]);
            }
            return frase;
        }

        private void ResetarSalvo()
        {
            if (Properties.Settings.Default.efeitoCartas == true)
            {
                ckEfeitoMouse.Checked = true;
                nUpTCarta.Enabled = true;
                nUpTCarta.Minimum = 1;
                nUpTCarta.Value = Properties.Settings.Default.efeitoValor;
            }
            else
            {
                ckEfeitoMouse.Checked = false;
                nUpTCarta.Minimum = 0;
                nUpTCarta.Value = 0;
                nUpTCarta.Enabled = false;
            }
            if (Properties.Settings.Default.tipCartas) ckNome.Checked = true;
            else ckNome.Checked = false;
            if (Properties.Settings.Default.cliqueCartas) ckEfeitoClick.Checked = true;
            else ckEfeitoClick.Checked = false;
            if (Properties.Settings.Default.desfoque) ckDesfoque.Checked = true;
            else ckDesfoque.Checked = false;
            if (Properties.Settings.Default.voltarDeck) ckVoltarDeck.Checked = true;
            else ckVoltarDeck.Checked = false;
            if (Properties.Settings.Default.minmmax == 0) rbMin.Checked = true;
            else if (Properties.Settings.Default.minmmax == 1) rbMed.Checked = true;
            else rbMax.Checked = true;
            if (Properties.Settings.Default.tema == 0) rbClaro.Checked = true;
            else if (Properties.Settings.Default.tema == 2) rbEscuro.Checked = true;
            else rbPadrao.Checked = true;
            if (Properties.Settings.Default.modo == true) rbGrafico.Checked = true;
            else rbMinimalistico.Checked = true;
        }

        private void ResetarPadrao()
        {
            ckNome.Checked = true;
            ckEfeitoMouse.Checked = true;
            ckEfeitoClick.Checked = true;
            nUpTCarta.Minimum = 1;
            nUpTCarta.Value = 2;
            nUpTCarta.Enabled = true;
            ckDesfoque.Checked = true;
            ckVoltarDeck.Checked = true;
            rbMed.Checked = true;
            rbPadrao.Checked = true;
            rbMinimalistico.Checked = true;
        }

        private void ClickBotao(bool atualiza, System.Windows.Forms.Panel painel, System.Windows.Forms.Button botao)
        {
            Mostrar(painel);
            AttNome(botao.Text);
            if (atualiza)
            {
                pSelected.Height = botao.Height;
                pSelected.Top = botao.Top;
            }
        }

        private void Seleciona(bool atualiza)
        {
            if ((this.Location.Y + btnGeradorDeck.Location.Y) <= MousePosition.Y && (this.Location.Y + btnGeradorDeck.Location.Y + btnGeradorDeck.Size.Height) >= MousePosition.Y)
                ClickBotao(atualiza, pGerador, btnGeradorDeck);
            else if ((this.Location.Y + btnSelecaoDeCartas.Location.Y) <= MousePosition.Y && (this.Location.Y + btnSelecaoDeCartas.Location.Y + btnSelecaoDeCartas.Size.Height) >= MousePosition.Y)
                ClickBotao(atualiza, pSelecaoDeCartas, btnSelecaoDeCartas);
            else if ((this.Location.Y + btnDecksSalvos.Location.Y) <= MousePosition.Y && (this.Location.Y + btnDecksSalvos.Location.Y + btnDecksSalvos.Size.Height) >= MousePosition.Y)
                ClickBotao(atualiza, pDecksSalvos, btnDecksSalvos);
            else if ((this.Location.Y + btnMelhoresDecks.Location.Y) <= MousePosition.Y && (this.Location.Y + btnMelhoresDecks.Location.Y + btnMelhoresDecks.Size.Height) >= MousePosition.Y)
                ClickBotao(atualiza, pMelhoresDecks, btnMelhoresDecks);
            else if ((this.Location.Y + btnBalanceamento.Location.Y) <= MousePosition.Y && (this.Location.Y + btnBalanceamento.Location.Y + btnBalanceamento.Size.Height) >= MousePosition.Y)
                ClickBotao(atualiza, pBalanceamento, btnBalanceamento);
            else if ((this.Location.Y + btnConfig.Location.Y) <= MousePosition.Y && (this.Location.Y + btnConfig.Location.Y + btnConfig.Size.Height) >= MousePosition.Y)
                ClickBotao(atualiza, pConfig, btnConfig);
            else if ((this.Location.Y + btnAtualizador.Location.Y) <= MousePosition.Y && (this.Location.Y + btnAtualizador.Location.Y + btnAtualizador.Size.Height) >= MousePosition.Y)
                ClickBotao(atualiza, pAtualizador, btnAtualizador);
            else if ((this.Location.Y + btnSobre.Location.Y) <= MousePosition.Y && (this.Location.Y + btnSobre.Location.Y + btnSobre.Size.Height) >= MousePosition.Y)
                ClickBotao(atualiza, pSobre, btnSobre);
            else if (pAtualizador.Visible && (this.Location.Y + btnAtualizador.Location.Y + btnAtualizador.Size.Height) <= MousePosition.Y && (this.Location.Y + btnSobre.Location.Y) + 1 >= MousePosition.Y)
                ClickBotao(atualiza, pAtualizador, btnAtualizador);
            else if (pSobre.Visible && (this.Location.Y + btnAtualizador.Location.Y + btnAtualizador.Size.Height) <= MousePosition.Y && (this.Location.Y + btnSobre.Location.Y) >= MousePosition.Y)
                ClickBotao(atualiza, pSobre, btnSobre);
        }

        private void AtualizaTamPos(byte i)
        {
            posInicialGD[i] = Cartas[i].Location;
            tamInicialGD[i] = Cartas[i].Size;
        }

        System.Drawing.Point newPoint;
        int X, Y;

        private void Control_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left && btnRedimensionar.Text != "2" && this.Size != System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Size)
            {
                newPoint = MousePosition;
                newPoint.X -= X;
                newPoint.Y -= Y;
                Location = newPoint;
            }
        }

        private void Control_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            X = MousePosition.X - Location.X;
            Y = MousePosition.Y - Location.Y;
        }

        System.Drawing.Point localInicialForm;
        private void Control_MaximizaRestaura()
        {
            if (cbSort.SelectedIndex != 0)
                cbSort.SelectedIndex = 0;

            for (byte i = 0; i < (decksSalvos != null ? (decksSalvos.Length > 50 ? 50 : decksSalvos.Length) : 0); i++)
            {
                grpBoxDSalvos[i].Anchor = btnCopia[i].Anchor = btnCola[i].Anchor = System.Windows.Forms.AnchorStyles.Top;
                if (btnApagar[i] != null) btnApagar[i].Anchor = System.Windows.Forms.AnchorStyles.Top;
            }

            for (byte i = 0; i < (melhoresDecks != null ? (melhoresDecks.Length > 11 ? 11 : melhoresDecks.Length) : 0); i++)
                grpBoxMDecks[i].Anchor = btnCopiaMDecks[i].Anchor = btnColaMDecks[i].Anchor = btnSalvaMDecks[i].Anchor = System.Windows.Forms.AnchorStyles.Top;

            for (byte i = 0; i < (cartasBalanceadas == null ? 0 : cartasBalanceadas.Length); i++)
                grpBoxBalanceamento[i].Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;

            int valorAtual = pBalanceamento.Size.Width;

            if (btnRedimensionar.Text != "2" && this.Size != System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Size)
            {
                this.btnRedimensionar.Font = new System.Drawing.Font("Marlett", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0x32);
                this.btnRedimensionar.Text = "2";
                localInicialForm = this.Location;
                this.Location = new System.Drawing.Point(0, 0);
                this.Size = new System.Drawing.Size(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width,
                    System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height);
            }
            else
            {
                this.btnRedimensionar.Font = new System.Drawing.Font("Marlett", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0x31);
                this.btnRedimensionar.Text = "1";
                this.Location = localInicialForm;
                this.Size = new System.Drawing.Size(933, 470);
            }
            if (pSobre.Visible) ClickBotao(true, pSobre, btnSobre);

            for (byte i = 0; i < Cartas.Length; i++)
                AtualizaTamPos(i);

            for (byte i = 0; i < grpBoxSCartas.Length; i++)
                localInicialGrpBox[i] = grpBoxSCartas[i].Location;

            int valorAumentado = pBalanceamento.Size.Width - valorAtual;
            for (int i = 0; i < (lblBalanceamento == null ? 0 : lblBalanceamento.Length); i++)
            {
                int qtdMaxLetras = 0;
                lblBalanceamento[i].Location = new System.Drawing.Point(137, 84);
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                for (int k = 0; k < cartasBalanceadas[i].Split('|')[1].Length; k++)
                    if (qtdMaxLetras > 69 + (valorAumentado < 0 ? 0 : valorAumentado / 5) && cartasBalanceadas[i].Split('|')[1][k] == ' ')
                    {
                        qtdMaxLetras = 0;
                        lblBalanceamento[i].Location = new System.Drawing.Point(lblBalanceamento[i].Location.X, lblBalanceamento[i].Location.Y - 5);
                        sb.Append(System.Environment.NewLine);
                    }
                    else { sb.Append(cartasBalanceadas[i].Split('|')[1][k]); qtdMaxLetras++; }

                lblBalanceamento[i].Text = sb.ToString();
            }
        }

        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();

            base.Dispose(disposing);
        }

        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, System.Windows.Forms.Keys keyData)
        {
            switch (keyData)
            {
                case System.Windows.Forms.Keys.E:
                    if (pGerador.Visible) EmbaralharDeck();
                    break;
                case System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C:
                    if (pGerador.Visible) CopiarDeck();
                    break;
                case System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V:
                    if (pGerador.Visible) ColarDeck();
                    break;
                case System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S:
                    if (pGerador.Visible) SalvarDeck();
                    break;
                case System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L:
                    if (pGerador.Visible) LimparDeck();
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        System.Collections.Generic.List<string> sList = new System.Collections.Generic.List<string> { };
        System.Collections.Generic.List<System.Windows.Forms.TextBox> tList = new System.Collections.Generic.List<System.Windows.Forms.TextBox> { };
        const int EM_SETCUEBANNER = 0x1501;
        [System.Runtime.InteropServices.DllImport("User32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto, SetLastError = false)]
        static extern System.IntPtr SendMessage(System.IntPtr hWnd, uint Msg, System.IntPtr w, string l);
        private void SetCueText(ref System.Collections.Generic.List<System.Windows.Forms.TextBox> textbox, System.Collections.Generic.List<string> CueText)
        {
            for (int x = 0; x < textbox.Count; x++)
                SendMessage(textbox[x].Handle, EM_SETCUEBANNER, (System.IntPtr)1, CueText[x]);
        }

        [System.Runtime.InteropServices.DllImport("User32.dll")]
        static extern void SwitchToThisWindow(System.IntPtr hWnd);

        #region Controles
        private System.Windows.Forms.ToolTip tip;
        private System.Windows.Forms.Timer rodarDeck;
        private System.Windows.Forms.Timer buscarDeck;
        // Barra superior
        private System.Windows.Forms.Panel pBarra;
        private System.Windows.Forms.PictureBox picIco;
        private System.Windows.Forms.Label lblNome;
        private System.Windows.Forms.Button btnMinimizar;
        private System.Windows.Forms.Button btnRedimensionar;
        private System.Windows.Forms.Button btnFechar;
        // Barra lateral esquerda
        private System.Windows.Forms.Panel pOpcoes;
        private System.Windows.Forms.Button btnGeradorDeck;
        private System.Windows.Forms.Button btnSelecaoDeCartas;
        private System.Windows.Forms.Button btnDecksSalvos;
        private System.Windows.Forms.Button btnMelhoresDecks;
        private System.Windows.Forms.Button btnBalanceamento;
        private System.Windows.Forms.Button btnAtualizador;
        private System.Windows.Forms.Button btnConfig;
        private System.Windows.Forms.Button btnSobre;
        private System.Windows.Forms.Panel pSelected;
        // Gerador
        private System.Windows.Forms.Panel pGerador;
        private System.Windows.Forms.ContextMenuStrip CMSGerador;
        private System.Windows.Forms.ToolStripMenuItem TSIGerador;
        private System.Windows.Forms.MenuStrip menuStripGerador;
        private System.Windows.Forms.PictureBox picBau;
        private System.Windows.Forms.PictureBox picDica;
        private System.Windows.Forms.PictureBox picYouTube;
        private System.Windows.Forms.Label lblMedia;
        private System.Windows.Forms.Label lbl1;
        private System.Windows.Forms.Label lblInformacoes;
        private System.Windows.Forms.Button btnDiminuirArena;
        private System.Windows.Forms.ComboBox cbArena;
        private System.Windows.Forms.Button btnAumentarArena;
        private System.Windows.Forms.Button btnDiminuirRaridade;
        private System.Windows.Forms.ComboBox cbRaridade;
        private System.Windows.Forms.Button btnAumentarRaridade;
        private System.Windows.Forms.Button btnDiminuirTipo;
        private System.Windows.Forms.ComboBox cbTipo;
        private System.Windows.Forms.Button btnAumentarTipo;
        private System.Windows.Forms.PictureBox Carta1;
        private System.Windows.Forms.PictureBox Carta5;
        private System.Windows.Forms.PictureBox Carta2;
        private System.Windows.Forms.PictureBox Carta6;
        private System.Windows.Forms.PictureBox Carta3;
        private System.Windows.Forms.PictureBox Carta7;
        private System.Windows.Forms.PictureBox Carta4;
        private System.Windows.Forms.PictureBox Carta8;
        private System.Windows.Forms.Button btnCopiarDeck;
        private System.Windows.Forms.Button btnSalvarDeck;
        private System.Windows.Forms.CheckBox chkBuscarDeck;
        private System.Windows.Forms.NumericUpDown valorMedia;
        private System.Windows.Forms.CheckBox ckGInteligente;
        private System.Windows.Forms.Button btnVoltarDeck;
        private System.Windows.Forms.Button btnGerar;
        private System.Windows.Forms.GroupBox gbFuncoes;
        // Seleção de Cartas
        System.Drawing.Point[] localInicialGrpBox;
        private System.Windows.Forms.Panel pSelecaoDeCartas;
        System.Windows.Forms.GroupBox[] grpBoxSCartas;
        System.Windows.Forms.GroupBox gbProx;
        System.Windows.Forms.Label lblProx;
        System.Windows.Forms.PictureBox picProx;
        System.Windows.Forms.CheckBox cbProx;
        System.Windows.Forms.Button btnProx;
        System.Windows.Forms.PictureBox[] picCarta;
        System.Windows.Forms.Label[] lblInfo;
        System.Windows.Forms.CheckBox[] cbPermitir;
        System.Windows.Forms.Button[] btnInfo;
        System.Windows.Forms.Button btnSalvar = new System.Windows.Forms.Button()
        {
            Anchor = System.Windows.Forms.AnchorStyles.Top,
            Text = "&Salvar",
            Cursor = System.Windows.Forms.Cursors.Hand,
            Size = new System.Drawing.Size(90, 30),
            FlatStyle = System.Windows.Forms.FlatStyle.Flat,
            Font = new System.Drawing.Font(DefaultFont.FontFamily, 9.75f, System.Drawing.FontStyle.Bold),
            ForeColor = System.Drawing.Color.White,
            TabIndex = 85
        };
        System.Windows.Forms.ContextMenuStrip cMenu = new System.Windows.Forms.ContextMenuStrip();
        System.Windows.Forms.TextBox txtPesquisa = new System.Windows.Forms.TextBox()
        {
            Anchor = System.Windows.Forms.AnchorStyles.Top,
            Size = new System.Drawing.Size(258, 30),
            Location = new System.Drawing.Point(315, 33),
            TextAlign = System.Windows.Forms.HorizontalAlignment.Center,
            MaxLength = 22,
            TabIndex = 1,
            AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend,
            AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        };
        System.Windows.Forms.Button btnPesquisar = new System.Windows.Forms.Button()
        {
            Anchor = System.Windows.Forms.AnchorStyles.Top,
            Text = "&Pesquisar",
            Size = new System.Drawing.Size(72, 26),
            Location = new System.Drawing.Point(581, 30),
            Cursor = System.Windows.Forms.Cursors.Hand,
            TabIndex = 2,
            FlatStyle = System.Windows.Forms.FlatStyle.Flat,
            Font = new System.Drawing.Font(DefaultFont.FontFamily, 8.25f, System.Drawing.FontStyle.Bold),
            ForeColor = System.Drawing.Color.White
        };
        System.Windows.Forms.ToolStripMenuItem[] tsItems = new System.Windows.Forms.ToolStripMenuItem[10];
        System.Windows.Forms.ComboBox cbSort = new System.Windows.Forms.ComboBox()
        {
            Anchor = System.Windows.Forms.AnchorStyles.Top,
            Location = new System.Drawing.Point(117, 33),
            Size = new System.Drawing.Size(190, 10),
            DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList,
            TabIndex = 0,
            FlatStyle = System.Windows.Forms.FlatStyle.Flat,
            ForeColor = System.Drawing.Color.White,
            Cursor = System.Windows.Forms.Cursors.Hand,
            Font = new System.Drawing.Font(DefaultFont.FontFamily, 8.25f, System.Drawing.FontStyle.Bold)
        };
        System.Windows.Forms.Label lblAjuda = new System.Windows.Forms.Label()
        {
            Anchor = System.Windows.Forms.AnchorStyles.Top,
            Location = new System.Drawing.Point(114, 9),
            Font = new System.Drawing.Font(DefaultFont.FontFamily, 9.75f, System.Drawing.FontStyle.Bold),
            Text = "Depois de escolher suas Cartas, salve clicando com o botão direito do mouse.",
            AutoSize = true
        };
        System.Windows.Forms.MenuStrip menuStripSC = new System.Windows.Forms.MenuStrip() { BackColor = System.Drawing.Color.White, ForeColor = System.Drawing.Color.Black };
        System.Windows.Forms.ToolStripMenuItem TSMI = new System.Windows.Forms.ToolStripMenuItem() { Text = "Menu" };
        // Decks salvos
        private System.Windows.Forms.Panel pDecksSalvos;
        System.Windows.Forms.Label lblInformacao = new System.Windows.Forms.Label()
        {
            Text = "Nenhum Deck foi salvo ainda. Salve-os no Gerador de Deck para administrá-los aqui.",
            Font = new System.Drawing.Font(DefaultFont.FontFamily, 9.75f, System.Drawing.FontStyle.Bold),
            AutoSize = true,
            Location = new System.Drawing.Point(8, 32)
        };
        System.Windows.Forms.ContextMenuStrip conMenu = new System.Windows.Forms.ContextMenuStrip() { Text = "Menu" };
        System.Windows.Forms.ContextMenuStrip[] conOpcoes;
        System.Windows.Forms.GroupBox[] grpBoxDSalvos;
        System.Windows.Forms.Button[] btnCopia;
        System.Windows.Forms.Button[] btnCola;
        System.Windows.Forms.Button[] btnApagar;
        System.Windows.Forms.Label[] lblDeck;
        System.Windows.Forms.PictureBox[][] picImagem;
        System.Windows.Forms.MenuStrip menuStripDS = new System.Windows.Forms.MenuStrip()
        {
            Text = "Menu",
            BackColor = System.Drawing.Color.White,
            ForeColor = System.Drawing.Color.Black,
            Dock = System.Windows.Forms.DockStyle.None,
            Location = new System.Drawing.Point(0, 0)
        };
        System.Windows.Forms.ToolStripMenuItem TSMIDS = new System.Windows.Forms.ToolStripMenuItem() { Text = "Menu" };
        string[] decksSalvos;
        // Melhores Decks
        private System.Windows.Forms.Panel pMelhoresDecks;
        System.Windows.Forms.ToolStripMenuItem tsmiMDecks;
        System.Windows.Forms.ContextMenuStrip cmsMDecks;
        System.Windows.Forms.MenuStrip menuStripMDecks = new System.Windows.Forms.MenuStrip()
        {
            Dock = System.Windows.Forms.DockStyle.None,
            Location = new System.Drawing.Point(0, 0),
            AutoSize = true,
            BackColor = System.Drawing.Color.White,
            ForeColor = System.Drawing.Color.Black,
            Text = "Menu"
        };
        System.Windows.Forms.Label lblAjuda2 = new System.Windows.Forms.Label()
        {
            AutoSize = true,
            Text = "Atualize para ver os Melhores Decks da atualidade.",
            Font = new System.Drawing.Font(DefaultFont.FontFamily, 9.75f, System.Drawing.FontStyle.Bold),
            Location = new System.Drawing.Point(8, 32)
        };
        System.Windows.Forms.GroupBox[] grpBoxMDecks;
        System.Windows.Forms.Button[] btnCopiaMDecks;
        System.Windows.Forms.Button[] btnColaMDecks;
        System.Windows.Forms.Button[] btnSalvaMDecks;
        System.Windows.Forms.PictureBox[][] picImagemMDecks;
        string[] melhoresDecks;
        // Balanceamento
        private System.Windows.Forms.Panel pBalanceamento;
        System.Windows.Forms.MenuStrip menuStripBalanceamento = new System.Windows.Forms.MenuStrip()
        {
            Text = "Menu",
            Dock = System.Windows.Forms.DockStyle.None,
            Location = new System.Drawing.Point(0, 0),
            BackColor = System.Drawing.Color.White,
            ForeColor = System.Drawing.Color.Black
        };
        System.Windows.Forms.ToolStripMenuItem tsmiBalanceamento;
        System.Windows.Forms.ContextMenuStrip cmsBalanceamento;
        System.Windows.Forms.GroupBox[] grpBoxBalanceamento;
        System.Windows.Forms.PictureBox[] picImagemBalanceamento;
        System.Windows.Forms.Label[] lblBalanceamento;
        System.Windows.Forms.ContextMenuStrip[] cmsPicBalanceamento;
        System.Windows.Forms.Label lblAjuda3 = new System.Windows.Forms.Label()
        {
            AutoSize = true,
            Text = "Atualize para ver o último Balanceamento do Clash Royale.",
            Font = new System.Drawing.Font(DefaultFont.FontFamily, 9.75f, System.Drawing.FontStyle.Bold),
            Location = new System.Drawing.Point(8, 32)
        };
        string[] cartasBalanceadas;
        // Configurações
        private System.Windows.Forms.Panel pConfig;
        private System.Windows.Forms.CheckBox ckEfeitoMouse;
        private System.Windows.Forms.Button btnCSalvar;
        private System.Windows.Forms.CheckBox ckNome;
        private System.Windows.Forms.CheckBox ckEfeitoClick;
        private System.Windows.Forms.NumericUpDown nUpTCarta;
        private System.Windows.Forms.Label lblTamanho;
        private System.Windows.Forms.ContextMenuStrip CMSConfig;
        private System.Windows.Forms.CheckBox ckDesfoque;
        private System.Windows.Forms.CheckBox ckVoltarDeck;
        private System.Windows.Forms.GroupBox gbTema;
        private System.Windows.Forms.RadioButton rbEscuro;
        private System.Windows.Forms.RadioButton rbClaro;
        private System.Windows.Forms.GroupBox gbCusto;
        private System.Windows.Forms.RadioButton rbMin;
        private System.Windows.Forms.RadioButton rbMax;
        private System.Windows.Forms.RadioButton rbMed;
        private System.Windows.Forms.RadioButton rbPadrao;
        private System.Windows.Forms.MenuStrip menuStripConfig;
        private System.Windows.Forms.ToolStripMenuItem TSIConfig;
        private System.Windows.Forms.GroupBox gbModo;
        private System.Windows.Forms.RadioButton rbGrafico;
        private System.Windows.Forms.RadioButton rbMinimalistico;
        // Atualizador
        private System.Windows.Forms.Panel pAtualizador;
        private System.Windows.Forms.Button btnAtualizar;
        private System.Windows.Forms.Label lbl13;
        private System.Windows.Forms.Label lblUBuild;
        private System.Windows.Forms.Label lblBuild;
        private System.Windows.Forms.PictureBox picGIF;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblBuild2;
        private System.Windows.Forms.Label lbl12;
        private System.Windows.Forms.Label lblV25;
        private System.Windows.Forms.Label lblV2;
        private System.Windows.Forms.Label lblV1;
        // Sobre
        private System.Windows.Forms.Panel pSobre;
        private System.Windows.Forms.PictureBox picCanal;
        private System.Windows.Forms.PictureBox picBau2;
        private System.Windows.Forms.PictureBox picDica2;
        private System.Windows.Forms.PictureBox picYouTube2;
        private System.Windows.Forms.PictureBox picGitHub;
        private System.Windows.Forms.PictureBox picAtalho;
        private System.Windows.Forms.Label lbl2;
        private System.Windows.Forms.Label lbl3;
        private System.Windows.Forms.Label lbl10;
        private System.Windows.Forms.Label lbl9;
        private System.Windows.Forms.Label lbl8;
        private System.Windows.Forms.Label lbl7;
        private System.Windows.Forms.Label lbl6;
        private System.Windows.Forms.Label lbl5;
        private System.Windows.Forms.Label lbl11;
        #endregion
    }
}
