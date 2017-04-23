namespace OpenDrive.Session
{
    public class LoginResponse
    {

        public string SessionID { get; set; }
        public string UserName { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public int AccType { get; set; }// Type of account(1 – personal, 2 – business).
        public string UserLang { get; set; }//: string - The user’s language.
        //English(En)
        //Spanish(Es)
        //Portuguese(Pt)
        //German(De)
        //French(Fr)
        //Simplified Chinese(Zhs)
        //Traditional Chinese(Zht)
        //Czech(Cz)
        //Hungarian(Hu)
        //Dutch(Nl)
        //Polish(Pl)
        //Russian(Ru)
        //Slovak(Sk)
        public int IsAccessUser { get; set; }//: integer – (0 – user, 1 – account user)
        public string DriveName { get; set; }//: string - The Drive name: OpenDrive
        public string UserLevel { get; set; }//: string - 
        public string UserPlan { get; set; }//: string - 
        public string FVersioning { get; set; }//: string - 
        public string UserDomain { get; set; }//: string - 
        public string PartnerUsersDomain { get; set; }//: string
        public string IsPartner { get; set; }//: string - Encoding: string - UTF8

        //Errors: 401: Unauthorized: Invalid username or password.

    }
}
