using ProtoBuf;
using SQLite;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Timers;
using Tizen.Wearable.CircularUI.Forms;
using TransitRealtime;
using Xamarin.Forms;

namespace Sean8
{
    public class App : Application
    {
        private SQLiteConnection cnn = null;
        private SQLiteCommand cmd = null;
        private List<routes> route_id = null;
        private List<string[]> bus_next_stop_sequence = null;
        private Marker[] bus_markers = new Marker[20];
        private Marker[] stop_markers = new Marker[50];
//        private string my_route_short_name = "130";
        private string my_route_short_name = "GLKN";
        private int my_stop_id = 10763;
        private int my_stop_sequence_int = -1;
        private static Timer aTimer;
        private GoogleMapView tt;
        private GoogleMapOption option;

        public App()
        {





            tt = new Tizen.Wearable.CircularUI.Forms.GoogleMapView();
            //      var option = new GoogleMapOption();

            tt.WidthRequest = 500;
            tt.HeightRequest = 500;

            var position = new LatLng(-27.641270, 153.049400);
            option = new GoogleMapOption(position);
            option.Zoom = 21;
            option.MapType = GoogleMapType.Hybrid;
            tt.Update(option);






            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;





            raw.SetProvider(new SQLite3Provider_sqlite3());
            raw.FreezeProvider(true);

            string connStr = "/opt/media/USBDriveA1/fred2.db"; // @"c:\dwn\hello.db";
            var cnn = new SQLiteConnection(connStr);

            //    var tt2 = cnn.SelectAllFrom<routes>();
            route_id = cnn.SelectFrom<routes>("SELECT route_id FROM {0} where route_short_name = '" + my_route_short_name + "';");

         //   int ff = tt2.Count;

            cnn.Close();
            cnn.Dispose();


                    UpdateGMap(null, null);

            
                        // Create a timer with a ten second interval.
                        aTimer = new System.Timers.Timer(10000);

                        // Hook up the Elapsed event for the timer.
                        aTimer.Elapsed += new ElapsedEventHandler(UpdateGMap);

                        // Set the Interval to 2 seconds (2000 milliseconds).
                        aTimer.Interval = 2000;
                        aTimer.Enabled = true;
                        

            //System.Windows.Forms.Timer timer = new Timer();
            ////            timer.Interval = (10 * 1000); // 10 secs
            //timer.Interval = (5 * 1000); // 10 secs
            //timer.Tick += new EventHandler(UpdateGMap);
            //timer.Start();




            // The root page of your application
            MainPage = new ContentPage
            {
                Content = new StackLayout
                {
                    VerticalOptions = LayoutOptions.Center,
                    Children = {
                        new Label {
                            HorizontalTextAlignment = TextAlignment.Center,
                            Text = "Welcome to Xamarin Forms!"
                        },
                        tt
                    }
                }
            };











        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }


