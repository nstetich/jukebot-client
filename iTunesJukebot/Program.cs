using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using iTunesLib;

namespace iTunesJukebot
{
    class Program
    {
        static void Main(string[] args)
        {
            var iTunes = new iTunesApp();
            System.Console.Out.WriteLine("Tracks in playlist:");
            foreach (var itrack in iTunes.LibraryPlaylist.Tracks)
            {
                var track = (IITFileOrCDTrack) itrack;
                System.Console.Out.WriteLine("* {0}", track.Name);
            }

            System.Console.Out.WriteLine("Waiting for events.");
            iTunes.OnPlayerPlayEvent += (obj) =>
            {
                var track = (IITFileOrCDTrack) obj;
                System.Console.Out.WriteLine("now playing: {0} by {1}", track.Name, track.Artist);
            };

            WaitForInterrupt();
        }

        private static void WaitForInterrupt()
        {
            var resetEvent = new ManualResetEvent(false);

            Console.CancelKeyPress += (sender, eventArgs) =>
            {
                resetEvent.Set();
                eventArgs.Cancel = true;
            };

            resetEvent.WaitOne();
        }
    }
}
