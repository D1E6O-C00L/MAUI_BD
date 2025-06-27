using Supabase; 
using ProyectoU2.Entities;

namespace ProyectoU2
{
    public partial class App : Application
    {
        public static Supabase.Client SupabaseClient { get; private set; }
        public App()
        {
            SupabaseClient = new Supabase.Client("https://ydufizvnjmnnnuonbgaj.supabase.co", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InlkdWZpenZuam1ubm51b25iZ2FqIiwicm9sZSI6ImFub24iLCJpYXQiOjE3NTA5NTI5OTksImV4cCI6MjA2NjUyODk5OX0.8GOqEs0MLHomejxMeXeM0yaN8RF2SyV9vsEJI-qUm0Y");
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}