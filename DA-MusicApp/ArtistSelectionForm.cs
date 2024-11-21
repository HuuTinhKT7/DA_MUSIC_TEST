using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DA_MusicApp
{
    public partial class ArtistSelectionForm : Form
    {
        public string SelectedArtist { get; private set; }
        public ArtistSelectionForm()
        {
            InitializeComponent();
            btnOK.Enabled = false;
        }
        string[]? artists;
        bool isok=false;
        public ArtistSelectionForm(string[] artists)
        {
            InitializeComponent();
            btnOK.Enabled = false;
            this.artists = artists;
        }


        private void btnOK_Click(object sender, EventArgs e)
        {
            SelectedArtist = listBoxArtists.SelectedItem?.ToString();
            this.DialogResult = DialogResult.OK;
            isok = true;
            this.Close();
        }

        private void ArtistSelectionForm_Shown(object sender, EventArgs e)
        {
            try
            {
                listBoxArtists.Items.AddRange(this.artists);
            }
            catch { }

        }

        private void listBoxArtists_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnOK.Enabled = true;
        }

        private void ArtistSelectionForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(!isok)
            {
                this.DialogResult = DialogResult.Cancel; 
            }
        }
    }
}
