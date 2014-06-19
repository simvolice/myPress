namespace MyPress.Client.Model
{
    public class DataItem
    {


       


        public DataItem(string user, string pass, string email, string reppass)
        {


            RepPass = reppass;
            User = user;
            Pass = pass;
            Email = email;
        }



        public string RepPass { get; private set; }

        public string User { get; private set; }



        public string Pass { get; private set; }

        public string Email { get; private set; }

    }



}
