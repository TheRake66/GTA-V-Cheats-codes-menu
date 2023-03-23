using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Policy;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace GTA_V_Cheats_codes_menu
{
    public partial class Menu : Form
    {
        private Color blackColor = Color.FromArgb(10, 10, 10);
        private Color whiteColor = Color.FromArgb(249, 249, 249);
        private List<Button> loadedCheats = new List<Button>();
        private Button? currentMenu;

        public Menu()
        {
            InitializeComponent();
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            this.moveForm();
            this.initTab();
        }

        private void Menu_Shown(object sender, EventArgs e)
        {
            this.Hide();
            this.Opacity = 0.85;
        }

        private void tutorielToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Bienvenue dans le menu des codes de triches de GTA V !" +
                Environment.NewLine +
                Environment.NewLine +
                "Tout d'abord, lancez le jeu en mode histoire." +
                Environment.NewLine +
                Environment.NewLine +
                "Puis, allez dans les paramètres, dans l'onglet « graphisme », et changez le mode d'affichage par « Fenêtre sans bordures »." +
                Environment.NewLine +
                Environment.NewLine +
                "Après avoir enregistré les paramètres, ouvrez le menu des codes de triches en appuyant sur la touche « ~ »." +
                Environment.NewLine +
                Environment.NewLine +
                "Il ne vous reste plus qu'à choisir le code que vous voulez utiliser." +
                Environment.NewLine +
                Environment.NewLine +
                "Bon jeu !", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void depotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string url = "https://github.com/TheRake66/GTA-V-Cheats-codes-menu";
                ProcessStartInfo info = new ProcessStartInfo(url)
                {
                    UseShellExecute = true
                };
                Process.Start(info);
            }
            catch
            {
            }
        }

        private void quitterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void timerKeyboard_Tick(object sender, EventArgs e)
        {
            this.toogleMenu();
        }

        private void moveForm()
        {
            Screen primary = Screen.PrimaryScreen;

            int width = primary.WorkingArea.Width;
            int height = primary.WorkingArea.Height;
            this.Location = new Point(0, 0);
            this.Size = new Size(width, height);
        }

        private void initTab()
        {
            string[][] armes = new string[5][]
            {
                new string[2] { "Obtenir des armes", "TOOLUP" },
                new string[2] { "Coups de poing explosifs", "HOTHANDS" },
                new string[2] { "Balles qui explosent", "HIGHEX" },
                new string[2] { "Visée au ralenti", "DEADEYE" },
                new string[2] { "Balles enflammées", "INCENDIARY" }
            };

            string[][] police = new string[2][]
            {
                new string[2] { "Diminuer l'indice de recherche", "LAWYERUP" },
                new string[2] { "Augmenter l'indice de recherche", "FUGITIVE" }
            };

            string[][] vehicules = new string[15][]
            {
                new string[2] { "Faire déraper les voitures", "SNOWDAY" },
                new string[2] { "Stunt Plane (avion de voltige)", "BARNSTORM" },
                new string[2] { "Duster (avion agricole)", "FLYSPRAY" },
                new string[2] { "Buzzard (hélicoptère)", "BUZZOFF" },
                new string[2] { "Comet (voiture sportive)", "COMET" },
                new string[2] { "Rapid GT (voiture de course)", "RAPIDGT" },
                new string[2] { "Sanchez (motocross)", "OFFROAD" },
                new string[2] { "Trashmaster (camion poubelle)", "TRASHED" },
                new string[2] { "Stretch (limousine)", "VINEWOOD" },
                new string[2] { "Caddie de golf", "HOLEIN1" },
                new string[2] { "PCJ 600 (moto de course)", "ROCKET" },
                new string[2] { "BMX", "BANDIT" },
                new string[2] { "Duke O'Death", "DEATHCAR" },
                new string[2] { "Hydravion Dodo", "EXTINCT" },
                new string[2] { "Submersible Kraken", "BUBBLES" }
            };

            string[][] meteo = new string[1][]
            {
                new string[2] { "Changer le temps", "MAKEITRAIN" }
            };

            string[][] personnages = new string[5][]
            {
                new string[2] { "Recharger la santé et l'armure", "TURTLE" },
                new string[2] { "Invincibilité pendant 5 minutes (n'affecte pas les véhicules)", "PAINKILLER" },
                new string[2] { "Recharger la compétence unique", "POWERUP" },
                new string[2] { "Courir plus vite", "CATCHME" },
                new string[2] { "Nager plus vite", "GOTGILLS" }
            };

            string[][] divers = new string[7][]
            {
                new string[2] { "Mode ivrogne", "LIQUOR" },
                new string[2] { "Ralentir le jeu", "SLOWMO" },
                new string[2] { "Obtenir un parachute", "SKYDIVE" },
                new string[2] { "Skyfall (chute libre depuis le ciel, veillez à bien avoir un parachute avant)", "SKYFALL" },
                new string[2] { "Gravité lunaire", "FLOATER" },
                new string[2] { "Super saut", "HOPTOIT" },
                new string[2] { "Mode Réalisateur", "LSTALENT" }
            };

            Dictionary<Button, string[][]> tabs = new Dictionary<Button, string[][]>
            {
                { this.buttonArmes, armes },
                { this.buttonPolice, police },
                { this.buttonVehicules, vehicules },
                { this.buttonMeteo, meteo },
                { this.buttonPersonnages, personnages },
                { this.buttonDivers, divers }
            };

            foreach (KeyValuePair<Button, string[][]> tab in tabs)
            {
                Button menu = tab.Key;
                string[][] buttons = tab.Value;
                menu.Click += (e, s) => this.changeMenu(menu, buttons);
            }

        }

        private void toogleMenu()
        {
            IntPtr handle = this.getHandle();
            if (this.isRunning(handle))
            {
                User32.VirtualKeys tilde = User32.VirtualKeys.VK_OEM_3;
                if (this.isPressed(tilde))
                {
                    this.Show();
                    User32.SetForegroundWindow(this.Handle);
                }

                User32.VirtualKeys escape = User32.VirtualKeys.VK_ESCAPE;
                if (this.isPressed(escape))
                {
                    this.Hide();
                    User32.SetForegroundWindow(handle);
                }
            }
        }

        private void changeMenu(Button menu, string[][] buttons)
        {
            Panel selected = this.panelSelected;
            Button current = this.currentMenu;

            if (current != null)
            {
                current.BackColor = this.blackColor;
                current.ForeColor = this.whiteColor;

                ControlCollection controls = (ControlCollection)this.Controls;
                List<Button> list = this.loadedCheats;
                foreach (Button cheat in list)
                {
                    controls.Remove(cheat);
                }
            }
            else
            {
                selected.Visible = true;
            }

            if (current != menu)
            {
                menu.BackColor = this.whiteColor;
                menu.ForeColor = this.blackColor;

                int x = menu.Location.X;
                int y = selected.Location.Y;
                selected.Location = new Point(x, y);

                this.loadButtons(buttons);

                this.currentMenu = menu;
            }
            else
            {
                selected.Visible = false;

                this.currentMenu = null;
            }
        }

        private void loadButtons(string[][] buttons)
        {
            ControlCollection controls = (ControlCollection)this.Controls;
            List<Button> list = this.loadedCheats;
            int x = 16;
            int y = 62;
            int width = 800;
            int height = 30;
            int margin = 4;

            foreach (string[] button in buttons)
            {
                Button cheat = new Button();
                cheat.TextAlign = ContentAlignment.MiddleLeft;
                cheat.BackColor = this.blackColor;
                cheat.ForeColor = this.whiteColor;
                cheat.FlatStyle = FlatStyle.Flat;
                cheat.FlatAppearance.BorderSize = 0;
                cheat.UseVisualStyleBackColor = false;
                cheat.Text = button[0];
                cheat.Tag = button[1];
                cheat.Location = new Point(x, y);
                cheat.Size = new Size(width, height);
                cheat.Click += (e, s) =>
                {
                    string code = cheat.Tag.ToString();
                    this.sendCode(code);
                };

                controls.Add(cheat);
                list.Add(cheat);

                y += height + margin;
            }
        }

        private void sendCode(string code)
        {
            IntPtr handle = this.getHandle();
            if (this.isRunning(handle))
            {
                this.Hide();
                User32.SetForegroundWindow(handle);
                Thread.Sleep(1000);
                SendKeys.SendWait(code);
            }
        }

        private IntPtr getHandle()
        {
            IntPtr handle = User32.FindWindow(null, "Grand Theft Auto V");
            return handle;
        }

        private bool isRunning(IntPtr handle = default(IntPtr))
        {
            if (handle == default(IntPtr))
            {
                handle = this.getHandle();
            }
            return handle != IntPtr.Zero;
        }

        private bool isPressed(User32.VirtualKeys key)
        {
            return User32.GetAsyncKeyState((int)key) != 0;
        }
    }
}