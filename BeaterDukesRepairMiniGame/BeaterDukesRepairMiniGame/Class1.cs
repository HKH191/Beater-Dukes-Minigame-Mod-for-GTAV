using GTA;
using GTA.Math;
using GTA.Native;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace BeaterDukesRepairMiniGame
{
    public class DukesSpawn : Script
        {
        public float Heading { get; set; }
        public Vector3 Position { get; set; }
        public DukesSpawn()
        {

        }
        public DukesSpawn(Vector3 P, float H)
        {
            Position = P;
            Heading = H;
        }
    }
    public class Class1 : Script
    {
        public Model RequestModel(string Name)
        {

            var model = new Model(Name);
            model.Request(10000);

            // Check the model is valid
            if (model.IsInCdImage && model.IsValid)
            {
                // Ensure the model is loaded before we try to create it in the world
                while (!model.IsLoaded) Script.Wait(50);
                return model;




            }

            // Mark the model as no longer needed to remove it from memory.

            model.MarkAsNoLongerNeeded();
            return model;
        }
        public Model RequestModel(PedHash Name)
        {

            var model = new Model(Name);
            model.Request(10000);

            // Check the model is valid
            if (model.IsInCdImage && model.IsValid)
            {
                // Ensure the model is loaded before we try to create it in the world
                while (!model.IsLoaded) Script.Wait(50);
                return model;




            }

            // Mark the model as no longer needed to remove it from memory.

            model.MarkAsNoLongerNeeded();
            return model;
        }
        public Model RequestModel(int Name)
        {

            var model = new Model(Name);
            model.Request(10000);

            // Check the model is valid
            if (model.IsInCdImage && model.IsValid)
            {
                // Ensure the model is loaded before we try to create it in the world
                while (!model.IsLoaded) Script.Wait(50);
                return model;




            }

            // Mark the model as no longer needed to remove it from memory.

            model.MarkAsNoLongerNeeded();
            return model;
        }
        public Model RequestModel(VehicleHash Name)
        {

            var model = new Model(Name);
            model.Request(10000);

            // Check the model is valid
            if (model.IsInCdImage && model.IsValid)
            {
                // Ensure the model is loaded before we try to create it in the world
                while (!model.IsLoaded) Script.Wait(50);
                return model;




            }

            // Mark the model as no longer needed to remove it from memory.

            model.MarkAsNoLongerNeeded();
            return model;
        }
        public static string LoadDict(string dict)
        {
            while (!Function.Call<bool>(Hash.HAS_ANIM_DICT_LOADED, dict))
            {
                Function.Call(Hash.REQUEST_ANIM_DICT, dict);
                Script.Yield();
            }

            return dict;
        }
        public DukesSpawn D = new DukesSpawn();
        public List<DukesSpawn> Spawns = new List<DukesSpawn>();
        public List<DukesSpawn> SpawnsOriginal = new List<DukesSpawn>();
        public List<bool> GotVehicle = new List<bool>();
        public List<float> dist = new List<float>();
        public Vehicle Duke;
        public new List<Blip> ChangePointsBlip = new List<Blip>();
        ScriptSettings Config;
        public bool SpawnCar;
        public bool firsttime;
        public bool ShowBlip= true;
        public Blip LocationBlip;
        public Vector3 CurrentPos;
        public Vector3 LastPos;
        public bool Presetvariable;
        public bool ShowDisttoPart;
        public bool ShowUINearPart;
        public bool ShowAllLocationsOnMap;
        public void SetActive(Vector3 V)
        {
            bool Found = false;
            if (SpawnsOriginal.Count >= 50)
            {
                for (int i = 0; i < 50; i++)
                {
                    if (V == SpawnsOriginal[i].Position)
                    {
                        GotVehicle[i] = true;
                      
                    }

                }
            }
          
        }
        public bool CheckifActive(Vector3 V)
        {
            bool Found = false;
            if (SpawnsOriginal.Count >= 50)
            {
                for (int i = 0; i < 50; i++)
                {
                   if(V== SpawnsOriginal[i].Position)
                    {
                        if(GotVehicle[i]==false)
                        {
                            Found = false;

                            return Found;
                        }
                        if (GotVehicle[i] == true)
                        {
                            Found = true;
                            return Found;
                        }
                    }
                   
                }
            }
            return Found;
        }
        public int ReturnCartoChange(Vector3 V)
        {
            int Found = 0;
            if (SpawnsOriginal.Count >= 50)
            {
                for (int i = 0; i < 50; i++)
                {
                    if (V == SpawnsOriginal[i].Position)
                    {
                       
                       
                        if (GotVehicle[i] == true)
                        {
                            Found = i;
                            return Found;
                        }
                    }

                }
            }
            return Found;
        }
        public Class1()
        {
            LoadIniFile("scripts//BeaterDukesMiniGame.ini");
            Tick += OnTick;
            Aborted += OnShutdown;

        }
        public static void TextNotification(string avatar, string author, string title, string message)
        {
            Function.Call(Hash.PLAY_SOUND_FRONTEND, -1, "CONFIRM_BEEP", "HUD_MINI_GAME_SOUNDSET");
            Function.Call(Hash._SET_NOTIFICATION_TEXT_ENTRY, new InputArgument[]
            {
            "STRING"
            });
            Function.Call(Hash._ADD_TEXT_COMPONENT_STRING, new InputArgument[]
            {
            message
            });
            int CurrentNotification = Function.Call<int>(Hash._SET_NOTIFICATION_MESSAGE, new InputArgument[]
            {
            avatar,
            avatar,
            true,
            0,
            title,
            author
            });
        }
        public bool CanSpawnCar;
        public bool SpawnedCar;
        public bool HasSpawnedcar = false;
        public void OnTick(object sender, EventArgs e)
        {

            if(Duke!=null)
            {
              
            }
            if (SpawnsOriginal.Count >= 50)
            {

                for (int i = 0; i < 50; i++)
                {
                    Vector3 S = SpawnsOriginal[i].Position;
                    if (S != null)
                    {
                        if (CheckifActive(SpawnsOriginal[i].Position) == true)
                        {
                            CanSpawnCar = true;
                        }
                        if (CheckifActive(SpawnsOriginal[i].Position) == false)
                        {
                            CanSpawnCar = false;
                            break;
                        }
                    }
                }
            }
            if (LocationBlip != null)
            {
              
                if (World.GetDistance(Game.Player.Character.Position, LocationBlip.Position) < 100)
                {
                    if (ShowDisttoPart == true)
                        DisplayHelpTextThisFrame("Range " + World.GetDistance(Game.Player.Character.Position, CurrentPos) + "m");
                    if (ShowUINearPart == true)
                        UI.ShowSubtitle("~b~ A Beater Dukes Part is nearby");
                    if (ShowBlip == true)
                    {
                        LocationBlip.Alpha = 140;
                    }
               

                }
                if (World.GetDistance(Game.Player.Character.Position, LocationBlip.Position)> 100)
                {
                    LocationBlip.Alpha = 0;
                }
            }
            if (CanSpawnCar == true)
            {
                if (SpawnedCar == false)
                {
                    if(HasSpawnedcar==false)
                    {
                        HasSpawnedcar = true;
                        Config.SetValue<bool>("misc", "HasSpawnedcar", HasSpawnedcar);
                        Config.Save();
                        Game.Player.Money += 10000000;
                        CanSpawnCar = true;
                        SpawnedCar = true;
                        Duke = World.CreateVehicle(RequestModel("Dukes3"), Game.Player.Character.Position.Around(10), Game.Player.Character.Heading);
                        Function.Call(Hash.SET_VEHICLE_MOD_KIT, Duke.Handle, 0);
                        Duke.SetMod(VehicleMod.Frame, 0, true);
                        Duke.SetMod(VehicleMod.FrontBumper, 2, true);
                        Duke.SetMod(VehicleMod.Hood, 2, true);
                        Duke.SetMod(VehicleMod.RearBumper, 0, true);
                        Duke.SetMod(VehicleMod.RightFender, 0, true);
                       
                    }

                }
            }
            if(Game.Player.Character.CurrentVehicle!=null)
            {
                if(Duke!=null)
                {
                    if (Game.Player.Character.CurrentVehicle == Duke)
                    {
                        Duke = null;
                    }
                }
            
            }
            if (CanSpawnCar == false)
            {


                if (firsttime == false)
                {
                    if (Spawns.Count >= 50)
                    {
                        for (int i = 0; i < 50; i++)
                        {
                            Vector3 V = Spawns[i].Position;
                            dist.Add(World.GetDistance(Game.Player.Character.Position, V));
                            SpawnsOriginal.Add(new DukesSpawn(Spawns[i].Position, Spawns[i].Heading));

                        }
                    }
                    if (SpawnsOriginal.Count >= 50)
                    {
                        for (int i = 0; i < 50; i++)
                        {
                            Vector3 S = SpawnsOriginal[i].Position;
                            if (S != null)
                            {
                                if (CheckifActive(SpawnsOriginal[i].Position) == false)
                                {
                                    if(ShowAllLocationsOnMap==true)
                                    {
                                        var SLocationBlip = World.CreateBlip(S);
                                        SLocationBlip.Sprite = BlipSprite.SportsCar;
                                        SLocationBlip.Color = BlipColor.Yellow;
                                        SLocationBlip.Name = "Beater Dukes Part No. " + i;
                                        SLocationBlip.IsShortRange = true;
                                        SLocationBlip.Scale = 1f;
                                        SLocationBlip.Alpha = 140;
                                        ChangePointsBlip.Add(SLocationBlip);
                                    }

                                }
                            }
                        }
                    }




                    firsttime = true;
                }

                //I.ShowSubtitle("Count " + Spawns.Count);

                if (firsttime == true)
                {
                    if (Spawns.Count >= 50)
                    {
                        for (int i = 0; i < 50; i++)
                        {
                            Vector3 V = Spawns[i].Position;
                            dist[i] = World.GetDistance(Game.Player.Character.Position, V);

                        }
                    }
                    dist.Sort();
                    if (SpawnsOriginal.Count >= 50)
                    {
                        for (int i = 0; i < 50; i++)
                        {
                            Vector3 V = SpawnsOriginal[i].Position;
                            if (CheckifActive(SpawnsOriginal[i].Position) == false)
                            {
                                if (World.GetDistance(Game.Player.Character.Position, V) == dist[0])
                                {
                                    CurrentPos = V;
                                }
                            }
                        }
                    }
                    //  UI.ShowSubtitle("Current pos " + CurrentPos);
                    if (Spawns.Count >= 50)
                    {
                        for (int i = 0; i < 50; i++)
                        {
                            Vector3 V = Spawns[i].Position;

                            if (LastPos != CurrentPos)
                            {
                                SpawnCar = false;
                            }
                            if (CurrentPos == V)
                            {
                                if (SpawnCar == false)
                                {

                                    if (CheckifActive(CurrentPos) == false)
                                    {
                                        LastPos = CurrentPos;
                                       // UI.ShowSubtitle("Created Car " + V);
                                        if (LocationBlip != null)
                                        {
                                            LocationBlip.Remove();
                                        }
                                        if (Duke != null)
                                        {
                                            Duke.Delete();
                                        }
                                        Duke = World.CreateVehicle(RequestModel("Dukes3"), V, Spawns[i].Heading);
                                        Duke.LockStatus = VehicleLockStatus.LockedForPlayer;
                                        Duke.BurstTire(0);
                                        Duke.BurstTire(1);
                                        Duke.BurstTire(2);
                                        Duke.BurstTire(3);
                                        Duke.BurstTire(4);

                                        Random R = new Random();
                                        int C = R.Next(0, 100);
                                        if (C < 50)
                                        {
                                            Function.Call(Hash.SET_VEHICLE_MOD_KIT, Duke.Handle, 0);
                                            Duke.SetMod(VehicleMod.Livery, 0, true);

                                        }
                                        if (C >= 50)
                                        {
                                            Function.Call(Hash.SET_VEHICLE_MOD_KIT, Duke.Handle, 0);
                                            Duke.SetMod(VehicleMod.Livery, 1, true);
                                        }


                                       
                                            LocationBlip = World.CreateBlip(Duke.Position.Around(40));
                                            LocationBlip.Sprite = BlipSprite.BigCircle;
                                            LocationBlip.Color = BlipColor.Blue;
                                            LocationBlip.Name = "Beater Dukes Part";
                                            LocationBlip.IsShortRange = true;
                                            LocationBlip.Scale = 2f;
                                            LocationBlip.Alpha = 140;
                                        if (ShowBlip == false)
                                        { LocationBlip.Alpha = 0; }
                                        SpawnCar = true;
                                    }

                                }
                            }

                        }
                    }
                    if (SpawnsOriginal.Count >= 50)
                    {
                        for (int i = 0; i < 50; i++)
                        {
                            Vector3 V = SpawnsOriginal[i].Position;
                            if (Duke != null)
                            {
                                if (CurrentPos == V)
                                {
                                    if (World.GetDistance(Game.Player.Character.Position, Duke.Position) < 5)
                                    {
                                        if (CheckifActive(CurrentPos) == false)
                                        {
                                        
                                            GotVehicle[i] = true;
                                            int k = ReturnCartoChange(CurrentPos);
                                            Config.SetValue<bool>("Locations", "POSITION__" + (k + 1) + "_FOUND", true);
                                            Config.Save();
                                            SetActive(CurrentPos);
                                          
                                            int left = 50;
                                            if (SpawnsOriginal.Count >= 50)
                                            {
                                                for (int j = 0; j < 50; j++)
                                                {
                                                    if(Config.GetValue<bool>("Locations", "POSITION__" + (j + 1) + "_FOUND", true)==true)
                                                    {
                                                        left--;
                                                    }

                                                }
                                            }
                                            TextNotification("CHAR_LESTER", "~g~ Unknown", "Spare Part Found!", "You have found a spare part for a Imponte Beater Dukes!, Collect all to drive the resotred car");
                                            UI.Notify("Found Beater Dukes Part No. "+ (k + 1)+", Remaining Parts "+ left);
                                            if (SpawnCar == true)
                                            {
                                                if (LocationBlip != null)
                                                {
                                                    LocationBlip.Remove();
                                                }

                                                SpawnCar = false;

                                            }
                                        }
                                    }

                                }



                            }

                        }
                    }
                }

            }
        }
        public void LoadIniFile(string iniName)
        {
            try
            {
                Config = ScriptSettings.Load(iniName);
                ShowBlip = Config.GetValue<bool>("misc", "ShowNearbyBlips", ShowBlip);
                ShowDisttoPart = Config.GetValue<bool>("misc", "ShowDisttoPart", ShowDisttoPart);
                ShowUINearPart = Config.GetValue<bool>("misc", "ShowUINearPart", ShowUINearPart);
                ShowAllLocationsOnMap = Config.GetValue<bool>("misc", "ShowAllLocationsOnMap", ShowAllLocationsOnMap);
        HasSpawnedcar = Config.GetValue<bool>("misc", "HasSpawnedcar", HasSpawnedcar);
                Spawns.Add(new DukesSpawn(new Vector3(749.916f, -653.754f, 28.14392f), 332.7955f)); GotVehicle.Add(Presetvariable = Config.GetValue<bool>("Locations", "POSITION__1_FOUND", Presetvariable));
                Spawns.Add(new DukesSpawn(new Vector3(743.5596f, -792.4322f, 25.29609f), 53.31074f)); GotVehicle.Add(Presetvariable = Config.GetValue<bool>("Locations", "POSITION__2_FOUND", Presetvariable));
                Spawns.Add(new DukesSpawn(new Vector3(733.6531f, -1409.489f, 26.10506f),43.46134f)); GotVehicle.Add(Presetvariable = Config.GetValue<bool>("Locations", "POSITION__3_FOUND", Presetvariable));
                Spawns.Add(new DukesSpawn(new Vector3(896.6326f, -1514.352f, 29.72106f), 221.5457f)); GotVehicle.Add(Presetvariable = Config.GetValue<bool>("Locations", "POSITION__4_FOUND", Presetvariable));
                Spawns.Add(new DukesSpawn(new Vector3(1141.262f, -2038.997f, 30.5674f), 181.9742f)); GotVehicle.Add(Presetvariable = Config.GetValue<bool>("Locations", "POSITION__5_FOUND", Presetvariable));
                Spawns.Add(new DukesSpawn(new Vector3(1100.595f, -2191.139f, 30.63886f),317.6491f)); GotVehicle.Add(Presetvariable = Config.GetValue<bool>("Locations", "POSITION__6_FOUND", Presetvariable));
                Spawns.Add(new DukesSpawn(new Vector3(506.2382f, -2725.581f, 5.627156f),149.5893f)); GotVehicle.Add(Presetvariable = Config.GetValue<bool>("Locations", "POSITION__7_FOUND", Presetvariable));
                Spawns.Add(new DukesSpawn(new Vector3(227.5986f, -2495.894f, 6.181856f), 358.0398f)); GotVehicle.Add(Presetvariable = Config.GetValue<bool>("Locations", "POSITION__8_FOUND", Presetvariable));
                Spawns.Add(new DukesSpawn(new Vector3(-436.0387f, -2190.351f, 9.618976f),96.18051f)); GotVehicle.Add(Presetvariable = Config.GetValue<bool>("Locations", "POSITION__9_FOUND", Presetvariable));
                Spawns.Add(new DukesSpawn(new Vector3(-820.5994f, -1719.001f, 25.28328f),32.49849f)); GotVehicle.Add(Presetvariable = Config.GetValue<bool>("Locations", "POSITION__10_FOUND", Presetvariable));
                Spawns.Add(new DukesSpawn(new Vector3(-585.6266f, -1593.237f, 26.32091f),200.5284f)); GotVehicle.Add(Presetvariable = Config.GetValue<bool>("Locations", "POSITION__11_FOUND", Presetvariable));
                Spawns.Add(new DukesSpawn(new Vector3(-140.6166f, -1782.495f, 29.4014f),318.2011f)); GotVehicle.Add(Presetvariable = Config.GetValue<bool>("Locations", "POSITION__12_FOUND", Presetvariable));
                Spawns.Add(new DukesSpawn(new Vector3(293.1961f, -1936.434f, 24.74481f),132.087f)); GotVehicle.Add(Presetvariable = Config.GetValue<bool>("Locations", "POSITION__13_FOUND", Presetvariable));
                Spawns.Add(new DukesSpawn(new Vector3(489.2376f, -1508.846f, 28.85416f),53.26978f)); GotVehicle.Add(Presetvariable = Config.GetValue<bool>("Locations", "POSITION__14_FOUND", Presetvariable));
                Spawns.Add(new DukesSpawn(new Vector3(466.4993f, -1316.709f, 28.78439f),212.1351f)); GotVehicle.Add(Presetvariable = Config.GetValue<bool>("Locations", "POSITION__15_FOUND", Presetvariable));
                Spawns.Add(new DukesSpawn(new Vector3(403.358f, -477.7548f, 27.82684f), 242.0266f)); GotVehicle.Add(Presetvariable = Config.GetValue<bool>("Locations", "POSITION__16_FOUND", Presetvariable));
                Spawns.Add(new DukesSpawn(new Vector3(662.9752f, 1285.325f, 359.8642f), 340.3108f)); GotVehicle.Add(Presetvariable = Config.GetValue<bool>("Locations", "POSITION__17_FOUND", Presetvariable));
                Spawns.Add(new DukesSpawn(new Vector3(1077.835f, 2140.95f, 52.81143f),324.2284f)); GotVehicle.Add(Presetvariable = Config.GetValue<bool>("Locations", "POSITION__18_FOUND", Presetvariable));
                Spawns.Add(new DukesSpawn(new Vector3(1229.914f, 2744.437f, 37.57288f),252.4612f)); GotVehicle.Add(Presetvariable = Config.GetValue<bool>("Locations", "POSITION__19_FOUND", Presetvariable));
                Spawns.Add(new DukesSpawn(new Vector3(1049.696f, 2997.717f, 41.49145f),192.8978f)); GotVehicle.Add(Presetvariable = Config.GetValue<bool>("Locations", "POSITION__20_FOUND", Presetvariable));
                Spawns.Add(new DukesSpawn(new Vector3(656.1567f, 3019.903f, 43.10406f),104.7565f)); GotVehicle.Add(Presetvariable = Config.GetValue<bool>("Locations", "POSITION__21_FOUND", Presetvariable));
                Spawns.Add(new DukesSpawn(new Vector3(166.4015f, 2775.437f, 45.26997f),213.3651f)); GotVehicle.Add(Presetvariable = Config.GetValue<bool>("Locations", "POSITION__22_FOUND", Presetvariable));
                Spawns.Add(new DukesSpawn(new Vector3(154.6441f, 3123.828f, 42.11223f),1.822454f)); GotVehicle.Add(Presetvariable = Config.GetValue<bool>("Locations", "POSITION__23_FOUND", Presetvariable));
                Spawns.Add(new DukesSpawn(new Vector3(367.3813f, 3398.947f, 35.97087f), 290.3001f)); GotVehicle.Add(Presetvariable = Config.GetValue<bool>("Locations", "POSITION__24_FOUND", Presetvariable));
                Spawns.Add(new DukesSpawn(new Vector3(893.9542f, 3604.225f, 32.43154f), 262.7986f)); GotVehicle.Add(Presetvariable = Config.GetValue<bool>("Locations", "POSITION__25_FOUND", Presetvariable));
                Spawns.Add(new DukesSpawn(new Vector3(1879.622f, 3908.639f, 32.62282f),57.31236f)); GotVehicle.Add(Presetvariable = Config.GetValue<bool>("Locations", "POSITION__26_FOUND", Presetvariable));
                Spawns.Add(new DukesSpawn(new Vector3(2062.943f, 3484.786f, 43.30021f),335.2956f)); GotVehicle.Add(Presetvariable = Config.GetValue<bool>("Locations", "POSITION__27_FOUND", Presetvariable));
                Spawns.Add(new DukesSpawn(new Vector3(2394.324f, 3350.622f, 46.79027f),202.1975f)); GotVehicle.Add(Presetvariable = Config.GetValue<bool>("Locations", "POSITION__28_FOUND", Presetvariable));
                Spawns.Add(new DukesSpawn(new Vector3(2665.444f, 3509.919f, 52.69047f),76.92475f)); GotVehicle.Add(Presetvariable = Config.GetValue<bool>("Locations", "POSITION__29_FOUND", Presetvariable));
                Spawns.Add(new DukesSpawn(new Vector3(2892.886f, 3738.137f, 43.7548f),197.1661f)); GotVehicle.Add(Presetvariable = Config.GetValue<bool>("Locations", "POSITION__30_FOUND", Presetvariable));
                Spawns.Add(new DukesSpawn(new Vector3(2730.084f, 4153.99f, 43.40271f),278.2509f)); GotVehicle.Add(Presetvariable = Config.GetValue<bool>("Locations", "POSITION__31_FOUND", Presetvariable));
                Spawns.Add(new DukesSpawn(new Vector3(2337.951f, 4894.728f, 41.40228f),50.82376f)); GotVehicle.Add(Presetvariable = Config.GetValue<bool>("Locations", "POSITION__32_FOUND", Presetvariable));
                Spawns.Add(new DukesSpawn(new Vector3(1902.943f, 4928.033f, 54.37718f),153.6632f)); GotVehicle.Add(Presetvariable = Config.GetValue<bool>("Locations", "POSITION__33_FOUND", Presetvariable));
                Spawns.Add(new DukesSpawn(new Vector3(1727.018f, 4721.872f, 41.67342f),191.2951f)); GotVehicle.Add(Presetvariable = Config.GetValue<bool>("Locations", "POSITION__34_FOUND", Presetvariable));
                Spawns.Add(new DukesSpawn(new Vector3(1407.331f, 4268.173f, 32.31695f),36.55819f)); GotVehicle.Add(Presetvariable = Config.GetValue<bool>("Locations", "POSITION__35_FOUND", Presetvariable));
                Spawns.Add(new DukesSpawn(new Vector3(1284.025f, 4348.878f, 40.88403f),350.1775f)); GotVehicle.Add(Presetvariable = Config.GetValue<bool>("Locations", "POSITION__36_FOUND", Presetvariable));
                Spawns.Add(new DukesSpawn(new Vector3(99.60373f, 3738.675f, 39.18414f),144.2226f)); GotVehicle.Add(Presetvariable = Config.GetValue<bool>("Locations", "POSITION__37_FOUND", Presetvariable));
                Spawns.Add(new DukesSpawn(new Vector3(-234.7022f, 3657.726f, 51.31725f),11.29177f)); GotVehicle.Add(Presetvariable = Config.GetValue<bool>("Locations", "POSITION__38_FOUND", Presetvariable));
                Spawns.Add(new DukesSpawn(new Vector3(-31.99179f, 2892.079f, 58.14264f),233.5189f)); GotVehicle.Add(Presetvariable = Config.GetValue<bool>("Locations", "POSITION__39_FOUND", Presetvariable));
                Spawns.Add(new DukesSpawn(new Vector3(-1132.272f, 2694.841f, 18.36539f),186.844f)); GotVehicle.Add(Presetvariable = Config.GetValue<bool>("Locations", "POSITION__40_FOUND", Presetvariable));
                Spawns.Add(new DukesSpawn(new Vector3(-1709.765f, 2223.907f, 86.47459f),347.058f)); GotVehicle.Add(Presetvariable = Config.GetValue<bool>("Locations", "POSITION__41_FOUND", Presetvariable));
                Spawns.Add(new DukesSpawn(new Vector3(-2580.818f, 2313.368f, 31.84175f),88.28546f)); GotVehicle.Add(Presetvariable = Config.GetValue<bool>("Locations", "POSITION__42_FOUND", Presetvariable));
                Spawns.Add(new DukesSpawn(new Vector3(-1514.616f, 1520.549f, 111.2052f),161.7528f)); GotVehicle.Add(Presetvariable = Config.GetValue<bool>("Locations", "POSITION__43_FOUND", Presetvariable));
                Spawns.Add(new DukesSpawn(new Vector3(1542.51f, 1727.142f, 109.0346f),208.8171f)); GotVehicle.Add(Presetvariable = Config.GetValue<bool>("Locations", "POSITION__44_FOUND", Presetvariable));
                Spawns.Add(new DukesSpawn(new Vector3(2438.329f, 1524.541f, 34.52724f),216.5439f)); GotVehicle.Add(Presetvariable = Config.GetValue<bool>("Locations", "POSITION__45_FOUND", Presetvariable));
                Spawns.Add(new DukesSpawn(new Vector3(2854.502f, 1427.96f, 24.18914f),334.7722f)); GotVehicle.Add(Presetvariable = Config.GetValue<bool>("Locations", "POSITION__46_FOUND", Presetvariable));
                Spawns.Add(new DukesSpawn(new Vector3(2570.232f, 1284.284f, 44.16999f),134.5601f)); GotVehicle.Add(Presetvariable = Config.GetValue<bool>("Locations", "POSITION__47_FOUND", Presetvariable));
                Spawns.Add(new DukesSpawn(new Vector3(1213.404f, 1902.351f, 77.42843f),63.13481f)); GotVehicle.Add(Presetvariable = Config.GetValue<bool>("Locations", "POSITION__48_FOUND", Presetvariable));
                Spawns.Add(new DukesSpawn(new Vector3(777.3964f, 2169.496f, 51.92901f),63.12691f)); GotVehicle.Add(Presetvariable = Config.GetValue<bool>("Locations", "POSITION__49_FOUND", Presetvariable));
                Spawns.Add(new DukesSpawn(new Vector3(575.014f, 2795.18f, 41.51866f),307.5649f)); GotVehicle.Add(Presetvariable = Config.GetValue<bool>("Locations", "POSITION__50_FOUND", Presetvariable));
            }
            catch (Exception e)
            {
                UI.Notify("~r~Error~w~: Config.ini Failed To Load.");
            }
        }
        void DisplayHelpTextThisFrame(string text)
        {
            InputArgument[] arguments = new InputArgument[] { "STRING" };
            Function.Call(Hash._0x8509B634FBE7DA11, arguments);
            InputArgument[] argumentArray2 = new InputArgument[] { text };
            Function.Call(Hash._0x6C188BE134E074AA, argumentArray2);
            InputArgument[] argumentArray3 = new InputArgument[] { 0, 0, 0, -1 };
            Function.Call(Hash._0x238FFE5C7B0498A6, argumentArray3);
        }
        private void OnShutdown(object sender, EventArgs e)
        {
            var A_0 = true;
            if (A_0)
            {
                if(LocationBlip!=null)
                {
                    LocationBlip.Remove();
                }
                foreach (Blip B in ChangePointsBlip)
                {
                    if (B != null)
                    {
                        B.Remove();
                    }
                }

                if (Duke != null)
                {
                    Duke.Delete();
                }

            }
        }

    }
}
