namespace ViewModel
{
    public class HomePageViewModel
    {
        public Entities.Account account { get; set; }
        public List<Entities.Account> accounts { get; set; }
        public List <Entities.Message> listMessages { get; set; }
        public List<ViewModel.UsersViewModel> users { get; set; }
        public List<ViewModel.iiUserViewModel> IIusers { get; set; }
    }
}