        private void UpdateGMap(object sender, EventArgs e)
        {
            bool found_position = false;

            try
            {

                WebRequest req = HttpWebRequest.Create("https://gtfsrt.api.translink.com.au/Feed/SEQ");
                FeedMessage feed = Serializer.Deserialize<FeedMessage>(req.GetResponse().GetResponseStream());
                List<object[]> bus_positions = new List<object[]>();

                foreach (FeedEntity entity in feed.Entities)
                {
                    if (found_position)

                        break;

                    if (entity.Vehicle != null)
                    {
                        //          Console.WriteLine("Route ID = " + entity.Vehicle.Trip.RouteId);

                        for (int route_num = 0; route_num < route_id.Count; route_num++)
                        {
                            if (entity.Vehicle.Trip.RouteId.Equals(route_id[route_num].route_id))
                            {


                                //                                bus_next_stop_sequence = SQLite.ToList("SELECT stop_times.stop_sequence FROM stop_times where stop_times.stop_id = '" + entity.Vehicle.StopId + "' and stop_times.trip_id = '13976771-BT 19_20-BOX-Saturday-01';", cnn);
                                //               bus_next_stop_sequence = SQLite.ToList("SELECT my_stop_times.stop_sequence FROM my_stop_times where my_stop_times.stop_id = '" + entity.Vehicle.StopId + "' and my_stop_times.trip_id = '13976771-BT 19_20-BOX-Saturday-01';", cnn);
                                //int bus_next_stop_sequence_int = -1;

                                //if (bus_next_stop_sequence.Count > 0)

                                //    bus_next_stop_sequence_int = Convert.ToInt32(bus_next_stop_sequence[0][0]);

                                //if (bus_next_stop_sequence_int > -1 && bus_next_stop_sequence_int <= my_stop_sequence_int)
                                //{


                                    Console.WriteLine("Trip ID = " + entity.Vehicle.Trip.TripId);
                                    Console.WriteLine("Route ID = " + entity.Vehicle.Trip.RouteId);
                                    Console.WriteLine("Vehicle ID = " + entity.Vehicle.Vehicle.Id);
                                    Console.WriteLine("Vehicle Label = " + entity.Vehicle.Vehicle.Label);
                                    Console.WriteLine("Vehicle License Plate = " + entity.Vehicle.Vehicle.LicensePlate);
                                    Console.WriteLine("Vehicle Stop ID = " + entity.Vehicle.StopId);
                                    Console.WriteLine("Vehicle Timestamp = " + entity.Vehicle.Timestamp);
                                    Console.WriteLine("Vehicle Speed = " + entity.Vehicle.Position.Speed);
                                    Console.WriteLine("Vehicle Bearing = " + entity.Vehicle.Position.Bearing);
                                    Console.WriteLine("Vehicle Odometer = " + entity.Vehicle.Position.Odometer);
                                    Console.WriteLine("Vehicle Latitude = " + entity.Vehicle.Position.Latitude);
                                    Console.WriteLine("Vehicle Longitude = " + entity.Vehicle.Position.Longitude);

                       //             cmd.CommandText = "SELECT Distance(GeomFromText('POINT(153.0503168 -27.6316159)',4326),GeomFromText('POINT(" + entity.Vehicle.Position.Longitude + " " + entity.Vehicle.Position.Latitude + ")',4326), 0) FROM routes;";  //set the passed query
                                                                                                                                                                                                                                                              //             var result = cmd.ExecuteScalar().ToString();
                                                                                                                                                                                                                                                              //            Console.WriteLine("Vehicle Distance = " + result + " metres");
                                                                                                                                                                                                                                                              //            double distance = Convert.ToDouble(result);

                         //           cmd.CommandText = "SELECT stop_name FROM stops WHERE stop_id = '" + entity.Vehicle.StopId + "';";  //set the passed query
                                                                                                                                       //              var stop_name = cmd.ExecuteScalar().ToString();




//                                    object[] bus_data = { (double)entity.Vehicle.Position.Latitude, (double)entity.Vehicle.Position.Longitude, (double)distance, stop_name };
                                    object[] bus_data = { (double)entity.Vehicle.Position.Latitude, (double)entity.Vehicle.Position.Longitude, (double)0.0, "" };
                                    bus_positions.Add(bus_data);
                            //    }


                                //                                marker.Position = new PointLatLng(entity.Vehicle.Position.Latitude, entity.Vehicle.Position.Longitude);
                                //                              gmap.Position = new PointLatLng(entity.Vehicle.Position.Latitude, entity.Vehicle.Position.Longitude);


                                //      found_position = true;
                                //     break;
                            }

                        }
                    }
                }

                Console.WriteLine("Number of " + my_route_short_name + " buses = " + bus_positions.Count);

                //for (int marker_num = 0; marker_num < 20; marker_num++)
                //{
                //    bus_markers[marker_num] = new Marker
                //    {
                //        Position = new LatLng(0, 0)
                //    };

                //    tt.Markers.Add(bus_markers[marker_num]);
                //}

         //       tt.Markers.Clear();

                for (int marker_num = 0; marker_num < 20; marker_num++)
                {
                    bus_markers[marker_num] = new Marker
                    {
                        Position = new LatLng(0, 0)
                    };

//                    tt.Markers.Add(bus_markers[marker_num]);
                }




                for (int bus_num = 0; bus_num < bus_positions.Count && bus_num < 20; bus_num++)
                {
                    bus_markers[bus_num].Position = new LatLng((double)bus_positions[bus_num][0], (double)bus_positions[bus_num][1]);
                    bus_markers[bus_num].Description = (string)bus_positions[bus_num][3];
                    option.Center = bus_markers[0].Position;
                    tt.Update(option);
                    tt.Markers.Add(bus_markers[bus_num]);
                }



                int i = 0;
            }
            catch (Exception e2)
            {

            }

        }


    }

    public class routes
    {
        public string route_id { get; set; }
        public string route_short_name { get; set; }
        public string route_long_name { get; set; }
        public string route_desc { get; set; }
        public string route_type { get; set; }
        public string route_url { get; set; }
        public string route_color { get; set; }
        public string route_text_color { get; set; }
    }
}
