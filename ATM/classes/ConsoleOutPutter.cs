using ATM.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ATM.classes;
using ATM.EventArgsClasses;
using Castle.Core.Internal;

namespace ATM.classes
{
    public class ConsoleOutPutter : IConsoleOutPutter
    {

        public static int ticks = 0;
        public IAnalyser _Analyser { get; set; }

        
        // Many Threads can access these
        // Therefore it is nessasary to lock when when add or remove
        private object enterLock = new object();
        private object ExitLock = new object();


        List<SeparationAircraftsData> SeparationTasks = new List<SeparationAircraftsData>();
        List<AircraftData> TrackEnteredAirSpaceTasks = new List<AircraftData>();
        List<AircraftData> TrackLeftAirSpaceTasks = new List<AircraftData>();

        public ConsoleOutPutter(IAnalyser analyser)
        {
            _Analyser = analyser;

            _Analyser.AnalysedDataReadyEvent += OutPutAircraftDataEventHandler;
            _Analyser.SeparationEvent += OutputSeparationTasks;
            _Analyser.TrackEnteredAirSpaceEvent += OutputTrackEnteredAirSpaceEventHandler;
            _Analyser.TrackLeftAirSpaceEvent += OutputTrackLeftAirSpaceEventHandler;

        }
        
        public void ClearConsole()
        {
            Console.Clear();
        }

        public void OutPutAircraftDataEventHandler(object o, AnalysedTransponderDataEventArgs e)
        {
            OutPutAircraftsWithinArea(e._AircraftData);
        }

        public void OutPutAircraftsWithinArea(List<AircraftData> aircraftData)
        {
            Console.CursorVisible = false;
            ClearConsole();
            Console.WriteLine($"Received transponder data, count : {ticks}");
            
            if (!SeparationTasks.IsNullOrEmpty())
            {
                Console.WriteLine("\nSEPARATION EVENTS:");
            }
            Console.ForegroundColor = ConsoleColor.Red;
            foreach (var item in SeparationTasks)
            {
                Console.WriteLine($"WARNING! Possible collision between flight {item.FirstAircraft.Tag} " +
                                  $"and {item.SecondAircraft.Tag}. {item.FirstAircraft.TimeStamp}");
            }
            SeparationTasks.Clear();
            Console.ResetColor();
            
            if (!TrackEnteredAirSpaceTasks.IsNullOrEmpty())
            {
                Console.WriteLine("\nAIRCRAFTS ENTERED AIR SPACE:");
            }
            Console.ForegroundColor = ConsoleColor.Green;
            foreach (var item in TrackEnteredAirSpaceTasks)
            {
                Console.WriteLine(item.ToString());
            }
            Console.ResetColor();

            
            if (!TrackLeftAirSpaceTasks.IsNullOrEmpty())
            {
                Console.WriteLine("\nAIRCRAFTS LEFT AIR SPACE:");
            }
            Console.ForegroundColor = ConsoleColor.Yellow;
            foreach (var item in TrackLeftAirSpaceTasks)
            {
                Console.WriteLine(item.ToString());
            }

            Console.ResetColor();


            //Console.ForegroundColor = ConsoleColor.Red;
            //Console.WriteLine($"WARNING! Possible collision between flight {_FilteredAircrafts[i].Tag} " +
            //                  $"and {_FilteredAircrafts[j].Tag}.");
            //Console.ResetColor();

            Console.WriteLine("\nAIRCRAFTS INSIDE AIR SPACE:");
            foreach (var item in aircraftData)
            {
                Console.WriteLine(item.ToString());
            }

            ticks++;
            Console.SetCursorPosition(0,0);


        }

        public void OutputSeparationTasks(object o, SeparationAircraftsData sepTracks)
        {
            SeparationTasks.Add(sepTracks);
        }

        public void OutputTrackEnteredAirSpaceEventHandler(object o, AircraftData track)
        {
            Thread t1 = new Thread(new ThreadStart(() =>
            {
                lock (enterLock)
                {
                    TrackEnteredAirSpaceTasks.Add(track);
                }

                Thread.Sleep(5000);

                lock (enterLock)
                {

                    TrackEnteredAirSpaceTasks.Remove(track);
                }
                
            } ));

            t1.Start();
        }

        public void OutputTrackLeftAirSpaceEventHandler(object o, AircraftData track)
        {
            Thread t1 = new Thread(new ThreadStart(() =>
            {
                lock (ExitLock)
                {
                    TrackLeftAirSpaceTasks.Add(track);
                }

                Thread.Sleep(5000);

                lock (ExitLock)
                {
                    TrackLeftAirSpaceTasks.Remove(track);
                }
            }));

            t1.Start();
        }

        
    }
}
