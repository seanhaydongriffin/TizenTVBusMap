using SQLite;
using SQLitePCL;
using System;
using System.IO;
using Tizen.Wearable.CircularUI.Forms.Renderer;
using Xamarin.Forms;


namespace Sean8
{
    class Program : global::Xamarin.Forms.Platform.Tizen.FormsApplication
    {
        private static string APIKEY = "AIzaSyDxmL_c2vxX1_sPMWqs2QJlodEDJb5G_ZA";
        protected override void OnCreate()
        {
            base.OnCreate();

            LoadApplication(new App());
        }

        static void Main(string[] args)
        {
            //            var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

            //            File.Create("file:///C:\\dwn\\ddd.db");


            //            // get correct path
            //            var dataPath2 = Tizen.Applications.Application.Current;
            //            var dataPath = Tizen.Applications.Application.Current.DirectoryInfo.Data;
            //            var filePath = Path.Combine(dataPath, "my.db");

            //            // create empty file
            //            File.Create(filePath);

            //            FileStream fs = File.Create("c:\\dwn\\hello.db");

            //            string applicationFolderPath = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "CanFindLocation");

            //            // Create the folder path.
            //            System.IO.Directory.CreateDirectory(applicationFolderPath);

            //            string databaseFileName = System.IO.Path.Combine(applicationFolderPath, "CanFindLocation.db");
            //        //    SQLite.SQLite3.ConfigOption(SQLite.SQLite3.ConfigOption.Serialized);
            //            var db = new SQLiteConnection(databaseFileName);

            //raw.SetProvider(new SQLite3Provider_sqlite3());
            //raw.FreezeProvider(true);

            //string connStr = "/opt/media/USBDriveA1/fred2.db"; // @"c:\dwn\hello.db";
            //var cnn = new SQLiteConnection(connStr);

//            //raw.SetProvider(new SQLite3Provider_sqlite3());
//            //raw.FreezeProvider(true);
//        //    SQLiteAsyncConnection db = new SQLiteAsyncConnection("./hello.db");
//       //     SQLiteConnection db = new SQLiteConnection(".\\hello.db");



////                        db.CreateTableAsync<Address>().Wait();
//            db.CreateTable<Address>();

            var app = new Program();
            global::Xamarin.Forms.Forms.Init(app);
            FormsCircularUI.Init(APIKEY);
            app.Run(args);
        }
    }

    public class Address
    {
        public string StreetName { get; set; }
        public string Number { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
    }
}
