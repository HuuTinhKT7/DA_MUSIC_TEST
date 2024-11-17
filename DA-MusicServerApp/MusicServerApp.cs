namespace DA_MusicServerApp
{
    public partial class MusicServerApp : Form
    {
        public MusicServerApp()
        {
            InitializeComponent();
        }
        private static PasswordHasher passwordHasher = new PasswordHasher();
    }
}
